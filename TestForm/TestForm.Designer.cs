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
            this.groupBoxItems = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveColumn = new System.Windows.Forms.Button();
            this.buttonAddColumn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAddRngItem = new System.Windows.Forms.Button();
            this.buttonRemoveItem = new System.Windows.Forms.Button();
            this.groupBoxEvents = new System.Windows.Forms.GroupBox();
            this.listBoxEvents = new System.Windows.Forms.ListBox();
            this.dList1 = new InCoding.DList.DList();
            this.groupBoxItems.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxEvents.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxItems
            // 
            this.groupBoxItems.Controls.Add(this.groupBox2);
            this.groupBoxItems.Controls.Add(this.groupBox1);
            this.groupBoxItems.Location = new System.Drawing.Point(12, 12);
            this.groupBoxItems.Name = "groupBoxItems";
            this.groupBoxItems.Size = new System.Drawing.Size(458, 336);
            this.groupBoxItems.TabIndex = 4;
            this.groupBoxItems.TabStop = false;
            this.groupBoxItems.Text = "Data";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonRemoveColumn);
            this.groupBox2.Controls.Add(this.buttonAddColumn);
            this.groupBox2.Location = new System.Drawing.Point(6, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 91);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Columns";
            // 
            // buttonRemoveColumn
            // 
            this.buttonRemoveColumn.Location = new System.Drawing.Point(124, 19);
            this.buttonRemoveColumn.Name = "buttonRemoveColumn";
            this.buttonRemoveColumn.Size = new System.Drawing.Size(112, 36);
            this.buttonRemoveColumn.TabIndex = 2;
            this.buttonRemoveColumn.Text = "Remove";
            this.buttonRemoveColumn.UseVisualStyleBackColor = true;
            // 
            // buttonAddColumn
            // 
            this.buttonAddColumn.Location = new System.Drawing.Point(6, 19);
            this.buttonAddColumn.Name = "buttonAddColumn";
            this.buttonAddColumn.Size = new System.Drawing.Size(112, 36);
            this.buttonAddColumn.TabIndex = 1;
            this.buttonAddColumn.Text = "Add";
            this.buttonAddColumn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonAddRngItem);
            this.groupBox1.Controls.Add(this.buttonRemoveItem);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 62);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items";
            // 
            // buttonAddRngItem
            // 
            this.buttonAddRngItem.Location = new System.Drawing.Point(6, 19);
            this.buttonAddRngItem.Name = "buttonAddRngItem";
            this.buttonAddRngItem.Size = new System.Drawing.Size(112, 36);
            this.buttonAddRngItem.TabIndex = 0;
            this.buttonAddRngItem.Text = "Add";
            this.buttonAddRngItem.UseVisualStyleBackColor = true;
            this.buttonAddRngItem.Click += new System.EventHandler(this.buttonAddRngItem_Click);
            // 
            // buttonRemoveItem
            // 
            this.buttonRemoveItem.Location = new System.Drawing.Point(124, 19);
            this.buttonRemoveItem.Name = "buttonRemoveItem";
            this.buttonRemoveItem.Size = new System.Drawing.Size(112, 36);
            this.buttonRemoveItem.TabIndex = 1;
            this.buttonRemoveItem.Text = "Remove";
            this.buttonRemoveItem.UseVisualStyleBackColor = true;
            this.buttonRemoveItem.Click += new System.EventHandler(this.buttonRemoveItem_Click);
            // 
            // groupBoxEvents
            // 
            this.groupBoxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxEvents.Controls.Add(this.listBoxEvents);
            this.groupBoxEvents.Location = new System.Drawing.Point(12, 354);
            this.groupBoxEvents.Name = "groupBoxEvents";
            this.groupBoxEvents.Size = new System.Drawing.Size(458, 278);
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
            this.listBoxEvents.Size = new System.Drawing.Size(446, 253);
            this.listBoxEvents.TabIndex = 0;
            // 
            // dList1
            // 
            this.dList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dList1.HighlightTextColor = System.Drawing.SystemColors.HighlightText;
            this.dList1.HotItemColor = System.Drawing.SystemColors.HotTrack;
            this.dList1.Location = new System.Drawing.Point(476, 12);
            this.dList1.Name = "dList1";
            this.dList1.SelectedItemColor = System.Drawing.SystemColors.Highlight;
            this.dList1.SelectionRectangleColor = System.Drawing.SystemColors.HotTrack;
            this.dList1.Size = new System.Drawing.Size(479, 278);
            this.dList1.TabIndex = 3;
            this.dList1.Text = "dList1";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 644);
            this.Controls.Add(this.groupBoxEvents);
            this.Controls.Add(this.groupBoxItems);
            this.Controls.Add(this.dList1);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.groupBoxItems.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxEvents.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DList.DList dList1;
        private System.Windows.Forms.GroupBox groupBoxItems;
        private System.Windows.Forms.Button buttonRemoveItem;
        private System.Windows.Forms.Button buttonAddRngItem;
        private System.Windows.Forms.GroupBox groupBoxEvents;
        private System.Windows.Forms.ListBox listBoxEvents;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRemoveColumn;
        private System.Windows.Forms.Button buttonAddColumn;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

