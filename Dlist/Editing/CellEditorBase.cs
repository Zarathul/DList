using System;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Editing
{
    public abstract class CellEditorBase : ICellEditor
    {
        private bool _Disposed = false;

        protected int ColumnIndex { get; set; }
        protected int ItemIndex { get; set; }
        public Control EditorControl { get; private set;  }

        public event EventHandler<CellEditorDoneEventArgs> Done;

        public CellEditorBase(Control editorControl = null)
        {
            if (editorControl != null)
            {
                EditorControl = editorControl;
                EditorControl.Visible = false;
            }
        }

        public abstract void BeginEdit(Rectangle cellBounds, int columnIndex, int itemIndex, object value);

        public abstract void Cancel();

        protected void OnDone(CellEditorDoneEventArgs args)
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
