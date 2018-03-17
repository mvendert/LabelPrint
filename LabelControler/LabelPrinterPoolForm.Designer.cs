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
    partial class LabelPrinterPoolForm
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
            if (disposing)
            {
                pgi = null;
            }
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("There are currently no printers in this pool. Use right-click to add a printer to" +
        " the pool.");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("There are currently no printjobs to handle.");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelPrinterPoolForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblPoolName = new System.Windows.Forms.Label();
            this.lstViewPrinters = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripPrinters = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changePapertypeInSelectedPrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.disablePrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addPrinterToPoolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removePrinterFromPoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewPrintJobs = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripJobs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.suspendJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discardJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.printersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePaperTypeInTheSelectedPrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.disablePrinterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.addPrintersToPoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedPrinterFromPoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printjobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suspendPrintjobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discardPrintjobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.viewPrintjobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.butClose = new System.Windows.Forms.Button();
            this.butRefresh = new System.Windows.Forms.Button();
            this.timerVervers = new System.Windows.Forms.Timer(this.components);
            this.lblTikken = new System.Windows.Forms.Label();
            this.contextMenuStripPrinters.SuspendLayout();
            this.contextMenuStripJobs.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pool";
            // 
            // lblPoolName
            // 
            this.lblPoolName.AutoSize = true;
            this.lblPoolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoolName.Location = new System.Drawing.Point(50, 24);
            this.lblPoolName.Name = "lblPoolName";
            this.lblPoolName.Size = new System.Drawing.Size(77, 13);
            this.lblPoolName.TabIndex = 1;
            this.lblPoolName.Text = "lblPoolName";
            // 
            // lstViewPrinters
            // 
            this.lstViewPrinters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstViewPrinters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstViewPrinters.ContextMenuStrip = this.contextMenuStripPrinters;
            this.lstViewPrinters.FullRowSelect = true;
            this.lstViewPrinters.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lstViewPrinters.Location = new System.Drawing.Point(16, 63);
            this.lstViewPrinters.MultiSelect = false;
            this.lstViewPrinters.Name = "lstViewPrinters";
            this.lstViewPrinters.Size = new System.Drawing.Size(577, 117);
            this.lstViewPrinters.TabIndex = 2;
            this.lstViewPrinters.UseCompatibleStateImageBehavior = false;
            this.lstViewPrinters.View = System.Windows.Forms.View.List;
            this.lstViewPrinters.SelectedIndexChanged += new System.EventHandler(this.lstViewPrinters_SelectedIndexChanged);
            this.lstViewPrinters.DoubleClick += new System.EventHandler(this.lstViewPrinters_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Printer Name";
            this.columnHeader1.Width = 300;
            // 
            // contextMenuStripPrinters
            // 
            this.contextMenuStripPrinters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePapertypeInSelectedPrinterToolStripMenuItem,
            this.toolStripSeparator2,
            this.disablePrinterToolStripMenuItem,
            this.toolStripSeparator1,
            this.addPrinterToPoolToolStripMenuItem1,
            this.removePrinterFromPoolToolStripMenuItem});
            this.contextMenuStripPrinters.Name = "contextMenuStripPrinters";
            this.contextMenuStripPrinters.Size = new System.Drawing.Size(212, 104);
            this.contextMenuStripPrinters.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStripPrinters_Closed);
            this.contextMenuStripPrinters.Opened += new System.EventHandler(this.contextMenuStripPrinters_Opened);
            // 
            // changePapertypeInSelectedPrinterToolStripMenuItem
            // 
            this.changePapertypeInSelectedPrinterToolStripMenuItem.Name = "changePapertypeInSelectedPrinterToolStripMenuItem";
            this.changePapertypeInSelectedPrinterToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.changePapertypeInSelectedPrinterToolStripMenuItem.Text = "Manage Printer";
            this.changePapertypeInSelectedPrinterToolStripMenuItem.Click += new System.EventHandler(this.changePapertypeInSelectedPrinterToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(208, 6);
            // 
            // disablePrinterToolStripMenuItem
            // 
            this.disablePrinterToolStripMenuItem.Name = "disablePrinterToolStripMenuItem";
            this.disablePrinterToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.disablePrinterToolStripMenuItem.Text = "Disable printer";
            this.disablePrinterToolStripMenuItem.Click += new System.EventHandler(this.disablePrinterToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // addPrinterToPoolToolStripMenuItem1
            // 
            this.addPrinterToPoolToolStripMenuItem1.Name = "addPrinterToPoolToolStripMenuItem1";
            this.addPrinterToPoolToolStripMenuItem1.Size = new System.Drawing.Size(211, 22);
            this.addPrinterToPoolToolStripMenuItem1.Text = "Add printer to pool";
            this.addPrinterToPoolToolStripMenuItem1.Click += new System.EventHandler(this.addPrinterToPoolToolStripMenuItem1_Click);
            // 
            // removePrinterFromPoolToolStripMenuItem
            // 
            this.removePrinterFromPoolToolStripMenuItem.Name = "removePrinterFromPoolToolStripMenuItem";
            this.removePrinterFromPoolToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.removePrinterFromPoolToolStripMenuItem.Text = "Remove printer from pool";
            this.removePrinterFromPoolToolStripMenuItem.Click += new System.EventHandler(this.removePrinterFromPoolToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Printers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Printjobs";
            // 
            // listViewPrintJobs
            // 
            this.listViewPrintJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPrintJobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewPrintJobs.ContextMenuStrip = this.contextMenuStripJobs;
            this.listViewPrintJobs.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listViewPrintJobs.Location = new System.Drawing.Point(16, 212);
            this.listViewPrintJobs.Name = "listViewPrintJobs";
            this.listViewPrintJobs.Size = new System.Drawing.Size(574, 95);
            this.listViewPrintJobs.TabIndex = 5;
            this.listViewPrintJobs.UseCompatibleStateImageBehavior = false;
            this.listViewPrintJobs.View = System.Windows.Forms.View.List;
            this.listViewPrintJobs.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewPrintJobs_ColumnClick);
            this.listViewPrintJobs.SelectedIndexChanged += new System.EventHandler(this.listViewPrintJobs_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Printjob";
            this.columnHeader2.Width = 300;
            // 
            // contextMenuStripJobs
            // 
            this.contextMenuStripJobs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.suspendJobToolStripMenuItem,
            this.discardJobToolStripMenuItem,
            this.toolStripSeparator6,
            this.viewToolStripMenuItem});
            this.contextMenuStripJobs.Name = "contextMenuStripJobs";
            this.contextMenuStripJobs.Size = new System.Drawing.Size(141, 76);
            this.contextMenuStripJobs.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStripJobs_Closed);
            this.contextMenuStripJobs.Opened += new System.EventHandler(this.contextMenuStripJobs_Opened);
            // 
            // suspendJobToolStripMenuItem
            // 
            this.suspendJobToolStripMenuItem.Name = "suspendJobToolStripMenuItem";
            this.suspendJobToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.suspendJobToolStripMenuItem.Text = "Suspend Job";
            this.suspendJobToolStripMenuItem.Click += new System.EventHandler(this.suspendJobToolStripMenuItem_Click);
            // 
            // discardJobToolStripMenuItem
            // 
            this.discardJobToolStripMenuItem.Name = "discardJobToolStripMenuItem";
            this.discardJobToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.discardJobToolStripMenuItem.Text = "Discard Job";
            this.discardJobToolStripMenuItem.Click += new System.EventHandler(this.discardJobToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(137, 6);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLViewToolStripMenuItem,
            this.graphToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // xMLViewToolStripMenuItem
            // 
            this.xMLViewToolStripMenuItem.Name = "xMLViewToolStripMenuItem";
            this.xMLViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xMLViewToolStripMenuItem.Text = "XML View";
            this.xMLViewToolStripMenuItem.Click += new System.EventHandler(this.xMLViewToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.graphToolStripMenuItem.Text = "Graphical View";
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.graphToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printersToolStripMenuItem,
            this.printjobsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(602, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // printersToolStripMenuItem
            // 
            this.printersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePaperTypeInTheSelectedPrinterToolStripMenuItem,
            this.toolStripSeparator5,
            this.disablePrinterToolStripMenuItem1,
            this.toolStripSeparator4,
            this.addPrintersToPoolToolStripMenuItem,
            this.removeSelectedPrinterFromPoolToolStripMenuItem});
            this.printersToolStripMenuItem.Name = "printersToolStripMenuItem";
            this.printersToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.printersToolStripMenuItem.Text = "Printers";
            // 
            // changePaperTypeInTheSelectedPrinterToolStripMenuItem
            // 
            this.changePaperTypeInTheSelectedPrinterToolStripMenuItem.Name = "changePaperTypeInTheSelectedPrinterToolStripMenuItem";
            this.changePaperTypeInTheSelectedPrinterToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.changePaperTypeInTheSelectedPrinterToolStripMenuItem.Text = "Manage printer";
            this.changePaperTypeInTheSelectedPrinterToolStripMenuItem.Click += new System.EventHandler(this.changePaperTypeInTheSelectedPrinterToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(208, 6);
            // 
            // disablePrinterToolStripMenuItem1
            // 
            this.disablePrinterToolStripMenuItem1.Name = "disablePrinterToolStripMenuItem1";
            this.disablePrinterToolStripMenuItem1.Size = new System.Drawing.Size(211, 22);
            this.disablePrinterToolStripMenuItem1.Text = "Disable printer";
            this.disablePrinterToolStripMenuItem1.Click += new System.EventHandler(this.disablePrinterToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(208, 6);
            // 
            // addPrintersToPoolToolStripMenuItem
            // 
            this.addPrintersToPoolToolStripMenuItem.Name = "addPrintersToPoolToolStripMenuItem";
            this.addPrintersToPoolToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.addPrintersToPoolToolStripMenuItem.Text = "Add printer to pool";
            this.addPrintersToPoolToolStripMenuItem.Click += new System.EventHandler(this.addPrintersToPoolToolStripMenuItem_Click);
            // 
            // removeSelectedPrinterFromPoolToolStripMenuItem
            // 
            this.removeSelectedPrinterFromPoolToolStripMenuItem.Name = "removeSelectedPrinterFromPoolToolStripMenuItem";
            this.removeSelectedPrinterFromPoolToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.removeSelectedPrinterFromPoolToolStripMenuItem.Text = "Remove printer from pool";
            this.removeSelectedPrinterFromPoolToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedPrinterFromPoolToolStripMenuItem_Click);
            // 
            // printjobsToolStripMenuItem
            // 
            this.printjobsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.suspendPrintjobToolStripMenuItem,
            this.discardPrintjobToolStripMenuItem,
            this.toolStripSeparator3,
            this.viewPrintjobToolStripMenuItem});
            this.printjobsToolStripMenuItem.Name = "printjobsToolStripMenuItem";
            this.printjobsToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.printjobsToolStripMenuItem.Text = "Printjobs";
            // 
            // suspendPrintjobToolStripMenuItem
            // 
            this.suspendPrintjobToolStripMenuItem.Name = "suspendPrintjobToolStripMenuItem";
            this.suspendPrintjobToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.suspendPrintjobToolStripMenuItem.Text = "Suspend printjob";
            this.suspendPrintjobToolStripMenuItem.Click += new System.EventHandler(this.suspendPrintjobToolStripMenuItem_Click);
            // 
            // discardPrintjobToolStripMenuItem
            // 
            this.discardPrintjobToolStripMenuItem.Name = "discardPrintjobToolStripMenuItem";
            this.discardPrintjobToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.discardPrintjobToolStripMenuItem.Text = "Discard printjob";
            this.discardPrintjobToolStripMenuItem.Click += new System.EventHandler(this.discardPrintjobToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // viewPrintjobToolStripMenuItem
            // 
            this.viewPrintjobToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLToolStripMenuItem1,
            this.graphicalToolStripMenuItem1});
            this.viewPrintjobToolStripMenuItem.Name = "viewPrintjobToolStripMenuItem";
            this.viewPrintjobToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.viewPrintjobToolStripMenuItem.Text = "View printjob";
            // 
            // xMLToolStripMenuItem1
            // 
            this.xMLToolStripMenuItem1.Name = "xMLToolStripMenuItem1";
            this.xMLToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.xMLToolStripMenuItem1.Text = "XML";
            this.xMLToolStripMenuItem1.Click += new System.EventHandler(this.xMLToolStripMenuItem1_Click);
            // 
            // graphicalToolStripMenuItem1
            // 
            this.graphicalToolStripMenuItem1.Name = "graphicalToolStripMenuItem1";
            this.graphicalToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.graphicalToolStripMenuItem1.Text = "Graphical";
            this.graphicalToolStripMenuItem1.Click += new System.EventHandler(this.graphicalToolStripMenuItem1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 344);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(602, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabel1.Text = "Initializing...";
            // 
            // butClose
            // 
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butClose.Location = new System.Drawing.Point(515, 314);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 8;
            this.butClose.Text = "Close";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // butRefresh
            // 
            this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butRefresh.Location = new System.Drawing.Point(434, 314);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(75, 23);
            this.butRefresh.TabIndex = 9;
            this.butRefresh.Text = "Refresh";
            this.butRefresh.UseVisualStyleBackColor = true;
            this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
            // 
            // timerVervers
            // 
            this.timerVervers.Interval = 1000;
            this.timerVervers.Tick += new System.EventHandler(this.timerVervers_Tick);
            // 
            // lblTikken
            // 
            this.lblTikken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTikken.AutoSize = true;
            this.lblTikken.Location = new System.Drawing.Point(16, 319);
            this.lblTikken.Name = "lblTikken";
            this.lblTikken.Size = new System.Drawing.Size(13, 13);
            this.lblTikken.TabIndex = 10;
            this.lblTikken.Text = "0";
            this.lblTikken.Visible = false;
            // 
            // LabelPrinterPoolForm
            // 
            this.AcceptButton = this.butRefresh;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.butClose;
            this.ClientSize = new System.Drawing.Size(602, 366);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblTikken);
            this.Controls.Add(this.butRefresh);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listViewPrintJobs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstViewPrinters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPoolName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1024, 1024);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "LabelPrinterPoolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage a printerpool";
            this.Load += new System.EventHandler(this.LabelPrinterPoolForm_Load);
            this.contextMenuStripPrinters.ResumeLayout(false);
            this.contextMenuStripJobs.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPoolName;
        private System.Windows.Forms.ListView lstViewPrinters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewPrintJobs;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPrintersToPoolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedPrinterFromPoolToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPrinters;
        private System.Windows.Forms.ToolStripMenuItem removePrinterFromPoolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disablePrinterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePapertypeInSelectedPrinterToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripJobs;
        private System.Windows.Forms.ToolStripMenuItem suspendJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discardJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPrinterToPoolToolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem changePaperTypeInTheSelectedPrinterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.Button butRefresh;
        private System.Windows.Forms.Timer timerVervers;
        private System.Windows.Forms.Label lblTikken;
        private System.Windows.Forms.ToolStripMenuItem xMLViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disablePrinterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem printjobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suspendPrintjobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewPrintjobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem graphicalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem discardPrintjobToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
