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
