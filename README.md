# How-to-skip-dragging-an-item-from-one-group-to-another-group-in-xamarin-forms-sflistview-

To skip dragging from one group to another group, handle the [ItemDragging](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemDragging_EV.html) event based on the conditions of [Action](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ItemDraggingEventArgs~Action.html) and `Bounds` event arguments.

N> While auto-scrolling, dragging item cannot be skipped.

Skip the dragging item by bounds of dragging item, and bounds of current and next group item.

```
using Syncfusion.ListView.XForms.Control.Helpers;
private async void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
{
  if (e.Action == DragAction.Dragging)
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

  foreach (var item in this.listView.DataSource.DisplayItems)
  {
    if (item is GroupResult)
      itemGroup = item as GroupResult;

    if (item == itemData)
      break;
  }
  return itemGroup;
}
```
