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
namespace LabelControler
{
    partial class LabelPrinterForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelPrinterForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblPrinterPoolName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPrinterName = new System.Windows.Forms.Label();
            this.lstViewTrays = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOK = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeSelectedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addTrayToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedTrayToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pool";
            // 
            // lblPrinterPoolName
            // 
            this.lblPrinterPoolName.AutoSize = true;
            this.lblPrinterPoolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterPoolName.Location = new System.Drawing.Point(48, 13);
            this.lblPrinterPoolName.Name = "lblPrinterPoolName";
            this.lblPrinterPoolName.Size = new System.Drawing.Size(114, 13);
            this.lblPrinterPoolName.TabIndex = 1;
            this.lblPrinterPoolName.Text = "lblPrinterPoolName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Printer";
            // 
            // lblPrinterName
            // 
            this.lblPrinterName.AutoSize = true;
            this.lblPrinterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterName.Location = new System.Drawing.Point(262, 12);
            this.lblPrinterName.Name = "lblPrinterName";
            this.lblPrinterName.Size = new System.Drawing.Size(89, 13);
            this.lblPrinterName.TabIndex = 3;
            this.lblPrinterName.Text = "lblPrinterName";
            // 
            // lstViewTrays
            // 
            this.lstViewTrays.ContextMenuStrip = this.contextMenuStrip1;
            this.lstViewTrays.Location = new System.Drawing.Point(16, 53);
            this.lstViewTrays.Name = "lstViewTrays";
            this.lstViewTrays.Size = new System.Drawing.Size(484, 163);
            this.lstViewTrays.TabIndex = 4;
            this.lstViewTrays.UseCompatibleStateImageBehavior = false;
            this.lstViewTrays.SelectedIndexChanged += new System.EventHandler(this.lstViewTrays_SelectedIndexChanged);
            this.lstViewTrays.DoubleClick += new System.EventHandler(this.lstViewTrays_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeSelectedToolStripMenuItem,
            this.addTrayToolStripMenuItem,
            this.removeSelectedTrayToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 70);
            // 
            // changeSelectedToolStripMenuItem
            // 
            this.changeSelectedToolStripMenuItem.Name = "changeSelectedToolStripMenuItem";
            this.changeSelectedToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.changeSelectedToolStripMenuItem.Text = "Change tray";
            this.changeSelectedToolStripMenuItem.Click += new System.EventHandler(this.changeSelectedToolStripMenuItem_Click);
            // 
            // addTrayToolStripMenuItem
            // 
            this.addTrayToolStripMenuItem.Name = "addTrayToolStripMenuItem";
            this.addTrayToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.addTrayToolStripMenuItem.Text = "Add tray";
            this.addTrayToolStripMenuItem.Click += new System.EventHandler(this.addTrayToolStripMenuItem_Click);
            // 
            // removeSelectedTrayToolStripMenuItem
            // 
            this.removeSelectedTrayToolStripMenuItem.Name = "removeSelectedTrayToolStripMenuItem";
            this.removeSelectedTrayToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.removeSelectedTrayToolStripMenuItem.Text = "Remove tray";
            this.removeSelectedTrayToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedTrayToolStripMenuItem_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(424, 289);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 6;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(343, 289);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 7;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optiesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 24);
            this.menuStrip1.TabIndex = 8;
            // 
            // optiesToolStripMenuItem
            // 
            this.optiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeSelectedToolStripMenuItem1,
            this.addTrayToolStripMenuItem1,
            this.removeSelectedTrayToolStripMenuItem1});
            this.optiesToolStripMenuItem.Name = "optiesToolStripMenuItem";
            this.optiesToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.optiesToolStripMenuItem.Text = "Opties";
            // 
            // changeSelectedToolStripMenuItem1
            // 
            this.changeSelectedToolStripMenuItem1.Name = "changeSelectedToolStripMenuItem1";
            this.changeSelectedToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.changeSelectedToolStripMenuItem1.Text = "Change tray";
            this.changeSelectedToolStripMenuItem1.Click += new System.EventHandler(this.changeSelectedToolStripMenuItem1_Click);
            // 
            // addTrayToolStripMenuItem1
            // 
            this.addTrayToolStripMenuItem1.Name = "addTrayToolStripMenuItem1";
            this.addTrayToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.addTrayToolStripMenuItem1.Text = "Add tray";
            this.addTrayToolStripMenuItem1.Click += new System.EventHandler(this.addTrayToolStripMenuItem1_Click);
            // 
            // removeSelectedTrayToolStripMenuItem1
            // 
            this.removeSelectedTrayToolStripMenuItem1.Name = "removeSelectedTrayToolStripMenuItem1";
            this.removeSelectedTrayToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.removeSelectedTrayToolStripMenuItem1.Text = "Remove tray";
            this.removeSelectedTrayToolStripMenuItem1.Click += new System.EventHandler(this.removeSelectedTrayToolStripMenuItem1_Click);
            // 
            // LabelPrinterForm
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(512, 324);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.lstViewTrays);
            this.Controls.Add(this.lblPrinterName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPrinterPoolName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LabelPrinterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Trays and Papertypes";
            this.Load += new System.EventHandler(this.LabelPrinterForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPrinterPoolName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPrinterName;
        private System.Windows.Forms.ListView lstViewTrays;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changeSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedTrayToolStripMenuItem;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeSelectedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addTrayToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedTrayToolStripMenuItem1;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
