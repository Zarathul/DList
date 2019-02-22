using System;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Rendering
{
    public class TextCellRenderer : IComplexRenderer
    {
        public string Format { get; set; }

        private static readonly TextFormatFlags TextFlags = TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;

        public TextCellRenderer() : this(null)
        {
        }

        public TextCellRenderer(string format)
        {
            Format = format;
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            if (state == RenderState.Selected)
            {
                gfx.FillRectangle(SystemBrushes.Highlight, bounds.ToGDI()); // @GDI+
                foreColor = SystemColors.HighlightText;
            }
            else if (state == RenderState.Hot)
            {
                gfx.FillRectangle(SystemBrushes.HotTrack, bounds.ToGDI()); // @GDI+
                foreColor = SystemColors.HighlightText;
            }

            if (value != null)
            {
                TextRenderer.DrawText(gfx, (Format != null) ? String.Format(Format, value) : value.ToString(), font, bounds, foreColor, TextFlags);
            }
        }
    }
}
