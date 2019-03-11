
namespace InCoding.DList.Editing
{
    public class BooleanCellEditor : CellEditorBase
    {
        public BooleanCellEditor() : base(null, false)
        {
        }

        protected override void EditInternal(int columnIndex, int itemIndex, object value)
        {
            EditDone();
        }

        protected override object GetResultValue()
        {
            return !((bool)Value);
        }
    }
}
