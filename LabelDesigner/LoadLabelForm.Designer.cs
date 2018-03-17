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
    partial class LoadLabelForm
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
            this.defaultlblbtn = new System.Windows.Forms.Button();
            this.newlblbtn = new System.Windows.Forms.Button();
            this.existinglblbtn = new System.Windows.Forms.Button();
            this.mainlbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // defaultlblbtn
            // 
            this.defaultlblbtn.Location = new System.Drawing.Point(31, 54);
            this.defaultlblbtn.Name = "defaultlblbtn";
            this.defaultlblbtn.Size = new System.Drawing.Size(102, 23);
            this.defaultlblbtn.TabIndex = 0;
            this.defaultlblbtn.Text = "Load default label";
            this.defaultlblbtn.UseVisualStyleBackColor = true;
            this.defaultlblbtn.Click += new System.EventHandler(this.defaultlblbtn_Click);
            // 
            // newlblbtn
            // 
            this.newlblbtn.Location = new System.Drawing.Point(273, 54);
            this.newlblbtn.Name = "newlblbtn";
            this.newlblbtn.Size = new System.Drawing.Size(102, 23);
            this.newlblbtn.TabIndex = 2;
            this.newlblbtn.Text = "Create new label";
            this.newlblbtn.UseVisualStyleBackColor = true;
            this.newlblbtn.Click += new System.EventHandler(this.newlblbtn_Click);
            // 
            // existinglblbtn
            // 
            this.existinglblbtn.Location = new System.Drawing.Point(153, 54);
            this.existinglblbtn.Name = "existinglblbtn";
            this.existinglblbtn.Size = new System.Drawing.Size(102, 23);
            this.existinglblbtn.TabIndex = 1;
            this.existinglblbtn.Text = "Load existing label";
            this.existinglblbtn.UseVisualStyleBackColor = true;
            this.existinglblbtn.Click += new System.EventHandler(this.existinglblbtn_Click);
            // 
            // mainlbl
            // 
            this.mainlbl.AutoSize = true;
            this.mainlbl.Location = new System.Drawing.Point(52, 23);
            this.mainlbl.Name = "mainlbl";
            this.mainlbl.Size = new System.Drawing.Size(278, 13);
            this.mainlbl.TabIndex = 8;
            this.mainlbl.Text = "Please select which label you want to use for this printjob.";
            // 
            // LoadLabelForm
            // 
            this.AcceptButton = this.defaultlblbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 94);
            this.Controls.Add(this.mainlbl);
            this.Controls.Add(this.existinglblbtn);
            this.Controls.Add(this.newlblbtn);
            this.Controls.Add(this.defaultlblbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadLabelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoadLabelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button defaultlblbtn;
        private System.Windows.Forms.Button newlblbtn;
        private System.Windows.Forms.Button existinglblbtn;
        private System.Windows.Forms.Label mainlbl;

    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
