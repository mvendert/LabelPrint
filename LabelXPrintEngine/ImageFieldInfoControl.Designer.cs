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
namespace ACA.LabelX.Controls
{
    partial class ImageFieldInfoControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.greyscalecheck = new System.Windows.Forms.CheckBox();
            this.keepratiocheck = new System.Windows.Forms.CheckBox();
            this.resizestylecombo = new System.Windows.Forms.ComboBox();
            this.rezisestylelbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // xpostxt
            // 
            this.xpostxt.TabIndex = 0;
            // 
            // ypostxt
            // 
            this.ypostxt.TabIndex = 1;
            // 
            // rotationcombo
            // 
            this.rotationcombo.TabIndex = 2;
            // 
            // manualwhcheck
            // 
            this.manualwhcheck.TabIndex = 4;
            // 
            // heighttxt
            // 
            this.heighttxt.TabIndex = 6;
            // 
            // widthtxt
            // 
            this.widthtxt.TabIndex = 5;
            // 
            // referencecombo
            // 
            this.referencecombo.TabIndex = 3;
            // 
            // moveleftbtn
            // 
            this.moveleftbtn.TabIndex = 11;
            // 
            // moverightbtn
            // 
            this.moverightbtn.TabIndex = 12;
            // 
            // moveupbtn
            // 
            this.moveupbtn.TabIndex = 10;
            // 
            // movedownbtn
            // 
            this.movedownbtn.TabIndex = 13;
            // 
            // greyscalecheck
            // 
            this.greyscalecheck.AutoSize = true;
            this.greyscalecheck.Location = new System.Drawing.Point(3, 220);
            this.greyscalecheck.Name = "greyscalecheck";
            this.greyscalecheck.Size = new System.Drawing.Size(73, 17);
            this.greyscalecheck.TabIndex = 9;
            this.greyscalecheck.Text = "Greyscale";
            this.greyscalecheck.UseVisualStyleBackColor = true;
            this.greyscalecheck.CheckedChanged += new System.EventHandler(this.greyscalecheck_CheckedChanged);
            // 
            // keepratiocheck
            // 
            this.keepratiocheck.AutoSize = true;
            this.keepratiocheck.Location = new System.Drawing.Point(3, 197);
            this.keepratiocheck.Name = "keepratiocheck";
            this.keepratiocheck.Size = new System.Drawing.Size(74, 17);
            this.keepratiocheck.TabIndex = 8;
            this.keepratiocheck.Text = "Keep ratio";
            this.keepratiocheck.UseVisualStyleBackColor = true;
            this.keepratiocheck.CheckedChanged += new System.EventHandler(this.keepratiocheck_CheckedChanged);
            // 
            // resizestylecombo
            // 
            this.resizestylecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resizestylecombo.FormattingEnabled = true;
            this.resizestylecombo.Location = new System.Drawing.Point(69, 170);
            this.resizestylecombo.Name = "resizestylecombo";
            this.resizestylecombo.Size = new System.Drawing.Size(111, 21);
            this.resizestylecombo.TabIndex = 7;
            this.resizestylecombo.SelectedIndexChanged += new System.EventHandler(this.resizestylecombo_SelectedIndexChanged);
            // 
            // rezisestylelbl
            // 
            this.rezisestylelbl.AutoSize = true;
            this.rezisestylelbl.Location = new System.Drawing.Point(3, 173);
            this.rezisestylelbl.Name = "rezisestylelbl";
            this.rezisestylelbl.Size = new System.Drawing.Size(69, 13);
            this.rezisestylelbl.TabIndex = 36;
            this.rezisestylelbl.Text = "Resize style: ";
            // 
            // ImageFieldInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.greyscalecheck);
            this.Controls.Add(this.keepratiocheck);
            this.Controls.Add(this.resizestylecombo);
            this.Controls.Add(this.rezisestylelbl);
            this.Name = "ImageFieldInfoControl";
            this.Controls.SetChildIndex(this.movedownbtn, 0);
            this.Controls.SetChildIndex(this.moveupbtn, 0);
            this.Controls.SetChildIndex(this.moverightbtn, 0);
            this.Controls.SetChildIndex(this.moveleftbtn, 0);
            this.Controls.SetChildIndex(this.adjustmentlbl, 0);
            this.Controls.SetChildIndex(this.xposlbl, 0);
            this.Controls.SetChildIndex(this.xpostxt, 0);
            this.Controls.SetChildIndex(this.yposlbl, 0);
            this.Controls.SetChildIndex(this.ypostxt, 0);
            this.Controls.SetChildIndex(this.rotationlbl, 0);
            this.Controls.SetChildIndex(this.rotationcombo, 0);
            this.Controls.SetChildIndex(this.referencelbl, 0);
            this.Controls.SetChildIndex(this.referencecombo, 0);
            this.Controls.SetChildIndex(this.widthlbl, 0);
            this.Controls.SetChildIndex(this.heightlbl, 0);
            this.Controls.SetChildIndex(this.widthtxt, 0);
            this.Controls.SetChildIndex(this.heighttxt, 0);
            this.Controls.SetChildIndex(this.manualwhcheck, 0);
            this.Controls.SetChildIndex(this.rezisestylelbl, 0);
            this.Controls.SetChildIndex(this.resizestylecombo, 0);
            this.Controls.SetChildIndex(this.keepratiocheck, 0);
            this.Controls.SetChildIndex(this.greyscalecheck, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox greyscalecheck;
        private System.Windows.Forms.CheckBox keepratiocheck;
        private System.Windows.Forms.ComboBox resizestylecombo;
        private System.Windows.Forms.Label rezisestylelbl;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
