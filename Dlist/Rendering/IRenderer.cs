using System.Drawing;

namespace InCoding.DList.Rendering
{
    public interface IRenderer
    {
        void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font);
    }
}
