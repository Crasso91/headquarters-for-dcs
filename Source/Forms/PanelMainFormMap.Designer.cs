namespace Headquarters4DCS.Forms
{
    partial class PanelMainFormMap
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
            this.MapIconsImageList = new System.Windows.Forms.ImageList(this.components);
            this.MapImageBox = new Cyotek.Windows.Forms.ImageBox();
            this.SuspendLayout();
            // 
            // MapIconsImageList
            // 
            this.MapIconsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.MapIconsImageList.ImageSize = new System.Drawing.Size(24, 24);
            this.MapIconsImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MapImageBox
            // 
            this.MapImageBox.AutoCenter = false;
            this.MapImageBox.AutoScroll = true;
            this.MapImageBox.AutoSize = false;
            this.MapImageBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.MapImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapImageBox.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            this.MapImageBox.ImageList = this.MapIconsImageList;
            this.MapImageBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.MapImageBox.Location = new System.Drawing.Point(0, 0);
            this.MapImageBox.Name = "MapImageBox";
            this.MapImageBox.Size = new System.Drawing.Size(800, 450);
            this.MapImageBox.TabIndex = 5;
            this.MapImageBox.ZoomIncrement = 15;
            this.MapImageBox.ZoomOnClick = false;
            this.MapImageBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MapImageBoxMouseDoubleClick);
            this.MapImageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapImageBoxMouseDown);
            this.MapImageBox.MouseEnter += new System.EventHandler(this.MapImageBoxMouseEnter);
            this.MapImageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapImageBoxMouseUp);
            // 
            // PanelMainFormMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MapImageBox);
            this.Name = "PanelMainFormMap";
            this.Text = "PanelMainFormMap";
            this.Load += new System.EventHandler(this.PanelMainFormMap_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Cyotek.Windows.Forms.ImageBox MapImageBox;
        private System.Windows.Forms.ImageList MapIconsImageList;
    }
}