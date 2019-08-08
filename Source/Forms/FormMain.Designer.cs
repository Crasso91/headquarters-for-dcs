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
            this.MenuMissionInvertAirbasesCoalition = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDevelopment = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDevelopmentMizToIni = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpS1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MainFormToolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStripButtonFileNew = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripButtonFileOpen = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonFileSave = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonFileSaveAs = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripButtonMissionGenerate = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.MenuDevelopmentRadioMessageGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.MainFormMenuStrip.SuspendLayout();
            this.MainFormToolStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormMenuStrip
            // 
            this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuMission,
            this.MenuDevelopment,
            this.MenuHelp});
            this.MainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainFormMenuStrip.Name = "MainFormMenuStrip";
            this.MainFormMenuStrip.Size = new System.Drawing.Size(1008, 24);
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
            this.MenuFile.Text = "&File";
            // 
            // MenuFileNew
            // 
            this.MenuFileNew.Name = "MenuFileNew";
            this.MenuFileNew.Size = new System.Drawing.Size(283, 22);
            this.MenuFileNew.Text = "&New mission template...";
            this.MenuFileNew.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Event_MenuFileNewDropDownItemClicked);
            // 
            // MenuFileOpen
            // 
            this.MenuFileOpen.Name = "MenuFileOpen";
            this.MenuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuFileOpen.Size = new System.Drawing.Size(283, 22);
            this.MenuFileOpen.Text = "&Open mission template";
            this.MenuFileOpen.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuFileSave
            // 
            this.MenuFileSave.Name = "MenuFileSave";
            this.MenuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuFileSave.Size = new System.Drawing.Size(283, 22);
            this.MenuFileSave.Text = "&Save mission template";
            this.MenuFileSave.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuFileSaveAs
            // 
            this.MenuFileSaveAs.Name = "MenuFileSaveAs";
            this.MenuFileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MenuFileSaveAs.Size = new System.Drawing.Size(283, 22);
            this.MenuFileSaveAs.Text = "Save mission template &as...";
            this.MenuFileSaveAs.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuFileS1
            // 
            this.MenuFileS1.Name = "MenuFileS1";
            this.MenuFileS1.Size = new System.Drawing.Size(280, 6);
            // 
            // MenuFileExit
            // 
            this.MenuFileExit.Name = "MenuFileExit";
            this.MenuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MenuFileExit.Size = new System.Drawing.Size(283, 22);
            this.MenuFileExit.Text = "&Exit";
            this.MenuFileExit.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuMission
            // 
            this.MenuMission.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMissionGenerate,
            this.MenuMissionS1,
            this.MenuMissionInvertAirbasesCoalition});
            this.MenuMission.Name = "MenuMission";
            this.MenuMission.Size = new System.Drawing.Size(60, 20);
            this.MenuMission.Text = "&Mission";
            // 
            // MenuMissionGenerate
            // 
            this.MenuMissionGenerate.Name = "MenuMissionGenerate";
            this.MenuMissionGenerate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.MenuMissionGenerate.Size = new System.Drawing.Size(208, 22);
            this.MenuMissionGenerate.Text = "&Generate mission";
            this.MenuMissionGenerate.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuMissionS1
            // 
            this.MenuMissionS1.Name = "MenuMissionS1";
            this.MenuMissionS1.Size = new System.Drawing.Size(205, 6);
            // 
            // MenuMissionInvertAirbasesCoalition
            // 
            this.MenuMissionInvertAirbasesCoalition.Name = "MenuMissionInvertAirbasesCoalition";
            this.MenuMissionInvertAirbasesCoalition.Size = new System.Drawing.Size(208, 22);
            this.MenuMissionInvertAirbasesCoalition.Text = "&Switch airbases coalitions";
            this.MenuMissionInvertAirbasesCoalition.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuDevelopment
            // 
            this.MenuDevelopment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuDevelopmentMizToIni,
            this.MenuDevelopmentRadioMessageGenerator});
            this.MenuDevelopment.Name = "MenuDevelopment";
            this.MenuDevelopment.Size = new System.Drawing.Size(119, 20);
            this.MenuDevelopment.Text = "&Development tools";
            // 
            // MenuDevelopmentMizToIni
            // 
            this.MenuDevelopmentMizToIni.Name = "MenuDevelopmentMizToIni";
            this.MenuDevelopmentMizToIni.Size = new System.Drawing.Size(207, 22);
            this.MenuDevelopmentMizToIni.Text = "&Miz to Ini";
            this.MenuDevelopmentMizToIni.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MenuHelp
            // 
            this.MenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuHelpWebsite,
            this.MenuHelpS1,
            this.MenuHelpAbout});
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(44, 20);
            this.MenuHelp.Text = "&Help";
            // 
            // MenuHelpWebsite
            // 
            this.MenuHelpWebsite.Name = "MenuHelpWebsite";
            this.MenuHelpWebsite.Size = new System.Drawing.Size(116, 22);
            this.MenuHelpWebsite.Text = "&Website";
            // 
            // MenuHelpS1
            // 
            this.MenuHelpS1.Name = "MenuHelpS1";
            this.MenuHelpS1.Size = new System.Drawing.Size(113, 6);
            // 
            // MenuHelpAbout
            // 
            this.MenuHelpAbout.Name = "MenuHelpAbout";
            this.MenuHelpAbout.Size = new System.Drawing.Size(116, 22);
            this.MenuHelpAbout.Text = "&About";
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
            this.ToolStripSeparator2,
            this.ToolStripButtonZoomIn,
            this.ToolStripButtonZoomOut});
            this.MainFormToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MainFormToolStrip.Name = "MainFormToolStrip";
            this.MainFormToolStrip.Size = new System.Drawing.Size(1008, 25);
            this.MainFormToolStrip.TabIndex = 1;
            this.MainFormToolStrip.Text = "toolStrip1";
            // 
            // ToolStripButtonFileNew
            // 
            this.ToolStripButtonFileNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileNew.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileNew.Image")));
            this.ToolStripButtonFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileNew.Name = "ToolStripButtonFileNew";
            this.ToolStripButtonFileNew.Size = new System.Drawing.Size(29, 22);
            this.ToolStripButtonFileNew.Text = "ToolStripButtonFileNew";
            this.ToolStripButtonFileNew.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Event_MenuFileNewDropDownItemClicked);
            // 
            // ToolStripButtonFileOpen
            // 
            this.ToolStripButtonFileOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileOpen.Image")));
            this.ToolStripButtonFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileOpen.Name = "ToolStripButtonFileOpen";
            this.ToolStripButtonFileOpen.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileOpen.Text = "ToolStripButtonFileOpen";
            this.ToolStripButtonFileOpen.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // ToolStripButtonFileSave
            // 
            this.ToolStripButtonFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileSave.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileSave.Image")));
            this.ToolStripButtonFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileSave.Name = "ToolStripButtonFileSave";
            this.ToolStripButtonFileSave.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileSave.Text = "ToolStripButtonFileSave";
            this.ToolStripButtonFileSave.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // ToolStripButtonFileSaveAs
            // 
            this.ToolStripButtonFileSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonFileSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonFileSaveAs.Image")));
            this.ToolStripButtonFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonFileSaveAs.Name = "ToolStripButtonFileSaveAs";
            this.ToolStripButtonFileSaveAs.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonFileSaveAs.Text = "ToolStripButtonFileSaveAs";
            this.ToolStripButtonFileSaveAs.Click += new System.EventHandler(this.Event_MenuClick);
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
            this.ToolStripButtonMissionGenerate.Text = "ToolStripButtonFileGenerate";
            this.ToolStripButtonMissionGenerate.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripButtonZoomIn
            // 
            this.ToolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonZoomIn.Image")));
            this.ToolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonZoomIn.Name = "ToolStripButtonZoomIn";
            this.ToolStripButtonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonZoomIn.Text = "ToolStripButtonZoomIn";
            this.ToolStripButtonZoomIn.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // ToolStripButtonZoomOut
            // 
            this.ToolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonZoomOut.Image")));
            this.ToolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonZoomOut.Name = "ToolStripButtonZoomOut";
            this.ToolStripButtonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButtonZoomOut.Text = "ToolStripButtonZoomOut";
            this.ToolStripButtonZoomOut.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabelInfo});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 707);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(1008, 22);
            this.MainStatusStrip.TabIndex = 2;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // StatusStripLabelInfo
            // 
            this.StatusStripLabelInfo.Name = "StatusStripLabelInfo";
            this.StatusStripLabelInfo.Size = new System.Drawing.Size(112, 17);
            this.StatusStripLabelInfo.Text = "StatusStripLabelInfo";
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
            this.MainSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.MainSplitContainer.Panel1MinSize = 128;
            this.MainSplitContainer.Panel2MinSize = 256;
            this.MainSplitContainer.Size = new System.Drawing.Size(1008, 658);
            this.MainSplitContainer.SplitterDistance = 256;
            this.MainSplitContainer.TabIndex = 3;
            // 
            // MenuDevelopmentRadioMessageGenerator
            // 
            this.MenuDevelopmentRadioMessageGenerator.Name = "MenuDevelopmentRadioMessageGenerator";
            this.MenuDevelopmentRadioMessageGenerator.Size = new System.Drawing.Size(207, 22);
            this.MenuDevelopmentRadioMessageGenerator.Text = "&Radio message generator";
            this.MenuDevelopmentRadioMessageGenerator.Click += new System.EventHandler(this.Event_MenuClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Event_MainFormClosing);
            this.Load += new System.EventHandler(this.Event_FormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Event_FormKeyDown);
            this.MainFormMenuStrip.ResumeLayout(false);
            this.MainFormMenuStrip.PerformLayout();
            this.MainFormToolStrip.ResumeLayout(false);
            this.MainFormToolStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStrip MainFormToolStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuFileNew;
        private System.Windows.Forms.ToolStripMenuItem MenuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem MenuFileExit;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripButton ToolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripMenuItem MenuMission;
        private System.Windows.Forms.ToolStripMenuItem MenuMissionInvertAirbasesCoalition;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSave;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileOpen;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileSave;
        private System.Windows.Forms.ToolStripButton ToolStripButtonFileSaveAs;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpWebsite;
        private System.Windows.Forms.ToolStripSeparator MenuHelpS1;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpAbout;
        private System.Windows.Forms.ToolStripSeparator MenuFileS1;
        private System.Windows.Forms.ToolStripButton ToolStripButtonMissionGenerate;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton ToolStripButtonFileNew;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLabelInfo;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem MenuMissionGenerate;
        private System.Windows.Forms.ToolStripSeparator MenuMissionS1;
        private System.Windows.Forms.ToolStripMenuItem MenuDevelopment;
        private System.Windows.Forms.ToolStripMenuItem MenuDevelopmentMizToIni;
        private System.Windows.Forms.ToolStripMenuItem MenuDevelopmentRadioMessageGenerator;
    }
}