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

    public class Column : INotifyPropertyChanged
    {
        private string _Name = "Column";
        private int _Width = 80;
        private Font _HeaderFont;
        private Font _ItemFont;
        private HeaderStyle _HeaderStyle = HeaderStyle.Static;
        private IComplexRenderer _CellRenderer = new TextCellRenderer();
        private ValueGetterFunc _ValueGetter;
        private ValueSetterFunc _ValueSetter;

        public string Name
        {
            get => _Name;
            set => Utils.CheckPropertyChanged(nameof(Name), ref _Name, ref value, OnPropertyChanged);
        }

        public int Width
        {
            get => _Width;
            set => Utils.CheckPropertyChanged(nameof(Width), ref _Width, ref value, OnPropertyChanged);
        }

        public Font HeaderFont
        {
            get => _HeaderFont;
            set => Utils.CheckPropertyChanged(nameof(HeaderFont), ref _HeaderFont, ref value, OnPropertyChanged);
        }

        public Font ItemFont
        {
            get => _ItemFont;
            set => Utils.CheckPropertyChanged(nameof(ItemFont), ref _ItemFont, ref value, OnPropertyChanged);
        }

        public HeaderStyle HeaderStyle
        {
            get => _HeaderStyle;
            set => Utils.CheckPropertyChanged(nameof(HeaderStyle), ref _HeaderStyle, ref value, OnPropertyChanged);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComplexRenderer CellRenderer
        {
            get => _CellRenderer;
            set => Utils.CheckPropertyChanged(nameof(CellRenderer), ref _CellRenderer, ref value, OnPropertyChanged);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ValueGetterFunc ValueGetter
        {
            get => _ValueGetter;
            set => Utils.CheckPropertyChanged(nameof(ValueGetter), ref _ValueGetter, ref value, OnPropertyChanged);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ValueSetterFunc ValueSetter
        {
            get => _ValueSetter;
            set => Utils.CheckPropertyChanged(nameof(ValueSetter), ref _ValueSetter, ref value, OnPropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Column(string name) : this()
        {
            Name = name;
        }

        public Column()
        {
            ValueGetter = (object item) => item.ToString();
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var Handler = PropertyChanged;
            Handler?.Invoke(this, args);
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
