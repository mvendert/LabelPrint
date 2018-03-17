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
using ACA.LabelX.Toolbox;
using System.Collections.Specialized;

namespace LabelControler
{
    public partial class PrinterTrayForm : FormBase
    {
        private PrintGroupItem pgi;

        public PrintGroupItem PrintGroup
        {
            get { return pgi; }
            set { pgi = value; }
        }
        private PrinterItem pi;

        public PrinterItem Printer
        {
            get { return pi; }
            set { pi = value; }
        }

        private PrinterTrayItem pti;

        public PrinterTrayItem PrinterTray
        {
            get { return pti; }
            set { pti = value; }
        }

        private ConnectionParameter conParm;

        public ConnectionParameter ConnectionParameter
        {
            get { return conParm; }
            set { conParm = value; }
        }

        public PrinterTrayForm()
        {
            InitializeComponent();
            pti = null;
        }

        private void PrinterTrayForm_Load(object sender, EventArgs e)
        {
            RemClientControlObjectProxy theObj;
            theObj = new RemClientControlObjectProxy();
            theObj.ConParameter = conParm;            
            
            if (PrinterTray != null)
            {
                cmbTray.Items.Add(pti.TrayName);
                cmbTray.SelectedIndex = 0;
                cmbTray.Enabled = false;
            }
            else
            {
                StringCollection Trays;
                
                try
                {
                    Trays = theObj.GetSupportedTraysOfPrinter(pi.LongName);
                }
                catch (RemClientControlObjectProxyException)
                {
                    Trays = new StringCollection();
                }
                foreach (string tn in Trays)
                {
                    //
                    //One should not be able to add a tray 2 times...
                    //
                    bool bSkip = false;
                    foreach (PrinterTrayItem t in Printer.Trays)
                    {
                        if (t.TrayName.Equals(tn, StringComparison.OrdinalIgnoreCase))
                        {
                            bSkip = true;
                            break;
                        }
                    }
                    if (bSkip == false)
                        cmbTray.Items.Add(tn);
                }
                if (cmbTray.Items.Count > 0)
                    cmbTray.SelectedIndex = 0;
            }

            StringCollection thePapers;
            thePapers = new StringCollection();
            try
            {
                thePapers = theObj.GetPaperTypes();
            }
            catch (RemClientControlObjectProxyException)
            {
                return;
            }
            finally
            {
                theObj.Dispose();
            }
            int waar = -1;
            foreach (string s in thePapers)
            {
                int h;
                h = cmbPapertype.Items.Add(s);
                if (PrinterTray != null)
                {
                    if (PrinterTray.CurrentPapertypeName == s)
                    {
                        waar = h;
                    }
                }
            }
            if (waar > -1)
            {
                cmbPapertype.SelectedIndex = waar;
            }
            else
            {
                if (cmbPapertype.Items.Count > 0)
                {
                    cmbPapertype.SelectedIndex = 0;
                }
            }
            SetFormTeksten();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            PrinterTrayItem tt;
            tt = new PrinterTrayItem();
            tt.TrayName = cmbTray.SelectedItem.ToString() ;
            tt.CurrentPapertypeName = cmbPapertype.SelectedItem.ToString() ;
            this.PrinterTray = tt;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void SetFormTeksten()
        {
            label1.Text = GetString("TRAY");
            label2.Text = GetString("PAPERTYPE");
            butCancel.Text = GetString("CANCEL");
            this.Text = GetString("MANAGEAPRINTERTRAY");
        }

    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
