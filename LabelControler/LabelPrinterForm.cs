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
    public partial class LabelPrinterForm : FormBase
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

        private ConnectionParameter conParm;

        public ConnectionParameter ConnectionParameter
        {
            get { return conParm; }
            set { conParm = value; }
        }

        public LabelPrinterForm()
        {
            InitializeComponent();
        }

        private void LabelPrinterForm_Load(object sender, EventArgs e)
        {
            changeSelectedToolStripMenuItem1.Enabled = false;
            removeSelectedTrayToolStripMenuItem1.Enabled = false;
            changeSelectedToolStripMenuItem.Enabled = false;
            removeSelectedTrayToolStripMenuItem.Enabled = false;
            SetFormTeksten();
            RenderPrinter();
        }

        private void RenderPrinter()
        {
            lblPrinterName.Text = Printer.LongName;
            lblPrinterPoolName.Text = PrintGroup.Name;
            lstViewTrays.Items.Clear();
            lstViewTrays.Columns.Clear();
            lstViewTrays.LabelEdit = false;
            lstViewTrays.AllowColumnReorder = false;
            lstViewTrays.FullRowSelect = true;
            lstViewTrays.GridLines = false;
            lstViewTrays.Sorting = SortOrder.None;
            lstViewTrays.View = View.Details;

            if (pi != null)
            {
                if (pi.Trays != null)
                {
                    lstViewTrays.Columns.Add(GetString("TRAY"), 150, HorizontalAlignment.Left);
                    lstViewTrays.Columns.Add(GetString("PAPERTYPE"), 100, HorizontalAlignment.Left);

                    foreach (PrinterTrayItem ti in pi.Trays)
                    {
                        ListViewItem ni;
                        ni = new ListViewItem();
                        ni.Text = ti.TrayName;
                        ni.SubItems.Add(ti.CurrentPapertypeName);
                        ni.Tag = ti;
                        lstViewTrays.Items.Add(ni);
                    }
                }
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lstViewTrays_DoubleClick(object sender, EventArgs e)
        {
            if (lstViewTrays.SelectedItems.Count > 0)
            {
                changeSelectedToolStripMenuItem_Click(sender, e);
            }
        }

        private void SetFormTeksten()
        {
            this.Text = GetString("MANAGETRAYSANDPAPERTYPES");
            label1.Text = GetString("POOL");
            label2.Text = GetString("PRINTER");
            changeSelectedToolStripMenuItem.Text = GetString("CHANGESELECTED");
            changeSelectedToolStripMenuItem1.Text = GetString("CHANGESELECTED");
            addTrayToolStripMenuItem.Text = GetString("ADDTRAY");
            addTrayToolStripMenuItem1.Text = GetString("ADDTRAY");
            removeSelectedTrayToolStripMenuItem.Text = GetString("REMOVESELECTEDTRAY");
            removeSelectedTrayToolStripMenuItem1.Text = GetString("REMOVESELECTEDTRAY");
            optiesToolStripMenuItem.Text = GetString("OPTIONS");
            butCancel.Text = GetString("CANCEL");
        }

        #region "Context MenuStrip functionality"
        private void changeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstViewTrays.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("SELECTATRAYINTHELISTOFTRAYSFIRST"));
                return;
            }
            if (lstViewTrays.SelectedItems.Count > 1)
            {
                MessageBox.Show(GetString("SELECTONLYONETRAY"));
                return;
            }
            PrinterTrayItem t;
            t = (PrinterTrayItem)lstViewTrays.SelectedItems[0].Tag;
            PrinterTrayForm theForm;
            theForm = new PrinterTrayForm();
            theForm.PrintGroup = this.PrintGroup;
            theForm.Printer = this.Printer;
            theForm.PrinterTray = t;
            theForm.ConnectionParameter = this.ConnectionParameter;

            if (theForm.ShowDialog() == DialogResult.OK)
            {
                t.CurrentPapertypeName = theForm.PrinterTray.CurrentPapertypeName;
            }
            RenderPrinter();
        }
        private void addTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrinterTrayForm theForm;
            theForm = new PrinterTrayForm();
            theForm.PrintGroup = this.PrintGroup;
            theForm.Printer = this.Printer;
            theForm.ConnectionParameter = this.ConnectionParameter;
            if (theForm.ShowDialog() == DialogResult.OK)
            {
                //Add the selected Tray and printer to the printeritem
                Printer.Trays.Add(theForm.PrinterTray);
                RenderPrinter();
            }
        }
        private void removeSelectedTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstViewTrays.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("SELECTATRAYINTHELISTOFTRAYSFIRST"));
                return;
            }

            foreach (ListViewItem it in lstViewTrays.SelectedItems)
            {
                PrinterTrayItem ti;
                ti = (PrinterTrayItem)it.Tag;
                Printer.Trays.Remove(ti);
                RenderPrinter();
            }
        }
        #endregion

        #region "Options MenuStrip functionality"
        private void changeSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            changeSelectedToolStripMenuItem_Click(sender, e);
        }
        private void addTrayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addTrayToolStripMenuItem_Click(sender, e);
        }
        private void removeSelectedTrayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            removeSelectedTrayToolStripMenuItem_Click(sender, e);
        }
        #endregion

        private void lstViewTrays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstViewTrays.SelectedItems.Count > 0)
            {
                changeSelectedToolStripMenuItem1.Enabled = true;
                removeSelectedTrayToolStripMenuItem1.Enabled = true;
                changeSelectedToolStripMenuItem.Enabled = true;
                removeSelectedTrayToolStripMenuItem.Enabled = true;
            }
            else
            {
                changeSelectedToolStripMenuItem1.Enabled = false;
                removeSelectedTrayToolStripMenuItem1.Enabled = false;
                changeSelectedToolStripMenuItem.Enabled = false;
                removeSelectedTrayToolStripMenuItem.Enabled = false;
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
