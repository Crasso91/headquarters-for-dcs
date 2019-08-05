namespace Headquarters4DCS.DevTools
{
    partial class FormMizToINI
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ParseButton = new System.Windows.Forms.Button();
            this.MainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.MainTableLayoutPanel.Controls.Add(this.CodeTextBox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.CloseButton, 1, 2);
            this.MainTableLayoutPanel.Controls.Add(this.ParseButton, 1, 1);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 3;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(784, 561);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeTextBox.Location = new System.Drawing.Point(3, 3);
            this.CodeTextBox.MaxLength = 1000000;
            this.CodeTextBox.Multiline = true;
            this.CodeTextBox.Name = "CodeTextBox";
            this.MainTableLayoutPanel.SetRowSpan(this.CodeTextBox, 3);
            this.CodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CodeTextBox.Size = new System.Drawing.Size(522, 555);
            this.CodeTextBox.TabIndex = 2;
            this.CodeTextBox.WordWrap = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CloseButton.Location = new System.Drawing.Point(531, 524);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(250, 34);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "&Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.Event_CloseButtonClick);
            // 
            // ParseButton
            // 
            this.ParseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParseButton.Location = new System.Drawing.Point(531, 484);
            this.ParseButton.Name = "ParseButton";
            this.ParseButton.Size = new System.Drawing.Size(250, 34);
            this.ParseButton.TabIndex = 4;
            this.ParseButton.Text = "&Parse";
            this.ParseButton.UseVisualStyleBackColor = true;
            this.ParseButton.Click += new System.EventHandler(this.Event_ParseButtonClick);
            // 
            // FormMizToINI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormMizToINI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIZ to INI";
            this.Load += new System.EventHandler(this.Event_FormLoad);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button ParseButton;
    }
}

