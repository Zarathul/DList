using System;

namespace InCoding.DList.Collections
{
    public class NotifyingCollectionChangingEventArgs : NotifyingCollectionChangedEventArgs
    {
        public bool Cancel { get; set; }

        public NotifyingCollectionChangingEventArgs(NotifyingCollectionChangeAction action) : base(action)
        {
        }

        public NotifyingCollectionChangingEventArgs(NotifyingCollectionChangeAction action, object newItem, int newItemIndex, object oldItem, int oldItemIndex)
            : base(action, newItem, newItemIndex, oldItem, oldItemIndex)
        {
        }
    }
}
