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

namespace LabelControler
{
    public partial class LabelQuitPaperChangeForm : FormBase
    {
        private ConnectionParameter conParm;
        private PrintGroupItemList printGroups;

        public ConnectionParameter ConnectionParameters
        {
            get { return conParm; }
            set { conParm = value; }
        }

        public LabelQuitPaperChangeForm()
        {
            InitializeComponent();
            printGroups = new PrintGroupItemList();
        }

        private void LabelQuitPaperChangeForm_Load(object sender, EventArgs e)
        {
            LoadData();
            SetFormTeksten();
            RenderData();
        }
        private void StartRemote()
        {
            Cursor = Cursors.WaitCursor;
            //toolStripStatusLabel1.Text = "Querying Printserver";
            //statusStrip1.Update();
            Application.DoEvents();
        }
        private void StopRemote()
        {
            Cursor = Cursors.Default;
            //toolStripStatusLabel1.Text = string.Empty;
            //statusStrip1.Update();
        }

        private void LoadData()
        {
            //Laden van alle printgroepen...
            RemClientControlObjectProxy remoteObj;
            remoteObj = null;
            try
            {
                remoteObj = new RemClientControlObjectProxy();
                remoteObj.ConParameter = conParm;
                StartRemote();
                printGroups = remoteObj.GetRemoteLabelPrintGroupsEx();
                remoteObj.Dispose();
            }
            catch (RemClientControlObjectProxyException ex)
            {
                RenderError(ex.Message);
            }
            finally
            {
                StopRemote();
                if (remoteObj != null)
                {
                    remoteObj.Dispose();
                }
            }
        }

        private void RenderError(string p)
        {
            throw new NotImplementedException();
        }

        private void RenderData()
        {
            lstPrinters.Items.Clear();
            lstPrinters.Columns.Clear();

            lstPrinters.LabelEdit = false;
            lstPrinters.AllowColumnReorder = false;
            lstPrinters.FullRowSelect = true;
            lstPrinters.GridLines = false;
            lstPrinters.Sorting = SortOrder.None;

            if (printGroups.Count > 0)
            {
                lstPrinters.Columns.Add(GetString("PRINTERPOOL"), 150, HorizontalAlignment.Left);
                lstPrinters.Columns.Add(GetString("PRINTER"), 150, HorizontalAlignment.Left);
                lstPrinters.Columns.Add(GetString("TRAY"), 100, HorizontalAlignment.Left);
                lstPrinters.Columns.Add(GetString("PAPERTYPE"), 80, HorizontalAlignment.Left);
                lstPrinters.View = View.Details;

                PrinterFullData theData;
                
                foreach (PrintGroupItem pi in printGroups)
                {
                    foreach (PrinterItem pit in pi.GroupPrinters)
                    {
                        foreach (PrinterTrayItem ptit in pit.Trays)
                        {
                            theData = new PrinterFullData();
                            theData.PrintGroup = pi;
                            theData.Printer = pit;
                            theData.Tray = ptit;
                            theData.PaperType = ptit.CurrentPapertypeName;

                            ListViewItem theItem;
                            theItem = new ListViewItem();
                            theItem.Text = pi.Name;
                            theItem.SubItems.Add(pit.LongName);
                            theItem.SubItems.Add(ptit.TrayName);
                            theItem.SubItems.Add(ptit.CurrentPapertypeName);
                            theItem.Tag = theData;
                            lstPrinters.Items.Add(theItem);
                        }
                    }
                }
            }
            else
            {
                lstPrinters.Columns.Add(GetString("PRINTERPOOL"), 300);
                lstPrinters.View = View.Details;
                lstPrinters.Items.Add(GetString("NOPRINTERPOOLDEFINEDUSESTANDARDMANAGEMENTTODEFINE"));
            }
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            RenderData();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lstPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lstPrinters_DoubleClick(object sender, EventArgs e)
        {
        }

        private void lstPrinters_Click(object sender, EventArgs e)
        {
            if (lstPrinters.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("DOUBLECLICKONAPRINTER"));
                return;
            }
            PrinterFullData fi;
            fi = (PrinterFullData)lstPrinters.SelectedItems[0].Tag;
            PrinterTrayForm ptForm;
            ptForm = new PrinterTrayForm();
            ptForm.ConnectionParameter = this.ConnectionParameters;
            ptForm.PrintGroup = fi.PrintGroup;
            ptForm.Printer = fi.Printer;
            ptForm.PrinterTray = fi.Tray;
            if (ptForm.ShowDialog() == DialogResult.OK)
            {
                fi.Tray.CurrentPapertypeName = ptForm.PrinterTray.CurrentPapertypeName;
                RemClientControlObjectProxy theObj;
                theObj = new RemClientControlObjectProxy();
                theObj.ConParameter = conParm;

                try
                {
                    StartRemote();
                    theObj.UpdatePrinterForPrintgroup(fi.PrintGroup, fi.Printer);
                }
                catch (RemClientControlObjectProxyException)
                {
                    MessageBox.Show(GetString("CHANGESCOULDNOTBESTOREDBYPRINTERSERVICE"));
                }
                finally
                {
                    StopRemote();
                    theObj.Dispose();
                }
                LoadData();
                RenderData();
            }
        }
        private void SetFormTeksten()
        {
            label1.Text = GetString("CLICKONAPRINTERTOCHANGEITSPAPERTYPE");
            butClose.Text = GetString("CLOSE");
            butRefresh.Text = GetString("REFRESH");
            this.Text = GetString("QUICKPAPERCHANGE");
        }


    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
