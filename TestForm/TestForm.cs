using System;
using System.Windows.Forms;

using InCoding.DList;
using InCoding.DList.Collections;
using InCoding.DList.Rendering;

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
            Column1.Width = 215;
            var Column2 = new Column("Numeric");
            Column2.ValueGetter = (object item) => ((TestItem)item).NumericValue;
            var Column3 = new Column("Boolean");
            Column3.Width = 150;
            Column3.ValueGetter = (object item) => ((TestItem)item).Flag;
            var Column4 = new Column("DateTime");
            Column4.ValueGetter = (object item) => ((TestItem)item).Date;
            var Temp = (TextCellRenderer)Column4.CellRenderer;
            Temp.Format = "{0:F}";

            dList1.Columns.Add(Column1);
            dList1.Columns.Add(Column2);
            dList1.Columns.Add(Column3);
            dList1.Columns.Add(Column4);

            // Items
            var Item0 = new TestItem("Item0", 0, true, DateTime.Now);
            var Item1 = new TestItem("Item1", 1, false, DateTime.Now);
            var Item2 = new TestItem("Item2", 2, true, DateTime.Now);
            var Item3 = new TestItem("Item3", 3, false, DateTime.Now);
            var Item4 = new TestItem("Item4", 4, true, DateTime.Now);
            var Item5 = new TestItem("Item5", 5, false, DateTime.Now);
            var Item6 = new TestItem("Item6", 6, true, DateTime.Now);
            var Item7 = new TestItem("Item7", 7, false, DateTime.Now);
            var Item8 = new TestItem("Item8", 8, true, DateTime.Now);
            var Item9 = new TestItem("Item9", 9, false, DateTime.Now);

            dList1.Items.Add(Item0);
            dList1.Items.Add(Item1);
            dList1.Items.Add(Item2);
            dList1.Items.Add(Item3);
            dList1.Items.Add(Item4);
            dList1.Items.Add(Item5);
            dList1.Items.Add(Item6);
            dList1.Items.Add(Item7);
            dList1.Items.Add(Item8);
            dList1.Items.Add(Item9);
        }

        private void ItemCollectionChanged(object sender, NotifyingCollectionChangedEventArgs args)
        {
            string Entry = string.Empty;

            switch (args.Action)
            {
                case ItemCollectionChangeAction.Add:
                    Entry = String.Format("{0} == ADD {1}[{2}]", nameof(ItemCollectionChanged), args.NewItem, args.NewItemIndex);
                    break;
                case ItemCollectionChangeAction.Remove:
                    Entry = String.Format("{0} == REMOVE {1}[{2}]", nameof(ItemCollectionChanged), args.OldItem, args.OldItemIndex);
                    break;
                case ItemCollectionChangeAction.Replace:
                    Entry = String.Format("{0} == REPLACE {1}[{2}] with {3}[{4}]", nameof(ItemCollectionChanged), args.OldItem, args.OldItemIndex, args.NewItem, args.NewItemIndex);
                    break;
                case ItemCollectionChangeAction.Clear:
                    Entry = String.Format("{0} == CLEAR", nameof(ItemCollectionChanged));
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
