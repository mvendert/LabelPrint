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
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using ACA.LabelX.Toolbox;
using System.Security;

namespace ACA.LabelX
{
    public class LabelXRemoteObjectException : ApplicationException
    {
        public LabelXRemoteObjectException()
        {
        }
        public LabelXRemoteObjectException(string message)
            : base(message)
        {
        }
        public LabelXRemoteObjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ClientMachine
    {
        public string Name;
        public string Address;
        public double ProgramVersion;

        public ClientMachine(string Name, string Address, double ProgramVersion)
        {
            this.Name = Name;
            this.Address = Address;
            this.ProgramVersion = ProgramVersion;
        }
    }

    public class RemoteObject : MarshalByRefObject
    {
        private string LabelDefinitionsRootFolder;
        private string PrintJobsRootFolder;
        private string PaperDefinitionsRootFolder;
        private string SettingsRootFolder;
        private string PicturesRootFolder;
        private string UpdateRootFolder;
        readonly IDictionary<string, ClientMachine> ClientList;

        public RemoteObject()
        {
            ClientList = new Dictionary<string, ClientMachine>();
            SelfInit();
        }
        //~RemoteObject()
        //{
        //}

        public void SelfInit()
        {
            GlobalDataStore.Logger.Debug(string.Format("Object is executing in AppDomain {0}", AppDomain.CurrentDomain.FriendlyName));

            string AppPath = GlobalDataStore.AppPath;
            string confPath = AppPath + @"\ACALabelXServer.config.xml";

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            string pj, ld, pd, sd, pf, uf;

            toolbox.GetGeneralServerConfiguraton(confPath, out pj, out ld, out pd, out sd, out pf, out uf);
            PrintJobsRootFolder = pj;
            LabelDefinitionsRootFolder = ld;
            PaperDefinitionsRootFolder = pd;
            SettingsRootFolder = sd;
            PicturesRootFolder = pf;
            UpdateRootFolder = uf;
        }

        public void InitServer(string iPrintJobsRootFolder, string iLabelDefinitionsRootFolder, string iPaperDefinitionsRootFolder, string iSettingsRootFolder, string iUpdateRootFolder)
        {
            GlobalDataStore.Logger.Debug("RemoteObject.InitServer");
            LabelDefinitionsRootFolder = iLabelDefinitionsRootFolder;
            PrintJobsRootFolder = iPrintJobsRootFolder;
            PaperDefinitionsRootFolder = iPaperDefinitionsRootFolder;
            SettingsRootFolder = iSettingsRootFolder;
            UpdateRootFolder = iUpdateRootFolder;

            if (!Directory.Exists(iPrintJobsRootFolder))
                throw new LabelXRemoteObjectException(string.Format("PrintJobsRootFolder doesn't exist: \"{0}\"", iPrintJobsRootFolder));

            if (!Directory.Exists(iLabelDefinitionsRootFolder))
                throw new LabelXRemoteObjectException(string.Format("LabelDefinitionsRootFolder doesn't exist: \"{0}\"", iLabelDefinitionsRootFolder));

            if (!Directory.Exists(iPaperDefinitionsRootFolder))
                throw new LabelXRemoteObjectException(string.Format("PaperDefinitionsRootFolder doesn't exist: \"{0}\"", iPaperDefinitionsRootFolder));

            if (!Directory.Exists(iSettingsRootFolder))
                throw new LabelXRemoteObjectException(string.Format("SettingsRootFolder doesn't exist: \"{0}\"", iSettingsRootFolder));

            if (!Directory.Exists(iUpdateRootFolder))
                throw new LabelXRemoteObjectException(string.Format("UpdateRootFolder doesn't exist: \"{0}\"", iUpdateRootFolder));
        }

        public bool Login(ACA.LabelX.Toolbox.LogonInfo info, out string updateNodig)
        {
            GlobalDataStore.Logger.Debug(info.MachineName + ".Login");
            if (!ClientList.ContainsKey(info.MachineName))
            {
                ClientList.Add(info.MachineName, new ClientMachine(info.MachineName, info.IpNumber.ToString(), info.ProgramVersion));
            }
            bool bRet = RemoteObjectHelper.HandleServerLogin(info, out updateNodig);
            return bRet;
        }

