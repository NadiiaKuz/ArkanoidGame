namespace ArkanoidGame
{
    partial class FrmArkanoidMain
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
            this.GameIterationTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // GameIterationTimer
            // 
            this.GameIterationTimer.Interval = 10;
            this.GameIterationTimer.Tick += new System.EventHandler(this.GameIterationTimer_Tick);
            // 
            // FrmArkanoidMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(420, 512);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArkanoidMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arkanoid Game";
            this.Load += new System.EventHandler(this.FrmArkanoidMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmArkanoidMain_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmArkanoidMain_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmArkanoidMain_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmArkanoidMain_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GameIterationTimer;
    }
}

