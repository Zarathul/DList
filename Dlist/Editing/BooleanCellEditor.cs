using System.Drawing;

namespace InCoding.DList.Editing
{
    public class BooleanCellEditor : CellEditorBase
    {
        public BooleanCellEditor()
        {
        }

        public override void BeginEdit(Rectangle cellBounds, int columnIndex, int itemIndex, object value)
        {
            bool NewValue = !((bool)value);
            var Args = new CellEditorDoneEventArgs(true, columnIndex, itemIndex, NewValue);
            OnDone(Args);
        }

        public override void Cancel()
        {
        }
    }
}