        //private void Upload(byte[] CompressedData, int UncompressedDataLength, string FilePath)
        //{
        //    GlobalDataStore.Logger.Debug("RemoteObject.Upload");

        //    byte[] Data = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);

        //    try
        //    {
        //        FileStream outfile = new FileStream(FilePath, FileMode.Open, FileAccess.Write, FileShare.Write);
        //        outfile.Write(Data, 0, Data.Length);
        //        outfile.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new LabelXRemoteObjectException(string.Format("Cannot write file: {0}", FilePath), e);
        //    }
        //}

        private static void Download(string FilePath, ref byte[] CompressedData, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug("RemoteObject.Download");
            CompressedData = null;

            if (!File.Exists(FilePath))
                throw new LabelXRemoteObjectException(string.Format("Cannot find file: {0}", FilePath));

            // Open the file as a FileStream object.
            FileStream infile = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[infile.Length];
            // Read the file to ensure it is readable.
            int count = infile.Read(buffer, 0, buffer.Length);
            if (count != buffer.Length)
            {
                infile.Close();
                throw new LabelXRemoteObjectException(string.Format("Unable to read data from file: {0}", FilePath));
            }
            infile.Close();

            UncompressedDataLength = buffer.Length;
            CompressedData = PSLib.Compression.Compress(buffer);
        }

        public void DownloadPaperDefinition(string PaperType, ref byte[] CompressedData, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug(string.Format("RemoteObject.DownloadPaperDefinition {0}", PaperType));
            string Filepath = PaperDefinitionsRootFolder + PaperType + ".xml";
            Download(Filepath, ref CompressedData, ref UncompressedDataLength);
        }

        public void DownloadLabelDefinition(string LabelName, ref byte[] CompressedData, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug(string.Format("RemoteObject.DownloadLabelDefinition {0}", LabelName));
            string Filepath = LabelDefinitionsRootFolder + LabelName + ".xml";
            Download(Filepath, ref CompressedData, ref UncompressedDataLength);
        }

        public void DownloadSetting(string SettingName, ref byte[] CompressedData, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug(string.Format("RemoteObject.DownloadSetting {0}", SettingName));
            string Filepath = SettingsRootFolder + SettingName + ".xml";
            Download(Filepath, ref CompressedData, ref UncompressedDataLength);
        }

        public void DownloadPicture(string PictureName, ref byte[] CompressedData, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug(string.Format("RemoteObject.DownloadPicture {0}", PictureName));
            //string Filepath = PicturesRootFolder + PictureName + ".jpg";
            Download(PictureName, ref CompressedData, ref UncompressedDataLength);
        }

        public void DownloadUpdate(string UpdateName, ref byte[] CompressedData, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug(string.Format("RemoteObject.DownloadUpdate {0}", UpdateName));
            Download(UpdateName, ref CompressedData, ref UncompressedDataLength);
        }

        public void DownloadPrintJob(string MachineName, string PrintGroup, string PrintJobID, ref byte[] CompressedData, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug(string.Format(MachineName + ".DownloadPrintJob for {0}:{1}", MachineName, PrintGroup));
            string Filepath = string.Format(@"{0}{1}\{2}\{3}.xml", PrintJobsRootFolder, MachineName, PrintGroup, PrintJobID);
            Download(Filepath, ref CompressedData, ref UncompressedDataLength);
        }

        public void Ping(string clientName)
        {
            CallContext.GetData("ClientIP");
            GlobalDataStore.Logger.Debug(clientName + ".Ping");
        }

        /*
                private byte[] ConvertPicturesToXML(IEnumerable<LabelXItem> items, string location)
                {
                    GlobalDataStore.Logger.Debug("RemoteObject.ConvertItemsToXML");
                    XmlDocument doc = new XmlDocument();
                    XmlElement root = doc.CreateElement("LabelXItems");

                    foreach (LabelX.Toolbox.LabelXItem item in items)
                    {
                        XmlElement itemXML = doc.CreateElement("item");
                        itemXML.SetAttribute("name", item.Name);
                        itemXML.SetAttribute("hash", item.Hash);

                        root.AppendChild(itemXML);
                    }

                    doc.AppendChild(root);

                    MemoryStream ms = new MemoryStream();
                    XmlTextWriter tw = new XmlTextWriter(ms, Encoding.UTF8);
                    tw.Formatting = Formatting.Indented;
                    doc.WriteContentTo(tw);
            
                    //if (location.EndsWith("Pictures\\")
                    doc.Save(location + "pictures.xml"); //send copy to harddrive = test
                    tw.Close();

                    return ms.ToArray();
                }
        */

