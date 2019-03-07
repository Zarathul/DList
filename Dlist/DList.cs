﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using InCoding.DList.Collections;
using InCoding.DList.Rendering;
using InCoding.DList.Editing;

/*
 * Tag notes:
 * ----------
 * @GDI: This is used to tag functions that expect rectangles in GDI off-by-one style. 
 * Graphics.DrawRectangle() for example will draw from (X,Y) to (Right,Bottom) where 
 * Right = X + Width and Bottom = Y + Height. Therefore drawing a rectangle at (0, 0) with 
 * a width and height of 10 will result in a 11 pixels wide and high rectangle. What makes 
 * things worse is that this is not true for every method. Some honor the width and height 
 * properties, while others don't. 
 * System.Drawing.Rectancle adopted the GDI way of calculating Right and Bottom, so all 
 * code in this project is (or should be written) assuming those properties return wrong 
 * values. Meaning either don't use them or subtract one. Therefore when passing rectangles 
 * to affected framework methods Utils.ToGDI() should be used.
 */

namespace InCoding.DList
{
    public class DList : Control
    {
        private bool _ShowGrid = true;
        private Color _SelectedItemColor = SystemColors.Highlight;
        private Color _HotItemColor = SystemColors.HotTrack;
        private Color _GridColor = Color.Black;
        private Color _HighlightTextColor = SystemColors.HighlightText;
        private Color _SelectionRectangleColor = SystemColors.HotTrack;
        private Color _SelectionRectangleBorderColor = Color.Black;
        private int _ItemHeight = 28;
        private int _ResizeGripWidth = 10;
        private int _FocusedItemIndex = -1;
        private int _SelectedItemIndex = -1;
        private int _HotItemIndex = -1;
        private int _HotColumnIndex = -1;
        private int _PressedColumnIndex = -1;
        private Point _ItemSelectionEnd = Point.Empty;

        private bool _AddToSelection = false;
        private bool _FirstPaint = true;
        private ICellEditor _ActiveCellEditor;

