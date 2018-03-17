/*
  2011 - This file is part of AcaLabelPrint 

  AcaLabelPrint is free Software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  AcaLabelprint is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with AcaLabelPrint.  If not, see <http:www.gnu.org/licenses/>.

  We encourage you to use and extend the functionality of AcaLabelPrint,
  and send us an e-mail on the outlines of the extension you build. If
  it's generic, maybe we could add it to the project.
  Send your mail to the projectadmin at http:sourceforge.net/projects/labelprint/
*/
namespace LabelControler
{
    partial class FormPrintPreviewLimit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrintPreviewLimit));
            this.lblExplain = new System.Windows.Forms.Label();
            this.lblSeqstart = new System.Windows.Forms.Label();
            this.lblSeqEnd = new System.Windows.Forms.Label();
            this.nudStart = new System.Windows.Forms.NumericUpDown();
            this.nudEnd = new System.Windows.Forms.NumericUpDown();
            this.butOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // lblExplain
            // 
            this.lblExplain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExplain.Location = new System.Drawing.Point(13, 17);
            this.lblExplain.Name = "lblExplain";
            this.lblExplain.Size = new System.Drawing.Size(432, 39);
            this.lblExplain.TabIndex = 0;
            this.lblExplain.Text = "The number of labels in the preview is to large. Please limit the number of label" +
    "s to be shown in your preview. ";
            // 
            // lblSeqstart
            // 
            this.lblSeqstart.AutoSize = true;
            this.lblSeqstart.Location = new System.Drawing.Point(13, 61);
            this.lblSeqstart.Name = "lblSeqstart";
            this.lblSeqstart.Size = new System.Drawing.Size(150, 13);
            this.lblSeqstart.TabIndex = 1;
            this.lblSeqstart.Text = "Sequence number of first label";
            this.lblSeqstart.Click += new System.EventHandler(this.lblSeqstart_Click);
            // 
            // lblSeqEnd
            // 
            this.lblSeqEnd.AutoSize = true;
            this.lblSeqEnd.Location = new System.Drawing.Point(13, 87);
            this.lblSeqEnd.Name = "lblSeqEnd";
            this.lblSeqEnd.Size = new System.Drawing.Size(150, 13);
            this.lblSeqEnd.TabIndex = 2;
            this.lblSeqEnd.Text = "Sequence number of last label";
            // 
            // nudStart
            // 
            this.nudStart.Location = new System.Drawing.Point(184, 59);
            this.nudStart.Name = "nudStart";
            this.nudStart.Size = new System.Drawing.Size(120, 20);
            this.nudStart.TabIndex = 3;
            this.nudStart.ValueChanged += new System.EventHandler(this.nudStart_ValueChanged);
            // 
            // nudEnd
            // 
            this.nudEnd.Location = new System.Drawing.Point(184, 85);
            this.nudEnd.Name = "nudEnd";
            this.nudEnd.Size = new System.Drawing.Size(120, 20);
            this.nudEnd.TabIndex = 4;
            this.nudEnd.ValueChanged += new System.EventHandler(this.nudEnd_ValueChanged);
            // 
            // butOK
            // 
            this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOK.Location = new System.Drawing.Point(370, 109);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 5;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // FormPrintPreviewLimit
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 144);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.nudEnd);
            this.Controls.Add(this.nudStart);
            this.Controls.Add(this.lblSeqEnd);
            this.Controls.Add(this.lblSeqstart);
            this.Controls.Add(this.lblExplain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPrintPreviewLimit";
            this.Text = "Limit number of labels";
            this.Load += new System.EventHandler(this.FormPrintPreviewLimit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblExplain;
        private System.Windows.Forms.Label lblSeqstart;
        private System.Windows.Forms.Label lblSeqEnd;
        private System.Windows.Forms.NumericUpDown nudStart;
        private System.Windows.Forms.NumericUpDown nudEnd;
        private System.Windows.Forms.Button butOK;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
