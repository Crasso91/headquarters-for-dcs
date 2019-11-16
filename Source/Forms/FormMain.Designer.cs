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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.MainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileS1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMission = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMissionGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMissionS1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuMissionExportMIZ = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMissionExportBriefing = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMissionExportBriefingHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMissionExportBriefingJPG = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMissionExportBriefingPNG = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolsRadioMessageGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.MainFormToolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStripButtonFileNew = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonFileOpen = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonFileSave = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonFileSaveAs = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripButtonMissionGenerate = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonMissionExportMIZ = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonMissionExportBriefing = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripButtonMissionExportBriefingHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripButtonMissionExportBriefingJPG = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripButtonMissionExportBriefingPNG = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.TemplatePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.BriefingPanel = new System.Windows.Forms.Panel();
            this.BriefingWebBrowser = new System.Windows.Forms.WebBrowser();
            this.MainFormMenuStrip.SuspendLayout();
            this.MainFormToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.BriefingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuMission,
            this.MenuTools});
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
            this.MenuFileExit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(37, 20);
            this.MenuFile.Text = "File";
            // 
            // MenuFileNew
            // 
            this.MenuFileNew.Name = "MenuFileNew";
            this.MenuFileNew.Size = new System.Drawing.Size(215, 22);
            this.MenuFileNew.Text = "&New mission template";
            this.MenuFileNew.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuFileOpen
            // 
            this.MenuFileOpen.Name = "MenuFileOpen";
            this.MenuFileOpen.Size = new System.Drawing.Size(215, 22);
            this.MenuFileOpen.Text = "&Open mission template";
            this.MenuFileOpen.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuFileSave
            // 
            this.MenuFileSave.Name = "MenuFileSave";
            this.MenuFileSave.Size = new System.Drawing.Size(215, 22);
            this.MenuFileSave.Text = "&Save mission template";
            this.MenuFileSave.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuFileSaveAs
            // 
            this.MenuFileSaveAs.Name = "MenuFileSaveAs";
            this.MenuFileSaveAs.Size = new System.Drawing.Size(215, 22);
            this.MenuFileSaveAs.Text = "S&ave mission template as...";
            this.MenuFileSaveAs.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuFileS1
            // 
            this.MenuFileS1.Name = "MenuFileS1";
            this.MenuFileS1.Size = new System.Drawing.Size(212, 6);
            // 
            // MenuFileExit
            // 
            this.MenuFileExit.Name = "MenuFileExit";
            this.MenuFileExit.Size = new System.Drawing.Size(215, 22);
            this.MenuFileExit.Text = "E&xit";
            this.MenuFileExit.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuMission
            // 
            this.MenuMission.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMissionGenerate,
            this.MenuMissionS1,
            this.MenuMissionExportMIZ,
            this.MenuMissionExportBriefing});
            this.MenuMission.Name = "MenuMission";
            this.MenuMission.Size = new System.Drawing.Size(60, 20);
            this.MenuMission.Text = "&Mission";
            // 
            // MenuMissionGenerate
            // 
            this.MenuMissionGenerate.Name = "MenuMissionGenerate";
            this.MenuMissionGenerate.Size = new System.Drawing.Size(219, 22);
            this.MenuMissionGenerate.Text = "&Generate another mission";
            this.MenuMissionGenerate.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuMissionS1
            // 
            this.MenuMissionS1.Name = "MenuMissionS1";
            this.MenuMissionS1.Size = new System.Drawing.Size(216, 6);
            // 
            // MenuMissionExportMIZ
            // 
            this.MenuMissionExportMIZ.Name = "MenuMissionExportMIZ";
            this.MenuMissionExportMIZ.Size = new System.Drawing.Size(219, 22);
            this.MenuMissionExportMIZ.Text = "E&xport mission to MIZ";
            this.MenuMissionExportMIZ.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuMissionExportBriefing
            // 
            this.MenuMissionExportBriefing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMissionExportBriefingHTML,
            this.MenuMissionExportBriefingJPG,
            this.MenuMissionExportBriefingPNG});
            this.MenuMissionExportBriefing.Name = "MenuMissionExportBriefing";
            this.MenuMissionExportBriefing.Size = new System.Drawing.Size(219, 22);
            this.MenuMissionExportBriefing.Text = "Export mission &briefing to...";
            // 
            // MenuMissionExportBriefingHTML
            // 
            this.MenuMissionExportBriefingHTML.Name = "MenuMissionExportBriefingHTML";
            this.MenuMissionExportBriefingHTML.Size = new System.Drawing.Size(106, 22);
            this.MenuMissionExportBriefingHTML.Text = "HTML";
            this.MenuMissionExportBriefingHTML.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuMissionExportBriefingJPG
            // 
            this.MenuMissionExportBriefingJPG.Name = "MenuMissionExportBriefingJPG";
            this.MenuMissionExportBriefingJPG.Size = new System.Drawing.Size(106, 22);
            this.MenuMissionExportBriefingJPG.Text = "JPG";
            this.MenuMissionExportBriefingJPG.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuMissionExportBriefingPNG
            // 
            this.MenuMissionExportBriefingPNG.Name = "MenuMissionExportBriefingPNG";
            this.MenuMissionExportBriefingPNG.Size = new System.Drawing.Size(106, 22);
            this.MenuMissionExportBriefingPNG.Text = "PNG";
            this.MenuMissionExportBriefingPNG.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuTools
            // 
            this.MenuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolsRadioMessageGenerator});
            this.MenuTools.Name = "MenuTools";
            this.MenuTools.Size = new System.Drawing.Size(46, 20);
            this.MenuTools.Text = "&Tools";
            this.MenuTools.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuToolsRadioMessageGenerator
            // 
            this.MenuToolsRadioMessageGenerator.Name = "MenuToolsRadioMessageGenerator";
            this.MenuToolsRadioMessageGenerator.Size = new System.Drawing.Size(207, 22);
            this.MenuToolsRadioMessageGenerator.Text = "&Radio message generator";
            // 
            // MainFormToolStrip
            // 
            this.MainFormToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButtonFileNew,
            this.ToolStripButtonFileOpen,
            this.ToolStripButtonFileSave,
            this.ToolStripButtonFileSaveAs,
            this.ToolStripSeparator1,
            this.ToolStripButtonMissionGenerate,
            this.ToolStripButtonMissionExportMIZ,
            this.ToolStripButtonMissionExportBriefing});
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
            // 
            // ToolStripButtonFileOpen
            // 
            this.ToolStripButtonFileOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileOpen.Image")));
            this.ToolStripButtonFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileOpen.Name = "ToolStripButtonFileOpen";
            this.ToolStripButtonFileOpen.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileOpen.Text = "Open mission template";
            // 
            // ToolStripButtonFileSave
            // 
            this.ToolStripButtonFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileSave.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileSave.Image")));
            this.ToolStripButtonFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileSave.Name = "ToolStripButtonFileSave";
            this.ToolStripButtonFileSave.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileSave.Text = "Save mission template";
            // 
            // ToolStripButtonFileSaveAs
            // 
            this.ToolStripButtonFileSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileSaveAs.Image")));
            this.ToolStripButtonFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileSaveAs.Name = "ToolStripButtonFileSaveAs";
            this.ToolStripButtonFileSaveAs.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileSaveAs.Text = "Save mission template as...";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripButtonMissionGenerate
            // 
            this.ToolStripButtonMissionGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonMissionGenerate.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonMissionGenerate.Image")));
            this.ToolStripButtonMissionGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonMissionGenerate.Name = "ToolStripButtonMissionGenerate";
            this.ToolStripButtonMissionGenerate.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonMissionGenerate.Text = "Generate another mission";
            this.ToolStripButtonMissionGenerate.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // ToolStripButtonMissionExportMIZ
            // 
            this.ToolStripButtonMissionExportMIZ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonMissionExportMIZ.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonMissionExportMIZ.Image")));
            this.ToolStripButtonMissionExportMIZ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonMissionExportMIZ.Name = "ToolStripButtonMissionExportMIZ";
            this.ToolStripButtonMissionExportMIZ.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonMissionExportMIZ.Text = "Export mission to MIZ";
            // 
            // ToolStripButtonMissionExportBriefing
            // 
            this.ToolStripButtonMissionExportBriefing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonMissionExportBriefing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButtonMissionExportBriefingHTML,
            this.ToolStripButtonMissionExportBriefingJPG,
            this.ToolStripButtonMissionExportBriefingPNG});
            this.ToolStripButtonMissionExportBriefing.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonMissionExportBriefing.Image")));
            this.ToolStripButtonMissionExportBriefing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonMissionExportBriefing.Name = "ToolStripButtonMissionExportBriefing";
            this.ToolStripButtonMissionExportBriefing.Size = new System.Drawing.Size(29, 22);
            this.ToolStripButtonMissionExportBriefing.Text = "Export mission briefing to...";
            // 
            // ToolStripButtonMissionExportBriefingHTML
            // 
            this.ToolStripButtonMissionExportBriefingHTML.Name = "ToolStripButtonMissionExportBriefingHTML";
            this.ToolStripButtonMissionExportBriefingHTML.Size = new System.Drawing.Size(106, 22);
            this.ToolStripButtonMissionExportBriefingHTML.Text = "HTML";
            // 
            // ToolStripButtonMissionExportBriefingJPG
            // 
            this.ToolStripButtonMissionExportBriefingJPG.Name = "ToolStripButtonMissionExportBriefingJPG";
            this.ToolStripButtonMissionExportBriefingJPG.Size = new System.Drawing.Size(106, 22);
            this.ToolStripButtonMissionExportBriefingJPG.Text = "JPG";
            // 
            // ToolStripButtonMissionExportBriefingPNG
            // 
            this.ToolStripButtonMissionExportBriefingPNG.Name = "ToolStripButtonMissionExportBriefingPNG";
            this.ToolStripButtonMissionExportBriefingPNG.Size = new System.Drawing.Size(106, 22);
            this.ToolStripButtonMissionExportBriefingPNG.Text = "PNG";
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 49);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.TemplatePropertyGrid);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.BriefingPanel);
            this.MainSplitContainer.Size = new System.Drawing.Size(784, 512);
            this.MainSplitContainer.SplitterDistance = 261;
            this.MainSplitContainer.TabIndex = 3;
            // 
            // TemplatePropertyGrid
            // 
            this.TemplatePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TemplatePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.TemplatePropertyGrid.Name = "TemplatePropertyGrid";
            this.TemplatePropertyGrid.Size = new System.Drawing.Size(261, 512);
            this.TemplatePropertyGrid.TabIndex = 0;
            this.TemplatePropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.Event_TemplatePropertyGridPropertyValueChanged);
            // 
            // BriefingPanel
            // 
            this.BriefingPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BriefingPanel.Controls.Add(this.BriefingWebBrowser);
            this.BriefingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BriefingPanel.Location = new System.Drawing.Point(0, 0);
            this.BriefingPanel.Name = "BriefingPanel";
            this.BriefingPanel.Size = new System.Drawing.Size(519, 512);
            this.BriefingPanel.TabIndex = 0;
            // 
            // BriefingWebBrowser
            // 
            this.BriefingWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BriefingWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.BriefingWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.BriefingWebBrowser.Name = "BriefingWebBrowser";
            this.BriefingWebBrowser.Size = new System.Drawing.Size(515, 508);
            this.BriefingWebBrowser.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.MainFormToolStrip);
            this.Controls.Add(this.MainFormMenuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.MainFormMenuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormMain";
            this.Text = "Headquarters for DCS World";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Event_MainFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Event_FormClosed);
            this.Load += new System.EventHandler(this.Event_FormLoad);
            this.MainFormMenuStrip.ResumeLayout(false);
            this.MainFormMenuStrip.PerformLayout();
            this.MainFormToolStrip.ResumeLayout(false);
            this.MainFormToolStrip.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.BriefingPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStrip MainFormToolStrip;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileNew;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileOpen;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.PropertyGrid TemplatePropertyGrid;
        private System.Windows.Forms.Panel BriefingPanel;
        private System.Windows.Forms.WebBrowser BriefingWebBrowser;
        private System.Windows.Forms.ToolStripMenuItem MenuMission;
        private System.Windows.Forms.ToolStripMenuItem MenuTools;
        private System.Windows.Forms.ToolStripMenuItem MenuFileNew;
        private System.Windows.Forms.ToolStripMenuItem MenuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSave;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator MenuFileS1;
        private System.Windows.Forms.ToolStripMenuItem MenuFileExit;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileSave;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolStripButtonMissionGenerate;
        private System.Windows.Forms.ToolStripMenuItem MenuMissionGenerate;
        private System.Windows.Forms.ToolStripMenuItem MenuToolsRadioMessageGenerator;
        private System.Windows.Forms.ToolStripButton ToolStripButtonMissionExportMIZ;
        private System.Windows.Forms.ToolStripDropDownButton ToolStripButtonMissionExportBriefing;
        private System.Windows.Forms.ToolStripMenuItem MenuMissionExportMIZ;
        private System.Windows.Forms.ToolStripMenuItem MenuMissionExportBriefing;
        private System.Windows.Forms.ToolStripMenuItem MenuMissionExportBriefingHTML;
        private System.Windows.Forms.ToolStripMenuItem MenuMissionExportBriefingJPG;
        private System.Windows.Forms.ToolStripMenuItem MenuMissionExportBriefingPNG;
        private System.Windows.Forms.ToolStripMenuItem ToolStripButtonMissionExportBriefingHTML;
        private System.Windows.Forms.ToolStripMenuItem ToolStripButtonMissionExportBriefingJPG;
        private System.Windows.Forms.ToolStripMenuItem ToolStripButtonMissionExportBriefingPNG;
        private System.Windows.Forms.ToolStripSeparator MenuMissionS1;
    }
}