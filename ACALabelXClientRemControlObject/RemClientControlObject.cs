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
using System.Text;
using System.Xml;
using System.IO;
using ACA.LabelX.Toolbox;
using System.Xml.XPath;
using System.Collections.Specialized;
using System.Printing;
using System.Security;

namespace ACA.LabelX.Client
{
    public class LabelXRemClientControlOjectException : ApplicationException
    {
        public LabelXRemClientControlOjectException()
        {
        }
        public LabelXRemClientControlOjectException(string message)
            : base(message)
        {
        }
        public LabelXRemClientControlOjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }


    public class RemClientControlObject : System.MarshalByRefObject
    {
        public RemClientControlObject()
        {
        }
        ~RemClientControlObject()
        {
        }
        public void InitServer()
        {
        }

        private void Upload(byte[] CompressedData, int UncompressedDataLength, string FilePath)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.Upload");
            byte[] Data = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);

            try
            {
                FileStream outfile;
                outfile = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                outfile.Write(Data, 0, Data.Length);
                outfile.Close();
            }
            catch (Exception e)
            {
                throw new LabelXRemClientControlOjectException(string.Format("Cannot write file: {0}", FilePath), e);
            }
        }
        private void Download(string FilePath, ref byte[] CompressedData, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.Download");
            CompressedData = null;

            if (!File.Exists(FilePath))
                throw new LabelXRemClientControlOjectException(string.Format("Cannot find file: {0}", FilePath));

            FileStream infile;
            // Open the file as a FileStream object.
            infile = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[infile.Length];
            // Read the file to ensure it is readable.
            int count = infile.Read(buffer, 0, buffer.Length);
            if (count != buffer.Length)
            {
                infile.Close();
                throw new LabelXRemClientControlOjectException(string.Format("Unable to read data from file: {0}", FilePath));
            }
            infile.Close();

            UncompressedDataLength = buffer.Length;
            CompressedData = PSLib.Compression.Compress(buffer);
        }
        public void Ping(string name)
        {
            GlobalDataStore.Logger.Debug(name + ".Ping");
        }
        private byte[] ConvertPrintergroupsToXML(List<ACA.LabelX.Toolbox.PrintGroupItem> items)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.ConvertPrintergroupsToXML");

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("labelxprintergroups");
            foreach (ACA.LabelX.Toolbox.PrintGroupItem g in items)
            {
                XmlElement itemXML = doc.CreateElement("printergroup");
                itemXML.SetAttribute("name", g.Name);
                root.AppendChild(itemXML);
            }
            doc.AppendChild(root);
            MemoryStream ms = new MemoryStream();
            XmlTextWriter tw = new XmlTextWriter(ms, Encoding.UTF8);
            tw.Formatting = Formatting.Indented;
            //doc.WriteContentTo(tw);
            doc.WriteTo(tw);
            tw.Close();

            return ms.ToArray();
        }
        public ACA.LabelX.Toolbox.PrintGroupItemList GetLabelPrintGroupsEx()
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetLabelPrintGroupsEx");
            ACA.LabelX.Toolbox.PrintGroupItemList PrintGroups;
            PrintGroups = new ACA.LabelX.Toolbox.PrintGroupItemList();

            string AppPath = GlobalDataStore.AppPath; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }
            //not need all but fasted to use this for now...
            string PrintJobsRootFolder;
            string LabelDefinitionsRootFolder;
            string PaperDefinitionsRootFolder;
            string SettingsRootFolder;
            string MachineName;
            int PollFrequency;

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetGeneralClientConfiguratonEx(AppPath + @"\ACALabelXClient.config.xml",
                                out PrintJobsRootFolder,
                                out LabelDefinitionsRootFolder,
                                out PaperDefinitionsRootFolder,
                                out SettingsRootFolder,
                                out MachineName,
                                out PollFrequency,
                                ref PrintGroups);

            return PrintGroups;
        }
        public void GetFolderInformation(out string PrintJobsRootFolder, out string LabelDefinitionsRootFolder, out string PaperDefinitionsRootFolder)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetFolderInformation");

            string AppPath = GlobalDataStore.AppPath;
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }
            //not need all but fasted to use this for now...
            string SettingsRootFolder;
            string MachineName;
            int PollFrequency;
            PrintGroupItemList PrintGroups = new PrintGroupItemList();
            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetGeneralClientConfiguratonEx(AppPath + @"\ACALabelXClient.config.xml",
                                out PrintJobsRootFolder,
                                out LabelDefinitionsRootFolder,
                                out PaperDefinitionsRootFolder,
                                out SettingsRootFolder,
                                out MachineName,
                                out PollFrequency,
                                ref PrintGroups);
        }
        public void GetName(out string name)
        {
            string AppPath = GlobalDataStore.AppPath;
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }
            XPathDocument theDoc;
            XPathNavigator nav;
            try
            {
                theDoc = new XPathDocument(RemotingConfigFilePath);
            }
            catch (System.Xml.XmlException e1)
            {
                ApplicationException e2 =
                    new ApplicationException(
                        string.Format("XML syntax error in {0}: {1}", RemotingConfigFilePath, e1.Message), e1);
                throw e2;
            }
            
            nav = theDoc.CreateNavigator();

            XPathNodeIterator nit = nav.Select("/configuration/general-settings/machine-name");
            nit.MoveNext();
            name = nit.Current.Value;
        }

        public ACA.LabelX.Toolbox.PrintGroupItemList GetLabelPrintGroupsEx(out string Machine)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetLabelPrintGroupsEx");
            ACA.LabelX.Toolbox.PrintGroupItemList PrintGroups;
            PrintGroups = new ACA.LabelX.Toolbox.PrintGroupItemList();

            string AppPath = GlobalDataStore.AppPath; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }
            //not need all but faster to use this for now...
            string PrintJobsRootFolder;
            string LabelDefinitionsRootFolder;
            string PaperDefinitionsRootFolder;
            string SettingsRootFolder;
            string MachineName;
            int PollFrequency;

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetGeneralClientConfiguratonEx(AppPath + @"\ACALabelXClient.config.xml",
                                out PrintJobsRootFolder,
                                out LabelDefinitionsRootFolder,
                                out PaperDefinitionsRootFolder,
                                out SettingsRootFolder,
                                out MachineName,
                                out PollFrequency,
                                ref PrintGroups);
            Machine = MachineName;
            return PrintGroups;
        }
        public void GetLabelPrintGroups(ref byte[] printgroupXMLCompressed, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetLabelPrintGroups");

            List<ACA.LabelX.Toolbox.PrintGroupItem> PrintGroups;
            PrintGroups = new List<LabelX.Toolbox.PrintGroupItem>();

            string AppPath = GlobalDataStore.AppPath; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }
            //not need all but fasted to use this for now...
            string PrintJobsRootFolder;
            string LabelDefinitionsRootFolder;
            string PaperDefinitionsRootFolder;
            string SettingsRootFolder;
            string MachineName;
            int PollFrequency;

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetGeneralClientConfiguraton(AppPath + @"\ACALabelXClient.config.xml",
                                out PrintJobsRootFolder,
                                out LabelDefinitionsRootFolder,
                                out PaperDefinitionsRootFolder,
                                out SettingsRootFolder,
                                out MachineName,
                                out PollFrequency,
                                ref PrintGroups);


            byte[] UncompressedPrintgroupXml;
            UncompressedPrintgroupXml = ConvertPrintergroupsToXML(PrintGroups);
            UncompressedDataLength = UncompressedPrintgroupXml.Length;
            printgroupXMLCompressed = PSLib.Compression.Compress(UncompressedPrintgroupXml);
        }
        public ACA.LabelX.Toolbox.PrintGroupItem GetLabelPrintGroupByName(string sName)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetLabelPrintGroupByName");
            string AppPath = GlobalDataStore.AppPath;
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            FileInfo fi;
            fi = new FileInfo(RemotingConfigFilePath);

            if (!fi.Exists)
            {
                throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }
            // if (!File.Exists(RemotingConfigFilePath))
            //     throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            PrintGroupItem pgItem = null;
            lock (GlobalDataStore.LockClass)
            {
                XPathDocument theDoc;
                XPathNavigator nav;
                string sHelp;

                try
                {
                    theDoc = new XPathDocument(RemotingConfigFilePath);
                }
                catch (System.Xml.XmlException e1)
                {
                    ApplicationException e2;
                    e2 = new ApplicationException(string.Format("XML syntax error in {0}: {1}", RemotingConfigFilePath, e1.Message), e1);
                    throw e2;
                }

                nav = theDoc.CreateNavigator();

                XPathNodeIterator nit;




                nit = nav.Select("/configuration/general-settings/print-groups/print-group[@name='" + sName + "']");
                if (nit != null)
                {
                    nit.MoveNext();
                    pgItem = new PrintGroupItem();
                    pgItem.GroupPrinters = new PrinterItems();
                    pgItem.Name = nit.Current.GetAttribute("name", string.Empty);
                    pgItem.Enabled = nit.Current.GetAttribute("enabled", string.Empty).Equals("true", StringComparison.OrdinalIgnoreCase);

                    nit = nav.Select("/configuration/general-settings/print-groups/print-group[@name='" + sName + "']/printer");
                    while (nit.MoveNext())
                    {
                        PrinterItem pit;
                        pit = new PrinterItem();

                        sHelp = nit.Current.GetAttribute("name", string.Empty);
                        pit.LongName = sHelp;
                        sHelp = nit.Current.GetAttribute("enabled", string.Empty);
                        pit.Enabled = sHelp.Equals("true", StringComparison.OrdinalIgnoreCase);

                        //Retrieve some other data for this printer...
                        System.Printing.LocalPrintServer localServer;
                        localServer = new System.Printing.LocalPrintServer();
                        System.Printing.PrintQueueCollection col;
                        System.Printing.EnumeratedPrintQueueTypes[] myEnum =  
                        {   EnumeratedPrintQueueTypes.Connections
                            ,EnumeratedPrintQueueTypes.Local };
                        col = localServer.GetPrintQueues(myEnum);
                        System.Printing.PrintQueue theQueue = null;
                        foreach (System.Printing.PrintQueue q in col)
                        {
                            if (q.FullName.Equals(pit.LongName, StringComparison.OrdinalIgnoreCase))
                            {
                                theQueue = q;
                                break;
                            }
                        }

                        if (theQueue != null)
                        {
                            pit.QueueLength = theQueue.NumberOfJobs;
                            pit.ShortName = theQueue.Name;
                            pit.Online = !theQueue.IsOffline;
                            pit.NeedsUserIntervention = theQueue.NeedUserIntervention;
                            //
                            //theQueue.NeedUserIntervention;
                        }
                        else
                        {
                            pit.QueueLength = 0;
                            pit.NeedsUserIntervention = false;
                            pit.ShortName = pit.LongName;
                            pit.Online = true;
                        }
                        if (col != null)
                        {
                            col.Dispose();
                            col = null;
                        }
                        if (theQueue != null)
                        {
                            theQueue.Dispose();
                            theQueue = null;
                        }

                        if (nit.Current.HasChildren)
                        {
                            //nit.Current.MoveToFirstChild();
                            nit.Current.MoveToFirstChild();
                            //MoveToNext("tray", nav.LookupNamespace(nav.Prefix)))
                            do
                            {
                                PrinterTrayItem ptit;
                                ptit = new PrinterTrayItem();
                                ptit.TrayName = nit.Current.GetAttribute("name", string.Empty);
                                ptit.CurrentPapertypeName = nit.Current.GetAttribute("papertype", string.Empty);
                                pit.Trays.Add(ptit);
                            } while (nit.Current.MoveToNext("tray", nav.LookupNamespace(nav.Prefix)));
                            nit.Current.MoveToParent();
                        }
                        //pit.CurrentPapertypeName = nit.Current.GetAttribute("papertype",string.Empty);
                        //pit.Tray = nit.Current.GetAttribute("tray", string.Empty);
                        pgItem.GroupPrinters.Add(pit);
                        localServer.Dispose();
                    }
                }
            }
            return pgItem;

        }
        public StringCollection GetPaperTypes()
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetPaperTypes");

            string PrintJobsRootFolder;
            string LabelDefinitionsRootFolder;
            string PaperDefinitionsRootFolder;
            string SettingsRootFolder;
            string MachineName;
            int PollFrequency;
            PrintGroupItemList PrintGroups;
            PrintGroups = new PrintGroupItemList();
            string AppPath = GlobalDataStore.AppPath;// System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetGeneralClientConfiguratonEx(AppPath + @"\ACALabelXClient.config.xml",
                                out PrintJobsRootFolder,
                                out LabelDefinitionsRootFolder,
                                out PaperDefinitionsRootFolder,
                                out SettingsRootFolder,
                                out MachineName,
                                out PollFrequency,
                                ref PrintGroups);

            StringCollection theList;
            List<LabelXItem> itemList;
            itemList = new List<LabelXItem>();
            theList = new StringCollection();
            Toolbox.Toolbox.GetItemsFromFolder(PaperDefinitionsRootFolder, ref itemList, string.Empty, LabelX.Toolbox.Toolbox.FileFilterXML);

            foreach (LabelXItem it in itemList)
            {
                theList.Add(it.Name);
            }
            return theList;
        }
        public StringCollection GetLocalPrinters()
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetLocalPrinters");
            StringCollection theCol;
            theCol = new StringCollection();
            foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                theCol.Add(s);
            }
            return theCol;
        }

        public PrinterItems GetLocalPrintersEx()
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetLocalPrintersEx");
            PrinterItems retItems = new PrinterItems();

            System.Printing.LocalPrintServer localServer;
            localServer = new System.Printing.LocalPrintServer();

            System.Printing.EnumeratedPrintQueueTypes[] myEnum =  
            {   EnumeratedPrintQueueTypes.Connections
               ,EnumeratedPrintQueueTypes.Local };
            System.Printing.PrintQueueCollection col = localServer.GetPrintQueues(myEnum);


            foreach (System.Printing.PrintQueue qit in col)
            {
                PrinterItem it;
                it = new PrinterItem();
                it.QueueLength = qit.NumberOfJobs;
                it.LongName = qit.FullName;
                it.ShortName = qit.Name;
                it.Enabled = true;
                it.Online = !qit.IsOffline;
                it.NeedsUserIntervention = qit.NeedUserIntervention;

                System.Drawing.Printing.PrinterSettings ps;
                ps = new System.Drawing.Printing.PrinterSettings();
                ps.PrinterName = qit.FullName;

                foreach (System.Drawing.Printing.PaperSource src in ps.PaperSources)
                {
                    PrinterTrayItem theTray;
                    theTray = new PrinterTrayItem();
                    theTray.TrayName = src.SourceName;
                    it.Trays.Add(theTray);
                }

                retItems.Add(it);
            }
            col.Dispose();
            return retItems;
        }
        public StringCollection GetSupportedTraysOfPrinter(string sPrinterName)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.GetSupportedTraysOfPrinter");

            StringCollection col;
            System.Drawing.Printing.PrinterSettings ps;
            col = new StringCollection();
            ps = new System.Drawing.Printing.PrinterSettings();
            ps.PrinterName = sPrinterName;
            foreach (System.Drawing.Printing.PaperSource src in ps.PaperSources)
            {
                col.Add(src.SourceName);
            }
            return col;
        }
        public bool AddPrinterToPrintGroupItem(PrintGroupItem it, PrinterItem pi)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.AddPrinterToPrintGroupItem");
            bool bRet = false;
            string PrintJobsRootFolder;
            string LabelDefinitionsRootFolder;
            string PaperDefinitionsRootFolder;
            string SettingsRootFolder;
            string MachineName;
            int PollFrequency;
            PrintGroupItemList PrintGroups;
            PrintGroups = new PrintGroupItemList();
            string AppPath = GlobalDataStore.AppPath; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetGeneralClientConfiguratonEx(AppPath + @"\ACALabelXClient.config.xml",
                                out PrintJobsRootFolder,
                                out LabelDefinitionsRootFolder,
                                out PaperDefinitionsRootFolder,
                                out SettingsRootFolder,
                                out MachineName,
                                out PollFrequency,
                                ref PrintGroups);

            //First check if the printgroupitem still exists
            foreach (PrintGroupItem item in PrintGroups)
            {
                if (item.Name.Equals(it.Name, StringComparison.OrdinalIgnoreCase))
                {
                    bRet = Toolbox.Toolbox.StorePrinter(AppPath + @"\ACALabelXClient.config.xml", item, pi);
                    break;
                }
            }
            return bRet;
        }
        public bool UpdatePrintgroupStatus(PrintGroupItem it)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.UpdatePrintgroupStatus");
            bool bRet = true;
            string AppPath = GlobalDataStore.AppPath;
            string sXMLFile;

            sXMLFile = AppPath + @"\ACALabelXClient.config.xml";
            PrintGroupItem theItem;
            theItem = GetLabelPrintGroupByName(it.Name); //Retrieve again... could be changed on the server
            theItem.Enabled = it.Enabled;

            bRet = Toolbox.Toolbox.StorePrinterGroupStatus(sXMLFile, theItem);

            return bRet;
        }
        public bool UpdatePrinterStatus(PrintGroupItem it, PrinterItem pi)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.UpdatePrinterStatus");
            return UpdatePrinterStatus(it, pi.LongName, pi.Enabled);
        }
        public bool UpdatePrinterStatus(PrintGroupItem it, string PrinterName, bool Status)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.UpdatePrinterStatus-2");
            bool bRet = true;
            string AppPath = GlobalDataStore.AppPath; ; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string sXMLFile;
            sXMLFile = AppPath + @"\ACALabelXClient.config.xml";

            PrintGroupItem theItem;
            theItem = GetLabelPrintGroupByName(it.Name); //Retrieve again... could be changed on the server
            foreach (PrinterItem pi in it.GroupPrinters)
            {
                if (pi.LongName.Equals(PrinterName, StringComparison.OrdinalIgnoreCase))
                {
                    PrinterItem pi2;
                    pi2 = pi;
                    pi2.Enabled = Status;
                    bRet = Toolbox.Toolbox.StorePrinter(sXMLFile, theItem, pi2);
                    break;
                }
            }
            return bRet;
        }
        public bool UpdatePrinterForPrintgroup(PrintGroupItem it, PrinterItem pi)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.UpdatePrinterForPrintgroup");
            bool bRet = true;
            string AppPath = GlobalDataStore.AppPath; ; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string sXMLFile;
            sXMLFile = AppPath + @"\ACALabelXClient.config.xml";

            PrintGroupItem theItem;
            theItem = GetLabelPrintGroupByName(it.Name); //Retrieve again... could be changed on the server
            bRet = Toolbox.Toolbox.StorePrinter(sXMLFile, theItem, pi);
            return bRet;
        }
        public bool RemovePrinterFromPrinterGroup(PrintGroupItem it, PrinterItem pi)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.RemovePrinterFromPrinterGroup");
            return RemovePrinterFromPrinterGroup(it, pi.LongName);
        }
        public bool RemovePrinterFromPrinterGroup(PrintGroupItem it, string PrinterName)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.RemovePrinterFromPrinterGroup-2");
            bool bRet;
            string AppPath = GlobalDataStore.AppPath; ; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            bRet = Toolbox.Toolbox.RemovePrinterFromPrinterGroup(AppPath + @"\ACALabelXClient.config.xml", it, PrinterName);
            return bRet;
        }
        public bool AddPrinterToPrintGroupItem(PrintGroupItem it, string PrinterName)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.AddPrinterToPrintGroupItem");
            bool bRet = false;
            string PrintJobsRootFolder;
            string LabelDefinitionsRootFolder;
            string PaperDefinitionsRootFolder;
            string SettingsRootFolder;
            string MachineName;
            int PollFrequency;
            PrintGroupItemList PrintGroups;
            PrintGroups = new PrintGroupItemList();
            string AppPath = GlobalDataStore.AppPath; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetGeneralClientConfiguratonEx(AppPath + @"\ACALabelXClient.config.xml",
                                out PrintJobsRootFolder,
                                out LabelDefinitionsRootFolder,
                                out PaperDefinitionsRootFolder,
                                out SettingsRootFolder,
                                out MachineName,
                                out PollFrequency,
                                ref PrintGroups);

            //First check if the printgroupitem still exists
            foreach (PrintGroupItem item in PrintGroups)
            {
                if (item.Name.Equals(it.Name, StringComparison.OrdinalIgnoreCase))
                {
                    PrinterItem pi;
                    pi = new PrinterItem();
                    pi.LongName = PrinterName;
                    pi.Enabled = true;
                    pi.Trays = new PrinterTrayItems();
                    bRet = Toolbox.Toolbox.StorePrinter(AppPath + @"\ACALabelXClient.config.xml", item, pi);
                    break;
                }
            }
            return bRet;
        }
        //mvesecurity!!
        [SecurityCritical] 
        public override object InitializeLifetimeService()
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.InitializeLifetimeService");
            return null; //null means the lease will never expire...
        }
        public PrintJobInfos GetPrintjobsForPrintgroup(PrintGroupItem it)
        {
            GlobalDataStore.Logger.Debug(string.Format("RemClientControlObject.GetPrintjobsForPrintgroup {0}", it.Name));
            string AppPath = GlobalDataStore.AppPath;// System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();

            PrintJobInfos infos;
            infos = toolbox.GetPrintjobsForPrintgroup(RemotingConfigFilePath, it);
            return infos;
        }
        public bool UpdatePrintjobStatus(PrintJobInfo fi)
        {
            GlobalDataStore.Logger.Debug("RemClientControlObject.UpdatePrintjobStatus");
            //Try to update the printjob xml... maybe it has already been printed...
            bool bRet;
            XmlDocument theDoc;
            theDoc = new XmlDocument();
            bRet = true;
            lock (GlobalDataStore.LockClass)
            {
                if (File.Exists(fi.FullFilename))
                {
                    try
                    {
                        theDoc.Load(fi.FullFilename);
                        XmlNode node = theDoc.SelectSingleNode("/printjob/autorelease");
                        if (node != null)
                        {
                            node.InnerText = fi.AutoRelease ? "true" : "false";
                        }
                        else
                        {
                            XmlNode node2 = theDoc.SelectSingleNode("/printjob");
                            if (node2 != null)
                            {
                                XmlElement el = theDoc.CreateElement("autorelease");
                                el.InnerText = fi.AutoRelease ? "true" : "false";
                                node2.AppendChild(el);
                            }
                        }
                        theDoc.Save(fi.FullFilename);
                    }
                    catch (Exception)
                    {
                        bRet = false;
                    }
                }
                else
                {
                    bRet = false;
                }
            }
            return bRet;
        }
        public bool RemovePrinterpool(string p)
        {
            GlobalDataStore.Logger.Debug(string.Format("RemClientControlObject.RemovePrinterpool", p));
            bool bRet = false;
            string AppPath = GlobalDataStore.AppPath;
            bRet = Toolbox.Toolbox.RemovePrintpool(AppPath + @"\ACALabelXClient.config.xml", p);
            //How do we do this.... warn the other thread to send info...
            //ACA.LabelX.Managers.LabelXClientServerManager.ResendInfo();
            lock (GlobalDataStore.LockClass)
            {
                if (GlobalDataStore.IsStandAlone)
                {
                    GlobalDataStore.MustWriteStandAlonePrintGroups = true;
                }
                else
                {
                    GlobalDataStore.ResendInfo();
                }
            }
            return bRet;
        }
        public bool AddPrinterpool(string p)
        {
            GlobalDataStore.Logger.Debug(string.Format("Addprinterpool {0}", p));
            bool bRet = false;
            string AppPath = GlobalDataStore.AppPath;
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            lock (GlobalDataStore.LockClass)
            {
                if (!File.Exists(RemotingConfigFilePath))
                    throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            }
            bRet = Toolbox.Toolbox.AddPrintpool(RemotingConfigFilePath, p);
            //How do we do this.... warn the other thread to send info...
            //ACA.LabelX.Managers.LabelXClientServerManager.ResendInfo();
            lock (GlobalDataStore.LockClass)
            {
                if (GlobalDataStore.IsStandAlone)
                {
                    GlobalDataStore.MustWriteStandAlonePrintGroups = true;
                }
                else
                {
                    GlobalDataStore.ResendInfo();
                }
            }
            return bRet;
        }
        public bool DiscardPrintJob(PrintJobInfo fi)
        {
            int teller = 0;
            bool bRet = false;
            while ((teller < 5) && (bRet == false))
            {
                //
                //It can be another tread is reading this file.
                //Wait sometime until they release the file.
                try
                {
                    File.Delete(fi.FullFilename);
                    bRet = true;
                }
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Error("Unable to discard printjob file: " + e.Message);
                }
                teller++;
                if (bRet == false)
                    System.Threading.Thread.Sleep(2000);
            }
            return bRet;
        }
        public string[] GetPrintjobPreview(PrintJobInfo fi)
        {
            string[] lines;
            lines = new string[1000];
            lines.Initialize();
            lock (GlobalDataStore.LockClass)
            {
                if (File.Exists(fi.FullFilename))
                {
                    using (System.IO.StreamReader sr = File.OpenText(fi.FullFilename))
                    {
                        string input;
                        int tel = 0;
                        while ((input = sr.ReadLine()) != null)
                        {
                            lines[tel] = input;
                            tel++;
                            if (tel > 999)
                                break;
                        }
                        sr.Close();
                        sr.Dispose();
                    }
                }
                else
                {
                    GlobalDataStore.Logger.Warning("File " + fi.FullFilename + " not found when trying to get a printjob preview");
                }
            }
            return lines;
        }
    }

}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
