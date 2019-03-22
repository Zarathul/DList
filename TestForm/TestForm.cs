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
using System.Drawing;
using System.Windows.Forms;

using InCoding.DList;
using InCoding.DList.Collections;
using InCoding.DList.Rendering;
using InCoding.DList.Editing;
using System.Text;

namespace InCoding
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            // Columns
            var Column1 = new Column("String")
            {
                ValueGetter = (item) => ((TestItem)item).Name,
                ValueSetter = (item, value) => ((TestItem)item).Name = value.ToString(),
                CellEditor = new TextCellEditor(),
                Width = 215,
                HeaderFont = new Font(dList1.HeaderFont, FontStyle.Bold | FontStyle.Italic),
                ItemFont = new Font(dList1.Font, FontStyle.Italic)
            };

            var Column2 = new Column("Numeric")
            {
                ValueGetter = (item) => ((TestItem)item).NumericValue,
                CellRenderer = new ProgressBarCellRenderer(0, 100)
            };

            var Column3 = new Column("Boolean")
            {
                Width = 150,
                ValueGetter = (item) => ((TestItem)item).Flag,
                ValueSetter = (item, value) => ((TestItem)item).Flag = (bool)value,
                CellRenderer = new CheckBoxCellRenderer(),
                CellEditor = new BooleanCellEditor(),
                CellColorEvaluator = (value) => ((bool)value) ? (Color.Black, Color.Green) : (Color.Black, Color.Red)
            };

            var Column4 = new Column("DateTime")
            {
                ValueGetter = (item) => ((TestItem)item).Date,
                ValueSetter = (item, value) => ((TestItem)item).Date = (DateTime)value,
                CellEditor = new DateTimeCellEditor()
            };

            var Temp = (TextCellRenderer)Column4.CellRenderer;
            Temp.Format = "{0:F}";

            dList1.Columns.Add(Column1);
            dList1.Columns.Add(Column2);
            dList1.Columns.Add(Column3);
            dList1.Columns.Add(Column4);

            dList1.ItemColorEvaluator = (item) => (((TestItem)item).NumericValue > 50) ? (Color.Black, Color.Gold) : (dList1.ForeColor, dList1.BackColor);

            // Progressbar test items
            var PTestItem0 = TestItem.GenerateRandomItem(0);
            PTestItem0.NumericValue = 0;
            var PTestItem1 = TestItem.GenerateRandomItem(1);
            PTestItem1.NumericValue = 50;
            var PTestItem2 = TestItem.GenerateRandomItem(2);
            PTestItem2.NumericValue = 100;

            dList1.Items.Add(PTestItem0);
            dList1.Items.Add(PTestItem1);
            dList1.Items.Add(PTestItem2);

            // Completely random items
            var Items = TestItem.GenerateRandomItems(17, 3);
            dList1.Items.AddRange(Items);

            int MaxItemIndex = dList1.Items.Count - 1;
            numericUpDownItemIndex.Maximum = MaxItemIndex;
            numericUpDownItemIndex2.Maximum = MaxItemIndex;
            numericUpDownSelectRangeFrom.Maximum = MaxItemIndex;
            numericUpDownSelectRangeTo.Maximum = MaxItemIndex;

            numericUpDownColumnIndex.Maximum = dList1.Columns.Count - 1;

            dList1.Items.CollectionChanged += CollectionChangedOrChanging;
            dList1.Items.CollectionChanging += CollectionChangedOrChanging;
            dList1.Items.ItemChanged += ItemChanged;

            dList1.Columns.CollectionChanged += CollectionChangedOrChanging;
            dList1.Columns.CollectionChanging += CollectionChangedOrChanging;
            dList1.Columns.ItemChanged += ItemChanged;

            dList1.SelectedItemIndices.CollectionChanged += CollectionChangedOrChanging;
            dList1.SelectedItemIndices.CollectionChanging += CollectionChangedOrChanging;
        }

        private void ButtonAddRngItemClick(object sender, EventArgs e)
        {
            int NewItemCount = (int)numericUpDownRandomItems.Value;

            if (NewItemCount == 1)  // This is intentional to test the different events.
            {
                var NewItem = TestItem.GenerateRandomItem();
                dList1.Items.Add(NewItem);
            }
            else if (NewItemCount > 1)
            {
                dList1.Items.AddRange(TestItem.GenerateRandomItems(NewItemCount));
            }
        }

        private void ButtonRemoveItemClick(object sender, EventArgs e)
        {
            dList1.RemoveSelectedItems();
        }

        private void ButtonSelectItemClick(object sender, EventArgs e)
        {
            if (dList1.Items.Count > 0 && (dList1.Items.Count > numericUpDownItemIndex.Value))
            {
                dList1.Select((int)numericUpDownItemIndex.Value, checkBoxAddToSelection.Checked);
            }
        }

        private void ButtonSelectRangeClick(object sender, EventArgs e)
        {
            int StartIndex = (int)numericUpDownSelectRangeFrom.Value;
            int EndIndex = (int)numericUpDownSelectRangeTo.Value;

            dList1.SelectRange(StartIndex, EndIndex, checkBoxAddToSelection.Checked);
        }

        private void ButtonSelectAllClick(object sender, EventArgs e)
        {
            dList1.SelectAll();
        }

        private void ButtonDeselectAllClick(object sender, EventArgs e)
        {
            dList1.DeselectAll();
        }

        private void ButtonEnsureItemVisibilityClick(object sender, EventArgs e)
        {
            dList1.EnsureItemVisibility((int)numericUpDownItemIndex2.Value);
        }

        private void ButtonEnsureColumnVisibilityClick(object sender, EventArgs e)
        {
            dList1.EnsureColumnVisibility((int)numericUpDownColumnIndex.Value);
        }

        private void ButtonEnsureCellVisibilityClick(object sender, EventArgs e)
        {
            dList1.EnsureCellVisibility((int)numericUpDownColumnIndex.Value, (int)numericUpDownItemIndex2.Value);
        }

        private void ButtonBeginEditClick(object sender, EventArgs e)
        {
            dList1.BeginCellEdit((int)numericUpDownColumnIndex.Value, (int)numericUpDownItemIndex2.Value);
        }

        private void ButtonCancelEditClick(object sender, EventArgs e)
        {
            dList1.CancelCellEdit();
        }

        private void ListBoxEventsMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var LBox = (ListBox)sender;
                LBox.Items.Clear();
            }
        }

        private void DList1CellClicked(object sender, CellClickEventArgs e)
        {
            if (!checkBoxCellClicked.Checked) return;

            string Entry = String.Format("CellClicked >> ColumnIndex: {0}, ItemIndex: {1}", e.ColumnIndex, e.ItemIndex);
            listBoxEvents.Items.Add(Entry);
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
        }

        private void DList1HeaderClicked(object sender, HeaderClickEventArgs e)
        {
            if (!checkBoxHeaderClicked.Checked) return;

            string Entry = String.Format("HeaderClicked >> ColumnIndex: {0}", e.Index);
            listBoxEvents.Items.Add(Entry);
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
        }

        private void DList1SelectedItemsChanged(object sender, EventArgs e)
        {
            if (!checkBoxSelectedItemsChanged.Checked) return;

            var ItemIndices = new StringBuilder();

            if (dList1.AllowMultipleSelectedItems)
            {
                foreach (var index in dList1.SelectedItemIndices)
                {
                    ItemIndices.AppendFormat("{0}, ", index);
                }

                if (dList1.SelectedItemIndices.Count > 0)
                {
                    ItemIndices.Remove(ItemIndices.Length - 2, 2);
                }
                else
                {
                    ItemIndices.Append("---");
                }
            }
            else
            {
                ItemIndices.Append(dList1.SelectedItemIndex);
            }

            string Entry = String.Format("SelectedItemsChanged >> {0}", ItemIndices.ToString());
            listBoxEvents.Items.Add(Entry);
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
        }

        private void CollectionChangedOrChanging(object sender, NotifyingCollectionChangedEventArgs args)
        {
            const int Items = 0;
            const int Columns = 1;
            const int Indices = 2;
            bool Changing = (args is NotifyingCollectionChangingEventArgs);
            int SourceType = (sender is NotifyingCollection<int>) ? Indices : (sender is NotifyingCollection<Column>) ? Columns : Items;
            bool AddEventLogEntry = false;

            switch (SourceType)
            {
                case Items:
                    AddEventLogEntry = ((Changing && checkBoxItemsChanging.Checked) || (!Changing && checkBoxItemsChanged.Checked));
                    break;

                case Columns:
                    AddEventLogEntry = ((Changing && checkBoxColumnsChanging.Checked) || (!Changing && checkBoxColumnsChanged.Checked));
                    break;

                case Indices:
                    AddEventLogEntry = ((Changing && checkBoxSelectedIndicesChanging.Checked) || (!Changing && checkBoxSelectedIndicesChanged.Checked));
                    break;
            }

            if (AddEventLogEntry)
            {
                string Source = (SourceType == Items) ? "Items" : (SourceType == Columns) ? "Columns" : "SelectedItemIndices";
                string EventType = (Changing) ? "CHANGING" : "CHANGED";
                string Entry = string.Empty;

                switch (args.Action)
                {
                    case NotifyingCollectionChangeAction.Add:
                        Entry = String.Format("{0}[{1}] == ADD {2}[{3}]", Source, EventType, args.NewItem, args.NewItemIndex);
                        break;
                    case NotifyingCollectionChangeAction.AddRange:
                        Entry = String.Format("{0}[{1}] == ADD_RANGE", Source, EventType);
                        break;
                    case NotifyingCollectionChangeAction.Remove:
                        Entry = String.Format("{0}[{1}] == REMOVE {2}[{3}]", Source, EventType, args.OldItem, args.OldItemIndex);
                        break;
                    case NotifyingCollectionChangeAction.RemoveRange:
                        Entry = String.Format("{0}[{1}] == REMOVE_RANGE", Source, EventType);
                        break;
                    case NotifyingCollectionChangeAction.Replace:
                        Entry = String.Format("{0}[{1}] == REPLACE {2}[{3}] with {4}[{5}]", Source, EventType, args.OldItem, args.OldItemIndex, args.NewItem, args.NewItemIndex);
                        break;
                    case NotifyingCollectionChangeAction.Clear:
                        Entry = String.Format("{0}[{1}] == CLEAR", Source, EventType);
                        break;
                    case NotifyingCollectionChangeAction.Sort:
                        Entry = String.Format("{0}[{1}] == SORT", Source, EventType);
                        break;
                    default:
                        Entry = String.Format("{0}[{1}] == UNKNOWN", Source, EventType);
                        break;
                }

                listBoxEvents.Items.Add(Entry);
                listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
            }


            if (SourceType == Columns)
            {
                numericUpDownColumnIndex.Maximum = dList1.Columns.Count - 1;
            }
            else if (SourceType == Items)
            {
                int NewMax = dList1.Items.Count - 1;
                numericUpDownItemIndex.Maximum = NewMax;
                numericUpDownItemIndex2.Maximum = NewMax;
                numericUpDownSelectRangeFrom.Maximum = NewMax;
                numericUpDownSelectRangeTo.Maximum = NewMax;
            }
        }

        private void ItemChanged(object sender, ItemPropertyChangedEventArgs args)
        {
            bool SourceIsColumns = (sender is NotifyingCollection<Column>);
            bool AddEventLogEntry = ((SourceIsColumns && checkBoxColumnChanged.Checked) || (!SourceIsColumns && checkBoxItemChanged.Checked));

            if (AddEventLogEntry)
            {
                string Entry = String.Format("{0} changed [{1}] == {2}", (SourceIsColumns) ? "Column" : "Item", args.PropertyName, args.Item);
                listBoxEvents.Items.Add(Entry);
                listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
            }
        }
    }
}