        private static byte[] ConvertItemsToXML(IEnumerable<LabelXItem> items)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("LabelXItems");

            foreach (LabelX.Toolbox.LabelXItem item in items)
            {
                XmlElement itemXML = doc.CreateElement("item");
                itemXML.SetAttribute("name", item.Name);
                itemXML.SetAttribute("hash", item.Hash);

                root.AppendChild(itemXML);
            }

            doc.AppendChild(root);

            MemoryStream ms = new MemoryStream();
            XmlTextWriter tw = new XmlTextWriter(ms, Encoding.UTF8) { Formatting = Formatting.Indented };
            doc.WriteContentTo(tw);
            tw.Close();

            return ms.ToArray();
        }

        public void GetLabelDefinitionItems(ref byte[] labelDefinitionsXMLCompressed, ref int UncompressedDataLength, string machineName)
        {
            List<LabelX.Toolbox.LabelXItem> items = new List<LabelX.Toolbox.LabelXItem>();
            LabelX.Toolbox.Toolbox.GetItemsFromFolder(LabelDefinitionsRootFolder, ref items, "", LabelX.Toolbox.Toolbox.FileFilterXML);
            GlobalDataStore.Logger.Debug(string.Format(machineName + ".GetLabelDefinitionItems"));
            byte[] UncompressedLabelDefinitionsXML = ConvertItemsToXML(items);
            UncompressedDataLength = UncompressedLabelDefinitionsXML.Length;
            labelDefinitionsXMLCompressed = PSLib.Compression.Compress(UncompressedLabelDefinitionsXML);
        }

        public void GetPaperDefinitionItems(ref byte[] paperDefinitionsXMLCompressed, ref int UncompressedDataLength, string machineName)
        {
            GlobalDataStore.Logger.Debug(machineName + ".GetPaperDefinitionItems");
            List<LabelX.Toolbox.LabelXItem> items = new List<LabelX.Toolbox.LabelXItem>();
            LabelX.Toolbox.Toolbox.GetItemsFromFolder(PaperDefinitionsRootFolder, ref items, "",LabelX.Toolbox.Toolbox.FileFilterXML);
            byte[] UncompressedPaperDefinitionsXML = ConvertItemsToXML(items);
            UncompressedDataLength = UncompressedPaperDefinitionsXML.Length;
            paperDefinitionsXMLCompressed = PSLib.Compression.Compress(UncompressedPaperDefinitionsXML);
        }

        public void GetSettingItems(ref byte[] SettingssXMLCompressed, ref int UncompressedDataLength,string machineName)
        {
            GlobalDataStore.Logger.Debug(machineName + ".GetSettingItems");
            List<LabelX.Toolbox.LabelXItem> items = new List<LabelX.Toolbox.LabelXItem>();
            LabelX.Toolbox.Toolbox.GetItemsFromFolder(SettingsRootFolder, ref items, "", LabelX.Toolbox.Toolbox.FileFilterXML);
            byte[] UncompressedSettingsXML = ConvertItemsToXML(items);
            UncompressedDataLength = UncompressedSettingsXML.Length;
            SettingssXMLCompressed = PSLib.Compression.Compress(UncompressedSettingsXML);
        }

        //private void GetSubDirectories(DirectoryInfo folder, ref System.Collections.ArrayList directoryList)//NOG NIET AF : MOET REMOTE
        //{
        //    DirectoryInfo[] dirs = folder.GetDirectories();
        //    if (dirs.Length > 0)
        //    {
        //        foreach (DirectoryInfo dir in dirs)
        //        {
        //            try
        //            {
        //                GetSubDirectories(folder, ref directoryList);

        //            }
        //            catch (UnauthorizedAccessException)
        //            {
        //                continue;
        //            }
        //        }
        //    }
        //    directoryList.Add(folder);
        //}

