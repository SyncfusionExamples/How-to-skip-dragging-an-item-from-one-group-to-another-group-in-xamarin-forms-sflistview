using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DragAndDrop
{
    public partial class MainPage : ContentPage
    {
        private ScrollAxisBase scrollRows;
        private VisualContainer visualContainer;
        public MainPage()
        {
            InitializeComponent();
            this.listView.ItemDragging += ListView_ItemDragging;
            this.listView.Loaded += ListView_Loaded;
        }

        private void ListView_Loaded(object sender, ListViewLoadedEventArgs e)
        {
            listView.CollapseAll();
            var group = listView.DataSource.Groups[2];
           listView.ExpandGroup(group);
        }

        private void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Dragging)
            {
                var currentGroup = this.GetGroup(e.ItemData);
                var container = this.listView.GetVisualContainer();
                var groupIndex = this.listView.DataSource.Groups.IndexOf(currentGroup);
                var nextGroup = (groupIndex + 1 < this.listView.DataSource.Groups.Count) ? this.listView.DataSource.Groups[groupIndex + 1] : null;
                ListViewItem groupItem = null;
                ListViewItem nextGroupItem = null;

                foreach (ListViewItem item in container.Children)
                {
                    if (item.BindingContext == null || !item.Visibility)
                        continue;

                    if (item.BindingContext.Equals(currentGroup))
                        groupItem = item;

                    if (nextGroup != null && item.BindingContext.Equals(nextGroup))
                        nextGroupItem = item;
                }

                if (groupItem != null && e.Bounds.Y <= groupItem.Y + groupItem.Height || nextGroupItem != null && (e.Bounds.Y + e.Bounds.Height >= nextGroupItem.Y))
                    e.Handled = true;
            }
        }

        private GroupResult GetGroup(object itemData)
        {
            GroupResult itemGroup = null;

            foreach (var item in this.listView.DataSource.DisplayItems)
            {
                if (item is GroupResult)
                    itemGroup = item as GroupResult;

                if (item == itemData)
                    break;
            }
            return itemGroup;
        }
    }
}
