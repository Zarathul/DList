namespace InCoding
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDeselectAll = new System.Windows.Forms.Button();
            this.numericUpDownSelectRangeTo = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownSelectRangeFrom = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSelectRange = new System.Windows.Forms.Button();
            this.numericUpDownRandomItems = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAddToSelection = new System.Windows.Forms.CheckBox();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.numericUpDownItemIndex = new System.Windows.Forms.NumericUpDown();
            this.buttonSelectItem = new System.Windows.Forms.Button();
            this.buttonAddRngItem = new System.Windows.Forms.Button();
            this.buttonRemoveItem = new System.Windows.Forms.Button();
            this.listBoxEvents = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.checkBoxSelectedIndicesChanged = new System.Windows.Forms.CheckBox();
            this.checkBoxSelectedIndicesChanging = new System.Windows.Forms.CheckBox();
            this.checkBoxItemChanged = new System.Windows.Forms.CheckBox();
            this.checkBoxItemsChanged = new System.Windows.Forms.CheckBox();
            this.checkBoxItemsChanging = new System.Windows.Forms.CheckBox();
            this.checkBoxColumnChanged = new System.Windows.Forms.CheckBox();
            this.checkBoxColumnsChanged = new System.Windows.Forms.CheckBox();
            this.checkBoxColumnsChanging = new System.Windows.Forms.CheckBox();
            this.checkBoxSelectedItemsChanged = new System.Windows.Forms.CheckBox();
            this.checkBoxCellClicked = new System.Windows.Forms.CheckBox();
            this.checkBoxHeaderClicked = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCancelEdit = new System.Windows.Forms.Button();
            this.buttonBeginEdit = new System.Windows.Forms.Button();
            this.buttonEnsureCellVisibility = new System.Windows.Forms.Button();
            this.buttonEnsureColumnVisibility = new System.Windows.Forms.Button();
            this.buttonEnsureItemVisibility = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownItemIndex2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownColumnIndex = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.dList1 = new InCoding.DList.DList();
            this.checkBoxHeaderDoubleClicked = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectRangeTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectRangeFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRandomItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemIndex2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumnIndex)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonDeselectAll);
            this.groupBox1.Controls.Add(this.numericUpDownSelectRangeTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDownSelectRangeFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonSelectRange);
            this.groupBox1.Controls.Add(this.numericUpDownRandomItems);
            this.groupBox1.Controls.Add(this.checkBoxAddToSelection);
            this.groupBox1.Controls.Add(this.buttonSelectAll);
            this.groupBox1.Controls.Add(this.numericUpDownItemIndex);
            this.groupBox1.Controls.Add(this.buttonSelectItem);
            this.groupBox1.Controls.Add(this.buttonAddRngItem);
            this.groupBox1.Controls.Add(this.buttonRemoveItem);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 124);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items";
            // 
            // buttonDeselectAll
            // 
            this.buttonDeselectAll.Location = new System.Drawing.Point(124, 97);
            this.buttonDeselectAll.Name = "buttonDeselectAll";
            this.buttonDeselectAll.Size = new System.Drawing.Size(112, 20);
            this.buttonDeselectAll.TabIndex = 13;
            this.buttonDeselectAll.Text = "Deselect all";
            this.buttonDeselectAll.UseVisualStyleBackColor = true;
            this.buttonDeselectAll.Click += new System.EventHandler(this.ButtonDeselectAllClick);
            // 
            // numericUpDownSelectRangeTo
            // 
            this.numericUpDownSelectRangeTo.Location = new System.Drawing.Point(264, 71);
            this.numericUpDownSelectRangeTo.Name = "numericUpDownSelectRangeTo";
            this.numericUpDownSelectRangeTo.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownSelectRangeTo.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(232, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "to";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownSelectRangeFrom
            // 
            this.numericUpDownSelectRangeFrom.Location = new System.Drawing.Point(167, 71);
            this.numericUpDownSelectRangeFrom.Name = "numericUpDownSelectRangeFrom";
            this.numericUpDownSelectRangeFrom.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownSelectRangeFrom.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(124, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "from";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonSelectRange
            // 
            this.buttonSelectRange.Location = new System.Drawing.Point(6, 71);
            this.buttonSelectRange.Name = "buttonSelectRange";
            this.buttonSelectRange.Size = new System.Drawing.Size(112, 20);
            this.buttonSelectRange.TabIndex = 8;
            this.buttonSelectRange.Text = "Select range";
            this.buttonSelectRange.UseVisualStyleBackColor = true;
            this.buttonSelectRange.Click += new System.EventHandler(this.ButtonSelectRangeClick);
            // 
            // numericUpDownRandomItems
            // 
            this.numericUpDownRandomItems.Location = new System.Drawing.Point(124, 19);
            this.numericUpDownRandomItems.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownRandomItems.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRandomItems.Name = "numericUpDownRandomItems";
            this.numericUpDownRandomItems.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownRandomItems.TabIndex = 7;
            this.numericUpDownRandomItems.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxAddToSelection
            // 
            this.checkBoxAddToSelection.AutoSize = true;
            this.checkBoxAddToSelection.Location = new System.Drawing.Point(189, 48);
            this.checkBoxAddToSelection.Name = "checkBoxAddToSelection";
            this.checkBoxAddToSelection.Size = new System.Drawing.Size(102, 17);
            this.checkBoxAddToSelection.TabIndex = 6;
            this.checkBoxAddToSelection.Text = "Add to selection";
            this.checkBoxAddToSelection.UseVisualStyleBackColor = true;
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(6, 97);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(112, 20);
            this.buttonSelectAll.TabIndex = 4;
            this.buttonSelectAll.Text = "Select all";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.ButtonSelectAllClick);
            // 
            // numericUpDownItemIndex
            // 
            this.numericUpDownItemIndex.Location = new System.Drawing.Point(124, 45);
            this.numericUpDownItemIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownItemIndex.Name = "numericUpDownItemIndex";
            this.numericUpDownItemIndex.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownItemIndex.TabIndex = 3;
            // 
            // buttonSelectItem
            // 
            this.buttonSelectItem.Location = new System.Drawing.Point(6, 45);
            this.buttonSelectItem.Name = "buttonSelectItem";
            this.buttonSelectItem.Size = new System.Drawing.Size(112, 20);
            this.buttonSelectItem.TabIndex = 2;
            this.buttonSelectItem.Text = "Select";
            this.buttonSelectItem.UseVisualStyleBackColor = true;
            this.buttonSelectItem.Click += new System.EventHandler(this.ButtonSelectItemClick);
            // 
            // buttonAddRngItem
            // 
            this.buttonAddRngItem.Location = new System.Drawing.Point(6, 19);
            this.buttonAddRngItem.Name = "buttonAddRngItem";
            this.buttonAddRngItem.Size = new System.Drawing.Size(112, 20);
            this.buttonAddRngItem.TabIndex = 0;
            this.buttonAddRngItem.Text = "Add random";
            this.buttonAddRngItem.UseVisualStyleBackColor = true;
            this.buttonAddRngItem.Click += new System.EventHandler(this.ButtonAddRngItemClick);
            // 
            // buttonRemoveItem
            // 
            this.buttonRemoveItem.Location = new System.Drawing.Point(189, 19);
            this.buttonRemoveItem.Name = "buttonRemoveItem";
            this.buttonRemoveItem.Size = new System.Drawing.Size(112, 20);
            this.buttonRemoveItem.TabIndex = 1;
            this.buttonRemoveItem.Text = "Remove selected";
            this.buttonRemoveItem.UseVisualStyleBackColor = true;
            this.buttonRemoveItem.Click += new System.EventHandler(this.ButtonRemoveItemClick);
            // 
            // listBoxEvents
            // 
            this.listBoxEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxEvents.FormattingEnabled = true;
            this.listBoxEvents.IntegralHeight = false;
            this.listBoxEvents.Location = new System.Drawing.Point(3, 3);
            this.listBoxEvents.Name = "listBoxEvents";
            this.listBoxEvents.Size = new System.Drawing.Size(323, 295);
            this.listBoxEvents.TabIndex = 0;
            this.listBoxEvents.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxEventsMouseDoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dList1);
            this.splitContainer1.Size = new System.Drawing.Size(967, 644);
            this.splitContainer1.SplitterDistance = 357;
            this.splitContainer1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(357, 644);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(349, 618);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Items & Events";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(6, 285);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(337, 327);
            this.tabControl2.TabIndex = 7;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBoxEvents);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(329, 301);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Events";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.checkBoxHeaderDoubleClicked);
            this.tabPage4.Controls.Add(this.checkBoxSelectedIndicesChanged);
            this.tabPage4.Controls.Add(this.checkBoxSelectedIndicesChanging);
            this.tabPage4.Controls.Add(this.checkBoxItemChanged);
            this.tabPage4.Controls.Add(this.checkBoxItemsChanged);
            this.tabPage4.Controls.Add(this.checkBoxItemsChanging);
            this.tabPage4.Controls.Add(this.checkBoxColumnChanged);
            this.tabPage4.Controls.Add(this.checkBoxColumnsChanged);
            this.tabPage4.Controls.Add(this.checkBoxColumnsChanging);
            this.tabPage4.Controls.Add(this.checkBoxSelectedItemsChanged);
            this.tabPage4.Controls.Add(this.checkBoxCellClicked);
            this.tabPage4.Controls.Add(this.checkBoxHeaderClicked);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(329, 301);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Filters";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // checkBoxSelectedIndicesChanged
            // 
            this.checkBoxSelectedIndicesChanged.Location = new System.Drawing.Point(6, 259);
            this.checkBoxSelectedIndicesChanged.Name = "checkBoxSelectedIndicesChanged";
            this.checkBoxSelectedIndicesChanged.Size = new System.Drawing.Size(151, 17);
            this.checkBoxSelectedIndicesChanged.TabIndex = 11;
            this.checkBoxSelectedIndicesChanged.Text = "Selected indices changed";
            this.checkBoxSelectedIndicesChanged.UseVisualStyleBackColor = true;
            // 
            // checkBoxSelectedIndicesChanging
            // 
            this.checkBoxSelectedIndicesChanging.Location = new System.Drawing.Point(6, 236);
            this.checkBoxSelectedIndicesChanging.Name = "checkBoxSelectedIndicesChanging";
            this.checkBoxSelectedIndicesChanging.Size = new System.Drawing.Size(151, 17);
            this.checkBoxSelectedIndicesChanging.TabIndex = 10;
            this.checkBoxSelectedIndicesChanging.Text = "Selected indices changing";
            this.checkBoxSelectedIndicesChanging.UseVisualStyleBackColor = true;
            // 
            // checkBoxItemChanged
            // 
            this.checkBoxItemChanged.Checked = true;
            this.checkBoxItemChanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxItemChanged.Location = new System.Drawing.Point(6, 213);
            this.checkBoxItemChanged.Name = "checkBoxItemChanged";
            this.checkBoxItemChanged.Size = new System.Drawing.Size(151, 17);
            this.checkBoxItemChanged.TabIndex = 9;
            this.checkBoxItemChanged.Text = "Item changed";
            this.checkBoxItemChanged.UseVisualStyleBackColor = true;
            // 
            // checkBoxItemsChanged
            // 
            this.checkBoxItemsChanged.Checked = true;
            this.checkBoxItemsChanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxItemsChanged.Location = new System.Drawing.Point(6, 190);
            this.checkBoxItemsChanged.Name = "checkBoxItemsChanged";
            this.checkBoxItemsChanged.Size = new System.Drawing.Size(151, 17);
            this.checkBoxItemsChanged.TabIndex = 8;
            this.checkBoxItemsChanged.Text = "Items changed";
            this.checkBoxItemsChanged.UseVisualStyleBackColor = true;
            // 
            // checkBoxItemsChanging
            // 
            this.checkBoxItemsChanging.Location = new System.Drawing.Point(6, 167);
            this.checkBoxItemsChanging.Name = "checkBoxItemsChanging";
            this.checkBoxItemsChanging.Size = new System.Drawing.Size(151, 17);
            this.checkBoxItemsChanging.TabIndex = 7;
            this.checkBoxItemsChanging.Text = "Items changing";
            this.checkBoxItemsChanging.UseVisualStyleBackColor = true;
            // 
            // checkBoxColumnChanged
            // 
            this.checkBoxColumnChanged.Checked = true;
            this.checkBoxColumnChanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxColumnChanged.Location = new System.Drawing.Point(6, 144);
            this.checkBoxColumnChanged.Name = "checkBoxColumnChanged";
            this.checkBoxColumnChanged.Size = new System.Drawing.Size(151, 17);
            this.checkBoxColumnChanged.TabIndex = 6;
            this.checkBoxColumnChanged.Text = "Column changed";
            this.checkBoxColumnChanged.UseVisualStyleBackColor = true;
            // 
            // checkBoxColumnsChanged
            // 
            this.checkBoxColumnsChanged.Checked = true;
            this.checkBoxColumnsChanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxColumnsChanged.Location = new System.Drawing.Point(6, 121);
            this.checkBoxColumnsChanged.Name = "checkBoxColumnsChanged";
            this.checkBoxColumnsChanged.Size = new System.Drawing.Size(151, 17);
            this.checkBoxColumnsChanged.TabIndex = 5;
            this.checkBoxColumnsChanged.Text = "Columns changed";
            this.checkBoxColumnsChanged.UseVisualStyleBackColor = true;
            // 
            // checkBoxColumnsChanging
            // 
            this.checkBoxColumnsChanging.Location = new System.Drawing.Point(6, 98);
            this.checkBoxColumnsChanging.Name = "checkBoxColumnsChanging";
            this.checkBoxColumnsChanging.Size = new System.Drawing.Size(151, 17);
            this.checkBoxColumnsChanging.TabIndex = 4;
            this.checkBoxColumnsChanging.Text = "Columns changing";
            this.checkBoxColumnsChanging.UseVisualStyleBackColor = true;
            // 
            // checkBoxSelectedItemsChanged
            // 
            this.checkBoxSelectedItemsChanged.Checked = true;
            this.checkBoxSelectedItemsChanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSelectedItemsChanged.Location = new System.Drawing.Point(6, 75);
            this.checkBoxSelectedItemsChanged.Name = "checkBoxSelectedItemsChanged";
            this.checkBoxSelectedItemsChanged.Size = new System.Drawing.Size(151, 17);
            this.checkBoxSelectedItemsChanged.TabIndex = 3;
            this.checkBoxSelectedItemsChanged.Text = "SelectedItemsChanged";
            this.checkBoxSelectedItemsChanged.UseVisualStyleBackColor = true;
            // 
            // checkBoxCellClicked
            // 
            this.checkBoxCellClicked.Checked = true;
            this.checkBoxCellClicked.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCellClicked.Location = new System.Drawing.Point(6, 52);
            this.checkBoxCellClicked.Name = "checkBoxCellClicked";
            this.checkBoxCellClicked.Size = new System.Drawing.Size(151, 17);
            this.checkBoxCellClicked.TabIndex = 2;
            this.checkBoxCellClicked.Text = "CellClicked";
            this.checkBoxCellClicked.UseVisualStyleBackColor = true;
            // 
            // checkBoxHeaderClicked
            // 
            this.checkBoxHeaderClicked.Checked = true;
            this.checkBoxHeaderClicked.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHeaderClicked.Location = new System.Drawing.Point(6, 6);
            this.checkBoxHeaderClicked.Name = "checkBoxHeaderClicked";
            this.checkBoxHeaderClicked.Size = new System.Drawing.Size(151, 17);
            this.checkBoxHeaderClicked.TabIndex = 0;
            this.checkBoxHeaderClicked.Text = "HeaderClicked";
            this.checkBoxHeaderClicked.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonCancelEdit);
            this.groupBox2.Controls.Add(this.buttonBeginEdit);
            this.groupBox2.Controls.Add(this.buttonEnsureCellVisibility);
            this.groupBox2.Controls.Add(this.buttonEnsureColumnVisibility);
            this.groupBox2.Controls.Add(this.buttonEnsureItemVisibility);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericUpDownItemIndex2);
            this.groupBox2.Controls.Add(this.numericUpDownColumnIndex);
            this.groupBox2.Location = new System.Drawing.Point(6, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 143);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visibilty && Editing";
            // 
            // buttonCancelEdit
            // 
            this.buttonCancelEdit.Location = new System.Drawing.Point(157, 97);
            this.buttonCancelEdit.Name = "buttonCancelEdit";
            this.buttonCancelEdit.Size = new System.Drawing.Size(134, 20);
            this.buttonCancelEdit.TabIndex = 8;
            this.buttonCancelEdit.Text = "Cancel edit";
            this.buttonCancelEdit.UseVisualStyleBackColor = true;
            this.buttonCancelEdit.Click += new System.EventHandler(this.ButtonCancelEditClick);
            // 
            // buttonBeginEdit
            // 
            this.buttonBeginEdit.Location = new System.Drawing.Point(17, 97);
            this.buttonBeginEdit.Name = "buttonBeginEdit";
            this.buttonBeginEdit.Size = new System.Drawing.Size(134, 20);
            this.buttonBeginEdit.TabIndex = 7;
            this.buttonBeginEdit.Text = "Begin edit";
            this.buttonBeginEdit.UseVisualStyleBackColor = true;
            this.buttonBeginEdit.Click += new System.EventHandler(this.ButtonBeginEditClick);
            // 
            // buttonEnsureCellVisibility
            // 
            this.buttonEnsureCellVisibility.Location = new System.Drawing.Point(157, 71);
            this.buttonEnsureCellVisibility.Name = "buttonEnsureCellVisibility";
            this.buttonEnsureCellVisibility.Size = new System.Drawing.Size(134, 20);
            this.buttonEnsureCellVisibility.TabIndex = 6;
            this.buttonEnsureCellVisibility.Text = "Ensure cell visibility";
            this.buttonEnsureCellVisibility.UseVisualStyleBackColor = true;
            this.buttonEnsureCellVisibility.Click += new System.EventHandler(this.ButtonEnsureCellVisibilityClick);
            // 
            // buttonEnsureColumnVisibility
            // 
            this.buttonEnsureColumnVisibility.Location = new System.Drawing.Point(157, 45);
            this.buttonEnsureColumnVisibility.Name = "buttonEnsureColumnVisibility";
            this.buttonEnsureColumnVisibility.Size = new System.Drawing.Size(134, 20);
            this.buttonEnsureColumnVisibility.TabIndex = 5;
            this.buttonEnsureColumnVisibility.Text = "Ensure column visibility";
            this.buttonEnsureColumnVisibility.UseVisualStyleBackColor = true;
            this.buttonEnsureColumnVisibility.Click += new System.EventHandler(this.ButtonEnsureColumnVisibilityClick);
            // 
            // buttonEnsureItemVisibility
            // 
            this.buttonEnsureItemVisibility.Location = new System.Drawing.Point(157, 19);
            this.buttonEnsureItemVisibility.Name = "buttonEnsureItemVisibility";
            this.buttonEnsureItemVisibility.Size = new System.Drawing.Size(134, 20);
            this.buttonEnsureItemVisibility.TabIndex = 4;
            this.buttonEnsureItemVisibility.Text = "Ensure item visibility";
            this.buttonEnsureItemVisibility.UseVisualStyleBackColor = true;
            this.buttonEnsureItemVisibility.Click += new System.EventHandler(this.ButtonEnsureItemVisibilityClick);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(8, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Item index";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(9, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Column index";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownItemIndex2
            // 
            this.numericUpDownItemIndex2.Location = new System.Drawing.Point(87, 19);
            this.numericUpDownItemIndex2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownItemIndex2.Name = "numericUpDownItemIndex2";
            this.numericUpDownItemIndex2.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownItemIndex2.TabIndex = 1;
            // 
            // numericUpDownColumnIndex
            // 
            this.numericUpDownColumnIndex.Location = new System.Drawing.Point(87, 45);
            this.numericUpDownColumnIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownColumnIndex.Name = "numericUpDownColumnIndex";
            this.numericUpDownColumnIndex.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownColumnIndex.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.propertyGrid1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(349, 618);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "PropertyGrid";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.dList1;
            this.propertyGrid1.Size = new System.Drawing.Size(343, 612);
            this.propertyGrid1.TabIndex = 0;
            // 
            // dList1
            // 
            this.dList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dList1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dList1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dList1.Location = new System.Drawing.Point(0, 0);
            this.dList1.Name = "dList1";
            this.dList1.Size = new System.Drawing.Size(606, 644);
            this.dList1.TabIndex = 3;
            this.dList1.Text = "dList1";
            this.dList1.HeaderClicked += new System.EventHandler<InCoding.DList.HeaderClickEventArgs>(this.DList1HeaderClicked);
            this.dList1.HeaderDoubleClicked += new System.EventHandler<InCoding.DList.HeaderClickEventArgs>(this.dList1_HeaderDoubleClicked);
            this.dList1.CellClicked += new System.EventHandler<InCoding.DList.CellClickEventArgs>(this.DList1CellClicked);
            this.dList1.SelectedItemsChanged += new System.EventHandler(this.DList1SelectedItemsChanged);
            // 
            // checkBoxHeaderDoubleClicked
            // 
            this.checkBoxHeaderDoubleClicked.Checked = true;
            this.checkBoxHeaderDoubleClicked.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHeaderDoubleClicked.Location = new System.Drawing.Point(6, 29);
            this.checkBoxHeaderDoubleClicked.Name = "checkBoxHeaderDoubleClicked";
            this.checkBoxHeaderDoubleClicked.Size = new System.Drawing.Size(151, 17);
            this.checkBoxHeaderDoubleClicked.TabIndex = 1;
            this.checkBoxHeaderDoubleClicked.Text = "HeaderDoubleClicked";
            this.checkBoxHeaderDoubleClicked.UseVisualStyleBackColor = true;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 644);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestForm";
            this.Text = "Dlist TestForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectRangeTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectRangeFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRandomItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemIndex)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemIndex2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumnIndex)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DList.DList dList1;
        private System.Windows.Forms.Button buttonRemoveItem;
        private System.Windows.Forms.Button buttonAddRngItem;
        private System.Windows.Forms.ListBox listBoxEvents;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.NumericUpDown numericUpDownItemIndex;
        private System.Windows.Forms.Button buttonSelectItem;
        private System.Windows.Forms.CheckBox checkBoxAddToSelection;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownColumnIndex;
        private System.Windows.Forms.Button buttonEnsureColumnVisibility;
        private System.Windows.Forms.Button buttonEnsureItemVisibility;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownItemIndex2;
        private System.Windows.Forms.Button buttonEnsureCellVisibility;
        private System.Windows.Forms.Button buttonCancelEdit;
        private System.Windows.Forms.Button buttonBeginEdit;
        private System.Windows.Forms.NumericUpDown numericUpDownRandomItems;
        private System.Windows.Forms.NumericUpDown numericUpDownSelectRangeTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownSelectRangeFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSelectRange;
        private System.Windows.Forms.Button buttonDeselectAll;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox checkBoxSelectedItemsChanged;
        private System.Windows.Forms.CheckBox checkBoxCellClicked;
        private System.Windows.Forms.CheckBox checkBoxHeaderClicked;
        private System.Windows.Forms.CheckBox checkBoxSelectedIndicesChanged;
        private System.Windows.Forms.CheckBox checkBoxSelectedIndicesChanging;
        private System.Windows.Forms.CheckBox checkBoxItemChanged;
        private System.Windows.Forms.CheckBox checkBoxItemsChanged;
        private System.Windows.Forms.CheckBox checkBoxItemsChanging;
        private System.Windows.Forms.CheckBox checkBoxColumnChanged;
        private System.Windows.Forms.CheckBox checkBoxColumnsChanged;
        private System.Windows.Forms.CheckBox checkBoxColumnsChanging;
        private System.Windows.Forms.CheckBox checkBoxHeaderDoubleClicked;
    }
}

