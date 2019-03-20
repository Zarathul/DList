/*
 * Copyright 2019 Zarathul
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList
{
    public static class Utils
    {
        #region Extensions

        public static int Clamp(this int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static Rectangle ToGDI(this Rectangle source)
        {
            return new Rectangle(source.X, source.Y, source.Width - 1, source.Height - 1);
        }

        public static Size Distance(this Point from, int toX, int toY)
        {
            return new Size(Math.Abs(from.X - toX), Math.Abs(from.Y - toY));
        }

        #endregion

        public static Rectangle GetRectangleFromPoints(Point p0, Point p1)
        {
            var RectangleStart = new Point(Math.Min(p0.X, p1.X), Math.Min(p0.Y, p1.Y));
            var RectangleEnd = new Point(Math.Max(p0.X, p1.X), Math.Max(p0.Y, p1.Y));
            var RectangleSize = new Size(RectangleEnd.X - RectangleStart.X, RectangleEnd.Y - RectangleStart.Y);

            return new Rectangle(RectangleStart, RectangleSize);
        }

        public static Rectangle AlignInRectangle(Rectangle bounds, Size alignmentSize, ContentAlignment alignment)
        {
            if (alignmentSize.Width > bounds.Width || alignmentSize.Height > bounds.Height)
            {
                return Rectangle.Empty;
            }

            int HorizontalOffset;
            int VerticalOffset;

            switch (alignment)
            {
                case ContentAlignment.TopCenter:
                    HorizontalOffset = bounds.Width / 2 - alignmentSize.Width / 2;
                    VerticalOffset = 0;
                    break;
                case ContentAlignment.TopRight:
                    HorizontalOffset = bounds.Width - alignmentSize.Width - 1;
                    VerticalOffset = 0;
                    break;
                case ContentAlignment.MiddleLeft:
                    HorizontalOffset = 0;
                    VerticalOffset = bounds.Height / 2 - alignmentSize.Height / 2;
                    break;
                case ContentAlignment.MiddleCenter:
                    HorizontalOffset = bounds.Width / 2 - alignmentSize.Width / 2;
                    VerticalOffset = bounds.Height / 2 - alignmentSize.Height / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    HorizontalOffset = bounds.Width - alignmentSize.Width - 1;
                    VerticalOffset = bounds.Height / 2 - alignmentSize.Height / 2;
                    break;
                case ContentAlignment.BottomLeft:
                    HorizontalOffset = 0;
                    VerticalOffset = bounds.Height - alignmentSize.Height - 1;
                    break;
                case ContentAlignment.BottomCenter:
                    HorizontalOffset = bounds.Width / 2 - alignmentSize.Width / 2;
                    VerticalOffset = bounds.Height - alignmentSize.Height - 1;
                    break;
                case ContentAlignment.BottomRight:
                    HorizontalOffset = bounds.Width - alignmentSize.Width - 1;
                    VerticalOffset = bounds.Height - alignmentSize.Height - 1;
                    break;
                case ContentAlignment.TopLeft:
                default:
                    HorizontalOffset = 0;
                    VerticalOffset = 0;
                    break;
            }

            var AlignedRectangle = new Rectangle(bounds.Left + HorizontalOffset, bounds.Top + VerticalOffset, alignmentSize.Width, alignmentSize.Height);

            return AlignedRectangle;
        }

        public static TextFormatFlags ConvertAlignmentToTextFormatFlags(ContentAlignment alignment)
        {
            TextFormatFlags Flags = TextFormatFlags.Default;

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    Flags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopCenter:
                    Flags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopRight:
                    Flags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;
                case ContentAlignment.MiddleLeft:
                    Flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleCenter:
                    Flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.MiddleRight:
                    Flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
                case ContentAlignment.BottomLeft:
                    Flags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomCenter:
                    Flags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomRight:
                    Flags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
            }

            return Flags;
        }

        public static void CheckPropertyChanged<T>(string propertyName, ref T oldValue, ref T newValue, Action<PropertyChangedEventArgs> eventTrigger)
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
