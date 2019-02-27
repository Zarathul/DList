using System;

namespace InCoding.DList.Rendering
{
    [Flags]
    public enum RenderState
    {
        None = 0,
        Normal = 1,
        Hot = 2,
        Pressed = 4,
        Selected = 8,
        Focused = 16
    }
}
