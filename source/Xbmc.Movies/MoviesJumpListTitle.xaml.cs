using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace Xbmc_Movies
{
    using System.ComponentModel;
    using System.Threading;

    public class ListExampleBase : UserControl
    {
        private bool isDisplayed;
        private bool isLoaded;
        private IEnumerable itemsSource;

        public ListExampleBase()
        {
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        public bool IsDisplayed
        {
            get
            {
                return this.isDisplayed;
            }
        }

        protected virtual RadDataBoundListBox ListBox
        {
            get
            {
                return null;
            }
        }

        protected virtual string ExampleName
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual bool EnableAsyncBalance
        {
            get
            {
                return true;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.isLoaded = true;
            if (DesignerProperties.IsInDesignTool)
            {
                return;
            }

            if (this.ListBox != null)
            {
                // enable async balance upon listbox loading
                this.ListBox.IsAsyncBalanceEnabled = this.EnableAsyncBalance;
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.isLoaded = false;
        }

        protected virtual void OnDisplayed()
        {
            if (this.ListBox == null)
            {
                return;
            }

            if (this.itemsSource == null)
            {
                ThreadPool.QueueUserWorkItem(this.LoadItemsSource);
            }
            else
            {
                this.DisplayData(this.itemsSource);
            }
        }

        protected virtual void OnHidden()
        {
            if (this.ListBox != null)
            {
                this.Dispatcher.BeginInvoke(() =>
                {
                    this.ListBox.ItemsSource = null;
                });
            }
        }

        private void LoadItemsSource(object state)
        {
            this.itemsSource = this.CreateItemsSource();
            if (this.isDisplayed)
            {
                this.DisplayData(this.itemsSource);
            }
        }

        /// <summary>
        /// Core items source creation method. Note that it is on a worker thread.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable CreateItemsSource()
        {
            return null;
        }

        protected virtual void DisplayData(IEnumerable items)
        {
            if (this.ListBox == null || !this.isDisplayed)
            {
                return;
            }

            this.Dispatcher.BeginInvoke(() =>
            {
                this.ListBox.BeginAsyncBalance();
                this.ListBox.ItemsSource = items;
            });
        }
    }


    public partial class MoviesJumpListTitle : UserControl
	{
		public MoviesJumpListTitle()
		{
			// Required to initialize variables
			InitializeComponent();

            this.jumpList.GroupHeaderItemTap += this.jumpList_GroupHeaderItemTap;
		}
		
        protected RadDataBoundListBox ListBox
        {
            get
            {
                return this.jumpList;
            }
        }

        private void jumpList_GroupHeaderItemTap(object sender, GroupHeaderItemTapEventArgs e)
        {
            Image image = e.ManipulationContainer as Image;
            if (image == null)
            {
                return;
            }

            if ((string)image.Tag == "up")
            {
                this.MoveUp(e);
            }
            else if ((string)image.Tag == "down")
            {
                this.MoveDown(e);
            }
        }

        private void MoveDown(GroupHeaderItemTapEventArgs e)
        {
            DataGroup[] groups = this.jumpList.Groups;
            int index = this.IndexOfGroup(groups, e.Item);
            index++;
            index = Math.Min(groups.Length - 1, index);
            this.jumpList.BringIntoView(groups[index]);

            e.ShowGroupPicker = false;
        }

        private void MoveUp(GroupHeaderItemTapEventArgs e)
        {
            DataGroup[] groups = this.jumpList.Groups;
            int index = this.IndexOfGroup(groups, e.Item);

            // find the first non-visible group on top of the tapped one
            while (true)
            {
                index--;
                if (index < 0)
                {
                    break;
                }

                if (!this.jumpList.IsItemInViewport(groups[index]))
                {
                    break;
                }
            }

            if (index >= 0)
            {
                this.jumpList.BringIntoView(groups[index]);
            }

            e.ShowGroupPicker = false;
        }

        private int IndexOfGroup(DataGroup[] groups, RadDataBoundListBoxItem item)
        {
            DataGroup hitGroup = item.Content as DataGroup;

            for (int i = 0; i < groups.Length; i++)
            {
                if (groups[i] == hitGroup)
                {
                    return i;
                }
            }

            return -1;
        }		
	}
}