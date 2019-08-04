namespace Headquarters4DCS.Forms
{
    partial class PanelNodePlayerFlightGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelNodePlayerFlightGroups));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddGroupToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveGroupToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FlightGroupsDataGridView = new System.Windows.Forms.DataGridView();
            this.DGVColumnAircraftType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DGVColumnCount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DGVColumnTask = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DGVColumnAIWingmen = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DGVColumnStartLocation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStrip1.SuspendLayout();
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
            this.FlightGroupsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlightGroupsDataGridView.Location = new System.Drawing.Point(0, 25);
            this.FlightGroupsDataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.FlightGroupsDataGridView.Name = "FlightGroupsDataGridView";
            this.FlightGroupsDataGridView.RowHeadersVisible = false;
            this.FlightGroupsDataGridView.RowTemplate.Height = 24;
            this.FlightGroupsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FlightGroupsDataGridView.Size = new System.Drawing.Size(624, 416);
            this.FlightGroupsDataGridView.TabIndex = 5;
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
            // PanelNodePlayerFlightGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.FlightGroupsDataGridView);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 384);
            this.Name = "PanelNodePlayerFlightGroups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Player flight groups";
            this.Load += new System.EventHandler(this.FormPlayerFlightGroups_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlightGroupsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AddGroupToolStripButton;
        private System.Windows.Forms.ToolStripButton RemoveGroupToolStripButton;
        private System.Windows.Forms.DataGridView FlightGroupsDataGridView;
        private System.Windows.Forms.DataGridViewComboBoxColumn DGVColumnAircraftType;
        private System.Windows.Forms.DataGridViewComboBoxColumn DGVColumnCount;
        private System.Windows.Forms.DataGridViewComboBoxColumn DGVColumnTask;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DGVColumnAIWingmen;
        private System.Windows.Forms.DataGridViewComboBoxColumn DGVColumnStartLocation;
    }
}