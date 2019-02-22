using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using InCoding.DList.Collections;
using InCoding.DList.Rendering;

namespace InCoding.DList
{
    public class DList : Control
    {
        [DefaultValue(true)]
        [Category("Appearance")]
        public bool ShowGrid { get; set; } = true;

        [DefaultValue(typeof(Color), "Black")]
        [Category("Appearance")]
        public Color GridColor { get; set; } = Color.Black;

        private int _ItemHeight = 28;

        [DefaultValue(28)]
        [Category("Layout")]
        public int ItemHeight
        {
            get => _ItemHeight;
            set
            {
                if (value > 0)
                {
                    _ItemHeight = value;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ContentRectangle { get; private set; }

        private int _SelectedItemIndex = -1;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedItemIndex
        {
            get => _SelectedItemIndex;
            set
            {
                if (_SelectedItemIndex != value)
                {
                    _SelectedItemIndex = value;
                    Invalidate();
                }
            }
        }

        private int _HotItemIndex = -1;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int HotItemIndex
        {
            get => _HotItemIndex;
            private set
            {
                if (_HotItemIndex != value)
                {
                    _HotItemIndex = value;
                    Invalidate();
                }
            }
        }

        private int _HotColumnIndex = -1;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int HotColumnIndex
        {
            get => _HotColumnIndex;
            private set
            {
                if (_HotColumnIndex != value)
                {
                    _HotColumnIndex = value;
                    Invalidate();
                }
            }
        }

        private int _PressedColumnIndex = -1;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PressedColumnIndex
        {
            get => _PressedColumnIndex;
            private set
            {
                if (_PressedColumnIndex != value)
                {
                    _PressedColumnIndex = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComplexRenderer HeaderRenderer { get; set; } = new HeaderRenderer();

        [Category("Data")]
        public NotifyingCollection<Column> Columns { get; } = new NotifyingCollection<Column>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NotifyingCollection<object> Items { get; } = new NotifyingCollection<object>();

        protected VisualStyleRenderer VsRenderer { get; private set; }

        protected HScrollBar HScroll { get; }
        protected VScrollBar VScroll { get; }

        protected override Size DefaultMinimumSize => new Size(4 * ItemHeight, ItemHeight * 2);


        public DList()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw,
                true);

            HScroll = new HScrollBar();
            HScroll.Visible = false;
            HScroll.Minimum = 0;
            HScroll.ValueChanged += ScrollValueChanged;
            Controls.Add(HScroll);

            VScroll = new VScrollBar();
            VScroll.Visible = false;
            VScroll.Minimum = 0;
            VScroll.ValueChanged += ScrollValueChanged;
            Controls.Add(VScroll);

            Items.CollectionChanged += ItemCollectionChanged;
            Items.ItemChanged += ItemChanged;

            Columns.CollectionChanged += ColumnCollectionChanged;
            Columns.ItemChanged += ColumnChanged;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var Gfx = e.Graphics;
            ContentRectangle = DrawBackground(Gfx);
            Gfx.SetClip(ContentRectangle);

            UpdateScrollBars();
            Gfx.SetClip(ContentRectangle);

            if (Items.Count > 0)
            {
                int FirstVisibleItemIndex = (!VScroll.Visible) ? 0 : Math.Max(0, (VScroll.Value - ItemHeight) / ItemHeight);
                DrawItems(Gfx, FirstVisibleItemIndex);
            }

            if (Columns.Count > 0)
            {
                DrawHeaders(Gfx);
                if (ShowGrid) DrawGrid(Gfx);
            }

            base.OnPaint(e);

            // TODO: Remove
            //Console.WriteLine("CR: {0} vs {1}", ContentRectangle, (Items.Count + 1) * ItemHeight);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            // TODO: remove
            Console.WriteLine("Mouse CLICK - Button: {0}, Clicks: {1}, WheelDelta: {2}, Pos: {3}", e.Button, e.Clicks, e.Delta, e.Location);

            if (e.Button == MouseButtons.Left)
            {
                SelectedItemIndex = GetItemIndexAt(e.X + HScroll.Value, e.Y + VScroll.Value);

                // TODO: remove
                Console.WriteLine("Mouse CLICK - SelectedItemIndex: {0}", SelectedItemIndex);
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int ContentX = e.X + HScroll.Value;
            int ContentY = e.Y + VScroll.Value;
            HotItemIndex = GetItemIndexAt(ContentX, ContentY);
            HotColumnIndex = GetColumnIndexAt(ContentX, ContentY);

            // TODO: remove
            Console.WriteLine("HotItem: {0}, HotColumn {1}", HotItemIndex, HotColumnIndex);

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HotItemIndex = -1;
            HotColumnIndex = -1;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (VScroll.Visible)
            {
                int ScrollDelta = (e.Delta / SystemInformation.MouseWheelScrollDelta) * ItemHeight * SystemInformation.MouseWheelScrollLines;
                int NewScrollValue = VScroll.Value - ScrollDelta;

                VScroll.Value = NewScrollValue.Clamp(VScroll.Minimum, VScroll.Maximum - VScroll.LargeChange + 1);

                Console.WriteLine("Old: {0} New: {1}", VScroll.Value, NewScrollValue);
            }

            base.OnMouseWheel(e);
        }

        protected void ItemCollectionChanged(object sender, NotifyingCollectionChangedEventArgs e)
        {
            Invalidate();
        }

        protected void ItemChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            Invalidate();
        }

        protected void ColumnCollectionChanged(object sender, NotifyingCollectionChangedEventArgs e)
        {
            Invalidate();
        }

        protected void ColumnChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            Invalidate();
        }

        protected void ScrollValueChanged(object sender, EventArgs e)
        {
            Invalidate();

            // TODO: Remove
            ScrollBar SBar = (ScrollBar)sender;
            Console.WriteLine("{0}, LC: {1}", SBar, SBar.LargeChange);
        }

        protected Rectangle DrawBackground(Graphics gfx)
        {
            if (SetVisualStyleRendererElement(VisualStyleElement.TextBox.TextEdit.Normal))
            {
                VsRenderer.DrawBackground(gfx, ClientRectangle);

                return VsRenderer.GetBackgroundContentRectangle(gfx, ClientRectangle);
            }
            else
            {
                gfx.DrawRectangle(Pens.Black, ClientRectangle.ToGDI()); // @GDI+

                return new Rectangle(ClientRectangle.X + 1, ClientRectangle.Y + 1, ClientRectangle.Width - 2, ClientRectangle.Height - 2);
            }
        }

        protected void DrawHeaders(Graphics gfx)
        {
            var Bounds = new Rectangle(ContentRectangle.Left - HScroll.Value, ContentRectangle.Top, 0, ItemHeight);
            int Index = 0;
            RenderState State;

            foreach (var column in Columns)
            {
                if (Bounds.X >= ContentRectangle.Right) break;
                Bounds.Width = column.Width;
                State = (Index == HotColumnIndex) ? RenderState.Hot : (Index == PressedColumnIndex) ? RenderState.Pressed : RenderState.Normal;
                HeaderRenderer.Draw(gfx, Bounds, State, column.Name, ForeColor, BackColor, Font);
                Bounds.X += column.Width;
                Index++;
            }
        }

        protected void DrawItems(Graphics gfx, int firstItemIndex)
        {
            var Bounds = new Rectangle(ContentRectangle.Left - HScroll.Value, ContentRectangle.Top + ItemHeight, 0, ItemHeight);

            foreach (var column in Columns)
            {
                if (Bounds.X > ContentRectangle.Right) break;

                Bounds.Width = column.Width;
                Bounds.Y -= (VScroll.Value - firstItemIndex * ItemHeight);

                for (int x = firstItemIndex; x < Items.Count; x++)
                {
                    if (Bounds.Y > ContentRectangle.Bottom) break;

                    var Item = Items[x];
                    var CellValue = column.GetValue(Item);
                    var State = (SelectedItemIndex == x) ? RenderState.Selected : (HotItemIndex == x) ? RenderState.Hot : RenderState.Normal;

                    column.CellRenderer.Draw(gfx, Bounds, State, CellValue, ForeColor, BackColor, Font);

                    Bounds.Y += ItemHeight;
                }

                Bounds.X += column.Width;
                Bounds.Y = ContentRectangle.Top + ItemHeight;
            }
        }

        protected void DrawGrid(Graphics gfx)
        {
            Color Color = (SetVisualStyleRendererElement(VisualStyleElement.Header.Item.Normal)) ? VsRenderer.GetColor(ColorProperty.EdgeFillColor) : GridColor;
            var X = ContentRectangle.X - 1 - HScroll.Value;
            var Y1 = ContentRectangle.Y + ItemHeight;
            var Y2 = Math.Min(ContentRectangle.Bottom - 1, Y1 + Items.Count * ItemHeight - 1);

            using (var GridPen = new Pen(Color))
            {
                var TotalColumnWidth = 0;

                // Vertical grid lines.
                foreach (var column in Columns)
                {
                    TotalColumnWidth += column.Width;
                    X += column.Width;
                    gfx.DrawLine(GridPen, X, Y1, X, Y2);
                }

                // Horizontal grid lines.
                var X1 = ContentRectangle.X;
                var X2 = X1 + TotalColumnWidth - 1;
                var Y = ContentRectangle.Top + ItemHeight - 1;

                // Headers
                gfx.DrawLine(GridPen, X1, Y, X2, Y);
                Y += ItemHeight - VScroll.Value;

                // Items
                foreach (var item in Items)
                {
                    if ((Y >= ContentRectangle.Y + ItemHeight) && (Y < ContentRectangle.Bottom))
                    {
                        gfx.DrawLine(GridPen, X1, Y, X2, Y);
                    }

                    Y += ItemHeight;
                }
            }
        }

        protected void UpdateScrollBars()
        {
            if (Columns.Count == 0) return;

            // Vertical
            int TotalContentHeight = (Items.Count + 1) * ItemHeight;
            int ContentHeight = (!HScroll.Visible) ? ContentRectangle.Height : ContentRectangle.Height - (HScroll.Height + 2);

            if (TotalContentHeight > ContentHeight)
            {
                VScroll.Left = ContentRectangle.Right - 1 - VScroll.Width;
                VScroll.Top = ContentRectangle.Top + 1;
                VScroll.Minimum = 0;
                VScroll.SmallChange = ItemHeight;

                if (VScroll.Value + VScroll.LargeChange - 1 > VScroll.Maximum)
                {
                    VScroll.Value = VScroll.Maximum - VScroll.LargeChange;
                }

                VScroll.Visible = true;
                VScroll.Update();

                var NewContentRectangle = ContentRectangle;
                NewContentRectangle.Width -= (VScroll.Width + 2);
                ContentRectangle = NewContentRectangle;
            }
            else
            {
                VScroll.Minimum = 0;
                VScroll.Maximum = 0;
                VScroll.Value = 0;
                VScroll.Visible = false;
            }

            // Horizontal
            int TotalContentWidth = 0;

            foreach (var column in Columns)
            {
                TotalContentWidth += column.Width;
            }

            if (TotalContentWidth > ContentRectangle.Width)
            {
                HScroll.Left = ContentRectangle.Left + 1;
                HScroll.Top = ContentRectangle.Bottom - 1 - HScroll.Height;
                HScroll.Minimum = 0;

                if (HScroll.Value + HScroll.LargeChange - 1 > HScroll.Maximum)
                {
                    HScroll.Value = HScroll.Maximum - HScroll.LargeChange;
                }

                HScroll.Visible = true;
                HScroll.Update();

                var NewContentRectangle = ContentRectangle;
                NewContentRectangle.Height -= (HScroll.Height + 2);
                ContentRectangle = NewContentRectangle;
            }
            else
            {
                HScroll.Minimum = 0;
                HScroll.Maximum = 0;
                HScroll.Value = 0;
                HScroll.Visible = false;
            }

            // Set values that are dependent on scroll bar visibility.
            VScroll.Height = ContentRectangle.Height - 2;
            VScroll.Maximum = TotalContentHeight - ItemHeight - 1;
            VScroll.LargeChange = Math.Max(0, ContentRectangle.Height - ItemHeight);

            HScroll.Width = ContentRectangle.Width - 2;
            HScroll.Maximum = TotalContentWidth - 1;
            HScroll.SmallChange = HScroll.LargeChange / 10;
            HScroll.LargeChange = Math.Max(0, ContentRectangle.Width);
        }

        protected bool SetVisualStyleRendererElement(VisualStyleElement element)
        {
            if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(element))
            {
                if (VsRenderer == null)
                {
                    VsRenderer = new VisualStyleRenderer(element);
                }
                else
                {
                    VsRenderer.SetParameters(element);
                }

                return true;
            }

            return false;
        }

        public int GetItemIndexAt(int x, int y)
        {
            // TODO: Think about ContentRectanlge
            if (x < 0) return -1;
            if (y < 0) return -1;
            if (Items.Count <= 0) return -1;
            if (Columns.Count <= 0) return -1;

            int LastColumnRight = 0;

            foreach (var column in Columns)
            {
                LastColumnRight += column.Width;
            }

            if (x >= LastColumnRight) return -1;

            int ItemIndex = (int)Math.Floor((y - ItemHeight) / (double)ItemHeight);

            if (ItemIndex >= Items.Count)
            {
                ItemIndex = -1;
            }

            ItemIndex = Math.Max(-1, ItemIndex);

            return ItemIndex;
        }

        public int GetColumnIndexAt(int x, int y)
        {
            if (x < 0) return -1;
            if ((y < 0) || (y > ItemHeight)) return -1;

            int Right = 0;
            int Index = 0;

            foreach (var column in Columns)
            {
                Right += column.Width;

                if (x < Right) return Index;

                Index++;
            }

            return -1;
        }
    }
}
