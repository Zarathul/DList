using System;

namespace InCoding.DList.Collections
{
    public class ItemPropertyChangedEventArgs : EventArgs
    {
        public object Item { get; }
        public string PropertyName { get; }

        public ItemPropertyChangedEventArgs(object item, string propertyName)
        {
            Item = item;
            PropertyName = propertyName;
        }
    }
}
