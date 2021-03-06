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

namespace InCoding.DList.Collections
{
    public enum NotifyingCollectionChangeAction
    {
        Add,
        AddRange,
        Remove,
        RemoveRange,
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
