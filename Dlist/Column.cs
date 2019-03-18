using System;
using System.ComponentModel;
using System.Drawing;

using InCoding.DList.Rendering;
using InCoding.DList.Editing;

namespace InCoding.DList
{
    // TODO: Maybe replace these with Func and Action.
    public delegate object ValueGetterFunc(object item);
    public delegate void ValueSetterFunc(object item, object value);

    public class Column : INotifyPropertyChanged
    {
        private string _Name = "Column";
        private int _Width = 80;
        private Font _HeaderFont;
        private Font _ItemFont;
        private IRenderer _CellRenderer = new TextCellRenderer();
        private ICellEditor _CellEditor;
        private ValueGetterFunc _ValueGetter;
        private ValueSetterFunc _ValueSetter;
        private Func<object, (Color, Color)> _CellColorEvaluator;

        #region Properties

        [DefaultValue("Column")]
        public string Name
        {
            get => _Name;
            set => Utils.CheckPropertyChanged(nameof(Name), ref _Name, ref value, OnPropertyChanged);
        }

        [DefaultValue(80)]
        public int Width
        {
            get => _Width;
            set => Utils.CheckPropertyChanged(nameof(Width), ref _Width, ref value, OnPropertyChanged);
        }

        [DefaultValue(null)]
        public Font HeaderFont
        {
            get => _HeaderFont;
            set => Utils.CheckPropertyChanged(nameof(HeaderFont), ref _HeaderFont, ref value, OnPropertyChanged);
        }

        [DefaultValue(null)]
        public Font ItemFont
        {
            get => _ItemFont;
            set => Utils.CheckPropertyChanged(nameof(ItemFont), ref _ItemFont, ref value, OnPropertyChanged);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanEdit
        {
            get => (_ValueGetter != null && _ValueSetter != null && _CellEditor != null);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IRenderer CellRenderer
        {
            get => _CellRenderer;
            set => Utils.CheckPropertyChanged(nameof(CellRenderer), ref _CellRenderer, ref value, OnPropertyChanged);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICellEditor CellEditor
        {
            get => _CellEditor;
            set => Utils.CheckPropertyChanged(nameof(CellEditor), ref _CellEditor, ref value, OnPropertyChanged);
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<object, (Color, Color)> CellColorEvaluator
        {
            get => _CellColorEvaluator;
            set => Utils.CheckPropertyChanged(nameof(CellColorEvaluator), ref _CellColorEvaluator, ref value, OnPropertyChanged);
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion

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
