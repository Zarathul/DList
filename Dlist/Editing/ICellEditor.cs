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
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Editing
{
    public interface ICellEditor : IDisposable
    {
        Control EditorControl { get; }
        event EventHandler<CellEditorDoneEventArgs> Done;

        void Edit(Rectangle cellBounds, int columnIndex, int itemIndex, object value);
        void Cancel();
    }
}
