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
    partial class AddItemForm
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
            this.idlbl = new System.Windows.Forms.Label();
            this.valuelbl = new System.Windows.Forms.Label();
            this.languagelbl = new System.Windows.Forms.Label();
            this.idtxt = new System.Windows.Forms.TextBox();
            this.valuetxt = new System.Windows.Forms.TextBox();
            this.languagecombo = new System.Windows.Forms.ComboBox();
            this.Okbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // idlbl
            // 
            this.idlbl.AutoSize = true;
            this.idlbl.Location = new System.Drawing.Point(12, 9);
            this.idlbl.Name = "idlbl";
            this.idlbl.Size = new System.Drawing.Size(21, 13);
            this.idlbl.TabIndex = 0;
            this.idlbl.Text = "ID:";
            // 
            // valuelbl
            // 
            this.valuelbl.AutoSize = true;
            this.valuelbl.Location = new System.Drawing.Point(12, 35);
            this.valuelbl.Name = "valuelbl";
            this.valuelbl.Size = new System.Drawing.Size(40, 13);
            this.valuelbl.TabIndex = 1;
            this.valuelbl.Text = "Value: ";
            // 
            // languagelbl
            // 
            this.languagelbl.AutoSize = true;
            this.languagelbl.Location = new System.Drawing.Point(12, 61);
            this.languagelbl.Name = "languagelbl";
            this.languagelbl.Size = new System.Drawing.Size(61, 13);
            this.languagelbl.TabIndex = 2;
            this.languagelbl.Text = "Language: ";
            // 
            // idtxt
            // 
            this.idtxt.Location = new System.Drawing.Point(79, 6);
            this.idtxt.Name = "idtxt";
            this.idtxt.Size = new System.Drawing.Size(119, 20);
            this.idtxt.TabIndex = 0;
            // 
            // valuetxt
            // 
            this.valuetxt.Location = new System.Drawing.Point(79, 32);
            this.valuetxt.Name = "valuetxt";
            this.valuetxt.Size = new System.Drawing.Size(119, 20);
            this.valuetxt.TabIndex = 1;
            // 
            // languagecombo
            // 
            this.languagecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languagecombo.FormattingEnabled = true;
            this.languagecombo.Location = new System.Drawing.Point(79, 58);
            this.languagecombo.Name = "languagecombo";
            this.languagecombo.Size = new System.Drawing.Size(119, 21);
            this.languagecombo.TabIndex = 2;
            // 
            // Okbtn
            // 
            this.Okbtn.Location = new System.Drawing.Point(204, 58);
            this.Okbtn.Name = "Okbtn";
            this.Okbtn.Size = new System.Drawing.Size(75, 23);
            this.Okbtn.TabIndex = 3;
            this.Okbtn.Text = "Ok";
            this.Okbtn.UseVisualStyleBackColor = true;
            this.Okbtn.Click += new System.EventHandler(this.Okbtn_Click);
            // 
            // AddItemForm
            // 
            this.AcceptButton = this.Okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 90);
            this.Controls.Add(this.Okbtn);
            this.Controls.Add(this.languagecombo);
            this.Controls.Add(this.valuetxt);
            this.Controls.Add(this.idtxt);
            this.Controls.Add(this.languagelbl);
            this.Controls.Add(this.valuelbl);
            this.Controls.Add(this.idlbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idlbl;
        private System.Windows.Forms.Label valuelbl;
        private System.Windows.Forms.Label languagelbl;
        private System.Windows.Forms.TextBox idtxt;
        private System.Windows.Forms.TextBox valuetxt;
        private System.Windows.Forms.ComboBox languagecombo;
        private System.Windows.Forms.Button Okbtn;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
