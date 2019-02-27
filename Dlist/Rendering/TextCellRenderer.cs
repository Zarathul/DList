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
            using (var backBrush = new SolidBrush(backColor))
            {
                gfx.FillRectangle(backBrush, bounds.ToGDI()); // @GDI
            }

            if (value != null)
            {
                TextRenderer.DrawText(gfx, (Format != null) ? string.Format(Format, value) : value.ToString(), font, bounds, foreColor, TextFlags);
            }
        }
    }
}
