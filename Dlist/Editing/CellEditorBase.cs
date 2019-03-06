using System;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Editing
{
    public abstract class CellEditorBase : ICellEditor
    {
        private bool _Disposed = false;

        protected int ColumnIndex { get; private set; }
        protected int ItemIndex { get; private set; }
        protected object Value { get; private set; }

        public Control EditorControl { get; private set; }

        public event EventHandler<CellEditorDoneEventArgs> Done;

        public CellEditorBase(Control editorControl = null)
        {
            if (editorControl != null)
            {
                EditorControl = editorControl;
                EditorControl.Visible = false;
            }
        }

        public void Edit(Rectangle cellBounds, int columnIndex, int itemIndex, object value)
        {
            ColumnIndex = columnIndex;
            ItemIndex = itemIndex;
            Value = value;

            if (EditorControl != null)
            {
                PositionEditorControl(cellBounds);
                EditorControl.Visible = true;
                EditorControl.BringToFront();
                EditorControl.Focus();
                EditorControl.LostFocus += EditorControlLostFocus;
            }

            EditInternal(columnIndex, itemIndex, value);
        }

        public void Cancel()
        {
            EditDone(false);
        }

        protected abstract object GetResultValue();

        protected void EditDone(bool success = true)
        {
            if (EditorControl != null)
            {
                EditorControl.LostFocus -= EditorControlLostFocus;
                EditorControl.Visible = false;
                EditorControl.SendToBack();
            }

            var Args = new CellEditorDoneEventArgs(success, ColumnIndex, ItemIndex, GetResultValue());
            OnDone(Args);
        }

        protected virtual void EditInternal(int columnIndex, int itemIndex, object value)
        {
        }

        protected virtual void PositionEditorControl(Rectangle bounds)
        {
            // Center vertically by default. This is useful for TextBoxes for example, because 
            // they ignore the height parameter and usually don't fill out the entire cell.
            int ControlY = bounds.Y + (bounds.Height - EditorControl.Height) / 2;
            EditorControl.SetBounds(bounds.X + 1, ControlY, bounds.Width - 2, bounds.Height - 2);
        }

        protected virtual void EditorControlLostFocus(object sender, EventArgs e)
        {
            EditDone(false);
        }

        protected virtual void OnDone(CellEditorDoneEventArgs args)
        {
            var Handler = Done;

            Handler?.Invoke(this, args);
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed) return;

            if (disposing)
            {
                EditorControl?.Dispose();
                EditorControl = null;
            }

            _Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
