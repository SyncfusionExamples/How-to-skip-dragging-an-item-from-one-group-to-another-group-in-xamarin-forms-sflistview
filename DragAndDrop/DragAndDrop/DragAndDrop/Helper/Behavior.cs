using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DragAndDrop
{
   public class Behavior :Behavior<SfListView>
    { 
        #region Fields
        private ScrollAxisBase scrollRows;
        private VisualContainer visualContainer;
        private SfListView ListView;

        #endregion
        #region Overrides
        protected override void OnAttachedTo(SfListView listview)
        {
            ListView = listview;
            ListView.ItemDragging += ListView_ItemDragging;
            base.OnAttachedTo(listview);
        }

        protected override void OnDetachingFrom(SfListView listview)
        {
            listview.ItemDragging -= ListView_ItemDragging;
            base.OnDetachingFrom(listview);
        }
        #endregion
        #region CallBacks
        private void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if(e.Action == DragAction.Dragging)
            {
                var currentGroup = this.GetGroup(e.ItemData);
                var container = this.ListView.GetVisualContainer();
                var groupIndex = this.ListView.DataSource.Groups.IndexOf(currentGroup);
                var nextGroup = (groupIndex + 1 < this.ListView.DataSource.Groups.Count) ? this.ListView.DataSource.Groups[groupIndex + 1] : null;
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

            foreach (var item in this.ListView.DataSource.DisplayItems)
            {
                if (item is GroupResult)
                    itemGroup = item as GroupResult;

                if (item == itemData)
                    break;
            }
            return itemGroup;
        }
        #endregion
    }
}
