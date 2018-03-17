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
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACA.LabelX.Toolbox;
using ACA.LabelX.PrintJob;
using ACA.LabelX.PrintEngine;
using System.Collections.Specialized;
using System.Xml.XPath;
using System.Net;
using System.Threading;

namespace LabelControler
{
    public partial class LabelPrinterPoolForm : FormBase
    {
        private PrintGroupItem pgi;
        private ConnectionParameter conParm;
        private int tikken;
        private ListViewColumnSorter lvwColumnSorter;
        private bool alreadyResized = false;

        public ConnectionParameter ConnectionParameter
        {
            get { return conParm; }
            set { conParm = value; }
        }

        public PrintGroupItem PrintGroup
        {
            get { return pgi; }
            set { pgi = value; }
        }

        public LabelPrinterPoolForm()
        {
            pgi = null;
            tikken = 10;
            InitializeComponent();
            changePaperTypeInTheSelectedPrinterToolStripMenuItem.Enabled = false;
            disablePrinterToolStripMenuItem1.Enabled = false;
            removeSelectedPrinterFromPoolToolStripMenuItem.Enabled = false;
            changePapertypeInSelectedPrinterToolStripMenuItem.Enabled = false;
            disablePrinterToolStripMenuItem.Enabled = false;
            removePrinterFromPoolToolStripMenuItem.Enabled = false;
            printjobsToolStripMenuItem.Enabled = false;
            suspendPrintjobToolStripMenuItem.Enabled = false;
            discardPrintjobToolStripMenuItem.Enabled = false;
            viewPrintjobToolStripMenuItem.Enabled = false;
            xMLToolStripMenuItem1.Enabled = false;
            graphicalToolStripMenuItem1.Enabled = false;
            suspendJobToolStripMenuItem.Enabled = false;
            discardJobToolStripMenuItem.Enabled = false;
            viewToolStripMenuItem.Enabled = false;
            xMLViewToolStripMenuItem.Enabled = false;
            graphToolStripMenuItem.Enabled = false;
        }

        private void LabelPrinterPoolForm_Load(object sender, EventArgs e)
        {
            if (pgi != null)
            {
                lblPoolName.Text = pgi.Name;
            }
            SetFormTeksten();
            Render();
            timerVervers.Enabled = true;
            lvwColumnSorter = new ListViewColumnSorter();
            this.listViewPrintJobs.ListViewItemSorter = lvwColumnSorter;


        }

        private void Render()
        {
            RemClientControlObjectProxy theObj;
            theObj = new RemClientControlObjectProxy();
            theObj.ConParameter = conParm;
            PrintGroupItem theItem;

            StartRemote();
            try
            {
                theItem = theObj.GetLabelPrintGroupByName(lblPoolName.Text);
            }
            catch (RemClientControlObjectProxyException)
            {
                bool Restart;
                Restart = false;
                if (timerVervers.Enabled)
                {
                    Restart = true;
                    timerVervers.Enabled = false;
                }
                MessageBox.Show(GetString("CONNECTIONLOST"));
                StopRemote();
                theObj.Dispose();
                if (Restart)
                {
                    timerVervers.Enabled = true;
                }
                return;
            }
            pgi = theItem;

            PrintJobInfos pjInfo;
            pjInfo = new PrintJobInfos();
            try
            {
                pjInfo = theObj.GetPrintjobsForPrintgroup(pgi);
            }
            catch (RemClientControlObjectProxyException)
            {
                MessageBox.Show(GetString("CONNECTIONLOST"));
                StopRemote();
                theObj.Dispose();
                return;
            }
            theObj.Dispose();
            StopRemote();
            RenderPrintGroupItem(theItem);
            RenderPrintJobInfo(pjInfo);
        }
        private void StartRemote()
        {
            Cursor = Cursors.WaitCursor;
            toolStripStatusLabel1.Text = GetString("QUERYINGPRINTSERVER");
            statusStrip1.Update();
        }
        private void StopRemote()
        {
            Cursor = Cursors.Default;
            toolStripStatusLabel1.Text = string.Empty;
            statusStrip1.Update();
        }

