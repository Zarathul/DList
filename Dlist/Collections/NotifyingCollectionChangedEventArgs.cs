using System;

namespace InCoding.DList.Collections
{
    public enum ItemCollectionChangeAction
    {
        Add,
        Remove,
        Replace,
        Clear
    };

    public class NotifyingCollectionChangedEventArgs : EventArgs
    {
        public ItemCollectionChangeAction Action { get; }
        public object NewItem { get; }
        public object OldItem { get; }
        public int NewItemIndex { get; }
        public int OldItemIndex { get; }

        public NotifyingCollectionChangedEventArgs(ItemCollectionChangeAction action)
            : this(action, null, 0, null, 0)
        {
        }

        public NotifyingCollectionChangedEventArgs(ItemCollectionChangeAction action, object newItem, int newItemIndex, object oldItem, int oldItemIndex)
        {
            Action = action;
            NewItem = newItem;
            NewItemIndex = newItemIndex;
            OldItem = oldItem;
            OldItemIndex = oldItemIndex;
        }
    }
}
