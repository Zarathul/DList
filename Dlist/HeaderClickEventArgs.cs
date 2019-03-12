using System;

namespace InCoding.DList
{
    public class HeaderClickEventArgs : EventArgs
    {
        public int Index { get; }

        public HeaderClickEventArgs(int index)
        {
            Index = index;
        }
    }
}
