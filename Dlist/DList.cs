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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
 * things worse is that this is not true for every method. Graphics.FillRectangle() will 
 * draw a 10 pixel wide and 10 pixel high filled rectangle when called with the same parameters. 
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
        private bool _IntegralHeight = false;
        private bool _AllowMultipleSelectedItems = true;
        private int _ItemHeight = 28;
        private int _ResizeGripWidth = 10;
        private int _FocusedItemIndex = -1;
        private int _SelectedItemIndex = -1;
        private int _HotItemIndex = -1;
        private int _HotHeaderIndex = -1;
        private int _PressedHeaderIndex = -1;
        private int _PressedItemIndex = -1;
        private int _SelectionRectangleAlpha = 128;
        private int _ReorderColumnIndex = -1;
        
        private Color _ItemForeColor = SystemColors.ControlText;
        private Color _ItemForeColorHot = SystemColors.HighlightText;
        private Color _ItemForeColorSelected = SystemColors.HighlightText;
        private Color _ItemBackColor = SystemColors.ControlLightLight;
        private Color _ItemBackColorHot = SystemColors.HotTrack;
        private Color _ItemBackColorSelected = SystemColors.Highlight;

        private Color _HeaderForeColor = SystemColors.ControlText;
        private Color _HeaderForeColorHot = SystemColors.InactiveCaptionText;
        private Color _HeaderForeColorPressed = SystemColors.ActiveCaptionText;
        private Color _HeaderBackColor = SystemColors.ControlLight;
        private Color _HeaderBackColorHot = SystemColors.InactiveCaption;
        private Color _HeaderBackColorPressed = SystemColors.ActiveCaption;
        private Color _HeaderSeparatorColor = SystemColors.ControlDark;
        private Color _HeaderReorderIndicatorColor = SystemColors.HotTrack;

        private Color _GridColor = SystemColors.ControlDark;
        private Color _SelectionRectangleColor = SystemColors.HotTrack;
        private Color _SelectionRectangleBorderColor = SystemColors.HotTrack;
        private Color _BorderColor = SystemColors.ControlDark;
        private Font _HeaderFont;
        private IHeaderRenderer _HeaderRenderer = new HeaderRenderer();
        private Func<object, (Color, Color)> _ItemColorEvaluator;
        private Pen _GridPen;
        private Pen _SelectionRectanglePen;
        private Pen _BorderPen;
        private SolidBrush _SelectionRectangleBrush;
        private SolidBrush _BackgroundBrush;
        private Rectangle _ContentRectangle;

        private bool IsFirstPaint = true;
        private bool DoColumnResizeOnLeftMouseDown = false;
        private bool IsSelectedItemChangedEventActive = true;
        private int LastClickTimestamp = 0;
        private int LastClickedHeaderIndex = -1;
        private Point ItemSelectionStart = Point.Empty;
        private Point ItemSelectionEnd = Point.Empty;
        private ICellEditor ActiveCellEditor;

        #region Properties
        
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
        public bool AllowColumnReordering { get; set; } = true;

        [Browsable(false)]
        public override Color ForeColor { get; set; }

        [DefaultValue(typeof(Color), "Control")]
        [Category("Appearance")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor != value)
                {
                    base.BackColor = value;

                    _BackgroundBrush?.Dispose();
                    _BackgroundBrush = new SolidBrush(value);

                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "ControlDark")]
        [Category("Appearance")]
        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                if (_BorderColor != value)
                {
                    _BorderColor = value;
                    
                    _BorderPen?.Dispose();
                    _BorderPen = new Pen(value);

                    Invalidate();
                }
            }
        }
        
        [DefaultValue(typeof(Color), "ControlText")]
        [Category("Appearance")]
        public Color ItemForeColor
        {
            get => _ItemForeColor;
            set
            {
                if (_ItemForeColor != value)
                {
                    _ItemForeColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "HighlightText")]
        [Category("Appearance")]
        public Color ItemForeColorHot
        {
            get => _ItemForeColorHot;
            set
            {
                if (_ItemForeColorHot != value)
                {
                    _ItemForeColorHot = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "HighlightText")]
        [Category("Appearance")]
        public Color ItemForeColorSelected
        {
            get => _ItemForeColorSelected;
            set
            {
                if (_ItemForeColorSelected != value)
                {
                    _ItemForeColorSelected = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "ControlLightLight")]
        [Category("Appearance")]
        public Color ItemBackColor
        {
            get => _ItemBackColor;
            set
            {
                if (_ItemBackColor != value)
                {
                    _ItemBackColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "HotTrack")]
        [Category("Appearance")]
        public Color ItemBackColorHot
        {
            get => _ItemBackColorHot;
            set
            {
                if (_ItemBackColorHot != value)
                {
                    _ItemBackColorHot = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "Highlight")]
        [Category("Appearance")]
        public Color ItemBackColorSelected
        {
            get => _ItemBackColorSelected;
            set
            {
                if (_ItemBackColorSelected != value)
                {
                    _ItemBackColorSelected = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "ControlText")]
        [Category("Appearance")]
        public Color HeaderForeColor
        {
            get => _HeaderForeColor;
            set
            {
                if (_HeaderForeColor != value)
                {
                    _HeaderForeColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "InactiveCaptionText")]
        [Category("Appearance")]
        public Color HeaderForeColorHot
        {
            get => _HeaderForeColorHot;
            set
            {
                if (_HeaderForeColorHot != value)
                {
                    _HeaderForeColorHot = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "ActiveCaptionText")]
        [Category("Appearance")]
        public Color HeaderForeColorPressed
        {
            get => _HeaderForeColorPressed;
            set
            {
                if (_HeaderForeColorPressed != value)
                {
                    _HeaderForeColorPressed = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "ControlLight")]
        [Category("Appearance")]
        public Color HeaderBackColor
        {
            get => _HeaderBackColor;
            set
            {
                if (_HeaderBackColor != value)
                {
                    _HeaderBackColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "InactiveCaption")]
        [Category("Appearance")]
        public Color HeaderBackColorHot
        {
            get => _HeaderBackColorHot;
            set
            {
                if (_HeaderBackColorHot != value)
                {
                    _HeaderBackColorHot = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "ActiveCaption")]
        [Category("Appearance")]
        public Color HeaderBackColorPressed
        {
            get => _HeaderBackColorPressed;
            set
            {
                if (_HeaderBackColorPressed != value)
                {
                    _HeaderBackColorPressed = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "ControlDark")]
        [Category("Appearance")]
        public Color HeaderSeparatorColor
        {
            get => _HeaderSeparatorColor;
            set
            {
                if (_HeaderSeparatorColor != value)
                {
                    _HeaderSeparatorColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "HotTrack")]
        [Category("Appearance")]
        public Color HeaderReorderIndicatorColor
        {
            get => _HeaderReorderIndicatorColor;
            set
            {
                if (_HeaderReorderIndicatorColor != value)
                {
                    _HeaderReorderIndicatorColor = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "ControlDark")]
        [Category("Appearance")]
        public Color GridColor
        {
            get => _GridColor;
            set
            {
                if (_GridColor != value)
                {
                    _GridColor = value;

                    _GridPen?.Dispose();
                    _GridPen = new Pen(value);

                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "HotTrack")]
        [Category("Appearance")]
        public Color SelectionRectangleColor
        {
            get => _SelectionRectangleColor;
            set
            {
                if (_SelectionRectangleColor != value)
                {
                    _SelectionRectangleColor = value;

                    _SelectionRectangleBrush?.Dispose();
                    _SelectionRectangleBrush = new SolidBrush(Color.FromArgb(_SelectionRectangleAlpha, value));

                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "HotTrack")]
        [Category("Appearance")]
        public Color SelectionRectangleBorderColor
        {
            get => _SelectionRectangleBorderColor;
            set
            {
                if (_SelectionRectangleBorderColor != value)
                {
                    _SelectionRectangleBorderColor = value;

                    _SelectionRectanglePen?.Dispose();
                    _SelectionRectanglePen = new Pen(value);

                    Invalidate();
                }
            }
        }

        [DefaultValue(null)]
        [Category("Appearance")]
        public Font HeaderFont
        {
            get => _HeaderFont;
            set
            {
                if (_HeaderFont != value)
                {
                    _HeaderFont = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(128)]
        [Category("Appearance")]
        public int SelectionRectangleAlpha
        {
            get => _SelectionRectangleAlpha;
            set
            {
                if (_SelectionRectangleAlpha != value)
                {
                    _SelectionRectangleAlpha = value;

                    _SelectionRectangleBrush?.Dispose();
                    _SelectionRectangleBrush = new SolidBrush(Color.FromArgb(value, _SelectionRectangleColor));
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
        public bool AllowColumnResize { get; set; } = true;

        [DefaultValue(true)]
        [Category("Layout")]
        public bool AllowColumnAutoResize { get; set; } = true;

        [DefaultValue(AutoResizeMode.OnlyVisibleItems)]
        [Category("Layout")]
        public AutoResizeMode ColumnAutoResizeMode { get; set; } = AutoResizeMode.OnlyVisibleItems;

        [DefaultValue(false)]
        [Category("Layout")]
        public bool IntegralHeight
        {
            get => _IntegralHeight;
            set
            {
                if ((_IntegralHeight != value) && (Dock == DockStyle.None))
                {
                    _IntegralHeight = value;
                    if (value) Height = CalculateIntegralHeight(Height);
                }
            }
        }

        [DefaultValue(true)]
        [Category("Behavior")]
        public bool AllowMultipleSelectedItems
        {
            get => this._AllowMultipleSelectedItems;
            set
            {
                if (!value) SelectedItemIndices.Clear();
                this._AllowMultipleSelectedItems = value;
            }
        }

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NotifyingCollection<Column> Columns { get; } = new NotifyingCollection<Column>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ContentRectangle
        {
            get => _ContentRectangle;
        }

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
                    OnSelectedItemsChanged(EventArgs.Empty);
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
        public int HotHeaderIndex
        {
            get => _HotHeaderIndex;
            private set
            {
                if (_HotHeaderIndex != value)
                {
                    _HotHeaderIndex = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PressedHeaderIndex
        {
            get => _PressedHeaderIndex;
            private set
            {
                if (_PressedHeaderIndex != value)
                {
                    _PressedHeaderIndex = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IHeaderRenderer HeaderRenderer
        {
            get => _HeaderRenderer;
            set
            {
                if (_HeaderRenderer != value)
                {
                    value = _HeaderRenderer;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<object, (Color, Color)> ItemColorEvaluator
        {
            get => _ItemColorEvaluator;
            set
            {
                if (_ItemColorEvaluator != value)
                {
                    _ItemColorEvaluator = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NotifyingCollection<object> Items { get; } = new NotifyingCollection<object>();

        public override DockStyle Dock
        {
            get => base.Dock;
            set
            {
                base.Dock = value;

                if (value != DockStyle.None)
                {
                    _IntegralHeight = false;
                }
            }
        }
        protected HScrollBar HScroll { get; }
        protected VScrollBar VScroll { get; }

        protected Pen GridPen { get => _GridPen; }
        protected Pen SelectionRectanglePen { get => _SelectionRectanglePen; }
        protected Pen BorderPen { get => _BorderPen; }

        protected SolidBrush SelectionRectangleBrush { get => _SelectionRectangleBrush; }
        protected SolidBrush BackgroundBrush { get => _BackgroundBrush; }

        protected override Size DefaultMinimumSize
        {
            get => new Size(4 * ItemHeight, 2 * ItemHeight);
        }

        private int ReorderColumnIndex
        {
            get { return _ReorderColumnIndex; }
            set
            {
                if (_ReorderColumnIndex != value)
                {
                    _ReorderColumnIndex = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region Events

        [Category("Action")]
        public event EventHandler<HeaderClickEventArgs> HeaderClicked;

        [Category("Action")]
        public event EventHandler<HeaderClickEventArgs> HeaderDoubleClicked;

        [Category("Action")]
        public event EventHandler<CellClickEventArgs> CellClicked;

        [Category("Behavior")]
        public event EventHandler SelectedItemsChanged;

        #endregion

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
            Columns.CollectionChanging += ColumnCollectionChanging;
            Columns.ItemChanged += ColumnChanged;

            SelectedItemIndices.CollectionChanged += SelectedItemIndicesChanged;

            // Create initial pens and brushes
            _GridPen = new Pen(_GridColor);
            _SelectionRectanglePen = new Pen(_SelectionRectangleBorderColor);
            _BorderPen = new Pen(_BorderColor);
            _SelectionRectangleBrush = new SolidBrush(Color.FromArgb(_SelectionRectangleAlpha, _SelectionRectangleColor));
            _BackgroundBrush = new SolidBrush(BackColor);
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
            _ContentRectangle = DrawBackground(Gfx);

            UpdateScrollBars();
            Gfx.SetClip(_ContentRectangle);

            if (Items.Count > 0)
            {
                int FirstVisibleItemIndex = GetFirstVisibleItemIndex();
                DrawItems(Gfx, FirstVisibleItemIndex);

                if (FocusedItemIndex >= 0) DrawFocusRectangle(Gfx);
                if (!ItemSelectionStart.IsEmpty && !ItemSelectionEnd.IsEmpty) DrawSelectionRectangle(Gfx, ItemSelectionStart, ItemSelectionEnd);
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
            if (IsFirstPaint)
            {
                IsFirstPaint = false;
                SetBoundsCore(Left, Top, Width, Height, BoundsSpecified.All);
            }
        }

        protected virtual Rectangle DrawBackground(Graphics gfx)
        {
            gfx.FillRectangle(BackgroundBrush, ClientRectangle);
            gfx.DrawRectangle(BorderPen, ClientRectangle.ToGDI()); // @GDI

            return new Rectangle(ClientRectangle.X + 1, ClientRectangle.Y + 1, ClientRectangle.Width - 2, ClientRectangle.Height - 2);
        }

        protected virtual void DrawHeaders(Graphics gfx)
        {
            var Bounds = new Rectangle(_ContentRectangle.Left - HScroll.Value, _ContentRectangle.Top, 0, ItemHeight);
            int Index = 0;
            RenderState State;

            foreach (var column in Columns)
            {
                if (Bounds.X >= _ContentRectangle.Right) break;

                Bounds.Width = column.Width;
                State = (Index == HotHeaderIndex) ? RenderState.Hot : (Index == PressedHeaderIndex) ? RenderState.Pressed : RenderState.Normal;

                if ((ReorderColumnIndex == Index) && (ReorderColumnIndex != PressedHeaderIndex))
                {
                    State |= RenderState.Focused;
                }

                var HeaderFont = column.HeaderFont ?? this.HeaderFont ?? Font;
                var ForegroundColor = Color.Empty;
                var BackgroundColor = Color.Empty;

                if (State.HasFlag(RenderState.Hot))
                {
                    ForegroundColor = HeaderForeColorHot;
                    BackgroundColor = HeaderBackColorHot;
                }
                else if (State.HasFlag(RenderState.Pressed))
                {
                    ForegroundColor = HeaderForeColorPressed;
                    BackgroundColor = HeaderBackColorPressed;
                }
                else
                {
                    ForegroundColor = HeaderForeColor;
                    BackgroundColor = HeaderBackColor;
                }

                var HeaderBounds = (!ShowGrid) ? Bounds : new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height - 1);

                HeaderRenderer.Draw(gfx, HeaderBounds, State, column.Name, ForegroundColor, BackgroundColor, HeaderSeparatorColor, HeaderReorderIndicatorColor, HeaderFont);

                Bounds.X += column.Width;
                Index++;
            }
        }

        protected virtual void DrawItems(Graphics gfx, int firstItemIndex)
        {
            var Bounds = new Rectangle(_ContentRectangle.Left - HScroll.Value, _ContentRectangle.Top + ItemHeight, 0, ItemHeight);

            foreach (var column in Columns)
            {
                if (Bounds.X > _ContentRectangle.Right) break;

                Bounds.Width = column.Width;
                Bounds.Y -= (VScroll.Value - firstItemIndex * ItemHeight);

                for (int x = firstItemIndex; x < Items.Count; x++)
                {
                    if (Bounds.Y > _ContentRectangle.Bottom) break;

                    var Item = Items[x];
                    var CellValue = column.GetValue(Item);
                    var State = (HotItemIndex == x)
                        ? RenderState.Hot
                        : ((AllowMultipleSelectedItems && SelectedItemIndices.Contains(x)) || SelectedItemIndex == x)
                        ? RenderState.Selected
                        : RenderState.Normal;

                    if (FocusedItemIndex == x)
                    {
                        State |= RenderState.Focused;
                    }

                    // SPEED: The ItemColorEvaluator is invoked for every cell if there is no CellColorEvaluator. Since drawing 
                    // is done column by column instead of item by item, the only way to get around this would be to cache the 
                    // ItemColorEvaluator results. This is not worth it unless the ItemColorEvaluator is very complex, which it
                    // should not be to begin with.
                    (Color ForegroundColor, Color BackgroundColor) = GetCellColors(column, Item, CellValue);

                    ForegroundColor = (State.HasFlag(RenderState.Hot))
                        ? ItemForeColorHot
                        : (State.HasFlag(RenderState.Selected)) ? ItemForeColorSelected
                        : (ForegroundColor.IsEmpty) ? ItemForeColor : ForegroundColor;

                    BackgroundColor = (State.HasFlag(RenderState.Hot))
                        ? ItemBackColorHot
                        : (State.HasFlag(RenderState.Selected)) ? ItemBackColorSelected
                        : (BackgroundColor.IsEmpty) ? ItemBackColor : BackgroundColor;

                    var ItemFont = column.ItemFont ?? Font;

                    // ToGDI() is abused here to shrink the cells bounds if the grid is shown. Since the grid will always 
                    // occupy the rightmost and bottommost pixels of the cell, ToGDI() gets us exactly what we need.
                    column.CellRenderer.Draw(gfx, (!ShowGrid) ? Bounds : Bounds.ToGDI(), State, CellValue, ForegroundColor, BackgroundColor, ItemFont);

                    Bounds.Y += ItemHeight;
                }

                Bounds.X += column.Width;
                Bounds.Y = _ContentRectangle.Top + ItemHeight;
            }
        }

        protected virtual void DrawGrid(Graphics gfx)
        {
            var X = _ContentRectangle.X - 1 - HScroll.Value;
            var Y1 = _ContentRectangle.Y + ItemHeight;
            var Y2 = Math.Min(_ContentRectangle.Bottom - 1, Y1 + Items.Count * ItemHeight - 1);

            var TotalColumnWidth = 0;

            if (Items.Count > 0)
            {
                // Vertical grid lines.
                foreach (var column in Columns)
                {
                    TotalColumnWidth += column.Width;
                    X += column.Width;
                    gfx.DrawLine(GridPen, X, Y1, X, Y2);
                }
            }
            else
            {
                foreach (var column in Columns)
                {
                    TotalColumnWidth += column.Width;
                }
            }


            // Horizontal grid lines.
            var X1 = _ContentRectangle.X;
            var X2 = X1 + TotalColumnWidth - 1;
            var Y = _ContentRectangle.Top + ItemHeight - 1;

            // Headers
            gfx.DrawLine(GridPen, X1, Y, X2, Y);
            Y += ItemHeight - VScroll.Value;

            // Items
            foreach (var item in Items)
            {
                if ((Y >= _ContentRectangle.Y + ItemHeight) && (Y < _ContentRectangle.Bottom))
                {
                    gfx.DrawLine(GridPen, X1, Y, X2, Y);
                }

                Y += ItemHeight;
            }
        }

        protected virtual void DrawFocusRectangle(Graphics gfx)
        {
            var ItemPos = new Point(_ContentRectangle.X, _ContentRectangle.Y - VScroll.Value + (FocusedItemIndex + 1) * ItemHeight);
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

        protected virtual void DrawSelectionRectangle(Graphics gfx, Point start, Point end)
        {
            var SelectionRectangle = Utils.GetRectangleFromPoints(start, end);
            SelectionRectangle.Offset(-HScroll.Value, -VScroll.Value);

            gfx.FillRectangle(SelectionRectangleBrush, SelectionRectangle);
            gfx.DrawRectangle(SelectionRectanglePen, SelectionRectangle.ToGDI());
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
                    e.Handled = HandleKeyArrowUp(e.Modifiers);
                    break;

                case Keys.Down:
                    e.Handled = HandleKeyArrowDown(e.Modifiers);
                    break;

                case Keys.Left:
                    e.Handled = HandleKeyArrowLeft(e.Modifiers);
                    break;

                case Keys.Right:
                    e.Handled = HandleKeyArrowRight(e.Modifiers);
                    break;

                case Keys.PageUp:
                    e.Handled = HandleKeyPageUp(e.Modifiers);
                    break;

                case Keys.PageDown:
                    e.Handled = HandleKeyPageDown(e.Modifiers);
                    break;

                case Keys.Home:
                    e.Handled = HandleKeyHome(e.Modifiers);
                    break;

                case Keys.End:
                    e.Handled = HandleKeyEnd(e.Modifiers);
                    break;

                case Keys.Space:
                    e.Handled = HandleKeySpace(e.Modifiers);
                    break;

                case Keys.Delete:
                    e.Handled = HandleKeyDelete(e.Modifiers);
                    break;

                case Keys.A:
                    e.Handled = HandleKeyA(e.Modifiers);
                    break;
#if DEBUG

                case Keys.F12:
                    e.Handled = HandleKeyDebugF12(e.Modifiers);
                    break;
#endif
            }

            base.OnKeyDown(e);
        }

#if DEBUG
        private bool HandleKeyDebugF12(Keys modifiers)
        {
            int TotalColumnWidth = 0;

            foreach (var column in Columns)
            {
                TotalColumnWidth += column.Width;
            }

            var HeadersArea = new Rectangle(_ContentRectangle.X, _ContentRectangle.Y, TotalColumnWidth, ItemHeight);
            var ItemArea = new Rectangle(_ContentRectangle.X, HeadersArea.Bottom, TotalColumnWidth, Items.Count * ItemHeight);
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("| Right and bottom are the actual coordinates not the rectangle values.       |");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("|               Content: {0} -> Right:{1}, Bottom:{2}", _ContentRectangle, _ContentRectangle.Right - 1, _ContentRectangle.Bottom - 1);
            Console.WriteLine("|               Headers: {0} -> Right:{1}, Bottom:{2}", HeadersArea, HeadersArea.Right - 1, HeadersArea.Bottom - 1);
            Console.WriteLine("|                 Items: {0} -> Right:{1}, Bottom:{2}", ItemArea, ItemArea.Right - 1, ItemArea.Bottom - 1);
            Console.WriteLine("|     SelectedItemCount: {0}", SelectedItemIndices.Count);
            Console.WriteLine("|     SelectedItemIndex: {0}", SelectedItemIndex);
            Console.WriteLine("|      FocusedItemIndex: {0}", FocusedItemIndex);
            Console.WriteLine("|          HotItemIndex: {0}", HotItemIndex);
            Console.WriteLine("|        HotHeaderIndex: {0}", HotHeaderIndex);
            Console.WriteLine("|    PressedHeaderIndex: {0}", PressedHeaderIndex);
            Console.WriteLine("| FirstVisibleItemIndex: {0}", GetFirstVisibleItemIndex());
            Console.WriteLine("|  LastVisibleItemIndex: {0}", GetLastVisibleItemIndex());
            Console.Write("|       SelectedIndices: ");

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

            return true;
        }

#endif
        private bool HandleKeyA(Keys modifiers)
        {
            if (modifiers.HasFlag(Keys.Control))
            {
                if (modifiers.HasFlag(Keys.Shift))
                {
                    SelectedItemIndices.Clear();
                }
                else
                {
                    SelectAll();
                }

                return true;
            }

            return false;
        }

        private bool HandleKeyDelete(Keys modifiers)
        {
            RemoveSelectedItems();

            return true;
        }

        private bool HandleKeyEnd(Keys modifiers)
        {
            if (Items.Count > 0 && (_FocusedItemIndex != Items.Count - 1))
            {
                ScrollToEnd();

                if (modifiers.HasFlag(Keys.Shift))
                {
                    SelectRange(_FocusedItemIndex, Items.Count - 1, true);
                }
                else if (!modifiers.HasFlag(Keys.Control))
                {
                    Select(Items.Count - 1);
                }

                FocusedItemIndex = Items.Count - 1;
            }

            return true;
        }

        private bool HandleKeyHome(Keys modifiers)
        {
            if (Items.Count > 0 && (_FocusedItemIndex > 0))
            {
                ScrollToBegin();

                if (modifiers.HasFlag(Keys.Shift))
                {
                    SelectRange(_FocusedItemIndex, 0, true);
                }
                else if (!modifiers.HasFlag(Keys.Control))
                {
                    Select(0);
                }

                FocusedItemIndex = 0;
            }

            return true;
        }

        private bool HandleKeyPageDown(Keys modifiers)
        {
            int LastVisibleItemIndex = GetLastVisibleItemIndex();
            int OldFocusedItemIndex = _FocusedItemIndex;

            if (_FocusedItemIndex == LastVisibleItemIndex)
            {
                ScrollPageDown();
                LastVisibleItemIndex = GetLastVisibleItemIndex();
            }

            if (LastVisibleItemIndex == OldFocusedItemIndex) return true;   // End of the list reached.

            if (modifiers.HasFlag(Keys.Shift) && (OldFocusedItemIndex != -1))
            {
                SelectRange(OldFocusedItemIndex, LastVisibleItemIndex, true);
            }
            else if (!modifiers.HasFlag(Keys.Control))
            {
                Select(LastVisibleItemIndex);
            }

            FocusedItemIndex = LastVisibleItemIndex;

            return true;
        }

        private bool HandleKeyPageUp(Keys modifiers)
        {
            int FirstVisibleItemIndex = GetFirstVisibleItemIndex();
            int OldFocusedItemIndex = _FocusedItemIndex;

            if (_FocusedItemIndex == FirstVisibleItemIndex)
            {
                ScrollPageUp();
                FirstVisibleItemIndex = GetFirstVisibleItemIndex();
            }

            if (FirstVisibleItemIndex == OldFocusedItemIndex) return true;   // Start of the list reached.

            if (modifiers.HasFlag(Keys.Shift) && (OldFocusedItemIndex != -1))
            {
                SelectRange(OldFocusedItemIndex, FirstVisibleItemIndex, true);
            }
            else if (!modifiers.HasFlag(Keys.Control))
            {
                Select(FirstVisibleItemIndex);
            }

            FocusedItemIndex = FirstVisibleItemIndex;

            return true;
        }

        private bool HandleKeySpace(Keys modifiers)
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

            return true;
        }

        private bool HandleKeyArrowUp(Keys modifiers)
        {
            if (Items.Count == 0) return true;

            int OldFocusedItemIndex = FocusedItemIndex;

            if (FocusedItemIndex >= 1)
            {
                FocusedItemIndex -= 1;
            }
            else if (FocusedItemIndex < 0)
            {
                FocusedItemIndex = Items.Count - 1;
            }

            if (FocusedItemIndex != OldFocusedItemIndex)
            {
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
            }

            EnsureItemVisibility(FocusedItemIndex);

            return true;
        }

        private bool HandleKeyArrowDown(Keys modifiers)
        {
            if (Items.Count == 0) return true;

            int OldFocusedItemIndex = FocusedItemIndex;

            if (FocusedItemIndex < Items.Count - 1)
            {
                FocusedItemIndex += 1;
            }

            if (FocusedItemIndex != OldFocusedItemIndex)
            {
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
            }

            EnsureItemVisibility(FocusedItemIndex);

            return true;
        }

        private bool HandleKeyArrowLeft(Keys modifiers)
        {
            if (Items.Count == 0 || !HScroll.Visible) return true;

            if (HScroll.Value > HScroll.Minimum)
            {
                HScroll.Value -= (HScroll.SmallChange > HScroll.Value) ? HScroll.Value : HScroll.SmallChange;
            }

            return true;
        }

        private bool HandleKeyArrowRight(Keys modifiers)
        {
            if (Items.Count == 0 || !HScroll.Visible) return true;

            int MaxHScrollValue = HScroll.Maximum - HScroll.LargeChange;

            if (HScroll.Value < MaxHScrollValue)
            {
                int RemainingHScrollSpace = MaxHScrollValue - HScroll.Value;
                HScroll.Value += (HScroll.SmallChange > RemainingHScrollSpace) ? RemainingHScrollSpace : HScroll.SmallChange;
            }

            return true;
        }

        #endregion

        #region Mouse handling

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Focus();

                if (HotHeaderIndex >= 0)
                {
                    PressedHeaderIndex = HotHeaderIndex;
                    if (!DoColumnResizeOnLeftMouseDown) HotHeaderIndex = -1;
                }
                else
                {
                    if (AllowMultipleSelectedItems)
                    {
                        StartSelectionRectangle(e.X, e.Y);
                    }

                    if (HotItemIndex >= 0)
                    {
                        _PressedItemIndex = HotItemIndex;
                    }
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            // This is a necessary precaution for edit controls with strange focus behavior like DateTimePicker.
            if (ActiveCellEditor != null)
            {
                ActiveCellEditor.Cancel();
            }

            if (PressedHeaderIndex >= 0)
            {
                int CurrentHeaderIndex;

                // Handle column reordering.
                if (ReorderColumnIndex >= 0)    // Should only be >=0 if AllowColumnReorder is set to true.
                {
                    if (ReorderColumnIndex != PressedHeaderIndex)
                    {
                        var ReorderColumn = Columns[PressedHeaderIndex];
                        Columns.RemoveAt(PressedHeaderIndex);
                        Columns.Insert(ReorderColumnIndex, ReorderColumn);
                    }

                    CurrentHeaderIndex = ReorderColumnIndex;
                    ReorderColumnIndex = -1;
                }
                else
                {
                    CurrentHeaderIndex = GetColumnHeaderIndexAt(e.X, e.Y);
                }

                // Handle header clicking.
                if (CurrentHeaderIndex == PressedHeaderIndex)
                {
                    HotHeaderIndex = PressedHeaderIndex;

                    // The bitwise AND shifts the range of the tick count to positive only at the cost of half the range (24'ish days).
                    int TicksSinceLastClick = (Environment.TickCount & Int32.MaxValue) - LastClickTimestamp;

                    if ((LastClickedHeaderIndex >= 0)
                        && (LastClickedHeaderIndex == PressedHeaderIndex)
                        && (TicksSinceLastClick <= SystemInformation.DoubleClickTime))
                    {
                        LastClickedHeaderIndex = -1;
                        OnHeaderDoubleClicked(new HeaderClickEventArgs(PressedHeaderIndex));

                        if (AllowColumnAutoResize && DoColumnResizeOnLeftMouseDown) AutoResizeColumn(CurrentHeaderIndex);
                    }
                    else
                    {
                        OnHeaderClicked(new HeaderClickEventArgs(PressedHeaderIndex));
                        LastClickTimestamp = Environment.TickCount & Int32.MaxValue;
                        LastClickedHeaderIndex = PressedHeaderIndex;
                    }
                }
                else
                {
                    LastClickTimestamp = 0;
                    LastClickedHeaderIndex = -1;
                }

                PressedHeaderIndex = -1;
            }
            else if (!DoColumnResizeOnLeftMouseDown)
            {
                int CurrentItemIndex = GetItemIndexAt(e.X, e.Y);
                int OldFocusedItemIndex = FocusedItemIndex;
                bool AddRangeToSelection = ModifierKeys.HasFlag(Keys.Shift);
                bool ToggleSelection = ModifierKeys.HasFlag(Keys.Control);
                bool ItemWasClicked = CurrentItemIndex >= 0 && CurrentItemIndex == _PressedItemIndex;
                bool MultiSelecting = !ItemSelectionEnd.IsEmpty;

                if (AllowMultipleSelectedItems)
                {
                    if (MultiSelecting)
                    {
                        EndSelectionRectangle();
                    }
                    else   // Select only the item under the cursor if there is no selection rectangle.
                    {
                        bool SelectedItemIndicesCleared = false;

                        // Clear the selection if no modifier key was pressed and either multiple items were selected or an item was 
                        // clicked that was not selected or no item was clicked at all (e.g. a blank space).
                        if (!AddRangeToSelection && !ToggleSelection
                            && (!ItemWasClicked || SelectedItemIndices.Count > 1 || !SelectedItemIndices.Contains(CurrentItemIndex)))
                        {
                            SelectedItemIndices.Clear();
                            SelectedItemIndicesCleared = true;
                        }

                        if (ItemWasClicked)
                        {
                            if (ToggleSelection)
                            {
                                if (SelectedItemIndices.Contains(CurrentItemIndex))
                                {
                                    SelectedItemIndices.Remove(CurrentItemIndex);
                                }
                                else
                                {
                                    SelectedItemIndices.Add(CurrentItemIndex);
                                }
                            }
                            else if (SelectedItemIndicesCleared || !SelectedItemIndices.Contains(CurrentItemIndex))
                            {
                                if (AddRangeToSelection)
                                {
                                    if (OldFocusedItemIndex >= 0)
                                    {
                                        this.AddRangeToSelection(CurrentItemIndex, OldFocusedItemIndex);
                                    }
                                }
                                else
                                {
                                    SelectedItemIndices.Add(CurrentItemIndex);
                                }
                            }
                        }

                        FocusedItemIndex = CurrentItemIndex;
                    }
                }
                else if (CurrentItemIndex == _PressedItemIndex)
                {
                    SelectedItemIndex = CurrentItemIndex;
                    FocusedItemIndex = CurrentItemIndex;
                }

                _PressedItemIndex = -1;

                if (!MultiSelecting && ItemWasClicked)
                {
                    int ColumnIndex = GetColumnIndexAt(e.X);

                    // Begin cell editing if an already focused item is clicked a second time without any modifier keys pressed.
                    if (!AddRangeToSelection && !ToggleSelection && FocusedItemIndex >= 0 && FocusedItemIndex == OldFocusedItemIndex)
                    {
                        ItemSelectionStart = Point.Empty;
                        EnsureCellVisibility(ColumnIndex, FocusedItemIndex);
                        BeginCellEdit(ColumnIndex, FocusedItemIndex);
                    }

                    OnCellClicked(new CellClickEventArgs(ColumnIndex, CurrentItemIndex));
                }
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                HotItemIndex = GetItemIndexAt(e.X, e.Y);
                HotHeaderIndex = GetColumnHeaderIndexAt(e.X, e.Y);

                if (AllowColumnResize)
                {
                    // Change the mouse cursor if columns can be resized and the cursor is inside the grip range.
                    if (HotHeaderIndex >= 0)
                    {
                        int RightColumnEdge = _ContentRectangle.X;

                        for (int i = 0; i <= HotHeaderIndex; i++)
                        {
                            RightColumnEdge += Columns[i].Width;
                        }

                        RightColumnEdge--;

                        int ScrolledMouseX = e.X + HScroll.Value;
                        int GripStart = RightColumnEdge - ResizeGripWidth;

                        if (ScrolledMouseX >= GripStart && ScrolledMouseX <= RightColumnEdge)
                        {
                            Cursor = Cursors.VSplit;
                            DoColumnResizeOnLeftMouseDown = true;
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            DoColumnResizeOnLeftMouseDown = false;
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        DoColumnResizeOnLeftMouseDown = false;
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                // Resize the hot column.
                if (DoColumnResizeOnLeftMouseDown)
                {
                    int RightColumnEdge = _ContentRectangle.X;

                    for (int i = 0; i <= HotHeaderIndex; i++)
                    {
                        RightColumnEdge += Columns[i].Width;
                    }

                    RightColumnEdge--;

                    int ScrolledMouseX = e.X + HScroll.Value;

                    int WidthDelta = ScrolledMouseX - RightColumnEdge;
                    var ResizingColumn = Columns[HotHeaderIndex];
                    int NewColumnWidth = ResizingColumn.Width + WidthDelta;

                    if (NewColumnWidth >= ResizeGripWidth)
                    {
                        ResizingColumn.Width = NewColumnWidth;
                    }
                }
                else if (PressedHeaderIndex == -1) // Don't update the selection rectangle if a header if currently pressed.
                {
                    if (AllowMultipleSelectedItems && !ItemSelectionStart.IsEmpty)
                    {
                        UpdateSelectionRectangle(e.X, e.Y);
                    }
                }
                else if (AllowColumnReordering)
                {
                    ReorderColumnIndex = GetColumnHeaderIndexAt(e.X, e.Y);
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HotItemIndex = -1;
            HotHeaderIndex = -1;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (VScroll.Visible)
            {
                int ScrollDelta = (e.Delta / SystemInformation.MouseWheelScrollDelta) * ItemHeight * SystemInformation.MouseWheelScrollLines;
                int NewScrollValue = VScroll.Value - ScrollDelta;

                VScroll.Value = NewScrollValue.Clamp(VScroll.Minimum, VScroll.Maximum - VScroll.LargeChange + 1);
            }

            base.OnMouseWheel(e);
        }

        #endregion

        #region Collection changes

        protected void ItemCollectionChanged(object sender, NotifyingCollectionChangedEventArgs e)
        {
            CancelCellEdit();
            Select(-1);
            _FocusedItemIndex = -1;
            Invalidate();
        }

        protected void ItemChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            Invalidate();
        }

        protected void ColumnCollectionChanged(object sender, NotifyingCollectionChangedEventArgs e)
        {
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
            }

            Invalidate();
            CancelCellEdit();
        }

        protected void ColumnCollectionChanging(object sender, NotifyingCollectionChangingEventArgs e)
        {
            // As a reminder (in case of temporary stupidity), removing the EditorControls has to be 
            // done here in the "changing" event, rather than in the "changed" event, because the column 
            // collection is already empty when that event is fired.
            if (e.Action == NotifyingCollectionChangeAction.Clear)
            {
                var ClearedCollection = (NotifyingCollection<Column>)sender;

                foreach (var column in ClearedCollection)
                {
                    if (column.CellEditor != null && column.CellEditor.EditorControl != null)
                    {
                        Controls.Remove(column.CellEditor.EditorControl);
                    }
                }
            }
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

            if (IsSelectedItemChangedEventActive)
            {
                OnSelectedItemsChanged(EventArgs.Empty);
            }
        }

        #endregion

        #region Scrolling

        protected void ScrollValueChanged(object sender, EventArgs e)
        {
            Invalidate();
            CancelCellEdit();
        }

        protected void UpdateScrollBars()
        {
            if (Columns.Count == 0) return; // This might be bad, but without it the VS form designer goes crazy.

            // Vertical
            int TotalContentHeight = (Items.Count + 1) * ItemHeight;
            int ContentHeight = (!HScroll.Visible) ? _ContentRectangle.Height : _ContentRectangle.Height - (HScroll.Height + 2);

            if (TotalContentHeight > ContentHeight)
            {
                VScroll.Left = _ContentRectangle.Right - 1 - VScroll.Width;
                VScroll.Top = _ContentRectangle.Top + 1;
                VScroll.Minimum = 0;
                VScroll.SmallChange = ItemHeight;

                VScroll.Visible = true;
                VScroll.Update();

                _ContentRectangle.Width -= (VScroll.Width + 2);
            }
            else if (VScroll.Visible)
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

            if (TotalContentWidth > _ContentRectangle.Width)
            {
                HScroll.Left = _ContentRectangle.Left + 1;
                HScroll.Top = _ContentRectangle.Bottom - 1 - HScroll.Height;
                HScroll.Minimum = 0;

                HScroll.Visible = true;
                HScroll.Update();

                _ContentRectangle.Height -= (HScroll.Height + 2);
            }
            else if (HScroll.Visible)
            {
                HScroll.Minimum = 0;
                HScroll.Maximum = 0;
                HScroll.Value = 0;
                HScroll.Visible = false;
            }

            // Set values that are dependent on scroll bar visibility.
            VScroll.Height = _ContentRectangle.Height - 2;
            VScroll.Maximum = TotalContentHeight - ItemHeight - 1;
            VScroll.LargeChange = Math.Max(0, _ContentRectangle.Height - ItemHeight);

            HScroll.Width = _ContentRectangle.Width - 2;
            HScroll.Maximum = TotalContentWidth - 1;
            HScroll.LargeChange = Math.Max(0, _ContentRectangle.Width);
            HScroll.SmallChange = HScroll.LargeChange / 10;

            if (VScroll.Value > (VScroll.Maximum - VScroll.LargeChange + 1))
            {
                VScroll.Value = VScroll.Maximum - VScroll.LargeChange + 1;
            }

            if (HScroll.Value > (HScroll.Maximum - HScroll.LargeChange + 1))
            {
                HScroll.Value = HScroll.Maximum - HScroll.LargeChange + 1;
            }
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

        #region Event trigger

        protected void OnHeaderClicked(HeaderClickEventArgs args)
        {
            var Handler = HeaderClicked;
            Handler?.Invoke(this, args);
        }

        protected void OnHeaderDoubleClicked(HeaderClickEventArgs args)
        {
            var Handler = HeaderDoubleClicked;
            Handler?.Invoke(this, args);
        }

        protected void OnCellClicked(CellClickEventArgs args)
        {
            var Handler = CellClicked;
            Handler?.Invoke(this, args);
        }

        protected void OnSelectedItemsChanged(EventArgs args)
        {
            var Handler = SelectedItemsChanged;
            Handler?.Invoke(this, args);
        }

        #endregion

        #region Misc

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            CancelCellEdit();

            // This prevents the control from being resized when minimized, which would mess up the integral height calculation.
            if (specified == BoundsSpecified.None && height == 0 && width == 0) return;

            if (IntegralHeight && height > MinimumSize.Height)
            {
                height = CalculateIntegralHeight(height);
            }

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

        protected (Color, Color) GetCellColors(Column column, object item, object cellValue)
        {
            if (column.CellColorEvaluator != null)
            {
                return column.CellColorEvaluator(cellValue);
            }
            else if (ItemColorEvaluator != null)
            {
                return ItemColorEvaluator(item);
            }
            else
            {
                return (Color.Empty, Color.Empty);
            }
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

            ActiveCellEditor = null;

            Focus();
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                _GridPen?.Dispose();
                _SelectionRectanglePen?.Dispose();
                _BorderPen?.Dispose();
                _SelectionRectangleBrush?.Dispose();
                _BackgroundBrush?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        public int GetItemIndexAt(int x, int y)
        {
            if (x < _ContentRectangle.X || x >= _ContentRectangle.Right) return -1;
            if (y < _ContentRectangle.Y + ItemHeight || y >= _ContentRectangle.Bottom) return -1;
            if (Items.Count <= 0) return -1;
            if (Columns.Count <= 0) return -1;

            int LastColumnRight = _ContentRectangle.X;

            foreach (var column in Columns)
            {
                LastColumnRight += column.Width;
            }

            if (x >= LastColumnRight) return -1;

            int ItemIndex = (int)Math.Floor((y - _ContentRectangle.Y + VScroll.Value - ItemHeight) / (double)ItemHeight);

            if (ItemIndex >= Items.Count)
            {
                ItemIndex = -1;
            }

            ItemIndex = Math.Max(-1, ItemIndex);

            return ItemIndex;
        }

        public int GetColumnIndexAt(int x)
        {
            return GetColumnHeaderIndexAt(x, _ContentRectangle.Y);
        }

        public int GetColumnHeaderIndexAt(int x, int y)
        {
            if (x < _ContentRectangle.X || x >= _ContentRectangle.Right) return -1;
            if ((y < _ContentRectangle.Y) || (y >= _ContentRectangle.Y + ItemHeight)) return -1;

            int ScrolledX = x + HScroll.Value;
            int Right = _ContentRectangle.X;
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

            var Bounds = new Rectangle(_ContentRectangle.X, _ContentRectangle.Y + ItemHeight, Columns[columnIndex].Width, ItemHeight);

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
            if (itemIndex < 0 || Items.Count == 0 || itemIndex >= Items.Count) return;

            int ItemTop = itemIndex * ItemHeight;
            int ItemBottom = ItemTop + ItemHeight - 1;

            int VisibleTop = VScroll.Value;
            int VisibleBottom = VScroll.Value + VScroll.LargeChange - 1;

            int ScrollOffset = (ItemTop < VisibleTop)
                ? ItemTop - VisibleTop
                : (ItemBottom > VisibleBottom)
                ? ItemBottom - VisibleBottom
                : 0;

            VScroll.Value += ScrollOffset;
        }

        public void EnsureColumnVisibility(int columnIndex)
        {
            if (columnIndex < 0 || Columns.Count == 0 || columnIndex >= Columns.Count) return;

            int ColumnLeft = 0;
            int ColumnRight = 0;

            for (int i = 0; i <= columnIndex; i++)
            {
                ColumnLeft = ColumnRight;
                ColumnRight += Columns[i].Width;
            }

            ColumnRight--;

            int VisibleLeft = HScroll.Value;
            int VisibleRight = HScroll.Value + HScroll.LargeChange - 1;

            int ScrollOffset = (ColumnLeft < VisibleLeft)
                ? ColumnLeft - VisibleLeft
                : (ColumnRight > VisibleRight)
                ? ColumnRight - VisibleRight
                : 0;

            HScroll.Value += ScrollOffset;
        }

        public void EnsureCellVisibility(int columnIndex, int itemIndex)
        {
            EnsureColumnVisibility(columnIndex);
            EnsureItemVisibility(itemIndex);
        }

        public void Select(int index, bool add = false)
        {
            if (index >= Items.Count) throw new ArgumentOutOfRangeException(nameof(index));

            if (AllowMultipleSelectedItems)
            {
                if (!add) SelectedItemIndices.Clear();

                if (index >= 0 && !SelectedItemIndices.Contains(index)) // This way selecting a negative index will clear the selection.
                {
                    SelectedItemIndices.Add(index);
                }
            }
            else
            {
                SelectedItemIndex = (index < -1) ? -1 : index;
            }
        }

        public void SelectRange(int startIndex, int endIndex, bool add = false)
        {
            if (!AllowMultipleSelectedItems) return;

            if (startIndex < 0 || startIndex >= Items.Count) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (endIndex < 0 || endIndex >= Items.Count) throw new ArgumentOutOfRangeException(nameof(endIndex));

            if (!add) SelectedItemIndices.Clear();

            AddRangeToSelection(startIndex, endIndex);
        }

        public void SelectAll()
        {
            if (AllowMultipleSelectedItems && Items.Count > 0)
            {
                SelectedItemIndices.AddRange(Enumerable.Range(0, Items.Count));
            }
        }

        public void DeselectAll()
        {
            if (AllowMultipleSelectedItems)
            {
                SelectedItemIndices.Clear();
            }
            else
            {
                SelectedItemIndex = -1;
            }
        }

        public void RemoveSelectedItems()
        {
            if (AllowMultipleSelectedItems)
            {
                Items.RemoveAtRange(SelectedItemIndices);
            }
            else if (_SelectedItemIndex >= 0)
            {
                Items.RemoveAt(_SelectedItemIndex);
            }
        }

        public void BeginCellEdit(int columnIndex, int itemIndex)
        {
            if (Columns.Count == 0) throw new InvalidOperationException(string.Format("{0} has no columns.", nameof(DList)));
            if (Items.Count == 0) throw new InvalidOperationException(string.Format("{0} has no items.", nameof(DList)));
            if (columnIndex < 0 || columnIndex >= Columns.Count) throw new ArgumentOutOfRangeException(nameof(columnIndex));
            if (itemIndex < 0 || itemIndex >= Items.Count) throw new ArgumentOutOfRangeException(nameof(itemIndex));

            if (ActiveCellEditor != null) ActiveCellEditor.Cancel();

            var Column = Columns[columnIndex];

            if (Column.CanEdit)
            {
                EnsureItemVisibility(itemIndex);

                object Value = Column.GetValue(Items[itemIndex]);
                var Editor = Column.CellEditor;
                Editor.Done += CellEditorDone;

                ActiveCellEditor = Editor;

                var CellBounds = GetCellBounds(columnIndex, itemIndex);
                Editor.Edit(CellBounds, columnIndex, itemIndex, Value);
            }
        }

        public void CancelCellEdit()
        {
            if (ActiveCellEditor != null)
            {
                ActiveCellEditor.Cancel();
            }
        }

        private void StartSelectionRectangle(int x, int y)
        {
            ItemSelectionStart.X = x + _ContentRectangle.X + HScroll.Value;
            ItemSelectionStart.Y = y + _ContentRectangle.Y + VScroll.Value;
        }

        private void UpdateSelectionRectangle(int x, int y)
        {
            int ScrolledX = x + _ContentRectangle.X + HScroll.Value;
            int ScrolledY = y + _ContentRectangle.Y + VScroll.Value;
            bool AddToSelection = (ModifierKeys.HasFlag(Keys.Shift) || ModifierKeys.HasFlag(Keys.Control));

            if ((Math.Abs(ItemSelectionStart.X - ScrolledX) >= SystemInformation.DragSize.Width)
                && (Math.Abs(ItemSelectionStart.Y - ScrolledY) >= SystemInformation.DragSize.Height))
            {
                if (!AddToSelection) SelectedItemIndices.Clear();
                if (HotItemIndex >= 0) HotItemIndex = -1;

                ItemSelectionEnd.X = ScrolledX;
                ItemSelectionEnd.Y = ScrolledY;
            }

            // Autoscroll up or down if the selection rectangle is dragged above or beneath the item list.
            if (y >= _ContentRectangle.Bottom || y < (_ContentRectangle.Y + ItemHeight))
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
            else
            {
                // If we don't scroll we need to invalidate manually to draw the selection rectangle.
                Invalidate();
            }
        }

        private void EndSelectionRectangle()
        {
            if (!ItemSelectionStart.IsEmpty && !ItemSelectionEnd.IsEmpty)
            {
                bool Invalidated = false;

                if (Columns.Count > 0 && Items.Count > 0)
                {
                    var Selection = Utils.GetRectangleFromPoints(ItemSelectionStart, ItemSelectionEnd);

                    if (Selection.Width >= SystemInformation.DragSize.Width && Selection.Height >= SystemInformation.DragSize.Height)
                    {
                        int ItemsStartY = _ContentRectangle.Y + ItemHeight;
                        int ItemsEndY = ItemsStartY + Items.Count * ItemHeight - 1;
                        bool VerticalOverlap = (Selection.Y <= ItemsEndY);
                        bool HorizontalOverlap = false;

                        if (VerticalOverlap)
                        {
                            // Check if the selection rectangle does horizontally overlap with any items
                            int RightEdge = _ContentRectangle.X;

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
                            int AffectedIndicesCount = LastItemIndex - FirstItemIndex + 1;
                            bool AddToSelection = ModifierKeys.HasFlag(Keys.Shift);
                            bool ToggleSelection = ModifierKeys.HasFlag(Keys.Control);

                            if (!AddToSelection && !ToggleSelection) SelectedItemIndices.Clear();

                            if (AffectedIndicesCount > 0)
                            {
                                var AffectedIndices = Enumerable.Range(FirstItemIndex, AffectedIndicesCount);

                                if (ToggleSelection)
                                {
                                    // Prevent event spam. Without this we would get an event for every toggled selection.
                                    IsSelectedItemChangedEventActive = false;

                                    foreach (int index in AffectedIndices)
                                    {
                                        if (SelectedItemIndices.Contains(index))
                                        {
                                            SelectedItemIndices.Remove(index);
                                        }
                                        else
                                        {
                                            SelectedItemIndices.Add(index);
                                        }
                                    }

                                    IsSelectedItemChangedEventActive = true;

                                    OnSelectedItemsChanged(EventArgs.Empty);    // Manually trigger SelectedItemsChanged once.
                                }
                                else
                                {
                                    SelectedItemIndices.AddRange(AffectedIndices);
                                }
                            }

                            // Focus the last item in the direction the selection rectangle was drawn
                            FocusedItemIndex = (ItemSelectionStart.Y < ItemSelectionEnd.Y) ? LastItemIndex : FirstItemIndex;
                            Invalidated = true;
                        }
                    }
                }

                // Clean up selection rectangle data
                ItemSelectionStart = Point.Empty;
                ItemSelectionEnd = Point.Empty;

                if (!Invalidated) Invalidate();
            }
        }

        private void AddRangeToSelection(int startIndex, int endIndex)
        {
            int IndicesToAddCount = Math.Abs(endIndex - startIndex) + 1;
            int FirstIndex = Math.Min(startIndex, endIndex);
            var IndicesToAdd = Enumerable.Range(FirstIndex, IndicesToAddCount);

            SelectedItemIndices.AddRange(IndicesToAdd);
        }

        private void AutoResizeColumn(int index)
        {
            if (Items.Count <= 0) return;

            int FirstIndex = 0;
            int LastIndex = 0;

            switch (ColumnAutoResizeMode)
            {
                case AutoResizeMode.AllItems:
                    FirstIndex = 0;
                    LastIndex = Items.Count - 1;
                    break;
                
                case AutoResizeMode.OnlyVisibleItems:
                    FirstIndex = GetFirstVisibleItemIndex();
                    LastIndex = GetLastVisibleItemIndex();
                    break;
                
                default:
                    return;
            }

            Column ResizingColumn = Columns[index];
            IRenderer Renderer = ResizingColumn.CellRenderer;
            Font ItemFont = ResizingColumn.ItemFont ?? Font;
            int OptimalWidth = -1;
            int CurrentOptimalWidth = -1;
            object Value = null;

            for (int i = FirstIndex; i <= LastIndex; i++)
            {
                Value = ResizingColumn.GetValue(Items[i]);
                CurrentOptimalWidth = Renderer.GetOptimalWidth(Value, ItemFont);
                if (CurrentOptimalWidth < 0) return;    // Early out if IRenderers do not provide an optimal width.
                
                OptimalWidth = Math.Max(OptimalWidth, CurrentOptimalWidth);
            }

            if (OptimalWidth > 0) ResizingColumn.Width = (_ShowGrid) ? OptimalWidth + 1 : OptimalWidth;
        }

        private int CalculateIntegralHeight(int height)
        {
            int ContentHeight = (HScroll.Visible) ? _ContentRectangle.Height + HScroll.Height + 2 : _ContentRectangle.Height;
            // SetBoundsCore(), and therefore this method, can and will get called before OnPaint() which means the ContentRectangle is empy at that time.
            int BorderHeight = (_ContentRectangle.Height > 0) ? ClientRectangle.Height - ContentHeight : 0;

            height = (ItemHeight * (int)Math.Floor(height / (double)ItemHeight)) + BorderHeight;

            if (HScroll.Visible)
            {
                height += HScroll.Height + 2;
            }

            return height;
        }
    }
}
