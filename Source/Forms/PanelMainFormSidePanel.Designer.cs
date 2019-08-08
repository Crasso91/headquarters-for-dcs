namespace Headquarters4DCS.Forms
{
    partial class PanelMainFormSidePanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelMainFormSidePanel));
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.MissionTabPage = new System.Windows.Forms.TabPage();
            this.TemplateSettingsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.LocationsTabPage = new System.Windows.Forms.TabPage();
            this.LocationsTreeView = new System.Windows.Forms.TreeView();
            this.SidePanelImageList = new System.Windows.Forms.ImageList(this.components);
            this.LocationsToolStrip = new System.Windows.Forms.ToolStrip();
            this.CollapseAllLocationsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ExpandAllLocationsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.LocationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MainTabControl.SuspendLayout();
            this.MissionTabPage.SuspendLayout();
            this.LocationsTabPage.SuspendLayout();
            this.LocationsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.MissionTabPage);
            this.MainTabControl.Controls.Add(this.LocationsTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(384, 561);
            this.MainTabControl.TabIndex = 1;
            // 
            // MissionTabPage
            // 
            this.MissionTabPage.Controls.Add(this.TemplateSettingsPropertyGrid);
            this.MissionTabPage.Location = new System.Drawing.Point(4, 22);
            this.MissionTabPage.Name = "MissionTabPage";
            this.MissionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MissionTabPage.Size = new System.Drawing.Size(376, 535);
            this.MissionTabPage.TabIndex = 0;
            this.MissionTabPage.Text = "Mission";
            this.MissionTabPage.UseVisualStyleBackColor = true;
            // 
            // TemplateSettingsPropertyGrid
            // 
            this.TemplateSettingsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TemplateSettingsPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.TemplateSettingsPropertyGrid.Name = "TemplateSettingsPropertyGrid";
            this.TemplateSettingsPropertyGrid.Size = new System.Drawing.Size(370, 529);
            this.TemplateSettingsPropertyGrid.TabIndex = 0;
            this.TemplateSettingsPropertyGrid.Click += new System.EventHandler(this.TemplateSettingsPropertyGrid_Click);
            // 
            // LocationsTabPage
            // 
            this.LocationsTabPage.Controls.Add(this.LocationsTreeView);
            this.LocationsTabPage.Controls.Add(this.LocationsToolStrip);
            this.LocationsTabPage.Location = new System.Drawing.Point(4, 22);
            this.LocationsTabPage.Name = "LocationsTabPage";
            this.LocationsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LocationsTabPage.Size = new System.Drawing.Size(376, 535);
            this.LocationsTabPage.TabIndex = 1;
            this.LocationsTabPage.Text = "Locations";
            this.LocationsTabPage.UseVisualStyleBackColor = true;
            // 
            // LocationsTreeView
            // 
            this.LocationsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationsTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationsTreeView.FullRowSelect = true;
            this.LocationsTreeView.HideSelection = false;
            this.LocationsTreeView.ImageIndex = 0;
            this.LocationsTreeView.ImageList = this.SidePanelImageList;
            this.LocationsTreeView.Location = new System.Drawing.Point(3, 28);
            this.LocationsTreeView.Name = "LocationsTreeView";
            this.LocationsTreeView.SelectedImageIndex = 0;
            this.LocationsTreeView.ShowNodeToolTips = true;
            this.LocationsTreeView.Size = new System.Drawing.Size(370, 504);
            this.LocationsTreeView.TabIndex = 2;
            this.LocationsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Event_LocationsTreeView_AfterSelect);
            this.LocationsTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Event_LocationsTreeView_NodeMouseClick);
            // 
            // SidePanelImageList
            // 
            this.SidePanelImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.SidePanelImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.SidePanelImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // LocationsToolStrip
            // 
            this.LocationsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.LocationsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CollapseAllLocationsToolStripButton,
            this.ExpandAllLocationsToolStripButton});
            this.LocationsToolStrip.Location = new System.Drawing.Point(3, 3);
            this.LocationsToolStrip.Name = "LocationsToolStrip";
            this.LocationsToolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LocationsToolStrip.Size = new System.Drawing.Size(370, 25);
            this.LocationsToolStrip.TabIndex = 0;
            this.LocationsToolStrip.Text = "toolStrip1";
            // 
            // CollapseAllLocationsToolStripButton
            // 
            this.CollapseAllLocationsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CollapseAllLocationsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CollapseAllLocationsToolStripButton.Image")));
            this.CollapseAllLocationsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CollapseAllLocationsToolStripButton.Name = "CollapseAllLocationsToolStripButton";
            this.CollapseAllLocationsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CollapseAllLocationsToolStripButton.Text = "Collapse all";
            this.CollapseAllLocationsToolStripButton.Click += new System.EventHandler(this.Event_LocationsToolStripButtons_Click);
            // 
            // ExpandAllLocationsToolStripButton
            // 
            this.ExpandAllLocationsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExpandAllLocationsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ExpandAllLocationsToolStripButton.Image")));
            this.ExpandAllLocationsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExpandAllLocationsToolStripButton.Name = "ExpandAllLocationsToolStripButton";
            this.ExpandAllLocationsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ExpandAllLocationsToolStripButton.Text = "Expand all";
            this.ExpandAllLocationsToolStripButton.Click += new System.EventHandler(this.Event_LocationsToolStripButtons_Click);
            // 
            // LocationContextMenuStrip
            // 
            this.LocationContextMenuStrip.Name = "LoctationContextMenuStrip";
            this.LocationContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // PanelMainFormSidePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Controls.Add(this.MainTabControl);
            this.Name = "PanelMainFormSidePanel";
            this.Text = "Side panel";
            this.Load += new System.EventHandler(this.Event_Form_Load);
            this.MainTabControl.ResumeLayout(false);
            this.MissionTabPage.ResumeLayout(false);
            this.LocationsTabPage.ResumeLayout(false);
            this.LocationsTabPage.PerformLayout();
            this.LocationsToolStrip.ResumeLayout(false);
            this.LocationsToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage MissionTabPage;
        private System.Windows.Forms.TabPage LocationsTabPage;
        private System.Windows.Forms.PropertyGrid TemplateSettingsPropertyGrid;
        private System.Windows.Forms.ImageList SidePanelImageList;
        private System.Windows.Forms.ContextMenuStrip LocationContextMenuStrip;
        private System.Windows.Forms.TreeView LocationsTreeView;
        private System.Windows.Forms.ToolStrip LocationsToolStrip;
        private System.Windows.Forms.ToolStripButton ExpandAllLocationsToolStripButton;
        private System.Windows.Forms.ToolStripButton CollapseAllLocationsToolStripButton;
    }
}