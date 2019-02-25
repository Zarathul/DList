using System;
using System.ComponentModel;
using System.Drawing;

namespace InCoding.DList
{
    internal delegate void PropertyChangedEventTrigger(PropertyChangedEventArgs args);

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

        public static void CheckPropertyChanged<T>(string propertyName, ref T oldValue, ref T newValue, PropertyChangedEventTrigger eventTrigger)
        {
            if (oldValue == null && newValue == null) return;

            if ((oldValue == null && newValue != null) || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                var Args = new PropertyChangedEventArgs(propertyName);
                eventTrigger(Args);
            }
        }
    }
}