        //public void GetPictureItems(ref byte[] PictureXMLCompressed, ref int UncompressedDataLength, out string remotePictureLocation)
        //{
        //    GlobalDataStore.Logger.Debug("RemoteObject.GetPictureItems");
        //    List<LabelX.Toolbox.LabelXItem> items = new List<LabelX.Toolbox.LabelXItem>();
        //    DirectoryInfo PicturesRootFolderDirectoryInfo = new DirectoryInfo(PicturesRootFolder);
        //    LabelX.Toolbox.Toolbox.GetPicturesFromFolderTree(PicturesRootFolderDirectoryInfo.FullName, ref items, "");
        //    remotePictureLocation = PicturesRootFolder;
        //    GlobalDataStore.Logger.Debug(string.Format("RemoteObject.GetPictureItems countend {0} items", items.Count));
        //    byte[] UncompressedPicturesXML = ConvertPicturesToXML(items,remotePictureLocation);
        //    UncompressedDataLength = UncompressedPicturesXML.Length;
        //    PictureXMLCompressed = PSLib.Compression.Compress(UncompressedPicturesXML);
        //}

        public void GetPictureXMLFile(ref byte[] PictureXMLCompressed, ref int UncompressedDataLength, out string remotePictureLocation,string machineName)
        {            
            GlobalDataStore.Logger.Debug(machineName + ".GetPictureXMLFile");        
            remotePictureLocation = PicturesRootFolder;
            byte[] UncompressedPicturesXML = LoadXMLFile(remotePictureLocation, "Pictures.xml");
            if (UncompressedPicturesXML != null)
            {
                UncompressedDataLength = UncompressedPicturesXML.Length;
                PictureXMLCompressed = PSLib.Compression.Compress(UncompressedPicturesXML);
            }
        }

        public void GetUpdateXMLFile(ref byte[] UpdateXMLCompressed, ref int UncompressedDataLength, out string remoteUpdateLocation, string machineName)
        {
            GlobalDataStore.Logger.Debug(machineName + ".GetUpdateXMLFile");
            remoteUpdateLocation = UpdateRootFolder;
            byte[] UncompressedUpdateXML = LoadXMLFile(remoteUpdateLocation, "Updates.xml");
            if (UncompressedUpdateXML != null)
            {
                UncompressedDataLength = UncompressedUpdateXML.Length;
                UpdateXMLCompressed = PSLib.Compression.Compress(UncompressedUpdateXML);
            }
        }

        public byte[] LoadXMLFile(string location, string filename)
        {
            try
            {
                FileStream fs = new FileStream(location + filename, FileMode.Open, FileAccess.Read);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                return data;
            }
            catch (Exception e)
            {
                GlobalDataStore.Logger.Debug("Error reading Updates.xml \n" + e.Message);
                return null;
            }
        }

        public void GetPrintJobs(string MachineName, string PrintGroup, ref byte[] printJobsXMLCompressed, ref int UncompressedDataLength)
        {
            GlobalDataStore.Logger.Debug(string.Format(MachineName + ".GetPrintJobs for {0}.{1}", MachineName, PrintGroup));
            List<LabelX.Toolbox.LabelXItem> items = new List<LabelX.Toolbox.LabelXItem>();
            string Path = string.Format(@"{0}{1}\{2}", PrintJobsRootFolder, MachineName, PrintGroup);
            LabelX.Toolbox.Toolbox.GetItemsFromFolder(Path, ref items, "", LabelX.Toolbox.Toolbox.FileFilterXML); //for PrintJobsRootFolder
            byte[] UncompressedPrintJobsXML = ConvertItemsToXML(items);
            UncompressedDataLength = UncompressedPrintJobsXML.Length;
            printJobsXMLCompressed = PSLib.Compression.Compress(UncompressedPrintJobsXML);
        }

        //mvesecurity!!
        [SecurityCritical]    
        public override Object InitializeLifetimeService()
        {
            GlobalDataStore.Logger.Debug("RemoteObject.InitializeLifetimeService");
            return null;
        }

