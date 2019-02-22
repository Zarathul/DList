using System.Drawing;

namespace InCoding.DList
{
    internal static class Utils
    {
        public static int Clamp(this int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static Rectangle ToGDI(this Rectangle source)
        {
            return new Rectangle(source.X, source.Y, source.Width - 1, source.Height - 1);
        }
    }
}
