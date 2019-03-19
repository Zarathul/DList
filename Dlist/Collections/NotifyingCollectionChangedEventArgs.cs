using System;

namespace InCoding.DList.Collections
{
    public enum NotifyingCollectionChangeAction
    {
        Add,
        Remove,
        Replace,
        Clear,
        Sort
    };

    public class NotifyingCollectionChangedEventArgs : EventArgs
    {
        public NotifyingCollectionChangeAction Action { get; }
        public object NewItem { get; }
        public object OldItem { get; }
        public int NewItemIndex { get; }
        public int OldItemIndex { get; }

        public NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction action)
            : this(action, null, -1, null, -1)
        {
        }

        public NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction action, object newItem, int newItemIndex, object oldItem, int oldItemIndex)
        {
            Action = action;
            NewItem = newItem;
            NewItemIndex = newItemIndex;
            OldItem = oldItem;
            OldItemIndex = oldItemIndex;
        }
    }
}
