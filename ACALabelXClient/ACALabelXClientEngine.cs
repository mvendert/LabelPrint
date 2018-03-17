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
using System.Globalization;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Xml;
using System.IO;
using System.Text;
using System.Runtime.Remoting;
using System.Collections;
using ACA.LabelX.Client;
using ACA.LabelX.Toolbox;

namespace ACA.LabelX.ClientEngine
{
    public class LabelXClientEngineException : ApplicationException
    {
        public LabelXClientEngineException()
        {
        }
        public LabelXClientEngineException(string message)
            : base(message)
        {
        }
        public LabelXClientEngineException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ACALabelXClientEngine
    {
        public string LabelDefinitionsRootFolder;
        public string PaperDefinitionsRootFolder;
        public string PrintJobsRootFolder;
        public string SettingsRootFolder;
        public string PicturesRootFolder;
        public string UpdateRootFolder;
        public string MachineName = "";
        public string ServerURL = "";
        public bool imgFolderChanged = true;
        public bool updFolderChanged = true;
        public List<LabelX.Toolbox.PrintGroupItem> PrintGroups;
        public int PollFrequency = 60 * 10; // 10 minutes

        enum FILESTATUS { FS_NONE, FS_DOWNLOADING };

        private bool StartCalled;

        public void EnsureStartIsCalled()
        {
            if (StartCalled == false)
                throw new LabelXClientEngineException("Not connected to the remote object, call Start first!");
        }

        public ACA.LabelX.RemoteObject GetRemoteObject()
        {
            EnsureStartIsCalled();

            ACA.LabelX.RemoteObject obj = (ACA.LabelX.RemoteObject)Activator.GetObject(typeof(ACA.LabelX.RemoteObject), ServerURL);
            if (obj == null)
                throw new RemotingException("Cannot get the remote object.");

            return obj;
        }

        public void PingRemoteServer()
        {
            GetRemoteObject().Ping(MachineName);
        }

        public void Start()
        {
            StartCalled = true;

            string AppPath = ACA.LabelX.GlobalDataStore.AppPath; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            if (!File.Exists(RemotingConfigFilePath))
                throw new LabelXClientEngineException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));

            string Protocol;
            string Address;
            string Port;
            string Uri;

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetRemotingClientConfiguraton(RemotingConfigFilePath, out Protocol, out Address, out Port, out Uri);

            Hashtable myTable = new Hashtable();
            switch (Protocol.ToLower())
            {
                case "http":
                    myTable["name"] = "servercon";
                    HttpClientChannel theHttp = new HttpClientChannel(myTable, null);
                    ChannelServices.RegisterChannel(theHttp, false);
                    break;
                case "tcp":
                    myTable["name"] = "servercon";
                    TcpClientChannel theTcp = new TcpClientChannel(myTable, null);
                    ChannelServices.RegisterChannel(theTcp, false);
                    break;
                default:
                    throw new LabelXClientEngineException(string.Format("Cannot obtain the correct protocol from the Remoting configuration file: {0}\r\nProtocol found: {1}\r\nShould be: http, or tcp", RemotingConfigFilePath, Protocol));
            }

            ServerURL = string.Format("{0}://{1}:{2}/{3}", Protocol, Address, Port, Uri);

            PrintGroups = new List<LabelX.Toolbox.PrintGroupItem>();
            toolbox.GetGeneralClientConfiguraton(AppPath + @"\ACALabelXClient.config.xml", out PrintJobsRootFolder, out LabelDefinitionsRootFolder, out PaperDefinitionsRootFolder, out SettingsRootFolder, out PicturesRootFolder, out UpdateRootFolder, out MachineName, out PollFrequency, ref PrintGroups);
        }

