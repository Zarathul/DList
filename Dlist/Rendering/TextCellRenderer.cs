using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Rendering
{
    public class TextCellRenderer : VisualStyleRendererBase, IComplexRenderer
    {
        public string Format { get; set; }

        public TextCellRenderer(ContentAlignment alignment = DefaultAlignment) : base(alignment)
        {
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            var BackgroundBrush = GetBrush(backColor);
            gfx.FillRectangle(BackgroundBrush, bounds.ToGDI()); // @GDI

            if (value != null)
            {
                var Bounds = bounds.ToGDI();  // HACK: @GRID
                TextRenderer.DrawText(gfx, (Format != null) ? string.Format(Format, value) : value.ToString(), font, Bounds, foreColor, TextFlags);
            }
        }
    }
}
