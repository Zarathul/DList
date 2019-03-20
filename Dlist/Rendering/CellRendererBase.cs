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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Styles = System.Windows.Forms.VisualStyles;

namespace InCoding.DList.Rendering
{
    public abstract class CellRendererBase : IDisposable
    {
        protected const TextFormatFlags BaseTextFlags = TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis;
        protected const ContentAlignment DefaultAlignment = ContentAlignment.MiddleCenter;

        private ContentAlignment _Alignment;
        private Dictionary<Color, Pen> _PenCache;
        private Dictionary<Color, Brush> _BrushCache;

        public ContentAlignment Alignment
        {
            get => _Alignment;
            set
            {
                _Alignment = value;
                TextFlags = BaseTextFlags | Utils.ConvertAlignmentToTextFormatFlags(value);
            }
        }

        public TextFormatFlags TextFlags { get; private set; }
        public Styles.VisualStyleRenderer VsRenderer { get; private set; }

        public CellRendererBase() : this(null, DefaultAlignment)
        {
        }

        public CellRendererBase(ContentAlignment alignment) : this(null, alignment)
        {
        }

        public CellRendererBase(IEnumerable<Styles.VisualStyleElement> requiredStyleElements) : this(requiredStyleElements, DefaultAlignment)
        {
        }

        public CellRendererBase(IEnumerable<Styles.VisualStyleElement> requiredStyleElements, ContentAlignment alignment)
        {
            Alignment = alignment;

            _PenCache = new Dictionary<Color, Pen>();
            _BrushCache = new Dictionary<Color, Brush>();

            if (requiredStyleElements != null)
            {
                // Check if all required visual style elements are defined and create a renderer if they are.
                Styles.VisualStyleElement Element = null;

                foreach (var element in requiredStyleElements)
                {
                    if (!Styles.VisualStyleRenderer.IsElementDefined(element)) break;
                    if (Element == null) Element = element;
                }

                if (Element != null) VsRenderer = new Styles.VisualStyleRenderer(Element);
            }
            else
            {
                VsRenderer = null;
            }
        }

        protected Pen GetPen(Color color)
        {
            if (_PenCache.ContainsKey(color))
            {
                return _PenCache[color];
            }

            var NewPen = new Pen(color);
            _PenCache.Add(color, NewPen);

            return NewPen;
        }

        protected Brush GetBrush(Color color)
        {
            if (_BrushCache.ContainsKey(color))
            {
                return _BrushCache[color];
            }

            var NewBrush = new SolidBrush(color);
            _BrushCache.Add(color, NewBrush);

            return NewBrush;
        }

        #region IDisposable

        private bool _Disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    foreach (var KVPair in _PenCache)
                    {
                        KVPair.Value.Dispose();
                    }

                    foreach (var KVPair in _BrushCache)
                    {
                        KVPair.Value.Dispose();
                    }
                }

                _Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        
        #endregion
    }
}
