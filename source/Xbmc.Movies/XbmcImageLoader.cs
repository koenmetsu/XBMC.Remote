namespace Xbmc_Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    using Xbmc.Client;

    public class XbmcImageLoader
    {
        private static bool Exit = false;
        private static readonly Thread Thread = new Thread(Worker);
        private static readonly Queue<PendingRequest> PendingRequests = new Queue<PendingRequest>();
        private static readonly object SyncBlock = new object();
        private static XbmcClient Client = new XbmcClient("http://FENPC100:8081/", executeCallbackOnUIThread: false);

        static XbmcImageLoader()
        {
            Thread.Start();
            Application.Current.Exit += new EventHandler(HandleApplicationExit);
        }

        public static readonly DependencyProperty UriSourceProperty = DependencyProperty.RegisterAttached("UriSource", typeof(Uri), typeof(XbmcImageLoader), new PropertyMetadata(OnUriSourceChanged));

        public static Uri GetUriSource(Image obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return (Uri)obj.GetValue(UriSourceProperty);
        }

        public static void SetUriSource(Image obj, Uri value)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            obj.SetValue(UriSourceProperty, value);
        }

        private static void OnUriSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("OnUriSourceChanged");
            var image = (Image)d;
            var uri = (Uri)e.NewValue;

            // There is no image
            if (uri == null)
            {
                image.Source = null;
                return;
            }

            if (DesignerProperties.IsInDesignTool)
            {
                // Avoid handing off to the worker thread (can cause problems for design tools)
                image.Source = new BitmapImage(uri);
            }
            else
            {
                // Clear-out the current image because it's now stale (helps when used with virtualization)
                image.Source = null;
                lock (SyncBlock)
                {
                    // Enqueue the request
                    PendingRequests.Enqueue(new PendingRequest(image, uri));
                    Monitor.Pulse(SyncBlock);
                }
            }
        }

        private static void Worker(object o)
        {
            var random = new Random();
            var pendingRequests = new List<PendingRequest>();
            while (!Exit)
            {
                lock (SyncBlock)
                {
                    if (PendingRequests.Count == 0 && pendingRequests.Count == 0)
                    {
                        Monitor.Wait(SyncBlock);
                        if (Exit)
                        {
                            return;
                        }
                    }

                    while (PendingRequests.Count > 0)
                    {
                        var pendingRequest = PendingRequests.Dequeue();

                        for (int i = 0; i < pendingRequests.Count; i++)
                        {
                            if (pendingRequests[i].Image == pendingRequest.Image)
                            {
                                pendingRequests[i] = pendingRequest;
                                pendingRequest = null;
                                break;
                            }
                        }

                        if (pendingRequest != null)
                        {
                            pendingRequests.Add(pendingRequest);
                        }
                    }
                }

                Queue<PendingCompletion> pendingCompletions = new Queue<PendingCompletion>();
                const int MaxWorkLoad = 5;
                int count = pendingRequests.Count;
                for (int i = 0; (count > 0) && (i < MaxWorkLoad); i++)
                {
                    int index = random.Next(count);
                    var request = pendingRequests[index];
                    pendingRequests[index] = pendingRequests[count - 1];
                    pendingRequests.RemoveAt(count - 1);
                    count--;

                    Client.Vfs.GetFile(request.Uri, (bytes, exception) =>
                        {
                            if (exception == null)
                            {
                                pendingCompletions.Enqueue(new PendingCompletion(request.Image, request.Uri, bytes));
                            }
                            Thread.Sleep(1);
                        });
                    Thread.Sleep(1);
                }

                if (pendingCompletions.Count > 0)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            while (0 < pendingCompletions.Count)
                            {
                                // Decode the image and set the source
                                var pendingCompletion = pendingCompletions.Dequeue();
                                if (GetUriSource(pendingCompletion.Image) == pendingCompletion.Uri)
                                {
                                    var bitmap = new BitmapImage();
                                    try
                                    {
                                        using (var stream = new MemoryStream(pendingCompletion.Data))
                                        {
                                            bitmap.SetSource(stream);
                                        }
                                    }
                                    catch
                                    {
                                        // Crappy image
                                    }

                                    Debug.WriteLine("Set image");
                                    pendingCompletion.Image.Source = bitmap;
                                }
                            }
                        });
                }
            }
        }

        private static void HandleApplicationExit(object sender, EventArgs e)
        {
            Exit = true;
            if (Monitor.TryEnter(SyncBlock, 100))
            {
                Monitor.Pulse(SyncBlock);
                Monitor.Exit(SyncBlock);
            }
        }

        private class PendingRequest
        {
            public Image Image { get; set; }

            public Uri Uri { get; set; }

            public PendingRequest(Image image, Uri uri)
            {
                Image = image;
                Uri = uri;
            }
        }

        private class PendingCompletion
        {
            public Image Image { get; private set; }

            public Uri Uri { get; private set; }
            
            public byte[] Data { get; private set; }
            
            public PendingCompletion(Image image, Uri uri, byte[] data)
            {
                Image = image;
                Uri = uri;
                Data = data;
            }
        }
    }
}