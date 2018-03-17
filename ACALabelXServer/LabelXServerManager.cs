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
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using ACA.LabelX.TestServer;

namespace ACA.LabelX.Managers
{
    public class LabelXServerManager
    {
        private static bool moetstoppen;
        private static int updateTimer = 2;
        string PrintJobsRootFolder = "";
        string LabelDefinitionsRootFolder = "";
        string PaperDefinitionsRootFolder = "";
        string SettingsRootFolder = "";
        string PicturesRootFolder = "";
        string UpdateRootFolder = "";
        private bool updatePictures = true;
        private bool updateUpdates = true;
        readonly System.Timers.Timer timer = new System.Timers.Timer();

        //public LabelXServerManager()
        //{
        //}
        
        public static void DoThreadWork()
        {
            LabelXServerManager theManager = new LabelXServerManager();
            theManager.Start();
        }
        public static void Stop()
        {
            moetstoppen = true;
        }

        public bool Start()
        {
            bool bRet;
            bool FirstRun = true;

            moetstoppen = false;

            Opnieuw:         
            bRet = true;            

            string AppPath = GlobalDataStore.AppPath; //System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            try
            {
                ACALabelXController controller = new ACALabelXController();
                controller.Start(AppPath + @"\ACALabelXServer.config.xml");
                GlobalDataStore.Logger.Info("=======================================");
                GlobalDataStore.Logger.Info("Configuration:");
                GlobalDataStore.Logger.Info("=======================================");
                Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
                toolbox.GetGeneralServerConfiguraton(AppPath + @"\ACALabelXServer.config.xml", out PrintJobsRootFolder, out LabelDefinitionsRootFolder, out PaperDefinitionsRootFolder, out SettingsRootFolder, out PicturesRootFolder, out UpdateRootFolder);
                GlobalDataStore.Logger.Info("PrintJobsRootFolder: " + PrintJobsRootFolder);
                GlobalDataStore.Logger.Info("LabelDefinitionsRootFolder: " + LabelDefinitionsRootFolder);
                GlobalDataStore.Logger.Info("PaperDefinitionsRootFolder: " + PaperDefinitionsRootFolder);
                GlobalDataStore.Logger.Info("SettingsRootFolder: " + SettingsRootFolder);
                GlobalDataStore.Logger.Info("PicturesRootFolder: " + PicturesRootFolder);
                GlobalDataStore.Logger.Info("UpdateRootFolder: " + UpdateRootFolder);
                GlobalDataStore.Logger.Info("=======================================");

                FileSystemWatcher updateFolderWatcher = new FileSystemWatcher
                {
                    Filter = "",
                    InternalBufferSize = 128,
                    Path = UpdateRootFolder,
                    IncludeSubdirectories = true,
                    NotifyFilter = (NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                    NotifyFilters.DirectoryName | NotifyFilters.FileName |
                                    NotifyFilters.LastWrite | NotifyFilters.Size)
                };

                updateFolderWatcher.Changed += onUpdateChanged;
                updateFolderWatcher.Created += onUpdateChanged;
                updateFolderWatcher.Deleted += onUpdateChanged;
                updateFolderWatcher.Renamed += onUpdateChanged;
                updateFolderWatcher.EnableRaisingEvents = true;

                FileSystemWatcher pictureFolderWatcher = new FileSystemWatcher
                {
                    Filter = "",
                    InternalBufferSize = 128,
                    Path = PicturesRootFolder,
                    IncludeSubdirectories = true,
                    NotifyFilter = (NotifyFilters.Attributes | NotifyFilters.CreationTime |
                                    NotifyFilters.DirectoryName | NotifyFilters.FileName | 
                                    NotifyFilters.LastWrite | NotifyFilters.Size)
                };

                pictureFolderWatcher.Changed += onPictureChanged;
                pictureFolderWatcher.Created += onPictureChanged;
                pictureFolderWatcher.Deleted += onPictureChanged;
                pictureFolderWatcher.Renamed += onPictureChanged;
                pictureFolderWatcher.EnableRaisingEvents = true;

                timer.Elapsed += timer_Tick;
                timer.Interval = 1000;
                timer.Enabled = true;
                FirstRun = false;
                while (!moetstoppen)
                {
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                GlobalDataStore.Logger.Error(string.Format("Error:\r\n{0}\r\n\r\nTarget site:\r\n{1}\r\n\r\nStack trace:\r\n{2}", e.Message, e.TargetSite, e.StackTrace));
                if (e.InnerException != null)
                {
                    GlobalDataStore.Logger.Error(string.Format("Error:\r\n{0}\r\n\r\nTarget site:\r\n{1}\r\n\r\nStack trace:\r\n{2}", e.InnerException.Message, e.InnerException.TargetSite, e.InnerException.StackTrace));
                }
                bRet = false;                
                if (FirstRun)
                {
                    FirstRun = false;
                    //echt stoppen
                } else
                {
                    //Log a speep event and try again in 10 minutes.
                    GlobalDataStore.Logger.Error("The above error prevents LabelPrint from printing. Retying in 10 minutes.");
                    Thread.Sleep(1000 * 60 * 10); // sleep 10 minutes.
                    
                    goto Opnieuw;  //Oeps... violation of all learned rules.
                }
            }
            return bRet;
        }

        private void onPictureChanged(object sender, FileSystemEventArgs e)
        {
            string strFileExt = getFileExt(e.FullPath);
            bool isDirectory = false;

            updatePictures = true;
            try
            {
                isDirectory = (File.GetAttributes(e.FullPath) & FileAttributes.Directory) == FileAttributes.Directory;
            }
            catch {}
                       
            if ((Regex.IsMatch(strFileExt, @"(.*\.jpg)|(.*\.jpeg)|(.*\.bmp)|(.*\.png)", RegexOptions.IgnoreCase)) | isDirectory)            
            {
                updateTimer = 10;
                timer.Start();
                GlobalDataStore.Logger.Debug("Change detected in Picture directory. Updating XML Timer");   
            }
        }

        private void onUpdateChanged(object sender, FileSystemEventArgs e)
        {
            if (e.Name.Equals("Updates.xml")) return;
            //string strFileExt = getFileExt(e.FullPath);           
            //if (Regex.IsMatch(strFileExt, @"(.*\.xml)", RegexOptions.IgnoreCase)) return;            

            updateUpdates = true;

            updateTimer = 10;
            timer.Start();
            GlobalDataStore.Logger.Debug("Change detected in Update directory. Updating XML Timer");
        }

        private void timer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            updateTimer--;
            if (updateTimer == 0)
            {
                timer.Stop();

                if (updatePictures)
                {
                    WritePictureXMLFile();
                    GlobalDataStore.Logger.Debug("Updated Pictures.xml");
                    updatePictures = false;
                }

                if (updateUpdates)
                {
                    WriteDirectoryXMLFile(UpdateRootFolder, "Updates.xml");
                    GlobalDataStore.Logger.Debug("Updated Updates.xml");
                    updateUpdates = false;
                }
            }
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
                //if( getFileExt(item.Name) == ".xml") continue;
                if (!item.Name.Equals(folder + fnaam))
                {

                    System.Xml.XmlElement itemXML = doc.CreateElement("item");
                    itemXML.SetAttribute("name", item.Name);
                    itemXML.SetAttribute("hash", item.Hash);

                    root.AppendChild(itemXML);
                }
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

    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
