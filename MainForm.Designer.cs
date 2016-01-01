namespace PhotoScreenSaver
{
    partial class MainForm
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblFolder = new BorderLabel();
			this.lblName = new BorderLabel();
			this.lblDate = new BorderLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(805, 800);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblFolder
			// 
			this.lblFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblFolder.BackColor = System.Drawing.Color.Transparent;
			this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFolder.ForeColor = System.Drawing.Color.White;
			this.lblFolder.Location = new System.Drawing.Point(-7, 0);
			this.lblFolder.Name = "lblFolder";
			this.lblFolder.Size = new System.Drawing.Size(812, 40);
			this.lblFolder.TabIndex = 1;
			this.lblDate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblFolder.BorderColor = System.Drawing.Color.Black;
			// 
			// lblName
			// 
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.ForeColor = System.Drawing.Color.White;
			this.lblName.Location = new System.Drawing.Point(-7, 760);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(530, 40);
			this.lblName.TabIndex = 2;
			this.lblName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.lblName.BorderColor = System.Drawing.Color.Black;
			// 
			// lblDate
			// 
			this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDate.BackColor = System.Drawing.Color.Transparent;
			this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDate.ForeColor = System.Drawing.Color.White;
			this.lblDate.Location = new System.Drawing.Point(531, 760);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(274, 40);
			this.lblDate.TabIndex = 3;
			this.lblDate.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.lblDate.BorderColor = System.Drawing.Color.Black;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(805, 800);
			this.Controls.Add(this.lblDate);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lblFolder);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "MainForm";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.Click += new System.EventHandler(this.MainForm_Click);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private BorderLabel lblFolder;
		private BorderLabel lblName;
		private BorderLabel lblDate;

    }
}