        private void RenderPrintJobInfo(PrintJobInfos info)
        {
            bool bSetDummy = false;
            listViewPrintJobs.Items.Clear();            
            listViewPrintJobs.LabelEdit = false;
            listViewPrintJobs.AllowColumnReorder = false;
            listViewPrintJobs.FullRowSelect = true;
            listViewPrintJobs.GridLines = false;
            listViewPrintJobs.Sorting = SortOrder.None;
            listViewPrintJobs.View = View.Details;
            if (info == null)
            {
                bSetDummy = true;
            }
            else
            {
                if (info.Count > 0)
                {

                    if (!listViewPrintJobs.Columns[0].Text.Equals(GetString("DESCRIPTION")))
                    {                        
                        listViewPrintJobs.Columns.Clear();
                        listViewPrintJobs.Columns.Add(GetString("DESCRIPTION"), 150, HorizontalAlignment.Left);
                        listViewPrintJobs.Columns.Add(GetString("LABELS"), 50, HorizontalAlignment.Left);
                        listViewPrintJobs.Columns.Add(GetString("RELEASED"), 40, HorizontalAlignment.Center);
                        listViewPrintJobs.Columns.Add(GetString("LASTPRINTED"), 120, HorizontalAlignment.Left);
                        listViewPrintJobs.Columns.Add(GetString("PRINTEDTO"), 150, HorizontalAlignment.Left);
                        listViewPrintJobs.Columns.Add(GetString("FROM"), 50, HorizontalAlignment.Left);
                        listViewPrintJobs.Columns.Add(GetString("BY"), 50, HorizontalAlignment.Left);
                        listViewPrintJobs.Columns.Add(GetString("LABELTYPE"), 80, HorizontalAlignment.Left);
                        listViewPrintJobs.Columns.Add(GetString("CREATED"), 120, HorizontalAlignment.Left);
                        listViewPrintJobs.Columns.Add(GetString("PRINTJOB"), 150, HorizontalAlignment.Left);
                    }
                    foreach (PrintJobInfo f in info)
                    {
                        ListViewItem it = new ListViewItem();
                        ListViewItem.ListViewSubItem si = new ListViewItem.ListViewSubItem();
                        if (f.AutoRelease)
                        {
                            si.Text = GetString("YES");
                        }
                        else
                        {
                            si.Text = GetString("NO");
                            si.ForeColor = Color.Red;
                        }

                        it.Text = f.Description;
                        it.SubItems.Add(f.NumberOfLabels.ToString());
                        it.SubItems.Add(si);
                        string lastprinted = f.LastPrinted.ToShortDateString() + " " + f.LastPrinted.ToShortTimeString();
                        if (lastprinted.Equals("1-1-0001 0:00"))
                            it.SubItems.Add("");
                        else
                            it.SubItems.Add(lastprinted);
                        it.SubItems.Add(f.PrintedTo);
                        it.SubItems.Add(f.From);
                        it.SubItems.Add(f.User);
                        it.SubItems.Add(f.LabelType);
                        it.SubItems.Add(f.CreationDateTime.ToShortDateString() + " " + f.CreationDateTime.ToShortTimeString());
                        it.SubItems.Add(f.ID);
                        it.Tag = f;
                        listViewPrintJobs.Items.Add(it);
                    }
                }
                else
                    bSetDummy = true;
            }

            if (bSetDummy)
            {
                listViewPrintJobs.Columns.Clear();
                listViewPrintJobs.Columns.Add(GetString("PRINTJOB"), 300);
                listViewPrintJobs.View = View.Details;
                listViewPrintJobs.Items.Add(GetString("THEREARECURRENTLYNOPRINTJOBSTOHANDLEMAYBETHEYAREALREADYASSIGNEDTOAPRINTER"));
            }
            if (!alreadyResized)
            {
                int controlwidth = 0;
                foreach (ColumnHeader col in listViewPrintJobs.Columns)
                {
                    col.Width = -2;
                    controlwidth += col.Width;
                }
                if ((controlwidth * 1.1) < SystemInformation.PrimaryMonitorMaximizedWindowSize.Width && (controlwidth * 1.1) > this.Size.Width)
                    this.Size = new Size((int)(controlwidth * 1.10), this.Size.Height);

                alreadyResized = true;
            }
        }

