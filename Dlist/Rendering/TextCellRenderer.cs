﻿/*
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
    public class TextCellRenderer : CellRendererBase, IRenderer
    {
        public string Format { get; set; }

        public TextCellRenderer(ContentAlignment alignment = DefaultAlignment) : base(alignment)
        {
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            var BackgroundBrush = GetBrush(backColor);
            gfx.FillRectangle(BackgroundBrush, bounds);

            if (value != null)
            {
                TextRenderer.DrawText(gfx, (Format != null) ? string.Format(Format, value) : value.ToString(), font, bounds, foreColor, TextFlags);
            }
        }

        public int GetOptimalWidth(object value, Font font)
        {
            string Text = (Format != null) ? string.Format(Format, value) : value.ToString();
            return TextRenderer.MeasureText(Text, font, new Size(int.MaxValue, int.MaxValue), TextFlags).Width;
        }
    }
}
