/*
 * Copyright 2019 Zarathul
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
