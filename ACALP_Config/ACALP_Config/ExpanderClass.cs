using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

namespace ACALP_Config
{

    class ExpanderClass
    {
                     
        public bool bClientInstalled;
        public bool bServerInstalled;
        public bool bControlerInstalled;
        public bool bDesignerInstalled;

        bool bClientAfterRemove;
        bool bServerAfterRemove;
        bool bControllerAfterRemove;
        bool bDesingerAfterRemove;

        private DefaultValues defValues;
        private string sMasterPath;

        public string MasterPath
        {
            get { return sMasterPath; }
            set { sMasterPath = value; }
        }

        public DefaultValues DefValues
        {
            get { return defValues; }
            set { defValues = value; }
        }
                
        public void ExpandAllFiles()
        {
            
            if (bClientInstalled)
            {
                ExpandFile("ClientService", "clientconfigxml", "clientxmlbase");
                ExpandFile("ClientService", "clientremoteconfigfile", "clientremoteconfig");
                ExpandFile("ClientService", "clientserviceconfigfile","clientserviceconfig");
            }

            if (bServerInstalled)
            {
                ExpandFile("ServerService", "serverconfigxml", "serverxmlbase");
                ExpandFile("ServerService", "serverserviceconfigfile", "serverserviceconfig");
            }

            if (bControlerInstalled)
            {
                ExpandFile("LabelControler", "labelcontrolerconfigfile", "labelcontrolerconfig");
            }

            if (bDesignerInstalled)
            {
                ExpandFile("LabelDesigner", "labeldesignerconfigfile", "labeldesignerconfig");
            }
            CreateDataFolders();
        }

        public void RemoveAllFiles()
        {
            RemoveDataFolders();
            if ( (bClientInstalled) && (!bClientAfterRemove))
            {
                RemoveFile("ClientService", "clientconfigxml", "clientxmlbase");
                RemoveFile("ClientService", "clientremoteconfigfile", "clientremoteconfig");
                RemoveFile("ClientService", "clientserviceconfigfile", "clientserviceconfig");
            }

            if ((bServerInstalled) && (!bServerAfterRemove))
            {
                RemoveFile("ServerService", "serverconfigxml", "serverxmlbase");
                RemoveFile("ServerService", "serverserviceconfigfile", "serverserviceconfig");
            }

            if ((bControlerInstalled) && (!bControllerAfterRemove))
            {
                RemoveFile("LabelControler", "labelcontrolerconfigfile", "labelcontrolerconfig");
            }

            if ((bDesignerInstalled) && (!bDesingerAfterRemove))
            {
                RemoveFile("LabelDesigner", "labeldesignerconfigfile", "labeldesignerconfig");
            }            
        }
        public void RemoveAllFiles(DefaultValues def)
        {
            defValues = def;
            RemoveAllFiles();
        }
        public void ExpandAllFiles(DefaultValues def)
        {
            defValues = def;
            ExpandAllFiles();
        }

        private bool ExpandFile(string sMainPackage,string sFileName, string sBaseName)
        {
            string sSourcePath;
            string sTargetPath = string.Empty;
            XmlNode savedPrintgroups = null;
            string savedPrintgroupsXml = "";

            sMasterPath = defValues.Values["masterpath"].Value;

            sSourcePath = sMasterPath + @"\DataFiles\" +sMainPackage +@"\"+ defValues.Values[sBaseName].Value;
            if (!File.Exists(sSourcePath))
            {
                MessageBox.Show(string.Format("Bestand {0} niet gevonden.", sSourcePath));
                return false;
            }

//            if (sMainPackage == "ClientService")
 //           {
                sTargetPath = defValues.Values[sFileName].Value;
   //         }

            if (sTargetPath != string.Empty)
            {
                FileInfo f;
                f = new FileInfo(sTargetPath);                
                CreateDirIfNotExists(f.DirectoryName);
                if (File.Exists(sTargetPath))
                {
                    try
                    {
                        if (sFileName.Equals("clientconfigxml"))
                        {
                            //Save old printgroups of client
                            XmlDocument doc = new XmlDocument();
                            doc.Load(sTargetPath);
                            savedPrintgroups = doc.SelectSingleNode("/configuration/general-settings/print-groups");                                               
                            savedPrintgroupsXml = savedPrintgroups.InnerXml;
                        }
                                        
                        File.Delete(sTargetPath);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(string.Format("File {0} kon niet verwijderd/overschreven worden", sTargetPath));
                        return false;
                    }
                }
            }
            StreamReader sr = null;
            StreamWriter sw = null;
            try
            {
                sr = new StreamReader(sSourcePath);
            }
            catch (Exception e4)
            {
                MessageBox.Show(string.Format("Bestand {0}: {1}", sSourcePath, e4.Message));
                return false;
            }
            try
            {
                sw = new StreamWriter(sTargetPath);
            }
            catch (Exception e5)
            {
                sr.Close();
                sr.Dispose();
                MessageBox.Show(string.Format("Bestand {0}: {1}", sTargetPath, e5.Message));
                return false;            
            }
        
            string sLine;
            while (sr.Peek() > 0)
            {
                sLine = sr.ReadLine();
                if (sLine.Contains("@@"))
                {
                    int pos1;
                    int pos2;
                    bool bVerder = false; ;

                    pos1 = sLine.IndexOf("@@");
                    if (pos1 >= 0)
                    {
                        bVerder = true;
                    }
                    while (bVerder)
                    {
                        bVerder = false;
                        pos2 = sLine.Length;
                        if (pos1 + 1 < sLine.Length - 1)
                        {
                            pos2 = sLine.IndexOf("@@", pos1 + 1);
                            string sVar = sLine.Substring(pos1 + 2, pos2 - pos1-2);
                            string sRep = string.Empty;
                            if (defValues.Values.ContainsKey(sVar))
                            {
                                sRep = defValues.Values[sVar].Value;
                                sVar = "@@" + sVar + "@@";
                                sLine = sLine.Replace(sVar, sRep);
                            }
                        }
                        if (pos2 + 2 < sLine.Length)
                        {
                            pos1 = sLine.IndexOf("@@", pos2 + 1);
                            if (pos1 >= 0)
                            {
                                bVerder = true;
                            }
                        }
                    }
                    sw.WriteLine(sLine);
                }
                else
                {
                    sw.WriteLine(sLine);
                }
            }
            sr.Close();
            sw.Close();

            if (sFileName.Equals("clientconfigxml") && (savedPrintgroups != null))
            {
                //Restore old printgroups of client
                XmlDocument doc = new XmlDocument();
                doc.Load(sTargetPath);
                XmlNode newPrintgroupNode = doc.SelectSingleNode("/configuration/general-settings/print-groups");
                newPrintgroupNode.InnerXml = savedPrintgroupsXml;
                doc.Save(sTargetPath);
            }
            return false;
        }

