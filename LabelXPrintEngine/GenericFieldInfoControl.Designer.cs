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
    partial class GenericFieldInfoControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenericFieldInfoControl));
            this.xposlbl = new System.Windows.Forms.Label();
            this.xpostxt = new System.Windows.Forms.TextBox();
            this.ypostxt = new System.Windows.Forms.TextBox();
            this.yposlbl = new System.Windows.Forms.Label();
            this.rotationlbl = new System.Windows.Forms.Label();
            this.rotationcombo = new System.Windows.Forms.ComboBox();
            this.manualwhcheck = new System.Windows.Forms.CheckBox();
            this.heighttxt = new System.Windows.Forms.TextBox();
            this.widthtxt = new System.Windows.Forms.TextBox();
            this.heightlbl = new System.Windows.Forms.Label();
            this.widthlbl = new System.Windows.Forms.Label();
            this.referencecombo = new System.Windows.Forms.ComboBox();
            this.referencelbl = new System.Windows.Forms.Label();
            this.adjustmentlbl = new System.Windows.Forms.Label();
            this.moveleftbtn = new System.Windows.Forms.Button();
            this.moverightbtn = new System.Windows.Forms.Button();
            this.moveupbtn = new System.Windows.Forms.Button();
            this.movedownbtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.statusPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // xposlbl
            // 
            this.xposlbl.AutoSize = true;
            this.xposlbl.Enabled = false;
            this.xposlbl.Location = new System.Drawing.Point(3, 11);
            this.xposlbl.Name = "xposlbl";
            this.xposlbl.Size = new System.Drawing.Size(60, 13);
            this.xposlbl.TabIndex = 2;
            this.xposlbl.Text = "X Position: ";
            // 
            // xpostxt
            // 
            this.xpostxt.Enabled = false;
            this.xpostxt.Location = new System.Drawing.Point(69, 8);
            this.xpostxt.Name = "xpostxt";
            this.xpostxt.Size = new System.Drawing.Size(46, 20);
            this.xpostxt.TabIndex = 0;
            this.xpostxt.LostFocus += new System.EventHandler(this.xpostxt_LostFocus);
            // 
            // ypostxt
            // 
            this.ypostxt.Enabled = false;
            this.ypostxt.Location = new System.Drawing.Point(69, 34);
            this.ypostxt.Name = "ypostxt";
            this.ypostxt.Size = new System.Drawing.Size(46, 20);
            this.ypostxt.TabIndex = 1;
            this.ypostxt.LostFocus += new System.EventHandler(this.ypostxt_LostFocus);
            // 
            // yposlbl
            // 
            this.yposlbl.AutoSize = true;
            this.yposlbl.Enabled = false;
            this.yposlbl.Location = new System.Drawing.Point(3, 37);
            this.yposlbl.Name = "yposlbl";
            this.yposlbl.Size = new System.Drawing.Size(60, 13);
            this.yposlbl.TabIndex = 4;
            this.yposlbl.Text = "Y Position: ";
            // 
            // rotationlbl
            // 
            this.rotationlbl.AutoSize = true;
            this.rotationlbl.Enabled = false;
            this.rotationlbl.Location = new System.Drawing.Point(3, 64);
            this.rotationlbl.Name = "rotationlbl";
            this.rotationlbl.Size = new System.Drawing.Size(50, 13);
            this.rotationlbl.TabIndex = 6;
            this.rotationlbl.Text = "Rotation:";
            // 
            // rotationcombo
            // 
            this.rotationcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rotationcombo.Enabled = false;
            this.rotationcombo.FormattingEnabled = true;
            this.rotationcombo.Location = new System.Drawing.Point(69, 61);
            this.rotationcombo.Name = "rotationcombo";
            this.rotationcombo.Size = new System.Drawing.Size(46, 21);
            this.rotationcombo.TabIndex = 2;
            this.rotationcombo.SelectionChangeCommitted += new System.EventHandler(this.rotationcombo_SelectedIndexChanged);
            // 
            // manualwhcheck
            // 
            this.manualwhcheck.AutoSize = true;
            this.manualwhcheck.Enabled = false;
            this.manualwhcheck.Location = new System.Drawing.Point(6, 115);
            this.manualwhcheck.Name = "manualwhcheck";
            this.manualwhcheck.Size = new System.Drawing.Size(163, 17);
            this.manualwhcheck.TabIndex = 4;
            this.manualwhcheck.Text = "Use manual width and height";
            this.manualwhcheck.UseVisualStyleBackColor = true;
            this.manualwhcheck.CheckedChanged += new System.EventHandler(this.manualwhcheck_CheckedChanged);
            // 
            // heighttxt
            // 
            this.heighttxt.Enabled = false;
            this.heighttxt.Location = new System.Drawing.Point(140, 138);
            this.heighttxt.Name = "heighttxt";
            this.heighttxt.Size = new System.Drawing.Size(40, 20);
            this.heighttxt.TabIndex = 6;
            this.heighttxt.LostFocus += new System.EventHandler(this.heighttxt_LostFocus);
            // 
            // widthtxt
            // 
            this.widthtxt.Enabled = false;
            this.widthtxt.Location = new System.Drawing.Point(47, 138);
            this.widthtxt.Name = "widthtxt";
            this.widthtxt.Size = new System.Drawing.Size(40, 20);
            this.widthtxt.TabIndex = 5;
            this.widthtxt.LostFocus += new System.EventHandler(this.widthtxt_LostFocus);
            // 
            // heightlbl
            // 
            this.heightlbl.AutoSize = true;
            this.heightlbl.Enabled = false;
            this.heightlbl.Location = new System.Drawing.Point(93, 141);
            this.heightlbl.Name = "heightlbl";
            this.heightlbl.Size = new System.Drawing.Size(41, 13);
            this.heightlbl.TabIndex = 22;
            this.heightlbl.Text = "Height:";
            // 
            // widthlbl
            // 
            this.widthlbl.AutoSize = true;
            this.widthlbl.Enabled = false;
            this.widthlbl.Location = new System.Drawing.Point(3, 141);
            this.widthlbl.Name = "widthlbl";
            this.widthlbl.Size = new System.Drawing.Size(38, 13);
            this.widthlbl.TabIndex = 21;
            this.widthlbl.Text = "Width:";
            // 
            // referencecombo
            // 
            this.referencecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.referencecombo.Enabled = false;
            this.referencecombo.FormattingEnabled = true;
            this.referencecombo.Location = new System.Drawing.Point(69, 88);
            this.referencecombo.Name = "referencecombo";
            this.referencecombo.Size = new System.Drawing.Size(111, 21);
            this.referencecombo.TabIndex = 3;
            this.referencecombo.SelectionChangeCommitted += new System.EventHandler(this.referencecombo_SelectedIndexChanged);
            // 
            // referencelbl
            // 
            this.referencelbl.AutoSize = true;
            this.referencelbl.Enabled = false;
            this.referencelbl.Location = new System.Drawing.Point(3, 91);
            this.referencelbl.Name = "referencelbl";
            this.referencelbl.Size = new System.Drawing.Size(60, 13);
            this.referencelbl.TabIndex = 20;
            this.referencelbl.Text = "Reference:";
            // 
            // adjustmentlbl
            // 
            this.adjustmentlbl.AutoSize = true;
            this.adjustmentlbl.Enabled = false;
            this.adjustmentlbl.Location = new System.Drawing.Point(3, 274);
            this.adjustmentlbl.Name = "adjustmentlbl";
            this.adjustmentlbl.Size = new System.Drawing.Size(62, 13);
            this.adjustmentlbl.TabIndex = 27;
            this.adjustmentlbl.Text = "Adjustment:";
            // 
            // moveleftbtn
            // 
            this.moveleftbtn.Enabled = false;
            this.moveleftbtn.Image = ((System.Drawing.Image)(resources.GetObject("moveleftbtn.Image")));
            this.moveleftbtn.Location = new System.Drawing.Point(107, 290);
            this.moveleftbtn.Name = "moveleftbtn";
            this.moveleftbtn.Size = new System.Drawing.Size(24, 22);
            this.moveleftbtn.TabIndex = 8;
            this.moveleftbtn.UseVisualStyleBackColor = true;
            this.moveleftbtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveleftbtn_MouseDown);
            this.moveleftbtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveleftbtn_MouseUp);
            // 
            // moverightbtn
            // 
            this.moverightbtn.Enabled = false;
            this.moverightbtn.Image = ((System.Drawing.Image)(resources.GetObject("moverightbtn.Image")));
            this.moverightbtn.Location = new System.Drawing.Point(154, 290);
            this.moverightbtn.Name = "moverightbtn";
            this.moverightbtn.Size = new System.Drawing.Size(24, 22);
            this.moverightbtn.TabIndex = 9;
            this.moverightbtn.UseVisualStyleBackColor = true;
            this.moverightbtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moverightbtn_MouseDown);
            this.moverightbtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moverightbtn_MouseUp);
            // 
            // moveupbtn
            // 
            this.moveupbtn.Enabled = false;
            this.moveupbtn.Image = ((System.Drawing.Image)(resources.GetObject("moveupbtn.Image")));
            this.moveupbtn.Location = new System.Drawing.Point(130, 269);
            this.moveupbtn.Name = "moveupbtn";
            this.moveupbtn.Size = new System.Drawing.Size(24, 22);
            this.moveupbtn.TabIndex = 7;
            this.moveupbtn.UseVisualStyleBackColor = true;
            this.moveupbtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveupbtn_MouseDown);
            this.moveupbtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveupbtn_MouseUp);
            // 
            // movedownbtn
            // 
            this.movedownbtn.Enabled = false;
            this.movedownbtn.Image = ((System.Drawing.Image)(resources.GetObject("movedownbtn.Image")));
            this.movedownbtn.Location = new System.Drawing.Point(130, 309);
            this.movedownbtn.Name = "movedownbtn";
            this.movedownbtn.Size = new System.Drawing.Size(24, 22);
            this.movedownbtn.TabIndex = 10;
            this.movedownbtn.UseVisualStyleBackColor = true;
            this.movedownbtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.movedownbtn_MouseDown);
            this.movedownbtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.movedownbtn_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusPictureBox
            // 
            this.statusPictureBox.Location = new System.Drawing.Point(132, 290);
            this.statusPictureBox.Name = "statusPictureBox";
            this.statusPictureBox.Size = new System.Drawing.Size(22, 22);
            this.statusPictureBox.TabIndex = 28;
            this.statusPictureBox.TabStop = false;
            this.statusPictureBox.Visible = false;
            // 
            // GenericFieldInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusPictureBox);
            this.Controls.Add(this.adjustmentlbl);
            this.Controls.Add(this.moveleftbtn);
            this.Controls.Add(this.moverightbtn);
            this.Controls.Add(this.moveupbtn);
            this.Controls.Add(this.movedownbtn);
            this.Controls.Add(this.manualwhcheck);
            this.Controls.Add(this.heighttxt);
            this.Controls.Add(this.widthtxt);
            this.Controls.Add(this.heightlbl);
            this.Controls.Add(this.widthlbl);
            this.Controls.Add(this.referencecombo);
            this.Controls.Add(this.referencelbl);
            this.Controls.Add(this.rotationcombo);
            this.Controls.Add(this.rotationlbl);
            this.Controls.Add(this.ypostxt);
            this.Controls.Add(this.yposlbl);
            this.Controls.Add(this.xpostxt);
            this.Controls.Add(this.xposlbl);
            this.Name = "GenericFieldInfoControl";
            this.Size = new System.Drawing.Size(192, 340);
            ((System.ComponentModel.ISupportInitialize)(this.statusPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label xposlbl;
        protected System.Windows.Forms.TextBox xpostxt;
        protected System.Windows.Forms.TextBox ypostxt;
        protected System.Windows.Forms.Label yposlbl;
        protected System.Windows.Forms.Label rotationlbl;
        protected System.Windows.Forms.ComboBox rotationcombo;
        protected System.Windows.Forms.CheckBox manualwhcheck;
        protected System.Windows.Forms.TextBox heighttxt;
        protected System.Windows.Forms.TextBox widthtxt;
        protected System.Windows.Forms.Label heightlbl;
        protected System.Windows.Forms.Label widthlbl;
        protected System.Windows.Forms.ComboBox referencecombo;
        protected System.Windows.Forms.Label referencelbl;
        protected System.Windows.Forms.Label adjustmentlbl;
        protected System.Windows.Forms.Button moveleftbtn;
        protected System.Windows.Forms.Button moverightbtn;
        protected System.Windows.Forms.Button moveupbtn;
        protected System.Windows.Forms.Button movedownbtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox statusPictureBox;

    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
