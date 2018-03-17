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
    partial class PrintCommanderMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintCommanderMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.printGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewPrinterPoolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.disablePrinterPoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.removePrinterPoolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hellpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstPrintGroups = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPrinterPoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removePrinterPoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setAsDefaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListPrintItems = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.butClose = new System.Windows.Forms.Button();
            this.butRefresh = new System.Windows.Forms.Button();
            this.butQuickChange = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printGroupsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.hellpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(547, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // printGroupsToolStripMenuItem
            // 
            this.printGroupsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defineToolStripMenuItem,
            this.refreshToolStripMenuItem2,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.printGroupsToolStripMenuItem.Name = "printGroupsToolStripMenuItem";
            this.printGroupsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.printGroupsToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.printGroupsToolStripMenuItem.Text = "File";
            // 
            // defineToolStripMenuItem
            // 
            this.defineToolStripMenuItem.Name = "defineToolStripMenuItem";
            this.defineToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.defineToolStripMenuItem.Text = "Connect to network printserver";
            this.defineToolStripMenuItem.Click += new System.EventHandler(this.defineToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem2
            // 
            this.refreshToolStripMenuItem2.Name = "refreshToolStripMenuItem2";
            this.refreshToolStripMenuItem2.Size = new System.Drawing.Size(238, 22);
            this.refreshToolStripMenuItem2.Text = "Refresh";
            this.refreshToolStripMenuItem2.Click += new System.EventHandler(this.refreshToolStripMenuItem2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(235, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageToolStripMenuItem1,
            this.toolStripSeparator5,
            this.addNewPrinterPoolToolStripMenuItem1,
            this.disablePrinterPoolToolStripMenuItem,
            this.toolStripSeparator4,
            this.removePrinterPoolToolStripMenuItem1});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // manageToolStripMenuItem1
            // 
            this.manageToolStripMenuItem1.Name = "manageToolStripMenuItem1";
            this.manageToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.manageToolStripMenuItem1.Text = "Manage";
            this.manageToolStripMenuItem1.Click += new System.EventHandler(this.manageToolStripMenuItem1_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(183, 6);
            // 
            // addNewPrinterPoolToolStripMenuItem1
            // 
            this.addNewPrinterPoolToolStripMenuItem1.Name = "addNewPrinterPoolToolStripMenuItem1";
            this.addNewPrinterPoolToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.addNewPrinterPoolToolStripMenuItem1.Text = "Add new printer pool";
            this.addNewPrinterPoolToolStripMenuItem1.Click += new System.EventHandler(this.addNewPrinterPoolToolStripMenuItem1_Click);
            // 
            // disablePrinterPoolToolStripMenuItem
            // 
            this.disablePrinterPoolToolStripMenuItem.Name = "disablePrinterPoolToolStripMenuItem";
            this.disablePrinterPoolToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.disablePrinterPoolToolStripMenuItem.Text = "Disable printer pool";
            this.disablePrinterPoolToolStripMenuItem.Click += new System.EventHandler(this.disablePrinterPoolToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(183, 6);
            // 
            // removePrinterPoolToolStripMenuItem1
            // 
            this.removePrinterPoolToolStripMenuItem1.Name = "removePrinterPoolToolStripMenuItem1";
            this.removePrinterPoolToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.removePrinterPoolToolStripMenuItem1.Text = "Remove printer pool";
            this.removePrinterPoolToolStripMenuItem1.Click += new System.EventHandler(this.removePrinterPoolToolStripMenuItem1_Click);
            // 
            // hellpToolStripMenuItem
            // 
            this.hellpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.hellpToolStripMenuItem.Name = "hellpToolStripMenuItem";
            this.hellpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.hellpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hellpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lstPrintGroups
            // 
            this.lstPrintGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPrintGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstPrintGroups.ContextMenuStrip = this.contextMenuStrip1;
            this.lstPrintGroups.LargeImageList = this.imageListPrintItems;
            this.lstPrintGroups.Location = new System.Drawing.Point(13, 28);
            this.lstPrintGroups.Name = "lstPrintGroups";
            this.lstPrintGroups.ShowItemToolTips = true;
            this.lstPrintGroups.Size = new System.Drawing.Size(522, 231);
            this.lstPrintGroups.SmallImageList = this.imageListPrintItems;
            this.lstPrintGroups.TabIndex = 1;
            this.lstPrintGroups.UseCompatibleStateImageBehavior = false;
            this.lstPrintGroups.View = System.Windows.Forms.View.List;
            this.lstPrintGroups.SelectedIndexChanged += new System.EventHandler(this.lstPrintGroups_SelectedIndexChanged);
            this.lstPrintGroups.DoubleClick += new System.EventHandler(this.lstPrintGroups_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Printer Pool";
            this.columnHeader1.Width = 400;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.toolStripSeparator2,
            this.manageToolStripMenuItem,
            this.addNewPrinterPoolToolStripMenuItem,
            this.removePrinterPoolToolStripMenuItem,
            this.toolStripSeparator1,
            this.setAsDefaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(187, 126);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.manageToolStripMenuItem.Text = "&Manage";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
            // 
            // addNewPrinterPoolToolStripMenuItem
            // 
            this.addNewPrinterPoolToolStripMenuItem.Name = "addNewPrinterPoolToolStripMenuItem";
            this.addNewPrinterPoolToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addNewPrinterPoolToolStripMenuItem.Text = "&Add new printer pool";
            this.addNewPrinterPoolToolStripMenuItem.Click += new System.EventHandler(this.addNewPrinterPoolToolStripMenuItem_Click);
            // 
            // removePrinterPoolToolStripMenuItem
            // 
            this.removePrinterPoolToolStripMenuItem.Name = "removePrinterPoolToolStripMenuItem";
            this.removePrinterPoolToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.removePrinterPoolToolStripMenuItem.Text = "&Remove printer pool";
            this.removePrinterPoolToolStripMenuItem.Click += new System.EventHandler(this.removePrinterPoolToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // setAsDefaToolStripMenuItem
            // 
            this.setAsDefaToolStripMenuItem.Name = "setAsDefaToolStripMenuItem";
            this.setAsDefaToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.setAsDefaToolStripMenuItem.Text = "&Disable printer pool";
            this.setAsDefaToolStripMenuItem.Click += new System.EventHandler(this.setAsDefaToolStripMenuItem_Click);
            // 
            // imageListPrintItems
            // 
            this.imageListPrintItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPrintItems.ImageStream")));
            this.imageListPrintItems.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPrintItems.Images.SetKeyName(0, "laser.jpg");
            this.imageListPrintItems.Images.SetKeyName(1, "tec_printer1.jpg");
            this.imageListPrintItems.Images.SetKeyName(2, "tec_printer2.jpg");
            this.imageListPrintItems.Images.SetKeyName(3, "impressora_256x256.png");
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 301);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(547, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(61, 17);
            this.toolStripStatusLabel1.Text = "Initializing";
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butClose.Location = new System.Drawing.Point(460, 266);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 3;
            this.butClose.Text = "Close";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butRefresh
            // 
            this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butRefresh.Location = new System.Drawing.Point(379, 265);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(75, 23);
            this.butRefresh.TabIndex = 4;
            this.butRefresh.Text = "Refresh";
            this.butRefresh.UseVisualStyleBackColor = true;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // butQuickChange
            // 
            this.butQuickChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butQuickChange.Location = new System.Drawing.Point(204, 265);
            this.butQuickChange.Name = "butQuickChange";
            this.butQuickChange.Size = new System.Drawing.Size(169, 23);
            this.butQuickChange.TabIndex = 5;
            this.butQuickChange.Text = "Quick Paper Change";
            this.butQuickChange.UseVisualStyleBackColor = true;
            this.butQuickChange.Click += new System.EventHandler(this.butQuickChange_Click);
            // 
            // PrintCommanderMainForm
            // 
            this.AcceptButton = this.butRefresh;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butClose;
            this.ClientSize = new System.Drawing.Size(547, 323);
            this.Controls.Add(this.butRefresh);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.lstPrintGroups);
            this.Controls.Add(this.butQuickChange);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PrintCommanderMainForm";
            this.Text = "~PRINTCOMMANDERMAINFORMTITLE~";
            this.Load += new System.EventHandler(this.PrintCommanderMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printGroupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListView lstPrintGroups;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPrinterPoolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removePrinterPoolToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem setAsDefaToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ImageList imageListPrintItems;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.Button butRefresh;
        private System.Windows.Forms.Button butQuickChange;
        private System.Windows.Forms.ToolStripMenuItem hellpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addNewPrinterPoolToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removePrinterPoolToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem disablePrinterPoolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
