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
    partial class AddBarcodeForm
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
            this.YPostxt = new System.Windows.Forms.TextBox();
            this.yposlbl = new System.Windows.Forms.Label();
            this.XPostxt = new System.Windows.Forms.TextBox();
            this.xposlbl = new System.Windows.Forms.Label();
            this.rotationcombo = new System.Windows.Forms.ComboBox();
            this.rotationlbl = new System.Windows.Forms.Label();
            this.idlbl = new System.Windows.Forms.Label();
            this.IDtxt = new System.Windows.Forms.TextBox();
            this.alignTextcombo = new System.Windows.Forms.ComboBox();
            this.alignlbl = new System.Windows.Forms.Label();
            this.heighttxt = new System.Windows.Forms.TextBox();
            this.widthtxt = new System.Windows.Forms.TextBox();
            this.heightlbl = new System.Windows.Forms.Label();
            this.widthlbl = new System.Windows.Forms.Label();
            this.maxcharcountlbl = new System.Windows.Forms.Label();
            this.maxcharcounttxt = new System.Windows.Forms.TextBox();
            this.Okbtn = new System.Windows.Forms.Button();
            this.typelbl = new System.Windows.Forms.Label();
            this.typecombo = new System.Windows.Forms.ComboBox();
            this.maxcharcountcheck = new System.Windows.Forms.CheckBox();
            this.referencecombo = new System.Windows.Forms.ComboBox();
            this.referencelbl = new System.Windows.Forms.Label();
            this.valuelbl = new System.Windows.Forms.Label();
            this.valuetxt = new System.Windows.Forms.TextBox();
            this.showtextcheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // YPostxt
            // 
            this.YPostxt.Location = new System.Drawing.Point(72, 113);
            this.YPostxt.Name = "YPostxt";
            this.YPostxt.Size = new System.Drawing.Size(40, 20);
            this.YPostxt.TabIndex = 4;
            // 
            // yposlbl
            // 
            this.yposlbl.AutoSize = true;
            this.yposlbl.Location = new System.Drawing.Point(11, 116);
            this.yposlbl.Name = "yposlbl";
            this.yposlbl.Size = new System.Drawing.Size(57, 13);
            this.yposlbl.TabIndex = 18;
            this.yposlbl.Text = "Y Position:";
            // 
            // XPostxt
            // 
            this.XPostxt.Location = new System.Drawing.Point(72, 87);
            this.XPostxt.Name = "XPostxt";
            this.XPostxt.Size = new System.Drawing.Size(40, 20);
            this.XPostxt.TabIndex = 3;
            // 
            // xposlbl
            // 
            this.xposlbl.AutoSize = true;
            this.xposlbl.Location = new System.Drawing.Point(11, 90);
            this.xposlbl.Name = "xposlbl";
            this.xposlbl.Size = new System.Drawing.Size(57, 13);
            this.xposlbl.TabIndex = 17;
            this.xposlbl.Text = "X Position:";
            // 
            // rotationcombo
            // 
            this.rotationcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rotationcombo.FormattingEnabled = true;
            this.rotationcombo.Location = new System.Drawing.Point(72, 139);
            this.rotationcombo.Name = "rotationcombo";
            this.rotationcombo.Size = new System.Drawing.Size(40, 21);
            this.rotationcombo.TabIndex = 5;
            // 
            // rotationlbl
            // 
            this.rotationlbl.AutoSize = true;
            this.rotationlbl.Location = new System.Drawing.Point(11, 142);
            this.rotationlbl.Name = "rotationlbl";
            this.rotationlbl.Size = new System.Drawing.Size(50, 13);
            this.rotationlbl.TabIndex = 16;
            this.rotationlbl.Text = "Rotation:";
            // 
            // idlbl
            // 
            this.idlbl.AutoSize = true;
            this.idlbl.Location = new System.Drawing.Point(12, 9);
            this.idlbl.Name = "idlbl";
            this.idlbl.Size = new System.Drawing.Size(24, 13);
            this.idlbl.TabIndex = 13;
            this.idlbl.Text = "ID: ";
            // 
            // IDtxt
            // 
            this.IDtxt.Enabled = false;
            this.IDtxt.Location = new System.Drawing.Point(73, 6);
            this.IDtxt.Name = "IDtxt";
            this.IDtxt.Size = new System.Drawing.Size(121, 20);
            this.IDtxt.TabIndex = 0;
            // 
            // alignTextcombo
            // 
            this.alignTextcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignTextcombo.FormattingEnabled = true;
            this.alignTextcombo.Location = new System.Drawing.Point(72, 168);
            this.alignTextcombo.Name = "alignTextcombo";
            this.alignTextcombo.Size = new System.Drawing.Size(121, 21);
            this.alignTextcombo.TabIndex = 6;
            // 
            // alignlbl
            // 
            this.alignlbl.AutoSize = true;
            this.alignlbl.Location = new System.Drawing.Point(11, 171);
            this.alignlbl.Name = "alignlbl";
            this.alignlbl.Size = new System.Drawing.Size(57, 13);
            this.alignlbl.TabIndex = 21;
            this.alignlbl.Text = "Align Text:";
            // 
            // heighttxt
            // 
            this.heighttxt.Location = new System.Drawing.Point(153, 197);
            this.heighttxt.Name = "heighttxt";
            this.heighttxt.Size = new System.Drawing.Size(40, 20);
            this.heighttxt.TabIndex = 8;
            // 
            // widthtxt
            // 
            this.widthtxt.Location = new System.Drawing.Point(55, 197);
            this.widthtxt.Name = "widthtxt";
            this.widthtxt.Size = new System.Drawing.Size(40, 20);
            this.widthtxt.TabIndex = 7;
            // 
            // heightlbl
            // 
            this.heightlbl.AutoSize = true;
            this.heightlbl.Location = new System.Drawing.Point(106, 200);
            this.heightlbl.Name = "heightlbl";
            this.heightlbl.Size = new System.Drawing.Size(41, 13);
            this.heightlbl.TabIndex = 25;
            this.heightlbl.Text = "Height:";
            // 
            // widthlbl
            // 
            this.widthlbl.AutoSize = true;
            this.widthlbl.Location = new System.Drawing.Point(11, 200);
            this.widthlbl.Name = "widthlbl";
            this.widthlbl.Size = new System.Drawing.Size(38, 13);
            this.widthlbl.TabIndex = 24;
            this.widthlbl.Text = "Width:";
            // 
            // maxcharcountlbl
            // 
            this.maxcharcountlbl.AutoSize = true;
            this.maxcharcountlbl.Enabled = false;
            this.maxcharcountlbl.Location = new System.Drawing.Point(11, 274);
            this.maxcharcountlbl.Name = "maxcharcountlbl";
            this.maxcharcountlbl.Size = new System.Drawing.Size(79, 13);
            this.maxcharcountlbl.TabIndex = 26;
            this.maxcharcountlbl.Text = "Character limit: ";
            // 
            // maxcharcounttxt
            // 
            this.maxcharcounttxt.Enabled = false;
            this.maxcharcounttxt.Location = new System.Drawing.Point(96, 271);
            this.maxcharcounttxt.Name = "maxcharcounttxt";
            this.maxcharcounttxt.Size = new System.Drawing.Size(38, 20);
            this.maxcharcounttxt.TabIndex = 12;
            // 
            // Okbtn
            // 
            this.Okbtn.Location = new System.Drawing.Point(197, 273);
            this.Okbtn.Name = "Okbtn";
            this.Okbtn.Size = new System.Drawing.Size(75, 23);
            this.Okbtn.TabIndex = 13;
            this.Okbtn.Text = "Done";
            this.Okbtn.UseVisualStyleBackColor = true;
            this.Okbtn.Click += new System.EventHandler(this.Okbtn_Click);
            // 
            // typelbl
            // 
            this.typelbl.AutoSize = true;
            this.typelbl.Location = new System.Drawing.Point(11, 60);
            this.typelbl.Name = "typelbl";
            this.typelbl.Size = new System.Drawing.Size(34, 13);
            this.typelbl.TabIndex = 29;
            this.typelbl.Text = "Type:";
            // 
            // typecombo
            // 
            this.typecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typecombo.FormattingEnabled = true;
            this.typecombo.Location = new System.Drawing.Point(72, 57);
            this.typecombo.Name = "typecombo";
            this.typecombo.Size = new System.Drawing.Size(121, 21);
            this.typecombo.TabIndex = 2;
            this.typecombo.SelectedIndexChanged += new System.EventHandler(this.typecombo_SelectedIndexChanged);
            // 
            // maxcharcountcheck
            // 
            this.maxcharcountcheck.AutoSize = true;
            this.maxcharcountcheck.Location = new System.Drawing.Point(14, 250);
            this.maxcharcountcheck.Name = "maxcharcountcheck";
            this.maxcharcountcheck.Size = new System.Drawing.Size(113, 17);
            this.maxcharcountcheck.TabIndex = 10;
            this.maxcharcountcheck.Text = "Use character limit";
            this.maxcharcountcheck.UseVisualStyleBackColor = true;
            this.maxcharcountcheck.CheckedChanged += new System.EventHandler(this.maxcharcountcheck_CheckedChanged);
            // 
            // referencecombo
            // 
            this.referencecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.referencecombo.FormattingEnabled = true;
            this.referencecombo.Location = new System.Drawing.Point(72, 223);
            this.referencecombo.Name = "referencecombo";
            this.referencecombo.Size = new System.Drawing.Size(121, 21);
            this.referencecombo.TabIndex = 9;
            // 
            // referencelbl
            // 
            this.referencelbl.AutoSize = true;
            this.referencelbl.Location = new System.Drawing.Point(11, 226);
            this.referencelbl.Name = "referencelbl";
            this.referencelbl.Size = new System.Drawing.Size(60, 13);
            this.referencelbl.TabIndex = 33;
            this.referencelbl.Text = "Reference:";
            // 
            // valuelbl
            // 
            this.valuelbl.AutoSize = true;
            this.valuelbl.Location = new System.Drawing.Point(12, 34);
            this.valuelbl.Name = "valuelbl";
            this.valuelbl.Size = new System.Drawing.Size(37, 13);
            this.valuelbl.TabIndex = 34;
            this.valuelbl.Text = "Value:";
            // 
            // valuetxt
            // 
            this.valuetxt.Enabled = false;
            this.valuetxt.Location = new System.Drawing.Point(72, 31);
            this.valuetxt.Name = "valuetxt";
            this.valuetxt.Size = new System.Drawing.Size(121, 20);
            this.valuetxt.TabIndex = 1;
            // 
            // showtextcheck
            // 
            this.showtextcheck.AutoSize = true;
            this.showtextcheck.Checked = true;
            this.showtextcheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showtextcheck.Location = new System.Drawing.Point(134, 250);
            this.showtextcheck.Name = "showtextcheck";
            this.showtextcheck.Size = new System.Drawing.Size(73, 17);
            this.showtextcheck.TabIndex = 11;
            this.showtextcheck.Text = "Show text";
            this.showtextcheck.UseVisualStyleBackColor = true;
            // 
            // AddBarcodeForm
            // 
            this.AcceptButton = this.Okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 308);
            this.Controls.Add(this.showtextcheck);
            this.Controls.Add(this.valuetxt);
            this.Controls.Add(this.valuelbl);
            this.Controls.Add(this.referencecombo);
            this.Controls.Add(this.referencelbl);
            this.Controls.Add(this.maxcharcountcheck);
            this.Controls.Add(this.typecombo);
            this.Controls.Add(this.typelbl);
            this.Controls.Add(this.Okbtn);
            this.Controls.Add(this.maxcharcounttxt);
            this.Controls.Add(this.maxcharcountlbl);
            this.Controls.Add(this.heighttxt);
            this.Controls.Add(this.widthtxt);
            this.Controls.Add(this.heightlbl);
            this.Controls.Add(this.widthlbl);
            this.Controls.Add(this.alignTextcombo);
            this.Controls.Add(this.alignlbl);
            this.Controls.Add(this.YPostxt);
            this.Controls.Add(this.yposlbl);
            this.Controls.Add(this.XPostxt);
            this.Controls.Add(this.xposlbl);
            this.Controls.Add(this.rotationcombo);
            this.Controls.Add(this.rotationlbl);
            this.Controls.Add(this.idlbl);
            this.Controls.Add(this.IDtxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddBarcodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddBarcodeForm";
            this.Load += new System.EventHandler(this.AddBarcodeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox YPostxt;
        private System.Windows.Forms.Label yposlbl;
        private System.Windows.Forms.TextBox XPostxt;
        private System.Windows.Forms.Label xposlbl;
        private System.Windows.Forms.ComboBox rotationcombo;
        private System.Windows.Forms.Label rotationlbl;
        private System.Windows.Forms.Label idlbl;
        private System.Windows.Forms.TextBox IDtxt;
        private System.Windows.Forms.ComboBox alignTextcombo;
        private System.Windows.Forms.Label alignlbl;
        private System.Windows.Forms.TextBox heighttxt;
        private System.Windows.Forms.TextBox widthtxt;
        private System.Windows.Forms.Label heightlbl;
        private System.Windows.Forms.Label widthlbl;
        private System.Windows.Forms.Label maxcharcountlbl;
        private System.Windows.Forms.TextBox maxcharcounttxt;
        private System.Windows.Forms.Button Okbtn;
        private System.Windows.Forms.Label typelbl;
        private System.Windows.Forms.ComboBox typecombo;
        private System.Windows.Forms.CheckBox maxcharcountcheck;
        private System.Windows.Forms.ComboBox referencecombo;
        private System.Windows.Forms.Label referencelbl;
        private System.Windows.Forms.Label valuelbl;
        private System.Windows.Forms.TextBox valuetxt;
        private System.Windows.Forms.CheckBox showtextcheck;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
