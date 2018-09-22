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
namespace LabelDesigner
{
    partial class AddImageForm
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
            this.manualwhcheck = new System.Windows.Forms.CheckBox();
            this.heighttxt = new System.Windows.Forms.TextBox();
            this.widthtxt = new System.Windows.Forms.TextBox();
            this.heightlbl = new System.Windows.Forms.Label();
            this.widthlbl = new System.Windows.Forms.Label();
            this.referencecombo = new System.Windows.Forms.ComboBox();
            this.referencelbl = new System.Windows.Forms.Label();
            this.YPostxt = new System.Windows.Forms.TextBox();
            this.yposlbl = new System.Windows.Forms.Label();
            this.XPostxt = new System.Windows.Forms.TextBox();
            this.xposlbl = new System.Windows.Forms.Label();
            this.rotationcombo = new System.Windows.Forms.ComboBox();
            this.rotationlbl = new System.Windows.Forms.Label();
            this.idlbl = new System.Windows.Forms.Label();
            this.IDtxt = new System.Windows.Forms.TextBox();
            this.Okbtn = new System.Windows.Forms.Button();
            this.resizestylelbl = new System.Windows.Forms.Label();
            this.resizestylecombo = new System.Windows.Forms.ComboBox();
            this.keepratiocheck = new System.Windows.Forms.CheckBox();
            this.greyscalecheck = new System.Windows.Forms.CheckBox();
            this.valuelbl = new System.Windows.Forms.Label();
            this.valuetxt = new System.Windows.Forms.TextBox();
            this.autorotatecombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // manualwhcheck
            // 
            this.manualwhcheck.AutoSize = true;
            this.manualwhcheck.Location = new System.Drawing.Point(12, 224);
            this.manualwhcheck.Name = "manualwhcheck";
            this.manualwhcheck.Size = new System.Drawing.Size(163, 17);
            this.manualwhcheck.TabIndex = 10;
            this.manualwhcheck.Text = "Use manual width and height";
            this.manualwhcheck.UseVisualStyleBackColor = true;
            this.manualwhcheck.CheckedChanged += new System.EventHandler(this.manualwhcheck_CheckedChanged);
            // 
            // heighttxt
            // 
            this.heighttxt.Enabled = false;
            this.heighttxt.Location = new System.Drawing.Point(146, 247);
            this.heighttxt.Name = "heighttxt";
            this.heighttxt.Size = new System.Drawing.Size(40, 20);
            this.heighttxt.TabIndex = 12;
            // 
            // widthtxt
            // 
            this.widthtxt.Enabled = false;
            this.widthtxt.Location = new System.Drawing.Point(53, 247);
            this.widthtxt.Name = "widthtxt";
            this.widthtxt.Size = new System.Drawing.Size(40, 20);
            this.widthtxt.TabIndex = 11;
            // 
            // heightlbl
            // 
            this.heightlbl.AutoSize = true;
            this.heightlbl.Enabled = false;
            this.heightlbl.Location = new System.Drawing.Point(99, 250);
            this.heightlbl.Name = "heightlbl";
            this.heightlbl.Size = new System.Drawing.Size(41, 13);
            this.heightlbl.TabIndex = 31;
            this.heightlbl.Text = "Height:";
            // 
            // widthlbl
            // 
            this.widthlbl.AutoSize = true;
            this.widthlbl.Enabled = false;
            this.widthlbl.Location = new System.Drawing.Point(9, 250);
            this.widthlbl.Name = "widthlbl";
            this.widthlbl.Size = new System.Drawing.Size(38, 13);
            this.widthlbl.TabIndex = 30;
            this.widthlbl.Text = "Width:";
            // 
            // referencecombo
            // 
            this.referencecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.referencecombo.FormattingEnabled = true;
            this.referencecombo.Location = new System.Drawing.Point(78, 197);
            this.referencecombo.Name = "referencecombo";
            this.referencecombo.Size = new System.Drawing.Size(121, 21);
            this.referencecombo.TabIndex = 9;
            // 
            // referencelbl
            // 
            this.referencelbl.AutoSize = true;
            this.referencelbl.Location = new System.Drawing.Point(12, 200);
            this.referencelbl.Name = "referencelbl";
            this.referencelbl.Size = new System.Drawing.Size(60, 13);
            this.referencelbl.TabIndex = 28;
            this.referencelbl.Text = "Reference:";
            // 
            // YPostxt
            // 
            this.YPostxt.Location = new System.Drawing.Point(78, 85);
            this.YPostxt.Name = "YPostxt";
            this.YPostxt.Size = new System.Drawing.Size(40, 20);
            this.YPostxt.TabIndex = 4;
            // 
            // yposlbl
            // 
            this.yposlbl.AutoSize = true;
            this.yposlbl.Location = new System.Drawing.Point(12, 88);
            this.yposlbl.Name = "yposlbl";
            this.yposlbl.Size = new System.Drawing.Size(57, 13);
            this.yposlbl.TabIndex = 26;
            this.yposlbl.Text = "Y Position:";
            // 
            // XPostxt
            // 
            this.XPostxt.Location = new System.Drawing.Point(78, 59);
            this.XPostxt.Name = "XPostxt";
            this.XPostxt.Size = new System.Drawing.Size(40, 20);
            this.XPostxt.TabIndex = 3;
            // 
            // xposlbl
            // 
            this.xposlbl.AutoSize = true;
            this.xposlbl.Location = new System.Drawing.Point(12, 62);
            this.xposlbl.Name = "xposlbl";
            this.xposlbl.Size = new System.Drawing.Size(57, 13);
            this.xposlbl.TabIndex = 23;
            this.xposlbl.Text = "X Position:";
            // 
            // rotationcombo
            // 
            this.rotationcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rotationcombo.FormattingEnabled = true;
            this.rotationcombo.Location = new System.Drawing.Point(78, 111);
            this.rotationcombo.Name = "rotationcombo";
            this.rotationcombo.Size = new System.Drawing.Size(40, 21);
            this.rotationcombo.TabIndex = 5;
            // 
            // rotationlbl
            // 
            this.rotationlbl.AutoSize = true;
            this.rotationlbl.Location = new System.Drawing.Point(12, 114);
            this.rotationlbl.Name = "rotationlbl";
            this.rotationlbl.Size = new System.Drawing.Size(50, 13);
            this.rotationlbl.TabIndex = 21;
            this.rotationlbl.Text = "Rotation:";
            // 
            // idlbl
            // 
            this.idlbl.AutoSize = true;
            this.idlbl.Location = new System.Drawing.Point(12, 9);
            this.idlbl.Name = "idlbl";
            this.idlbl.Size = new System.Drawing.Size(24, 13);
            this.idlbl.TabIndex = 19;
            this.idlbl.Text = "ID: ";
            // 
            // IDtxt
            // 
            this.IDtxt.Enabled = false;
            this.IDtxt.Location = new System.Drawing.Point(78, 6);
            this.IDtxt.Name = "IDtxt";
            this.IDtxt.Size = new System.Drawing.Size(121, 20);
            this.IDtxt.TabIndex = 1;
            // 
            // Okbtn
            // 
            this.Okbtn.Location = new System.Drawing.Point(197, 244);
            this.Okbtn.Name = "Okbtn";
            this.Okbtn.Size = new System.Drawing.Size(75, 23);
            this.Okbtn.TabIndex = 13;
            this.Okbtn.Text = "Done";
            this.Okbtn.UseVisualStyleBackColor = true;
            this.Okbtn.Click += new System.EventHandler(this.Okbtn_Click);
            // 
            // resizestylelbl
            // 
            this.resizestylelbl.AutoSize = true;
            this.resizestylelbl.Location = new System.Drawing.Point(12, 142);
            this.resizestylelbl.Name = "resizestylelbl";
            this.resizestylelbl.Size = new System.Drawing.Size(69, 13);
            this.resizestylelbl.TabIndex = 32;
            this.resizestylelbl.Text = "Resize style: ";
            // 
            // resizestylecombo
            // 
            this.resizestylecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resizestylecombo.FormattingEnabled = true;
            this.resizestylecombo.Location = new System.Drawing.Point(78, 139);
            this.resizestylecombo.Name = "resizestylecombo";
            this.resizestylecombo.Size = new System.Drawing.Size(121, 21);
            this.resizestylecombo.TabIndex = 6;
            this.resizestylecombo.SelectedIndexChanged += new System.EventHandler(this.resizestylecombo_SelectedIndexChanged);
            // 
            // keepratiocheck
            // 
            this.keepratiocheck.AutoSize = true;
            this.keepratiocheck.Location = new System.Drawing.Point(13, 166);
            this.keepratiocheck.Name = "keepratiocheck";
            this.keepratiocheck.Size = new System.Drawing.Size(74, 17);
            this.keepratiocheck.TabIndex = 7;
            this.keepratiocheck.Text = "Keep ratio";
            this.keepratiocheck.UseVisualStyleBackColor = true;
            // 
            // greyscalecheck
            // 
            this.greyscalecheck.AutoSize = true;
            this.greyscalecheck.Location = new System.Drawing.Point(93, 166);
            this.greyscalecheck.Name = "greyscalecheck";
            this.greyscalecheck.Size = new System.Drawing.Size(73, 17);
            this.greyscalecheck.TabIndex = 8;
            this.greyscalecheck.Text = "Greyscale";
            this.greyscalecheck.UseVisualStyleBackColor = true;
            // 
            // valuelbl
            // 
            this.valuelbl.AutoSize = true;
            this.valuelbl.Location = new System.Drawing.Point(12, 36);
            this.valuelbl.Name = "valuelbl";
            this.valuelbl.Size = new System.Drawing.Size(40, 13);
            this.valuelbl.TabIndex = 33;
            this.valuelbl.Text = "Value: ";
            // 
            // valuetxt
            // 
            this.valuetxt.Enabled = false;
            this.valuetxt.Location = new System.Drawing.Point(78, 33);
            this.valuetxt.Name = "valuetxt";
            this.valuetxt.Size = new System.Drawing.Size(100, 20);
            this.valuetxt.TabIndex = 2;
            // 
            // autorotatecombo
            // 
            this.autorotatecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.autorotatecombo.FormattingEnabled = true;
            this.autorotatecombo.Location = new System.Drawing.Point(259, 164);
            this.autorotatecombo.Name = "autorotatecombo";
            this.autorotatecombo.Size = new System.Drawing.Size(121, 21);
            this.autorotatecombo.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Auto Rotate";
            // 
            // AddImageForm
            // 
            this.AcceptButton = this.Okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 279);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.autorotatecombo);
            this.Controls.Add(this.valuetxt);
            this.Controls.Add(this.valuelbl);
            this.Controls.Add(this.greyscalecheck);
            this.Controls.Add(this.keepratiocheck);
            this.Controls.Add(this.resizestylecombo);
            this.Controls.Add(this.resizestylelbl);
            this.Controls.Add(this.manualwhcheck);
            this.Controls.Add(this.heighttxt);
            this.Controls.Add(this.widthtxt);
            this.Controls.Add(this.heightlbl);
            this.Controls.Add(this.widthlbl);
            this.Controls.Add(this.referencecombo);
            this.Controls.Add(this.referencelbl);
            this.Controls.Add(this.YPostxt);
            this.Controls.Add(this.yposlbl);
            this.Controls.Add(this.XPostxt);
            this.Controls.Add(this.xposlbl);
            this.Controls.Add(this.rotationcombo);
            this.Controls.Add(this.rotationlbl);
            this.Controls.Add(this.idlbl);
            this.Controls.Add(this.IDtxt);
            this.Controls.Add(this.Okbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add an image";
            this.Load += new System.EventHandler(this.AddImageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox manualwhcheck;
        private System.Windows.Forms.TextBox heighttxt;
        private System.Windows.Forms.TextBox widthtxt;
        private System.Windows.Forms.Label heightlbl;
        private System.Windows.Forms.Label widthlbl;
        private System.Windows.Forms.ComboBox referencecombo;
        private System.Windows.Forms.Label referencelbl;
        private System.Windows.Forms.TextBox YPostxt;
        private System.Windows.Forms.Label yposlbl;
        private System.Windows.Forms.TextBox XPostxt;
        private System.Windows.Forms.Label xposlbl;
        private System.Windows.Forms.ComboBox rotationcombo;
        private System.Windows.Forms.Label rotationlbl;
        private System.Windows.Forms.Label idlbl;
        private System.Windows.Forms.TextBox IDtxt;
        private System.Windows.Forms.Button Okbtn;
        private System.Windows.Forms.Label resizestylelbl;
        private System.Windows.Forms.ComboBox resizestylecombo;
        private System.Windows.Forms.CheckBox keepratiocheck;
        private System.Windows.Forms.CheckBox greyscalecheck;
        private System.Windows.Forms.Label valuelbl;
        private System.Windows.Forms.TextBox valuetxt;
        private System.Windows.Forms.ComboBox autorotatecombo;
        private System.Windows.Forms.Label label1;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