        private void RenderPrintGroupItem(PrintGroupItem it)
        {
            lblPoolName.Text = it.Name;
            lstViewPrinters.Items.Clear();            
            lstViewPrinters.LabelEdit = false;
            lstViewPrinters.AllowColumnReorder = false;
            lstViewPrinters.FullRowSelect = true;
            lstViewPrinters.GridLines = false;
            lstViewPrinters.Sorting = SortOrder.None;
            lstViewPrinters.View = View.Details;

            if (it.GroupPrinters != null)
            {
                if (it.GroupPrinters.Count > 0)
                {
                    if(!lstViewPrinters.Columns[0].Text.Equals(GetString("PRINTER")))
                    {                    
                        lstViewPrinters.Columns.Clear();
                        lstViewPrinters.Columns.Add(GetString("PRINTER"), 150, HorizontalAlignment.Left);
                        lstViewPrinters.Columns.Add(GetString("JOBS"), 100, HorizontalAlignment.Left);
                        lstViewPrinters.Columns.Add(GetString("ENABLED"), 80, HorizontalAlignment.Left);
                        lstViewPrinters.Columns.Add(GetString("STATUS"), 90, HorizontalAlignment.Left);
                        lstViewPrinters.Columns.Add(GetString("PAPERTYPES"), 200, HorizontalAlignment.Left);
                    }
                    foreach (PrinterItem pit in pgi.GroupPrinters)
                    {
                        ListViewItem theItem;
                        int count;
                        theItem = new ListViewItem();

                        theItem.ImageIndex = 0;
                        theItem.Text = pit.LongName;

                        count = pit.QueueLength;

                        theItem.SubItems.Add(count.ToString());
                        theItem.SubItems.Add(pit.Enabled ? GetString("ENABLED") : GetString("DISABLED"));
                        theItem.SubItems.Add(pit.NeedsUserIntervention ? GetString("ATTENTION") : "");

                        if (pit.Trays == null)
                        {
                            theItem.SubItems.Add(GetString("DEFAULT"));
                        }
                        else
                        {
                            StringBuilder sb;
                            sb = new StringBuilder();
                            foreach (PrinterTrayItem ptit in pit.Trays)
                            {
                                if (sb.Length > 0)
                                    sb.Append(", ");
                                sb.Append(ptit.TrayName);
                                sb.Append(": ");
                                sb.Append(ptit.CurrentPapertypeName);
                            }
                            theItem.SubItems.Add(sb.ToString());
                        }
                        theItem.Tag = pit;
                        lstViewPrinters.Items.Add(theItem);
                    }
                }
                else
                {
                    lstViewPrinters.Columns.Clear();
                    lstViewPrinters.Columns.Add(GetString("PRINTERS"), 300);
                    lstViewPrinters.View = View.Details;
                    lstViewPrinters.Items.Add(GetString("THEREARECURRENTLYNOPRINTERSINTHISPOOLUSERIGHTCLICKTOADDAPRINTERTOTHEPOOL"));
                }
            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            Render();
            tikken = 10;
        }

        private void lstViewPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem it;
            if (lstViewPrinters.SelectedIndices.Count > 0 && (!(lstViewPrinters.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTERSINTHISPOOLUSERIGHTCLICKTOADDAPRINTERTOTHEPOOL")))))
            {
                changePaperTypeInTheSelectedPrinterToolStripMenuItem.Enabled = true;
                disablePrinterToolStripMenuItem1.Enabled = true;
                removeSelectedPrinterFromPoolToolStripMenuItem.Enabled = true;
                changePapertypeInSelectedPrinterToolStripMenuItem.Enabled = true;
                disablePrinterToolStripMenuItem.Enabled = true;
                removePrinterFromPoolToolStripMenuItem.Enabled = true;

                it = lstViewPrinters.SelectedItems[0];
                if (it.Tag != null)
                {
                    PrinterItem pi = (PrinterItem)it.Tag;
                    if (pi.Enabled == true)
                    {
                        disablePrinterToolStripMenuItem.Text = GetString("DISABLEPRINTER");
                        disablePrinterToolStripMenuItem1.Text = GetString("DISABLEPRINTER");
                    }
                    else
                    {
                        disablePrinterToolStripMenuItem.Text = GetString("ENABLEPRINTER");
                        disablePrinterToolStripMenuItem1.Text = GetString("ENABLEPRINTER");
                    }
                }
            }
            else
            {
                changePaperTypeInTheSelectedPrinterToolStripMenuItem.Enabled = false;
                disablePrinterToolStripMenuItem1.Enabled = false;
                removeSelectedPrinterFromPoolToolStripMenuItem.Enabled = false;
                changePapertypeInSelectedPrinterToolStripMenuItem.Enabled = false;
                disablePrinterToolStripMenuItem.Enabled = false;
                removePrinterFromPoolToolStripMenuItem.Enabled = false;
            }
        }

        private void lstViewPrinters_DoubleClick(object sender, EventArgs e)
        {
            if (lstViewPrinters.SelectedItems.Count > 0)
            {
                changePapertypeInSelectedPrinterToolStripMenuItem_Click(sender, e);
            }
        }

        private void StoreUpdatedJobStatus(PrintJobInfo fi)
        {
            //the change in autorelease should go to the server
            RemClientControlObjectProxy theObj;
            theObj = new RemClientControlObjectProxy();
            theObj.ConParameter = conParm;
            try
            {
                StartRemote();
                theObj.UpdatePrintjobStatus(fi);
            }
            catch (RemClientControlObjectProxyException)
            {
                MessageBox.Show(GetString("CALLTOPRINTERSERVICEFAILEDCHECKYOURCONNECTION"));
            }
            finally
            {
                StopRemote();
                theObj.Dispose();
            }
        }

        private void listViewPrintJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPrintJobs.SelectedItems.Count > 0 && (!(listViewPrintJobs.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTJOBSTOHANDLEMAYBETHEYAREALREADYASSIGNEDTOAPRINTER")))))
            {
                printjobsToolStripMenuItem.Enabled = true;
                suspendPrintjobToolStripMenuItem.Enabled = true;
                discardPrintjobToolStripMenuItem.Enabled = true;
                viewPrintjobToolStripMenuItem.Enabled = true;
                xMLToolStripMenuItem1.Enabled = true;
                graphicalToolStripMenuItem1.Enabled = true;
                suspendJobToolStripMenuItem.Enabled = true;
                discardJobToolStripMenuItem.Enabled = true;
                viewToolStripMenuItem.Enabled = true;
                xMLViewToolStripMenuItem.Enabled = true;
                graphToolStripMenuItem.Enabled = true;
                if (listViewPrintJobs.SelectedItems[0].Tag != null)
                {
                    PrintJobInfo f;
                    f = (PrintJobInfo)listViewPrintJobs.SelectedItems[0].Tag;
                    if (f.AutoRelease)
                    {
                        suspendJobToolStripMenuItem.Text = GetString("SUSPENDPRINTJOB");
                        suspendPrintjobToolStripMenuItem.Text = GetString("SUSPENDPRINTJOB");
                    }
                    else
                    {
                        suspendJobToolStripMenuItem.Text = GetString("RELEASEPRINTJOB");
                        suspendPrintjobToolStripMenuItem.Text = GetString("RELEASEPRINTJOB");
                    }
                }
            }
            else
            {
                printjobsToolStripMenuItem.Enabled = false;
                suspendPrintjobToolStripMenuItem.Enabled = false;
                discardPrintjobToolStripMenuItem.Enabled = false;
                viewPrintjobToolStripMenuItem.Enabled = false;
                xMLToolStripMenuItem1.Enabled = false;
                graphicalToolStripMenuItem1.Enabled = false;
                suspendJobToolStripMenuItem.Enabled = false;
                discardJobToolStripMenuItem.Enabled = false;
                viewToolStripMenuItem.Enabled = false;
                xMLViewToolStripMenuItem.Enabled = false;
                graphToolStripMenuItem.Enabled = false;
            }
        }

        private void DiscardPrintJob(PrintJobInfo fi)
        {
            bool bRet;
            RemClientControlObjectProxy theObj;
            theObj = new RemClientControlObjectProxy();
            theObj.ConParameter = conParm;
            try
            {
                StartRemote();
                bRet = theObj.DiscardPrintJob(fi);
                if (!bRet)
                {
                    MessageBox.Show(GetString("DISCARDINGPRINTJOBFAILEDTRYAGAINLATER[INUSE]"));
                }
            }
            catch (RemClientControlObjectProxyException)
            {
                MessageBox.Show(GetString("CALLTOPRINTERSERVICEFAILEDCHECKYOURCONNECTION"));
            }
            finally
            {
                StopRemote();
                theObj.Dispose();
            }
        }

        private void ShowPreview(string[] lines)
        {
            FormXmlPreview theForm;
            theForm = new FormXmlPreview();
            theForm.LabelData = lines;
            theForm.ShowDialog();
            return;
        }

        private void timerVervers_Tick(object sender, EventArgs e)
        {
            tikken--;
            lblTikken.Text = tikken.ToString();
            if (tikken < 0)
            {
                butRefresh_Click(sender, e);
            }
        }

        private bool HandlePrintPreview(PrintGroupItem it, PrintJobInfo jobinfo, string PrintJobsRootFolder, string PaperDefinitionsRootFolder, string LabelDefinitionsRootFolder, int language)
        {

            bool ret = true;
            string sXMLFile;
            sXMLFile = jobinfo.FullFilename;

            //Retrieve some information for the selected printjob
            // The requested queueu, the printgroup (should be the current), and the LabelType
            string sPrintQueue;
            string sPrintGroup;
            string sLabelType;

            sPrintGroup = string.Empty;
            sPrintQueue = string.Empty;
            sLabelType = string.Empty;

            XPathDocument doc;
            doc = new XPathDocument(sXMLFile);

            XPathNavigator nav;
            nav = doc.CreateNavigator();

            XPathNodeIterator theNode;
            theNode = nav.Select("/printjob/destination/printqueue");

            if (theNode != null)
            {
                theNode.MoveNext();
                sPrintQueue = theNode.Current.Value;
            }
            theNode = nav.Select("/printjob/destination/printgroup");

            if (theNode != null)
            {
                theNode.MoveNext();
                sPrintGroup = theNode.Current.Value;
            }

            theNode = nav.Select("/printjob/destination/labeltype");

            if (theNode != null)
            {
                theNode.MoveNext();
                sLabelType = theNode.Current.Value;
            }

            //We now have the labeltype requested... we gather information on which paper this can be printed...
            List<string> papertypes;

            papertypes = new List<string>();
            papertypes = GetPaperTypesOfLabelType(sLabelType, LabelDefinitionsRootFolder);

            if (papertypes.Count == 0)
            {
                //can not print on no paper...
                //Error here
                return false;
            }
            else
            {
                if (papertypes.Count > 1)
                {
                    //only one default allowed for now.
                    return false;
                }
            }
            PrinterItemLocals pils;
            pils = new PrinterItemLocals();
            foreach (PrinterItem pit in it.GroupPrinters)
            {
                PrinterItemLocal pil = new PrinterItemLocal();
                pil.item = pit;
                pils.Add(pil);
                //
                //Gather windowsinformation for each printer in the windows printer queue
                // We gather the numberofjobs and if the printer is online.
                //
            }
            PrinterItemLocal bestFit = null; ;
            if (pils.Count > 0)
            {
                bestFit = pils[0];
                bestFit.Tray = new PrinterTrayItem();
                bestFit.Tray.TrayName = "Phantom Tray";
                bestFit.Tray.CurrentPapertypeName = papertypes[0];

            }

            //We have selected a printer to print to
            if (bestFit != null)
            {
                ACA.LabelX.PrintJob.PrintJob p = new ACA.LabelX.PrintJob.PrintJob(PaperDefinitionsRootFolder, LabelDefinitionsRootFolder);
                p.Parse(jobinfo.FullFilename);

                if (p.labels.Count == 0)
                {
                    MessageBox.Show("No Labels in printjob");
                    return false;
                }

                //
                //check if th language requested is pressent in the printjob
                //
                bool bFound = false;
                if (p.languages != null)
                {
                    if (p.languages.Count > 0)
                    {
                        foreach (PrintJob.PrintLanguage x in p.languages)
                        {
                            if (x.Id == language)
                            {
                                bFound = true;
                            }
                        }
                    }
                }

                //We have selected a language this job can be printed in...
                if (bFound)
                {
                    uint nStartLabel;
                    uint nEndLabel;
                    nStartLabel = 0;
                    nEndLabel = 99;
                    if (jobinfo.NumberOfLabels > 100)
                    {
                        //
                        //The number of labels to preview makes no sence. A user does not want to browse
                        //hundres of labels in a little preview window. To make sence, he has to limit the
                        //number of labels to be shown in the preview.
                        //
                        FormPrintPreviewLimit theForm;
                        theForm = new FormPrintPreviewLimit();
                        theForm.MaxRange = 100;
                        theForm.MaxValue = (uint)jobinfo.NumberOfLabels;
                        theForm.StartLabel = 1;
                        theForm.EndLabel = 100;
                        theForm.StartPosition = FormStartPosition.CenterParent;
                        theForm.ShowDialog();
                        nEndLabel = theForm.EndLabel;
                        if (nEndLabel > 0) nEndLabel--;// we work zero based. Users work 0ne based
                        nStartLabel = theForm.StartLabel;
                        if (nStartLabel > 0) nStartLabel--;   // we work zero based. Users work 0ne based

                    }
                    ACA.LabelX.PrintEngine.PrintEngine pi = new ACA.LabelX.PrintEngine.PrintEngine(Environment.MachineName);
                    pi.DesignMode = ACA.LabelX.GlobalDataStore.GetInstance().DesignMode;
                    pi.AddPrintJob(p);
                    
                    pi.PrintPreview(bestFit.item.LongName, papertypes[0], bestFit.Tray.TrayName, ACA.LabelX.GlobalDataStore.GetInstance().DesignMode, nStartLabel, nEndLabel);
                }
                else
                {
                    MessageBox.Show("Language of printjob not suitable for this location.");
                    ret = false;
                }

            }
            else
            {                
                MessageBox.Show("Unable to find a suitable printer in this printgroup.\nThis can also be so if the printgroup in the printjob xml file is different from the printgroup the file is in.");
                ret = false;
            }

            return ret;
        }

        private List<string> GetPaperTypesOfLabelType(string sLabelType, string LabelDefinitionsRootFolder)
        {
            List<string> theList;
            string sLabelXML;
            sLabelXML = LabelDefinitionsRootFolder + sLabelType + ".XML";
            XPathDocument theDoc;
            XPathNavigator nav;
            XPathNodeIterator nit;
            theList = new List<string>();
            try
            {

                theDoc = new XPathDocument(sLabelXML);
                nav = theDoc.CreateNavigator();
                nit = nav.Select("/labeldef/validpapertypes/paper");

                if (nit != null)
                {
                    while (nit.MoveNext())
                    {
                        string sHelp = nit.Current.GetAttribute("type", string.Empty);
                        if (sHelp != string.Empty)
                        {
                            //Only add default papertypes for now...
                            string sHelp2 = nit.Current.GetAttribute("default", string.Empty);

                            if (sHelp == string.Empty)
                                sHelp = "false";

                            if (sHelp2.Equals("true", StringComparison.OrdinalIgnoreCase))
                            {
                                theList.Add(sHelp);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Could not find the file: " + sLabelXML,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unknown exception occured: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return theList;
        }

        private void getPrintPreviewInfo()
        {
            RemClientControlObjectProxy theObj;
            theObj = new RemClientControlObjectProxy();
            theObj.ConParameter = conParm;
            PrintGroupItem theItem;

            StartRemote();
            try
            {
                theItem = theObj.GetLabelPrintGroupByName(lblPoolName.Text);
            }
            catch (RemClientControlObjectProxyException)
            {
                bool Restart;
                Restart = false;
                if (timerVervers.Enabled)
                {
                    Restart = true;
                    timerVervers.Enabled = false;
                }
                MessageBox.Show(GetString("CONNECTIONLOST"));
                StopRemote();
                theObj.Dispose();
                if (Restart)
                {
                    timerVervers.Enabled = true;
                }
                return;
            }
            pgi = theItem;

            PrintJobInfos pjInfo;
            pjInfo = new PrintJobInfos();
            try
            {
                pjInfo = theObj.GetPrintjobsForPrintgroup(pgi);
            }
            catch (RemClientControlObjectProxyException)
            {
                MessageBox.Show(GetString("CONNECTIONLOST"));
                StopRemote();
                theObj.Dispose();
                return;
            }
            theObj.Dispose();
            StopRemote();


            RenderPrintGroupItem(theItem);
            RenderPrintJobInfo(pjInfo);

        }

        private void listViewPrintJobs_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewPrintJobs.Sort();


        }

        private void SetFormTeksten()
        {
            this.Text = GetString("MANAGEAPRINTERPOOL");
            columnHeader1.Text = GetString("PRINTERNAME");
            columnHeader2.Text = GetString("PRINTJOB");

            label3.Text = GetString("PRINTJOBS");
            menuStrip1.Text = NC("menuStrip1");
            statusStrip1.Text = NC("statusStrip1");
            butClose.Text = GetString("CLOSE");
            butRefresh.Text = GetString("REFRESH");
            label1.Text = GetString("POOL");

            //Menu Printers
            printersToolStripMenuItem.Text = GetString("PRINTERS");
            changePaperTypeInTheSelectedPrinterToolStripMenuItem.Text = GetString("MANAGEPRINTER");
            disablePrinterToolStripMenuItem1.Text = GetString("DISABLEPRINTER");
            addPrintersToPoolToolStripMenuItem.Text = GetString("ADDPRINTERSTOPOOL");
            removeSelectedPrinterFromPoolToolStripMenuItem.Text = GetString("REMOVESELECTEDPRINTERFROMPOOL");

            //Menu Printjobs
            printjobsToolStripMenuItem.Text = GetString("PRINTJOBS");
            suspendPrintjobToolStripMenuItem.Text = GetString("SUSPENDSELECTEDPRINTJOB");
            discardPrintjobToolStripMenuItem.Text = GetString("DISCARDSELECTEDPRINTJOB");
            viewPrintjobToolStripMenuItem.Text = GetString("VIEWSELECTEDPRINTJOB");
            xMLToolStripMenuItem1.Text = GetString("XMLVIEW");
            graphicalToolStripMenuItem1.Text = GetString("GRAPHICALVIEW");

            //ContextMenuStrip(printers)
            changePapertypeInSelectedPrinterToolStripMenuItem.Text = GetString("MANAGEPRINTER");
            disablePrinterToolStripMenuItem.Text = GetString("DISABLEPRINTER");
            addPrinterToPoolToolStripMenuItem1.Text = GetString("ADDPRINTERSTOPOOL");
            removePrinterFromPoolToolStripMenuItem.Text = GetString("REMOVESELECTEDPRINTERFROMPOOL");

            //ContextMenuStrip(printjobs)
            suspendJobToolStripMenuItem.Text = GetString("SUSPENDSELECTEDPRINTJOB");
            discardJobToolStripMenuItem.Text = GetString("DISCARDSELECTEDPRINTJOB");
            viewToolStripMenuItem.Text = GetString("VIEWSELECTEDPRINTJOB");
            xMLViewToolStripMenuItem.Text = GetString("XMLVIEW");
            graphToolStripMenuItem.Text = GetString("GRAPHICALVIEW");
        }

        #region "Printers ToolStripMenu functionality"
        private void contextMenuStripPrinters_Opened(object sender, EventArgs e)
        {
            timerVervers.Enabled = false;
        }
        private void contextMenuStripPrinters_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            timerVervers.Enabled = true;
        }
        private void changePaperTypeInTheSelectedPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePapertypeInSelectedPrinterToolStripMenuItem_Click(sender, e);
        }
        private void disablePrinterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            disablePrinterToolStripMenuItem_Click(sender, e);
        }
        private void addPrintersToPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addPrinterToPoolToolStripMenuItem1_Click(sender, e);
        }
        private void removeSelectedPrinterFromPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb;
            StringBuilder sbError;
            bool bRetTotaal = true;
            bool bRet = false;
            sb = new StringBuilder();
            sbError = new StringBuilder();
            sb.Append(GetString("PRINTERSWILLBEREMOVEDFROMPRINTERPOOL"));
            sb.Append(lblPoolName.Text);
            sb.Append(": ");
            //Check if an item is selected in the list
            if (lstViewPrinters.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("SELECTAPRINTERINTHELISTOFPRINTERSFIRST"));
            }
            else
            {
                if (lstViewPrinters.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTERSINTHISPOOLUSERIGHTCLICKTOADDAPRINTERTOTHEPOOL")))
                {
                    return;
                }
                foreach (ListViewItem it in lstViewPrinters.SelectedItems)
                {
                    sb.Append(it.Text);
                    sb.Append(", ");
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append(GetString("PROCEED"));
                if (MessageBox.Show(sb.ToString(), GetString("WARNING"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    RemClientControlObjectProxy theObj;
                    theObj = new RemClientControlObjectProxy();
                    theObj.ConParameter = conParm;

                    try
                    {
                        StartRemote();
                        PrinterItem pi;
                        foreach (ListViewItem it in lstViewPrinters.SelectedItems)
                        {
                            pi = new PrinterItem();
                            pi.LongName = it.Text;
                            //pi.Tray = string.Empty;
                            //pi.Enabled = false;
                            //pi.CurrentPapertypeName = string.Empty;
                            bRet = theObj.RemovePrinterFromPrinterGroup(pgi, pi);
                            if (!bRet)
                            {
                                if (sbError.Length > 0)
                                    sbError.Append(", ");

                                sbError.Append(pi.LongName);

                                bRetTotaal = false;
                            }
                        }
                        theObj.Dispose();
                        StopRemote();

                        if (!bRetTotaal)
                        {
                            MessageBox.Show(string.Format(GetString("FOLLOWINGPRINTERSCOULDNOTBEREMOVEDFROMPRINTSERVICE0"), sbError.ToString()));
                        }
                    }
                    catch (RemClientControlObjectProxyException)
                    {
                        if (theObj != null)
                        {
                            theObj.Dispose();
                        }
                        StopRemote();
                        MessageBox.Show(GetString("CALLTOPRINTERSERVICEFAILEDCHECKYOURCONNECTION"));
                    }
                }
                Render();
            }
        }
        #endregion

        #region "Printjob ToolStripMenu functionality"
        private void contextMenuStripJobs_Opened(object sender, EventArgs e)
        {
            timerVervers.Enabled = false;
        }
        private void contextMenuStripJobs_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            timerVervers.Enabled = true;
        }
        private void suspendPrintjobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewPrintJobs.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("SELECTANEXISTINGPRINTJOB"));
                return;
            }
            if (listViewPrintJobs.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTJOBSTOHANDLEMAYBETHEYAREALREADYASSIGNEDTOAPRINTER")))
            {
                return;
            }
            

            PrintJobInfo fi;
            fi = (PrintJobInfo)listViewPrintJobs.SelectedItems[0].Tag;

            fi.AutoRelease = !fi.AutoRelease;

            StoreUpdatedJobStatus(fi);

            Render();
        }
        private void discardPrintjobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            discardJobToolStripMenuItem_Click(sender, e);
        }
        private void xMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            xMLViewToolStripMenuItem_Click(sender, e);
        }
        private void graphicalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            graphToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region "Printer ContextMenuStrip functionality"
        private void changePapertypeInSelectedPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstViewPrinters.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("SELECTAPRINTERINTHELISTOFPRINTERSFIRST"));
            }
            else if (lstViewPrinters.SelectedItems.Count > 1)
            {
                MessageBox.Show(GetString("SELECTONLYONEPRINTERINTHELISTOFPRINTERS"));
            }
            else
            {
                if (lstViewPrinters.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTERSINTHISPOOLUSERIGHTCLICKTOADDAPRINTERTOTHEPOOL")))
                {
                    return;
                }
                ListViewItem it;
                it = lstViewPrinters.SelectedItems[0];
                PrinterItem theItem;
                theItem = (PrinterItem)it.Tag;

                LabelPrinterForm theChangeForm;
                theChangeForm = new LabelPrinterForm();
                theChangeForm.ConnectionParameter = this.ConnectionParameter;
                theChangeForm.PrintGroup = this.PrintGroup;
                theChangeForm.Printer = theItem;

                if (theChangeForm.ShowDialog(this) == DialogResult.OK)
                {
                    //else
                    //Discard changes
                    RemClientControlObjectProxy theObj;
                    theObj = new RemClientControlObjectProxy();
                    theObj.ConParameter = conParm;

                    try
                    {
                        StartRemote();
                        theObj.UpdatePrinterForPrintgroup(theChangeForm.PrintGroup, theChangeForm.Printer);
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
                    Render();
                }
            }
        }
        private void disablePrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem it;

            if (lstViewPrinters.SelectedIndices.Count > 0)
            {
                if (lstViewPrinters.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTERSINTHISPOOLUSERIGHTCLICKTOADDAPRINTERTOTHEPOOL")))
                {
                    return;
                }
                it = lstViewPrinters.SelectedItems[0];
                PrinterItem pi = (PrinterItem)it.Tag;
                pi.Enabled = !pi.Enabled;

                //Store this in remote object...
                RemClientControlObjectProxy theObj;
                theObj = new RemClientControlObjectProxy();
                theObj.ConParameter = conParm;
                try
                {
                    StartRemote();
                    theObj.UpdatePrinterStatus(pgi, pi);
                }
                catch (RemClientControlObjectProxyException)
                {
                    MessageBox.Show(GetString("CALLTOPRINTERSERVICEFAILEDCHECKYOURCONNECTION"));
                }
                finally
                {
                    StopRemote();
                    theObj.Dispose();
                }
            }
            Render();
        }
        private void addPrinterToPoolToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Show a list of all available printers on the
            //remote printer server
            bool nono;
            bool bRet;
            RemClientControlObjectProxy theObj;
            theObj = new RemClientControlObjectProxy();
            theObj.ConParameter = conParm;
            StringCollection theList;
            try
            {
                StartRemote();
                theList = theObj.GetLocalPrinters();
                StopRemote();
            }
            catch (RemClientControlObjectProxyException)
            {
                //Ergens melden dat het niet gelukt is...
                MessageBox.Show(GetString("CALLTOPRINTERSERVICEFAILEDCHECKYOURCONNECTION"));
                theList = new StringCollection();
                StopRemote();
            }

            theObj.Dispose();

            StringCollection notConnected;
            notConnected = new StringCollection();
            foreach (string s in theList)
            {
                nono = false;
                foreach (PrinterItem pi in pgi.GroupPrinters)
                {
                    if (pi.LongName.Equals(s, StringComparison.OrdinalIgnoreCase))
                    {
                        nono = true;
                        break;
                    }
                }
                if (!nono)
                {
                    notConnected.Add(s);
                }
            }
            SelectPrinterForm theForm;
            theForm = new SelectPrinterForm();
            theForm.Printers = notConnected;
            theForm.ConnectionParameter = conParm;
            if (theForm.ShowDialog() == DialogResult.OK)
            {
                theObj = new RemClientControlObjectProxy();
                theObj.ConParameter = conParm;
                try
                {
                    StartRemote();
                    PrinterItem pi;
                    pi = new PrinterItem();
                    pi.LongName = theForm.SelectedPrinter;

                    PrinterTrayItem ptit;
                    ptit = new PrinterTrayItem();
                    if (theForm.SelectedTray != null) //Some printers have no trays, leave empty
                    {
                        ptit.TrayName = theForm.SelectedTray;
                    }
                    ptit.CurrentPapertypeName = theForm.SelectedPaperType;
                    pi.Trays.Add(ptit);

                    bRet = theObj.AddPrinterToPrintGroupItem(pgi, pi);
                    theObj.Dispose();
                    StopRemote();
                    if (!bRet)
                    {
                        MessageBox.Show(GetString("PRINTERCOULDNOTBEADDEDONPRINTSERVICE"));
                    }
                }
                catch (RemClientControlObjectProxyException)
                {
                    //Ergens melden dat het niet gelukt is...
                    MessageBox.Show(GetString("CALLTOPRINTERSERVICEFAILEDCHECKYOURCONNECTION"));
                    StopRemote();
                }
            }
            Render();
        }
        private void removePrinterFromPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeSelectedPrinterFromPoolToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region "Printjob ContextMenuStrip functionality"
        private void suspendJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            suspendPrintjobToolStripMenuItem_Click(sender, e);
        }
        private void discardJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewPrintJobs.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("SELECTANEXISTINGPRINTJOB"));
                return;
            }
            if (listViewPrintJobs.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTJOBSTOHANDLEMAYBETHEYAREALREADYASSIGNEDTOAPRINTER")))
            {
                return;
            }
            PrintJobInfo fi;
            fi = (PrintJobInfo)listViewPrintJobs.SelectedItems[0].Tag;

            DiscardPrintJob(fi);
            Render();
        }
        private void xMLViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] lines;
            lines = null;
            if (listViewPrintJobs.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("SELECTANEXISTINGPRINTJOB"));
                return;
            }
            if (listViewPrintJobs.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTJOBSTOHANDLEMAYBETHEYAREALREADYASSIGNEDTOAPRINTER")))
            {
                return;
            }
            PrintJobInfo fi;
            fi = (PrintJobInfo)listViewPrintJobs.SelectedItems[0].Tag;

            RemClientControlObjectProxy theObj;
            theObj = new RemClientControlObjectProxy();
            theObj.ConParameter = conParm;
            try
            {
                StartRemote();
                lines = theObj.GetPrintjobPreview(fi);
            }
            catch (RemClientControlObjectProxyException)
            {
                MessageBox.Show(GetString("CALLTOPRINTERSERVICEFAILEDCHECKYOURCONNECTION"));
            }
            finally
            {
                StopRemote();
                theObj.Dispose();
            }
            if (lines == null)
            {
                MessageBox.Show(GetString("PREVIEWPRINTJOBFAILEDTRYAGAINLATER[INUSE]"));
            }
            ShowPreview(lines);

            Render();
        }
        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewPrintJobs.SelectedItems.Count == 0)
            {
                MessageBox.Show(GetString("SELECTANEXISTINGPRINTJOB"));
                return;
            }
            if (listViewPrintJobs.SelectedItems[0].Text.Equals(GetString("THEREARECURRENTLYNOPRINTJOBSTOHANDLEMAYBETHEYAREALREADYASSIGNEDTOAPRINTER")))
            {
                return;
            }
            PrintJobInfo printJobInfo;
            printJobInfo = (PrintJobInfo)listViewPrintJobs.SelectedItems[0].Tag;
            if (printJobInfo != null)
            {
                bool isAlsoClient = false;
                string PrintJobsRootFolder;
                string LabelDefinitionsRootFolder;
                string PaperDefinitionsRootFolder;
                int currentLanguage = 1043;

                string localHostName = Dns.GetHostName();
                IPHostEntry localIpEntry = Dns.GetHostEntry(localHostName);
                IPAddress[] localAddresList = localIpEntry.AddressList;
                IPHostEntry remoteIpEntry = Dns.GetHostEntry(conParm.Computer);
                IPAddress[] remoteAddresList = remoteIpEntry.AddressList;
                for (int j = 0; j < remoteAddresList.Length; j++)
                {
                    for (int i = 0; i < localAddresList.Length; i++)
                    {
                        if (remoteAddresList[j].ToString().Equals(localAddresList[i].ToString(), StringComparison.OrdinalIgnoreCase) ||
                            remoteAddresList[j].ToString().Equals(IPAddress.Loopback.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            isAlsoClient = true;
                        }
                    }
                }


                if (isAlsoClient)
                {
                    string slang = System.Configuration.ConfigurationSettings.AppSettings["LanguageId"].ToString();                    
                    
                    try
                    {
                        currentLanguage = int.Parse(slang);
                    }
                    catch (Exception)
                    {
                        currentLanguage = 1043;
                    }


                    RemClientControlObjectProxy theObj;
                    theObj = new RemClientControlObjectProxy();
                    theObj.ConParameter = conParm;

                    StartRemote();
                    try
                    {
                        theObj.GetClientFolders(out PrintJobsRootFolder, out LabelDefinitionsRootFolder, out PaperDefinitionsRootFolder);
                    }
                    catch (RemClientControlObjectProxyException)
                    {
                        bool Restart;
                        Restart = false;
                        if (timerVervers.Enabled)
                        {
                            Restart = true;
                            timerVervers.Enabled = false;
                        }
                        MessageBox.Show(GetString("CONNECTIONLOST"));
                        StopRemote();
                        theObj.Dispose();
                        if (Restart)
                        {
                            timerVervers.Enabled = true;
                        }
                        return;
                    }
                    theObj.Dispose();
                    StopRemote();

                    RemClientControlObjectProxy theObj2;
                    theObj2 = new RemClientControlObjectProxy();
                    theObj2.ConParameter = conParm;
                    PrintGroupItem pgi;

                    StartRemote();
                    try
                    {
                        pgi = theObj2.GetLabelPrintGroupByName(printJobInfo.PrintGroup);
                    }
                    catch (RemClientControlObjectProxyException)
                    {
                        bool Restart;
                        Restart = false;
                        if (timerVervers.Enabled)
                        {
                            Restart = true;
                            timerVervers.Enabled = false;
                        }
                        MessageBox.Show(GetString("CONNECTIONLOST"));
                        StopRemote();
                        theObj2.Dispose();
                        if (Restart)
                        {
                            timerVervers.Enabled = true;
                        }
                        return;
                    }
                    theObj2.Dispose();
                    StopRemote();

                    HandlePrintPreview(pgi, printJobInfo, PrintJobsRootFolder, PaperDefinitionsRootFolder, LabelDefinitionsRootFolder, currentLanguage);

                }
                else
                {
                    MessageBox.Show("Remote Print Previews are not yet supported.");
                }
                Render();
            }
        }
        #endregion
    }

    public class ListViewColumnSorter : System.Collections.IComparer
    {
        private int ColumnToSort;
        private SortOrder OrderOfSort;
        private System.Collections.CaseInsensitiveComparer ObjectCompare;

        public ListViewColumnSorter()
        {
            ColumnToSort = 0;
            OrderOfSort = SortOrder.None;
            ObjectCompare = new System.Collections.CaseInsensitiveComparer();
        }

        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                return 0;
            }
        }

        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
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
