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

using System.Drawing;
using System.Windows.Forms;
using Styles = System.Windows.Forms.VisualStyles;

namespace InCoding.DList.Rendering
{
    public class CheckBoxCellRenderer : CellRendererBase, IRenderer
    {
        public CheckBoxCellRenderer(ContentAlignment alignment = DefaultAlignment)
        {
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            var BackgroundBrush = GetBrush(backColor);
            gfx.FillRectangle(BackgroundBrush, bounds);

            bool Checked = (bool)value;
            var CheckBoxDrawState = (Checked) ? Styles.CheckBoxState.CheckedNormal : Styles.CheckBoxState.UncheckedNormal;
            var CheckBoxSize = CheckBoxRenderer.GetGlyphSize(gfx, CheckBoxDrawState);

            var AlignedCheckboxRectangle = Utils.AlignInRectangle(bounds, CheckBoxSize, Alignment);

            if (!AlignedCheckboxRectangle.IsEmpty)  // Can happen if the cell is very tiny and the checkbox does not fit.
            {
                CheckBoxRenderer.DrawCheckBox(gfx, AlignedCheckboxRectangle.Location, CheckBoxDrawState);
            }
        }
    }
}
