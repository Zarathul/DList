using System;

namespace InCoding.DList
{
    public class CellClickEventArgs : EventArgs
    {
        public int ColumnIndex { get; }
        public int ItemIndex { get; }

        public CellClickEventArgs(int columnIndex, int itemIndex)
        {
            ColumnIndex = columnIndex;
            ItemIndex = itemIndex;
        }
    }
}
