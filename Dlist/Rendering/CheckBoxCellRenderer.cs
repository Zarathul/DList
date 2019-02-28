using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace InCoding.DList.Rendering
{
    public class CheckBoxCellRenderer : IComplexRenderer
    {
        public CheckBoxCellRenderer()
        {
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            bool Checked = (bool)value;
            var CheckBoxDrawState = (Checked) ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
            var CheckBoxSize = CheckBoxRenderer.GetGlyphSize(gfx, CheckBoxDrawState);
            var CheckBoxPos = new Point(bounds.Left + bounds.Width / 2 - CheckBoxSize.Width / 2,
                bounds.Top + bounds.Height / 2 - CheckBoxSize.Height / 2);

            using (var backgroundBrush = new SolidBrush(backColor))
            {
                gfx.FillRectangle(backgroundBrush, bounds.ToGDI());
            }

            CheckBoxRenderer.DrawCheckBox(gfx, CheckBoxPos, CheckBoxDrawState);
        }
    }
}
