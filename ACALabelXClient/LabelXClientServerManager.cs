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
using ACA.LabelX.ClientEngine;
using ACA.LabelX.Toolbox;
using System.Runtime.Remoting;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;

namespace ACA.LabelX.Managers
{
    public class LabelXClientServerManager
    {
        private static bool moetstoppen;
        private static string updateNodig = "";
        private static bool moetinfoverzenden = true;
        private static ACALabelXClientEngine clientEngine;

        //public LabelXClientServerManager()
        //{

        //}
        public static void DoThreadWork()
        {
            LabelXClientServerManager theManager = new LabelXClientServerManager();
            theManager.Start();
        }

        public static void Stop()
        {
            moetstoppen = true;
        }

        public static void ResendInfo()
        {
            moetinfoverzenden = true;
        }

        public bool Start()
        {
            bool updateAvailable = false;

            try
            {
                clientEngine = new ACALabelXClientEngine();
                clientEngine.Start();

                ACA.LabelX.Toolbox.Toolbox toolbox = new ACA.LabelX.Toolbox.Toolbox();
                bool bStandAllone = toolbox.GetIsStandalloneInstallation(GlobalDataStore.AppPath + @"\ACALabelXClient.config.xml");
                if (bStandAllone)
                {
                    GlobalDataStore.Logger.Warning("This is a standallone installation.");
                    GlobalDataStore.Logger.Warning("Not connected to a central server.");
                    return true;
                }

                GlobalDataStore.Logger.Warning("Starting synchronisation with the server...");
                GlobalDataStore.Logger.Info("Configuration:");
                GlobalDataStore.Logger.Info("MachineName: " + clientEngine.MachineName);
                GlobalDataStore.Logger.Info("PrintJobsRootFolder: " + clientEngine.PrintJobsRootFolder);
                GlobalDataStore.Logger.Info("LabelDefinitionsRootFolder: " + clientEngine.LabelDefinitionsRootFolder);
                GlobalDataStore.Logger.Info("PaperDefinitionsRootFolder: " + clientEngine.PaperDefinitionsRootFolder);
                GlobalDataStore.Logger.Info("PicturesRootFolder: " + clientEngine.PicturesRootFolder);
                GlobalDataStore.Logger.Info("UpdateRootFolder: " + clientEngine.UpdateRootFolder);
                GlobalDataStore.Logger.Info("Printgroups:");

                foreach (PrintGroupItem item in clientEngine.PrintGroups)
                {
                    GlobalDataStore.Logger.Info(string.Format("PrintGroup: {0}", item.Name));
                }

                FileSystemWatcher updateFolderWatcher = new FileSystemWatcher
                {
                    Filter = "*.*",
                    InternalBufferSize = 128,
                    Path = clientEngine.UpdateRootFolder,
                    IncludeSubdirectories = true,
                    NotifyFilter = (NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName |
                                    NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size)
                };

                updateFolderWatcher.Changed += onUpdChanged;
                updateFolderWatcher.Created += onUpdChanged;
                updateFolderWatcher.Deleted += onUpdChanged;
                updateFolderWatcher.Renamed += onUpdChanged;
                updateFolderWatcher.EnableRaisingEvents = true;

                FileSystemWatcher pictureFolderWatcher = new FileSystemWatcher
                {
                    Filter = "",
                    InternalBufferSize = 128,
                    Path = clientEngine.PicturesRootFolder,
                    IncludeSubdirectories = true,                    
                    NotifyFilter = (NotifyFilters.Attributes |
                         NotifyFilters.CreationTime |
                         NotifyFilters.DirectoryName |
                         NotifyFilters.FileName | NotifyFilters.LastWrite |
                         NotifyFilters.Size)
                };

                pictureFolderWatcher.Changed += onImgChanged;
                pictureFolderWatcher.Created += onImgChanged;
                pictureFolderWatcher.Deleted += onImgChanged;
                pictureFolderWatcher.Renamed += onImgChanged;               
                pictureFolderWatcher.EnableRaisingEvents = true;

                while (true)
                {
                    GlobalDataStore.Logger.Debug("Connecting...");
                    try
                    {
                        clientEngine.PingRemoteServer();
                        GlobalDataStore.Logger.Debug("Connected!");

                        lock (GlobalDataStore.LockClass)
                        {
                            if (GlobalDataStore.MustResendInfo)
                            {
                                moetinfoverzenden = true;
                                GlobalDataStore.ResetResendInfo();
                            }
                        }

                        if (moetinfoverzenden)
                        {
                            GlobalDataStore.Logger.Debug("Sending client info!");
                            clientEngine.Login(out updateNodig);

                            moetinfoverzenden = false;
                        }

                        //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                        //sw.Start();
                        GlobalDataStore.Logger.Debug("Synchronizing Label Definitions...");
                        clientEngine.SynchronizeLabelDefinitions();

                        GlobalDataStore.Logger.Debug("Synchronizing Paper Definitions...");
                        clientEngine.SynchronizePaperDefinitions();

                        GlobalDataStore.Logger.Debug("Synchronizing PrintJobs...");
                        clientEngine.SynchronizePrintJobs();

                        GlobalDataStore.Logger.Debug("Synchonizing Settings...");
                        clientEngine.SynchronizeSettings();

                        GlobalDataStore.Logger.Debug("Synchronizing Pictures...");
                        clientEngine.SynchronizePictures();

                        GlobalDataStore.Logger.Debug("Synchronizing Updates...");
                        clientEngine.SynchronizeUpdates();

                        GlobalDataStore.Logger.Debug("Updating Status...");
                        clientEngine.ReceiveStatusUpdate(out updateNodig);

                        GlobalDataStore.Logger.Debug("Synchronization ready.");

                        GlobalDataStore.Logger.Debug("Checking for update...");
                        updateAvailable = clientEngine.checkUpdate();
                        string MachineName = toolbox.GetMachineName(GlobalDataStore.AppPath + @"\ACALabelXClient.config.xml");
                        clientEngine.GetRemoteObject().updateClientXML(MachineName, "readyForUpdate", updateAvailable.ToString());

                        //sw.Stop();
                        //GlobalDataStore.Logger.Info("Time needed for Synchronization: " + sw.ElapsedMilliseconds + " ms");
                    }
                    catch (RemotingException e)
                    {
                        GlobalDataStore.Logger.Error("Remoting error: " + e.Message);
                        GlobalDataStore.Logger.Info("Will retry shortly...");
                    }
                    catch (WebException e)
                    {
                        GlobalDataStore.Logger.Error("Web error: " + e.Message);
                        GlobalDataStore.Logger.Info("Will retry shortly...");
                        GlobalDataStore.ResendInfo();
                    }
                    catch (IOException e)
                    {
                        GlobalDataStore.Logger.Error("IO error: " + e.Message);
                        GlobalDataStore.Logger.Info("Will retry shortly...");
                    }
                    catch (Exception e)
                    {
                        GlobalDataStore.Logger.Error("Unspecified error in synchronisation: " + e.Message);
                        GlobalDataStore.Logger.Info("Will retry shortly...");
                    }
                    try
                    {
                        if (updateAvailable && updateNodig.Length > 0)
                        {
                            if (File.Exists(clientEngine.UpdateRootFolder + updateNodig))
                            {
                                GlobalDataStore.Logger.Warning("Update Available, Executing");
                                System.Diagnostics.Process.Start(clientEngine.UpdateRootFolder + updateNodig);
                                moetstoppen = true; //Check if this works, untested
                            }
                        }

                        GlobalDataStore.Logger.Info("Waiting...");
                        for (int Index = clientEngine.PollFrequency; Index > 0; Index--)
                        {
                            GlobalDataStore.Logger.Debug(string.Format("{0:0#}", Index));
                            if (moetstoppen)
                                break;
                            Thread.Sleep(1000);
                        }
                        GlobalDataStore.Logger.Info("Ready waiting...");
                        if (moetstoppen)
                            break;
                    }
                    catch (Exception e)
                    {
                        GlobalDataStore.Logger.Error("Unspecified post-synchronisation error: " + e.Message);
                        GlobalDataStore.Logger.Info("Will retry shortly...");
                    }
                    
                }
            }
            catch (Exception e)
            {
                GlobalDataStore.Logger.Error(string.Format("Error: {0} Target site:{1} Stack trace:", e.Message, e.TargetSite));
            }
            finally
            {
                GlobalDataStore.Logger.Info("Done...");
            }
            return true;
        }

