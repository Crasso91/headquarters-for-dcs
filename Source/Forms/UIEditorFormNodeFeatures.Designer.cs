namespace Headquarters4DCS.Forms
{
    partial class UIEditorFormNodeFeatures
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
            this.CancelChangesButton = new System.Windows.Forms.Button();
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SelectedFeaturesGroupBox = new System.Windows.Forms.GroupBox();
            this.SelectedFeaturesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SelectedFeaturesTreeView = new System.Windows.Forms.TreeView();
            this.SelectedFeaturesInfoLabel = new System.Windows.Forms.Label();
            this.AvailableFeaturesGroupBox = new System.Windows.Forms.GroupBox();
            this.AvailableFeaturesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.AvailableFeaturesTreeView = new System.Windows.Forms.TreeView();
            this.AvailableFeaturesInfoLabel = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.SelectedFeaturesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedFeaturesSplitContainer)).BeginInit();
            this.SelectedFeaturesSplitContainer.Panel1.SuspendLayout();
            this.SelectedFeaturesSplitContainer.Panel2.SuspendLayout();
            this.SelectedFeaturesSplitContainer.SuspendLayout();
            this.AvailableFeaturesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvailableFeaturesSplitContainer)).BeginInit();
            this.AvailableFeaturesSplitContainer.Panel1.SuspendLayout();
            this.AvailableFeaturesSplitContainer.Panel2.SuspendLayout();
            this.AvailableFeaturesSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelChangesButton
            // 
            this.CancelChangesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancelChangesButton.Location = new System.Drawing.Point(495, 404);
            this.CancelChangesButton.Name = "CancelChangesButton";
            this.CancelChangesButton.Size = new System.Drawing.Size(126, 34);
            this.CancelChangesButton.TabIndex = 3;
            this.CancelChangesButton.Text = "&Cancel";
            this.CancelChangesButton.UseVisualStyleBackColor = true;
            this.CancelChangesButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.Controls.Add(this.CancelChangesButton, 2, 1);
            this.MainTableLayoutPanel.Controls.Add(this.MainSplitContainer, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.OkButton, 1, 1);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(624, 441);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // MainSplitContainer
            // 
            this.MainTableLayoutPanel.SetColumnSpan(this.MainSplitContainer, 3);
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.SelectedFeaturesGroupBox);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.AvailableFeaturesGroupBox);
            this.MainSplitContainer.Size = new System.Drawing.Size(618, 395);
            this.MainSplitContainer.SplitterDistance = 308;
            this.MainSplitContainer.TabIndex = 4;
            // 
            // SelectedFeaturesGroupBox
            // 
            this.SelectedFeaturesGroupBox.Controls.Add(this.SelectedFeaturesSplitContainer);
            this.SelectedFeaturesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedFeaturesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.SelectedFeaturesGroupBox.Name = "SelectedFeaturesGroupBox";
            this.SelectedFeaturesGroupBox.Size = new System.Drawing.Size(308, 395);
            this.SelectedFeaturesGroupBox.TabIndex = 1;
            this.SelectedFeaturesGroupBox.TabStop = false;
            this.SelectedFeaturesGroupBox.Text = "Selected features";
            // 
            // SelectedFeaturesSplitContainer
            // 
            this.SelectedFeaturesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedFeaturesSplitContainer.Location = new System.Drawing.Point(3, 16);
            this.SelectedFeaturesSplitContainer.Name = "SelectedFeaturesSplitContainer";
            this.SelectedFeaturesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SelectedFeaturesSplitContainer.Panel1
            // 
            this.SelectedFeaturesSplitContainer.Panel1.Controls.Add(this.SelectedFeaturesTreeView);
            // 
            // SelectedFeaturesSplitContainer.Panel2
            // 
            this.SelectedFeaturesSplitContainer.Panel2.Controls.Add(this.SelectedFeaturesInfoLabel);
            this.SelectedFeaturesSplitContainer.Size = new System.Drawing.Size(302, 376);
            this.SelectedFeaturesSplitContainer.SplitterDistance = 241;
            this.SelectedFeaturesSplitContainer.TabIndex = 0;
            // 
            // SelectedFeaturesTreeView
            // 
            this.SelectedFeaturesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedFeaturesTreeView.Location = new System.Drawing.Point(0, 0);
            this.SelectedFeaturesTreeView.Name = "SelectedFeaturesTreeView";
            this.SelectedFeaturesTreeView.ShowLines = false;
            this.SelectedFeaturesTreeView.ShowPlusMinus = false;
            this.SelectedFeaturesTreeView.ShowRootLines = false;
            this.SelectedFeaturesTreeView.Size = new System.Drawing.Size(302, 241);
            this.SelectedFeaturesTreeView.TabIndex = 0;
            this.SelectedFeaturesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SelectedFeaturesTreeViewNodeMouseDoubleClick);
            // 
            // SelectedFeaturesInfoLabel
            // 
            this.SelectedFeaturesInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedFeaturesInfoLabel.Location = new System.Drawing.Point(0, 0);
            this.SelectedFeaturesInfoLabel.Margin = new System.Windows.Forms.Padding(3);
            this.SelectedFeaturesInfoLabel.Name = "SelectedFeaturesInfoLabel";
            this.SelectedFeaturesInfoLabel.Size = new System.Drawing.Size(302, 131);
            this.SelectedFeaturesInfoLabel.TabIndex = 0;
            this.SelectedFeaturesInfoLabel.Text = "label2";
            // 
            // AvailableFeaturesGroupBox
            // 
            this.AvailableFeaturesGroupBox.Controls.Add(this.AvailableFeaturesSplitContainer);
            this.AvailableFeaturesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableFeaturesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.AvailableFeaturesGroupBox.Name = "AvailableFeaturesGroupBox";
            this.AvailableFeaturesGroupBox.Size = new System.Drawing.Size(306, 395);
            this.AvailableFeaturesGroupBox.TabIndex = 0;
            this.AvailableFeaturesGroupBox.TabStop = false;
            this.AvailableFeaturesGroupBox.Text = "Available features";
            // 
            // AvailableFeaturesSplitContainer
            // 
            this.AvailableFeaturesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableFeaturesSplitContainer.Location = new System.Drawing.Point(3, 16);
            this.AvailableFeaturesSplitContainer.Name = "AvailableFeaturesSplitContainer";
            this.AvailableFeaturesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // AvailableFeaturesSplitContainer.Panel1
            // 
            this.AvailableFeaturesSplitContainer.Panel1.Controls.Add(this.AvailableFeaturesTreeView);
            // 
            // AvailableFeaturesSplitContainer.Panel2
            // 
            this.AvailableFeaturesSplitContainer.Panel2.Controls.Add(this.AvailableFeaturesInfoLabel);
            this.AvailableFeaturesSplitContainer.Size = new System.Drawing.Size(300, 376);
            this.AvailableFeaturesSplitContainer.SplitterDistance = 241;
            this.AvailableFeaturesSplitContainer.TabIndex = 0;
            // 
            // AvailableFeaturesTreeView
            // 
            this.AvailableFeaturesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableFeaturesTreeView.Location = new System.Drawing.Point(0, 0);
            this.AvailableFeaturesTreeView.Name = "AvailableFeaturesTreeView";
            this.AvailableFeaturesTreeView.ShowLines = false;
            this.AvailableFeaturesTreeView.ShowPlusMinus = false;
            this.AvailableFeaturesTreeView.ShowRootLines = false;
            this.AvailableFeaturesTreeView.Size = new System.Drawing.Size(300, 241);
            this.AvailableFeaturesTreeView.TabIndex = 0;
            this.AvailableFeaturesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.AvailableFeaturesTreeViewNodeMouseDoubleClick);
            // 
            // AvailableFeaturesInfoLabel
            // 
            this.AvailableFeaturesInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableFeaturesInfoLabel.Location = new System.Drawing.Point(0, 0);
            this.AvailableFeaturesInfoLabel.Margin = new System.Windows.Forms.Padding(3);
            this.AvailableFeaturesInfoLabel.Name = "AvailableFeaturesInfoLabel";
            this.AvailableFeaturesInfoLabel.Size = new System.Drawing.Size(300, 131);
            this.AvailableFeaturesInfoLabel.TabIndex = 0;
            this.AvailableFeaturesInfoLabel.Text = "label1";
            // 
            // OkButton
            // 
            this.OkButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OkButton.Location = new System.Drawing.Point(363, 404);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(126, 34);
            this.OkButton.TabIndex = 5;
            this.OkButton.Text = "&Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // UIEditorFormNodeFeatures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 384);
            this.Name = "UIEditorFormNodeFeatures";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIEditorFormNodeFeatures";
            this.Load += new System.EventHandler(this.UIEditorMultipleDefinitions_Load);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.SelectedFeaturesGroupBox.ResumeLayout(false);
            this.SelectedFeaturesSplitContainer.Panel1.ResumeLayout(false);
            this.SelectedFeaturesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SelectedFeaturesSplitContainer)).EndInit();
            this.SelectedFeaturesSplitContainer.ResumeLayout(false);
            this.AvailableFeaturesGroupBox.ResumeLayout(false);
            this.AvailableFeaturesSplitContainer.Panel1.ResumeLayout(false);
            this.AvailableFeaturesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AvailableFeaturesSplitContainer)).EndInit();
            this.AvailableFeaturesSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelChangesButton;
        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.GroupBox AvailableFeaturesGroupBox;
        private System.Windows.Forms.SplitContainer AvailableFeaturesSplitContainer;
        private System.Windows.Forms.TreeView AvailableFeaturesTreeView;
        private System.Windows.Forms.Label AvailableFeaturesInfoLabel;
        private System.Windows.Forms.GroupBox SelectedFeaturesGroupBox;
        private System.Windows.Forms.SplitContainer SelectedFeaturesSplitContainer;
        private System.Windows.Forms.TreeView SelectedFeaturesTreeView;
        private System.Windows.Forms.Label SelectedFeaturesInfoLabel;
        private System.Windows.Forms.Button OkButton;
    }
}