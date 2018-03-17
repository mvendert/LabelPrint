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
using System.Windows.Forms;
using ACA.LabelX.Toolbox;

namespace LabelControler
{
    public partial class PrintCommanderMainForm : FormBase
    {
        private PrintGroupItemList printGroups;
        private ConnectionParameter conParm;

        public PrintCommanderMainForm()
        {
            InitializeComponent();
            printGroups = new PrintGroupItemList();
            conParm = new ConnectionParameter();
            manageToolStripMenuItem1.Enabled = false;
            disablePrinterPoolToolStripMenuItem.Enabled = false;
            removePrinterPoolToolStripMenuItem1.Enabled = false;
            manageToolStripMenuItem.Enabled = false;
            removePrinterPoolToolStripMenuItem.Enabled = false;
            setAsDefaToolStripMenuItem.Enabled = false;
        }

        private void StartRemote()
        {
            Cursor = Cursors.WaitCursor;
            toolStripStatusLabel1.Text = GetString("QUERYINGPRINTSERVER");
            statusStrip1.Update();
            Application.DoEvents();            
        }

        private void StopRemote()
        {
            Cursor = Cursors.Default;
            toolStripStatusLabel1.Text = string.Empty;
            statusStrip1.Update();
        }

        private bool GetGroupData()
        {
            bool bRet = true;
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
                bRet = false;
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
            return bRet;
        }

        private void RenderError(string sError)
        {
            lstPrintGroups.Items.Clear();
            lstPrintGroups.Columns.Clear();

            lstPrintGroups.LabelEdit = false;
            lstPrintGroups.AllowColumnReorder = false;
            lstPrintGroups.FullRowSelect = true;
            lstPrintGroups.GridLines = false;
            lstPrintGroups.Sorting = SortOrder.None;

            lstPrintGroups.Columns.Add(GetString("PRINTERPOOL"), 300);
            lstPrintGroups.View = View.Details;
            lstPrintGroups.Items.Add(sError);
        }

        private void RenderPrintGroups()
        {
            //Aanpassen van het aantal kolommen en type
            //van tonen.
            lstPrintGroups.Items.Clear();
            lstPrintGroups.Columns.Clear();

            lstPrintGroups.LabelEdit = false;
            lstPrintGroups.AllowColumnReorder = false;
            lstPrintGroups.FullRowSelect = true;
            lstPrintGroups.GridLines = false;
            lstPrintGroups.Sorting = SortOrder.None;

            if (printGroups.Count > 0)
            {
                lstPrintGroups.Columns.Add(GetString("PRINTERPOOL"), 150, HorizontalAlignment.Left);
                lstPrintGroups.Columns.Add(GetString("NUMBEROFPRINTERS"), 100, HorizontalAlignment.Left);
                lstPrintGroups.Columns.Add(GetString("STATUS"), 80, HorizontalAlignment.Left);
                lstPrintGroups.Columns.Add(GetString("PAPERTYPE"), 150, HorizontalAlignment.Left);

                lstPrintGroups.View = View.Details;
                lstPrintGroups.LargeImageList = imageListPrintItems;
                //lstPrintGroups.SmallImageList = imageListPrintItems;

                System.Text.StringBuilder sb;
                foreach (PrintGroupItem pi in printGroups)
                {
                    ListViewItem theItem;
                    int count;
                    theItem = new ListViewItem();
                    sb = new System.Text.StringBuilder();

                    //zoeken aan de hand van omschrijving.
                    theItem.ImageIndex = 3;
                    theItem.Text = pi.Name;
                    theItem.Tag = pi;
                    count = 0;
                    if (pi.GroupPrinters != null)
                    {
                        count = pi.GroupPrinters.Count;
                        foreach (PrinterItem pp in pi.GroupPrinters)
                        {
                            foreach (PrinterTrayItem ptit in pp.Trays)
                            {
                                if (sb.Length > 0)
                                    sb.Append(", ");
                                if (ptit.CurrentPapertypeName == string.Empty)
                                {
                                    sb.Append(NC("default"));
                                }
                                else
                                {
                                    sb.Append(ptit.CurrentPapertypeName);
                                }
                            }
                        }
                    }
                    theItem.SubItems.Add(count.ToString());
                    theItem.SubItems.Add(pi.Enabled ? GetString("ENABLED") : GetString("DISABLED"));
                    theItem.SubItems.Add(sb.ToString());
                    lstPrintGroups.Items.Add(theItem);
                }

            }
            else
            {
                lstPrintGroups.Columns.Add(GetString("PRINTERPOOL"), 300);
                lstPrintGroups.View = View.Details;
                lstPrintGroups.Items.Add(GetString("NOPRINTERPOOLDEFINEDUSERIGHTCLICKTODEFINEAPRINTERPOOL"));
            }

            //SetViewToSelection();
        }

