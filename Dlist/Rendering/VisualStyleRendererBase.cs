using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Styles = System.Windows.Forms.VisualStyles;

namespace InCoding.DList.Rendering
{
    public abstract class VisualStyleRendererBase
    {
        protected const TextFormatFlags BaseTextFlags = TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis;
        protected const ContentAlignment DefaultAlignment = ContentAlignment.MiddleCenter;

        private ContentAlignment _Alignment;

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

        public VisualStyleRendererBase() : this(null, DefaultAlignment)
        {
        }

        public VisualStyleRendererBase(ContentAlignment alignment) : this(null, alignment)
        {
        }

        public VisualStyleRendererBase(IEnumerable<Styles.VisualStyleElement> requiredStyleElements) : this(requiredStyleElements, DefaultAlignment)
        {
        }

        public VisualStyleRendererBase(IEnumerable<Styles.VisualStyleElement> requiredStyleElements, ContentAlignment alignment)
        {
            Alignment = alignment;

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
    }
}
