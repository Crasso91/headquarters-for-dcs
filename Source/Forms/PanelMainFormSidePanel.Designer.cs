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
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.TemplateSettingsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.NodesTabPage = new System.Windows.Forms.TabPage();
            this.NodesTreeView = new System.Windows.Forms.TreeView();
            this.SidePanelimageList = new System.Windows.Forms.ImageList(this.components);
            this.MainTabControl.SuspendLayout();
            this.SettingsTabPage.SuspendLayout();
            this.NodesTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.SettingsTabPage);
            this.MainTabControl.Controls.Add(this.NodesTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(384, 561);
            this.MainTabControl.TabIndex = 1;
            // 
            // SettingsTabPage
            // 
            this.SettingsTabPage.Controls.Add(this.TemplateSettingsPropertyGrid);
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTabPage.Size = new System.Drawing.Size(376, 535);
            this.SettingsTabPage.TabIndex = 0;
            this.SettingsTabPage.Text = "SettingsTabPage";
            this.SettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // TemplateSettingsPropertyGrid
            // 
            this.TemplateSettingsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TemplateSettingsPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.TemplateSettingsPropertyGrid.Name = "TemplateSettingsPropertyGrid";
            this.TemplateSettingsPropertyGrid.Size = new System.Drawing.Size(370, 529);
            this.TemplateSettingsPropertyGrid.TabIndex = 0;
            // 
            // NodesTabPage
            // 
            this.NodesTabPage.Controls.Add(this.NodesTreeView);
            this.NodesTabPage.Location = new System.Drawing.Point(4, 22);
            this.NodesTabPage.Name = "NodesTabPage";
            this.NodesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.NodesTabPage.Size = new System.Drawing.Size(376, 535);
            this.NodesTabPage.TabIndex = 1;
            this.NodesTabPage.Text = "NodesTabPage";
            this.NodesTabPage.UseVisualStyleBackColor = true;
            // 
            // NodesTreeView
            // 
            this.NodesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NodesTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NodesTreeView.FullRowSelect = true;
            this.NodesTreeView.HideSelection = false;
            this.NodesTreeView.ImageIndex = 0;
            this.NodesTreeView.ImageList = this.SidePanelimageList;
            this.NodesTreeView.Location = new System.Drawing.Point(3, 3);
            this.NodesTreeView.Name = "NodesTreeView";
            this.NodesTreeView.SelectedImageIndex = 0;
            this.NodesTreeView.ShowLines = false;
            this.NodesTreeView.ShowPlusMinus = false;
            this.NodesTreeView.ShowRootLines = false;
            this.NodesTreeView.Size = new System.Drawing.Size(370, 529);
            this.NodesTreeView.TabIndex = 1;
            this.NodesTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodesTreeViewNodeMouseClick);
            this.NodesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodesTreeViewNodeMouseDoubleClick);
            this.NodesTreeView.MouseEnter += new System.EventHandler(this.SidePanelMouseEnter);
            // 
            // SidePanelimageList
            // 
            this.SidePanelimageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.SidePanelimageList.ImageSize = new System.Drawing.Size(16, 16);
            this.SidePanelimageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // PanelMainFormSidePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Controls.Add(this.MainTabControl);
            this.Name = "PanelMainFormSidePanel";
            this.Text = "PanelMainFormSidePanel";
            this.Load += new System.EventHandler(this.FormLoad);
            this.MouseEnter += new System.EventHandler(this.SidePanelMouseEnter);
            this.MainTabControl.ResumeLayout(false);
            this.SettingsTabPage.ResumeLayout(false);
            this.NodesTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage SettingsTabPage;
        private System.Windows.Forms.TabPage NodesTabPage;
        private System.Windows.Forms.PropertyGrid TemplateSettingsPropertyGrid;
        private System.Windows.Forms.TreeView NodesTreeView;
        private System.Windows.Forms.ImageList SidePanelimageList;
    }
}