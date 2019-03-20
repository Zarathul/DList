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
    public class HeaderRenderer : CellRendererBase, IRenderer
    {
        public HeaderRenderer(ContentAlignment alignment = DefaultAlignment) : base(
            new Styles.VisualStyleElement[] {
            Styles.VisualStyleElement.Header.Item.Normal,
            Styles.VisualStyleElement.Header.Item.Hot,
            Styles.VisualStyleElement.Header.Item.Pressed },
            alignment)
        {
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            Color TextColor = Color.Empty;

            if (Application.RenderWithVisualStyles && VsRenderer != null)
            {
                if (state.HasFlag(RenderState.Hot))
                {
                    VsRenderer.SetParameters(Styles.VisualStyleElement.Header.Item.Hot);
                }
                else if (state.HasFlag(RenderState.Pressed))
                {
                    VsRenderer.SetParameters(Styles.VisualStyleElement.Header.Item.Pressed);
                }
                else
                {
                    VsRenderer.SetParameters(Styles.VisualStyleElement.Header.Item.Normal);
                }

                VsRenderer.DrawBackground(gfx, bounds);
                TextColor = VsRenderer.GetColor(Styles.ColorProperty.TextColor);
            }
            else
            {
                var BackgroundBrush = GetBrush((state.HasFlag(RenderState.Hot))
                    ? SystemColors.HotTrack
                    : (state.HasFlag(RenderState.Pressed)) ? SystemColors.Highlight : backColor);

                gfx.FillRectangle(BackgroundBrush, bounds);
                gfx.DrawLine(SystemPens.ControlDark, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
            }

            // The VisualStyleRenderer does not center the text vertically with the correct TextFormatFlags, 
            // so we draw it ourselves even if visual styles are on.
            if (value != null)
            {
                if (TextColor.IsEmpty)
                {
                    TextColor = (state.HasFlag(RenderState.Normal)) ? foreColor : SystemColors.HighlightText;
                }

                TextRenderer.DrawText(gfx, value.ToString(), font, bounds, TextColor, TextFlags);
            }
        }
    }
}
