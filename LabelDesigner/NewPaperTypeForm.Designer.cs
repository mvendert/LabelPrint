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
    partial class NewPaperTypeForm
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
            this.unitcombo = new System.Windows.Forms.ComboBox();
            this.unitlbl = new System.Windows.Forms.Label();
            this.DPIYlbl = new System.Windows.Forms.Label();
            this.DPIYtxt = new System.Windows.Forms.TextBox();
            this.DPIXlbl = new System.Windows.Forms.Label();
            this.DPIXtxt = new System.Windows.Forms.TextBox();
            this.idtxt = new System.Windows.Forms.TextBox();
            this.idlbl = new System.Windows.Forms.Label();
            this.horzlblslbl = new System.Windows.Forms.Label();
            this.vertlblslbl = new System.Windows.Forms.Label();
            this.horzinterlblgaplbl = new System.Windows.Forms.Label();
            this.verticalinterlblgaplbl = new System.Windows.Forms.Label();
            this.horzmarginlbl = new System.Windows.Forms.Label();
            this.vertmarginlbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sizeytxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sizextxt = new System.Windows.Forms.TextBox();
            this.horzmargintxt = new System.Windows.Forms.TextBox();
            this.vertmargintxt = new System.Windows.Forms.TextBox();
            this.nrhorzlblstxt = new System.Windows.Forms.TextBox();
            this.nrvertlblstxt = new System.Windows.Forms.TextBox();
            this.horzgaptxt = new System.Windows.Forms.TextBox();
            this.vertgaptxt = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DPIGroupBox = new System.Windows.Forms.GroupBox();
            this.PaperSizeGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.defhorzoffsetlbl = new System.Windows.Forms.Label();
            this.defvertoffsetlbl = new System.Windows.Forms.Label();
            this.defhorzoffsettxt = new System.Windows.Forms.TextBox();
            this.defvertoffsettxt = new System.Windows.Forms.TextBox();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.GeneralTab = new System.Windows.Forms.TabPage();
            this.SpecificOffsetTab = new System.Windows.Forms.TabPage();
            this.CurrentOffsetsLbl = new System.Windows.Forms.Label();
            this.PropertyBox = new System.Windows.Forms.GroupBox();
            this.DeleteOffsetBtn = new System.Windows.Forms.Button();
            this.SaveOffsetBtn = new System.Windows.Forms.Button();
            this.SpecOffsetVertOffsetTxt = new System.Windows.Forms.TextBox();
            this.SpecOffsetHorzOffsetTxt = new System.Windows.Forms.TextBox();
            this.SpecOffsetPrinterTxt = new System.Windows.Forms.TextBox();
            this.SpecOffsetClientTxt = new System.Windows.Forms.TextBox();
            this.SpecVertOffsetlbl = new System.Windows.Forms.Label();
            this.SpecHorzOffsetlbl = new System.Windows.Forms.Label();
            this.PrinterNamelbl = new System.Windows.Forms.Label();
            this.ClientNamelbl = new System.Windows.Forms.Label();
            this.SpecOffsetListbox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.DPIGroupBox.SuspendLayout();
            this.PaperSizeGroupBox.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.GeneralTab.SuspendLayout();
            this.SpecificOffsetTab.SuspendLayout();
            this.PropertyBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // unitcombo
            // 
            this.unitcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitcombo.FormattingEnabled = true;
            this.unitcombo.Location = new System.Drawing.Point(44, 36);
            this.unitcombo.Name = "unitcombo";
            this.unitcombo.Size = new System.Drawing.Size(100, 21);
            this.unitcombo.TabIndex = 3;
            // 
            // unitlbl
            // 
            this.unitlbl.AutoSize = true;
            this.unitlbl.Location = new System.Drawing.Point(6, 40);
            this.unitlbl.Name = "unitlbl";
            this.unitlbl.Size = new System.Drawing.Size(32, 13);
            this.unitlbl.TabIndex = 40;
            this.unitlbl.Text = "Unit: ";
            // 
            // DPIYlbl
            // 
            this.DPIYlbl.AutoSize = true;
            this.DPIYlbl.Location = new System.Drawing.Point(74, 22);
            this.DPIYlbl.Name = "DPIYlbl";
            this.DPIYlbl.Size = new System.Drawing.Size(20, 13);
            this.DPIYlbl.TabIndex = 39;
            this.DPIYlbl.Text = "Y: ";
            // 
            // DPIYtxt
            // 
            this.DPIYtxt.Location = new System.Drawing.Point(100, 19);
            this.DPIYtxt.Name = "DPIYtxt";
            this.DPIYtxt.Size = new System.Drawing.Size(32, 20);
            this.DPIYtxt.TabIndex = 2;
            this.DPIYtxt.Text = "200";
            // 
            // DPIXlbl
            // 
            this.DPIXlbl.AutoSize = true;
            this.DPIXlbl.Location = new System.Drawing.Point(12, 22);
            this.DPIXlbl.Name = "DPIXlbl";
            this.DPIXlbl.Size = new System.Drawing.Size(20, 13);
            this.DPIXlbl.TabIndex = 37;
            this.DPIXlbl.Text = "X: ";
            // 
            // DPIXtxt
            // 
            this.DPIXtxt.Location = new System.Drawing.Point(38, 19);
            this.DPIXtxt.Name = "DPIXtxt";
            this.DPIXtxt.Size = new System.Drawing.Size(32, 20);
            this.DPIXtxt.TabIndex = 1;
            this.DPIXtxt.Text = "200";
            // 
            // idtxt
            // 
            this.idtxt.Location = new System.Drawing.Point(44, 10);
            this.idtxt.Name = "idtxt";
            this.idtxt.Size = new System.Drawing.Size(100, 20);
            this.idtxt.TabIndex = 0;
            // 
            // idlbl
            // 
            this.idlbl.AutoSize = true;
            this.idlbl.Location = new System.Drawing.Point(6, 13);
            this.idlbl.Name = "idlbl";
            this.idlbl.Size = new System.Drawing.Size(24, 13);
            this.idlbl.TabIndex = 33;
            this.idlbl.Text = "ID :";
            // 
            // horzlblslbl
            // 
            this.horzlblslbl.AutoSize = true;
            this.horzlblslbl.Location = new System.Drawing.Point(6, 20);
            this.horzlblslbl.Name = "horzlblslbl";
            this.horzlblslbl.Size = new System.Drawing.Size(57, 13);
            this.horzlblslbl.TabIndex = 44;
            this.horzlblslbl.Text = "Horizontal:";
            // 
            // vertlblslbl
            // 
            this.vertlblslbl.AutoSize = true;
            this.vertlblslbl.Location = new System.Drawing.Point(6, 46);
            this.vertlblslbl.Name = "vertlblslbl";
            this.vertlblslbl.Size = new System.Drawing.Size(45, 13);
            this.vertlblslbl.TabIndex = 45;
            this.vertlblslbl.Text = "Vertical:";
            // 
            // horzinterlblgaplbl
            // 
            this.horzinterlblgaplbl.AutoSize = true;
            this.horzinterlblgaplbl.Location = new System.Drawing.Point(6, 22);
            this.horzinterlblgaplbl.Name = "horzinterlblgaplbl";
            this.horzinterlblgaplbl.Size = new System.Drawing.Size(80, 13);
            this.horzinterlblgaplbl.TabIndex = 46;
            this.horzinterlblgaplbl.Text = "Horizontal Gap:";
            // 
            // verticalinterlblgaplbl
            // 
            this.verticalinterlblgaplbl.AutoSize = true;
            this.verticalinterlblgaplbl.Location = new System.Drawing.Point(6, 48);
            this.verticalinterlblgaplbl.Name = "verticalinterlblgaplbl";
            this.verticalinterlblgaplbl.Size = new System.Drawing.Size(68, 13);
            this.verticalinterlblgaplbl.TabIndex = 47;
            this.verticalinterlblgaplbl.Text = "Vertical Gap:";
            // 
            // horzmarginlbl
            // 
            this.horzmarginlbl.AutoSize = true;
            this.horzmarginlbl.Location = new System.Drawing.Point(6, 22);
            this.horzmarginlbl.Name = "horzmarginlbl";
            this.horzmarginlbl.Size = new System.Drawing.Size(57, 13);
            this.horzmarginlbl.TabIndex = 48;
            this.horzmarginlbl.Text = "Horizontal:";
            // 
            // vertmarginlbl
            // 
            this.vertmarginlbl.AutoSize = true;
            this.vertmarginlbl.Location = new System.Drawing.Point(6, 48);
            this.vertmarginlbl.Name = "vertmarginlbl";
            this.vertmarginlbl.Size = new System.Drawing.Size(45, 13);
            this.vertmarginlbl.TabIndex = 49;
            this.vertmarginlbl.Text = "Vertical:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Height:";
            // 
            // sizeytxt
            // 
            this.sizeytxt.Location = new System.Drawing.Point(106, 45);
            this.sizeytxt.Name = "sizeytxt";
            this.sizeytxt.Size = new System.Drawing.Size(32, 20);
            this.sizeytxt.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Width: ";
            // 
            // sizextxt
            // 
            this.sizextxt.Location = new System.Drawing.Point(106, 19);
            this.sizextxt.Name = "sizextxt";
            this.sizextxt.Size = new System.Drawing.Size(32, 20);
            this.sizextxt.TabIndex = 4;
            // 
            // horzmargintxt
            // 
            this.horzmargintxt.Location = new System.Drawing.Point(106, 19);
            this.horzmargintxt.Name = "horzmargintxt";
            this.horzmargintxt.Size = new System.Drawing.Size(32, 20);
            this.horzmargintxt.TabIndex = 6;
            // 
            // vertmargintxt
            // 
            this.vertmargintxt.Location = new System.Drawing.Point(106, 45);
            this.vertmargintxt.Name = "vertmargintxt";
            this.vertmargintxt.Size = new System.Drawing.Size(32, 20);
            this.vertmargintxt.TabIndex = 7;
            // 
            // nrhorzlblstxt
            // 
            this.nrhorzlblstxt.Location = new System.Drawing.Point(106, 14);
            this.nrhorzlblstxt.Name = "nrhorzlblstxt";
            this.nrhorzlblstxt.Size = new System.Drawing.Size(32, 20);
            this.nrhorzlblstxt.TabIndex = 8;
            // 
            // nrvertlblstxt
            // 
            this.nrvertlblstxt.Location = new System.Drawing.Point(106, 40);
            this.nrvertlblstxt.Name = "nrvertlblstxt";
            this.nrvertlblstxt.Size = new System.Drawing.Size(32, 20);
            this.nrvertlblstxt.TabIndex = 9;
            // 
            // horzgaptxt
            // 
            this.horzgaptxt.Location = new System.Drawing.Point(106, 19);
            this.horzgaptxt.Name = "horzgaptxt";
            this.horzgaptxt.Size = new System.Drawing.Size(32, 20);
            this.horzgaptxt.TabIndex = 10;
            // 
            // vertgaptxt
            // 
            this.vertgaptxt.Location = new System.Drawing.Point(106, 45);
            this.vertgaptxt.Name = "vertgaptxt";
            this.vertgaptxt.Size = new System.Drawing.Size(32, 20);
            this.vertgaptxt.TabIndex = 11;
            // 
            // okBtn
            // 
            this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBtn.Location = new System.Drawing.Point(253, 376);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 12;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.horzlblslbl);
            this.groupBox1.Controls.Add(this.vertlblslbl);
            this.groupBox1.Controls.Add(this.nrhorzlblstxt);
            this.groupBox1.Controls.Add(this.nrvertlblstxt);
            this.groupBox1.Location = new System.Drawing.Point(160, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 79);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Amount of Labels on Page";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.horzgaptxt);
            this.groupBox2.Controls.Add(this.vertgaptxt);
            this.groupBox2.Controls.Add(this.horzinterlblgaplbl);
            this.groupBox2.Controls.Add(this.verticalinterlblgaplbl);
            this.groupBox2.Location = new System.Drawing.Point(160, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(148, 74);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Area Between Labels";
            // 
            // DPIGroupBox
            // 
            this.DPIGroupBox.Controls.Add(this.DPIYtxt);
            this.DPIGroupBox.Controls.Add(this.DPIXtxt);
            this.DPIGroupBox.Controls.Add(this.DPIXlbl);
            this.DPIGroupBox.Controls.Add(this.DPIYlbl);
            this.DPIGroupBox.Location = new System.Drawing.Point(160, 7);
            this.DPIGroupBox.Name = "DPIGroupBox";
            this.DPIGroupBox.Size = new System.Drawing.Size(148, 50);
            this.DPIGroupBox.TabIndex = 56;
            this.DPIGroupBox.TabStop = false;
            this.DPIGroupBox.Text = "DPI";
            // 
            // PaperSizeGroupBox
            // 
            this.PaperSizeGroupBox.Controls.Add(this.sizextxt);
            this.PaperSizeGroupBox.Controls.Add(this.label4);
            this.PaperSizeGroupBox.Controls.Add(this.sizeytxt);
            this.PaperSizeGroupBox.Controls.Add(this.label2);
            this.PaperSizeGroupBox.Location = new System.Drawing.Point(9, 67);
            this.PaperSizeGroupBox.Name = "PaperSizeGroupBox";
            this.PaperSizeGroupBox.Size = new System.Drawing.Size(148, 75);
            this.PaperSizeGroupBox.TabIndex = 57;
            this.PaperSizeGroupBox.TabStop = false;
            this.PaperSizeGroupBox.Text = "Paper Size";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.horzmarginlbl);
            this.groupBox5.Controls.Add(this.vertmarginlbl);
            this.groupBox5.Controls.Add(this.horzmargintxt);
            this.groupBox5.Controls.Add(this.vertmargintxt);
            this.groupBox5.Location = new System.Drawing.Point(9, 146);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(148, 74);
            this.groupBox5.TabIndex = 58;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Margin";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.defhorzoffsetlbl);
            this.groupBox6.Controls.Add(this.defvertoffsetlbl);
            this.groupBox6.Controls.Add(this.defhorzoffsettxt);
            this.groupBox6.Controls.Add(this.defvertoffsettxt);
            this.groupBox6.Location = new System.Drawing.Point(9, 226);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(148, 74);
            this.groupBox6.TabIndex = 59;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Default Offset";
            // 
            // defhorzoffsetlbl
            // 
            this.defhorzoffsetlbl.AutoSize = true;
            this.defhorzoffsetlbl.Location = new System.Drawing.Point(6, 22);
            this.defhorzoffsetlbl.Name = "defhorzoffsetlbl";
            this.defhorzoffsetlbl.Size = new System.Drawing.Size(57, 13);
            this.defhorzoffsetlbl.TabIndex = 52;
            this.defhorzoffsetlbl.Text = "Horizontal:";
            // 
            // defvertoffsetlbl
            // 
            this.defvertoffsetlbl.AutoSize = true;
            this.defvertoffsetlbl.Location = new System.Drawing.Point(6, 48);
            this.defvertoffsetlbl.Name = "defvertoffsetlbl";
            this.defvertoffsetlbl.Size = new System.Drawing.Size(45, 13);
            this.defvertoffsetlbl.TabIndex = 53;
            this.defvertoffsetlbl.Text = "Vertical:";
            // 
            // defhorzoffsettxt
            // 
            this.defhorzoffsettxt.Location = new System.Drawing.Point(106, 19);
            this.defhorzoffsettxt.Name = "defhorzoffsettxt";
            this.defhorzoffsettxt.Size = new System.Drawing.Size(32, 20);
            this.defhorzoffsettxt.TabIndex = 50;
            // 
            // defvertoffsettxt
            // 
            this.defvertoffsettxt.Location = new System.Drawing.Point(106, 45);
            this.defvertoffsettxt.Name = "defvertoffsettxt";
            this.defvertoffsettxt.Size = new System.Drawing.Size(32, 20);
            this.defvertoffsettxt.TabIndex = 51;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.GeneralTab);
            this.MainTabControl.Controls.Add(this.SpecificOffsetTab);
            this.MainTabControl.Location = new System.Drawing.Point(10, 8);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(326, 363);
            this.MainTabControl.TabIndex = 60;
            // 
            // GeneralTab
            // 
            this.GeneralTab.Controls.Add(this.idlbl);
            this.GeneralTab.Controls.Add(this.groupBox6);
            this.GeneralTab.Controls.Add(this.idtxt);
            this.GeneralTab.Controls.Add(this.groupBox5);
            this.GeneralTab.Controls.Add(this.unitlbl);
            this.GeneralTab.Controls.Add(this.PaperSizeGroupBox);
            this.GeneralTab.Controls.Add(this.unitcombo);
            this.GeneralTab.Controls.Add(this.DPIGroupBox);
            this.GeneralTab.Controls.Add(this.groupBox1);
            this.GeneralTab.Controls.Add(this.groupBox2);
            this.GeneralTab.Location = new System.Drawing.Point(4, 22);
            this.GeneralTab.Name = "GeneralTab";
            this.GeneralTab.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTab.Size = new System.Drawing.Size(318, 337);
            this.GeneralTab.TabIndex = 0;
            this.GeneralTab.Text = "General";
            this.GeneralTab.UseVisualStyleBackColor = true;
            // 
            // SpecificOffsetTab
            // 
            this.SpecificOffsetTab.Controls.Add(this.CurrentOffsetsLbl);
            this.SpecificOffsetTab.Controls.Add(this.PropertyBox);
            this.SpecificOffsetTab.Controls.Add(this.SpecOffsetListbox);
            this.SpecificOffsetTab.Location = new System.Drawing.Point(4, 22);
            this.SpecificOffsetTab.Name = "SpecificOffsetTab";
            this.SpecificOffsetTab.Padding = new System.Windows.Forms.Padding(3);
            this.SpecificOffsetTab.Size = new System.Drawing.Size(318, 337);
            this.SpecificOffsetTab.TabIndex = 1;
            this.SpecificOffsetTab.Text = "Specific Offsets";
            this.SpecificOffsetTab.UseVisualStyleBackColor = true;
            // 
            // CurrentOffsetsLbl
            // 
            this.CurrentOffsetsLbl.AutoSize = true;
            this.CurrentOffsetsLbl.Location = new System.Drawing.Point(12, 3);
            this.CurrentOffsetsLbl.Name = "CurrentOffsetsLbl";
            this.CurrentOffsetsLbl.Size = new System.Drawing.Size(80, 13);
            this.CurrentOffsetsLbl.TabIndex = 10;
            this.CurrentOffsetsLbl.Text = "Current Offsets:";
            // 
            // PropertyBox
            // 
            this.PropertyBox.Controls.Add(this.DeleteOffsetBtn);
            this.PropertyBox.Controls.Add(this.SaveOffsetBtn);
            this.PropertyBox.Controls.Add(this.SpecOffsetVertOffsetTxt);
            this.PropertyBox.Controls.Add(this.SpecOffsetHorzOffsetTxt);
            this.PropertyBox.Controls.Add(this.SpecOffsetPrinterTxt);
            this.PropertyBox.Controls.Add(this.SpecOffsetClientTxt);
            this.PropertyBox.Controls.Add(this.SpecVertOffsetlbl);
            this.PropertyBox.Controls.Add(this.SpecHorzOffsetlbl);
            this.PropertyBox.Controls.Add(this.PrinterNamelbl);
            this.PropertyBox.Controls.Add(this.ClientNamelbl);
            this.PropertyBox.Enabled = false;
            this.PropertyBox.Location = new System.Drawing.Point(6, 170);
            this.PropertyBox.Name = "PropertyBox";
            this.PropertyBox.Size = new System.Drawing.Size(306, 161);
            this.PropertyBox.TabIndex = 9;
            this.PropertyBox.TabStop = false;
            this.PropertyBox.Text = "Properties";
            // 
            // DeleteOffsetBtn
            // 
            this.DeleteOffsetBtn.Location = new System.Drawing.Point(144, 129);
            this.DeleteOffsetBtn.Name = "DeleteOffsetBtn";
            this.DeleteOffsetBtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteOffsetBtn.TabIndex = 18;
            this.DeleteOffsetBtn.Text = "Delete";
            this.DeleteOffsetBtn.UseVisualStyleBackColor = true;
            this.DeleteOffsetBtn.Click += new System.EventHandler(this.DeleteOffsetBtn_Click);
            // 
            // SaveOffsetBtn
            // 
            this.SaveOffsetBtn.Location = new System.Drawing.Point(225, 129);
            this.SaveOffsetBtn.Name = "SaveOffsetBtn";
            this.SaveOffsetBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveOffsetBtn.TabIndex = 17;
            this.SaveOffsetBtn.Text = "Save";
            this.SaveOffsetBtn.UseVisualStyleBackColor = true;
            this.SaveOffsetBtn.Click += new System.EventHandler(this.SaveOffsetBtn_Click);
            // 
            // SpecOffsetVertOffsetTxt
            // 
            this.SpecOffsetVertOffsetTxt.Location = new System.Drawing.Point(111, 99);
            this.SpecOffsetVertOffsetTxt.Name = "SpecOffsetVertOffsetTxt";
            this.SpecOffsetVertOffsetTxt.Size = new System.Drawing.Size(48, 20);
            this.SpecOffsetVertOffsetTxt.TabIndex = 16;
            // 
            // SpecOffsetHorzOffsetTxt
            // 
            this.SpecOffsetHorzOffsetTxt.Location = new System.Drawing.Point(111, 77);
            this.SpecOffsetHorzOffsetTxt.Name = "SpecOffsetHorzOffsetTxt";
            this.SpecOffsetHorzOffsetTxt.Size = new System.Drawing.Size(48, 20);
            this.SpecOffsetHorzOffsetTxt.TabIndex = 15;
            // 
            // SpecOffsetPrinterTxt
            // 
            this.SpecOffsetPrinterTxt.Location = new System.Drawing.Point(111, 51);
            this.SpecOffsetPrinterTxt.Name = "SpecOffsetPrinterTxt";
            this.SpecOffsetPrinterTxt.Size = new System.Drawing.Size(189, 20);
            this.SpecOffsetPrinterTxt.TabIndex = 14;
            // 
            // SpecOffsetClientTxt
            // 
            this.SpecOffsetClientTxt.Location = new System.Drawing.Point(111, 25);
            this.SpecOffsetClientTxt.Name = "SpecOffsetClientTxt";
            this.SpecOffsetClientTxt.Size = new System.Drawing.Size(189, 20);
            this.SpecOffsetClientTxt.TabIndex = 13;
            // 
            // SpecVertOffsetlbl
            // 
            this.SpecVertOffsetlbl.AutoSize = true;
            this.SpecVertOffsetlbl.Location = new System.Drawing.Point(6, 106);
            this.SpecVertOffsetlbl.Name = "SpecVertOffsetlbl";
            this.SpecVertOffsetlbl.Size = new System.Drawing.Size(76, 13);
            this.SpecVertOffsetlbl.TabIndex = 12;
            this.SpecVertOffsetlbl.Text = "Vertical Offset:";
            // 
            // SpecHorzOffsetlbl
            // 
            this.SpecHorzOffsetlbl.AutoSize = true;
            this.SpecHorzOffsetlbl.Location = new System.Drawing.Point(6, 80);
            this.SpecHorzOffsetlbl.Name = "SpecHorzOffsetlbl";
            this.SpecHorzOffsetlbl.Size = new System.Drawing.Size(88, 13);
            this.SpecHorzOffsetlbl.TabIndex = 11;
            this.SpecHorzOffsetlbl.Text = "Horizontal Offset:";
            // 
            // PrinterNamelbl
            // 
            this.PrinterNamelbl.AutoSize = true;
            this.PrinterNamelbl.Location = new System.Drawing.Point(6, 54);
            this.PrinterNamelbl.Name = "PrinterNamelbl";
            this.PrinterNamelbl.Size = new System.Drawing.Size(71, 13);
            this.PrinterNamelbl.TabIndex = 10;
            this.PrinterNamelbl.Text = "Printer Name:";
            // 
            // ClientNamelbl
            // 
            this.ClientNamelbl.AutoSize = true;
            this.ClientNamelbl.Location = new System.Drawing.Point(6, 28);
            this.ClientNamelbl.Name = "ClientNamelbl";
            this.ClientNamelbl.Size = new System.Drawing.Size(67, 13);
            this.ClientNamelbl.TabIndex = 9;
            this.ClientNamelbl.Text = "Client Name:";
            // 
            // SpecOffsetListbox
            // 
            this.SpecOffsetListbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SpecOffsetListbox.FormattingEnabled = true;
            this.SpecOffsetListbox.HorizontalScrollbar = true;
            this.SpecOffsetListbox.Location = new System.Drawing.Point(15, 19);
            this.SpecOffsetListbox.Name = "SpecOffsetListbox";
            this.SpecOffsetListbox.Size = new System.Drawing.Size(291, 147);
            this.SpecOffsetListbox.TabIndex = 0;
            this.SpecOffsetListbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SpecOffsetListbox_MouseClick);
            this.SpecOffsetListbox.SelectedIndexChanged += new System.EventHandler(this.SpecOffsetListbox_SelectedIndexChanged);
            // 
            // NewPaperTypeForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 411);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.okBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewPaperTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create a new paper definition";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.DPIGroupBox.ResumeLayout(false);
            this.DPIGroupBox.PerformLayout();
            this.PaperSizeGroupBox.ResumeLayout(false);
            this.PaperSizeGroupBox.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.MainTabControl.ResumeLayout(false);
            this.GeneralTab.ResumeLayout(false);
            this.GeneralTab.PerformLayout();
            this.SpecificOffsetTab.ResumeLayout(false);
            this.SpecificOffsetTab.PerformLayout();
            this.PropertyBox.ResumeLayout(false);
            this.PropertyBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox unitcombo;
        private System.Windows.Forms.Label unitlbl;
        private System.Windows.Forms.Label DPIYlbl;
        private System.Windows.Forms.TextBox DPIYtxt;
        private System.Windows.Forms.Label DPIXlbl;
        private System.Windows.Forms.TextBox DPIXtxt;
        private System.Windows.Forms.TextBox idtxt;
        private System.Windows.Forms.Label idlbl;
        private System.Windows.Forms.Label horzlblslbl;
        private System.Windows.Forms.Label vertlblslbl;
        private System.Windows.Forms.Label horzinterlblgaplbl;
        private System.Windows.Forms.Label verticalinterlblgaplbl;
        private System.Windows.Forms.Label horzmarginlbl;
        private System.Windows.Forms.Label vertmarginlbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sizeytxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sizextxt;
        private System.Windows.Forms.TextBox horzmargintxt;
        private System.Windows.Forms.TextBox vertmargintxt;
        private System.Windows.Forms.TextBox nrhorzlblstxt;
        private System.Windows.Forms.TextBox nrvertlblstxt;
        private System.Windows.Forms.TextBox horzgaptxt;
        private System.Windows.Forms.TextBox vertgaptxt;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox DPIGroupBox;
        private System.Windows.Forms.GroupBox PaperSizeGroupBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label defhorzoffsetlbl;
        private System.Windows.Forms.Label defvertoffsetlbl;
        private System.Windows.Forms.TextBox defhorzoffsettxt;
        private System.Windows.Forms.TextBox defvertoffsettxt;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage GeneralTab;
        private System.Windows.Forms.TabPage SpecificOffsetTab;
        private System.Windows.Forms.ListBox SpecOffsetListbox;
        private System.Windows.Forms.GroupBox PropertyBox;
        private System.Windows.Forms.Button SaveOffsetBtn;
        private System.Windows.Forms.TextBox SpecOffsetVertOffsetTxt;
        private System.Windows.Forms.TextBox SpecOffsetHorzOffsetTxt;
        private System.Windows.Forms.TextBox SpecOffsetPrinterTxt;
        private System.Windows.Forms.TextBox SpecOffsetClientTxt;
        private System.Windows.Forms.Label SpecVertOffsetlbl;
        private System.Windows.Forms.Label SpecHorzOffsetlbl;
        private System.Windows.Forms.Label PrinterNamelbl;
        private System.Windows.Forms.Label ClientNamelbl;
        private System.Windows.Forms.Label CurrentOffsetsLbl;
        private System.Windows.Forms.Button DeleteOffsetBtn;
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
