using System.ComponentModel;
using System.Drawing;

using InCoding.DList.Rendering;

namespace InCoding.DList
{
    public delegate object ValueGetterFunc(object item);
    public delegate void ValueSetterFunc(object item, object value);

    public enum HeaderStyle
    {
        Static,
        Moving
    }

    public class Column
    {
        public string Name { get; set; } = "Column";
        public int Width { get; set; } = 80;
        public Font HeaderFont { get; set; }
        public Font ItemFont { get; set; }
        public HeaderStyle HeaderStyle { get; set; } = HeaderStyle.Static;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComplexRenderer CellRenderer { get; set; } = new TextCellRenderer();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ValueGetterFunc ValueGetter { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ValueSetterFunc ValueSetter { get; set; }

        public Column(string name) : this()
        {
            Name = name;
        }

        public Column()
        {
            ValueGetter = (object item) => item.ToString();
        }

        public object GetValue(object item)
        {
            if ((item != null) && (ValueGetter != null))
            {
                object Value = ValueGetter.Invoke(item);

                return Value;
            }

            return null;
        }

        public void SetValue(object item, object value)
        {
            if (item != null)
            {
                ValueSetter?.Invoke(item, value);
            }
        }
    }
}
