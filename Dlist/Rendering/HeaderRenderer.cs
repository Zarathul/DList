using System.Drawing;
using System.Windows.Forms;
using Styles = System.Windows.Forms.VisualStyles;

namespace InCoding.DList.Rendering
{
    public class HeaderRenderer : VisualStyleRendererBase, IComplexRenderer
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
            }
            else
            {
                using (var BackgroundBrush = new SolidBrush(backColor))
                {
                    gfx.FillRectangle(BackgroundBrush, bounds.ToGDI());
                }

                gfx.DrawLine(Pens.Black, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
            }

            // The VisualStyleRenderer does not center the text vertically with the correct TextFormatFlags, 
            // so we draw it ourselves even if visual styles are on.
            if (value != null)
            {
                var Bounds = bounds.ToGDI();  // HACK: @GRID
                TextRenderer.DrawText(gfx, value.ToString(), font, Bounds, foreColor, TextFlags);
            }
        }
    }
}