        private static void onUpdChanged(object sender, FileSystemEventArgs e)
        {
            //string strFileExt = getFileExt(e.FullPath);
            //if (Regex.IsMatch(strFileExt, @"(.*\.xml)", RegexOptions.IgnoreCase)) return;
            if (e.Name.Equals("Updates.xml",StringComparison.OrdinalIgnoreCase)) return;

            clientEngine.updFolderChanged = true;
            GlobalDataStore.Logger.Debug("Change detected in Update Folder. Will rebuild XML.");
        }

        

        private static void onImgChanged(object sender, FileSystemEventArgs e)
        {
            string strFileExt = getFileExt(e.FullPath);
            bool isDirectory = false;

            try
            {
                isDirectory = (File.GetAttributes(e.FullPath) & FileAttributes.Directory) == FileAttributes.Directory;
            }
            catch { }

            if ((Regex.IsMatch(strFileExt, @"(.*\.jpg)|(.*\.jpeg)|(.*\.bmp)|(.*\.png)", RegexOptions.IgnoreCase)) | isDirectory)                
            {
                if (isDirectory)
                {
                    //if (e.ChangeType != WatcherChangeTypes.Changed)
                    //    clientEngine.updateInit = true;
                }
                else
                {
                    Monitor.Enter(clientEngine._object);
                    clientEngine.listPictureChanged.Add(e);
                    Monitor.Exit(clientEngine._object);
                }

                Monitor.Enter(clientEngine._object);
                clientEngine.imgFolderChanged = true;
                Monitor.Exit(clientEngine._object);

                GlobalDataStore.Logger.Debug(string.Format("Picture directory change: {0}, {1}", e.ChangeType.ToString(), e.FullPath));
            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                //Een onbekende file of een directory zijn verwijderd !?
                Monitor.Enter(clientEngine._object);
                clientEngine.updateInit = true;
                Monitor.Exit(clientEngine._object);

                string fmt = string.Format("Picture directory delete: Deleted, {0}", e.FullPath);
                GlobalDataStore.Logger.Debug(fmt);
            }
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
