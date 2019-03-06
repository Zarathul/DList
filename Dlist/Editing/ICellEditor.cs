using System;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Editing
{
    public interface ICellEditor : IDisposable
    {
        Control EditorControl { get; }
        event EventHandler<CellEditorDoneEventArgs> Done;

        void Edit(Rectangle cellBounds, int columnIndex, int itemIndex, object value);
        void Cancel();
    }
}
