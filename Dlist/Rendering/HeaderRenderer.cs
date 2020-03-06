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

namespace InCoding.DList.Rendering
{
    public class HeaderRenderer : CellRendererBase, IHeaderRenderer
    {
        public HeaderRenderer(ContentAlignment alignment = DefaultAlignment) : base(alignment)
        {
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor,
            Color separatorColor, Color reorderIndicatorColor, Font font)
        {
            var BackgroundBrush = GetBrush(backColor);
            var SeparatorPen = GetPen(separatorColor);

            gfx.FillRectangle(BackgroundBrush, bounds);
            gfx.DrawLine(SeparatorPen, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);

            // Draw reorder indicator.
            if (state.HasFlag(RenderState.Focused))
            {
                var IndicatorBrush = GetBrush(reorderIndicatorColor);
                var IndicatorRect = new Rectangle(bounds.Left, bounds.Bottom - 2, bounds.Width, 2);
                gfx.FillRectangle(IndicatorBrush, IndicatorRect);
            }

            if (value != null)
            {
                TextRenderer.DrawText(gfx, value.ToString(), font, bounds, foreColor, TextFlags);
            }
        }
    }
}