        private bool RemoveFile(string sMainPackage, string sFileName, string sBaseName)
        {                        
            string sTargetPath = string.Empty;
            sMasterPath = defValues.Values["masterpath"].Value;

            sTargetPath = defValues.Values[sFileName].Value;            
            if (sTargetPath != string.Empty)
            {
                FileInfo f;
                f = new FileInfo(sTargetPath);            
                if (File.Exists(sTargetPath))
                {
                    try
                    {
                        File.Delete(sTargetPath);
                    }
                    catch (Exception)
                    {                        
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CreateDirIfNotExists(string sDirName)
        {
            try
            {
                if (!Directory.Exists(sDirName))
                {
                    Directory.CreateDirectory(sDirName);
                }
            }
            catch (Exception)
            {
                return false;
            }
            
            return (true);
        }

        public void SetInstalled(bool bClient, bool bServer, bool bController, bool bDesinger)
        {
            bClientInstalled = bClient;
            bServerInstalled = bServer;
            bControlerInstalled = bController;
            bDesignerInstalled = bDesinger;
        }
        public void SetAfterRemove(bool bClientServerviceAfterRemove, bool bServerServiceAfterRemove, bool bControllerAfterRemoveIn, bool bDesingerAfterRemoveIn)
        {
            bClientAfterRemove = bClientServerviceAfterRemove;
            bServerAfterRemove = bServerServiceAfterRemove;
            bControllerAfterRemove  = bControllerAfterRemoveIn;
            bDesingerAfterRemove = bDesingerAfterRemoveIn;            
        }
        public void CreateDataFolders()
        {
            try
            {
                if (bClientInstalled)
                {
                    Directory.CreateDirectory(defValues.Values["clientpjrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["clientldrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["clientpdrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["clientstrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["clientptrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["clientuprootfolder"].Value);
                }

                if (bServerInstalled)
                {
                    Directory.CreateDirectory(defValues.Values["printjobsrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["labeldefinitionrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["paperdefinitionsrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["settingsrootfolder"].Value);                  
                    Directory.CreateDirectory(defValues.Values["picturesrootfolder"].Value);
                    Directory.CreateDirectory(defValues.Values["updaterootfolder"].Value);
                }
            }
            catch (Exception ee1)
            {
                MessageBox.Show(string.Format("Oeps: {0}",ee1.Message));                
            }
        }

        public void RemoveDataFolders()
        {
            if ((bClientInstalled) && (! bClientAfterRemove))
            {
                TryRemoveDirectory(defValues.Values["clientpjrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["clientldrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["clientpdrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["clientstrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["clientptrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["clientuprootfolder"].Value);
            }

            if ((bServerInstalled) && (! bServerAfterRemove))
            {
                TryRemoveDirectory(defValues.Values["printjobsrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["labeldefinitionrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["paperdefinitionsrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["settingsrootfolder"].Value);
                //Never remove the server picture folder. It needs to be removed manually!
                //TryRemoveDirectory(defValues.Values["picturesrootfolder"].Value);
                TryRemoveDirectory(defValues.Values["updaterootfolder"].Value);
            }                        
        }

        private bool TryRemoveDirectory(string sDir)
        {
            try
            {
                Directory.Delete(sDir, true);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


    }
}
