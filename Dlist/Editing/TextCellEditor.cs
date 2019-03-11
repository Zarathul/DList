using System.Windows.Forms;

namespace InCoding.DList.Editing
{
    public class TextCellEditor : CellEditorBase
    {
        private TextBox _TextBox;

        public TextBox TextBox { get => _TextBox; }

        public TextCellEditor(HorizontalAlignment textAlignment = HorizontalAlignment.Center) : base(new TextBox())
        {
            _TextBox = (TextBox)EditorControl;
            _TextBox.TextAlign = textAlignment;
        }

        protected override void EditInternal(int columnIndex, int itemIndex, object value)
        {
            _TextBox.Text = value.ToString();
            _TextBox.SelectAll();
        }

        protected override object GetResultValue()
        {
            return _TextBox.Text;
        }
    }
}
