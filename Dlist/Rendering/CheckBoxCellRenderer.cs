using System.Drawing;
using System.Windows.Forms;
using Vs = System.Windows.Forms.VisualStyles;

namespace InCoding.DList.Rendering
{
    public class CheckBoxCellRenderer : VisualStyleRendererBase, IRenderer
    {
        public CheckBoxCellRenderer(ContentAlignment alignment = DefaultAlignment)
        {
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            var BackgroundBrush = GetBrush(backColor);
            gfx.FillRectangle(BackgroundBrush, bounds);

            bool Checked = (bool)value;
            var CheckBoxDrawState = (Checked) ? Vs.CheckBoxState.CheckedNormal : Vs.CheckBoxState.UncheckedNormal;
            var CheckBoxSize = CheckBoxRenderer.GetGlyphSize(gfx, CheckBoxDrawState);

            var AlignedCheckboxRectangle = Utils.AlignInRectangle(bounds, CheckBoxSize, Alignment);
            CheckBoxRenderer.DrawCheckBox(gfx, AlignedCheckboxRectangle.Location, CheckBoxDrawState);
        }
    }
}
