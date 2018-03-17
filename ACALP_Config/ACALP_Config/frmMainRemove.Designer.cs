namespace ACALP_Config
{
    partial class frmMainRemove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainRemove));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblVoortgang = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblVoortgang
            // 
            this.lblVoortgang.AutoSize = true;
            this.lblVoortgang.Location = new System.Drawing.Point(12, 9);
            this.lblVoortgang.Name = "lblVoortgang";
            this.lblVoortgang.Size = new System.Drawing.Size(256, 13);
            this.lblVoortgang.TabIndex = 0;
            this.lblVoortgang.Text = "Voorbereiden van het verwijderen van de toepassing";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 37);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(507, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // frmMainRemove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 97);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblVoortgang);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMainRemove";
            this.Text = "Instellingen verwijderen";
            this.Load += new System.EventHandler(this.frmMainRemove_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblVoortgang;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}