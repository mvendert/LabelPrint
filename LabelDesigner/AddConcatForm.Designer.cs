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
    partial class AddConcatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddConcatForm));
            this.availabletextfieldslist = new System.Windows.Forms.ListBox();
            this.availabletextfieldslbl = new System.Windows.Forms.Label();
            this.addedtextfieldslist = new System.Windows.Forms.ListBox();
            this.addeditemslbl = new System.Windows.Forms.Label();
            this.addtextfieldbtn = new System.Windows.Forms.Button();
            this.removetextfieldbtn = new System.Windows.Forms.Button();
            this.alignTextcombo = new System.Windows.Forms.ComboBox();
            this.alignlbl = new System.Windows.Forms.Label();
            this.manualwhcheck = new System.Windows.Forms.CheckBox();
            this.heighttxt = new System.Windows.Forms.TextBox();
            this.widthtxt = new System.Windows.Forms.TextBox();
            this.heightlbl = new System.Windows.Forms.Label();
            this.widthlbl = new System.Windows.Forms.Label();
            this.YPostxt = new System.Windows.Forms.TextBox();
            this.yposlbl = new System.Windows.Forms.Label();
            this.XPostxt = new System.Windows.Forms.TextBox();
            this.xposlbl = new System.Windows.Forms.Label();
            this.rotationcombo = new System.Windows.Forms.ComboBox();
            this.rotationlbl = new System.Windows.Forms.Label();
            this.idlbl = new System.Windows.Forms.Label();
            this.IDtxt = new System.Windows.Forms.TextBox();
            this.Okbtn = new System.Windows.Forms.Button();
            this.concatmethodlbl = new System.Windows.Forms.Label();
            this.concatmethodcombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // availabletextfieldslist
            // 
            this.availabletextfieldslist.FormattingEnabled = true;
            this.availabletextfieldslist.HorizontalScrollbar = true;
            this.availabletextfieldslist.Location = new System.Drawing.Point(295, 24);
            this.availabletextfieldslist.Name = "availabletextfieldslist";
            this.availabletextfieldslist.Size = new System.Drawing.Size(287, 82);
            this.availabletextfieldslist.TabIndex = 8;
            // 
            // availabletextfieldslbl
            // 
            this.availabletextfieldslbl.AutoSize = true;
            this.availabletextfieldslbl.Location = new System.Drawing.Point(292, 8);
            this.availabletextfieldslbl.Name = "availabletextfieldslbl";
            this.availabletextfieldslbl.Size = new System.Drawing.Size(97, 13);
            this.availabletextfieldslbl.TabIndex = 1;
            this.availabletextfieldslbl.Text = "Available textfields:";
            // 
            // addedtextfieldslist
            // 
            this.addedtextfieldslist.FormattingEnabled = true;
            this.addedtextfieldslist.HorizontalScrollbar = true;
            this.addedtextfieldslist.Location = new System.Drawing.Point(295, 135);
            this.addedtextfieldslist.Name = "addedtextfieldslist";
            this.addedtextfieldslist.Size = new System.Drawing.Size(287, 82);
            this.addedtextfieldslist.TabIndex = 9;
            // 
            // addeditemslbl
            // 
            this.addeditemslbl.AutoSize = true;
            this.addeditemslbl.Location = new System.Drawing.Point(292, 119);
            this.addeditemslbl.Name = "addeditemslbl";
            this.addeditemslbl.Size = new System.Drawing.Size(85, 13);
            this.addeditemslbl.TabIndex = 3;
            this.addeditemslbl.Text = "Added textfields:";
            // 
            // addtextfieldbtn
            // 
            this.addtextfieldbtn.Image = ((System.Drawing.Image)(resources.GetObject("addtextfieldbtn.Image")));
            this.addtextfieldbtn.Location = new System.Drawing.Point(270, 60);
            this.addtextfieldbtn.Name = "addtextfieldbtn";
            this.addtextfieldbtn.Size = new System.Drawing.Size(19, 20);
            this.addtextfieldbtn.TabIndex = 4;
            this.addtextfieldbtn.UseVisualStyleBackColor = true;
            this.addtextfieldbtn.Click += new System.EventHandler(this.addtextfieldbtn_Click);
            // 
            // removetextfieldbtn
            // 
            this.removetextfieldbtn.Image = ((System.Drawing.Image)(resources.GetObject("removetextfieldbtn.Image")));
            this.removetextfieldbtn.Location = new System.Drawing.Point(270, 162);
            this.removetextfieldbtn.Name = "removetextfieldbtn";
            this.removetextfieldbtn.Size = new System.Drawing.Size(19, 20);
            this.removetextfieldbtn.TabIndex = 5;
            this.removetextfieldbtn.UseVisualStyleBackColor = true;
            this.removetextfieldbtn.Click += new System.EventHandler(this.removetextfieldbtn_Click);
            // 
            // alignTextcombo
            // 
            this.alignTextcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignTextcombo.FormattingEnabled = true;
            this.alignTextcombo.Location = new System.Drawing.Point(69, 105);
            this.alignTextcombo.Name = "alignTextcombo";
            this.alignTextcombo.Size = new System.Drawing.Size(121, 21);
            this.alignTextcombo.TabIndex = 4;
            // 
            // alignlbl
            // 
            this.alignlbl.AutoSize = true;
            this.alignlbl.Location = new System.Drawing.Point(10, 108);
            this.alignlbl.Name = "alignlbl";
            this.alignlbl.Size = new System.Drawing.Size(57, 13);
            this.alignlbl.TabIndex = 38;
            this.alignlbl.Text = "Align Text:";
            // 
            // manualwhcheck
            // 
            this.manualwhcheck.AutoSize = true;
            this.manualwhcheck.Location = new System.Drawing.Point(12, 178);
            this.manualwhcheck.Name = "manualwhcheck";
            this.manualwhcheck.Size = new System.Drawing.Size(163, 17);
            this.manualwhcheck.TabIndex = 31;
            this.manualwhcheck.Text = "Use manual width and height";
            this.manualwhcheck.UseVisualStyleBackColor = true;
            this.manualwhcheck.CheckedChanged += new System.EventHandler(this.manualwhcheck_CheckedChanged);
            // 
            // heighttxt
            // 
            this.heighttxt.Enabled = false;
            this.heighttxt.Location = new System.Drawing.Point(162, 201);
            this.heighttxt.Name = "heighttxt";
            this.heighttxt.Size = new System.Drawing.Size(40, 20);
            this.heighttxt.TabIndex = 7;
            // 
            // widthtxt
            // 
            this.widthtxt.Enabled = false;
            this.widthtxt.Location = new System.Drawing.Point(69, 201);
            this.widthtxt.Name = "widthtxt";
            this.widthtxt.Size = new System.Drawing.Size(40, 20);
            this.widthtxt.TabIndex = 6;
            // 
            // heightlbl
            // 
            this.heightlbl.AutoSize = true;
            this.heightlbl.Enabled = false;
            this.heightlbl.Location = new System.Drawing.Point(115, 204);
            this.heightlbl.Name = "heightlbl";
            this.heightlbl.Size = new System.Drawing.Size(41, 13);
            this.heightlbl.TabIndex = 37;
            this.heightlbl.Text = "Height:";
            // 
            // widthlbl
            // 
            this.widthlbl.AutoSize = true;
            this.widthlbl.Enabled = false;
            this.widthlbl.Location = new System.Drawing.Point(10, 204);
            this.widthlbl.Name = "widthlbl";
            this.widthlbl.Size = new System.Drawing.Size(38, 13);
            this.widthlbl.TabIndex = 36;
            this.widthlbl.Text = "Width:";
            // 
            // YPostxt
            // 
            this.YPostxt.Location = new System.Drawing.Point(178, 38);
            this.YPostxt.Name = "YPostxt";
            this.YPostxt.Size = new System.Drawing.Size(40, 20);
            this.YPostxt.TabIndex = 2;
            // 
            // yposlbl
            // 
            this.yposlbl.AutoSize = true;
            this.yposlbl.Location = new System.Drawing.Point(115, 41);
            this.yposlbl.Name = "yposlbl";
            this.yposlbl.Size = new System.Drawing.Size(57, 13);
            this.yposlbl.TabIndex = 34;
            this.yposlbl.Text = "Y Position:";
            // 
            // XPostxt
            // 
            this.XPostxt.Location = new System.Drawing.Point(69, 38);
            this.XPostxt.Name = "XPostxt";
            this.XPostxt.Size = new System.Drawing.Size(40, 20);
            this.XPostxt.TabIndex = 1;
            // 
            // xposlbl
            // 
            this.xposlbl.AutoSize = true;
            this.xposlbl.Location = new System.Drawing.Point(10, 41);
            this.xposlbl.Name = "xposlbl";
            this.xposlbl.Size = new System.Drawing.Size(57, 13);
            this.xposlbl.TabIndex = 30;
            this.xposlbl.Text = "X Position:";
            // 
            // rotationcombo
            // 
            this.rotationcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rotationcombo.FormattingEnabled = true;
            this.rotationcombo.Location = new System.Drawing.Point(69, 78);
            this.rotationcombo.Name = "rotationcombo";
            this.rotationcombo.Size = new System.Drawing.Size(40, 21);
            this.rotationcombo.TabIndex = 3;
            // 
            // rotationlbl
            // 
            this.rotationlbl.AutoSize = true;
            this.rotationlbl.Location = new System.Drawing.Point(10, 81);
            this.rotationlbl.Name = "rotationlbl";
            this.rotationlbl.Size = new System.Drawing.Size(50, 13);
            this.rotationlbl.TabIndex = 27;
            this.rotationlbl.Text = "Rotation:";
            // 
            // idlbl
            // 
            this.idlbl.AutoSize = true;
            this.idlbl.Location = new System.Drawing.Point(10, 15);
            this.idlbl.Name = "idlbl";
            this.idlbl.Size = new System.Drawing.Size(24, 13);
            this.idlbl.TabIndex = 22;
            this.idlbl.Text = "ID: ";
            // 
            // IDtxt
            // 
            this.IDtxt.Location = new System.Drawing.Point(69, 12);
            this.IDtxt.Name = "IDtxt";
            this.IDtxt.Size = new System.Drawing.Size(121, 20);
            this.IDtxt.TabIndex = 0;
            // 
            // Okbtn
            // 
            this.Okbtn.Enabled = false;
            this.Okbtn.Location = new System.Drawing.Point(507, 225);
            this.Okbtn.Name = "Okbtn";
            this.Okbtn.Size = new System.Drawing.Size(75, 23);
            this.Okbtn.TabIndex = 10;
            this.Okbtn.Text = "Done";
            this.Okbtn.UseVisualStyleBackColor = true;
            this.Okbtn.Click += new System.EventHandler(this.Okbtn_Click);
            // 
            // concatmethodlbl
            // 
            this.concatmethodlbl.AutoSize = true;
            this.concatmethodlbl.Location = new System.Drawing.Point(10, 135);
            this.concatmethodlbl.Name = "concatmethodlbl";
            this.concatmethodlbl.Size = new System.Drawing.Size(83, 13);
            this.concatmethodlbl.TabIndex = 39;
            this.concatmethodlbl.Text = "Concat Method:";
            // 
            // concatmethodcombo
            // 
            this.concatmethodcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.concatmethodcombo.FormattingEnabled = true;
            this.concatmethodcombo.Location = new System.Drawing.Point(102, 132);
            this.concatmethodcombo.Name = "concatmethodcombo";
            this.concatmethodcombo.Size = new System.Drawing.Size(88, 21);
            this.concatmethodcombo.TabIndex = 5;
            // 
            // AddConcatForm
            // 
            this.AcceptButton = this.Okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 260);
            this.Controls.Add(this.concatmethodcombo);
            this.Controls.Add(this.concatmethodlbl);
            this.Controls.Add(this.alignTextcombo);
            this.Controls.Add(this.alignlbl);
            this.Controls.Add(this.manualwhcheck);
            this.Controls.Add(this.heighttxt);
            this.Controls.Add(this.widthtxt);
            this.Controls.Add(this.heightlbl);
            this.Controls.Add(this.widthlbl);
            this.Controls.Add(this.YPostxt);
            this.Controls.Add(this.yposlbl);
            this.Controls.Add(this.XPostxt);
            this.Controls.Add(this.xposlbl);
            this.Controls.Add(this.rotationcombo);
            this.Controls.Add(this.rotationlbl);
            this.Controls.Add(this.idlbl);
            this.Controls.Add(this.IDtxt);
            this.Controls.Add(this.Okbtn);
            this.Controls.Add(this.removetextfieldbtn);
            this.Controls.Add(this.addtextfieldbtn);
            this.Controls.Add(this.addeditemslbl);
            this.Controls.Add(this.addedtextfieldslist);
            this.Controls.Add(this.availabletextfieldslbl);
            this.Controls.Add(this.availabletextfieldslist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddConcatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddConcatForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox availabletextfieldslist;
        private System.Windows.Forms.Label availabletextfieldslbl;
        private System.Windows.Forms.ListBox addedtextfieldslist;
        private System.Windows.Forms.Label addeditemslbl;
        private System.Windows.Forms.Button addtextfieldbtn;
        private System.Windows.Forms.Button removetextfieldbtn;
        private System.Windows.Forms.ComboBox alignTextcombo;
        private System.Windows.Forms.Label alignlbl;
        private System.Windows.Forms.CheckBox manualwhcheck;
        private System.Windows.Forms.TextBox heighttxt;
        private System.Windows.Forms.TextBox widthtxt;
        private System.Windows.Forms.Label heightlbl;
        private System.Windows.Forms.Label widthlbl;
        private System.Windows.Forms.TextBox YPostxt;
        private System.Windows.Forms.Label yposlbl;
        private System.Windows.Forms.TextBox XPostxt;
        private System.Windows.Forms.Label xposlbl;
        private System.Windows.Forms.ComboBox rotationcombo;
        private System.Windows.Forms.Label rotationlbl;
        private System.Windows.Forms.Label idlbl;
        private System.Windows.Forms.TextBox IDtxt;
        private System.Windows.Forms.Button Okbtn;
        private System.Windows.Forms.Label concatmethodlbl;
        private System.Windows.Forms.ComboBox concatmethodcombo;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
