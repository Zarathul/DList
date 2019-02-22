using System.Drawing;

namespace InCoding.DList.Rendering
{
    public interface ISimpleRenderer
    {
        void Draw(Graphics gfx, Rectangle bounds);
    }
}
