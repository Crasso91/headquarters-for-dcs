namespace Headquarters4DCS.Forms
{
    partial class PanelLocationPropertyGrid
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
            this.NodePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // NodePropertyGrid
            // 
            this.NodePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NodePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.NodePropertyGrid.Name = "NodePropertyGrid";
            this.NodePropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.NodePropertyGrid.Size = new System.Drawing.Size(800, 450);
            this.NodePropertyGrid.TabIndex = 0;
            this.NodePropertyGrid.ToolbarVisible = false;
            // 
            // PanelNodePropertyGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NodePropertyGrid);
            this.Name = "PanelNodePropertyGrid";
            this.Text = "PanelNodePropertyGrid";
            this.Load += new System.EventHandler(this.PanelNodePropertyGrid_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid NodePropertyGrid;
    }
}