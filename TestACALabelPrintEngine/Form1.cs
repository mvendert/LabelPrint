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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ACA.LabelX.PrintJob;
using System.Printing;
using System.Collections;
using System.Printing.IndexedProperties;
using System.IO;


namespace ACA.LabelX.PrintEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SelectPrinter(sender, e);
            txtPaperDef.Text = @"C:\ACALabelX\Client\PaperDefinition";
            txtPaperDef.Text = @"C:\ACALabelX\Client\PaperDefinitions";
        }

        private void buttonPrintPreview_Click(object sender, EventArgs e)
        {
            PrintJob.PrintJob printJob = new PrintJob.PrintJob(txtPaperDef.Text, txtLabelDef.Text + @"\");
            try
            {
                printJob.Parse(txtBaseFolder.Text + @"\" + txtFilename.Text);


                PrintEngine printEngine = new PrintEngine("pc-psmo");
                printEngine.DesignMode = chkDebugMode.Checked;
                printEngine.AddPrintJob(printJob);
                printEngine.PrintPreview(cmbPrinters.Text, cmbPaper.Text, cmbTray.Text, chkDebugMode.Checked, 0, uint.MaxValue);
            }
            catch
            {
                MessageBox.Show("Error trying to show print preview.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintJob.PrintJob printJob = new PrintJob.PrintJob(txtPaperDef.Text, txtLabelDef.Text + @"\");
            try{
                printJob.Parse(txtBaseFolder.Text + @"\" + txtFilename.Text);

                PrintEngine pi = new PrintEngine("pc-mve2");
                pi.DesignMode = false;
                pi.AddPrintJob(printJob);
                pi.Print("doc", cmbPrinters.Text, cmbTray.Text, cmbPaper.Text, 0, int.MaxValue, 1043);
            }
            catch
            {
                MessageBox.Show("Error trying to print.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPapertype_TextChanged(object sender, EventArgs e)
        {

        }

        private void butPrintInfo_Click(object sender, EventArgs e)
        {
            System.Printing.LocalPrintServer theServer;
            theServer = new System.Printing.LocalPrintServer();
            System.Printing.EnumeratedPrintQueueTypes[] myEnum =
            {
                EnumeratedPrintQueueTypes.Connections
               ,EnumeratedPrintQueueTypes.Local 
            };
            System.Printing.PrintQueueCollection col = theServer.GetPrintQueues(myEnum);

            foreach (System.Printing.PrintQueue qit in col)
            {
                System.Diagnostics.Debug.WriteLine(qit.ClientPrintSchemaVersion);
                System.Diagnostics.Debug.WriteLine(qit.Comment);
                System.Diagnostics.Debug.WriteLine(qit.CurrentJobSettings.Description);
                System.Diagnostics.Debug.WriteLine(qit.Description);
                System.Diagnostics.Debug.WriteLine(qit.FullName);
                System.Printing.PrintCapabilities pc = qit.GetPrintCapabilities();
                System.Diagnostics.Debug.WriteLine(pc.InputBinCapability.ToString());
                System.Diagnostics.Debug.WriteLine(pc.MaxCopyCount);
                System.Diagnostics.Debug.WriteLine(pc.OrientedPageMediaHeight);
                System.Diagnostics.Debug.WriteLine(pc.OrientedPageMediaWidth);
                System.Diagnostics.Debug.WriteLine(pc.PageBorderlessCapability);
                System.Diagnostics.Debug.WriteLine(qit.QueueAttributes.ToString());
                System.Diagnostics.Debug.WriteLine(qit.ShareName);
                System.Diagnostics.Debug.WriteLine(qit.QueueDriver.PropertiesCollection.ToString());  
                System.IO.MemoryStream str = qit.GetPrintCapabilitiesAsXml();
                System.IO.StreamReader tr = new System.IO.StreamReader(str);
                String line;
                while ((line = tr.ReadLine()) != null)
                {
                    System.Diagnostics.Debug.WriteLine(line);
                }
                foreach (DictionaryEntry entry in qit.PropertiesCollection)
                {
                    PrintProperty prop;
                    prop = (PrintProperty)entry.Value;
                    if (prop.Value != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Name " + prop.Name + " value " + prop.Value);
                    }
                }
            }
            //System.Drawing.Printing.PaperKind.A4;
            //System.Printing.PrintDriver;
            //System.Printing.PrintJobSettings;
            //System.Printing.PrintDocumentImageableArea;
            //System.Printing.PrintDriver;
            //System.Printing.PrintJobStatus;
            //System.Printing.PrintQueue;
            //System.Printing.PrintQueueStringProperty;
            //System.Drawing.Printing.PrinterSettings;
            System.Drawing.Printing.PrinterSettings ps;
            ps = new System.Drawing.Printing.PrinterSettings();
            ps.PrinterName = cmbPrinters.Text;
            System.Drawing.Printing.PrinterSettings.PaperSizeCollection papers;
            papers = ps.PaperSizes;
            foreach (System.Drawing.Printing.PaperSize ps2 in papers)
            {
                System.Diagnostics.Debug.WriteLine(ps2.PaperName + " " + ps2.Kind.ToString() + " " + ps2.Height.ToString() + " " + ps2.Width.ToString());
            }
        }

        private void butBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = txtBaseFolder.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilename.Text = openFileDialog1.FileName;
                FileInfo fi = new FileInfo(txtFilename.Text);
                txtFilename.Text = fi.Name;
            }

        }

        private void butBrowsebase_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtBaseFolder.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBaseFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void SelectPrinter(object sender, EventArgs e)
        {
            LocalPrintServer localServer;
            localServer = new LocalPrintServer();
            EnumeratedPrintQueueTypes[] myEnum =
                {EnumeratedPrintQueueTypes.Connections
                ,EnumeratedPrintQueueTypes.Local };
            PrintQueueCollection col = localServer.GetPrintQueues(myEnum);

            foreach (System.Printing.PrintQueue qit in col)
            {
                cmbPrinters.Items.Add(qit.Name);
            }
            cmbPrinters.SelectedIndex = 0;
        }

        private void butPaperDef_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtPaperDef.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPaperDef.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void butLabelDef_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtLabelDef.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLabelDef.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void cmbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            //Vullen van de trays van deze printer...
            //
            string sPrinter;
            sPrinter = cmbPrinters.Text;

            System.Drawing.Printing.PrinterSettings ps;
            ps = new System.Drawing.Printing.PrinterSettings();
            ps.PrinterName = sPrinter;
            cmbTray.Items.Clear();
            foreach (System.Drawing.Printing.PaperSource src in ps.PaperSources)
            {
                cmbTray.Items.Add(src.SourceName);    
            }
            cmbTray.SelectedIndex = 0;
        }

        private void txtPaperDef_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(txtPaperDef.Text))
            {
                cmbPaper.Items.Clear();
                DirectoryInfo di;
                di = new DirectoryInfo(txtPaperDef.Text);
                foreach (FileInfo f in di.GetFiles())
                {
                    if (f.Name.EndsWith(".XML", StringComparison.OrdinalIgnoreCase))
                    {
                        string s = f.Name.Substring(0,f.Name.Length-4);
                        cmbPaper.Items.Add(s);
                    }
                }
                if (cmbPaper.Items.Count > 0)
                    cmbPaper.SelectedIndex = 0;
            }  
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
