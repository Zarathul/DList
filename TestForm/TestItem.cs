using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InCoding
{
    public class TestItem : INotifyPropertyChanged
    {
        private string _Name;
        private int _NumericValue;
        private bool _Flag;
        private DateTime _Date;

        public string Name
        {
            get => _Name;
            set => CheckPropertyChanged(nameof(Name), ref _Name, ref value);
        }

        public int NumericValue
        {
            get => _NumericValue;
            set => CheckPropertyChanged(nameof(NumericValue), ref _NumericValue, ref value);
        }

        public bool Flag
        {
            get => _Flag;
            set => CheckPropertyChanged(nameof(Flag), ref _Flag, ref value);
        }

        public DateTime Date
        {
            get => _Date;
            set => CheckPropertyChanged(nameof(Date), ref _Date, ref value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TestItem(string name, int numericValue, bool flag, DateTime date)
        {
            _Name = name ?? throw new ArgumentNullException(nameof(name));
            _NumericValue = numericValue;
            _Flag = flag;
            _Date = date;
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var Handler = PropertyChanged;
            Handler?.Invoke(this, args);
        }

        protected bool CheckPropertyChanged<T>(string propertyName, ref T oldValue, ref T newValue) where T : IEquatable<T>
        {
            if (oldValue == null || newValue == null) return false;

            if ((oldValue == null && newValue != null) || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                var Args = new PropertyChangedEventArgs(propertyName);
                OnPropertyChanged(Args);

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - [{2}] - {3}", _Name, _NumericValue, _Flag, _Date);
        }

        public override bool Equals(object obj)
        {
            var item = obj as TestItem;
            return item != null &&
                   _Name == item._Name &&
                   _NumericValue == item._NumericValue &&
                   _Flag == item._Flag &&
                   _Date == item._Date;
        }

        public override int GetHashCode()
        {
            var hashCode = -1281208867;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_Name);
            hashCode = hashCode * -1521134295 + _NumericValue.GetHashCode();
            hashCode = hashCode * -1521134295 + _Flag.GetHashCode();
            hashCode = hashCode * -1521134295 + _Date.GetHashCode();
            return hashCode;
        }
    }
}