        private void PrintCommanderMainForm_Load(object sender, EventArgs e)
        {
            //System.Resources.ResourceManager myRM;
            //myRM = new System.Resources.ResourceManager(NC("LabelControler.strings"), System.Reflection.Assembly.GetExecutingAssembly());
            //this.rcManager.RegisterResource(myRM);
            StopRemote();
            timer1.Enabled = true;
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(GetString("NOPRINTERPOOLDEFINEDUSERIGHTCLICKTODEFINEAPRINTERPOOL"));
            this.lstPrintGroups.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            SetFormTeksten();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (GetGroupData())
            {
                RenderPrintGroups();

                if (lstPrintGroups.Items.Count == 1)
                {
                    manageToolStripMenuItem_Click(this, EventArgs.Empty);
                }
            }
            else
            {
                defineToolStripMenuItem_Click(sender, e);
            }
        }

        private void lstPrintGroups_DoubleClick(object sender, EventArgs e)
        {
            if (lstPrintGroups.SelectedItems.Count > 0)
            {
                manageToolStripMenuItem_Click(sender, e);
            }
        }

        private void lstPrintGroups_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lstPrintGroups.SelectedItems.Count > 0)
            {
                manageToolStripMenuItem1.Enabled = true;
                disablePrinterPoolToolStripMenuItem.Enabled = true;
                removePrinterPoolToolStripMenuItem1.Enabled = true;
                manageToolStripMenuItem.Enabled = true;
                removePrinterPoolToolStripMenuItem.Enabled = true;
                setAsDefaToolStripMenuItem.Enabled = true;

                PrintGroupItem pgi;
                pgi = (PrintGroupItem)lstPrintGroups.SelectedItems[0].Tag;
                if (pgi != null)
                {

                    if (pgi.Enabled)
                    {
                        setAsDefaToolStripMenuItem.Text = GetString("DISABLEPRINTGROUP");
                        disablePrinterPoolToolStripMenuItem.Text = GetString("DISABLEPRINTGROUP");
                    }
                    else
                    {
                        setAsDefaToolStripMenuItem.Text = GetString("ENABLEPRINTGROUP");
                        disablePrinterPoolToolStripMenuItem.Text = GetString("ENABLEPRINTGROUP");
                    }
                }
            }
            else
            {
                manageToolStripMenuItem1.Enabled = false;
                disablePrinterPoolToolStripMenuItem.Enabled = false;
                removePrinterPoolToolStripMenuItem1.Enabled = false;
                manageToolStripMenuItem.Enabled = false;
                removePrinterPoolToolStripMenuItem.Enabled = false;
                setAsDefaToolStripMenuItem.Enabled = false;
            }


        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            refreshToolStripMenuItem_Click(sender, e);
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void butQuickChange_Click(object sender, EventArgs e)
        {
            LabelQuitPaperChangeForm theForm;
            theForm = new LabelQuitPaperChangeForm();
            theForm.ConnectionParameters = conParm;
            theForm.ShowDialog();
        }

        private void SetFormTeksten()
        {
            this.Text = GetString("LABELPRINTCOMMANDER");
            menuStrip1.Text = NC("menuStrip1");
            columnHeader1.Text = GetString("PRINTERPOOL");
            //File
            printGroupsToolStripMenuItem.Text = GetString("FILE");
            defineToolStripMenuItem.Text = GetString("CONNECTTONETWORKPRINTSERVER");
            refreshToolStripMenuItem2.Text = GetString("REFRESH");
            exitToolStripMenuItem.Text = GetString("EXIT");

            //Options
            optionsToolStripMenuItem.Text = GetString("OPTIONS");
            manageToolStripMenuItem1.Text = GetString("MANAGE");
            addNewPrinterPoolToolStripMenuItem1.Text = GetString("ADDNEWPRINTERPOOL");
            removePrinterPoolToolStripMenuItem1.Text = GetString("REMOVEPRINTERPOOL");

            //About
            hellpToolStripMenuItem.Text = GetString("HELP");
            aboutToolStripMenuItem.Text = GetString("ABOUT");

            //ContextMenuStrip
            refreshToolStripMenuItem.Text = GetString("REFRESH");
            manageToolStripMenuItem.Text = GetString("MANAGE");
            addNewPrinterPoolToolStripMenuItem.Text = GetString("ADDNEWPRINTERPOOL");
            removePrinterPoolToolStripMenuItem.Text = GetString("REMOVEPRINTERPOOL");

            statusStrip1.Text = NC("statusStrip1");
            butClose.Text = GetString("CLOSE");
            butRefresh.Text = GetString("REFRESH");
            butQuickChange.Text = GetString("QUICKPAPERCHANGE");
        }

