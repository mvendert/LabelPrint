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
    partial class NewLabelDefForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewLabelDefForm));
            this.idlbl = new System.Windows.Forms.Label();
            this.supportedpapertypeslbl = new System.Windows.Forms.Label();
            this.DPIlbl = new System.Windows.Forms.Label();
            this.usedpapertypelist = new System.Windows.Forms.ListBox();
            this.newBtn = new System.Windows.Forms.Button();
            this.availablepapertypelist = new System.Windows.Forms.ListBox();
            this.availablepapertypeslbl = new System.Windows.Forms.Label();
            this.propertieslbl = new System.Windows.Forms.Label();
            this.propidlbl = new System.Windows.Forms.Label();
            this.propDPIlbl = new System.Windows.Forms.Label();
            this.propsizelbl = new System.Windows.Forms.Label();
            this.propnrhorzlblslbl = new System.Windows.Forms.Label();
            this.propnrvertlblslbl = new System.Windows.Forms.Label();
            this.prophorzinterlblgaplbl = new System.Windows.Forms.Label();
            this.propvertinterlblgaplbl = new System.Windows.Forms.Label();
            this.propid = new System.Windows.Forms.Label();
            this.propDPI = new System.Windows.Forms.Label();
            this.propsize = new System.Windows.Forms.Label();
            this.prophorzlbls = new System.Windows.Forms.Label();
            this.propvertlbls = new System.Windows.Forms.Label();
            this.prophorzinterlblgap = new System.Windows.Forms.Label();
            this.propvertinterlblgap = new System.Windows.Forms.Label();
            this.idtxt = new System.Windows.Forms.TextBox();
            this.DPIXtxt = new System.Windows.Forms.TextBox();
            this.DPIXlbl = new System.Windows.Forms.Label();
            this.DPIYlbl = new System.Windows.Forms.Label();
            this.DPIYtxt = new System.Windows.Forms.TextBox();
            this.unitlbl = new System.Windows.Forms.Label();
            this.unitcombo = new System.Windows.Forms.ComboBox();
            this.addpapertypebtn = new System.Windows.Forms.Button();
            this.removepapertypebtn = new System.Windows.Forms.Button();
            this.Okbtn = new System.Windows.Forms.Button();
            this.defaultcheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // idlbl
            // 
            this.idlbl.AutoSize = true;
            this.idlbl.Location = new System.Drawing.Point(12, 23);
            this.idlbl.Name = "idlbl";
            this.idlbl.Size = new System.Drawing.Size(24, 13);
            this.idlbl.TabIndex = 0;
            this.idlbl.Text = "ID :";
            // 
            // supportedpapertypeslbl
            // 
            this.supportedpapertypeslbl.AutoSize = true;
            this.supportedpapertypeslbl.Location = new System.Drawing.Point(12, 46);
            this.supportedpapertypeslbl.Name = "supportedpapertypeslbl";
            this.supportedpapertypeslbl.Size = new System.Drawing.Size(122, 13);
            this.supportedpapertypeslbl.TabIndex = 1;
            this.supportedpapertypeslbl.Text = "Supported Paper Types:";
            // 
            // DPIlbl
            // 
            this.DPIlbl.AutoSize = true;
            this.DPIlbl.Location = new System.Drawing.Point(13, 204);
            this.DPIlbl.Name = "DPIlbl";
            this.DPIlbl.Size = new System.Drawing.Size(31, 13);
            this.DPIlbl.TabIndex = 2;
            this.DPIlbl.Text = "DPI: ";
            // 
            // usedpapertypelist
            // 
            this.usedpapertypelist.FormattingEnabled = true;
            this.usedpapertypelist.Location = new System.Drawing.Point(16, 62);
            this.usedpapertypelist.Name = "usedpapertypelist";
            this.usedpapertypelist.Size = new System.Drawing.Size(127, 82);
            this.usedpapertypelist.TabIndex = 1;
            this.usedpapertypelist.SelectedIndexChanged += new System.EventHandler(this.usedpapertypelist_SelectedIndexChanged);
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(15, 173);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(75, 23);
            this.newBtn.TabIndex = 3;
            this.newBtn.Text = "New ";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // availablepapertypelist
            // 
            this.availablepapertypelist.FormattingEnabled = true;
            this.availablepapertypelist.Location = new System.Drawing.Point(179, 62);
            this.availablepapertypelist.Name = "availablepapertypelist";
            this.availablepapertypelist.Size = new System.Drawing.Size(120, 212);
            this.availablepapertypelist.TabIndex = 7;
            this.availablepapertypelist.SelectedIndexChanged += new System.EventHandler(this.ShowPaperTypeProperties);
            // 
            // availablepapertypeslbl
            // 
            this.availablepapertypeslbl.AutoSize = true;
            this.availablepapertypeslbl.Location = new System.Drawing.Point(176, 46);
            this.availablepapertypeslbl.Name = "availablepapertypeslbl";
            this.availablepapertypeslbl.Size = new System.Drawing.Size(116, 13);
            this.availablepapertypeslbl.TabIndex = 6;
            this.availablepapertypeslbl.Text = "Available Paper Types:";
            // 
            // propertieslbl
            // 
            this.propertieslbl.AutoSize = true;
            this.propertieslbl.Location = new System.Drawing.Point(305, 46);
            this.propertieslbl.Name = "propertieslbl";
            this.propertieslbl.Size = new System.Drawing.Size(57, 13);
            this.propertieslbl.TabIndex = 7;
            this.propertieslbl.Text = "Properties:";
            // 
            // propidlbl
            // 
            this.propidlbl.AutoSize = true;
            this.propidlbl.Location = new System.Drawing.Point(305, 62);
            this.propidlbl.Name = "propidlbl";
            this.propidlbl.Size = new System.Drawing.Size(24, 13);
            this.propidlbl.TabIndex = 8;
            this.propidlbl.Text = "ID: ";
            // 
            // propDPIlbl
            // 
            this.propDPIlbl.AutoSize = true;
            this.propDPIlbl.Location = new System.Drawing.Point(305, 84);
            this.propDPIlbl.Name = "propDPIlbl";
            this.propDPIlbl.Size = new System.Drawing.Size(28, 13);
            this.propDPIlbl.TabIndex = 9;
            this.propDPIlbl.Text = "DPI:";
            // 
            // propsizelbl
            // 
            this.propsizelbl.AutoSize = true;
            this.propsizelbl.Location = new System.Drawing.Point(305, 108);
            this.propsizelbl.Name = "propsizelbl";
            this.propsizelbl.Size = new System.Drawing.Size(30, 13);
            this.propsizelbl.TabIndex = 10;
            this.propsizelbl.Text = "Size:";
            // 
            // propnrhorzlblslbl
            // 
            this.propnrhorzlblslbl.AutoSize = true;
            this.propnrhorzlblslbl.Location = new System.Drawing.Point(305, 131);
            this.propnrhorzlblslbl.Name = "propnrhorzlblslbl";
            this.propnrhorzlblslbl.Size = new System.Drawing.Size(101, 13);
            this.propnrhorzlblslbl.TabIndex = 11;
            this.propnrhorzlblslbl.Text = "# Horizontal Labels:";
            // 
            // propnrvertlblslbl
            // 
            this.propnrvertlblslbl.AutoSize = true;
            this.propnrvertlblslbl.Location = new System.Drawing.Point(305, 155);
            this.propnrvertlblslbl.Name = "propnrvertlblslbl";
            this.propnrvertlblslbl.Size = new System.Drawing.Size(89, 13);
            this.propnrvertlblslbl.TabIndex = 12;
            this.propnrvertlblslbl.Text = "# Vertical Labels:";
            // 
            // prophorzinterlblgaplbl
            // 
            this.prophorzinterlblgaplbl.AutoSize = true;
            this.prophorzinterlblgaplbl.Location = new System.Drawing.Point(305, 178);
            this.prophorzinterlblgaplbl.Name = "prophorzinterlblgaplbl";
            this.prophorzinterlblgaplbl.Size = new System.Drawing.Size(126, 13);
            this.prophorzinterlblgaplbl.TabIndex = 15;
            this.prophorzinterlblgaplbl.Text = "Horizontal Interlabel Gap:";
            // 
            // propvertinterlblgaplbl
            // 
            this.propvertinterlblgaplbl.AutoSize = true;
            this.propvertinterlblgaplbl.Location = new System.Drawing.Point(305, 204);
            this.propvertinterlblgaplbl.Name = "propvertinterlblgaplbl";
            this.propvertinterlblgaplbl.Size = new System.Drawing.Size(114, 13);
            this.propvertinterlblgaplbl.TabIndex = 16;
            this.propvertinterlblgaplbl.Text = "Vertical Interlabel Gap:";
            // 
            // propid
            // 
            this.propid.AutoSize = true;
            this.propid.Location = new System.Drawing.Point(335, 62);
            this.propid.Name = "propid";
            this.propid.Size = new System.Drawing.Size(0, 13);
            this.propid.TabIndex = 17;
            // 
            // propDPI
            // 
            this.propDPI.AutoSize = true;
            this.propDPI.Location = new System.Drawing.Point(341, 84);
            this.propDPI.Name = "propDPI";
            this.propDPI.Size = new System.Drawing.Size(0, 13);
            this.propDPI.TabIndex = 18;
            // 
            // propsize
            // 
            this.propsize.AutoSize = true;
            this.propsize.Location = new System.Drawing.Point(341, 108);
            this.propsize.Name = "propsize";
            this.propsize.Size = new System.Drawing.Size(0, 13);
            this.propsize.TabIndex = 19;
            // 
            // prophorzlbls
            // 
            this.prophorzlbls.AutoSize = true;
            this.prophorzlbls.Location = new System.Drawing.Point(412, 131);
            this.prophorzlbls.Name = "prophorzlbls";
            this.prophorzlbls.Size = new System.Drawing.Size(0, 13);
            this.prophorzlbls.TabIndex = 20;
            // 
            // propvertlbls
            // 
            this.propvertlbls.AutoSize = true;
            this.propvertlbls.Location = new System.Drawing.Point(400, 155);
            this.propvertlbls.Name = "propvertlbls";
            this.propvertlbls.Size = new System.Drawing.Size(0, 13);
            this.propvertlbls.TabIndex = 21;
            // 
            // prophorzinterlblgap
            // 
            this.prophorzinterlblgap.AutoSize = true;
            this.prophorzinterlblgap.Location = new System.Drawing.Point(437, 178);
            this.prophorzinterlblgap.Name = "prophorzinterlblgap";
            this.prophorzinterlblgap.Size = new System.Drawing.Size(0, 13);
            this.prophorzinterlblgap.TabIndex = 24;
            // 
            // propvertinterlblgap
            // 
            this.propvertinterlblgap.AutoSize = true;
            this.propvertinterlblgap.Location = new System.Drawing.Point(425, 204);
            this.propvertinterlblgap.Name = "propvertinterlblgap";
            this.propvertinterlblgap.Size = new System.Drawing.Size(0, 13);
            this.propvertinterlblgap.TabIndex = 25;
            // 
            // idtxt
            // 
            this.idtxt.Location = new System.Drawing.Point(43, 20);
            this.idtxt.Name = "idtxt";
            this.idtxt.Size = new System.Drawing.Size(100, 20);
            this.idtxt.TabIndex = 0;
            // 
            // DPIXtxt
            // 
            this.DPIXtxt.Location = new System.Drawing.Point(48, 220);
            this.DPIXtxt.Name = "DPIXtxt";
            this.DPIXtxt.Size = new System.Drawing.Size(32, 20);
            this.DPIXtxt.TabIndex = 4;
            this.DPIXtxt.Text = "200";
            // 
            // DPIXlbl
            // 
            this.DPIXlbl.AutoSize = true;
            this.DPIXlbl.Location = new System.Drawing.Point(22, 223);
            this.DPIXlbl.Name = "DPIXlbl";
            this.DPIXlbl.Size = new System.Drawing.Size(20, 13);
            this.DPIXlbl.TabIndex = 28;
            this.DPIXlbl.Text = "X: ";
            // 
            // DPIYlbl
            // 
            this.DPIYlbl.AutoSize = true;
            this.DPIYlbl.Location = new System.Drawing.Point(84, 223);
            this.DPIYlbl.Name = "DPIYlbl";
            this.DPIYlbl.Size = new System.Drawing.Size(20, 13);
            this.DPIYlbl.TabIndex = 30;
            this.DPIYlbl.Text = "Y: ";
            // 
            // DPIYtxt
            // 
            this.DPIYtxt.Location = new System.Drawing.Point(110, 220);
            this.DPIYtxt.Name = "DPIYtxt";
            this.DPIYtxt.Size = new System.Drawing.Size(32, 20);
            this.DPIYtxt.TabIndex = 5;
            this.DPIYtxt.Text = "200";
            // 
            // unitlbl
            // 
            this.unitlbl.AutoSize = true;
            this.unitlbl.Location = new System.Drawing.Point(22, 250);
            this.unitlbl.Name = "unitlbl";
            this.unitlbl.Size = new System.Drawing.Size(32, 13);
            this.unitlbl.TabIndex = 31;
            this.unitlbl.Text = "Unit: ";
            // 
            // unitcombo
            // 
            this.unitcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitcombo.FormattingEnabled = true;
            this.unitcombo.Location = new System.Drawing.Point(60, 246);
            this.unitcombo.Name = "unitcombo";
            this.unitcombo.Size = new System.Drawing.Size(82, 21);
            this.unitcombo.TabIndex = 6;
            // 
            // addpapertypebtn
            // 
            this.addpapertypebtn.Image = ((System.Drawing.Image)(resources.GetObject("addpapertypebtn.Image")));
            this.addpapertypebtn.Location = new System.Drawing.Point(149, 74);
            this.addpapertypebtn.Name = "addpapertypebtn";
            this.addpapertypebtn.Size = new System.Drawing.Size(24, 23);
            this.addpapertypebtn.TabIndex = 33;
            this.addpapertypebtn.UseVisualStyleBackColor = true;
            this.addpapertypebtn.Click += new System.EventHandler(this.addpapertypebtn_Click);
            // 
            // removepapertypebtn
            // 
            this.removepapertypebtn.Enabled = false;
            this.removepapertypebtn.Image = ((System.Drawing.Image)(resources.GetObject("removepapertypebtn.Image")));
            this.removepapertypebtn.Location = new System.Drawing.Point(149, 108);
            this.removepapertypebtn.Name = "removepapertypebtn";
            this.removepapertypebtn.Size = new System.Drawing.Size(24, 23);
            this.removepapertypebtn.TabIndex = 34;
            this.removepapertypebtn.UseVisualStyleBackColor = true;
            this.removepapertypebtn.Click += new System.EventHandler(this.removepapertypebtn_Click);
            // 
            // Okbtn
            // 
            this.Okbtn.Enabled = false;
            this.Okbtn.Location = new System.Drawing.Point(307, 251);
            this.Okbtn.Name = "Okbtn";
            this.Okbtn.Size = new System.Drawing.Size(75, 23);
            this.Okbtn.TabIndex = 8;
            this.Okbtn.Text = "Ok";
            this.Okbtn.UseVisualStyleBackColor = true;
            this.Okbtn.Click += new System.EventHandler(this.Okbtn_Click);
            // 
            // defaultcheck
            // 
            this.defaultcheck.AutoSize = true;
            this.defaultcheck.Enabled = false;
            this.defaultcheck.Location = new System.Drawing.Point(15, 150);
            this.defaultcheck.Name = "defaultcheck";
            this.defaultcheck.Size = new System.Drawing.Size(144, 17);
            this.defaultcheck.TabIndex = 2;
            this.defaultcheck.Text = "Set paper type as default";
            this.defaultcheck.UseVisualStyleBackColor = true;
            this.defaultcheck.CheckedChanged += new System.EventHandler(this.defaultcheck_CheckedChanged);
            // 
            // NewLabelDefForm
            // 
            this.AcceptButton = this.Okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 292);
            this.Controls.Add(this.defaultcheck);
            this.Controls.Add(this.Okbtn);
            this.Controls.Add(this.removepapertypebtn);
            this.Controls.Add(this.addpapertypebtn);
            this.Controls.Add(this.unitcombo);
            this.Controls.Add(this.unitlbl);
            this.Controls.Add(this.DPIYlbl);
            this.Controls.Add(this.DPIYtxt);
            this.Controls.Add(this.DPIXlbl);
            this.Controls.Add(this.DPIXtxt);
            this.Controls.Add(this.idtxt);
            this.Controls.Add(this.propvertinterlblgap);
            this.Controls.Add(this.prophorzinterlblgap);
            this.Controls.Add(this.propvertlbls);
            this.Controls.Add(this.prophorzlbls);
            this.Controls.Add(this.propsize);
            this.Controls.Add(this.propDPI);
            this.Controls.Add(this.propid);
            this.Controls.Add(this.propvertinterlblgaplbl);
            this.Controls.Add(this.prophorzinterlblgaplbl);
            this.Controls.Add(this.propnrvertlblslbl);
            this.Controls.Add(this.propnrhorzlblslbl);
            this.Controls.Add(this.propsizelbl);
            this.Controls.Add(this.propDPIlbl);
            this.Controls.Add(this.propidlbl);
            this.Controls.Add(this.propertieslbl);
            this.Controls.Add(this.availablepapertypeslbl);
            this.Controls.Add(this.availablepapertypelist);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.usedpapertypelist);
            this.Controls.Add(this.DPIlbl);
            this.Controls.Add(this.supportedpapertypeslbl);
            this.Controls.Add(this.idlbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLabelDefForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Label definition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idlbl;
        private System.Windows.Forms.Label supportedpapertypeslbl;
        private System.Windows.Forms.Label DPIlbl;
        private System.Windows.Forms.ListBox usedpapertypelist;
        private System.Windows.Forms.Button newBtn;
        private System.Windows.Forms.ListBox availablepapertypelist;
        private System.Windows.Forms.Label availablepapertypeslbl;
        private System.Windows.Forms.Label propertieslbl;
        private System.Windows.Forms.Label propidlbl;
        private System.Windows.Forms.Label propDPIlbl;
        private System.Windows.Forms.Label propsizelbl;
        private System.Windows.Forms.Label propnrhorzlblslbl;
        private System.Windows.Forms.Label propnrvertlblslbl;
        private System.Windows.Forms.Label prophorzinterlblgaplbl;
        private System.Windows.Forms.Label propvertinterlblgaplbl;
        private System.Windows.Forms.Label propid;
        private System.Windows.Forms.Label propDPI;
        private System.Windows.Forms.Label propsize;
        private System.Windows.Forms.Label prophorzlbls;
        private System.Windows.Forms.Label propvertlbls;
        private System.Windows.Forms.Label prophorzinterlblgap;
        private System.Windows.Forms.Label propvertinterlblgap;
        private System.Windows.Forms.TextBox idtxt;
        private System.Windows.Forms.TextBox DPIXtxt;
        private System.Windows.Forms.Label DPIXlbl;
        private System.Windows.Forms.Label DPIYlbl;
        private System.Windows.Forms.TextBox DPIYtxt;
        private System.Windows.Forms.Label unitlbl;
        private System.Windows.Forms.ComboBox unitcombo;
        private System.Windows.Forms.Button addpapertypebtn;
        private System.Windows.Forms.Button removepapertypebtn;
        private System.Windows.Forms.Button Okbtn;
        private System.Windows.Forms.CheckBox defaultcheck;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
