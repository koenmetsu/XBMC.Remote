namespace Sysmeta.Xbmc.Remote.Services
{
    using System;
    using System.Linq;

    public class DemoVfs : IVfs
    {
        public void GetFile(Uri uri, Action<byte[], Exception> callback)
        {
            string resource = string.Format("Sysmeta.Xbmc.Remote.Images.Demo.{0}", uri.AbsolutePath.Split('/').Last());

            var names = typeof(DemoVfs).Assembly.GetManifestResourceStream(resource);
            if (names != null)
            {
                using (names)
                {
                    int length = (int)names.Length;
                    var buffer = new byte[length];
                    names.Read(buffer, 0, length);
                    callback(buffer, null);
                }
            }
            else
            {
                callback(null, null);
            }
        }
    }
}