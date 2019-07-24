namespace Headquarters4DCS.Forms
{
    partial class FormPlayerFlightGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlayerFlightGroups));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddGroupToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveGroupToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.OkButton = new System.Windows.Forms.Button();
            this.FlightGroupsDataGridView = new System.Windows.Forms.DataGridView();
            this.CancelChangesButton = new System.Windows.Forms.Button();
            this.DGVColumnAircraftType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DGVColumnCount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DGVColumnTask = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DGVColumnAIWingmen = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DGVColumnStartLocation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlightGroupsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddGroupToolStripButton,
            this.RemoveGroupToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AddGroupToolStripButton
            // 
            this.AddGroupToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddGroupToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("AddGroupToolStripButton.Image")));
            this.AddGroupToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddGroupToolStripButton.Name = "AddGroupToolStripButton";
            this.AddGroupToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.AddGroupToolStripButton.Text = "toolStripButton1";
            this.AddGroupToolStripButton.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // RemoveGroupToolStripButton
            // 
            this.RemoveGroupToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveGroupToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveGroupToolStripButton.Image")));
            this.RemoveGroupToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveGroupToolStripButton.Name = "RemoveGroupToolStripButton";
            this.RemoveGroupToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.RemoveGroupToolStripButton.Text = "toolStripButton2";
            this.RemoveGroupToolStripButton.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.MainTableLayoutPanel.Controls.Add(this.OkButton, 1, 1);
            this.MainTableLayoutPanel.Controls.Add(this.FlightGroupsDataGridView, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.CancelChangesButton, 2, 1);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 25);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(624, 416);
            this.MainTableLayoutPanel.TabIndex = 2;
            // 
            // OkButton
            // 
            this.OkButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OkButton.Location = new System.Drawing.Point(347, 379);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(134, 34);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "&Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // FlightGroupsDataGridView
            // 
            this.FlightGroupsDataGridView.AllowUserToAddRows = false;
            this.FlightGroupsDataGridView.AllowUserToDeleteRows = false;
            this.FlightGroupsDataGridView.AllowUserToResizeColumns = false;
            this.FlightGroupsDataGridView.AllowUserToResizeRows = false;
            this.FlightGroupsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FlightGroupsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FlightGroupsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DGVColumnAircraftType,
            this.DGVColumnCount,
            this.DGVColumnTask,
            this.DGVColumnAIWingmen,
            this.DGVColumnStartLocation});
            this.MainTableLayoutPanel.SetColumnSpan(this.FlightGroupsDataGridView, 3);
            this.FlightGroupsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlightGroupsDataGridView.Location = new System.Drawing.Point(2, 2);
            this.FlightGroupsDataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.FlightGroupsDataGridView.Name = "FlightGroupsDataGridView";
            this.FlightGroupsDataGridView.RowHeadersVisible = false;
            this.FlightGroupsDataGridView.RowTemplate.Height = 24;
            this.FlightGroupsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FlightGroupsDataGridView.Size = new System.Drawing.Size(620, 372);
            this.FlightGroupsDataGridView.TabIndex = 4;
            // 
            // CancelChangesButton
            // 
            this.CancelChangesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancelChangesButton.Location = new System.Drawing.Point(487, 379);
            this.CancelChangesButton.Name = "CancelChangesButton";
            this.CancelChangesButton.Size = new System.Drawing.Size(134, 34);
            this.CancelChangesButton.TabIndex = 5;
            this.CancelChangesButton.Text = "&Cancel";
            this.CancelChangesButton.UseVisualStyleBackColor = true;
            this.CancelChangesButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // DGVColumnAircraftType
            // 
            this.DGVColumnAircraftType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DGVColumnAircraftType.HeaderText = "Aircraft type";
            this.DGVColumnAircraftType.Name = "DGVColumnAircraftType";
            // 
            // DGVColumnCount
            // 
            this.DGVColumnCount.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DGVColumnCount.FillWeight = 50F;
            this.DGVColumnCount.HeaderText = "Count";
            this.DGVColumnCount.Name = "DGVColumnCount";
            // 
            // DGVColumnTask
            // 
            this.DGVColumnTask.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DGVColumnTask.HeaderText = "Task";
            this.DGVColumnTask.Name = "DGVColumnTask";
            // 
            // DGVColumnAIWingmen
            // 
            this.DGVColumnAIWingmen.FillWeight = 50F;
            this.DGVColumnAIWingmen.HeaderText = "AI Wingmen";
            this.DGVColumnAIWingmen.Name = "DGVColumnAIWingmen";
            // 
            // DGVColumnStartLocation
            // 
            this.DGVColumnStartLocation.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DGVColumnStartLocation.HeaderText = "Start location";
            this.DGVColumnStartLocation.Name = "DGVColumnStartLocation";
            // 
            // FormPlayerFlightGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 384);
            this.Name = "FormPlayerFlightGroups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Player flight groups";
            this.Load += new System.EventHandler(this.FormPlayerFlightGroups_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MainTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlightGroupsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AddGroupToolStripButton;
        private System.Windows.Forms.ToolStripButton RemoveGroupToolStripButton;
        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.DataGridView FlightGroupsDataGridView;
        private System.Windows.Forms.Button CancelChangesButton;
        private System.Windows.Forms.DataGridViewComboBoxColumn DGVColumnAircraftType;
        private System.Windows.Forms.DataGridViewComboBoxColumn DGVColumnCount;
        private System.Windows.Forms.DataGridViewComboBoxColumn DGVColumnTask;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DGVColumnAIWingmen;
        private System.Windows.Forms.DataGridViewComboBoxColumn DGVColumnStartLocation;
    }
}