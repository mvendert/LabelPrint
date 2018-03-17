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
    partial class TextFieldInfoControl
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
            this.alignTextcombo = new System.Windows.Forms.ComboBox();
            this.alignlbl = new System.Windows.Forms.Label();
            this.Fontcombo = new System.Windows.Forms.ComboBox();
            this.Fontlbl = new System.Windows.Forms.Label();
            this.formatlbl = new System.Windows.Forms.Label();
            this.formattxt = new System.Windows.Forms.TextBox();
            this.typelbl = new System.Windows.Forms.Label();
            this.typecombo = new System.Windows.Forms.ComboBox();
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
            this.moveleftbtn.TabIndex = 12;
            // 
            // moverightbtn
            // 
            this.moverightbtn.TabIndex = 13;
            // 
            // moveupbtn
            // 
            this.moveupbtn.TabIndex = 11;
            // 
            // movedownbtn
            // 
            this.movedownbtn.TabIndex = 14;
            // 
            // alignTextcombo
            // 
            this.alignTextcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignTextcombo.FormattingEnabled = true;
            this.alignTextcombo.Location = new System.Drawing.Point(69, 164);
            this.alignTextcombo.Name = "alignTextcombo";
            this.alignTextcombo.Size = new System.Drawing.Size(111, 21);
            this.alignTextcombo.TabIndex = 7;
            this.alignTextcombo.SelectionChangeCommitted += new System.EventHandler(this.alignTextcombo_SelectedIndexChanged);
            // 
            // alignlbl
            // 
            this.alignlbl.AutoSize = true;
            this.alignlbl.Location = new System.Drawing.Point(3, 167);
            this.alignlbl.Name = "alignlbl";
            this.alignlbl.Size = new System.Drawing.Size(57, 13);
            this.alignlbl.TabIndex = 27;
            this.alignlbl.Text = "Align Text:";
            // 
            // Fontcombo
            // 
            this.Fontcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Fontcombo.FormattingEnabled = true;
            this.Fontcombo.Location = new System.Drawing.Point(69, 191);
            this.Fontcombo.Name = "Fontcombo";
            this.Fontcombo.Size = new System.Drawing.Size(111, 21);
            this.Fontcombo.TabIndex = 8;
            this.Fontcombo.SelectionChangeCommitted += new System.EventHandler(this.Fontcombo_SelectedIndexChanged);
            // 
            // Fontlbl
            // 
            this.Fontlbl.AutoSize = true;
            this.Fontlbl.Location = new System.Drawing.Point(3, 194);
            this.Fontlbl.Name = "Fontlbl";
            this.Fontlbl.Size = new System.Drawing.Size(31, 13);
            this.Fontlbl.TabIndex = 23;
            this.Fontlbl.Text = "Font:";
            // 
            // formatlbl
            // 
            this.formatlbl.AutoSize = true;
            this.formatlbl.Location = new System.Drawing.Point(3, 248);
            this.formatlbl.Name = "formatlbl";
            this.formatlbl.Size = new System.Drawing.Size(45, 13);
            this.formatlbl.TabIndex = 28;
            this.formatlbl.Text = "Format: ";
            // 
            // formattxt
            // 
            this.formattxt.Location = new System.Drawing.Point(69, 245);
            this.formattxt.Name = "formattxt";
            this.formattxt.Size = new System.Drawing.Size(111, 20);
            this.formattxt.TabIndex = 10;
            this.formattxt.LostFocus += new System.EventHandler(this.formattxt_LostFocus);
            // 
            // typelbl
            // 
            this.typelbl.AutoSize = true;
            this.typelbl.Location = new System.Drawing.Point(3, 221);
            this.typelbl.Name = "typelbl";
            this.typelbl.Size = new System.Drawing.Size(34, 13);
            this.typelbl.TabIndex = 30;
            this.typelbl.Text = "Type:";
            // 
            // typecombo
            // 
            this.typecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typecombo.FormattingEnabled = true;
            this.typecombo.Location = new System.Drawing.Point(69, 218);
            this.typecombo.Name = "typecombo";
            this.typecombo.Size = new System.Drawing.Size(111, 21);
            this.typecombo.TabIndex = 9;
            this.typecombo.SelectionChangeCommitted += new System.EventHandler(this.typecombo_SelectionChangeCommitted);
            // 
            // TextFieldInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.typecombo);
            this.Controls.Add(this.typelbl);
            this.Controls.Add(this.formattxt);
            this.Controls.Add(this.formatlbl);
            this.Controls.Add(this.alignTextcombo);
            this.Controls.Add(this.alignlbl);
            this.Controls.Add(this.Fontcombo);
            this.Controls.Add(this.Fontlbl);
            this.Name = "TextFieldInfoControl";
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
            this.Controls.SetChildIndex(this.Fontlbl, 0);
            this.Controls.SetChildIndex(this.Fontcombo, 0);
            this.Controls.SetChildIndex(this.alignlbl, 0);
            this.Controls.SetChildIndex(this.alignTextcombo, 0);
            this.Controls.SetChildIndex(this.formatlbl, 0);
            this.Controls.SetChildIndex(this.formattxt, 0);
            this.Controls.SetChildIndex(this.typelbl, 0);
            this.Controls.SetChildIndex(this.typecombo, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox alignTextcombo;
        private System.Windows.Forms.Label alignlbl;
        private System.Windows.Forms.ComboBox Fontcombo;
        private System.Windows.Forms.Label Fontlbl;
        private System.Windows.Forms.Label formatlbl;
        private System.Windows.Forms.TextBox formattxt;
        private System.Windows.Forms.Label typelbl;
        private System.Windows.Forms.ComboBox typecombo;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