        private byte[] GetRemotePaperDefinition(string PaperType, ref int bufLen)
        {
            byte[] CompressedData = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().DownloadPaperDefinition(PaperType, ref CompressedData, ref UncompressedDataLength);

            byte[] UncompressedData = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);
            bufLen = UncompressedDataLength;
            return UncompressedData;
        }

        private byte[] GetRemoteLabelDefinition(string LabelName, ref int bufLen)
        {
            byte[] CompressedData = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().DownloadLabelDefinition(LabelName, ref CompressedData, ref UncompressedDataLength);

            byte[] UncompressedData = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);
            bufLen = UncompressedDataLength;
            return UncompressedData;
        }

        private byte[] GetRemoteSetting(string SettingsName, ref int bufLen)
        {
            byte[] CompressedData = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().DownloadSetting(SettingsName, ref CompressedData, ref UncompressedDataLength);

            byte[] UncompressedData = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);
            bufLen = UncompressedDataLength;
            return UncompressedData;
        }

        private byte[] GetRemotePicture(string PictureName, ref int bufLen)
        {
            byte[] CompressedData = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().DownloadPicture(PictureName, ref CompressedData, ref UncompressedDataLength);

            byte[] UncompressedData = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);
            bufLen = UncompressedDataLength;
            return UncompressedData;
        }

        private byte[] GetRemoteUpdate(string UpdateName, ref int bufLen)
        {
            byte[] CompressedData = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().DownloadUpdate(UpdateName, ref CompressedData, ref UncompressedDataLength);

            byte[] UncompressedData = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);
            bufLen = UncompressedDataLength;
            return UncompressedData;
        }

        private byte[] GetRemotePrintJob(string gMachineName, string PrintGroup, string PrintJobID, ref int bufLen)
        {
            byte[] CompressedData = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().DownloadPrintJob(gMachineName, PrintGroup, PrintJobID, ref CompressedData, ref UncompressedDataLength);

            byte[] UncompressedData = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);
            bufLen = UncompressedDataLength;
            return UncompressedData;
            //return Encoding.ASCII.GetString(UncompressedData);
        }

        private static string GetFilePathWithStatus(string FilePath, FILESTATUS FileStatus)
        {
            int StatusPos = FilePath.LastIndexOf(".{");
            if (StatusPos > -1)
                FilePath = FilePath.Remove(StatusPos);

            string NewStatus;
            switch (FileStatus)
            {
                case FILESTATUS.FS_DOWNLOADING: NewStatus = "downloading"; break;
                case FILESTATUS.FS_NONE: NewStatus = ""; break;
                default:
                    throw new ArgumentException("Unknown FileStatus parameter");
            }

            string NewFilePath = FilePath;
            if (NewStatus.Length > 0)
                NewFilePath += ".{" + NewStatus + "}";
            return NewFilePath;
        }

        public void DownloadPaperDefinition(string PaperType)
        {
            string Filepath = PaperDefinitionsRootFolder + PaperType + ".xml";
            Directory.CreateDirectory(Path.GetDirectoryName(Filepath));
            int DataLength = 0;
            byte[] Data = GetRemotePaperDefinition(PaperType, ref DataLength);

            File.Delete(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING));
            FileStream outStream;

            try
            {
                outStream = new FileStream(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING), FileMode.Create, FileAccess.Write, FileShare.Write);
                outStream.Write(Data, 0, DataLength);
                outStream.Close();
                File.Delete(GetFilePathWithStatus(Filepath, FILESTATUS.FS_NONE));
                File.Move(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING), GetFilePathWithStatus(Filepath, FILESTATUS.FS_NONE));
            }
            catch (IOException e)
            {
                GlobalDataStore.Logger.Debug("Error reading File: \n" + e.Message);
            }
        }

        public void DownloadLabelDefinition(string LabelName)
        {
            string Filepath = LabelDefinitionsRootFolder + LabelName + ".xml";
            Directory.CreateDirectory(Path.GetDirectoryName(Filepath));

            int DataLength = 0;
            byte[] Data = GetRemoteLabelDefinition(LabelName, ref DataLength);

            File.Delete(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING));
            FileStream outStream = new FileStream(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING), FileMode.Create, FileAccess.Write, FileShare.Write);

            try
            {
                outStream.Write(Data, 0, DataLength);
                outStream.Close();
                File.Delete(GetFilePathWithStatus(Filepath, FILESTATUS.FS_NONE));
                File.Move(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING), GetFilePathWithStatus(Filepath, FILESTATUS.FS_NONE));
            }
            catch (IOException e)
            {
                GlobalDataStore.Logger.Debug("Error reading File: \n" + e.Message);
            }
        }

        public void DownloadSetting(string SettingName)
        {
            string Filepath = SettingsRootFolder + SettingName + ".xml";
            Directory.CreateDirectory(Path.GetDirectoryName(Filepath));

            int DataLength = 0;
            byte[] Data = GetRemoteSetting(SettingName, ref DataLength);

            File.Delete(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING));

            try
            {
                FileStream outStream = new FileStream(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING), FileMode.Create, FileAccess.Write, FileShare.Write);
                outStream.Write(Data, 0, DataLength);
                outStream.Close();
                File.Delete(GetFilePathWithStatus(Filepath, FILESTATUS.FS_NONE));
                File.Move(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING), GetFilePathWithStatus(Filepath, FILESTATUS.FS_NONE));
            }
            catch (IOException e)
            {
                GlobalDataStore.Logger.Debug("Error reading File: \n" + e.Message);
            }
        }

        public void DownloadPicture(string ServerPictureName, string remotePicturesFolder, string localPicturesFolder)
        {
            string GeneralPictureName = ServerPictureName.Replace(remotePicturesFolder, "");
            string LocalPictureName = localPicturesFolder + GeneralPictureName;

            Directory.CreateDirectory(Path.GetDirectoryName(LocalPictureName));

            int DataLength = 0;
            byte[] Data = GetRemotePicture(ServerPictureName, ref DataLength);

            File.Delete(GetFilePathWithStatus(LocalPictureName, FILESTATUS.FS_DOWNLOADING));

            try
            {
                FileStream outStream = new FileStream(GetFilePathWithStatus(LocalPictureName, FILESTATUS.FS_DOWNLOADING), FileMode.Create, FileAccess.Write, FileShare.Write);
                outStream.Write(Data, 0, DataLength);
                outStream.Close();
                File.Delete(GetFilePathWithStatus(LocalPictureName, FILESTATUS.FS_NONE));
                File.Move(GetFilePathWithStatus(LocalPictureName, FILESTATUS.FS_DOWNLOADING), GetFilePathWithStatus(LocalPictureName, FILESTATUS.FS_NONE));
            }
            catch (IOException e)
            {
                GlobalDataStore.Logger.Debug("Error reading File: \n" + e.Message);
            }
        }

        public void DownloadUpdate(string ServerUpdateName, string remoteUpdateFolder, string localUpdateFolder)
        {
            string GeneralUpdateName = ServerUpdateName.Replace(remoteUpdateFolder, "");
            string LocalUpdateName = localUpdateFolder + GeneralUpdateName;

            Directory.CreateDirectory(Path.GetDirectoryName(LocalUpdateName));

            int DataLength = 0;
            byte[] Data = GetRemoteUpdate(ServerUpdateName, ref DataLength);

            File.Delete(GetFilePathWithStatus(LocalUpdateName, FILESTATUS.FS_DOWNLOADING));

            try
            {
                FileStream outStream = new FileStream(GetFilePathWithStatus(LocalUpdateName, FILESTATUS.FS_DOWNLOADING), FileMode.Create, FileAccess.Write, FileShare.Write);
                outStream.Write(Data, 0, DataLength);
                outStream.Close();
                File.Delete(GetFilePathWithStatus(LocalUpdateName, FILESTATUS.FS_NONE));
                File.Move(GetFilePathWithStatus(LocalUpdateName, FILESTATUS.FS_DOWNLOADING), GetFilePathWithStatus(LocalUpdateName, FILESTATUS.FS_NONE));
            }
            catch (IOException e)
            {
                GlobalDataStore.Logger.Debug("Error reading File: \n" + e.Message);
            }
        }

        public void DownloadPrintJob(string iMachineName, string PrintGroup, string PrintJobID)
        {
            string Filepath = string.Format(@"{0}{1}\{2}.xml", PrintJobsRootFolder, PrintGroup, PrintJobID);
            Directory.CreateDirectory(Path.GetDirectoryName(Filepath));

            //string Data = GetRemotePrintJob(MachineName, PrintGroup, PrintJobID);
            int DataLength = 0;
            byte[] Data = GetRemotePrintJob(iMachineName, PrintGroup, PrintJobID, ref DataLength);

            File.Delete(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING));
            try
            {
                FileStream outStream = new FileStream(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING), FileMode.Create, FileAccess.Write, FileShare.Write);

                outStream.Write(Data, 0, DataLength);
                outStream.Close();

                //TextWriter tw = new StreamWriter(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING));
                //if (tw != null)
                //{
                //    tw.Write(Data);
                //    tw.Close();
                File.Delete(GetFilePathWithStatus(Filepath, FILESTATUS.FS_NONE));
                File.Move(GetFilePathWithStatus(Filepath, FILESTATUS.FS_DOWNLOADING), GetFilePathWithStatus(Filepath, FILESTATUS.FS_NONE));
            }
            catch (IOException e)
            {
                GlobalDataStore.Logger.Debug("Error reading File: \n" + e.Message);
            }
        }

        private void GetLocalLabelDefinitionItems(ref List<Toolbox.LabelXItem> items)
        {
            LabelX.Toolbox.Toolbox.GetItemsFromFolder(LabelDefinitionsRootFolder, ref items, "", LabelX.Toolbox.Toolbox.FileFilterXML);
        }

        private void GetLocalPaperDefinitionItems(ref List<Toolbox.LabelXItem> items)
        {
            LabelX.Toolbox.Toolbox.GetItemsFromFolder(PaperDefinitionsRootFolder, ref items, "", LabelX.Toolbox.Toolbox.FileFilterXML);
        }

        private void GetLocalSettingItems(ref List<Toolbox.LabelXItem> items)
        {
            LabelX.Toolbox.Toolbox.GetItemsFromFolder(SettingsRootFolder, ref items, "", LabelX.Toolbox.Toolbox.FileFilterXML);
        }

        private void GetLocalUpdateItems(ICollection<LabelXItem> items, out string LocalUpdateFolder)
        {
            LocalUpdateFolder = UpdateRootFolder;
            if (updFolderChanged || !(System.IO.File.Exists(LocalUpdateFolder + "Updates.xml")))
            {
                WriteDirectoryXMLFile(UpdateRootFolder, "Updates.xml");
                updFolderChanged = false;
            }

            ExtractItemsFromXML(ConvertXMLtoByte(UpdateRootFolder, "Updates.xml"), items, "");
        }

        private void GetLocalPictureItems(ICollection<LabelXItem> items, out string LocalPicturesFolder)
        {
            GlobalDataStore.Logger.Debug("GetLocalPictureItems started.");
            LocalPicturesFolder = PicturesRootFolder;
            if (imgFolderChanged || !(System.IO.File.Exists(LocalPicturesFolder + "Pictures.xml")))
            {
                GlobalDataStore.Logger.Debug("Something changed. Creating new XML file for pictures.");
                WritePictureXMLFile();
                imgFolderChanged = false;
            }

            GlobalDataStore.Logger.Debug("Creating itemlist for xml file.");
            ExtractItemsFromXML(ConvertXMLtoByte(PicturesRootFolder, "Pictures.xml"), items, "");
            GlobalDataStore.Logger.Debug(string.Format("GetLocalPictureItems returned items.count {0}", items.Count));
        }

        public byte[] ConvertXMLtoByte(string location, string xmlFile)
        {
            try
            {
                FileStream fs = new FileStream(location + xmlFile, FileMode.Open, FileAccess.Read);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                return data;

            }
            catch (Exception e)
            {
                GlobalDataStore.Logger.Debug("Error reading " + xmlFile + " \n" + e.Message);
                return null;
            }

        }

        public void WritePictureXMLFile()
        {
            GlobalDataStore.Logger.Debug("Updating Pictures.xml ...");
            List<LabelX.Toolbox.LabelXItem> items = new List<LabelX.Toolbox.LabelXItem>();
            DirectoryInfo PicturesRootFolderDirectoryInfo = new DirectoryInfo(PicturesRootFolder);
            LabelX.Toolbox.Toolbox.GetPicturesFromFolderTree(PicturesRootFolderDirectoryInfo.FullName, ref items);

            //items = alle ingelezen pictures. Nu gaan wegschrijven.
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = doc.CreateElement("LabelXItems");

            foreach (LabelX.Toolbox.LabelXItem item in items)
            {
                System.Xml.XmlElement itemXML = doc.CreateElement("item");
                itemXML.SetAttribute("name", item.Name);
                itemXML.SetAttribute("hash", item.Hash);

                root.AppendChild(itemXML);
            }

            doc.AppendChild(root);

            MemoryStream ms = new MemoryStream();
            System.Xml.XmlTextWriter tw = new System.Xml.XmlTextWriter(ms, Encoding.UTF8);
            tw.Formatting = System.Xml.Formatting.Indented;
            doc.WriteContentTo(tw);
            doc.Save(PicturesRootFolder + "pictures.xml");


            tw.Close();
        }

        private static string getFileExt(string filePath)
        {
            if (filePath == null) return "";
            if (filePath.Length == 0) return "";
            if (filePath.LastIndexOf(".") == -1) return "";
            return filePath.Substring(filePath.LastIndexOf("."));
        }

        public void WriteDirectoryXMLFile(string folder, string fnaam)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            GlobalDataStore.Logger.Debug("Updating " + fnaam + " ...");
            List<LabelX.Toolbox.LabelXItem> items = new List<LabelX.Toolbox.LabelXItem>();
            DirectoryInfo FolderDirectoryInfo = new DirectoryInfo(folder);
            LabelX.Toolbox.Toolbox.GetFilesFromFolderTree(FolderDirectoryInfo.FullName, ref items);

            //items = alle ingelezen bestanden. Nu gaan wegschrijven.
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement root = doc.CreateElement("LabelXItems");

            foreach (LabelX.Toolbox.LabelXItem item in items)
            {
                if (getFileExt(item.Name) == ".xml") continue;

                System.Xml.XmlElement itemXML = doc.CreateElement("item");
                itemXML.SetAttribute("name", item.Name);
                itemXML.SetAttribute("hash", item.Hash);

                root.AppendChild(itemXML);
            }

            doc.AppendChild(root);

            MemoryStream ms = new MemoryStream();
            System.Xml.XmlTextWriter tw = new System.Xml.XmlTextWriter(ms, Encoding.UTF8) { Formatting = System.Xml.Formatting.Indented };
            doc.WriteContentTo(tw);
            doc.Save(folder + fnaam);
            tw.Close();

            sw.Stop();
            GlobalDataStore.Logger.Debug("Creating the XML Hash file for the directory took: " + sw.ElapsedMilliseconds + " ms (" + folder + ")");
            sw.Reset();
        }

        private void GetLocalPrintJobs(ref List<Toolbox.LabelXItem> items)
        {
            foreach (Toolbox.PrintGroupItem PrintGroup in PrintGroups)
            {
                LabelX.Toolbox.Toolbox.GetItemsFromFolder(PrintJobsRootFolder + PrintGroup.Name, ref items, PrintGroup.Name + @"\", LabelX.Toolbox.Toolbox.FileFilterXML);
            }
        }

        private static void ExtractItemsFromXML(byte[] XML, ICollection<LabelXItem> items, string PreFix)
        {
            MemoryStream ms = new MemoryStream(XML);
            XmlTextReader rdr = new XmlTextReader(ms);
            while (rdr.Read())
            {
                if (XmlNodeType.Element == rdr.NodeType)
                {
                    string Name = "";
                    string Hash = "";
                    while (rdr.MoveToNextAttribute())
                    {
                        switch (rdr.Name)
                        {
                            case "name": Name = PreFix + rdr.Value; break;
                            case "hash": Hash = rdr.Value; break;
                        }
                    }
                    if (Name.Length > 0)
                    {
                        Toolbox.LabelXItem item = new Toolbox.LabelXItem { Name = Name, Hash = Hash };
                        items.Add(item);
                    }
                }
            }
            rdr.Close();
        }

        private void GetRemoteLabelDefinitionItems(ICollection<LabelXItem> items)
        {
            byte[] labelDefinitionsXMLCompressed = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().GetLabelDefinitionItems(ref labelDefinitionsXMLCompressed, ref UncompressedDataLength, MachineName);
            byte[] XMLUncompressed = PSLib.Compression.Decompress(labelDefinitionsXMLCompressed, UncompressedDataLength);
            ExtractItemsFromXML(XMLUncompressed, items, "");
            GlobalDataStore.Logger.Debug(string.Format("GetRemoteLabelDefinitionItems returned items.count {0}", items.Count));
        }

        private void GetRemotePaperDefinitionItems(ICollection<LabelXItem> items)
        {
            byte[] paperDefinitionsXMLCompressed = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().GetPaperDefinitionItems(ref paperDefinitionsXMLCompressed, ref UncompressedDataLength,MachineName);
            byte[] XMLUncompressed = PSLib.Compression.Decompress(paperDefinitionsXMLCompressed, UncompressedDataLength);
            ExtractItemsFromXML(XMLUncompressed, items, "");
        }

        private void GetRemoteSettingItems(ICollection<LabelXItem> items)
        {
            byte[] settingsXMLCompressed = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().GetSettingItems(ref settingsXMLCompressed, ref UncompressedDataLength, MachineName);
            byte[] XMLUncompressed = PSLib.Compression.Decompress(settingsXMLCompressed, UncompressedDataLength);
            ExtractItemsFromXML(XMLUncompressed, items, "");
        }

        //private void GetRemoteDirectoryListing(ref List items)
        //{
        //    //byte[] settingsXMLCompressed = null;
        //    //int UncompressedDataLength = 0;
        //    GetRemoteObject().GetSettingItems(ref settingsXMLCompressed, ref UncompressedDataLength);
        //    //byte[] XMLUncompressed = PSLib.Compression.Decompress(settingsXMLCompressed, UncompressedDataLength);
        //    //ExtractItemsFromXML(XMLUncompressed, ref items, "");
        //}

        private void GetRemoteUpdateItems(ICollection<LabelXItem> items, out string RemoteUpdateFolder)
        {
            byte[] UpdatesXMLCompressed = null;
            int UncompressedDataLength = 0;
            //GetRemoteObject().GetPictureItems(ref PicturesXMLCompressed, ref UncompressedDataLength, out RemotePicturesFolder);
            GetRemoteObject().GetUpdateXMLFile(ref UpdatesXMLCompressed, ref UncompressedDataLength, out RemoteUpdateFolder,MachineName);
            byte[] XMLUncompressed = PSLib.Compression.Decompress(UpdatesXMLCompressed, UncompressedDataLength);
            ExtractItemsFromXML(XMLUncompressed, items, "");
            GlobalDataStore.Logger.Debug(string.Format("GetRemoteUpdateItems returned items.count {0}", items.Count));
        }

        private void GetRemotePictureItems(ICollection<LabelXItem> items, out string RemotePicturesFolder)
        {
            byte[] PicturesXMLCompressed = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().GetPictureXMLFile(ref PicturesXMLCompressed, ref UncompressedDataLength, out RemotePicturesFolder,MachineName);
            byte[] XMLUncompressed = PSLib.Compression.Decompress(PicturesXMLCompressed, UncompressedDataLength);
            ExtractItemsFromXML(XMLUncompressed, items, "");
            GlobalDataStore.Logger.Debug(string.Format("GetRemotePictureItems returned items.count {0}", items.Count));
        }

        private void RemoveRemotePrintJobs(Toolbox.LabelXItem item)
        {
            RemoteObject obj = GetRemoteObject();
            obj.RemovePrintJob(MachineName, item.Name);
        }

        private void GetRemotePrintJobs(ICollection<LabelXItem> items)
        {
            byte[] printJobsXMLCompressed = null;
            int UncompressedDataLength = 0;

            RemoteObject obj = GetRemoteObject();
            foreach (Toolbox.PrintGroupItem PrintGroup in PrintGroups)
            {
                obj.GetPrintJobs(MachineName, PrintGroup.Name, ref printJobsXMLCompressed, ref UncompressedDataLength);
                byte[] XMLUncompressed = PSLib.Compression.Decompress(printJobsXMLCompressed, UncompressedDataLength);
                ExtractItemsFromXML(XMLUncompressed, items, PrintGroup.Name + @"\");
            }
        }

        public void SynchronizeLabelDefinitions()
        {
            EnsureStartIsCalled();
            Directory.CreateDirectory(LabelDefinitionsRootFolder);

            List<Toolbox.LabelXItem> remoteItems = new List<Toolbox.LabelXItem>();
            GetRemoteLabelDefinitionItems(remoteItems);

            List<Toolbox.LabelXItem> localItems = new List<Toolbox.LabelXItem>();
            GetLocalLabelDefinitionItems(ref localItems);

            List<Toolbox.LabelXItem> itemsToDownload = new List<Toolbox.LabelXItem>();
            Toolbox.Toolbox.GetAddedItems(localItems, remoteItems, ref itemsToDownload);
            Toolbox.Toolbox.GetChangedItems(localItems, remoteItems, ref itemsToDownload);

            List<Toolbox.LabelXItem> itemsToDelete = new List<Toolbox.LabelXItem>();
            Toolbox.Toolbox.GetDeletedItems(localItems, remoteItems, ref itemsToDelete);

            foreach (Toolbox.LabelXItem itemToDelete in itemsToDelete)
            {
                GlobalDataStore.Logger.Info("Deleting Label Definition: " + itemToDelete.Name);
                string path = LabelDefinitionsRootFolder + itemToDelete.Name + ".xml";
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_NONE));
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_DOWNLOADING));
            }

            foreach (Toolbox.LabelXItem itemToDownload in itemsToDownload)
            {
                GlobalDataStore.Logger.Info("Downloading Label Definition: " + itemToDownload.Name);
                DownloadLabelDefinition(itemToDownload.Name);
            }
        }

        public void SynchronizePaperDefinitions()
        {
            EnsureStartIsCalled();
            Directory.CreateDirectory(PaperDefinitionsRootFolder);

            List<Toolbox.LabelXItem> remoteItems = new List<Toolbox.LabelXItem>();
            GetRemotePaperDefinitionItems(remoteItems);

            List<Toolbox.LabelXItem> localItems = new List<Toolbox.LabelXItem>();
            GetLocalPaperDefinitionItems(ref localItems);

            List<Toolbox.LabelXItem> itemsToDownload = new List<Toolbox.LabelXItem>();
            Toolbox.Toolbox.GetAddedItems(localItems, remoteItems, ref itemsToDownload);
            Toolbox.Toolbox.GetChangedItems(localItems, remoteItems, ref itemsToDownload);

            List<Toolbox.LabelXItem> itemsToDelete = new List<Toolbox.LabelXItem>();
            Toolbox.Toolbox.GetDeletedItems(localItems, remoteItems, ref itemsToDelete);

            foreach (Toolbox.LabelXItem itemToDelete in itemsToDelete)
            {
                GlobalDataStore.Logger.Info("Deleting Paper Definition: " + itemToDelete.Name);
                string path = PaperDefinitionsRootFolder + itemToDelete.Name + ".xml";
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_NONE));
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_DOWNLOADING));
            }

            foreach (Toolbox.LabelXItem itemToDownload in itemsToDownload)
            {
                GlobalDataStore.Logger.Info("Downloading Paper Definition: " + itemToDownload.Name);
                DownloadPaperDefinition(itemToDownload.Name);
            }
        }

        public void SynchronizeSettings()
        {
            EnsureStartIsCalled();
            Directory.CreateDirectory(SettingsRootFolder);

            List<Toolbox.LabelXItem> remoteItems = new List<Toolbox.LabelXItem>();
            GetRemoteSettingItems(remoteItems);

            List<Toolbox.LabelXItem> localItems = new List<Toolbox.LabelXItem>();
            GetLocalSettingItems(ref localItems);

            List<Toolbox.LabelXItem> itemsToDownload = new List<Toolbox.LabelXItem>();
            Toolbox.Toolbox.GetAddedItems(localItems, remoteItems, ref itemsToDownload);
            Toolbox.Toolbox.GetChangedItems(localItems, remoteItems, ref itemsToDownload);

            List<Toolbox.LabelXItem> itemsToDelete = new List<Toolbox.LabelXItem>();
            Toolbox.Toolbox.GetDeletedItems(localItems, remoteItems, ref itemsToDelete);

            foreach (Toolbox.LabelXItem itemToDelete in itemsToDelete)
            {
                GlobalDataStore.Logger.Info("Deleting Setting: " + itemToDelete.Name);
                string path = SettingsRootFolder + itemToDelete.Name + ".xml";
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_NONE));
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_DOWNLOADING));
            }

            foreach (Toolbox.LabelXItem itemToDownload in itemsToDownload)
            {
                GlobalDataStore.Logger.Info("Downloading Setting: " + itemToDownload.Name);
                DownloadSetting(itemToDownload.Name);
            }
        }

        public void SynchronizeUpdates()
        {
            EnsureStartIsCalled();
            Directory.CreateDirectory(UpdateRootFolder);
            string RemoteUpdateFolder;
            string LocalUpdateFolder;

            List<Toolbox.LabelXItem> remoteItems = new List<Toolbox.LabelXItem>();
            GetRemoteUpdateItems(remoteItems, out RemoteUpdateFolder);

            List<Toolbox.LabelXItem> localItems = new List<Toolbox.LabelXItem>();
            GetLocalUpdateItems(localItems, out LocalUpdateFolder);

            List<Toolbox.LabelXItem> itemsToDownload = new List<Toolbox.LabelXItem>();
            List<Toolbox.LabelXItem> itemsToDelete = new List<Toolbox.LabelXItem>();

            Toolbox.Toolbox.GetAddedFiles(localItems, remoteItems, ref itemsToDownload, RemoteUpdateFolder, LocalUpdateFolder);
            Toolbox.Toolbox.GetChangedFiles(localItems, remoteItems, ref itemsToDownload, RemoteUpdateFolder, LocalUpdateFolder);
            Toolbox.Toolbox.GetDeletedFiles(localItems, remoteItems, ref itemsToDelete, RemoteUpdateFolder, LocalUpdateFolder);

            foreach (Toolbox.LabelXItem itemToDelete in itemsToDelete)
            {
                GlobalDataStore.Logger.Warning("Deleting Update: " + itemToDelete.Name);
                string path = itemToDelete.Name;
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_NONE));
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_DOWNLOADING));
                DirectoryInfo dirInfo = new DirectoryInfo(Path.GetDirectoryName(itemToDelete.Name));
                if (dirInfo.GetFiles().Length < 1)
                {
                    try
                    {
                        Directory.Delete(dirInfo.FullName);
                    }
                    catch (Exception e)
                    {
                        GlobalDataStore.Logger.Error("Error deleting Directory: " + dirInfo.FullName + "\n" + e.Message);
                    }
                }

            }

            foreach (Toolbox.LabelXItem itemToDownload in itemsToDownload)
            {
                GlobalDataStore.Logger.Info("Downloading Update: " + itemToDownload.Name);
                DownloadUpdate(itemToDownload.Name, RemoteUpdateFolder, LocalUpdateFolder);
            }
        }

        public void SynchronizePictures()
        {
            EnsureStartIsCalled();
            Directory.CreateDirectory(PicturesRootFolder);
            string RemotePicturesFolder;
            string LocalPicturesFolder;

            List<Toolbox.LabelXItem> remoteItems = new List<Toolbox.LabelXItem>();
            GetRemotePictureItems(remoteItems, out RemotePicturesFolder);

            List<Toolbox.LabelXItem> localItems = new List<Toolbox.LabelXItem>();
            GetLocalPictureItems(localItems, out LocalPicturesFolder);

            List<Toolbox.LabelXItem> itemsToDownload = new List<Toolbox.LabelXItem>();
            GlobalDataStore.Logger.Debug("Calculating a list of added files");
            Toolbox.Toolbox.GetAddedFiles(localItems, remoteItems, ref itemsToDownload, RemotePicturesFolder, LocalPicturesFolder);
            GlobalDataStore.Logger.Debug("Calculating a list of changed files");
            Toolbox.Toolbox.GetChangedFiles(localItems, remoteItems, ref itemsToDownload, RemotePicturesFolder, LocalPicturesFolder);

            List<Toolbox.LabelXItem> itemsToDelete = new List<Toolbox.LabelXItem>();
            GlobalDataStore.Logger.Debug("Calculating a list of deleted files");
            Toolbox.Toolbox.GetDeletedFiles(localItems, remoteItems, ref itemsToDelete, RemotePicturesFolder, LocalPicturesFolder);
            GlobalDataStore.Logger.Debug("Calculating ready. Performing action...");
            foreach (Toolbox.LabelXItem itemToDelete in itemsToDelete)
            {
                GlobalDataStore.Logger.Info("Deleting Picture: " + itemToDelete.Name);
                string path = itemToDelete.Name;
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_NONE));
                File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_DOWNLOADING));
                DirectoryInfo dirInfo = new DirectoryInfo(Path.GetDirectoryName(itemToDelete.Name));
                if (dirInfo.GetFiles().Length < 1)
                {
                    try
                    {
                        Directory.Delete(dirInfo.FullName);
                    }
                    catch (Exception e)
                    {
                        GlobalDataStore.Logger.Info("Error deleting Directory: " + dirInfo.FullName + "\n" + e.Message);
                    }
                }

            }

            foreach (Toolbox.LabelXItem itemToDownload in itemsToDownload)
            {
                GlobalDataStore.Logger.Info("Downloading Picture: " + itemToDownload.Name);
                DownloadPicture(itemToDownload.Name, RemotePicturesFolder, LocalPicturesFolder);
            }
        }

        public void SynchronizePrintJobs()
        {
            EnsureStartIsCalled();

            Directory.CreateDirectory(PrintJobsRootFolder);

            //Get all printgroups 
            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetClientPrintGroups(ACA.LabelX.GlobalDataStore.AppPath + @"\ACALabelXClient.config.xml", ref PrintGroups);

            foreach (Toolbox.PrintGroupItem printGroup in PrintGroups)
                Directory.CreateDirectory(PrintJobsRootFolder + printGroup.Name);

            List<Toolbox.LabelXItem> remoteItems = new List<Toolbox.LabelXItem>();
            GetRemotePrintJobs(remoteItems);

            List<Toolbox.LabelXItem> localItems = new List<Toolbox.LabelXItem>();
            GetLocalPrintJobs(ref localItems);

            List<Toolbox.LabelXItem> itemsToDownload = new List<Toolbox.LabelXItem>();
            Toolbox.Toolbox.GetAddedItems(localItems, remoteItems, ref itemsToDownload);
            Toolbox.Toolbox.GetChangedItems(localItems, remoteItems, ref itemsToDownload);
            //
            //Code below will remove local printjobs if server printjobs are removed.
            //This would be usable if we wantted to keep the files on the server until
            //they are realy printed. We decided not to do this. Jobs on the server 
            //will be removed as soon as a file is transfered and the hass is successfully
            //matched...
            //List<Toolbox.LabelXItem> itemsToDelete = new List<Toolbox.LabelXItem>();
            //Toolbox.Toolbox.GetDeletedItems(localItems, remoteItems, ref itemsToDelete);
            //
            //foreach (Toolbox.LabelXItem itemToDelete in itemsToDelete)
            //{
            //    GlobalDataStore.Logger.Info("Deleting printjob: " + itemToDelete.Name);
            //    string path = PrintJobsRootFolder + itemToDelete.Name + ".xml";
            //    File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_NONE));
            //    File.Delete(GetFilePathWithStatus(path, FILESTATUS.FS_DOWNLOADING));
            //}

            foreach (Toolbox.LabelXItem itemToDownload in itemsToDownload)
            {
                GlobalDataStore.Logger.Info("Downloading printjob: " + itemToDownload.Name);
                DownloadPrintJob(MachineName, Path.GetDirectoryName(itemToDownload.Name), Path.GetFileName(itemToDownload.Name));
            }

            //New handler
            //We have transferd some  files now. We re-retrieve the localItems and compare this
            //with the local items
            List<Toolbox.LabelXItem> localItemsBis = new List<Toolbox.LabelXItem>();
            GetLocalPrintJobs(ref localItemsBis);
            foreach (Toolbox.LabelXItem itemOnLocalDir in localItemsBis)
            {
                foreach (Toolbox.LabelXItem itemRemote in itemsToDownload)
                {
                    if (itemOnLocalDir.Name.Equals(itemRemote.Name))
                    {
                        //this was the file we needed... check hash to be sure
                        if (itemOnLocalDir.Hash == itemRemote.Hash)
                        {
                            RemoveRemotePrintJobs(itemRemote);
                            break;
                        }
                    }
                }
            }

            //See if duplicate items exist on the server and delete them. (This shouldn't happen but you never know)
            foreach (Toolbox.LabelXItem localitem in localItems)
            {
                foreach (Toolbox.LabelXItem remoteItem in remoteItems)
                {
                    if (localitem.Name.Equals(remoteItem.Name))
                    {
                        //this was the file we needed... check hash to be sure
                        if (localitem.Hash == remoteItem.Hash)
                        {
                            RemoveRemotePrintJobs(remoteItem);
                            break;
                        }
                    }
                }
            }


        }

        public void ReceiveStatusUpdate(out string updateNodig)
        {
            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            MachineName = toolbox.GetMachineName(GlobalDataStore.AppPath + @"\ACALabelXClient.config.xml");
            updateNodig = GetRemoteObject().startUpdate(GlobalDataStore.ProgramVersion, MachineName);
        }

        public bool checkUpdate()
        {
            bool ret = false;
            
            string updateLoc = UpdateRootFolder + "setup.upd";            

            if (File.Exists(updateLoc))
            {
                GlobalDataStore.Logger.Debug("Checking if update is ready...");
                ret = true;
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(updateLoc);

                XmlNodeList nodes = xmlDoc.SelectNodes("/LabelXUpdate/file");

                if (nodes != null)
                {
                    foreach (XmlNode n in nodes)
                    {
                        string fnaam = UpdateRootFolder + n.Attributes["name"].Value;
                        if (!File.Exists(fnaam))
                            ret = false;
                    }
                }
            }
            else
            {
                GlobalDataStore.Logger.Debug("Client is up to date.");
                return ret;
            }
     

            if( !ret)
            {
                GlobalDataStore.Logger.Warning("Update files missing, client not ready to update yet.");
            }
            else
            {
                GlobalDataStore.Logger.Warning("Client is ready to execute update");
            }

           
            return ret;
        }

        public bool Login(out string updateNodig)
        {
            updateNodig = "";
            bool bRet;
            string HostName = System.Net.Dns.GetHostName();
            RemClientControlObject helper = new ACA.LabelX.Client.RemClientControlObject();
            string mach;

            LogonInfo theInfo = new ACA.LabelX.Toolbox.LogonInfo();
                                    //{
                                    //    MachineDescription = Environment.MachineName,
                                    //    Username = "ACAClient",
                                    //    Password = "(C)Retailium Software Development BV 2008",
                                    //    ProgramVersion = GlobalDataStore.ProgramVersion,
                                    //    updateReady = checkUpdate(),
                                    //    IpNumber = System.Net.Dns.GetHostEntry(HostName).AddressList[0],
                                    //    PrintGroups = helper.GetLabelPrintGroupsEx(out mach),
                                    //    MachineName = mach
                                    //};
            theInfo.MachineDescription = Environment.MachineName;
            theInfo.Username = "ACAClient";
            theInfo.Password = "(C)Retailium Software Development BV 2008";
            theInfo.ProgramVersion = GlobalDataStore.ProgramVersion;
            theInfo.updateReady = checkUpdate();

            System.Net.IPHostEntry IpHostEntry = System.Net.Dns.GetHostEntry(HostName);
            System.Net.IPAddress[] IpArray = IpHostEntry.AddressList;
            foreach (System.Net.IPAddress IPAddress in IpArray)
            {
                if (!IPAddress.IsIPv6LinkLocal)
                       theInfo.IpNumber = IPAddress;
            }

            theInfo.PrintGroups = helper.GetLabelPrintGroupsEx(out mach);
            theInfo.MachineName = mach;
            
            RemoteObject obj = GetRemoteObject();
            try
            {
                bRet = obj.Login(theInfo, out updateNodig);
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
