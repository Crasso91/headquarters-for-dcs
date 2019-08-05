namespace Headquarters4DCS.Forms
{
    partial class PanelLocationEditorFeatures
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
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SelectedFeaturesGroupBox = new System.Windows.Forms.GroupBox();
            this.AvailableFeaturesGroupBox = new System.Windows.Forms.GroupBox();
            this.AvailableFeaturesTreeView = new System.Windows.Forms.TreeView();
            this.SelectedFeaturesTreeView = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.SelectedFeaturesGroupBox.SuspendLayout();
            this.AvailableFeaturesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.SelectedFeaturesGroupBox);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.AvailableFeaturesGroupBox);
            this.MainSplitContainer.Size = new System.Drawing.Size(624, 441);
            this.MainSplitContainer.SplitterDistance = 310;
            this.MainSplitContainer.TabIndex = 5;
            // 
            // SelectedFeaturesGroupBox
            // 
            this.SelectedFeaturesGroupBox.Controls.Add(this.SelectedFeaturesTreeView);
            this.SelectedFeaturesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedFeaturesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.SelectedFeaturesGroupBox.Name = "SelectedFeaturesGroupBox";
            this.SelectedFeaturesGroupBox.Size = new System.Drawing.Size(310, 441);
            this.SelectedFeaturesGroupBox.TabIndex = 1;
            this.SelectedFeaturesGroupBox.TabStop = false;
            this.SelectedFeaturesGroupBox.Text = "Selected features";
            // 
            // AvailableFeaturesGroupBox
            // 
            this.AvailableFeaturesGroupBox.Controls.Add(this.AvailableFeaturesTreeView);
            this.AvailableFeaturesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableFeaturesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.AvailableFeaturesGroupBox.Name = "AvailableFeaturesGroupBox";
            this.AvailableFeaturesGroupBox.Size = new System.Drawing.Size(310, 441);
            this.AvailableFeaturesGroupBox.TabIndex = 0;
            this.AvailableFeaturesGroupBox.TabStop = false;
            this.AvailableFeaturesGroupBox.Text = "Available features";
            // 
            // AvailableFeaturesTreeView
            // 
            this.AvailableFeaturesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableFeaturesTreeView.FullRowSelect = true;
            this.AvailableFeaturesTreeView.HideSelection = false;
            this.AvailableFeaturesTreeView.Location = new System.Drawing.Point(3, 16);
            this.AvailableFeaturesTreeView.Name = "AvailableFeaturesTreeView";
            this.AvailableFeaturesTreeView.ShowNodeToolTips = true;
            this.AvailableFeaturesTreeView.Size = new System.Drawing.Size(304, 422);
            this.AvailableFeaturesTreeView.TabIndex = 1;
            this.AvailableFeaturesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.AvailableFeaturesTreeViewNodeMouseDoubleClick);
            // 
            // SelectedFeaturesTreeView
            // 
            this.SelectedFeaturesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedFeaturesTreeView.FullRowSelect = true;
            this.SelectedFeaturesTreeView.HideSelection = false;
            this.SelectedFeaturesTreeView.Location = new System.Drawing.Point(3, 16);
            this.SelectedFeaturesTreeView.Name = "SelectedFeaturesTreeView";
            this.SelectedFeaturesTreeView.ShowLines = false;
            this.SelectedFeaturesTreeView.ShowNodeToolTips = true;
            this.SelectedFeaturesTreeView.ShowPlusMinus = false;
            this.SelectedFeaturesTreeView.ShowRootLines = false;
            this.SelectedFeaturesTreeView.Size = new System.Drawing.Size(304, 422);
            this.SelectedFeaturesTreeView.TabIndex = 1;
            this.SelectedFeaturesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SelectedFeaturesTreeViewNodeMouseDoubleClick);
            // 
            // PanelNodeEditorFeatures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.MainSplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 384);
            this.Name = "PanelNodeEditorFeatures";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIEditorFormNodeFeatures";
            this.Load += new System.EventHandler(this.PanelNodeEditorFeatures_Load);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.SelectedFeaturesGroupBox.ResumeLayout(false);
            this.AvailableFeaturesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.GroupBox SelectedFeaturesGroupBox;
        private System.Windows.Forms.GroupBox AvailableFeaturesGroupBox;
        private System.Windows.Forms.TreeView SelectedFeaturesTreeView;
        private System.Windows.Forms.TreeView AvailableFeaturesTreeView;
    }
}