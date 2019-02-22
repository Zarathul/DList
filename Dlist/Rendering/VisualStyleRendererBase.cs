using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace InCoding.DList.Rendering
{
    public abstract class VisualStyleRendererBase
    {
        public bool CanUseVisualStyles { get; } = false;
        public VisualStyleRenderer VsRenderer { get; }

        public VisualStyleRendererBase(IEnumerable<VisualStyleElement> requiredStyleElements)
        {
            CanUseVisualStyles = true;
            VisualStyleElement Element = null;

            foreach (var element in requiredStyleElements)
            {
                if (!VisualStyleRenderer.IsElementDefined(element))
                {
                    CanUseVisualStyles = false;
                    break;
                }

                if (Element == null)
                {
                    Element = element;
                }
            }

            if (CanUseVisualStyles && Element != null)
            {
                VsRenderer = new VisualStyleRenderer(Element);
            }
        }
    }
}