        #region "File MenuStrip functionality"
        private void defineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1) //Are there command line arguments?
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Equals(@"/Remote", StringComparison.OrdinalIgnoreCase))
                        conParm.Computer = args[i + 1];
                    else if (args[i].Equals(@"/Port", StringComparison.OrdinalIgnoreCase))
                        conParm.PortNumber = System.Convert.ToInt32(args[i + 1]);
                    else if (args[i].Equals(@"/Protocol", StringComparison.OrdinalIgnoreCase))
                        conParm.Protocol = args[i + 1];
                }
            }
            else //No arguments, show connection dialog
            {
                FormConnectionParameters myForm;
                myForm = new FormConnectionParameters();
                myForm.Computer = conParm.Computer;
                myForm.PortNumber = conParm.PortNumber;
                myForm.Protocol = conParm.Protocol;
                if (myForm.ShowDialog() == DialogResult.OK)
                {
                    conParm.Computer = myForm.Computer;
                    conParm.Protocol = myForm.Protocol;
                    conParm.PortNumber = myForm.PortNumber;
                }

            }
            if (GetGroupData())
                RenderPrintGroups();

            if (lstPrintGroups.Items.Count == 1)
            {
                manageToolStripMenuItem_Click(this, EventArgs.Empty);
            }
        }
        private void refreshToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            refreshToolStripMenuItem_Click(sender, e);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region "PrintGroup MenuStrip functionality"
        private void manageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            manageToolStripMenuItem_Click(sender, e);
        }
        private void addNewPrinterPoolToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addNewPrinterPoolToolStripMenuItem_Click(sender, e);
        }
        private void removePrinterPoolToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            removePrinterPoolToolStripMenuItem_Click(sender, e);
        }
        private void disablePrinterPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setAsDefaToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region "Help MenuStrip functionality"
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 mybox;
            mybox = new AboutBox1();
            mybox.ShowDialog();
        }
        #endregion

        #region "ContextMenuStrip functionality"
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetGroupData())
                RenderPrintGroups();
        }
        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstPrintGroups.Items.Count == 1)
            {
                lstPrintGroups.Items[0].Selected = true;
            }

            if (lstPrintGroups.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("NOSELECTION"));
                return;
            }

            PrintGroupItem pgi;
            pgi = (PrintGroupItem)lstPrintGroups.SelectedItems[0].Tag;
            if (pgi != null)
            {
                LabelPrinterPoolForm theForm;
                theForm = new LabelPrinterPoolForm();
                theForm.PrintGroup = pgi;
                theForm.ConnectionParameter = conParm;
                theForm.ShowDialog();
                theForm.Dispose();
            }
            if (GetGroupData())
                RenderPrintGroups();
        }
        private void addNewPrinterPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bRet = false;
            LabelPrinterPoolNewForm myForm;
            myForm = new LabelPrinterPoolNewForm();
            if (myForm.ShowDialog() == DialogResult.OK)
            {
                //Fire remote call
                RemClientControlObjectProxy remoteObj;
                remoteObj = new RemClientControlObjectProxy();
                remoteObj.ConParameter = conParm;
                try
                {
                    StartRemote();
                    bRet = remoteObj.AddPrinterpool(myForm.PoolName);
                    remoteObj.Dispose();
                    StopRemote();
                }
                catch (RemClientControlObjectProxyException ex)
                {
                    RenderError(ex.Message);
                }
                finally
                {
                    StopRemote();
                    if (remoteObj != null)
                        remoteObj.Dispose();
                }
                if (GetGroupData())
                    RenderPrintGroups();
            }
        }
        private void removePrinterPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bRet = false;
            RemClientControlObjectProxy remoteObj;

            if (lstPrintGroups.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("NOSELECTION"));
                return;
            }
            if (lstPrintGroups.SelectedItems.Count > 1)
            {
                MessageBox.Show(GetString("ONLYSELECTONEPRINTERPOOL"));
                return;
            }

            if (MessageBox.Show(GetString("PRINTGROUPWILLBEREMOVED"), GetString("WARNING"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                PrintGroupItem pgi;
                pgi = (PrintGroupItem)lstPrintGroups.SelectedItems[0].Tag;
                pgi.Enabled = !pgi.Enabled;
                if (pgi != null)
                {
                    remoteObj = new RemClientControlObjectProxy();
                    remoteObj.ConParameter = conParm;
                    try
                    {
                        StartRemote();
                        bRet = remoteObj.RemovePrinterpool(pgi.Name);
                        remoteObj.Dispose();
                        StopRemote();
                    }
                    catch (RemClientControlObjectProxyException ex)
                    {
                        RenderError(ex.Message);
                    }
                    finally
                    {
                        StopRemote();
                        if (remoteObj != null)
                            remoteObj.Dispose();
                    }
                    if (GetGroupData())
                        RenderPrintGroups();
                }
            }
        }
        private void setAsDefaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstPrintGroups.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("NOSELECTION"));
                return;
            }
            if (lstPrintGroups.SelectedItems.Count > 1)
            {
                MessageBox.Show(GetString("ONLYSELECTONEPRINTERPOOL"));
                return;
            }
            PrintGroupItem pgi;
            pgi = (PrintGroupItem)lstPrintGroups.SelectedItems[0].Tag;
            pgi.Enabled = !pgi.Enabled;
            if (pgi != null)
            {
                RemClientControlObjectProxy remObj;
                remObj = new RemClientControlObjectProxy();
                remObj.ConParameter = conParm;
                try
                {
                    StartRemote();
                    remObj.UpdatePrintgroupStatus(pgi);
                    StopRemote();
                }
                catch (RemClientControlObjectProxyException ex)
                {
                    RenderError(ex.Message);
                }
                finally
                {
                    StopRemote();
                    remObj.Dispose();
                }
            }
            RenderPrintGroups();
        }
        #endregion
    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
