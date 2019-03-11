using System;
using System.Windows.Forms;

namespace InCoding.DList.Editing
{
    public class DateTimeCellEditor : CellEditorBase
    {
        private DateTimePicker _DateTimePicker;

        public DateTimePicker DateTimePicker { get => _DateTimePicker; }

        public DateTimeCellEditor() : base(new DateTimePicker())
        {
            _DateTimePicker = (DateTimePicker)EditorControl;
            
            // DateTimePickerFormat.Long seems to be bugged at the moment. The text is often 
            // misaligned and/or cut off at the left side. Short format seems ok though.
            _DateTimePicker.Format = DateTimePickerFormat.Short;
        }

        protected override void EditInternal(int columnIndex, int itemIndex, object value)
        {
            _DateTimePicker.Value = (DateTime)value;
        }

        protected override object GetResultValue()
        {
            return _DateTimePicker.Value;
        }
    }
}
