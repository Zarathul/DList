﻿/*
 * Copyright 2019 Zarathul
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace InCoding.DList.Collections
{
    public class NotifyingCollection<T> : Collection<T>
    {
        private List<T> _ItemList;
        private HashSet<T> _Set;

        public event EventHandler<NotifyingCollectionChangedEventArgs> CollectionChanged;
        public event EventHandler<NotifyingCollectionChangingEventArgs> CollectionChanging;
        public event EventHandler<ItemPropertyChangedEventArgs> ItemChanged;

        public NotifyingCollection() : this(new List<T>())
        {
        }

        public NotifyingCollection(List<T> list) : base(list)
        {
            _ItemList = list;
            _Set = new HashSet<T>(list);
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            int FirstEmptyIndex = Items.Count;

            foreach (var item in items)
            {
                InsertItem(FirstEmptyIndex, item);
                FirstEmptyIndex++;
            }
        }

        public void RemoveAt(IEnumerable<int> indices)
        {
            if (indices == null) throw new ArgumentNullException(nameof(indices));

            var SortedIndices = indices.Distinct().OrderBy(index => index).ToArray();

            if (SortedIndices.Length == 0) return;
            if (SortedIndices[0] < 0 || SortedIndices[SortedIndices.Length - 1] >= Count) throw new ArgumentOutOfRangeException(nameof(indices));

            for (int i = SortedIndices.Length - 1; i >= 0; i--)
            {
                RemoveAt(SortedIndices[i]);
            }
        }

        public void Sort()
        {
            _ItemList.Sort();
            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Sort);
            OnCollectionChanged(Args);
        }

        public new bool Contains(T item)
        {
            if (item == null) return false;

            return _Set.Contains(item);
        }

        protected override void ClearItems()
        {
            if (Items.Count == 0) return;

            var ChangingArgs = new NotifyingCollectionChangingEventArgs(NotifyingCollectionChangeAction.Clear);
            OnCollectionChanging(ChangingArgs);

            if (ChangingArgs.Cancel) return;

            foreach (var item in Items)
            {
                if (item is INotifyPropertyChanged Iface)
                {
                    Iface.PropertyChanged -= HandleItemNotification;
                }
            }

            _Set.Clear();
            base.ClearItems();

            var ChangedArgs = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Clear);
            OnCollectionChanged(ChangedArgs);
        }

        protected override void InsertItem(int index, T item)
        {
            var ChangingArgs = new NotifyingCollectionChangingEventArgs(NotifyingCollectionChangeAction.Add, item, index, null, -1);
            OnCollectionChanging(ChangingArgs);

            if (ChangingArgs.Cancel) return;

            if (_Set.Add(item))
            {
                base.InsertItem(index, item);

                if (item is INotifyPropertyChanged Iface)
                {
                    Iface.PropertyChanged += HandleItemNotification;
                }

                NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Add, item, index, null, -1);
                OnCollectionChanged(Args);
            }
        }

        protected override void RemoveItem(int index)
        {
            var Item = Items[index];
            var ChangingArgs = new NotifyingCollectionChangingEventArgs(NotifyingCollectionChangeAction.Remove, null, -1, Item, index);
            OnCollectionChanging(ChangingArgs);

            if (ChangingArgs.Cancel) return;

            _Set.Remove(Item);
            base.RemoveItem(index);

            if (Item is INotifyPropertyChanged Iface)
            {
                Iface.PropertyChanged -= HandleItemNotification;
            }

            NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Remove, null, -1, Item, index);
            OnCollectionChanged(Args);
        }

        protected override void SetItem(int index, T item)
        {
            var OldItem = Items[index];
            var ChangingArgs = new NotifyingCollectionChangingEventArgs(NotifyingCollectionChangeAction.Replace, item, index, OldItem, index);
            OnCollectionChanging(ChangingArgs);

            if (ChangingArgs.Cancel) return;

            if (_Set.Add(item))
            {
                _Set.Remove(OldItem);
                base.SetItem(index, item);

                if (OldItem is INotifyPropertyChanged OldIface)
                {
                    OldIface.PropertyChanged -= HandleItemNotification;
                }

                if (item is INotifyPropertyChanged Iface)
                {
                    Iface.PropertyChanged += HandleItemNotification;
                }

                NotifyingCollectionChangedEventArgs Args = new NotifyingCollectionChangedEventArgs(NotifyingCollectionChangeAction.Replace, item, index, OldItem, index);
                OnCollectionChanged(Args);
            }
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

        protected void OnCollectionChanging(NotifyingCollectionChangingEventArgs args)
        {
            var Handler = CollectionChanging;
            Handler?.Invoke(this, args);
        }

        private void HandleItemNotification(object sender, PropertyChangedEventArgs args)
        {
            var Args = new ItemPropertyChangedEventArgs(sender, args.PropertyName);
            OnItemChanged(Args);
        }
    }
}
