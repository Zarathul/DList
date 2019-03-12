using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InCoding.DList.Collections
{
    public class NotifyingCollection<T> : Collection<T>
    {
        private List<T> _ItemList;

        public event EventHandler<NotifyingCollectionChangedEventArgs> CollectionChanged;
        public event EventHandler<ItemPropertyChangedEventArgs> ItemChanged;

        public NotifyingCollection() : this(new List<T>())
        {
        }

        public NotifyingCollection(List<T> list) : base(list)
        {
            _ItemList = list;
        }

        public void AddRange(IEnumerable<T> items)
        {
            int FirstEmptyIndex = Items.Count;

            foreach (var item in items)
            {
                InsertItem(FirstEmptyIndex, item);
                FirstEmptyIndex++;
            }
        }

        public void Sort()
        {
            _ItemList.Sort();
            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Sort);
            OnCollectionChanged(Args);
        }

        protected override void ClearItems()
        {
            if (Items.Count == 0) return;

            foreach (var item in Items)
            {
                var Iface = item as INotifyPropertyChanged;

                if (Iface != null)
                {
                    Iface.PropertyChanged -= HandleItemNotification;
                }
            }

            base.ClearItems();

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Clear);
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

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Add, item, index, null, 0);
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

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Remove, null, 0, Item, index);
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

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Replace, item, index, OldItem, index);
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
