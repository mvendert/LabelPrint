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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using ACA.LabelX.Toolbox;

namespace LabelControler
{
    public partial class SelectPrinterForm : FormBase
    {
        StringCollection thePrinters;
        StringCollection thePaperTypes;
        private ConnectionParameter conParm;

        public ConnectionParameter ConnectionParameter
        {
            get { return conParm; }
            set { conParm = value; }
        }
        public StringCollection Printers
        {
            get { return thePrinters; }
            set { thePrinters = value; }
        }
        private string selectedPrinter;
        public string SelectedPrinter
        {
            get { return selectedPrinter; }
            set { selectedPrinter = value; }
        }
        private string selectedTray;
        public string SelectedTray
        {
            get { return selectedTray; }
            set { selectedTray = value; }
        }
        private string selectedPaperType;
        public string SelectedPaperType
        {
            get { return selectedPaperType; }
            set { selectedPaperType = value; }
        }

        public SelectPrinterForm()
        {
            InitializeComponent();
            selectedPrinter = string.Empty;
        }
        private void SelectPrinterForm_Load(object sender, EventArgs e)
        {
            if (Printers.Count == 0)
            {
                AcceptButton = butCancel;
                butOK.Enabled = false;
            }
            else
            {
                foreach (string s in Printers)
                {
                    cmbPrinters.Items.Add(s);
                }
                cmbPrinters.SelectedIndex = 0;
            }
            RemClientControlObjectProxy theObj;

            theObj = new RemClientControlObjectProxy();
            theObj.ConParameter = conParm;
            thePaperTypes = new StringCollection();
            try
            {
                thePaperTypes = theObj.GetPaperTypes();
            }
            catch (RemClientControlObjectProxyException)
            {
                MessageBox.Show(GetString("UNABLETOAVAILABLEPAPERTYPESFROMPRINTERSERVICE"));
            }
            finally
            {
                theObj.Dispose();
            }
            foreach (string s in thePaperTypes)
            {
                cmbPapertype.Items.Add(s);
            }
            if (thePaperTypes.Count > 0)
            {
                cmbPapertype.SelectedIndex = 0;
            }
            SetFormTeksten();
        }
        private void butOK_Click(object sender, EventArgs e)
        {
            if (cmbPrinters.SelectedItem != null)
            {
                selectedPrinter = cmbPrinters.SelectedItem.ToString();
            }
            else
            {
                selectedPrinter = string.Empty;
            }
            if (cmbPrinterTray.SelectedItem != null)
            {
                selectedTray = cmbPrinterTray.SelectedItem.ToString();
            }
            else
            {
                selectedTray = null;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void cmbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringCollection col;

            col = new StringCollection();
            cmbPrinterTray.Items.Clear();

            if (cmbPrinters.SelectedItem != null)
            {   
                string sPrinter = cmbPrinters.SelectedItem.ToString();
                RemClientControlObjectProxy theObj;

                theObj = new RemClientControlObjectProxy();
                theObj.ConParameter = conParm;
                try
                {
                    col = theObj.GetSupportedTraysOfPrinter(sPrinter);
                }
                catch (RemClientControlObjectProxyException)
                {
                    MessageBox.Show(GetString("UNABLETORETRIEVETRAYSFORTHISPRINTER"));
                }
                finally
                {
                    theObj.Dispose();
                }
                if (col.Count > 0)
                {
                    foreach (string s in col)
                    {
                        cmbPrinterTray.Items.Add(s);
                    }
                    cmbPrinterTray.SelectedIndex = 0;
                }
            }   
        }

        private void cmbPapertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPapertype.SelectedItem != null)
            {
                selectedPaperType = cmbPapertype.SelectedItem.ToString();
            }
        }
        private void SetFormTeksten()
        {
            label1.Text = GetString("PRINTER");
            butCancel.Text = GetString("CANCEL");
            label2.Text = GetString("TRAY");
            label3.Text = GetString("PAPER");
            this.Text = GetString("SELECTAPRINTER");
        }

    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