        //mvesecurity!!
        [SecurityCritical] 
        public void RemovePrintJob(string MachineName, string jobname)
        {
            GlobalDataStore.Logger.Debug(string.Format(MachineName + ".RemovePrintJob {0}.{1}", MachineName, jobname));
            string Path = string.Format(@"{0}{1}\{2}.XML", PrintJobsRootFolder, MachineName, jobname);

            const int times = 5;
            int tel = 0;
            while ((tel < times))
            {
                try
                {
                    if (File.Exists(Path))
                        File.Delete(Path);
                    break;
                }
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Error("Remove of Printjob after successfull transfer failed: " + e.Message);
                }
                tel++;
                System.Threading.Thread.Sleep(2000);
            }
        }

        public string startUpdate(double ProgramVersion, string MachineName)
        {
            GlobalDataStore.Logger.Debug(MachineName + ".startUpdate");

            string AppPath = GlobalDataStore.AppPath;
            string confPath = AppPath + @"\ACALabelXServer.config.xml";

            lock (GlobalDataStore.LockClass)
            {
                XmlDocument theDoc = new XmlDocument();
                if (!File.Exists(confPath))
                {
                    return "";
                }
                theDoc.Load(confPath);

                XmlNode node = theDoc.SelectSingleNode("/configuration/general-settings/beginUpdate");
                if (node != null)
                {
                    Double updateVersion = Double.Parse(node.Attributes["version"].Value, CultureInfo.InvariantCulture);
                    if (ProgramVersion < updateVersion)
                    {
                        GlobalDataStore.Logger.Warning("Sending Update notice to client");
                        return node.Attributes["executable"].Value;
                    }
                }
            }
            return "";
        }

