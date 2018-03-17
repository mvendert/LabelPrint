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
using System.Xml;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace ACA.LabelX.Toolbox
{
    public class LabelXToolboxException : ApplicationException
    {
        public LabelXToolboxException()
        {
        }
        public LabelXToolboxException(string message)
            : base(message)
        {
        }
        public LabelXToolboxException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public struct LabelXItem
    {
        public string Name;
        public string Hash;
    }

    [Serializable]
    public class PrintLanguage
    {
        public int Id;
    }

    [Serializable]
    [XmlInclude(typeof(PrintLanguage))]
    public class PrintLanguages : List<PrintLanguage>
    {
    }

    //
    // PrintGroupItem
    //      string              Name
    //      bool                Enabled
    //      List <PrinterItem>  Printers
    //
    // PrinterItem
    //      string  Name
    //      bool    Enabled
    //      string  PaperTypeName
    [Serializable]
    public class PrinterTrayItem
    {
        public string TrayName;
        public string CurrentPapertypeName;

        public PrinterTrayItem()
        {
            TrayName = string.Empty;
            CurrentPapertypeName = string.Empty;
        }
    }

    [Serializable]
    public class PrinterTrayItems : List<PrinterTrayItem>
    {
    }

    [Serializable]
    public class PrinterItem
    {
        public string LongName;
        public string ShortName;
        public bool Enabled;
        public bool NeedsUserIntervention;
        public PrinterTrayItems Trays;
        public int QueueLength;
        public bool Online;

        public PrinterItem()
        {
            ShortName = string.Empty;
            LongName = string.Empty;
            Enabled = false;
            Online = true;
            NeedsUserIntervention = false;
            QueueLength = 0;
            Trays = new PrinterTrayItems();
        }
    }

    //This data is not remoted. It if for Local handling the jobs...
    public class PrinterItemLocal
    {

        public PrinterItem item;
        /// <summary>
        /// The Actual selected tray for printing. Only used in LabelXPrintGroupManager!!!
        /// </summary>
        public PrinterTrayItem Tray;

        public PrinterItemLocal()
        {
            item = new PrinterItem();
            Tray = null;
        }

    }
    public class PrinterItemLocals : List<PrinterItemLocal>
    {
    }

    [Serializable]
    [XmlInclude(typeof(PrinterItem))]
    public class PrinterItems : List<PrinterItem>
    {
    }
    [Serializable]
    [XmlInclude(typeof(PrinterItem)), XmlInclude(typeof(PrinterItems)), XmlInclude(typeof(PrintGroupItem)), XmlInclude(typeof(PrinterTrayItem)), XmlInclude(typeof(PrinterTrayItems))]
    public class PrintGroupItem
    {
        public string Name;
        public bool Enabled;
        public PrinterItems GroupPrinters;

        public PrintGroupItem()
        {
            Name = string.Empty;
            Enabled = false;
            GroupPrinters = new PrinterItems();
        }
    }
    [Serializable]
    [XmlInclude(typeof(PrinterItem)), XmlInclude(typeof(PrinterItems)), XmlInclude(typeof(PrintGroupItem)), XmlInclude(typeof(PrinterTrayItem)), XmlInclude(typeof(PrinterTrayItems))]
    public class PrintGroupItemList : List<ACA.LabelX.Toolbox.PrintGroupItem>
    {
    }

    [Serializable]
    [XmlInclude(typeof(PrinterItem)), XmlInclude(typeof(PrinterItems)), XmlInclude(typeof(PrintGroupItem)), XmlInclude(typeof(PrinterTrayItem)), XmlInclude(typeof(PrinterTrayItems)), XmlInclude(typeof(PrintGroupItemList))]
    public class LogonInfo
    {
        public string Username;
        public string Password;
        public System.Net.IPAddress IpNumber;
        public string MachineName;
        public string MachineDescription;
        public double ProgramVersion;
        public bool updateReady;
        public PrintGroupItemList PrintGroups;
    }

    public class PrinterFullData
    {
        public PrintGroupItem PrintGroup;
        public PrinterItem Printer;
        public PrinterTrayItem Tray;
        public string PaperType;
    }

    [Serializable]
    public class PrintJobInfo
    {
        public string FullFilename;
        public String LabelType;
        public String MachineName;
        public String PrintGroup;
        public String Description;
        public String PrintedTo;
        public DateTime LastPrinted;
        public DateTime CreationDateTime;
        public bool AutoRelease;
        public int NumberOfLabels;
        public Int64 Size;
        public string ID;
        public string From;
        public string User;
        public PrintLanguages SupportedLanguages;
        public PrintJobInfo()
        {
            LabelType = string.Empty;
            MachineName = string.Empty;
            PrintGroup = string.Empty;
            CreationDateTime = DateTime.Now;
            AutoRelease = false;
            NumberOfLabels = 0;
            Description = string.Empty;
            SupportedLanguages = new PrintLanguages();
        }
    }
    [Serializable]
    [XmlInclude(typeof(PrintJobInfo)), XmlInclude(typeof(PrintLanguages)), XmlInclude(typeof(PrintLanguage))]
    public class PrintJobInfos : List<PrintJobInfo>
    {
    }


    public class ConnectionParameter
    {
        public enum ConnectionTypes { Client, Sever };
        public ConnectionTypes ConnectionType;
        public string ConnectionName;
        public string Computer;
        public int PortNumber;
        public string Protocol;

        public ConnectionParameter()
        {
            ConnectionName = "default";
            //Computer = "localhost";
            Computer = "127.0.0.1";
            PortNumber = 18081;
            Protocol = "http";
            ConnectionType = ConnectionTypes.Client;
        }
    }
    /*
    public class PrinterSupport
    {
        public string GetLongNameOfShortName(string sShortName)
        {
            string sRet;
            sRet = sShortName;
            System.Printing.LocalPrintServer localServer;
            localServer = new System.Printing.LocalPrintServer();

            //
            //We need to retrieve local printers and network printers for this
            //workstation or server.
            System.Printing.EnumeratedPrintQueueTypes [] myEnum =  
            {   EnumeratedPrintQueueTypes.Connections
               ,EnumeratedPrintQueueTypes.Local };
            System.Printing.PrintQueueCollection col = localServer.GetPrintQueues(myEnum);

            
            foreach (System.Printing.PrintQueue qit in col)
            {
                if (qit.Name.Equals(sShortName,StringComparison.OrdinalIgnoreCase)
                {
                    sRet = qit.FullName;
                    break;
                }
            }
            return sRet;
        }

        public PrinterItemLocals GetLocalPrinterItems()
        {
            PrinterItemLocals retItems = new PrinterItemLocals();
            System.Printing.LocalPrintServer localServer;
            localServer = new System.Printing.LocalPrintServer();

            //
            //We need to retrieve local printers and network printers for this
            //workstation or server.
            System.Printing.EnumeratedPrintQueueTypes [] myEnum =  
            {   EnumeratedPrintQueueTypes.Connections
               ,EnumeratedPrintQueueTypes.Local };
            System.Printing.PrintQueueCollection col = localServer.GetPrintQueues(myEnum);

            
            foreach (System.Printing.PrintQueue qit in col)
            {
                PrinterItemLocal it;
                it = new PrinterItem();
                it.Fullname = qit.FullName;
                it.ShortName = qit.Name;
                it.QueueLength = qit.NumberOfJobs;
                it.item.Name = qit.FullName;

                System.Drawing.Printing.PrinterSettings ps;
                ps = new System.Drawing.Printing.PrinterSettings();
                ps.PrinterName = qit.FullName;
                StringCollection col;
                col = new StringCollection();
                foreach (System.Drawing.Printing.PaperSource src in ps.PaperSources)
                {
                    PrinterTrayItem theTray;
                    theTray = new PrinterTrayItem();
                    theTray.TrayName =
                    it.item.Trays.Add(
                    it.Tray.TrayName = src.SourceName;
                }
                return col;

                retItems.Add(it);
            }
                
        }
    }
     */
    public class Toolbox
    {
        public const string FileFilterXML = "*.XML";
        public void GetConfiguraton(string ConfigFilePath, string XMLSchemaResourceName, ref IDictionary<string, string> dictionary)
        {
            //bool mustExport = false;
            /*

            Assembly assembly = this.GetType().Assembly;
            if (assembly != null && (assembly.Location != null))
            {
                FileInfo fi = new FileInfo(assembly.Location);

                if (!File.Exists(XMLSchemaResourceName))
                {
                    mustExport = true;
                }

                if (!mustExport)
                {
                    FileInfo fi2 = new FileInfo(XMLSchemaResourceName);
                    if (fi2.LastWriteTime < fi.LastWriteTime)
                    {
                        mustExport = true;
                    }
                }
            }

            if (mustExport)
            {
                Stream s = assembly.GetManifestResourceStream("ACALabelXToolbox." + XMLSchemaResourceName);
                if (s != null)
                {
                    TextReader tr = new StreamReader(s);
                    string XMLSchema = tr.ReadToEnd();
                    tr.Close();

                    TextWriter tw = new StreamWriter(XMLSchemaResourceName);
                    tw.Write(XMLSchema);
                    tw.Close();
                }
            }
            Export of file not needed anymore because we do not validate...
            */
            lock (GlobalDataStore.LockClass)
            {
                //               ACA.PSLib.Xml.ValidateXML(ConfigFilePath, XMLSchemaResourceName);
                ACA.PSLib.Xml.XMLToDictionary(ConfigFilePath, ref dictionary);
            }
        }

        public void GetRemotingClientRemoteConfiguration(string RemotingConfigFilePath, out string Protocol, out string Address, out string Port, out string Uri)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            GetConfiguraton(RemotingConfigFilePath, "ACALabelXClientRemote.config.xsd", ref dictionary);

            Uri = dictionary["/configuration/system.runtime.remoting/application/service/wellknown.0@objectUri"];
            Address = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@address"];
            Protocol = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@ref"];
            Port = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@port"];

        }

        public void GetRemotingServerConfiguraton(string RemotingConfigFilePath, out string Protocol, out string Address, out string Port, out string Uri)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            GetConfiguraton(RemotingConfigFilePath, "ACALabelXServer.config.xsd", ref dictionary);

            Uri = dictionary["/configuration/system.runtime.remoting/application/service/wellknown.0@objectUri"];
            Address = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@address"];
            Protocol = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@ref"];
            Port = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@port"];
        }

        public void GetRemotingClientConfiguraton(string RemotingConfigFilePath, out string Protocol, out string Address, out string Port, out string Uri)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            GetConfiguraton(RemotingConfigFilePath, "ACALabelXClient.config.xsd", ref dictionary);

            Uri = dictionary["/configuration/system.runtime.remoting/application/service/wellknown.0@objectUri"];
            Address = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@address"];
            Protocol = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@ref"];
            Port = dictionary["/configuration/system.runtime.remoting/application/channels/channel.0@port"];
        }

        public void GetGeneralClientConfiguraton(string GeneralConfigFilePath, out string PrintJobsRootFolder, out string LabelDefinitionsRootFolder, out string PaperDefinitionsRootFolder, out string SettingsRootFolder, out string MachineName, out int PollFrequency, ref List<PrintGroupItem> PrintGroups)
        {
            //            PollFrequency = 60 * 10; // 10 minutes

            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            GetConfiguraton(GeneralConfigFilePath, "ACALabelXClient.config.xsd", ref dictionary);

            PrintJobsRootFolder = dictionary["/configuration/general-settings/folders/PrintJobsRootFolder.0"];
            LabelDefinitionsRootFolder = dictionary["/configuration/general-settings/folders/LabelDefinitionsRootFolder.0"];
            PaperDefinitionsRootFolder = dictionary["/configuration/general-settings/folders/PaperDefinitionsRootFolder.0"];
            SettingsRootFolder = dictionary["/configuration/general-settings/folders/SettingsRootFolder.0"];

            MachineName = dictionary["/configuration/general-settings/machine-name.0"];

            for (int Index = 0; Index < 100; Index++)
            {
                string XMLPrintGroupName = string.Format("/configuration/general-settings/print-groups/print-group.{0:D}@name", Index);
                //                string XMLPrintGroupPrinter = string.Format("/configuration/general-settings/print-groups/print-group.{0:D}@printer", Index);
                try
                {
                    PrintGroupItem pgItem = new PrintGroupItem { Name = dictionary[XMLPrintGroupName] };
                    //pgItem.Printer = dictionary[XMLPrintGroupPrinter];
                    PrintGroups.Add(pgItem);
                }
                catch (KeyNotFoundException)
                {
                    break;
                }
            }

            PollFrequency = Convert.ToInt32(dictionary["/configuration/general-settings/poll-frequency.0"]);

            if (false == PrintJobsRootFolder.EndsWith("\\"))
                PrintJobsRootFolder += "\\";

            if (false == PaperDefinitionsRootFolder.EndsWith("\\"))
                PaperDefinitionsRootFolder += "\\";

            if (false == LabelDefinitionsRootFolder.EndsWith("\\"))
                LabelDefinitionsRootFolder += "\\";

            if (false == SettingsRootFolder.EndsWith("\\"))
                SettingsRootFolder += "\\";
        }

        public void GetGeneralClientConfiguraton(string GeneralConfigFilePath, out string PrintJobsRootFolder, out string LabelDefinitionsRootFolder, out string PaperDefinitionsRootFolder, out string SettingsRootFolder, out string PicturesRootFolder, out string UpdateRootFolder, out string MachineName, out int PollFrequency, ref List<PrintGroupItem> PrintGroups)
        {
            //            PollFrequency = 60 * 10; // 10 minutes

            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            GetConfiguraton(GeneralConfigFilePath, "ACALabelXClient.config.xsd", ref dictionary);

            PrintJobsRootFolder = dictionary["/configuration/general-settings/folders/PrintJobsRootFolder.0"];
            LabelDefinitionsRootFolder = dictionary["/configuration/general-settings/folders/LabelDefinitionsRootFolder.0"];
            PaperDefinitionsRootFolder = dictionary["/configuration/general-settings/folders/PaperDefinitionsRootFolder.0"];
            SettingsRootFolder = dictionary["/configuration/general-settings/folders/SettingsRootFolder.0"];
            PicturesRootFolder = dictionary["/configuration/general-settings/folders/PicturesRootFolder.0"];
            UpdateRootFolder = dictionary["/configuration/general-settings/folders/UpdateRootFolder.0"];
            MachineName = dictionary["/configuration/general-settings/machine-name.0"];

            for (int Index = 0; Index < 100; Index++)
            {
                string XMLPrintGroupName = string.Format("/configuration/general-settings/print-groups/print-group.{0:D}@name", Index);
                //                string XMLPrintGroupPrinter = string.Format("/configuration/general-settings/print-groups/print-group.{0:D}@printer", Index);
                try
                {
                    PrintGroupItem pgItem = new PrintGroupItem { Name = dictionary[XMLPrintGroupName] };
                    //pgItem.Printer = dictionary[XMLPrintGroupPrinter];
                    PrintGroups.Add(pgItem);
                }
                catch (KeyNotFoundException)
                {
                    break;
                }
            }

            PollFrequency = Convert.ToInt32(dictionary["/configuration/general-settings/poll-frequency.0"]);

            if (false == PrintJobsRootFolder.EndsWith("\\"))
                PrintJobsRootFolder += "\\";

            if (false == PaperDefinitionsRootFolder.EndsWith("\\"))
                PaperDefinitionsRootFolder += "\\";

            if (false == LabelDefinitionsRootFolder.EndsWith("\\"))
                LabelDefinitionsRootFolder += "\\";

            if (false == SettingsRootFolder.EndsWith("\\"))
                SettingsRootFolder += "\\";

            if (false == PicturesRootFolder.EndsWith("\\"))
                PicturesRootFolder += "\\";

            if (false == UpdateRootFolder.EndsWith("\\"))
                UpdateRootFolder += "\\";
        }

        public void GetClientPrintGroups(string GeneralConfigFilePath, ref List<PrintGroupItem> PrintGroups)
        {

            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            GetConfiguraton(GeneralConfigFilePath, "ACALabelXClient.config.xsd", ref dictionary);

            PrintGroups.Clear();
            for (int Index = 0; Index < 100; Index++)
            {
                string XMLPrintGroupName = string.Format("/configuration/general-settings/print-groups/print-group.{0:D}@name", Index);
                try
                {
                    PrintGroupItem pgItem = new PrintGroupItem { Name = dictionary[XMLPrintGroupName] };
                    PrintGroups.Add(pgItem);
                }
                catch (KeyNotFoundException)
                {
                    break;
                }
            }

        }

        public string GetClientConfigurationLanguage(string GeneralConfigFilePath)
        {
            XPathDocument theDoc;
            XPathNavigator nav;
            string sLanguage;

            lock (GlobalDataStore.LockClass)
            {
                try
                {
                    theDoc = new XPathDocument(GeneralConfigFilePath);
                }
                catch (System.Xml.XmlException e1)
                {
                    ApplicationException e2 = new ApplicationException(string.Format("XML syntax error in {0}: {1}", GeneralConfigFilePath, e1.Message), e1);
                    throw e2;
                }

                nav = theDoc.CreateNavigator();

                XPathNodeIterator nit = nav.Select("/configuration/general-settings/printlanguage");
                nit.MoveNext();
                sLanguage = nit.Current.GetAttribute("id", string.Empty) ?? string.Empty;

                if (sLanguage == string.Empty)
                    sLanguage = "1043";
            }
            return sLanguage;
        }

        public bool GetIsStandalloneInstallation(string GeneralConfigFilePath)
        {
            XPathDocument theDoc;
            XPathNavigator nav;
            string sValue;
            bool bRet;

            lock (GlobalDataStore.LockClass)
            {
                try
                {
                    theDoc = new XPathDocument(GeneralConfigFilePath);
                }
                catch (System.Xml.XmlException e1)
                {
                    ApplicationException e2 = new ApplicationException(string.Format("XML syntax error in {0}: {1}", GeneralConfigFilePath, e1.Message), e1);
                    throw e2;
                }

                nav = theDoc.CreateNavigator();

                XPathNodeIterator nit = nav.Select("/configuration/general-settings/standallone");
                nit.MoveNext();
                sValue = nit.Current.GetAttribute("value", string.Empty);

                if (sValue == null)
                {
                    bRet = false;
                }
                else
                {
                    bRet = !(sValue == string.Empty) && sValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                GlobalDataStore.IsStandAlone = bRet;
            }
            return bRet;
        }

        public string GetMachineName(string GeneralConfigFilePath)
        {
            XPathDocument theDoc;
            XPathNodeIterator nit = null;
            lock (GlobalDataStore.LockClass)
            {
                try
                {
                    theDoc = new XPathDocument(GeneralConfigFilePath);
                }
                catch (System.Xml.XmlException e1)
                {
                    ApplicationException e2 =
                        new ApplicationException(
                            string.Format("XML syntax error in {0}: {1}", GeneralConfigFilePath, e1.Message), e1);
                    throw e2;
                }

                XPathNavigator nav = theDoc.CreateNavigator();

                nit = nav.Select("/configuration/general-settings/machine-name");
                nit.MoveNext();
            }
            return nit.Current.Value;
        }

        public void GetGeneralClientConfiguratonEx(string GeneralConfigFilePath, out string PrintJobsRootFolder, out string LabelDefinitionsRootFolder, out string PaperDefinitionsRootFolder, out string SettingsRootFolder, out string MachineName, out int PollFrequency, ref PrintGroupItemList PrintGroups)
        {
            PollFrequency = 60 * 10; // 10 minutes
            XPathDocument theDoc;
            XPathNavigator nav;
            string sHelp;

            PrintJobsRootFolder = string.Empty;
            LabelDefinitionsRootFolder = string.Empty;
            PaperDefinitionsRootFolder = string.Empty;
            SettingsRootFolder = string.Empty;
            MachineName = string.Empty;
            lock (GlobalDataStore.LockClass)
            {
                try
                {
                    theDoc = new XPathDocument(GeneralConfigFilePath);
                }
                catch (System.Xml.XmlException e1)
                {
                    ApplicationException e2 =
                        new ApplicationException(
                            string.Format("XML syntax error in {0}: {1}", GeneralConfigFilePath, e1.Message), e1);
                    throw e2;
                }

                nav = theDoc.CreateNavigator();

                XPathNodeIterator nit = nav.Select("/configuration/general-settings/folders/PrintJobsRootFolder");
                nit.MoveNext();
                PrintJobsRootFolder = nit.Current.Value;

                nit = nav.Select("/configuration/general-settings/folders/LabelDefinitionsRootFolder");
                nit.MoveNext();
                LabelDefinitionsRootFolder = nit.Current.Value;

                nit = nav.Select("/configuration/general-settings/folders/PaperDefinitionsRootFolder");
                nit.MoveNext();
                PaperDefinitionsRootFolder = nit.Current.Value;

                nit = nav.Select("/configuration/general-settings/folders/SettingsRootFolder");
                nit.MoveNext();
                SettingsRootFolder = nit.Current.Value;

                PrintGroupItem pgItem;

                nit = nav.Select("/configuration/general-settings/machine-name");
                nit.MoveNext();
                MachineName = nit.Current.Value;

                nit = nav.Select("/configuration/general-settings/print-groups/print-group");
                while (nit.MoveNext())
                {
                    pgItem = new PrintGroupItem();
                    sHelp = nit.Current.GetAttribute("name", string.Empty);
                    pgItem.Name = sHelp;
                    sHelp = nit.Current.GetAttribute("enabled", string.Empty);
                    pgItem.Enabled = sHelp.Equals("true", StringComparison.OrdinalIgnoreCase);
                    //pgItem.Enabled = sHelp.Equals("true",StringComparer.OrdinalIgnoreCase);
                    //sHelp = nit.Current.GetAttribute("papertype",string.Empty);
                    PrintGroups.Add(pgItem);
                }
                foreach (PrintGroupItem it in PrintGroups)
                {
                    if (it.GroupPrinters == null)
                    {
                        it.GroupPrinters = new PrinterItems();
                    }
                    else
                    {
                        it.GroupPrinters.Clear();
                    }
                    nit = nav.Select("/configuration/general-settings/print-groups/print-group[@name='" + it.Name + "']/printer");
                    while (nit.MoveNext())
                    {
                        PrinterItem pit = new PrinterItem();


                        sHelp = nit.Current.GetAttribute("name", string.Empty);
                        pit.LongName = sHelp;
                        sHelp = nit.Current.GetAttribute("enabled", string.Empty);
                        pit.Enabled = sHelp.Equals("true", StringComparison.OrdinalIgnoreCase);
                        //Handle the list of trays
                        if (nit.Current.HasChildren)
                        {
                            nit.Current.MoveToFirstChild();
                            do
                            {
                                PrinterTrayItem ptit = new PrinterTrayItem
                                                           {
                                                               TrayName = nit.Current.GetAttribute("name", string.Empty),
                                                               CurrentPapertypeName = nit.Current.GetAttribute("papertype", string.Empty)
                                                           };
                                pit.Trays.Add(ptit);
                            }
                            while (nit.Current.MoveToNext("tray", nav.LookupNamespace(nav.Prefix)));
                            nit.Current.MoveToParent();
                        }
                        it.GroupPrinters.Add(pit);
                    }
                }
                nit = nav.Select("/configuration/general-settings/poll-frequency");
                nit.MoveNext();
                PollFrequency = nit.Current.ValueAsInt;

                if (false == PrintJobsRootFolder.EndsWith("\\"))
                    PrintJobsRootFolder += "\\";

                if (false == PaperDefinitionsRootFolder.EndsWith("\\"))
                    PaperDefinitionsRootFolder += "\\";

                if (false == LabelDefinitionsRootFolder.EndsWith("\\"))
                    LabelDefinitionsRootFolder += "\\";

                if (false == SettingsRootFolder.EndsWith("\\"))
                    SettingsRootFolder += "\\";

            }
        }

        public void GetGeneralServerConfiguraton(string GeneralConfigFilePath, out string PrintJobsRootFolder, out string LabelDefinitionsRootFolder, out string PaperDefinitionsRootFolder, out string SettingsRootFolder, out string PicturesRootFolder, out string UpdateRootFolder)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            GetConfiguraton(GeneralConfigFilePath, "ACALabelXServer.config.xsd", ref dictionary);

            PrintJobsRootFolder = dictionary["/configuration/general-settings/folders/PrintJobsRootFolder.0"];
            LabelDefinitionsRootFolder = dictionary["/configuration/general-settings/folders/LabelDefinitionsRootFolder.0"];
            PaperDefinitionsRootFolder = dictionary["/configuration/general-settings/folders/PaperDefinitionsRootFolder.0"];
            SettingsRootFolder = dictionary["/configuration/general-settings/folders/SettingsRootFolder.0"];
            PicturesRootFolder = dictionary["/configuration/general-settings/folders/PicturesRootFolder.0"];
            UpdateRootFolder = dictionary["/configuration/general-settings/folders/UpdateRootFolder.0"];

            if (false == PrintJobsRootFolder.EndsWith("\\"))
                PrintJobsRootFolder += "\\";

            if (false == PaperDefinitionsRootFolder.EndsWith("\\"))
                PaperDefinitionsRootFolder += "\\";

            if (false == LabelDefinitionsRootFolder.EndsWith("\\"))
                LabelDefinitionsRootFolder += "\\";

            if (false == SettingsRootFolder.EndsWith("\\"))
                SettingsRootFolder += "\\";

            if (false == PicturesRootFolder.EndsWith("\\"))
                PicturesRootFolder += "\\";

            if (false == UpdateRootFolder.EndsWith("\\"))
                UpdateRootFolder += "\\";
        }

        public string GetGeneralClientPicturesFolder(string GeneralConfigFilePath)
        {
            string PicturesRootFolder;
            XPathDocument theDoc;
            XPathNavigator nav;
            XPathNodeIterator nit;
            lock (GlobalDataStore.LockClass)
            {
                try
                {
                    theDoc = new XPathDocument(GeneralConfigFilePath);
                }
                catch (System.Xml.XmlException e1)
                {
                    ApplicationException e2 =
                        new ApplicationException(
                            string.Format("XML syntax error in {0}: {1}", GeneralConfigFilePath, e1.Message), e1);
                    throw e2;
                }

                nav = theDoc.CreateNavigator();

                nit = nav.Select("/configuration/general-settings/folders/PicturesRootFolder");
                nit.MoveNext();
                PicturesRootFolder = nit.Current.Value;

                if (false == PicturesRootFolder.EndsWith("\\"))
                    PicturesRootFolder += "\\";

                return PicturesRootFolder;
            }
        }

        static public void GetItemsFromFolder(string FolderPath, ref List<LabelX.Toolbox.LabelXItem> items, string PreFix, string FileFilter)
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);            
            }
            string[] files;
            if (FileFilter == null)
            {
                FileFilter = string.Empty;
            }

            if (FileFilter.Length == 0)
            {
                files = Directory.GetFiles(FolderPath);
            }
            else
            {
                files = Directory.GetFiles(FolderPath, FileFilter);
            }            
            foreach (string FilePath in files)
            {
                LabelXItem item = new LabelXItem { Name = (PreFix + Path.GetFileNameWithoutExtension(FilePath)) };
                //item.Name = FilePath;
                try
                {
                    if (!FilePath.EndsWith("{downloading}"))
                    {
                        item.Hash = PSLib.FilesAndFolders.GetFileHash(FilePath);
                        items.Add(item);
                    }
                }
                catch (System.IO.IOException)
                {
                    //I found this error occuring when using 3th party programs to open the file, and
                    //or deleting the file, at the right moment. Just skip for now
                    continue;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        static public void GetItemsFromFolder(string FolderPath, ref List<LabelX.Toolbox.LabelXItem> items, string PreFix)
        {
            GetItemsFromFolder(FolderPath, ref items, PreFix, string.Empty);
        }

        static public void GetPicturesFromFolderTree(string FolderPath, ref List<LabelX.Toolbox.LabelXItem> items)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            ArrayList directoryList = new ArrayList();
            DirectoryInfo folderPathInfo = new DirectoryInfo(FolderPath);
            GetSubDirectories(folderPathInfo, ref directoryList);
            foreach (DirectoryInfo dir in directoryList)
            {
                string pictureFolderFullPath = dir.FullName;
                GetPicturesFromFolder(pictureFolderFullPath, ref items);
            }
            sw.Stop();
            GlobalDataStore.Logger.Debug("Creating the XML Hash file for pictures took: " + sw.ElapsedMilliseconds + " ms (" + FolderPath + ")");
            sw.Reset();
        }

        static public void GetPicturesFromFolder(string FolderPath, ref List<LabelX.Toolbox.LabelXItem> items)
        {
            GlobalDataStore.Logger.Debug("Creating the XML Hash file for pictures " + FolderPath);
            if (!Directory.Exists(FolderPath))
            {
                return;
            }
            string[] files = Directory.GetFiles(FolderPath);
            foreach (string FilePath in files)
            { //Get only the .jpg and .bmp files mve,1.3.2
                if (FilePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || 
                    FilePath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                    FilePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                    FilePath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) 
                   )
                {
                    LabelXItem item = new LabelXItem { Name = FilePath };
                    try
                    {
                        item.Hash = PSLib.FilesAndFolders.GetFileHash(FilePath);
                        items.Add(item);
                    }
                    catch (System.IO.IOException)
                    {
                        //I found this error occuring when using 3th party programs to open the file, and
                        //or deleting the file, at the right moment. Just skip for now
                        continue;
                    }
                }
            }
        }

        static public void GetSubDirectories(DirectoryInfo folder, ref ArrayList directoryList)
        {
            DirectoryInfo[] dirs = folder.GetDirectories();

            if (dirs.Length > 0)
            {
                foreach (DirectoryInfo dir in dirs)
                {
                    try
                    {
                        GetSubDirectories(dir, ref directoryList);

                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                }
            }
            directoryList.Add(folder);
        }

        static public bool VerifyDirectory(string FolderPath)
        {
            bool bRet = true;
            if (Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                }
                catch (Exception)
                {
                    bRet = false;
                }
            }
            return bRet;
        }
        static public void GetFilesFromFolderTree(string FolderPath, ref List<LabelX.Toolbox.LabelXItem> items)
        {            
            ArrayList directoryList = new ArrayList();
            DirectoryInfo folderPathInfo = new DirectoryInfo(FolderPath);
            GetSubDirectories(folderPathInfo, ref directoryList);
            foreach (DirectoryInfo dir in directoryList)
            {
                string FolderFullPath = dir.FullName;
                GetFilesFromFolder(FolderFullPath, ref items);
            }
        }
        static public void GetFilesFromFolder(string FolderPath, ref List<LabelX.Toolbox.LabelXItem> items)
        {
             
            if (!Directory.Exists(FolderPath))
            {
                return;
            }
            string[] files = Directory.GetFiles(FolderPath);
            foreach (string FilePath in files)
            {
                LabelXItem item = new LabelXItem { Name = FilePath };
                try
                {
                    item.Hash = PSLib.FilesAndFolders.GetFileHash(FilePath);
                    items.Add(item);
                }
                catch (System.IO.IOException)
                {
                    continue;
                }
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Warning("Unable to hash file " + FilePath + ". " + e.Message);
                }
            }
        }

        static public void GetChangedItems(List<LabelXItem> LocalItems, List<LabelXItem> RemoteItems, ref List<LabelXItem> ChangedItems)
        {

            GlobalDataStore.Logger.Debug("Comparing local an remote items by name and hash.");

            Dictionary<string, LabelXItem> LocalItemDict = new Dictionary<string, LabelXItem>();
            Dictionary<string, LabelXItem> RemoteItemDict = new Dictionary<string, LabelXItem>();
            foreach (LabelXItem localItem in LocalItems)
            {
                string localItemName = localItem.Name;                
                LocalItemDict.Add(localItemName, localItem);
            }
            foreach (LabelXItem remoteItem in RemoteItems)
            {
                string remoteItemName = remoteItem.Name;                
                RemoteItemDict.Add(remoteItemName, remoteItem);
            }

            foreach (KeyValuePair<string, LabelXItem> it in LocalItemDict)
            {
                if (RemoteItemDict.ContainsKey(it.Key))
                {
                    LabelXItem remItem = RemoteItemDict[it.Key];
                    if (remItem.Hash != it.Value.Hash)
                    {
                        ChangedItems.Add(remItem);
                    }
                }
            }
            LocalItemDict.Clear();
            RemoteItemDict.Clear();
            /*
            foreach (LabelXItem localItem in LocalItems)
            {
                foreach (LabelXItem remoteItem in RemoteItems)
                {
                    if (localItem.Name == remoteItem.Name &&
                        localItem.Hash != remoteItem.Hash)
                    {
                        ChangedItems.Add(remoteItem);
                        break;
                    }
                }
            }
            */
        }

        static public void GetAddedItems(List<LabelXItem> LocalItems, List<LabelXItem> RemoteItems, ref List<LabelXItem> AddedItems)
        {
            Dictionary<string, LabelXItem> LocalItemDict = new Dictionary<string, LabelXItem>();
            Dictionary<string, LabelXItem> RemoteItemDict = new Dictionary<string, LabelXItem>();
            foreach (LabelXItem localItem in LocalItems)
            {
                string localItemName = localItem.Name;               
                LocalItemDict.Add(localItemName, localItem);
            }
            foreach (LabelXItem remoteItem in RemoteItems)
            {
                string remoteItemName = remoteItem.Name;                
                RemoteItemDict.Add(remoteItemName, remoteItem);
            }
            foreach (KeyValuePair<string, LabelXItem> it in RemoteItemDict)
            {
                if (!LocalItemDict.ContainsKey(it.Key))
                {
                    AddedItems.Add(it.Value);
                }
            }
            LocalItemDict.Clear();
            RemoteItemDict.Clear();
            /*
            foreach (LabelXItem remoteItem in RemoteItems)
            {
                bool Found = false;
                foreach (LabelXItem localItem in LocalItems)
                {
                    if (localItem.Name == remoteItem.Name)
                    {
                        Found = true;
                        break;
                    }
                }

                if (false == Found)
                {
                    AddedItems.Add(remoteItem);
                }
            }*/
        }

        static public void GetDeletedItems(List<LabelXItem> LocalItems, List<LabelXItem> RemoteItems, ref List<LabelXItem> DeletedItems)
        {
            Dictionary<string, LabelXItem> LocalItemDict = new Dictionary<string, LabelXItem>();
            Dictionary<string, LabelXItem> RemoteItemDict = new Dictionary<string, LabelXItem>();
            foreach (LabelXItem localItem in LocalItems)
            {
                string localItemName = localItem.Name;                
                LocalItemDict.Add(localItemName, localItem);
            }
            foreach (LabelXItem remoteItem in RemoteItems)
            {
                string remoteItemName = remoteItem.Name;                
                RemoteItemDict.Add(remoteItemName, remoteItem);
            }

            foreach (KeyValuePair<string, LabelXItem> it in LocalItemDict)
            {
                if (!RemoteItemDict.ContainsKey(it.Key))
                {
                    DeletedItems.Add(it.Value);
                }
            }
            LocalItemDict.Clear();
            RemoteItemDict.Clear();
            /*
            foreach (LabelXItem localItem in LocalItems)
            {
                bool Found = false;
                foreach (LabelXItem remoteItem in RemoteItems)
                {
                    if (localItem.Name == remoteItem.Name)
                    {
                        Found = true;
                        break;
                    }
                }

                if (false == Found)
                {
                    DeletedItems.Add(localItem);
                }
            }
             */
        }

        static public void GetChangedFiles(List<LabelXItem> LocalItems, List<LabelXItem> RemoteItems, ref List<LabelXItem> ChangedItems, string remoteFolder, string localFolder)
        {
            GlobalDataStore.Logger.Debug("Comparing local an remote items by name and hash.");

            Dictionary<string, LabelXItem> LocalItemDict = new Dictionary<string, LabelXItem>();
            Dictionary<string, LabelXItem> RemoteItemDict = new Dictionary<string, LabelXItem>();
            foreach (LabelXItem localItem in LocalItems)
            {
                string localItemName = localItem.Name;
                localItemName = localItemName.Replace(localFolder, "");
                LocalItemDict.Add(localItemName, localItem);
            }
            foreach (LabelXItem remoteItem in RemoteItems)
            {
                string remoteItemName = remoteItem.Name;
                remoteItemName = remoteItemName.Replace(remoteFolder, "");
                RemoteItemDict.Add(remoteItemName, remoteItem);
            }

            foreach (KeyValuePair<string, LabelXItem> it in LocalItemDict)
            {
                if (RemoteItemDict.ContainsKey(it.Key))
                {
                    LabelXItem remItem = RemoteItemDict[it.Key];
                    if (remItem.Hash != it.Value.Hash)
                    {
                        ChangedItems.Add(remItem);
                    }
                }
            }
            LocalItemDict.Clear();
            RemoteItemDict.Clear();

            /*
            foreach (LabelXItem localItem in LocalItems)
            {
                foreach (LabelXItem remoteItem in RemoteItems)
                {
                    string localItemName = localItem.Name;
                    string remoteItemName = remoteItem.Name;
                    localItemName = localItemName.Replace(localFolder, "");
                    remoteItemName = remoteItemName.Replace(remoteFolder, "");

                    if (localItemName.Equals(remoteItemName, StringComparison.OrdinalIgnoreCase) &&
                        localItem.Hash != remoteItem.Hash)
                    {
                        GlobalDataStore.Logger.Info(string.Format("Difference found for {0}", remoteItem.Name));
                        ChangedItems.Add(remoteItem);
                        break;
                    }
                }
            }
             */
        }

        static public void GetAddedFiles(List<LabelXItem> LocalItems, List<LabelXItem> RemoteItems, ref List<LabelXItem> AddedItems, string remoteFolder, string localFolder)
        {
            Dictionary<string, LabelXItem> LocalItemDict = new Dictionary<string,LabelXItem>();
            Dictionary<string, LabelXItem> RemoteItemDict = new Dictionary<string, LabelXItem>();
            foreach (LabelXItem localItem in LocalItems)
            {
                string localItemName = localItem.Name;
                localItemName = localItemName.Replace(localFolder, "");
                LocalItemDict.Add(localItemName, localItem);
            }
            foreach (LabelXItem remoteItem in RemoteItems)
            {
                string remoteItemName = remoteItem.Name;
                remoteItemName = remoteItemName.Replace(remoteFolder, "");
                RemoteItemDict.Add(remoteItemName, remoteItem);
            }
            foreach (KeyValuePair<string,LabelXItem> it in RemoteItemDict)
            {
                if (!LocalItemDict.ContainsKey(it.Key))
                {
                    AddedItems.Add(it.Value);
                }
            }
            LocalItemDict.Clear();
            RemoteItemDict.Clear();
            /*
            foreach (LabelXItem remoteItem in RemoteItems)
            {
                bool Found = false;
                foreach (LabelXItem localItem in LocalItems)
                {
                    string localItemName = localItem.Name;
                    string remoteItemName = remoteItem.Name;
                    localItemName = localItemName.Replace(localFolder, "");
                    remoteItemName = remoteItemName.Replace(remoteFolder, "");

                    if (localItemName.Equals(remoteItemName, StringComparison.OrdinalIgnoreCase))
                    {
                        Found = true;
                        break;
                    }
                }

                if (false == Found)
                {
                    AddedItems.Add(remoteItem);
                }
            }
             * */
        }

        static public void GetDeletedFiles(List<LabelXItem> LocalItems, List<LabelXItem> RemoteItems, ref List<LabelXItem> DeletedItems, string remoteFolder, string localFolder)
        {

            Dictionary<string, LabelXItem> LocalItemDict = new Dictionary<string, LabelXItem>();
            Dictionary<string, LabelXItem> RemoteItemDict = new Dictionary<string, LabelXItem>();
            foreach (LabelXItem localItem in LocalItems)
            {
                string localItemName = localItem.Name;
                localItemName = localItemName.Replace(localFolder, "");
                LocalItemDict.Add(localItemName, localItem);
            }
            foreach (LabelXItem remoteItem in RemoteItems)
            {
                string remoteItemName = remoteItem.Name;
                remoteItemName = remoteItemName.Replace(remoteFolder, "");
                RemoteItemDict.Add(remoteItemName, remoteItem);
            }

            foreach (KeyValuePair<string, LabelXItem> it in LocalItemDict)
            {
                if (!RemoteItemDict.ContainsKey(it.Key))
                {
                    DeletedItems.Add(it.Value);
                }
            }
            LocalItemDict.Clear();
            RemoteItemDict.Clear();
        /* 
            foreach (LabelXItem localItem in LocalItems)
            {
                bool Found = false;
                foreach (LabelXItem remoteItem in RemoteItems)
                {
                    string localItemName = localItem.Name;
                    string remoteItemName = remoteItem.Name;
                    localItemName = localItemName.Replace(localFolder, "");
                    remoteItemName = remoteItemName.Replace(remoteFolder, "");

                    if (!localItemName.Equals(remoteItemName, StringComparison.OrdinalIgnoreCase)) continue;
                    Found = true;
                    break;
                }

                if (false == Found)
                {
                    DeletedItems.Add(localItem);
                }
            }
        */
        }

        public static bool StorePrinter(string sXMLFile, PrintGroupItem item, PrinterItem pi)
        {
            XmlDocument theDoc = new XmlDocument();
            lock (GlobalDataStore.LockClass)
            {
                theDoc.Load(sXMLFile);

                XmlNode theNode = theDoc.SelectSingleNode("/configuration/general-settings/print-groups/print-group[@name='" + item.Name + "']/printer[@name='" + pi.LongName + "']");
                if (theNode == null) // did not exists
                {
                    theNode = theDoc.SelectSingleNode("/configuration/general-settings/print-groups/print-group[@name='" + item.Name + "']");
                    if (theNode != null)
                    {
                        XmlElement newEl = theDoc.CreateElement("printer");
                        XmlAttribute newName = theDoc.CreateAttribute("name");
                        newName.Value = pi.LongName;
                        XmlAttribute newEnabled = theDoc.CreateAttribute("enabled");
                        newEnabled.Value = pi.Enabled ? "true" : "false";
                        if (pi.Trays.Count > 0)
                        {
                            foreach (PrinterTrayItem ptit in pi.Trays)
                            {
                                XmlElement traysElement = theDoc.CreateElement("tray");

                                XmlAttribute tn = theDoc.CreateAttribute("name");
                                tn.Value = ptit.TrayName;
                                XmlAttribute pt = theDoc.CreateAttribute("papertype");
                                pt.Value = ptit.CurrentPapertypeName;

                                traysElement.Attributes.Append(tn);
                                traysElement.Attributes.Append(pt);

                                newEl.AppendChild(traysElement);
                            }
                        }
                        newEl.Attributes.Append(newName);
                        newEl.Attributes.Append(newEnabled);
                        theNode.AppendChild(newEl);
                    }
                }
                else
                {
                    if (theNode.Attributes.GetNamedItem("enabled") != null)
                    {
                        theNode.Attributes["enabled"].Value = pi.Enabled ? "true" : "false";
                    }
                    else
                    {
                        XmlAttribute newEnabled = theDoc.CreateAttribute("enabled");
                        newEnabled.Value = pi.Enabled ? "true" : "false";
                        theNode.Attributes.Append(newEnabled);
                    }

                    //Store the trays and papertypes
                    foreach (PrinterTrayItem ptit in pi.Trays)
                    {
                        XmlNode TrayNode = theDoc.SelectSingleNode("/configuration/general-settings/print-groups/print-group[@name='" + item.Name + "']/printer[@name='" + pi.LongName + "']/tray[@name='" + ptit.TrayName + "']");
                        if (TrayNode == null)
                        {
                            //This tray was not set for this printer
                            XmlElement traysElement = theDoc.CreateElement("tray");

                            XmlAttribute tn = theDoc.CreateAttribute("name");
                            tn.Value = ptit.TrayName;
                            XmlAttribute pt = theDoc.CreateAttribute("papertype");
                            pt.Value = ptit.CurrentPapertypeName;

                            traysElement.Attributes.Append(tn);
                            traysElement.Attributes.Append(pt);

                            theNode.AppendChild(traysElement);
                        }
                        else
                        {
                            //The Tray has been found... update the papertype
                            if (TrayNode.Attributes.GetNamedItem("papertype") != null)
                            {
                                TrayNode.Attributes["papertype"].Value = ptit.CurrentPapertypeName;
                            }
                            else
                            {
                                XmlAttribute newPt = theDoc.CreateAttribute("papertype");
                                newPt.Value = ptit.CurrentPapertypeName;
                                TrayNode.Attributes.Append(newPt);
                            }
                        }
                    }
                }

                //Remove nodes for trays that are in XML but are not in the traylist anymore...
                XmlNodeList nl = theDoc.SelectNodes("/configuration/general-settings/print-groups/print-group[@name='" + item.Name + "']/printer[@name='" + pi.LongName + "']/tray");
                if (nl != null)
                    foreach (XmlNode nd in nl)
                    {
                        string sTrayName = nd.Attributes["name"].Value;
                        bool bGevonden = false;
                        foreach (PrinterTrayItem t in pi.Trays)
                        {
                            if (t.TrayName.Equals(sTrayName, StringComparison.OrdinalIgnoreCase))
                            {
                                bGevonden = true;
                                break;
                            }
                        }
                        if (!bGevonden)
                        {
                            //Remove this node
                            nd.ParentNode.RemoveChild(nd);
                        }
                    }

                /*
                //XPathDocument theDoc;
                //theDoc = new XPathDocument(sXMLFile);
                XPathNavigator nav;
                nav = theDoc.CreateNavigator();
            
                XPathNodeIterator theNode;
                theNode = nav.Select("/configuration/general-settings/print-groups/print-group[@name='" + item.Name + "']/printer[@name='" + pi.Name + "']");
                if (theNode.MoveNext())
                {
                    //update settings if different
                }
                else
                {
                    theNode = nav.Select("/configuration/general-settings/print-groups/print-group[@name='" + item.Name + "']");
                    if (theNode != null)
                    {
                        theNode.MoveNext();
                        theNode.Current.PrependChildElement(nav.Prefix, "printer", nav.LookupNamespace(nav.Prefix),string.Empty);
                        theNode.Current.MoveToFirstChild();
                        theNode.Current.CreateAttribute(nav.Prefix, "name", nav.LookupNamespace(nav.Prefix), pi.Name);
                        theNode.Current.CreateAttribute(nav.Prefix, "enabled", nav.LookupNamespace(nav.Prefix), pi.Enabled? "true":"false");
                        theNode.Current.CreateAttribute(nav.Prefix, "papertype", nav.LookupNamespace(nav.Prefix), pi.CurrentPapertypeName);
                    
                    }
                }
                //Open the XML file for Write,
                //search for the right path
                //add the printer
                //throw new NotImplementedException();
                 */
                theDoc.Save(sXMLFile);
            }
            return true;
        }

        public static bool RemovePrinterFromPrinterGroup(string sXMLFile, PrintGroupItem it, string PrinterName)
        {
            XmlDocument theDoc;
            bool bRet = false;
            theDoc = new XmlDocument();

            lock (GlobalDataStore.LockClass)
            {
                theDoc.Load(sXMLFile);

                XmlNode theNode = theDoc.SelectSingleNode("/configuration/general-settings/print-groups/print-group[@name='" + it.Name + "']/printer[@name='" + PrinterName + "']");
                if (theNode != null)
                {
                    theNode.ParentNode.RemoveChild(theNode);
                    theDoc.Save(sXMLFile);
                    bRet = true;
                }
            }
            return bRet;
        }

        public PrintJobInfos GetPrintjobsForPrintgroup(string GeneralConfigFilePath, PrintGroupItem pgi)
        {
            PrintJobInfos theInfos;
            XPathDocument theDoc;
            XPathNavigator nav = null;
            string PrintJobsRootFolder;

            theInfos = new PrintJobInfos();

            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            GetConfiguraton(GeneralConfigFilePath, "ACALabelXClient.config.xsd", ref dictionary);

            PrintJobsRootFolder = dictionary["/configuration/general-settings/folders/PrintJobsRootFolder.0"];

            List<LabelXItem> itemList;
            itemList = new List<LabelXItem>();

            Toolbox.GetItemsFromFolder(PrintJobsRootFolder + @"\" + pgi.Name, ref itemList, string.Empty, LabelX.Toolbox.Toolbox.FileFilterXML);

            foreach (LabelXItem item in itemList)
            {

                //We hebben nu elke printjob in de directory
                //deze proberen we te openen om de basisgegevens in te lezen.
                string sXMLFile;
                Int64 theSize;
                bool bRemoveFile = false;
                sXMLFile = PrintJobsRootFolder + @"\" + pgi.Name + @"\" + item.Name + ".XML";

                theSize = 0;
                if (File.Exists(sXMLFile))
                {
                    FileInfo fi = new FileInfo(sXMLFile);
                    theSize = fi.Length;
                }
                try
                {
                    theDoc = new XPathDocument(sXMLFile);
                    nav = theDoc.CreateNavigator();
                }
                catch (Exception e1)
                {
                    GlobalDataStore.Logger.Error(string.Format("Printjob {0}: {1}", sXMLFile, e1.Message)); 
                    //Hier moet nog een goede afhandeling komen van deze melding. Momenteel kan de controler
                    //enkel objecten van het type printjob tonen. Maar dit is een fout bestand, dus kunnen we geen
                    //printjob maken. Hoe moeten we dan aangeven dat het bestand gaat verdwijnen omdat het stuk is...
                    //de controler zou ook een lijst van fouten moeten kunnen tonen...
                    bRemoveFile = true;                    
                }
                if (bRemoveFile)
                {
                    bRemoveFile = false;
                    try
                    {
                        GlobalDataStore.Logger.Error(string.Format("Removing printjob {0}. It contains invalid XML", sXMLFile));
                        File.Delete(sXMLFile);
                    }
                    catch
                    {
                        //nop;
                    }
                    continue;
                }
                PrintJobInfo theInfo;
                theInfo = new PrintJobInfo();
                theInfo.FullFilename = sXMLFile;
                theInfo.Size = theSize;
                XPathNodeIterator it = nav.Select("/printjob");
                it.MoveNext();
                theInfo.ID = it.Current.GetAttribute("id", string.Empty);
                theInfo.From = it.Current.GetAttribute("from", string.Empty);
                theInfo.User = it.Current.GetAttribute("user", string.Empty);

                it = nav.Select("/printjob/destination/printqueue");
                if (it.MoveNext())
                {
                    theInfo.MachineName = it.Current.Value;
                }

                it = nav.Select("/printjob/destination/printgroup");
                if (it.MoveNext())
                {
                    theInfo.PrintGroup = it.Current.Value;
                }

                it = nav.Select("/printjob/destination/labeltype");
                if (it.MoveNext())
                {
                    theInfo.LabelType = it.Current.Value;
                }

                it = nav.Select("/printjob/destination/printedto");
                if (it.MoveNext())
                {
                    try
                    {
                        theInfo.PrintedTo = it.Current.Value;
                    }
                    catch { }
                }

                it = nav.Select("/printjob/destination/lastprinted");
                if (it.MoveNext())
                {
                    try
                    {
                        theInfo.LastPrinted = it.Current.ValueAsDateTime;
                    }
                    catch{}
                }

                it = nav.Select("/printjob/createtime");
                if (it.MoveNext())
                {
                    string datetimestr = it.Current.InnerXml;
                    //if (datetimestr.EndsWith("z", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    datetimestr.Remove(datetimestr.Length - 1);
                    //}
                    theInfo.CreationDateTime = DateTime.Parse(datetimestr);
                    //theInfo.CreationDateTime = it.Current.ValueAsDateTime;
                }

                it = nav.Select("/printjob/labels/numberoflabels");
                if (it.MoveNext())
                {
                    try
                    {
                        theInfo.NumberOfLabels = it.Current.ValueAsInt;
                    }
                    catch (Exception ee1)
                    {
                        //Skip this printjob
                        GlobalDataStore.Logger.Error(string.Format("Skipped Printjob {0}: {1}",sXMLFile,ee1.Message));
                        continue;
                    }
                }

                it = nav.Select("/printjob/description");
                if (it.MoveNext())
                {
                    theInfo.Description = it.Current.Value;
                }

                it = nav.Select("/printjob/autorelease");
                if (it.MoveNext())
                {
                    theInfo.AutoRelease = it.Current.ValueAsBoolean;
                }

                it = nav.Select("/printjob/languages/language");
                while (it.MoveNext())
                {
                    PrintLanguage thelang;
                    thelang = new PrintLanguage();
                    string sVal;
                    sVal = it.Current.GetAttribute("id", string.Empty);
                    thelang.Id = int.Parse(sVal);
                    theInfo.SupportedLanguages.Add(thelang);
                }

                theInfos.Add(theInfo);
            }
            return theInfos;
        }

        public static bool StorePrinterGroupStatus(string sXMLFile, PrintGroupItem theItem)
        {
            bool bRet = false;
            XmlDocument theDoc;
            theDoc = new XmlDocument();

            lock (GlobalDataStore.LockClass)
            {

                theDoc.Load(sXMLFile);
                XmlNode theNode = theDoc.SelectSingleNode("/configuration/general-settings/print-groups/print-group[@name='" + theItem.Name + "']");
                if (theNode != null)
                {
                    if (theNode.Attributes.GetNamedItem("enabled") != null)
                    {
                        theNode.Attributes["enabled"].Value = theItem.Enabled ? "true" : "false";
                        bRet = true;
                    }
                    else
                    {
                        XmlAttribute newEnabled = theDoc.CreateAttribute("enabled");
                        newEnabled.Value = theItem.Enabled ? "true" : "false";
                        theNode.Attributes.Append(newEnabled);
                        bRet = true;
                    }
                    theDoc.Save(sXMLFile);
                }
            }
            return bRet;
        }

        public static bool RemovePrintpool(string sXMLFile, string sPoolName)
        {
            XmlDocument theDoc;
            bool bRet = false;
            theDoc = new XmlDocument();
            lock (GlobalDataStore.LockClass)
            {

                theDoc.Load(sXMLFile);
                XmlNode theNode = theDoc.SelectSingleNode("/configuration/general-settings/print-groups/print-group[@name='" + sPoolName + "']");
                if (theNode != null)
                {
                    theNode.ParentNode.RemoveChild(theNode);
                    theDoc.Save(sXMLFile);
                    bRet = true;
                }
            }
            return bRet;
        }

        public static bool AddPrintpool(string RemotingConfigFilePath, string p)
        {
            string PrintJobsRootFolder;
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            Toolbox tb;
            tb = new Toolbox();
            tb.GetConfiguraton(RemotingConfigFilePath, "ACALabelXClient.config.xsd", ref dictionary);
            PrintJobsRootFolder = dictionary["/configuration/general-settings/folders/PrintJobsRootFolder.0"];

            //Step 1: Check if the printerpool does not already exist.
            XmlDocument theDoc;
            bool bRet = false;
            theDoc = new XmlDocument();
            lock (GlobalDataStore.LockClass)
            {

                theDoc.Load(RemotingConfigFilePath);
                XmlNode theNode = theDoc.SelectSingleNode("/configuration/general-settings/print-groups/print-group[@name='" + p + "']");
                if (theNode != null)
                {
                    //The node already existed. Cannot create duplicate.
                    bRet = false;
                }
                else
                {
                    theNode = theDoc.SelectSingleNode("/configuration/general-settings/print-groups");
                    if (theNode != null)
                    {
                        XmlElement newEl = theDoc.CreateElement("print-group");
                        XmlAttribute newName = theDoc.CreateAttribute("name");
                        newName.Value = p;
                        XmlAttribute newEnabled = theDoc.CreateAttribute("enabled");
                        newEnabled.Value = "true";

                        newEl.Attributes.Append(newName);
                        newEl.Attributes.Append(newEnabled);
                        theNode.AppendChild(newEl);

                        if (!VerifyDirectory(PrintJobsRootFolder + @"\" + p))
                        {
                            bRet = false;
                        }
                        else
                        {
                            bRet = true;
                        }
                    }
                }
                theDoc.Save(RemotingConfigFilePath);
            }
            return bRet;
        }

        public static string getSelectedIdFromTreeView(System.Windows.Forms.TreeView treeview)
        {
            string tempstring = treeview.SelectedNode.FullPath;
            return tempstring.Split('(')[0].Trim();
        }
        public static string getSelectedValueFromTreeView(System.Windows.Forms.TreeView treeview)
        {
            try
            {
                string tempstring = treeview.SelectedNode.FullPath;
                string id = tempstring.Split('(')[0].Trim();
                return tempstring.Substring(id.Length + 2, tempstring.Length - id.Length - 3).Trim();
            }
            catch
            {
                return "";
            }
        }

        public static string getSelectedIdFromListBox(System.Windows.Forms.ListBox listbox)
        {
                string tempstring = listbox.SelectedItem.ToString();
                return tempstring.Split('(')[0].Trim();
        }

        public static string getSelectedValueFromListBox(System.Windows.Forms.ListBox listbox)
        {
            try
            {
                string tempstring = listbox.SelectedItem.ToString();
                string id = tempstring.Split('(')[0].Trim();
                return tempstring.Substring(id.Length + 2, tempstring.Length - id.Length - 3).Trim();
            }
            catch
            {
                return "";
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
