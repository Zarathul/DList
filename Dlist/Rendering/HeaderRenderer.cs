using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using InCoding.DList;

namespace InCoding.DList.Rendering
{
    public class HeaderRenderer : VisualStyleRendererBase, IComplexRenderer
    {
        private readonly TextFormatFlags TextFlags = TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;

        public HeaderRenderer() : base(
            new VisualStyleElement[] {
            VisualStyleElement.Header.Item.Normal,
            VisualStyleElement.Header.Item.Hot,
            VisualStyleElement.Header.Item.Pressed })
        {
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            if (Application.RenderWithVisualStyles && CanUseVisualStyles)
            {
                if (state.HasFlag(RenderState.Hot))
                {
                    VsRenderer.SetParameters(VisualStyleElement.Header.Item.Hot);
                }
                else if (state.HasFlag(RenderState.Pressed))
                {
                    VsRenderer.SetParameters(VisualStyleElement.Header.Item.Pressed);
                }
                else
                {
                    VsRenderer.SetParameters(VisualStyleElement.Header.Item.Normal);
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
                TextRenderer.DrawText(gfx, value.ToString(), font, bounds, foreColor, TextFlags);
            }
        }
    }
}
