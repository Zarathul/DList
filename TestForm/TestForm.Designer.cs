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
            this.checkBoxAddToSelection = new System.Windows.Forms.CheckBox();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.numericUpDownItemIndex = new System.Windows.Forms.NumericUpDown();
            this.buttonSelectItem = new System.Windows.Forms.Button();
            this.buttonAddRngItem = new System.Windows.Forms.Button();
            this.buttonRemoveItem = new System.Windows.Forms.Button();
            this.groupBoxEvents = new System.Windows.Forms.GroupBox();
            this.listBoxEvents = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemIndex)).BeginInit();
            this.groupBoxEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.checkBoxAddToSelection);
            this.groupBox1.Controls.Add(this.buttonSelectAll);
            this.groupBox1.Controls.Add(this.numericUpDownItemIndex);
            this.groupBox1.Controls.Add(this.buttonSelectItem);
            this.groupBox1.Controls.Add(this.buttonAddRngItem);
            this.groupBox1.Controls.Add(this.buttonRemoveItem);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 98);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items";
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
            this.buttonSelectAll.Location = new System.Drawing.Point(6, 71);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(112, 20);
            this.buttonSelectAll.TabIndex = 4;
            this.buttonSelectAll.Text = "Select all items";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // numericUpDownItemIndex
            // 
            this.numericUpDownItemIndex.Location = new System.Drawing.Point(124, 45);
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
            this.buttonSelectItem.Text = "Select item";
            this.buttonSelectItem.UseVisualStyleBackColor = true;
            this.buttonSelectItem.Click += new System.EventHandler(this.buttonSelectItem_Click);
            // 
            // buttonAddRngItem
            // 
            this.buttonAddRngItem.Location = new System.Drawing.Point(6, 19);
            this.buttonAddRngItem.Name = "buttonAddRngItem";
            this.buttonAddRngItem.Size = new System.Drawing.Size(112, 20);
            this.buttonAddRngItem.TabIndex = 0;
            this.buttonAddRngItem.Text = "Add random";
            this.buttonAddRngItem.UseVisualStyleBackColor = true;
            this.buttonAddRngItem.Click += new System.EventHandler(this.buttonAddRngItem_Click);
            // 
            // buttonRemoveItem
            // 
            this.buttonRemoveItem.Location = new System.Drawing.Point(124, 19);
            this.buttonRemoveItem.Name = "buttonRemoveItem";
            this.buttonRemoveItem.Size = new System.Drawing.Size(112, 20);
            this.buttonRemoveItem.TabIndex = 1;
            this.buttonRemoveItem.Text = "Remove selected";
            this.buttonRemoveItem.UseVisualStyleBackColor = true;
            this.buttonRemoveItem.Click += new System.EventHandler(this.buttonRemoveItem_Click);
            // 
            // groupBoxEvents
            // 
            this.groupBoxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEvents.Controls.Add(this.listBoxEvents);
            this.groupBoxEvents.Location = new System.Drawing.Point(8, 399);
            this.groupBoxEvents.Name = "groupBoxEvents";
            this.groupBoxEvents.Size = new System.Drawing.Size(335, 211);
            this.groupBoxEvents.TabIndex = 5;
            this.groupBoxEvents.TabStop = false;
            this.groupBoxEvents.Text = "Events";
            // 
            // listBoxEvents
            // 
            this.listBoxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxEvents.FormattingEnabled = true;
            this.listBoxEvents.IntegralHeight = false;
            this.listBoxEvents.Location = new System.Drawing.Point(6, 19);
            this.listBoxEvents.Name = "listBoxEvents";
            this.listBoxEvents.Size = new System.Drawing.Size(323, 186);
            this.listBoxEvents.TabIndex = 0;
            this.listBoxEvents.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxEvents_MouseDoubleClick);
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
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBoxEvents);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(349, 618);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Items & Events";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.groupBox2.Location = new System.Drawing.Point(6, 110);
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
            this.buttonCancelEdit.Click += new System.EventHandler(this.buttonCancelEdit_Click);
            // 
            // buttonBeginEdit
            // 
            this.buttonBeginEdit.Location = new System.Drawing.Point(17, 97);
            this.buttonBeginEdit.Name = "buttonBeginEdit";
            this.buttonBeginEdit.Size = new System.Drawing.Size(134, 20);
            this.buttonBeginEdit.TabIndex = 7;
            this.buttonBeginEdit.Text = "Begin edit";
            this.buttonBeginEdit.UseVisualStyleBackColor = true;
            this.buttonBeginEdit.Click += new System.EventHandler(this.buttonBeginEdit_Click);
            // 
            // buttonEnsureCellVisibility
            // 
            this.buttonEnsureCellVisibility.Location = new System.Drawing.Point(157, 71);
            this.buttonEnsureCellVisibility.Name = "buttonEnsureCellVisibility";
            this.buttonEnsureCellVisibility.Size = new System.Drawing.Size(134, 20);
            this.buttonEnsureCellVisibility.TabIndex = 6;
            this.buttonEnsureCellVisibility.Text = "Ensure cell visibility";
            this.buttonEnsureCellVisibility.UseVisualStyleBackColor = true;
            this.buttonEnsureCellVisibility.Click += new System.EventHandler(this.buttonEnsureCellVisibility_Click);
            // 
            // buttonEnsureColumnVisibility
            // 
            this.buttonEnsureColumnVisibility.Location = new System.Drawing.Point(157, 45);
            this.buttonEnsureColumnVisibility.Name = "buttonEnsureColumnVisibility";
            this.buttonEnsureColumnVisibility.Size = new System.Drawing.Size(134, 20);
            this.buttonEnsureColumnVisibility.TabIndex = 5;
            this.buttonEnsureColumnVisibility.Text = "Ensure column visibility";
            this.buttonEnsureColumnVisibility.UseVisualStyleBackColor = true;
            this.buttonEnsureColumnVisibility.Click += new System.EventHandler(this.buttonEnsureColumnVisibility_Click);
            // 
            // buttonEnsureItemVisibility
            // 
            this.buttonEnsureItemVisibility.Location = new System.Drawing.Point(157, 19);
            this.buttonEnsureItemVisibility.Name = "buttonEnsureItemVisibility";
            this.buttonEnsureItemVisibility.Size = new System.Drawing.Size(134, 20);
            this.buttonEnsureItemVisibility.TabIndex = 4;
            this.buttonEnsureItemVisibility.Text = "Ensure item visibility";
            this.buttonEnsureItemVisibility.UseVisualStyleBackColor = true;
            this.buttonEnsureItemVisibility.Click += new System.EventHandler(this.buttonEnsureItemVisibility_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Item index";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Column index";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownItemIndex2
            // 
            this.numericUpDownItemIndex2.Location = new System.Drawing.Point(87, 45);
            this.numericUpDownItemIndex2.Name = "numericUpDownItemIndex2";
            this.numericUpDownItemIndex2.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownItemIndex2.TabIndex = 1;
            // 
            // numericUpDownColumnIndex
            // 
            this.numericUpDownColumnIndex.Location = new System.Drawing.Point(87, 19);
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
            this.dList1.HeaderClicked += new System.EventHandler<InCoding.DList.HeaderClickEventArgs>(this.dList1_HeaderClicked);
            this.dList1.CellClicked += new System.EventHandler<InCoding.DList.CellClickEventArgs>(this.dList1_CellClicked);
            this.dList1.SelectedItemsChanged += new System.EventHandler(this.dList1_SelectedItemsChanged);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 644);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemIndex)).EndInit();
            this.groupBoxEvents.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBoxEvents;
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
    }
}

