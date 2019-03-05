using System;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Editing
{
    public class TextCellEditor : CellEditorBase
    {
        public TextCellEditor() : base(new TextBox())
        {
            EditorControl.KeyDown += EditorControlKeyDown;
        }

        public override void BeginEdit(Rectangle cellBounds, int columnIndex, int itemIndex, object value)
        {
            ColumnIndex = columnIndex;
            ItemIndex = itemIndex;

            int ControlY = cellBounds.Y + (cellBounds.Height - EditorControl.Height) / 2;
            EditorControl.SetBounds(cellBounds.X + 1, ControlY, cellBounds.Width - 2, cellBounds.Height - 2);
            EditorControl.Text = value.ToString();
            EditorControl.Visible = true;
            EditorControl.BringToFront();
            EditorControl.Focus();
            EditorControl.LostFocus += EditorControlLostFocus;
        }

        public override void Cancel()
        {
            EditorControl.LostFocus -= EditorControlLostFocus;
            EditorControl.Visible = false;
            EditorControl.SendToBack();

            var Args = new CellEditorDoneEventArgs(false, ColumnIndex, ItemIndex, "");
            OnDone(Args);

            EditorControl.Text = "";
        }

        private void EditorControlLostFocus(object sender, EventArgs e)
        {
            Cancel();
        }

        private void EditDone()
        {
            EditorControl.LostFocus -= EditorControlLostFocus;
            EditorControl.Visible = false;
            EditorControl.SendToBack();

            var Args = new CellEditorDoneEventArgs(true, ColumnIndex, ItemIndex, EditorControl.Text);
            OnDone(Args);

            EditorControl.Text = "";
        }

        private void EditorControlKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    EditDone();

                    break;
                case Keys.Escape:
                    Cancel();
                    break;
            }
        }
    }
}
