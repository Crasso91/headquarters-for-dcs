namespace Headquarters4DCS.Forms
{
    partial class FormMissionOutput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMissionOutput));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.BriefingLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BriefingWebBrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip1.SuspendLayout();
            this.BriefingLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(114, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // BriefingLayoutPanel
            // 
            this.BriefingLayoutPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BriefingLayoutPanel.ColumnCount = 3;
            this.BriefingLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BriefingLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 512F));
            this.BriefingLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BriefingLayoutPanel.Controls.Add(this.BriefingWebBrowser, 1, 0);
            this.BriefingLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BriefingLayoutPanel.Location = new System.Drawing.Point(0, 25);
            this.BriefingLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.BriefingLayoutPanel.Name = "BriefingLayoutPanel";
            this.BriefingLayoutPanel.RowCount = 1;
            this.BriefingLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.BriefingLayoutPanel.Size = new System.Drawing.Size(624, 416);
            this.BriefingLayoutPanel.TabIndex = 1;
            // 
            // BriefingWebBrowser
            // 
            this.BriefingWebBrowser.AllowNavigation = false;
            this.BriefingWebBrowser.AllowWebBrowserDrop = false;
            this.BriefingWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BriefingWebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.BriefingWebBrowser.Location = new System.Drawing.Point(59, 3);
            this.BriefingWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.BriefingWebBrowser.Name = "BriefingWebBrowser";
            this.BriefingWebBrowser.Size = new System.Drawing.Size(506, 410);
            this.BriefingWebBrowser.TabIndex = 1;
            this.BriefingWebBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // FormMissionOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.BriefingLayoutPanel);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(512, 384);
            this.Name = "FormMissionOutput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMissionOutput";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.BriefingLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TableLayoutPanel BriefingLayoutPanel;
        private System.Windows.Forms.WebBrowser BriefingWebBrowser;
    }
}