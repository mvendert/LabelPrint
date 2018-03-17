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
namespace ACA.LabelX.PrintEngine
{
    partial class Form1
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
            this.buttonPrintPreview = new System.Windows.Forms.Button();
            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.butPrintInfo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.butBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBaseFolder = new System.Windows.Forms.TextBox();
            this.butBrowsebase = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPaperDef = new System.Windows.Forms.TextBox();
            this.butPaperDef = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLabelDef = new System.Windows.Forms.TextBox();
            this.butLabelDef = new System.Windows.Forms.Button();
            this.cmbTray = new System.Windows.Forms.ComboBox();
            this.cmbPaper = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonPrintPreview
            // 
            this.buttonPrintPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPrintPreview.Location = new System.Drawing.Point(18, 270);
            this.buttonPrintPreview.Name = "buttonPrintPreview";
            this.buttonPrintPreview.Size = new System.Drawing.Size(130, 23);
            this.buttonPrintPreview.TabIndex = 0;
            this.buttonPrintPreview.Text = "Print Preview";
            this.buttonPrintPreview.UseVisualStyleBackColor = true;
            this.buttonPrintPreview.Click += new System.EventHandler(this.buttonPrintPreview_Click);
            // 
            // chkDebugMode
            // 
            this.chkDebugMode.AutoSize = true;
            this.chkDebugMode.Checked = true;
            this.chkDebugMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDebugMode.Location = new System.Drawing.Point(18, 238);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(88, 17);
            this.chkDebugMode.TabIndex = 1;
            this.chkDebugMode.Text = "Debug Mode";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(154, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Direct Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "XML Printjob file";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(107, 119);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(340, 20);
            this.txtFilename.TabIndex = 4;
            this.txtFilename.Text = "acatk10job.xml";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Printername";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Papertype";
            // 
            // butPrintInfo
            // 
            this.butPrintInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPrintInfo.Location = new System.Drawing.Point(444, 270);
            this.butPrintInfo.Name = "butPrintInfo";
            this.butPrintInfo.Size = new System.Drawing.Size(75, 23);
            this.butPrintInfo.TabIndex = 9;
            this.butPrintInfo.Text = "Printer Info";
            this.butPrintInfo.UseVisualStyleBackColor = true;
            this.butPrintInfo.Click += new System.EventHandler(this.butPrintInfo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tray";
            // 
            // butBrowse
            // 
            this.butBrowse.Location = new System.Drawing.Point(444, 119);
            this.butBrowse.Name = "butBrowse";
            this.butBrowse.Size = new System.Drawing.Size(75, 23);
            this.butBrowse.TabIndex = 12;
            this.butBrowse.Text = "Browse";
            this.butBrowse.UseVisualStyleBackColor = true;
            this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.FileName = "*";
            this.openFileDialog1.Filter = "XML Printjob files|*.xml";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Printgroup folder";
            // 
            // txtBaseFolder
            // 
            this.txtBaseFolder.Location = new System.Drawing.Point(107, 58);
            this.txtBaseFolder.Name = "txtBaseFolder";
            this.txtBaseFolder.Size = new System.Drawing.Size(340, 20);
            this.txtBaseFolder.TabIndex = 15;
            this.txtBaseFolder.Text = "C:\\ACALabelX\\Client\\PrintJobs\\PrintGroup1";
            // 
            // butBrowsebase
            // 
            this.butBrowsebase.Location = new System.Drawing.Point(444, 58);
            this.butBrowsebase.Name = "butBrowsebase";
            this.butBrowsebase.Size = new System.Drawing.Size(75, 23);
            this.butBrowsebase.TabIndex = 16;
            this.butBrowsebase.Text = "Browse";
            this.butBrowsebase.UseVisualStyleBackColor = true;
            this.butBrowsebase.Click += new System.EventHandler(this.butBrowsebase_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(107, 145);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(121, 21);
            this.cmbPrinters.TabIndex = 17;
            this.cmbPrinters.SelectedIndexChanged += new System.EventHandler(this.cmbPrinters_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Paperdef folder";
            // 
            // txtPaperDef
            // 
            this.txtPaperDef.Location = new System.Drawing.Point(107, 32);
            this.txtPaperDef.Name = "txtPaperDef";
            this.txtPaperDef.Size = new System.Drawing.Size(340, 20);
            this.txtPaperDef.TabIndex = 19;
            this.txtPaperDef.Text = "C:\\ACALabelX\\Client\\PaperDefinitions";
            this.txtPaperDef.TextChanged += new System.EventHandler(this.txtPaperDef_TextChanged);
            // 
            // butPaperDef
            // 
            this.butPaperDef.Location = new System.Drawing.Point(444, 31);
            this.butPaperDef.Name = "butPaperDef";
            this.butPaperDef.Size = new System.Drawing.Size(75, 23);
            this.butPaperDef.TabIndex = 20;
            this.butPaperDef.Text = "Browse";
            this.butPaperDef.UseVisualStyleBackColor = true;
            this.butPaperDef.Click += new System.EventHandler(this.butPaperDef_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "LabelDef folder";
            // 
            // txtLabelDef
            // 
            this.txtLabelDef.Location = new System.Drawing.Point(107, 5);
            this.txtLabelDef.Name = "txtLabelDef";
            this.txtLabelDef.Size = new System.Drawing.Size(340, 20);
            this.txtLabelDef.TabIndex = 22;
            this.txtLabelDef.Text = "C:\\ACALabelX\\Client\\LabelDefinitions";
            // 
            // butLabelDef
            // 
            this.butLabelDef.Location = new System.Drawing.Point(444, 3);
            this.butLabelDef.Name = "butLabelDef";
            this.butLabelDef.Size = new System.Drawing.Size(75, 23);
            this.butLabelDef.TabIndex = 23;
            this.butLabelDef.Text = "Browse";
            this.butLabelDef.UseVisualStyleBackColor = true;
            this.butLabelDef.Click += new System.EventHandler(this.butLabelDef_Click);
            // 
            // cmbTray
            // 
            this.cmbTray.FormattingEnabled = true;
            this.cmbTray.Location = new System.Drawing.Point(107, 171);
            this.cmbTray.Name = "cmbTray";
            this.cmbTray.Size = new System.Drawing.Size(121, 21);
            this.cmbTray.TabIndex = 24;
            // 
            // cmbPaper
            // 
            this.cmbPaper.FormattingEnabled = true;
            this.cmbPaper.Location = new System.Drawing.Point(107, 200);
            this.cmbPaper.Name = "cmbPaper";
            this.cmbPaper.Size = new System.Drawing.Size(121, 21);
            this.cmbPaper.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(235, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(251, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "(The papertype in the label definition will be ignored)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(235, 238);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "(Print borders around each field)";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(12, 314);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(527, 55);
            this.label10.TabIndex = 28;
            this.label10.Text = "(Remember: This program will crash if ANY of the settings are incorrec. Set them " +
                "via browse or select tem in de combobox)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 378);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbPaper);
            this.Controls.Add(this.cmbTray);
            this.Controls.Add(this.butLabelDef);
            this.Controls.Add(this.txtLabelDef);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.butPaperDef);
            this.Controls.Add(this.txtPaperDef);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbPrinters);
            this.Controls.Add(this.butBrowsebase);
            this.Controls.Add(this.txtBaseFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.butBrowse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.butPrintInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkDebugMode);
            this.Controls.Add(this.buttonPrintPreview);
            this.Name = "Form1";
            this.Text = "Labelprint Previewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPrintPreview;
        private System.Windows.Forms.CheckBox chkDebugMode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butPrintInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button butBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBaseFolder;
        private System.Windows.Forms.Button butBrowsebase;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox cmbPrinters;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPaperDef;
        private System.Windows.Forms.Button butPaperDef;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLabelDef;
        private System.Windows.Forms.Button butLabelDef;
        private System.Windows.Forms.ComboBox cmbTray;
        private System.Windows.Forms.ComboBox cmbPaper;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
