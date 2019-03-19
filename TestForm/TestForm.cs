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
            var Column1 = new Column("String");
            Column1.ValueGetter = (item) => ((TestItem)item).Name;
            Column1.ValueSetter = (item, value) => ((TestItem)item).Name = value.ToString();
            Column1.CellEditor = new TextCellEditor();
            Column1.Width = 215;
            Column1.HeaderFont = new Font(dList1.HeaderFont, FontStyle.Bold | FontStyle.Italic);
            Column1.ItemFont = new Font(dList1.Font, FontStyle.Italic);

            var Column2 = new Column("Numeric");
            Column2.ValueGetter = (item) => ((TestItem)item).NumericValue;
            Column2.CellRenderer = new ProgressBarCellRenderer(0, 100);

            var Column3 = new Column("Boolean");
            Column3.Width = 150;
            Column3.ValueGetter = (item) => ((TestItem)item).Flag;
            Column3.ValueSetter = (item, value) => ((TestItem)item).Flag = (bool)value;
            Column3.CellRenderer = new CheckBoxCellRenderer();
            Column3.CellEditor = new BooleanCellEditor();
            Column3.CellColorEvaluator = (value) => ((bool)value) ? (Color.Black, Color.Green) : (Color.Black, Color.Red);

            var Column4 = new Column("DateTime");
            Column4.ValueGetter = (item) => ((TestItem)item).Date;
            Column4.ValueSetter = (item, value) => ((TestItem)item).Date = (DateTime)value;
            Column4.CellEditor = new DateTimeCellEditor();
            var Temp = (TextCellRenderer)Column4.CellRenderer;
            Temp.Format = "{0:F}";

            dList1.Columns.Add(Column1);
            dList1.Columns.Add(Column2);
            dList1.Columns.Add(Column3);
            dList1.Columns.Add(Column4);

            dList1.ItemColorEvaluator = (item) => (((TestItem)item).NumericValue > 50) ? (Color.Black, Color.Gold) : (dList1.ForeColor, dList1.BackColor);

            // Progressbar test items
            var PTestItem0 = TestItem.GenerateRandom(0);
            PTestItem0.NumericValue = 0;
            var PTestItem1 = TestItem.GenerateRandom(1);
            PTestItem1.NumericValue = 50;
            var PTestItem2 = TestItem.GenerateRandom(2);
            PTestItem2.NumericValue = 100;

            dList1.Items.Add(PTestItem0);
            dList1.Items.Add(PTestItem1);
            dList1.Items.Add(PTestItem2);

            // Items
            for (int i = 3; i < 20; i++)
            {
                var Item = TestItem.GenerateRandom(i);
                dList1.Items.Add(Item);
            }

            numericUpDownItemIndex.Minimum = 0;
            numericUpDownItemIndex.Maximum = dList1.Items.Count - 1;

            numericUpDownItemIndex2.Minimum = 0;
            numericUpDownItemIndex2.Maximum = dList1.Items.Count - 1;

            numericUpDownColumnIndex.Minimum = 0;
            numericUpDownColumnIndex.Maximum = dList1.Columns.Count - 1;

            dList1.Items.CollectionChanged += CollectionChangedOrChanging;
            dList1.Items.CollectionChanging += CollectionChangedOrChanging;
            dList1.Items.ItemChanged += ItemChanged;

            dList1.Columns.CollectionChanged += CollectionChangedOrChanging;
            dList1.Columns.CollectionChanging += CollectionChangedOrChanging;
        }

        private void buttonAddRngItem_Click(object sender, EventArgs e)
        {
            var NewItem = TestItem.GenerateRandom();
            dList1.Items.Add(NewItem);
        }

        private void buttonRemoveItem_Click(object sender, EventArgs e)
        {
            if (dList1.AllowMultipleSelectedItems)
            {
                var Indices = new int[dList1.SelectedItemIndices.Count];
                dList1.SelectedItemIndices.CopyTo(Indices, 0);
                dList1.Items.RemoveAt(Indices);
            }
            else if (dList1.SelectedItemIndex >= 0)
            {
                dList1.Items.RemoveAt(dList1.SelectedItemIndex);
            }
        }

        private void buttonSelectItem_Click(object sender, EventArgs e)
        {
            if (dList1.Items.Count > 0 && (dList1.Items.Count > numericUpDownItemIndex.Value))
            {
                dList1.Select((int)numericUpDownItemIndex.Value, checkBoxAddToSelection.Checked);
            }
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            dList1.SelectAllItems();
        }

        private void buttonEnsureItemVisibility_Click(object sender, EventArgs e)
        {
            dList1.EnsureItemVisibility((int)numericUpDownItemIndex2.Value);
        }

        private void buttonEnsureColumnVisibility_Click(object sender, EventArgs e)
        {
            dList1.EnsureColumnVisibility((int)numericUpDownColumnIndex.Value);
        }

        private void buttonEnsureCellVisibility_Click(object sender, EventArgs e)
        {
            dList1.EnsureCellVisibility((int)numericUpDownColumnIndex.Value, (int)numericUpDownItemIndex2.Value);
        }

        private void buttonBeginEdit_Click(object sender, EventArgs e)
        {
            dList1.BeginCellEdit((int)numericUpDownColumnIndex.Value, (int)numericUpDownItemIndex2.Value);
        }

        private void buttonCancelEdit_Click(object sender, EventArgs e)
        {
            dList1.CancelCellEdit();
        }

        private void listBoxEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var LBox = (ListBox)sender;
                LBox.Items.Clear();
            }
        }

        private void dList1_CellClicked(object sender, CellClickEventArgs e)
        {
            string Entry = String.Format("CellClicked >> ColumnIndex: {0}, ItemIndex: {1}", e.ColumnIndex, e.ItemIndex);
            listBoxEvents.Items.Add(Entry);
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
        }

        private void dList1_HeaderClicked(object sender, HeaderClickEventArgs e)
        {
            string Entry = String.Format("HeaderClicked >> ColumnIndex: {0}", e.Index);
            listBoxEvents.Items.Add(Entry);
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
        }

        private void dList1_SelectedItemsChanged(object sender, EventArgs e)
        {
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
            string Entry = string.Empty;
            bool SourceIsColumns = (sender is NotifyingCollection<Column>);
            string Source = SourceIsColumns ? "Columns" : "Items";
            string EventType = (args is NotifyingCollectionChangingEventArgs) ? "CHANGING" : "CHANGED";

            switch (args.Action)
            {
                case NotifyingCollectionChangeAction.Add:
                    Entry = String.Format("{0}[{1}] == ADD {2}[{3}]", Source, EventType, args.NewItem, args.NewItemIndex);
                    break;
                case NotifyingCollectionChangeAction.Remove:
                    Entry = String.Format("{0}[{1}] == REMOVE {2}[{3}]", Source, EventType, args.OldItem, args.OldItemIndex);
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
                    break;
            }

            listBoxEvents.Items.Add(Entry);
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;

            if (SourceIsColumns)
            {
                numericUpDownColumnIndex.Maximum = dList1.Columns.Count - 1;
            }
            else
            {
                numericUpDownItemIndex.Maximum = dList1.Items.Count - 1;
                numericUpDownItemIndex2.Maximum = dList1.Items.Count - 1;
            }
        }

        private void ItemChanged(object sender, ItemPropertyChangedEventArgs args)
        {
            string Entry = String.Format("ItemChanged [{0}] == {1}", args.PropertyName, args.Item);
            listBoxEvents.Items.Add(Entry);
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1;
        }
    }
}
