using System;
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

        public static bool CheckPropertyChanged<T>(string propertyName, ref T oldValue, ref T newValue) where T : IEquatable<T>
        {
            if (oldValue == null || newValue == null) return false;

            if ((oldValue == null && newValue != null) || !oldValue.Equals(newValue))
            {
                oldValue = newValue;

                return true;
            }

            return false;
        }
    }
}
