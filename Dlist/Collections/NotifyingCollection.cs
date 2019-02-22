using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InCoding.DList.Collections
{
    public class NotifyingCollection<T> : Collection<T>
    {
        public event EventHandler<NotifyingCollectionChangedEventArgs> CollectionChanged;
        public event EventHandler<ItemPropertyChangedEventArgs> ItemChanged;

        public NotifyingCollection()
        {
        }

        protected override void ClearItems()
        {
            foreach (var item in Items)
            {
                var Iface = item as INotifyPropertyChanged;

                if (Iface != null)
                {
                    Iface.PropertyChanged -= HandleItemNotification;
                }
            }

            base.ClearItems();

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(ItemCollectionChangeAction.Clear);
            OnCollectionChanged(Args);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            var Iface = item as INotifyPropertyChanged;

            if (Iface != null)
            {
                Iface.PropertyChanged += HandleItemNotification;
            }

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(ItemCollectionChangeAction.Add, item, index, null, 0);
            OnCollectionChanged(Args);
        }

        protected override void RemoveItem(int index)
        {
            var Item = Items[index];

            base.RemoveItem(index);

            var Iface = Item as INotifyPropertyChanged;

            if (Iface != null)
            {
                Iface.PropertyChanged -= HandleItemNotification;
            }

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(ItemCollectionChangeAction.Remove, null, 0, Item, index);
            OnCollectionChanged(Args);
        }

        protected override void SetItem(int index, T item)
        {
            var OldItem = Items[index];

            base.SetItem(index, item);

            var OldIface = OldItem as INotifyPropertyChanged;

            if (OldIface != null)
            {
                OldIface.PropertyChanged -= HandleItemNotification;
            }

            var Iface = item as INotifyPropertyChanged;

            if (Iface != null)
            {
                Iface.PropertyChanged += HandleItemNotification;
            }

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(ItemCollectionChangeAction.Replace, item, index, OldItem, index);
            OnCollectionChanged(Args);
        }

        protected void OnItemChanged(ItemPropertyChangedEventArgs args)
        {
            var Handler = ItemChanged;
            Handler?.Invoke(this, args);
        }

        protected void OnCollectionChanged(NotifyingCollectionChangedEventArgs args)
        {
            var Handler = CollectionChanged;
            Handler?.Invoke(this, args);
        }

        private void HandleItemNotification(object sender, PropertyChangedEventArgs args)
        {
            var Args = new ItemPropertyChangedEventArgs(sender, args.PropertyName);
            OnItemChanged(Args);
        }
    }
}
