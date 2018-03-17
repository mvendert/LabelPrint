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
/*

Doel:
Zorgen dat de gegevens in de acalabelxclient.config.xml en acalabelxserver.config.xml ook veilig 
 * gesteld worden op een backup. Momenteel staan deze bestanden in de programmamap, 
 * en deze wordt niet meegenomen in de standaard ACA backup.
 * 
Oplossingsrichting:
De programmamap wordt door de standaard backup methode niet meegenomen. 
Het is niet zo eenvoudig om bij huidige installaties dit zomaar aan te passen. 
Daarnaast zou ook de .exe.config bestanden die bij de executable MOETEN staan in .net, 
meegenomen moeten worden in de backup.
Het meest eenvoudige is om deze bestanden op regelmatige basis veilig te stellen in 
een andere map. D.w.z. dat je de clientservice.exe.config en serverservice.exe.config 
moet openen, lezen en daar de waarde van de property ConfigXML moet ophalen. Dit XML 
bestand moet je veilig stellen. Daarnaast moet je de folders node onderzoeken en 
een backup maken van de inhoud van een aantal van deze folders: labeldefinitionsrootfolder, 
paperdefinitionsrootfolder, settingsrootfolder en updaterootfolder. Omdat de pictures folder te 
groot kan zijn moet deze uitmaken van de serverbackup.
Onder C:\ACA\LabelprintBackup maak je dan een backup van deze gegevens in hun folderstructuur.
De backup procedure zal deze map normaliter meenemen.
Het herstellen na een volledig falen zal dan toch een handmatige actie zijn in eerste instantie. 
Opnieuw installeren en dan de juiste mappen terug kopiëren.

Probleem:
 * In de situatie waar decentraal geprint wordt staat de ACA Labelprint Client op een bepaalde decentrale computer.
 * Dit hoeft geen computer te zijn waarop nog andere ACA software geïnstalleerd is. 
 * Dan moet er wel gedacht worden aan een back-up van deze gegevens. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using System.IO;


namespace ACA.LabelX.Toolbox
{
    public class BackupFolderstructure
    {

        public class BackupElement
        {
            public string   name;
            public bool     isFolder;
            public string   toFolder;

            public BackupElement(string FileOrFolderName, bool IsAFolder, string ToBackupLocation)
            {
                name = FileOrFolderName;
                isFolder = IsAFolder;
                toFolder = ToBackupLocation;
            }
        }
        private bool bForClient;
        private bool bForServer;
        private bool mustBackup;

        private string ServerBackupFolder;
        private string ClientBackupFolder;

        public BackupFolderstructure()
        {
            ServerBackupFolder = @"c:\aca\LabelPrintBackup\Server";
            ClientBackupFolder = @"c:\aca\LabelPrintBackup\Client";


            if (ConfigurationManager.AppSettings["ServerBackupFolder"] != null)
            {
                ServerBackupFolder = ConfigurationManager.AppSettings["ServerBackupFolder"].ToString();
            }
            if (ConfigurationManager.AppSettings["ClientBackupFolder"] != null)
            {
                ClientBackupFolder = ConfigurationManager.AppSettings["ClientBackupFolder"].ToString();
            }
            string toBackup = "TRUE";
            if (ConfigurationManager.AppSettings["BackupFolders"] != null)
            {
                toBackup = ConfigurationManager.AppSettings["BackupFolders"].ToString().ToUpper();
            }
            if (toBackup.Equals("TRUE"))
            {
                mustBackup = true;
            } else
            {
                mustBackup = false;
            }
        }

        private bool BackupClientStructure(string ClientBackupFolder)
        {
            Stack<BackupElement> filesToBackup = new Stack<BackupElement>();

            //Retrieve the main xml file name
            string ConfigXML = string.Empty;


            string appPath = GlobalDataStore.AppPath;
            string ConfigFilePath = Path.Combine(appPath , "ACALabelXClient.config.xml");
            if (!(File.Exists(ConfigFilePath)))
            {
                try
                {
                    ConfigFilePath = ConfigurationManager.AppSettings["ConfigXML"].ToString();                                                                        
                }
                catch (NullReferenceException)
                {
                    ConfigFilePath = "";
                    return false;
                }
            }
            ConfigXML = ConfigFilePath;
            if (ConfigXML.Length == 0)
            {
                return false;
            }

            //Open the XML, and read the appropriate items
            XPathDocument xDoc;
            xDoc = new XPathDocument(ConfigXML);
            XPathNavigator nav;
            nav = xDoc.CreateNavigator();
            XPathNodeIterator theNodes;
            theNodes = nav.Select("/configuration/general-settings/folders");
            if (theNodes != null)
            {
                while (theNodes.MoveNext())
                {
                    if (theNodes.Current.HasChildren)
                    {
                        XPathNodeIterator childNodes = theNodes.Current.SelectChildren(XPathNodeType.Element);
                        while (childNodes.MoveNext())
                        {
                            string dirname = childNodes.Current.Value;
                            if ((childNodes.Current.Name == "LabelDefinitionsRootFolder") ||
                                (childNodes.Current.Name == "PaperDefinitionsRootFolder") ||
                                (childNodes.Current.Name == "SettingsRootFolder"))                                
                            {
                                filesToBackup.Push(new BackupElement(dirname, true, ClientBackupFolder));
                            }
                        }
                    }                                      
                }
            }
            HandleStack(filesToBackup);
            return true;
        }

        private void HandleStack(Stack<BackupElement> filesToBackup)
        {
            //Create a backup of all files and folders
            while (filesToBackup.Count > 0)
            {
                BackupElement el = filesToBackup.Pop();
                if (el.isFolder)
                {
                    if (Directory.Exists(el.name))
                    {
                        DirectoryInfo dif = new DirectoryInfo(el.name);
                        string outputdir = Path.Combine(el.toFolder,dif.Name); //the target is the backupfolder, extended with the directory name
                        Copy(el.name, outputdir);
                    }
                }
                else
                {
                    //it's a file, just copy it the base directory
                    if (File.Exists(el.name))
                    {
                        FileInfo fi = new FileInfo(el.name);
                        fi.CopyTo(Path.Combine(el.toFolder, fi.Name), true);                        
                    }
                }
            }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (!Directory.Exists(target.FullName))
            {
                Directory.CreateDirectory(target.FullName);
            }
            //Copy each file in the source to the (new) target
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name),true);
            }
            //Copy subdirectories recursively
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }       
         
        private bool BackupServerStructure(string ServerBackupFolder)
        {
            Stack<BackupElement> filesToBackup = new Stack<BackupElement>();

            //Retrieve the main xml file name
            string ConfigXML = string.Empty;
            if (ConfigurationManager.AppSettings["ConfigXML"] != null)
            {
                ConfigXML = ConfigurationManager.AppSettings["ConfigXML"].ToString();
                filesToBackup.Push(new BackupElement(ConfigXML, false,ServerBackupFolder));
            }
            if (ConfigXML.Length == 0)
            {
                return false;
            }

            //Open the XML, and read the appropriate items
            XPathDocument xDoc;
            xDoc = new XPathDocument(ConfigXML);
            XPathNavigator nav;
            nav = xDoc.CreateNavigator();
            XPathNodeIterator theNodes;
            theNodes = nav.Select("/configuration/general-settings/folders");
            if (theNodes != null)
            {
                while (theNodes.MoveNext())
                {
                    if (theNodes.Current.HasChildren)
                    {
                        XPathNodeIterator childNodes = theNodes.Current.SelectChildren(XPathNodeType.Element);
                        while (childNodes.MoveNext())
                        {
                            string dirname = childNodes.Current.Value;
                            if ((childNodes.Current.Name == "LabelDefinitionsRootFolder") ||
                                (childNodes.Current.Name == "PaperDefinitionsRootFolder") ||
                                (childNodes.Current.Name == "SettingsRootFolder") ||
                                (childNodes.Current.Name == "UpdateRootFolder"))
                            {
                                filesToBackup.Push(new BackupElement(dirname, true, ServerBackupFolder));
                            }
                        }
                    }                                      
                }
            }
            HandleStack(filesToBackup);
            return true;
        }        

        public bool CheckExistence(string foldername)
        {
            bool bRet = true ;
            bool AlreadyExists;

            AlreadyExists = System.IO.Directory.Exists(foldername);
            if (!AlreadyExists)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(foldername);
                }
                catch (Exception ex2)
                {
                    System.Diagnostics.Debug.WriteLine(ex2.Message);
                    bRet = false;
                }
            }
            return bRet;
        }

        public bool ForServer
        {
            get { return bForServer; }
            set { bForServer = value; }
        }

        public bool ForClient
        {
            get { return bForClient; }
            set { bForClient = value; }
        }

        public bool BackupNow()
        {
            bool bRet = true;
            if (!mustBackup)
            {
                return true;
            }
            try
            {
                if (bForServer)
                {
                    if (CheckExistence(ServerBackupFolder))
                    {
                        BackupServerStructure(ServerBackupFolder);
                    }
                }

                if (bForClient)
                {
                    if (CheckExistence(ClientBackupFolder))
                    {
                        BackupClientStructure(ClientBackupFolder);
                    }
                }
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }
    }
}
