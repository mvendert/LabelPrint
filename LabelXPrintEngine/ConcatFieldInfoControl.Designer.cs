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
    partial class ConcatFieldInfoControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConcatFieldInfoControl));
            this.alignTextcombo = new System.Windows.Forms.ComboBox();
            this.aligntxtlbl = new System.Windows.Forms.Label();
            this.concatmethodcombo = new System.Windows.Forms.ComboBox();
            this.concatmethodlbl = new System.Windows.Forms.Label();
            this.linkedtextfieldslbl = new System.Windows.Forms.Label();
            this.linkedtextfieldslist = new System.Windows.Forms.ListBox();
            this.fontlbl = new System.Windows.Forms.Label();
            this.Fontcombo = new System.Windows.Forms.ComboBox();
            this.formatlbl = new System.Windows.Forms.Label();
            this.formattxt = new System.Windows.Forms.TextBox();
            this.backbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // manualwhcheck
            // 
            this.manualwhcheck.Location = new System.Drawing.Point(6, 88);
            this.manualwhcheck.TabIndex = 3;
            // 
            // heighttxt
            // 
            this.heighttxt.Location = new System.Drawing.Point(140, 111);
            this.heighttxt.TabIndex = 5;
            // 
            // widthtxt
            // 
            this.widthtxt.Location = new System.Drawing.Point(47, 111);
            this.widthtxt.TabIndex = 4;
            // 
            // heightlbl
            // 
            this.heightlbl.Location = new System.Drawing.Point(93, 114);
            // 
            // widthlbl
            // 
            this.widthlbl.Location = new System.Drawing.Point(3, 114);
            // 
            // referencecombo
            // 
            this.referencecombo.Location = new System.Drawing.Point(69, 191);
            this.referencecombo.TabIndex = 9;
            this.referencecombo.Visible = false;
            // 
            // referencelbl
            // 
            this.referencelbl.Location = new System.Drawing.Point(3, 194);
            this.referencelbl.Visible = false;
            // 
            // moveleftbtn
            // 
            this.moveleftbtn.TabIndex = 13;
            // 
            // moverightbtn
            // 
            this.moverightbtn.TabIndex = 14;
            // 
            // moveupbtn
            // 
            this.moveupbtn.TabIndex = 12;
            // 
            // movedownbtn
            // 
            this.movedownbtn.TabIndex = 15;
            // 
            // alignTextcombo
            // 
            this.alignTextcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignTextcombo.FormattingEnabled = true;
            this.alignTextcombo.Location = new System.Drawing.Point(69, 137);
            this.alignTextcombo.Name = "alignTextcombo";
            this.alignTextcombo.Size = new System.Drawing.Size(111, 21);
            this.alignTextcombo.TabIndex = 6;
            this.alignTextcombo.SelectionChangeCommitted += new System.EventHandler(this.alignTextcombo_SelectedIndexChanged);
            // 
            // aligntxtlbl
            // 
            this.aligntxtlbl.AutoSize = true;
            this.aligntxtlbl.Location = new System.Drawing.Point(3, 140);
            this.aligntxtlbl.Name = "aligntxtlbl";
            this.aligntxtlbl.Size = new System.Drawing.Size(57, 13);
            this.aligntxtlbl.TabIndex = 31;
            this.aligntxtlbl.Text = "Align Text:";
            // 
            // concatmethodcombo
            // 
            this.concatmethodcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.concatmethodcombo.FormattingEnabled = true;
            this.concatmethodcombo.Location = new System.Drawing.Point(92, 164);
            this.concatmethodcombo.Name = "concatmethodcombo";
            this.concatmethodcombo.Size = new System.Drawing.Size(88, 21);
            this.concatmethodcombo.TabIndex = 7;
            this.concatmethodcombo.SelectionChangeCommitted += new System.EventHandler(this.concatmethodcombo_SelectedIndexChanged);
            // 
            // concatmethodlbl
            // 
            this.concatmethodlbl.AutoSize = true;
            this.concatmethodlbl.Location = new System.Drawing.Point(3, 167);
            this.concatmethodlbl.Name = "concatmethodlbl";
            this.concatmethodlbl.Size = new System.Drawing.Size(83, 13);
            this.concatmethodlbl.TabIndex = 41;
            this.concatmethodlbl.Text = "Concat Method:";
            // 
            // linkedtextfieldslbl
            // 
            this.linkedtextfieldslbl.AutoSize = true;
            this.linkedtextfieldslbl.Location = new System.Drawing.Point(3, 194);
            this.linkedtextfieldslbl.Name = "linkedtextfieldslbl";
            this.linkedtextfieldslbl.Size = new System.Drawing.Size(69, 13);
            this.linkedtextfieldslbl.TabIndex = 43;
            this.linkedtextfieldslbl.Text = "Linked fields:";
            // 
            // linkedtextfieldslist
            // 
            this.linkedtextfieldslist.FormattingEnabled = true;
            this.linkedtextfieldslist.HorizontalScrollbar = true;
            this.linkedtextfieldslist.Location = new System.Drawing.Point(69, 193);
            this.linkedtextfieldslist.Name = "linkedtextfieldslist";
            this.linkedtextfieldslist.Size = new System.Drawing.Size(111, 69);
            this.linkedtextfieldslist.TabIndex = 8;
            this.linkedtextfieldslist.SelectedIndexChanged += new System.EventHandler(this.linkedtextfieldslist_SelectedIndexChanged);
            // 
            // fontlbl
            // 
            this.fontlbl.AutoSize = true;
            this.fontlbl.Location = new System.Drawing.Point(3, 221);
            this.fontlbl.Name = "fontlbl";
            this.fontlbl.Size = new System.Drawing.Size(34, 13);
            this.fontlbl.TabIndex = 45;
            this.fontlbl.Text = "Font: ";
            this.fontlbl.Visible = false;
            // 
            // Fontcombo
            // 
            this.Fontcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Fontcombo.FormattingEnabled = true;
            this.Fontcombo.Location = new System.Drawing.Point(69, 218);
            this.Fontcombo.Name = "Fontcombo";
            this.Fontcombo.Size = new System.Drawing.Size(111, 21);
            this.Fontcombo.TabIndex = 10;
            this.Fontcombo.Visible = false;
            this.Fontcombo.SelectionChangeCommitted += new System.EventHandler(this.Fontcombo_SelectedIndexChanged);
            // 
            // formatlbl
            // 
            this.formatlbl.AutoSize = true;
            this.formatlbl.Location = new System.Drawing.Point(3, 248);
            this.formatlbl.Name = "formatlbl";
            this.formatlbl.Size = new System.Drawing.Size(42, 13);
            this.formatlbl.TabIndex = 47;
            this.formatlbl.Text = "Format:";
            this.formatlbl.Visible = false;
            // 
            // formattxt
            // 
            this.formattxt.Location = new System.Drawing.Point(69, 245);
            this.formattxt.Name = "formattxt";
            this.formattxt.Size = new System.Drawing.Size(111, 20);
            this.formattxt.TabIndex = 11;
            this.formattxt.Visible = false;
            this.formattxt.LostFocus += new System.EventHandler(this.formattxt_LostFocus);
            // 
            // backbtn
            // 
            this.backbtn.Image = ((System.Drawing.Image)(resources.GetObject("backbtn.Image")));
            this.backbtn.Location = new System.Drawing.Point(43, 218);
            this.backbtn.Name = "backbtn";
            this.backbtn.Size = new System.Drawing.Size(22, 21);
            this.backbtn.TabIndex = 49;
            this.backbtn.UseVisualStyleBackColor = true;
            this.backbtn.Visible = false;
            this.backbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // ConcatFieldInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backbtn);
            this.Controls.Add(this.formattxt);
            this.Controls.Add(this.formatlbl);
            this.Controls.Add(this.Fontcombo);
            this.Controls.Add(this.fontlbl);
            this.Controls.Add(this.linkedtextfieldslist);
            this.Controls.Add(this.linkedtextfieldslbl);
            this.Controls.Add(this.concatmethodcombo);
            this.Controls.Add(this.concatmethodlbl);
            this.Controls.Add(this.alignTextcombo);
            this.Controls.Add(this.aligntxtlbl);
            this.Name = "ConcatFieldInfoControl";
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
            this.Controls.SetChildIndex(this.movedownbtn, 0);
            this.Controls.SetChildIndex(this.moveupbtn, 0);
            this.Controls.SetChildIndex(this.moverightbtn, 0);
            this.Controls.SetChildIndex(this.moveleftbtn, 0);
            this.Controls.SetChildIndex(this.adjustmentlbl, 0);
            this.Controls.SetChildIndex(this.aligntxtlbl, 0);
            this.Controls.SetChildIndex(this.alignTextcombo, 0);
            this.Controls.SetChildIndex(this.concatmethodlbl, 0);
            this.Controls.SetChildIndex(this.concatmethodcombo, 0);
            this.Controls.SetChildIndex(this.linkedtextfieldslbl, 0);
            this.Controls.SetChildIndex(this.linkedtextfieldslist, 0);
            this.Controls.SetChildIndex(this.fontlbl, 0);
            this.Controls.SetChildIndex(this.Fontcombo, 0);
            this.Controls.SetChildIndex(this.formatlbl, 0);
            this.Controls.SetChildIndex(this.formattxt, 0);
            this.Controls.SetChildIndex(this.backbtn, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox alignTextcombo;
        private System.Windows.Forms.Label aligntxtlbl;
        private System.Windows.Forms.ComboBox concatmethodcombo;
        private System.Windows.Forms.Label concatmethodlbl;
        private System.Windows.Forms.Label linkedtextfieldslbl;
        private System.Windows.Forms.ListBox linkedtextfieldslist;
        private System.Windows.Forms.Label fontlbl;
        private System.Windows.Forms.ComboBox Fontcombo;
        private System.Windows.Forms.Label formatlbl;
        private System.Windows.Forms.TextBox formattxt;
        private System.Windows.Forms.Button backbtn;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
