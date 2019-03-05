using System;
using System.Windows.Forms;

using InCoding.DList;
using InCoding.DList.Collections;
using InCoding.DList.Rendering;
using InCoding.DList.Editing;

namespace InCoding
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            // Columns
            var Column1 = new Column("String");
            Column1.ValueGetter = (object item) => ((TestItem)item).Name;
            Column1.ValueSetter = (object item, object value) => ((TestItem)item).Name = value.ToString();
            Column1.CellEditor = new TextCellEditor();
            Column1.Width = 215;

            var Column2 = new Column("Numeric");
            Column2.ValueGetter = (object item) => ((TestItem)item).NumericValue;
            Column2.CellRenderer = new ProgressBarCellRenderer(0, 100);

            var Column3 = new Column("Boolean");
            Column3.Width = 150;
            Column3.ValueGetter = (object item) => ((TestItem)item).Flag;
            Column3.ValueSetter = (object item, object value) => ((TestItem)item).Flag = (bool)value;
            Column3.CellRenderer = new CheckBoxCellRenderer();
            Column3.CellEditor = new BooleanCellEditor();

            var Column4 = new Column("DateTime");
            Column4.ValueGetter = (object item) => ((TestItem)item).Date;
            var Temp = (TextCellRenderer)Column4.CellRenderer;
            Temp.Format = "{0:F}";

            dList1.Columns.Add(Column1);
            dList1.Columns.Add(Column2);
            dList1.Columns.Add(Column3);
            dList1.Columns.Add(Column4);

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
        }

        private void ItemCollectionChanged(object sender, NotifyingCollectionChangedEventArgs args)
        {
            string Entry = string.Empty;

            switch (args.Action)
            {
                case NotifyingCollectionChangeAction.Add:
                    Entry = String.Format("{0} == ADD {1}[{2}]", nameof(ItemCollectionChanged), args.NewItem, args.NewItemIndex);
                    break;
                case NotifyingCollectionChangeAction.Remove:
                    Entry = String.Format("{0} == REMOVE {1}[{2}]", nameof(ItemCollectionChanged), args.OldItem, args.OldItemIndex);
                    break;
                case NotifyingCollectionChangeAction.Replace:
                    Entry = String.Format("{0} == REPLACE {1}[{2}] with {3}[{4}]", nameof(ItemCollectionChanged), args.OldItem, args.OldItemIndex, args.NewItem, args.NewItemIndex);
                    break;
                case NotifyingCollectionChangeAction.Clear:
                    Entry = String.Format("{0} == CLEAR", nameof(ItemCollectionChanged));
                    break;
                case NotifyingCollectionChangeAction.Sort:
                    Entry = String.Format("{0} == SORT", nameof(ItemCollectionChanged));
                    break;
                default:
                    break;
            }

            listBoxEvents.Items.Add(Entry);
        }

        private void ItemChanged(object sender, ItemPropertyChangedEventArgs args)
        {
            string Entry = String.Format("ItemChanged [{0}] == {1}", args.PropertyName, args.Item);
            listBoxEvents.Items.Add(Entry);
        }

        private void buttonAddRngItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonRemoveItem_Click(object sender, EventArgs e)
        {
            if (dList1.Items.Count > 0)
            {
                dList1.Items.RemoveAt(dList1.Items.Count - 1);
            }
        }
    }
}
