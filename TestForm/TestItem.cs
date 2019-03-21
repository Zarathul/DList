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
using System.ComponentModel;
using System.Text;

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

        protected void CheckPropertyChanged<T>(string propertyName, ref T oldValue, ref T newValue) where T : IEquatable<T>
        {
            if (oldValue == null && newValue == null) return;

            if ((oldValue == null && newValue != null) || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                var Args = new PropertyChangedEventArgs(propertyName);
                OnPropertyChanged(Args);
            }
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

        private static Random _Rng = new Random();

        public static TestItem GenerateRandomItem(int index = -1)
        {
            int NameLength = _Rng.Next(5, 24);
            if (index >= 0) NameLength += 8;
            var RngName = new StringBuilder(NameLength);

            if (index >= 0) RngName.AppendFormat("[{0}] - ", index);

            for (int i = 0; i < NameLength; i++)
            {

                switch (_Rng.Next(0, 2))
                {
                    case 0:
                        // Number
                        RngName.Append(_Rng.Next(0, 10));
                        break;
                    case 1:
                        // Upper- or lowercase letter

                        char Letter = (char)_Rng.Next(65, 91);

                        if (_Rng.Next(0, 2) == 0)
                        {
                            Letter = char.ToLowerInvariant(Letter);
                        }

                        RngName.Append(Letter);

                        break;
                    default:
                        RngName.Append("?");
                        break;
                }
            }

            int RngNumber = _Rng.Next(0, 101);
            bool RngFlag = (_Rng.Next(0, 2) == 1);
            DateTime RngDate = DateTimeOffset.FromUnixTimeSeconds(_Rng.Next()).DateTime;

            return new TestItem(RngName.ToString(), RngNumber, RngFlag, RngDate);
        }

        public static TestItem[] GenerateRandomItems(int count, int startIndex = -1)
        {
            if (count <= 0) return null;

            var Items = new TestItem[count];
            int IndexOffset = (startIndex >= 0) ? 1 : 0;

            for (int i = 0; i < count; i++)
            {
                Items[i] = GenerateRandomItem(startIndex);
                startIndex += IndexOffset;
            }

            return Items;
        }
    }
}