        [DefaultValue(true)]
        [Category("Appearance")]
        public bool ShowGrid
        {
            get => _ShowGrid;
            set
            {
                if (_ShowGrid != value)
                {
                    _ShowGrid = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(true)]
        [Category("Behavior")]
        public bool AllowMultipleSelectedItems { get; set; } = true;

        [DefaultValue(typeof(SystemColors), "Highlight")]
        [Category("Appearance")]
        public Color SelectedItemColor
        {
            get => _SelectedItemColor;
            set
            {
                if (_SelectedItemColor != value)
                {
                    _SelectedItemColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(SystemColors), "HotTrack")]
        [Category("Appearance")]
        public Color SelectionRectangleColor
        {
            get => _SelectionRectangleColor;
            set
            {
                if (_SelectionRectangleColor != value)
                {
                    _SelectionRectangleColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "Black")]
        [Category("Appearance")]
        public Color SelectionRectangleBorderColor
        {
            get => _SelectionRectangleBorderColor;
            set
            {
                if (_SelectionRectangleBorderColor != value)
                {
                    _SelectionRectangleBorderColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(SystemColors), "HotTrack")]
        [Category("Appearance")]
        public Color HotItemColor
        {
            get => _HotItemColor;
            set
            {
                if (_HotItemColor != value)
                {
                    _HotItemColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "Black")]
        [Category("Appearance")]
        public Color GridColor
        {
            get => _GridColor;
            set
            {
                if (_GridColor != value)
                {
                    _GridColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(SystemColors), "HighlightText")]
        [Category("Appearance")]
        public Color HighlightTextColor
        {
            get => _HighlightTextColor;
            set
            {
                if (_HighlightTextColor != value)
                {
                    _HighlightTextColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(28)]
        [Category("Layout")]
        public int ItemHeight
        {
            get => _ItemHeight;
            set
            {
                if (value > 0 && _ItemHeight != value)
                {
                    _ItemHeight = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(10)]
        [Category("Layout")]
        public int ResizeGripWidth
        {
            get => _ResizeGripWidth;
            set
            {
                if (value > 0)
                {
                    _ResizeGripWidth = value;
                }
            }
        }

        [DefaultValue(true)]
        [Category("Layout")]
        public bool AllowResize { get; set; } = true;

        [DefaultValue(false)]
        [Category("Layout")]
        public bool IntegralHeight { get; set; } = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ContentRectangle { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NotifyingCollection<int> SelectedItemIndices { get; } = new NotifyingCollection<int>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FocusedItemIndex
        {
            get => _FocusedItemIndex;
            set
            {
                if (_FocusedItemIndex != value)
                {
                    _FocusedItemIndex = value;
                    Invalidate();
                }
            }
        }

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
        protected Point ItemSelectionStart { get; private set; } = Point.Empty;
        protected Point ItemSelectionEnd
        {
            get => _ItemSelectionEnd;
            private set
            {
                _ItemSelectionEnd = value;
                Invalidate();
            }
        }

        protected override Size DefaultMinimumSize => new Size(4 * ItemHeight, ItemHeight * 2);

        public DList()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw,
                true);

            SetStyle(ControlStyles.StandardDoubleClick, false);

            HScroll = new HScrollBar
            {
                Visible = false,
                Minimum = 0,
            };

            HScroll.ValueChanged += ScrollValueChanged;
            Controls.Add(HScroll);

            VScroll = new VScrollBar
            {
                Visible = false,
                Minimum = 0
            };

            VScroll.ValueChanged += ScrollValueChanged;
            Controls.Add(VScroll);

            Items.CollectionChanged += ItemCollectionChanged;
            Items.ItemChanged += ItemChanged;

            Columns.CollectionChanged += ColumnCollectionChanged;
            Columns.ItemChanged += ColumnChanged;

            SelectedItemIndices.CollectionChanged += SelectedItemIndicesChanged;
        }

        #region Drawing

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width == 0 || Height == 0)
            {
                base.OnPaint(e);
                return;
            }

            var Gfx = e.Graphics;
            ContentRectangle = DrawBackground(Gfx);

            UpdateScrollBars();
            Gfx.SetClip(ContentRectangle);

            if (Items.Count > 0)
            {
                int FirstVisibleItemIndex = GetFirstVisibleItemIndex();
                DrawItems(Gfx, FirstVisibleItemIndex);

                if (FocusedItemIndex >= 0) DrawFocusRectangle(Gfx);
                if (!ItemSelectionStart.IsEmpty && !ItemSelectionEnd.IsEmpty) DrawSelectionRectangle(Gfx);
            }

            if (Columns.Count > 0)
            {
                DrawHeaders(Gfx);
                if (ShowGrid) DrawGrid(Gfx);
            }

            base.OnPaint(e);

            // This is necessary in integral height mode. Because the ContentRectangle is constructed here in 
            // OnPaint() and when SetBoundsCore() is called the first few times, OnPaint() has not been called yet. 
            // Therefore, the height calculation in SetBoundsCore() will fail. Just calling SetBoundsCore() once
            // after the ContentRectangle has been created will fix this.
            if (_FirstPaint)
            {
                _FirstPaint = false;
                SetBoundsCore(Left, Top, Width, Height, BoundsSpecified.All);
            }
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
                gfx.DrawRectangle(Pens.Black, ClientRectangle.ToGDI()); // @GDI

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
                    var State = ((AllowMultipleSelectedItems && SelectedItemIndices.Contains(x)) || SelectedItemIndex == x)
                        ? RenderState.Selected
                        : (HotItemIndex == x) ? RenderState.Hot : RenderState.Normal;

                    if (FocusedItemIndex == x)
                    {
                        State |= RenderState.Focused;
                    }

                    var TextColor = (State.HasFlag(RenderState.Normal)) ? ForeColor : HighlightTextColor;
                    var BackgroundColor = (State.HasFlag(RenderState.Selected))
                        ? SelectedItemColor
                        : (State.HasFlag(RenderState.Hot)) ? HotItemColor : BackColor;

                    column.CellRenderer.Draw(gfx, Bounds, State, CellValue, TextColor, BackgroundColor, Font);

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

            using (var gridPen = new Pen(Color))
            {
                var TotalColumnWidth = 0;

                // Vertical grid lines.
                foreach (var column in Columns)
                {
                    TotalColumnWidth += column.Width;
                    X += column.Width;
                    gfx.DrawLine(gridPen, X, Y1, X, Y2);
                }

                // Horizontal grid lines.
                var X1 = ContentRectangle.X;
                var X2 = X1 + TotalColumnWidth - 1;
                var Y = ContentRectangle.Top + ItemHeight - 1;

                // Headers
                gfx.DrawLine(gridPen, X1, Y, X2, Y);
                Y += ItemHeight - VScroll.Value;

                // Items
                foreach (var item in Items)
                {
                    if ((Y >= ContentRectangle.Y + ItemHeight) && (Y < ContentRectangle.Bottom))
                    {
                        gfx.DrawLine(gridPen, X1, Y, X2, Y);
                    }

                    Y += ItemHeight;
                }
            }
        }

        protected void DrawFocusRectangle(Graphics gfx)
        {
            var ItemPos = new Point(ContentRectangle.X, ContentRectangle.Y - VScroll.Value + (FocusedItemIndex + 1) * ItemHeight);
            var ItemSize= new Size(0, ItemHeight);

            foreach (var column in Columns)
            {
                ItemSize.Width += column.Width;
            }

            // The grid, if drawn, occupies space in the cell. The most right and most bottom pixels.
            // Because of this the focus rectangle is drawn 1 pixel smaller if the grid is rendered.
            if (ShowGrid)
            {
                ItemSize.Width--;
                ItemSize.Height--;
            }

            var Bounds = new Rectangle(ItemPos, ItemSize);
            ControlPaint.DrawFocusRectangle(gfx, Bounds);
        }

        protected void DrawSelectionRectangle(Graphics gfx)
        {
            var SelectionRectangle = Utils.GetRectangleFromPoints(ItemSelectionStart, ItemSelectionEnd).ToGDI();
            SelectionRectangle.Offset(-HScroll.Value, -VScroll.Value);

            // TODO: remove
            //Console.WriteLine("Select DRAW - {0}", SelectionRectangle);

            int Alpha = (SelectionRectangleColor.A <= 200) ? SelectionRectangleColor.A : 128;

            using (var rectangleBrush = new SolidBrush(Color.FromArgb(Alpha, SelectionRectangleColor)))
            {
                gfx.FillRectangle(rectangleBrush, SelectionRectangle);
            }

            using (Pen framePen = new Pen(SelectionRectangleBorderColor))
            {
                gfx.DrawRectangle(framePen, SelectionRectangle);
            }
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

        #endregion

        #region Key handling

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData.HasFlag(Keys.Up)
                || keyData.HasFlag(Keys.Down)
                || keyData.HasFlag(Keys.Left)
                || keyData.HasFlag(Keys.Right))
            {
                return true;
            }

            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    HandleArrowUp(e.Modifiers);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    HandleArrowDown(e.Modifiers);
                    e.Handled = true;
                    break;

                case Keys.Left:
                    HandleArrowLeft(e.Modifiers);
                    e.Handled = true;
                    break;

                case Keys.Right:
                    HandleArrowRight(e.Modifiers);
                    e.Handled = true;
                    break;

                case Keys.PageUp:
                    ScrollPageUp();
                    e.Handled = true;
                    break;

                case Keys.PageDown:
                    ScrollPageDown();
                    e.Handled = true;
                    break;

                case Keys.Home:
                    ScrollToBegin();
                    e.Handled = true;
                    break;

                case Keys.End:
                    ScrollToEnd();
                    e.Handled = true;
                    break;

                case Keys.Space:
                    HandleSpace(e.Modifiers);
                    e.Handled = true;
                    break;

                case Keys.A:
                    if (e.Modifiers.HasFlag(Keys.Control))
                    {
                        if (e.Modifiers.HasFlag(Keys.Shift))
                        {
                            SelectedItemIndices.Clear();
                        }
                        else
                        {
                            SelectAllItems();
                        }

                        e.Handled = true;
                    }
                    break;

#if DEBUG
                case Keys.F12:
                    int TotalColumnWidth = 0;

                    foreach (var column in Columns)
                    {
                        TotalColumnWidth += column.Width;
                    }

                    var HeadersArea = new Rectangle(ContentRectangle.X, ContentRectangle.Y, TotalColumnWidth, ItemHeight);
                    var ItemArea = new Rectangle(ContentRectangle.X, HeadersArea.Bottom, TotalColumnWidth, Items.Count * ItemHeight);
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.WriteLine("| Right and bottom are the actual coordinates not the rectangle values.       |");
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.WriteLine("|               Content: {0} -> Right:{1}, Bottom:{2}", ContentRectangle, ContentRectangle.Right - 1, ContentRectangle.Bottom - 1);
                    Console.WriteLine("|               Headers: {0} -> Right:{1}, Bottom:{2}", HeadersArea, HeadersArea.Right - 1, HeadersArea.Bottom - 1);
                    Console.WriteLine("|                 Items: {0} -> Right:{1}, Bottom:{2}", ItemArea, ItemArea.Right - 1, ItemArea.Bottom - 1);
                    Console.WriteLine("|     SelectedItemCount: {0}", SelectedItemIndices.Count);
                    Console.WriteLine("|     SelectedItemIndex: {0}", SelectedItemIndex);
                    Console.WriteLine("|      FocusedItemIndex: {0}", FocusedItemIndex);
                    Console.WriteLine("|          HotItemIndex: {0}", HotItemIndex);
                    Console.WriteLine("|        HotColumnIndex: {0}", HotColumnIndex);
                    Console.WriteLine("|    PressedColumnIndex: {0}", PressedColumnIndex);
                    Console.WriteLine("| FirstVisibleItemIndex: {0}", GetFirstVisibleItemIndex());
                    Console.WriteLine("|  LastVisibleItemIndex: {0}", GetLastVisibleItemIndex());
                    Console.Write(    "|       SelectedIndices: ");

                    if (SelectedItemIndices.Count >= 1)
                    {
                        Console.Write("{0}", SelectedItemIndices[0]);
                    }

                    for (int i = 1; i < SelectedItemIndices.Count; i++)
                    {
                        Console.Write(", {0}", SelectedItemIndices[i]);
                    }

                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------------------------------------");

                    e.Handled = true;
                    break;
#endif

                default:
                    break;
            }

            base.OnKeyDown(e);
        }

        protected void HandleSpace(Keys modifiers)
        {
            if (AllowMultipleSelectedItems)
            {
                if (SelectedItemIndices.Contains(FocusedItemIndex))
                {
                    SelectedItemIndices.Remove(FocusedItemIndex);
                }
                else
                {
                    SelectedItemIndices.Add(FocusedItemIndex);
                }
            }
            else
            {
                if (SelectedItemIndex == FocusedItemIndex)
                {
                    SelectedItemIndex = -1;
                }
                else
                {
                    SelectedItemIndex = FocusedItemIndex;
                }
            }
        }

        protected void HandleArrowUp(Keys modifiers)
        {
            if (Items.Count == 0) return;

            if (FocusedItemIndex >= 1)
            {
                FocusedItemIndex -= 1;
            }

            if (!AllowMultipleSelectedItems)
            {
                if (!modifiers.HasFlag(Keys.Control))
                {
                    SelectedItemIndex = FocusedItemIndex;
                }
            }
            else
            {
                if (!modifiers.HasFlag(Keys.Control))
                {
                    if (!modifiers.HasFlag(Keys.Shift))
                    {
                        SelectedItemIndices.Clear();
                    }

                    SelectedItemIndices.Add(FocusedItemIndex);
                }
            }

            EnsureItemVisibility(FocusedItemIndex);
        }

        protected void HandleArrowDown(Keys modifiers)
        {
            if (Items.Count == 0) return;

            if (FocusedItemIndex < Items.Count - 1)
            {
                FocusedItemIndex += 1;
            }

            if (!AllowMultipleSelectedItems)
            {
                if (!modifiers.HasFlag(Keys.Control))
                {
                    SelectedItemIndex = FocusedItemIndex;
                }
            }
            else
            {
                if (!modifiers.HasFlag(Keys.Control))
                {
                    if (!modifiers.HasFlag(Keys.Shift))
                    {
                        SelectedItemIndices.Clear();
                    }

                    SelectedItemIndices.Add(FocusedItemIndex);
                }
            }

            EnsureItemVisibility(FocusedItemIndex);
        }

        protected void HandleArrowLeft(Keys modifiers)
        {
            if (Items.Count == 0 || !HScroll.Visible) return;

            if (HScroll.Value > HScroll.Minimum)
            {
                HScroll.Value -= (HScroll.SmallChange > HScroll.Value) ? HScroll.Value : HScroll.SmallChange;

                // TODO: remove
                //Console.WriteLine("Left: {0}", HScroll);
            }
        }

        protected void HandleArrowRight(Keys modifiers)
        {
            if (Items.Count == 0 || !HScroll.Visible) return;

            int MaxHScrollValue = HScroll.Maximum - HScroll.LargeChange;

            if (HScroll.Value < MaxHScrollValue)
            {
                int RemainingHScrollSpace = MaxHScrollValue - HScroll.Value;
                HScroll.Value += (HScroll.SmallChange > RemainingHScrollSpace) ? RemainingHScrollSpace : HScroll.SmallChange;

                // TODO: remove
                //Console.WriteLine("Right: {0}, Max: {1}, Remainder: {2}", HScroll, MaxHScrollValue, RemainingHScrollSpace);
            }
        }

        #endregion

        #region Mouse handling

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();

            if (HotColumnIndex >= 0 && Cursor.Current != Cursors.VSplit)
            {
                PressedColumnIndex = HotColumnIndex;
                HotColumnIndex = -1;
            }
            else if (AllowMultipleSelectedItems && e.Button == MouseButtons.Left)
            {
                StartSelectionRectangle(e.X, e.Y);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (PressedColumnIndex >= 0)
            {
                HotColumnIndex = PressedColumnIndex;
                PressedColumnIndex = -1;
            }

            if (AllowMultipleSelectedItems)
            {
                EndSelectionRectangle();
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            // TODO: remove
            Console.WriteLine("Mouse CLICK - Button: {0}, Clicks: {1}, WheelDelta: {2}, Pos: {3}", e.Button, e.Clicks, e.Delta, e.Location);

            if (e.Button == MouseButtons.Left)
            {
                int OldFocusIndex = FocusedItemIndex;
                int SelectedIndex = GetItemIndexAt(e.X, e.Y);

                if (AllowMultipleSelectedItems)
                {
                    // Select only the item under the cursor if there is no selection rectangle.
                    if (ItemSelectionEnd.IsEmpty)
                    {
                        if (ModifierKeys.HasFlag(Keys.Shift) || ModifierKeys.HasFlag(Keys.Control))
                        {
                            if (SelectedIndex >= 0 && !SelectedItemIndices.Contains(SelectedIndex))
                            {
                                SelectedItemIndices.Add(SelectedIndex);
                            }
                        }
                        else
                        {
                            SelectedItemIndices.Clear();

                            if (SelectedIndex >= 0)
                            {
                                SelectedItemIndices.Add(SelectedIndex);
                            }
                        }

                        FocusedItemIndex = SelectedIndex;
                    }
                }
                else
                {
                    SelectedItemIndex = SelectedIndex;
                    FocusedItemIndex = SelectedIndex;
                }

                // Begin cell editing if possible.
                if (ItemSelectionEnd.IsEmpty && FocusedItemIndex >= 0 && (FocusedItemIndex == OldFocusIndex)
                    && !ModifierKeys.HasFlag(Keys.Shift) && !ModifierKeys.HasFlag(Keys.Control))
                {
                    int ColumnIndex = GetColumnIndexAt(e.X);
                    BeginCellEdit(ColumnIndex, FocusedItemIndex);
                }

                // TODO: remove
                //Console.WriteLine("Mouse CLICK - SelectedItemIndex: {0}", SelectedItemIndex);
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                HotItemIndex = GetItemIndexAt(e.X, e.Y);
                HotColumnIndex = GetColumnHeaderIndexAt(e.X, e.Y);

                if (AllowResize)
                {
                    // Change the mouse cursor if columns can be resized an the cursor inside the grip range.
                    if (HotColumnIndex >= 0)
                    {
                        int RightColumnEdge = ContentRectangle.X;

                        for (int i = 0; i <= HotColumnIndex; i++)
                        {
                            RightColumnEdge += Columns[i].Width;
                        }

                        RightColumnEdge--;

                        int ScrolledMouseX = e.X + HScroll.Value;
                        int GripStart = RightColumnEdge - ResizeGripWidth;

                        // TODO: Remove
                        //Console.WriteLine("Lim: {0}, Right {2}, SX: {1}", GripStart, ScrolledMouseX, RightColumnEdge);

                        if (ScrolledMouseX >= GripStart && ScrolledMouseX <= RightColumnEdge)
                        {
                            Cursor = Cursors.VSplit;
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                    }
                }
                // TODO: remove
                //Console.WriteLine("HotItem: {0}, HotColumn {1}, Col {2}", HotItemIndex, HotColumnIndex, GetColumnIndexAt(e.X));
            }
            else if (e.Button == MouseButtons.Left)
            {
                // Resize the hot column.
                if (Cursor == Cursors.VSplit)
                {
                    int RightColumnEdge = ContentRectangle.X;

                    for (int i = 0; i <= HotColumnIndex; i++)
                    {
                        RightColumnEdge += Columns[i].Width;
                    }

                    RightColumnEdge--;

                    int ScrolledMouseX = e.X + HScroll.Value;

                    int WidthDelta = ScrolledMouseX - RightColumnEdge;
                    var ResizingColumn = Columns[HotColumnIndex];
                    int NewColumnWidth = ResizingColumn.Width + WidthDelta;

                    if (NewColumnWidth >= ResizeGripWidth)
                    {
                        ResizingColumn.Width = NewColumnWidth;
                    }
                }
                else
                {
                    if (AllowMultipleSelectedItems && !ItemSelectionStart.IsEmpty)
                    {
                        var SelectionSize = ItemSelectionStart.Distance(e.X + ContentRectangle.X + HScroll.Value, e.Y + ContentRectangle.Y + VScroll.Value);

                        if (SelectionSize.Width >= SystemInformation.DragSize.Width && SelectionSize.Height >= SystemInformation.DragSize.Height)
                        {
                            if (SelectedItemIndices.Count > 0) SelectedItemIndices.Clear();
                            if (HotItemIndex >= 0) HotItemIndex = -1;
                        }

                        UpdateSelectionRectangle(e.X, e.Y);
                    }
                }
            }

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

                // TODO: Remove
                //Console.WriteLine("Old: {0} New: {1}", VScroll.Value, NewScrollValue);
            }

            base.OnMouseWheel(e);
        }

        #endregion

        #region Collection changes

        protected void ItemCollectionChanged(object sender, NotifyingCollectionChangedEventArgs e)
        {
            Invalidate();
            CancelCellEdit();
        }

        protected void ItemChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            Invalidate();
        }

        protected void ColumnCollectionChanged(object sender, NotifyingCollectionChangedEventArgs e)
        {
            // TODO: Test if this stuff actually works.
            switch (e.Action)
            {
                case NotifyingCollectionChangeAction.Add:
                    var AddedColumn = (Column)e.NewItem;

                    if (AddedColumn.CellEditor != null && AddedColumn.CellEditor.EditorControl != null)
                    {
                        Controls.Add(AddedColumn.CellEditor.EditorControl);
                    }

                    break;
                case NotifyingCollectionChangeAction.Remove:
                    var RemovedColumn = (Column)e.OldItem;

                    if (RemovedColumn.CellEditor != null && RemovedColumn.CellEditor.EditorControl != null)
                    {
                        Controls.Remove(RemovedColumn.CellEditor.EditorControl);
                    }

                    break;
                case NotifyingCollectionChangeAction.Replace:
                    var OldColumn = (Column)e.OldItem;
                    var NewColumn = (Column)e.NewItem;

                    if (OldColumn.CellEditor != null && OldColumn.CellEditor.EditorControl != null)
                    {
                        Controls.Remove(OldColumn.CellEditor.EditorControl);
                    }

                    if (NewColumn.CellEditor != null && NewColumn.CellEditor.EditorControl != null)
                    {
                        Controls.Add(NewColumn.CellEditor.EditorControl);
                    }

                    break;
                case NotifyingCollectionChangeAction.Clear:
                    var ClearedCollection = (NotifyingCollection<Column>)sender;

                    foreach (var column in ClearedCollection)
                    {
                        if (column.CellEditor != null && column.CellEditor.EditorControl != null)
                        {
                            Controls.Remove(column.CellEditor.EditorControl);
                        }
                    }

                    break;
            }

            Invalidate();
            CancelCellEdit();
        }

        protected void ColumnChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Column.CellEditor))
            {
                var Msg = "The {0} property of a column object may not be changed once it is added to a {1} instance.";
                throw new InvalidOperationException(string.Format(Msg, nameof(Column.CellEditor), nameof(DList)));
            }

            Invalidate();
        }

        protected void SelectedItemIndicesChanged(object sender, NotifyingCollectionChangedEventArgs e)
        {
            if (AllowMultipleSelectedItems) Invalidate();
        }
        
        #endregion

        #region Scrolling

        protected void ScrollValueChanged(object sender, EventArgs e)
        {
            Invalidate();
            CancelCellEdit();

            // TODO: Remove
            //ScrollBar SBar = (ScrollBar)sender;
            //Console.WriteLine("{0}, LC: {1}", SBar, SBar.LargeChange);
        }

        protected void UpdateScrollBars()
        {
            if (Columns.Count == 0) return; // This might be bad, but without it the VS form designer goes crazy.

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
            HScroll.LargeChange = Math.Max(0, ContentRectangle.Width);
            HScroll.SmallChange = HScroll.LargeChange / 10;
        }
        
        public void ScrollPageUp()
        {
            VScroll.Value = Math.Max(0, VScroll.Value - VScroll.LargeChange);
        }

        public void ScrollPageDown()
        {
            VScroll.Value = Math.Min(VScroll.Maximum - VScroll.LargeChange + 1, VScroll.Value + VScroll.LargeChange);
        }

        public void ScrollToBegin()
        {
            VScroll.Value = VScroll.Minimum;
        }

        public void ScrollToEnd()
        {
            VScroll.Value = VScroll.Maximum - VScroll.LargeChange + 1;
        }

        #endregion

        #region Misc

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // TODO: Remove
            //Console.WriteLine("SetBoundsCoreStart: x{0}, y{1}, w{2}, h{3}", x, y, width, height);

            // This prevents the control from being resized when minimized, which would mess up the integral height calculation.
            if (specified == BoundsSpecified.None && height == 0 && width == 0) return;

            if (IntegralHeight && height > MinimumSize.Height)
            {
                int ContentHeight = (HScroll.Visible) ? ContentRectangle.Height + HScroll.Height + 2 : ContentRectangle.Height;
                // SetBoundsCore() can and will get called before OnPaint() which means the ContentRectangle is empy at that time.
                int BorderHeight = (ContentRectangle.Height > 0) ? ClientRectangle.Height - ContentHeight : 0;

                height = (ItemHeight * (int)Math.Round(height / (double)ItemHeight)) + BorderHeight;

                if (HScroll.Visible)
                {
                    height += HScroll.Height + 2;
                }
            }

            // TODO: remove
            //Console.WriteLine("SetBoundsCoreEnd: x{0}, y{1}, w{2}, h{3} - Crect: {4}", x, y, width, height, ContentRectangle);

            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (AllowMultipleSelectedItems)
            {
                EndSelectionRectangle();
            }

            base.OnLostFocus(e);
        }

        private void CellEditorDone(object sender, CellEditorDoneEventArgs e)
        {
            if (e.Success)
            {
                var Column = Columns[e.ColumnIndex];
                var Item = Items[e.ItemIndex];
                Column.SetValue(Item, e.NewValue);
            }

            var Editor = (ICellEditor)sender;
            Editor.Done -= CellEditorDone;

            Focus();

            // TODO: Remove
            Console.WriteLine("EditDone - success: {0}, value: {1}", e.Success, e.NewValue);
        }

        #endregion

        public int GetItemIndexAt(int x, int y)
        {
            if (x < ContentRectangle.X || x >= ContentRectangle.Right) return -1;
            if (y < ContentRectangle.Y + ItemHeight || y >= ContentRectangle.Bottom) return -1;
            if (Items.Count <= 0) return -1;
            if (Columns.Count <= 0) return -1;

            int LastColumnRight = ContentRectangle.X;

            foreach (var column in Columns)
            {
                LastColumnRight += column.Width;
            }

            if (x >= LastColumnRight) return -1;

            int ItemIndex = (int)Math.Floor((y - ContentRectangle.Y + VScroll.Value - ItemHeight) / (double)ItemHeight);

            if (ItemIndex >= Items.Count)
            {
                ItemIndex = -1;
            }

            ItemIndex = Math.Max(-1, ItemIndex);

            return ItemIndex;
        }

        public int GetColumnIndexAt(int x)
        {
            return GetColumnHeaderIndexAt(x, ContentRectangle.Y);
        }

        public int GetColumnHeaderIndexAt(int x, int y)
        {
            if (x < ContentRectangle.X || x >= ContentRectangle.Right) return -1;
            if ((y < ContentRectangle.Y) || (y >= ContentRectangle.Y + ItemHeight)) return -1;

            int ScrolledX = x + HScroll.Value;
            int Right = ContentRectangle.X;
            int Index = 0;

            foreach (var column in Columns)
            {
                Right += column.Width;

                if (ScrolledX < Right) return Index;

                Index++;
            }

            return -1;
        }

        public int GetFirstVisibleItemIndex()
        {
            if (Items.Count == 0) return -1;

            return (!VScroll.Visible) ? 0 : VScroll.Value / ItemHeight;
        }

        public int GetLastVisibleItemIndex()
        {
            if (Items.Count == 0) return -1;

            return (!VScroll.Visible) ? Items.Count - 1 : (int)Math.Ceiling((VScroll.Value + VScroll.LargeChange) / (double)ItemHeight) - 1;
        }

        public Rectangle GetCellBounds(int columnIndex, int itemIndex)
        {
            if (columnIndex < 0) throw new ArgumentException("Index is less than zero.", nameof(columnIndex));
            if (columnIndex > Columns.Count - 1) throw new ArgumentOutOfRangeException(nameof(columnIndex));
            if (itemIndex < 0) throw new ArgumentException("Index is less than zero.", nameof(itemIndex));
            if (itemIndex > Items.Count - 1) throw new ArgumentOutOfRangeException(nameof(itemIndex));

            if (Items.Count <= 0) return Rectangle.Empty;

            var Bounds = new Rectangle(ContentRectangle.X, ContentRectangle.Y + ItemHeight, Columns[columnIndex].Width, ItemHeight);

            for (int i = 0; i < columnIndex; i++)
            {
                Bounds.X += Columns[i].Width;
            }

            for (int i = 0; i < itemIndex; i++)
            {
                Bounds.Y += ItemHeight;
            }

            Bounds.X -= HScroll.Value;
            Bounds.Y -= VScroll.Value;

            return Bounds;
        }

        public void EnsureItemVisibility(int itemIndex)
        {
            if (Items.Count == 0 || itemIndex < 0) return;

            int ItemTop = itemIndex * ItemHeight;
            int ItemBottom = ItemTop + ItemHeight - 1;

            int VisibleTop = VScroll.Value;
            int VisibleBottom = VScroll.Value + VScroll.LargeChange - 1;

            // TODO: Remove
            //Console.WriteLine("Ensure: ITop {0}, IBot {1}, VScroll {2}", ItemTop, ItemBottom, VScroll.Value);
            //Console.WriteLine("Ensure: VTop {0}, VBot {1}", VisibleTop, VisibleBottom);

            int ScrollOffset = (ItemTop < VisibleTop)
                ? ItemTop - VisibleTop
                : (ItemBottom > VisibleBottom)
                ? ItemBottom - VisibleBottom
                : 0;
            
            // TODO: Remove
            //Console.WriteLine("Ensure: SOffset {0}", ScrollOffset);

            VScroll.Value += ScrollOffset;
        }

        public void SelectAllItems()
        {
            if (AllowMultipleSelectedItems)
            {
                SelectedItemIndices.Clear();

                for (int i = 0; i < Items.Count; i++)
                {
                    SelectedItemIndices.Add(i);
                }
            }
        }

        public void BeginCellEdit(int columnIndex, int itemIndex)
        {
            if (Columns.Count == 0) throw new InvalidOperationException(string.Format("{0} has no columns.", nameof(DList)));
            if (Items.Count == 0) throw new InvalidOperationException(string.Format("{0} has no items.", nameof(DList)));
            if (columnIndex < 0 || columnIndex >= Columns.Count) throw new ArgumentOutOfRangeException(nameof(columnIndex));
            if (itemIndex < 0 || itemIndex >= Items.Count) throw new ArgumentOutOfRangeException(nameof(itemIndex));

            var Column = Columns[columnIndex];

            if (Column.CanEdit)
            {
                object Value = Column.GetValue(Items[FocusedItemIndex]);
                var Editor = Column.CellEditor;
                Editor.Done += CellEditorDone;

                var CellBounds = GetCellBounds(columnIndex, FocusedItemIndex);
                Editor.Edit(CellBounds, columnIndex, FocusedItemIndex, Value);

                _ActiveCellEditor = Column.CellEditor;

                // TODO: Remove
                Console.WriteLine("CellBounds: {0}", CellBounds);
            }
        }

        public void CancelCellEdit()
        {
            if (_ActiveCellEditor != null)
            {
                _ActiveCellEditor.Cancel();
            }
        }

        private void StartSelectionRectangle(int x, int y)
        {
            if (ModifierKeys.HasFlag(Keys.Shift) || ModifierKeys.HasFlag(Keys.Control))
            {
                _AddToSelection = true;
            }

            ItemSelectionStart = new Point(x + ContentRectangle.X + HScroll.Value, y + ContentRectangle.Y + VScroll.Value);
            // TODO: Remove
            //Console.WriteLine("Select START - {0}", ItemSelectionStart);
        }

        private void UpdateSelectionRectangle(int x, int y)
        {
            // Update the selection rectangle
            int ScrolledX = x + ContentRectangle.X + HScroll.Value;
            int ScrolledY = y + ContentRectangle.Y + VScroll.Value;

            if ((Math.Abs(ItemSelectionStart.X - ScrolledX) + Math.Abs(ItemSelectionStart.Y - ScrolledY)) >= 3)
            {
                ItemSelectionEnd = new Point(ScrolledX, ScrolledY);
            }

            // Autoscroll up or down if the selection rectangle is dragged above or beneath the item list.
            if (y >= ContentRectangle.Bottom || y < (ContentRectangle.Y + ItemHeight))
            {
                if (ItemSelectionEnd.Y > ItemSelectionStart.Y)
                {
                    int MaxVScroll = VScroll.Maximum - VScroll.LargeChange + 1;

                    if (VScroll.Value < MaxVScroll)
                    {
                        VScroll.Value += Math.Min(MaxVScroll - VScroll.Value, VScroll.SmallChange);
                    }
                }
                else
                {
                    if (VScroll.Value > VScroll.Minimum)
                    {
                        VScroll.Value -= Math.Min(VScroll.Value, VScroll.SmallChange);
                    }
                }
            }

            // TODO: Remove
            //Console.WriteLine("Select UPDATE - {0} -> {1}", ItemSelectionStart, ItemSelectionEnd);
        }

        private void EndSelectionRectangle()
        {
            if (Columns.Count > 0
                && Items.Count > 0
                && !ItemSelectionStart.IsEmpty
                && !ItemSelectionEnd.IsEmpty)
            {
                // Finish selection rectangle handling

                if (!_AddToSelection) SelectedItemIndices.Clear();

                // TODO: Remove
                //Console.WriteLine("Select END - {0} -> {1}", ItemSelectionStart, ItemSelectionEnd);

                var Selection = Utils.GetRectangleFromPoints(ItemSelectionStart, ItemSelectionEnd);

                int ItemsStartY = ContentRectangle.Y + ItemHeight;
                int ItemsEndY = ItemsStartY + Items.Count * ItemHeight - 1;
                bool VerticalOverlap = (Selection.Y <= ItemsEndY);
                bool HorizontalOverlap = false;

                if (VerticalOverlap)
                {
                    // Check if the selection rectangle does horizontally overlap with any items
                    int RightEdge = ContentRectangle.X;

                    foreach (var column in Columns)
                    {
                        RightEdge += column.Width;

                        if (Selection.X < RightEdge)
                        {
                            HorizontalOverlap = true;
                            break;
                        }
                    }
                }

                if (HorizontalOverlap && VerticalOverlap)
                {
                    int FirstItemIndex = Math.Max(0, (int)Math.Floor((Selection.Y - ItemHeight) / (double)ItemHeight));
                    int LastItemIndex = Math.Min(Items.Count - 1, (int)Math.Floor((Selection.Bottom - 1 - ItemHeight) / (double)ItemHeight));

                    for (int i = FirstItemIndex; i <= LastItemIndex; i++)
                    {
                        if (_AddToSelection)
                        {
                            if (!SelectedItemIndices.Contains(i))
                            {
                                SelectedItemIndices.Add(i);
                            }
                        }
                        else
                        {
                            SelectedItemIndices.Add(i);
                        }
                    }

                    // Focus the last item in the direction the selection rectangle was drawn
                    FocusedItemIndex = (ItemSelectionStart.Y < ItemSelectionEnd.Y) ? LastItemIndex : FirstItemIndex;

                    // TODO: remove
                    //Console.WriteLine("Select INDICES - {0} -> {1}", FirstItemIndex, LastItemIndex);
                }
            }

            // Clean up selection rectangle data
            ItemSelectionStart = Point.Empty;
            ItemSelectionEnd = Point.Empty;
            _AddToSelection = false;
        }
    }
}