        public void updateClientXML(string MachineName, string veld, string waarde)
        {
            string confPath = SettingsRootFolder + "Clients.xml";
            //const string confPath = @"C:\acaLabelX\Server\Settings\Clients.xml";
            XmlDocument theDoc = new XmlDocument();

            if (!File.Exists(confPath)) return;
            try
            {
                theDoc.Load(confPath);
                        
                string xpath = "/clients/machine[@name='" + MachineName + "']";
                XmlNodeList lst = theDoc.SelectNodes(xpath);
                XmlNode node2;
                if (lst == null)
                {
                    node2 = null;
                }
                else
                {
                    node2 = lst.Count > 0 ? lst[0] : null;
                }
                //XmlNode node2 = theDoc.SelectSingleNode(xpath);
                if (node2 == null) return;
                node2.Attributes[veld].Value = waarde;

                theDoc.Save(confPath);

            }
            catch (Exception eeg)
            {
                GlobalDataStore.Logger.Error("Clients.xml damaged.");

                GlobalDataStore.Logger.Error(string.Format("Exception: {0}", eeg.Message));
                GlobalDataStore.Logger.Error(string.Format("Stack: {0}", eeg.StackTrace));
                if (eeg.InnerException != null)
                {
                    GlobalDataStore.Logger.Error(string.Format("Inner Exception: {0} ", eeg.InnerException.Message));
                }
            }
        }
    }

    public class RemoteObjectHelper
    {
        public static bool HandleServerLogin(ACA.LabelX.Toolbox.LogonInfo info, out string updateNodig)
        {
            updateNodig = String.Empty;

            GlobalDataStore.Logger.Debug(info.MachineName + ".HandleServerLoging");
            //The received data should be saved to the right place...
            if (Authenticate(info.MachineName, info.Username, info.Password) == false)
            {
                return false;
            }
            string AppPath = GlobalDataStore.AppPath;
            string confPath = AppPath + @"\ACALabelXServer.config.xml";

            lock (GlobalDataStore.LockClass)
            {
                XmlDocument theDoc = new XmlDocument();
                if (!File.Exists(confPath))
                {
                    return false;
                }
                theDoc.Load(confPath);
                XmlNode node0 = theDoc.SelectSingleNode("/configuration/general-settings/allowed_versions");
                if (node0 == null)
                {
                    return false;
                }
                Double minVersion = Double.Parse(node0.Attributes["begin"].Value,CultureInfo.InvariantCulture);
                Double maxVersion = Double.Parse(node0.Attributes["end"].Value, CultureInfo.InvariantCulture);

                if(( minVersion > info.ProgramVersion) || (maxVersion < info.ProgramVersion))
                {
                    return false;
                }

                XmlNode node3 = theDoc.SelectSingleNode("/configuration/general-settings/beginUpdate");
                if (node3 != null)
                {
                    Double updateVersion = Double.Parse(node3.Attributes["version"].Value, CultureInfo.InvariantCulture);                    
                    if( info.ProgramVersion < updateVersion )
                    {
                        updateNodig = node3.Attributes["executable"].Value;
                        GlobalDataStore.Logger.Warning("Sending Update notice to client");
                    }
                }

                XmlNode node4 = theDoc.SelectSingleNode("/configuration/general-settings/folders/SettingsRootFolder");
                string settingsFolder = node4.InnerText;
                if (!settingsFolder.EndsWith("\\")) settingsFolder += "\\";
                createClientXML(info, settingsFolder + "Clients.xml");

               theDoc.Save(confPath); //Is this really needed? JBOS 19-04-2013
            }
            return true;
        }

        private static void createClientXML(ACA.LabelX.Toolbox.LogonInfo info, string confPath)
        {
            XmlDocument theDoc = new XmlDocument();
            XmlNode rootNode;

            if (File.Exists(confPath))
            {
                try
                {
                    theDoc.Load(confPath);
                    rootNode = theDoc.SelectSingleNode("/clients");
                }
                catch
                {
                    //clients.xml damaged, recreate clients.xml
                    XmlDeclaration xmlDeclaration = theDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    // Create the root element
                    rootNode = theDoc.CreateElement("clients");
                    theDoc.InsertBefore(xmlDeclaration, theDoc.DocumentElement);
                    theDoc.AppendChild(rootNode);
                }
                
            }
            else
            {
                XmlDeclaration xmlDeclaration = theDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                // Create the root element
                rootNode = theDoc.CreateElement("clients");
                theDoc.InsertBefore(xmlDeclaration, theDoc.DocumentElement);
                theDoc.AppendChild(rootNode);
            }

            string xpath = "/clients/machine[@name='" + info.MachineName + "']";
            XmlNodeList lst = theDoc.SelectNodes(xpath);
            XmlNode node2;
            if (lst == null)
            {
                node2 = null;
            }
            else
            {
                node2 = lst.Count > 0 ? lst[0] : null;
            }
            //XmlNode node2 = theDoc.SelectSingleNode(xpath);
            if (node2 != null)
            {
                foreach (XmlNode chld in node2.ChildNodes)
                {
                    node2.RemoveChild(chld);
                }
                node2.Attributes["externalip"].Value = ((CallContext.GetData("ClientIP"))).ToString();
                node2.Attributes["internalip"].Value = info.IpNumber.ToString();
                node2.Attributes["description"].Value = info.MachineDescription;
                node2.Attributes["lastseen"].Value = DateTime.Now.ToString("o");
                node2.Attributes["version"].Value = info.ProgramVersion.ToString(CultureInfo.InvariantCulture);
                node2.Attributes["readyForUpdate"].Value = info.updateReady.ToString();
            }
            else
            {
                XmlElement el = theDoc.CreateElement("machine");
                el.SetAttribute("name", info.MachineName);
                el.SetAttribute("externalip", (CallContext.GetData("ClientIP")).ToString());
                el.SetAttribute("internalip", info.IpNumber.ToString());
                el.SetAttribute("description", info.MachineDescription);
                el.SetAttribute("firstseen", DateTime.Now.ToString("o"));
                el.SetAttribute("lastseen", DateTime.Now.ToString("o"));
                el.SetAttribute("version", info.ProgramVersion.ToString(CultureInfo.InvariantCulture));
                el.SetAttribute("readyForUpdate", info.updateReady.ToString());
                node2 = rootNode.AppendChild(el);
            }
            XmlElement pgsnode = theDoc.CreateElement("printgroups");

            foreach (PrintGroupItem o in info.PrintGroups)
            {
                XmlElement gnode = theDoc.CreateElement("printgroup");
                gnode.SetAttribute("name", o.Name);
                pgsnode.AppendChild(gnode);
            }
            node2.AppendChild(pgsnode);
            theDoc.Save(confPath);
        }

        private static bool Authenticate(string MachineName, string Username, string Password)
        {
            GlobalDataStore.Logger.Debug(MachineName + ".Authenticate");
            return true;
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
