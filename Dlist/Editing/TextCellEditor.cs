using System.Windows.Forms;

namespace InCoding.DList.Editing
{
    public class TextCellEditor : CellEditorBase
    {
        private TextBox TBox { get; }

        public TextCellEditor(HorizontalAlignment textAlignment = HorizontalAlignment.Center) : base(new TextBox())
        {
            TBox = (TextBox)EditorControl;
            TBox.TextAlign = textAlignment;
            TBox.KeyPress += EditorControlKeyPress;
        }

        protected override void EditInternal(int columnIndex, int itemIndex, object value)
        {
            TBox.Text = value.ToString();
            TBox.SelectAll();
        }

        protected override object GetResultValue()
        {
            return TBox.Text;
        }

        protected override void OnDone(CellEditorDoneEventArgs args)
        {
            base.OnDone(args);
            TBox.Text = "";
        }

        private void EditorControlKeyPress(object sender, KeyPressEventArgs e)
        {
            // This has to be handled in the KeyPress event, otherwise you'll 
            // get a Windows 'Ding' sound everytime enter or escape is pressed.
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    EditDone();
                    e.Handled = true;
                    break;

                case (char)Keys.Escape:
                    Cancel();
                    e.Handled = true;
                    break;
            }
        }
    }
}
