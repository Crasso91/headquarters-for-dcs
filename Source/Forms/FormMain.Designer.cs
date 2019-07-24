namespace Headquarters4DCS.Forms
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.MainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileS1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuFileGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuEditInvertAirbasesCoalition = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainFormToolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStripButtonFileNew = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonFileOpen = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonFileSave = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonFileSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripButtonFileGenerate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.MapContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SettingsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.MapScrollPanel = new System.Windows.Forms.Panel();
            this.MapPictureBox = new System.Windows.Forms.PictureBox();
            this.MainFormMenuStrip.SuspendLayout();
            this.MainFormToolStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.MapScrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuEdit,
            this.helpToolStripMenuItem});
            this.MainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainFormMenuStrip.Name = "MainFormMenuStrip";
            this.MainFormMenuStrip.Size = new System.Drawing.Size(784, 24);
            this.MainFormMenuStrip.TabIndex = 0;
            this.MainFormMenuStrip.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFileNew,
            this.MenuFileOpen,
            this.MenuFileSave,
            this.MenuFileSaveAs,
            this.MenuFileS1,
            this.MenuFileGenerate,
            this.toolStripMenuItem2,
            this.MenuFileExit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(37, 20);
            this.MenuFile.Text = "&File";
            // 
            // MenuFileNew
            // 
            this.MenuFileNew.Name = "MenuFileNew";
            this.MenuFileNew.Size = new System.Drawing.Size(283, 22);
            this.MenuFileNew.Text = "&New mission template...";
            this.MenuFileNew.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuFileNewDropDownItemClicked);
            // 
            // MenuFileOpen
            // 
            this.MenuFileOpen.Name = "MenuFileOpen";
            this.MenuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuFileOpen.Size = new System.Drawing.Size(283, 22);
            this.MenuFileOpen.Text = "&Open mission template";
            this.MenuFileOpen.Click += new System.EventHandler(this.MenuClick);
            // 
            // MenuFileSave
            // 
            this.MenuFileSave.Name = "MenuFileSave";
            this.MenuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuFileSave.Size = new System.Drawing.Size(283, 22);
            this.MenuFileSave.Text = "&Save mission template";
            this.MenuFileSave.Click += new System.EventHandler(this.MenuClick);
            // 
            // MenuFileSaveAs
            // 
            this.MenuFileSaveAs.Name = "MenuFileSaveAs";
            this.MenuFileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MenuFileSaveAs.Size = new System.Drawing.Size(283, 22);
            this.MenuFileSaveAs.Text = "Save mission template &as...";
            this.MenuFileSaveAs.Click += new System.EventHandler(this.MenuClick);
            // 
            // MenuFileS1
            // 
            this.MenuFileS1.Name = "MenuFileS1";
            this.MenuFileS1.Size = new System.Drawing.Size(280, 6);
            // 
            // MenuFileGenerate
            // 
            this.MenuFileGenerate.Name = "MenuFileGenerate";
            this.MenuFileGenerate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.MenuFileGenerate.Size = new System.Drawing.Size(283, 22);
            this.MenuFileGenerate.Text = "&Generate mission";
            this.MenuFileGenerate.Click += new System.EventHandler(this.MenuClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(280, 6);
            // 
            // MenuFileExit
            // 
            this.MenuFileExit.Name = "MenuFileExit";
            this.MenuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MenuFileExit.Size = new System.Drawing.Size(283, 22);
            this.MenuFileExit.Text = "&Exit";
            this.MenuFileExit.Click += new System.EventHandler(this.MenuClick);
            // 
            // MenuEdit
            // 
            this.MenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuEditInvertAirbasesCoalition});
            this.MenuEdit.Name = "MenuEdit";
            this.MenuEdit.Size = new System.Drawing.Size(39, 20);
            this.MenuEdit.Text = "&Edit";
            // 
            // MenuEditInvertAirbasesCoalition
            // 
            this.MenuEditInvertAirbasesCoalition.Name = "MenuEditInvertAirbasesCoalition";
            this.MenuEditInvertAirbasesCoalition.Size = new System.Drawing.Size(208, 22);
            this.MenuEditInvertAirbasesCoalition.Text = "Switch airbases coalitions";
            this.MenuEditInvertAirbasesCoalition.Click += new System.EventHandler(this.MenuClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.websiteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.websiteToolStripMenuItem.Text = "&Website";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // MainFormToolStrip
            // 
            this.MainFormToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButtonFileNew,
            this.ToolStripButtonFileOpen,
            this.ToolStripButtonFileSave,
            this.ToolStripButtonFileSaveAs,
            this.toolStripSeparator1,
            this.ToolStripButtonFileGenerate,
            this.toolStripSeparator2,
            this.ToolStripButtonZoomIn,
            this.ToolStripButtonZoomOut});
            this.MainFormToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MainFormToolStrip.Name = "MainFormToolStrip";
            this.MainFormToolStrip.Size = new System.Drawing.Size(784, 25);
            this.MainFormToolStrip.TabIndex = 1;
            this.MainFormToolStrip.Text = "toolStrip1";
            // 
            // ToolStripButtonFileNew
            // 
            this.ToolStripButtonFileNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileNew.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileNew.Image")));
            this.ToolStripButtonFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileNew.Name = "ToolStripButtonFileNew";
            this.ToolStripButtonFileNew.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileNew.Text = "New mission template";
            this.ToolStripButtonFileNew.Click += new System.EventHandler(this.MenuClick);
            // 
            // ToolStripButtonFileOpen
            // 
            this.ToolStripButtonFileOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileOpen.Image")));
            this.ToolStripButtonFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileOpen.Name = "ToolStripButtonFileOpen";
            this.ToolStripButtonFileOpen.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileOpen.Text = "Open mission template";
            this.ToolStripButtonFileOpen.Click += new System.EventHandler(this.MenuClick);
            // 
            // ToolStripButtonFileSave
            // 
            this.ToolStripButtonFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileSave.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileSave.Image")));
            this.ToolStripButtonFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileSave.Name = "ToolStripButtonFileSave";
            this.ToolStripButtonFileSave.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileSave.Text = "Save mission template";
            this.ToolStripButtonFileSave.Click += new System.EventHandler(this.MenuClick);
            // 
            // ToolStripButtonFileSaveAs
            // 
            this.ToolStripButtonFileSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileSaveAs.Image")));
            this.ToolStripButtonFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileSaveAs.Name = "ToolStripButtonFileSaveAs";
            this.ToolStripButtonFileSaveAs.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileSaveAs.Text = "Save mission template as...";
            this.ToolStripButtonFileSaveAs.Click += new System.EventHandler(this.MenuClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripButtonFileGenerate
            // 
            this.ToolStripButtonFileGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileGenerate.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileGenerate.Image")));
            this.ToolStripButtonFileGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileGenerate.Name = "ToolStripButtonFileGenerate";
            this.ToolStripButtonFileGenerate.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileGenerate.Text = "Generate mission";
            this.ToolStripButtonFileGenerate.Click += new System.EventHandler(this.MenuClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripButtonZoomIn
            // 
            this.ToolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonZoomIn.Image")));
            this.ToolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonZoomIn.Name = "ToolStripButtonZoomIn";
            this.ToolStripButtonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonZoomIn.Text = "Zoom in";
            this.ToolStripButtonZoomIn.Click += new System.EventHandler(this.MenuClick);
            // 
            // ToolStripButtonZoomOut
            // 
            this.ToolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonZoomOut.Image")));
            this.ToolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonZoomOut.Name = "ToolStripButtonZoomOut";
            this.ToolStripButtonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonZoomOut.Text = "Zoom out";
            this.ToolStripButtonZoomOut.Click += new System.EventHandler(this.MenuClick);
            // 
            // MapContextMenuStrip
            // 
            this.MapContextMenuStrip.Name = "MapContextMenuStrip";
            this.MapContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 539);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(784, 22);
            this.MainStatusStrip.TabIndex = 2;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(502, 17);
            this.StatusStripLabel.Text = "Click an location on the map for options, or click on an empty spot for global mi" +
    "ssion settings";
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 49);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.SettingsPropertyGrid);
            this.MainSplitContainer.Panel1MinSize = 128;
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.MapScrollPanel);
            this.MainSplitContainer.Panel2MinSize = 256;
            this.MainSplitContainer.Size = new System.Drawing.Size(784, 490);
            this.MainSplitContainer.SplitterDistance = 260;
            this.MainSplitContainer.TabIndex = 4;
            // 
            // SettingsPropertyGrid
            // 
            this.SettingsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.SettingsPropertyGrid.Name = "SettingsPropertyGrid";
            this.SettingsPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.SettingsPropertyGrid.Size = new System.Drawing.Size(260, 490);
            this.SettingsPropertyGrid.TabIndex = 2;
            this.SettingsPropertyGrid.ToolbarVisible = false;
            this.SettingsPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.SettingsPropertyGridPropertyValueChanged);
            // 
            // MapScrollPanel
            // 
            this.MapScrollPanel.AutoScroll = true;
            this.MapScrollPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(155)))), ((int)(((byte)(107)))));
            this.MapScrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MapScrollPanel.Controls.Add(this.MapPictureBox);
            this.MapScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapScrollPanel.Location = new System.Drawing.Point(0, 0);
            this.MapScrollPanel.Name = "MapScrollPanel";
            this.MapScrollPanel.Size = new System.Drawing.Size(520, 490);
            this.MapScrollPanel.TabIndex = 3;
            // 
            // MapPictureBox
            // 
            this.MapPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(155)))), ((int)(((byte)(107)))));
            this.MapPictureBox.Location = new System.Drawing.Point(0, 0);
            this.MapPictureBox.Name = "MapPictureBox";
            this.MapPictureBox.Size = new System.Drawing.Size(100, 50);
            this.MapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.MapPictureBox.TabIndex = 0;
            this.MapPictureBox.TabStop = false;
            this.MapPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapPictureBoxMouseDown);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MainFormToolStrip);
            this.Controls.Add(this.MainFormMenuStrip);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainFormMenuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Headquarters for DCS World";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.MainFormMenuStrip.ResumeLayout(false);
            this.MainFormMenuStrip.PerformLayout();
            this.MainFormToolStrip.ResumeLayout(false);
            this.MainFormToolStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.MapScrollPanel.ResumeLayout(false);
            this.MapScrollPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStrip MainFormToolStrip;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileNew;
        private System.Windows.Forms.ToolStripMenuItem MenuFileNew;
        private System.Windows.Forms.ToolStripMenuItem MenuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator MenuFileS1;
        private System.Windows.Forms.ToolStripMenuItem MenuFileExit;
        private System.Windows.Forms.ContextMenuStrip MapContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripButton ToolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripMenuItem MenuEdit;
        private System.Windows.Forms.ToolStripMenuItem MenuEditInvertAirbasesCoalition;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSave;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileOpen;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileSave;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileSaveAs;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.PropertyGrid SettingsPropertyGrid;
        private System.Windows.Forms.Panel MapScrollPanel;
        private System.Windows.Forms.PictureBox MapPictureBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuFileGenerate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileGenerate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}