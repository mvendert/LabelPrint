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
    partial class AddTextFieldForm
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
            this.Okbtn = new System.Windows.Forms.Button();
            this.IDtxt = new System.Windows.Forms.TextBox();
            this.idlbl = new System.Windows.Forms.Label();
            this.Fontlbl = new System.Windows.Forms.Label();
            this.Fontcombo = new System.Windows.Forms.ComboBox();
            this.rotationlbl = new System.Windows.Forms.Label();
            this.rotationcombo = new System.Windows.Forms.ComboBox();
            this.xposlbl = new System.Windows.Forms.Label();
            this.XPostxt = new System.Windows.Forms.TextBox();
            this.YPostxt = new System.Windows.Forms.TextBox();
            this.yposlbl = new System.Windows.Forms.Label();
            this.referencelbl = new System.Windows.Forms.Label();
            this.referencecombo = new System.Windows.Forms.ComboBox();
            this.widthlbl = new System.Windows.Forms.Label();
            this.heightlbl = new System.Windows.Forms.Label();
            this.widthtxt = new System.Windows.Forms.TextBox();
            this.heighttxt = new System.Windows.Forms.TextBox();
            this.manualwhcheck = new System.Windows.Forms.CheckBox();
            this.alignlbl = new System.Windows.Forms.Label();
            this.alignTextcombo = new System.Windows.Forms.ComboBox();
            this.fontbtn = new System.Windows.Forms.Button();
            this.valuelbl = new System.Windows.Forms.Label();
            this.valuetxt = new System.Windows.Forms.TextBox();
            this.typecombo = new System.Windows.Forms.ComboBox();
            this.typelbl = new System.Windows.Forms.Label();
            this.formatStringlbl = new System.Windows.Forms.Label();
            this.formatstringlist = new System.Windows.Forms.ComboBox();
            this.addformatstringbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Okbtn
            // 
            this.Okbtn.Location = new System.Drawing.Point(203, 273);
            this.Okbtn.Name = "Okbtn";
            this.Okbtn.Size = new System.Drawing.Size(75, 23);
            this.Okbtn.TabIndex = 15;
            this.Okbtn.Text = "Done";
            this.Okbtn.UseVisualStyleBackColor = true;
            this.Okbtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // IDtxt
            // 
            this.IDtxt.Enabled = false;
            this.IDtxt.Location = new System.Drawing.Point(76, 8);
            this.IDtxt.Name = "IDtxt";
            this.IDtxt.Size = new System.Drawing.Size(121, 20);
            this.IDtxt.TabIndex = 0;
            // 
            // idlbl
            // 
            this.idlbl.AutoSize = true;
            this.idlbl.Location = new System.Drawing.Point(15, 11);
            this.idlbl.Name = "idlbl";
            this.idlbl.Size = new System.Drawing.Size(24, 13);
            this.idlbl.TabIndex = 2;
            this.idlbl.Text = "ID: ";
            // 
            // Fontlbl
            // 
            this.Fontlbl.AutoSize = true;
            this.Fontlbl.Location = new System.Drawing.Point(15, 116);
            this.Fontlbl.Name = "Fontlbl";
            this.Fontlbl.Size = new System.Drawing.Size(31, 13);
            this.Fontlbl.TabIndex = 3;
            this.Fontlbl.Text = "Font:";
            // 
            // Fontcombo
            // 
            this.Fontcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Fontcombo.FormattingEnabled = true;
            this.Fontcombo.Location = new System.Drawing.Point(76, 113);
            this.Fontcombo.Name = "Fontcombo";
            this.Fontcombo.Size = new System.Drawing.Size(121, 21);
            this.Fontcombo.TabIndex = 5;
            this.Fontcombo.DropDown += new System.EventHandler(this.fontcombo_DropDown);
            // 
            // rotationlbl
            // 
            this.rotationlbl.AutoSize = true;
            this.rotationlbl.Location = new System.Drawing.Point(15, 89);
            this.rotationlbl.Name = "rotationlbl";
            this.rotationlbl.Size = new System.Drawing.Size(50, 13);
            this.rotationlbl.TabIndex = 5;
            this.rotationlbl.Text = "Rotation:";
            // 
            // rotationcombo
            // 
            this.rotationcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rotationcombo.FormattingEnabled = true;
            this.rotationcombo.Location = new System.Drawing.Point(76, 86);
            this.rotationcombo.Name = "rotationcombo";
            this.rotationcombo.Size = new System.Drawing.Size(40, 21);
            this.rotationcombo.TabIndex = 4;
            // 
            // xposlbl
            // 
            this.xposlbl.AutoSize = true;
            this.xposlbl.Location = new System.Drawing.Point(15, 63);
            this.xposlbl.Name = "xposlbl";
            this.xposlbl.Size = new System.Drawing.Size(57, 13);
            this.xposlbl.TabIndex = 8;
            this.xposlbl.Text = "X Position:";
            // 
            // XPostxt
            // 
            this.XPostxt.Location = new System.Drawing.Point(76, 60);
            this.XPostxt.Name = "XPostxt";
            this.XPostxt.Size = new System.Drawing.Size(40, 20);
            this.XPostxt.TabIndex = 2;
            // 
            // YPostxt
            // 
            this.YPostxt.Location = new System.Drawing.Point(185, 60);
            this.YPostxt.Name = "YPostxt";
            this.YPostxt.Size = new System.Drawing.Size(40, 20);
            this.YPostxt.TabIndex = 3;
            // 
            // yposlbl
            // 
            this.yposlbl.AutoSize = true;
            this.yposlbl.Location = new System.Drawing.Point(122, 63);
            this.yposlbl.Name = "yposlbl";
            this.yposlbl.Size = new System.Drawing.Size(57, 13);
            this.yposlbl.TabIndex = 10;
            this.yposlbl.Text = "Y Position:";
            // 
            // referencelbl
            // 
            this.referencelbl.AutoSize = true;
            this.referencelbl.Location = new System.Drawing.Point(15, 170);
            this.referencelbl.Name = "referencelbl";
            this.referencelbl.Size = new System.Drawing.Size(60, 13);
            this.referencelbl.TabIndex = 12;
            this.referencelbl.Text = "Reference:";
            // 
            // referencecombo
            // 
            this.referencecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.referencecombo.FormattingEnabled = true;
            this.referencecombo.Location = new System.Drawing.Point(76, 167);
            this.referencecombo.Name = "referencecombo";
            this.referencecombo.Size = new System.Drawing.Size(121, 21);
            this.referencecombo.TabIndex = 8;
            // 
            // widthlbl
            // 
            this.widthlbl.AutoSize = true;
            this.widthlbl.Enabled = false;
            this.widthlbl.Location = new System.Drawing.Point(16, 278);
            this.widthlbl.Name = "widthlbl";
            this.widthlbl.Size = new System.Drawing.Size(38, 13);
            this.widthlbl.TabIndex = 14;
            this.widthlbl.Text = "Width:";
            // 
            // heightlbl
            // 
            this.heightlbl.AutoSize = true;
            this.heightlbl.Enabled = false;
            this.heightlbl.Location = new System.Drawing.Point(110, 278);
            this.heightlbl.Name = "heightlbl";
            this.heightlbl.Size = new System.Drawing.Size(41, 13);
            this.heightlbl.TabIndex = 15;
            this.heightlbl.Text = "Height:";
            // 
            // widthtxt
            // 
            this.widthtxt.Enabled = false;
            this.widthtxt.Location = new System.Drawing.Point(58, 275);
            this.widthtxt.Name = "widthtxt";
            this.widthtxt.Size = new System.Drawing.Size(40, 20);
            this.widthtxt.TabIndex = 13;
            // 
            // heighttxt
            // 
            this.heighttxt.Enabled = false;
            this.heighttxt.Location = new System.Drawing.Point(157, 275);
            this.heighttxt.Name = "heighttxt";
            this.heighttxt.Size = new System.Drawing.Size(40, 20);
            this.heighttxt.TabIndex = 14;
            // 
            // manualwhcheck
            // 
            this.manualwhcheck.AutoSize = true;
            this.manualwhcheck.Location = new System.Drawing.Point(18, 249);
            this.manualwhcheck.Name = "manualwhcheck";
            this.manualwhcheck.Size = new System.Drawing.Size(163, 17);
            this.manualwhcheck.TabIndex = 12;
            this.manualwhcheck.Text = "Use manual width and height";
            this.manualwhcheck.UseVisualStyleBackColor = true;
            this.manualwhcheck.CheckedChanged += new System.EventHandler(this.manualwhcheck_CheckedChanged);
            // 
            // alignlbl
            // 
            this.alignlbl.AutoSize = true;
            this.alignlbl.Location = new System.Drawing.Point(15, 143);
            this.alignlbl.Name = "alignlbl";
            this.alignlbl.Size = new System.Drawing.Size(57, 13);
            this.alignlbl.TabIndex = 19;
            this.alignlbl.Text = "Align Text:";
            // 
            // alignTextcombo
            // 
            this.alignTextcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignTextcombo.FormattingEnabled = true;
            this.alignTextcombo.Location = new System.Drawing.Point(76, 140);
            this.alignTextcombo.Name = "alignTextcombo";
            this.alignTextcombo.Size = new System.Drawing.Size(121, 21);
            this.alignTextcombo.TabIndex = 7;
            // 
            // fontbtn
            // 
            this.fontbtn.Location = new System.Drawing.Point(203, 113);
            this.fontbtn.Name = "fontbtn";
            this.fontbtn.Size = new System.Drawing.Size(75, 23);
            this.fontbtn.TabIndex = 6;
            this.fontbtn.Text = "Add Font";
            this.fontbtn.UseVisualStyleBackColor = true;
            this.fontbtn.Click += new System.EventHandler(this.fontbtn_Click);
            // 
            // valuelbl
            // 
            this.valuelbl.AutoSize = true;
            this.valuelbl.Location = new System.Drawing.Point(15, 37);
            this.valuelbl.Name = "valuelbl";
            this.valuelbl.Size = new System.Drawing.Size(37, 13);
            this.valuelbl.TabIndex = 20;
            this.valuelbl.Text = "Value:";
            // 
            // valuetxt
            // 
            this.valuetxt.Enabled = false;
            this.valuetxt.Location = new System.Drawing.Point(76, 34);
            this.valuetxt.Name = "valuetxt";
            this.valuetxt.Size = new System.Drawing.Size(121, 20);
            this.valuetxt.TabIndex = 1;
            // 
            // typecombo
            // 
            this.typecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typecombo.FormattingEnabled = true;
            this.typecombo.Location = new System.Drawing.Point(76, 195);
            this.typecombo.Name = "typecombo";
            this.typecombo.Size = new System.Drawing.Size(121, 21);
            this.typecombo.TabIndex = 9;
            this.typecombo.SelectedIndexChanged += new System.EventHandler(this.typecombo_SelectedIndexChanged);
            // 
            // typelbl
            // 
            this.typelbl.AutoSize = true;
            this.typelbl.Location = new System.Drawing.Point(16, 198);
            this.typelbl.Name = "typelbl";
            this.typelbl.Size = new System.Drawing.Size(37, 13);
            this.typelbl.TabIndex = 23;
            this.typelbl.Text = "Type: ";
            // 
            // formatStringlbl
            // 
            this.formatStringlbl.AutoSize = true;
            this.formatStringlbl.Enabled = false;
            this.formatStringlbl.Location = new System.Drawing.Point(15, 226);
            this.formatStringlbl.Name = "formatStringlbl";
            this.formatStringlbl.Size = new System.Drawing.Size(42, 13);
            this.formatStringlbl.TabIndex = 24;
            this.formatStringlbl.Text = "Format:";
            // 
            // formatstringlist
            // 
            this.formatstringlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatstringlist.FormattingEnabled = true;
            this.formatstringlist.Location = new System.Drawing.Point(76, 223);
            this.formatstringlist.Name = "formatstringlist";
            this.formatstringlist.Size = new System.Drawing.Size(121, 21);
            this.formatstringlist.TabIndex = 10;
            // 
            // addformatstringbtn
            // 
            this.addformatstringbtn.Enabled = false;
            this.addformatstringbtn.Location = new System.Drawing.Point(203, 221);
            this.addformatstringbtn.Name = "addformatstringbtn";
            this.addformatstringbtn.Size = new System.Drawing.Size(75, 23);
            this.addformatstringbtn.TabIndex = 11;
            this.addformatstringbtn.Text = "Add Format";
            this.addformatstringbtn.UseVisualStyleBackColor = true;
            this.addformatstringbtn.Click += new System.EventHandler(this.addformatstringbtn_Click);
            // 
            // AddTextFieldForm
            // 
            this.AcceptButton = this.Okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 303);
            this.Controls.Add(this.addformatstringbtn);
            this.Controls.Add(this.formatstringlist);
            this.Controls.Add(this.formatStringlbl);
            this.Controls.Add(this.typelbl);
            this.Controls.Add(this.typecombo);
            this.Controls.Add(this.valuetxt);
            this.Controls.Add(this.valuelbl);
            this.Controls.Add(this.fontbtn);
            this.Controls.Add(this.alignTextcombo);
            this.Controls.Add(this.alignlbl);
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
            this.Controls.Add(this.Fontcombo);
            this.Controls.Add(this.Fontlbl);
            this.Controls.Add(this.idlbl);
            this.Controls.Add(this.IDtxt);
            this.Controls.Add(this.Okbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTextFieldForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a textfield";
            this.Load += new System.EventHandler(this.AddTextFieldForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Okbtn;
        private System.Windows.Forms.TextBox IDtxt;
        private System.Windows.Forms.Label idlbl;
        private System.Windows.Forms.Label Fontlbl;
        private System.Windows.Forms.ComboBox Fontcombo;
        private System.Windows.Forms.Label rotationlbl;
        private System.Windows.Forms.ComboBox rotationcombo;
        private System.Windows.Forms.Label xposlbl;
        private System.Windows.Forms.TextBox XPostxt;
        private System.Windows.Forms.TextBox YPostxt;
        private System.Windows.Forms.Label yposlbl;
        private System.Windows.Forms.Label referencelbl;
        private System.Windows.Forms.ComboBox referencecombo;
        private System.Windows.Forms.Label widthlbl;
        private System.Windows.Forms.Label heightlbl;
        private System.Windows.Forms.TextBox widthtxt;
        private System.Windows.Forms.TextBox heighttxt;
        private System.Windows.Forms.CheckBox manualwhcheck;
        private System.Windows.Forms.Label alignlbl;
        private System.Windows.Forms.ComboBox alignTextcombo;
        private System.Windows.Forms.Button fontbtn;
        private System.Windows.Forms.Label valuelbl;
        private System.Windows.Forms.TextBox valuetxt;
        private System.Windows.Forms.ComboBox typecombo;
        private System.Windows.Forms.Label typelbl;
        private System.Windows.Forms.Label formatStringlbl;
        private System.Windows.Forms.ComboBox formatstringlist;
        private System.Windows.Forms.Button addformatstringbtn;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
