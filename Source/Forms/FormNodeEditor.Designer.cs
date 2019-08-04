namespace Headquarters4DCS.Forms
{
    partial class FormNodeEditor
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
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CancelChangesButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.MainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.Controls.Add(this.CancelChangesButton, 2, 1);
            this.MainTableLayoutPanel.Controls.Add(this.OkButton, 1, 1);
            this.MainTableLayoutPanel.Controls.Add(this.MainTabControl, 0, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(624, 441);
            this.MainTableLayoutPanel.TabIndex = 1;
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
            // MainTabControl
            // 
            this.MainTableLayoutPanel.SetColumnSpan(this.MainTabControl, 3);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(3, 3);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(618, 395);
            this.MainTabControl.TabIndex = 6;
            // 
            // FormNodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(512, 384);
            this.Name = "FormNodeEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Node editor";
            this.Load += new System.EventHandler(this.FormNodeEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormNodeEditor_KeyDown);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.Button CancelChangesButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.TabControl MainTabControl;
    }
}