using System;

namespace InCoding.DList.Editing
{
    public class CellEditorDoneEventArgs : EventArgs
    {
        public bool Success { get; }
        public int ColumnIndex { get; }
        public int ItemIndex { get; }
        public object NewValue { get; }

        public CellEditorDoneEventArgs(bool success, int columnIndex, int itemIndex, object newValue)
        {
            if (columnIndex < 0) throw new ArgumentNullException(nameof(columnIndex));
            if (itemIndex < 0) throw new ArgumentNullException(nameof(itemIndex));

            Success = success;
            ColumnIndex = columnIndex;
            ItemIndex = itemIndex;
            NewValue = newValue ?? throw new ArgumentNullException(nameof(newValue));
        }
    }
}
