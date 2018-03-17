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
namespace ACA.Support.Tools.ACAException
{
    partial class ExceptionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionDialog));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblErrorHeading = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.RichTextBox();
            this.lblScopeHeading = new System.Windows.Forms.Label();
            this.txtScope = new System.Windows.Forms.RichTextBox();
            this.lblActionHeading = new System.Windows.Forms.Label();
            this.txtAction = new System.Windows.Forms.RichTextBox();
            this.lblMoreHeading = new System.Windows.Forms.Label();
            this.butMoreInfo = new System.Windows.Forms.Button();
            this.txtMore = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.toolTipDlg = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(405, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // lblErrorHeading
            // 
            this.lblErrorHeading.AutoSize = true;
            this.lblErrorHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorHeading.Location = new System.Drawing.Point(123, 12);
            this.lblErrorHeading.Name = "lblErrorHeading";
            this.lblErrorHeading.Size = new System.Drawing.Size(140, 13);
            this.lblErrorHeading.TabIndex = 1;
            this.lblErrorHeading.Text = "Wat is er aan de hand?";
            // 
            // txtError
            // 
            this.txtError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtError.BackColor = System.Drawing.SystemColors.Window;
            this.txtError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtError.Location = new System.Drawing.Point(126, 28);
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(308, 143);
            this.txtError.TabIndex = 2;
            this.txtError.Text = "(Error Message)";
            this.txtError.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtError_LinkClicked);
            // 
            // lblScopeHeading
            // 
            this.lblScopeHeading.AutoSize = true;
            this.lblScopeHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScopeHeading.Location = new System.Drawing.Point(123, 179);
            this.lblScopeHeading.Name = "lblScopeHeading";
            this.lblScopeHeading.Size = new System.Drawing.Size(114, 13);
            this.lblScopeHeading.TabIndex = 3;
            this.lblScopeHeading.Text = "Wat is het gevolg?";
            // 
            // txtScope
            // 
            this.txtScope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScope.BackColor = System.Drawing.SystemColors.Window;
            this.txtScope.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScope.Location = new System.Drawing.Point(126, 195);
            this.txtScope.Name = "txtScope";
            this.txtScope.ReadOnly = true;
            this.txtScope.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtScope.Size = new System.Drawing.Size(308, 63);
            this.txtScope.TabIndex = 4;
            this.txtScope.Text = "(scope)";
            this.txtScope.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtScope_LinkClicked);
            // 
            // lblActionHeading
            // 
            this.lblActionHeading.AutoSize = true;
            this.lblActionHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionHeading.Location = new System.Drawing.Point(123, 266);
            this.lblActionHeading.Name = "lblActionHeading";
            this.lblActionHeading.Size = new System.Drawing.Size(148, 13);
            this.lblActionHeading.TabIndex = 5;
            this.lblActionHeading.Text = "Wat kan je er aan doen?";
            // 
            // txtAction
            // 
            this.txtAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAction.BackColor = System.Drawing.SystemColors.Window;
            this.txtAction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAction.Location = new System.Drawing.Point(126, 282);
            this.txtAction.Name = "txtAction";
            this.txtAction.ReadOnly = true;
            this.txtAction.Size = new System.Drawing.Size(308, 69);
            this.txtAction.TabIndex = 6;
            this.txtAction.Text = "(action)";
            this.txtAction.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtAction_LinkClicked);
            // 
            // lblMoreHeading
            // 
            this.lblMoreHeading.AutoSize = true;
            this.lblMoreHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoreHeading.Location = new System.Drawing.Point(12, 311);
            this.lblMoreHeading.Name = "lblMoreHeading";
            this.lblMoreHeading.Size = new System.Drawing.Size(94, 13);
            this.lblMoreHeading.TabIndex = 7;
            this.lblMoreHeading.Text = "Meer informatie";
            // 
            // butMoreInfo
            // 
            this.butMoreInfo.Location = new System.Drawing.Point(123, 306);
            this.butMoreInfo.Name = "butMoreInfo";
            this.butMoreInfo.Size = new System.Drawing.Size(27, 23);
            this.butMoreInfo.TabIndex = 8;
            this.butMoreInfo.Text = ">>";
            this.butMoreInfo.UseVisualStyleBackColor = true;
            this.butMoreInfo.Click += new System.EventHandler(this.butMoreInfo_Click);
            // 
            // txtMore
            // 
            this.txtMore.BackColor = System.Drawing.SystemColors.Window;
            this.txtMore.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMore.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMore.Location = new System.Drawing.Point(12, 335);
            this.txtMore.Name = "txtMore";
            this.txtMore.ReadOnly = true;
            this.txtMore.Size = new System.Drawing.Size(413, 61);
            this.txtMore.TabIndex = 9;
            this.txtMore.Text = "(detailed information, such as a stack trace)";
            this.txtMore.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtMore_LinkClicked);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(361, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(280, 407);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 11;
            this.butCancel.Text = "Annuleren";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(94, 159);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            this.toolTipDlg.SetToolTip(this.pictureBox2, "George");
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // toolTipDlg
            // 
            // 
            // ExceptionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(449, 442);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMore);
            this.Controls.Add(this.butMoreInfo);
            this.Controls.Add(this.lblMoreHeading);
            this.Controls.Add(this.txtAction);
            this.Controls.Add(this.lblActionHeading);
            this.Controls.Add(this.txtScope);
            this.Controls.Add(this.lblScopeHeading);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.lblErrorHeading);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExceptionDialog";
            this.Text = "Uitzondering";
            this.Load += new System.EventHandler(this.ExceptionDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblErrorHeading;
        private System.Windows.Forms.RichTextBox txtError;
        private System.Windows.Forms.Label lblScopeHeading;
        private System.Windows.Forms.RichTextBox txtScope;
        private System.Windows.Forms.Label lblActionHeading;
        private System.Windows.Forms.RichTextBox txtAction;
        private System.Windows.Forms.Label lblMoreHeading;
        private System.Windows.Forms.Button butMoreInfo;
        private System.Windows.Forms.RichTextBox txtMore;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolTip toolTipDlg;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
