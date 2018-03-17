using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;

namespace ACALP_Config
{
    class InstallerPaths
    {
        private string sClientServicePath;

        public string ClientServicePath
        {
            get { return sClientServicePath; }
            set { sClientServicePath = value; }
        }
        private string sServerServicePath;

        public string ServerServicePath
        {
            get { return sServerServicePath; }
            set { sServerServicePath = value; }
        }
        private string sLabelControllerPath;

        public string LabelControllerPath
        {
            get { return sLabelControllerPath; }
            set { sLabelControllerPath = value; }
        }
        private string sLabelDesignerPath;

        public string LabelDesignerPath
        {
            get { return sLabelDesignerPath; }
            set { sLabelDesignerPath = value; }
        }
        private bool bClientInstalled;

        public bool ClientInstalled
        {
            get { return bClientInstalled; }
            set { bClientInstalled = value; }
        }
        private bool bServerInstalled;

        public bool ServerInstalled
        {
            get { return bServerInstalled; }
            set { bServerInstalled = value; }
        }
        private bool bControllerInstalled;

        public bool ControllerInstalled
        {
            get { return bControllerInstalled; }
            set { bControllerInstalled = value; }
        }
        private bool bDesingerInstalled;

        public bool DesingerInstalled
        {
            get { return bDesingerInstalled; }
            set { bDesingerInstalled = value; }
        }        

        public InstallerPaths()
        {
            sClientServicePath = string.Empty;
            sServerServicePath = string.Empty;
            sLabelControllerPath = string.Empty;
            sLabelDesignerPath = string.Empty;  
        }

        public RegistryKey TryOpenKey(RegistryKey basek, string subkey, bool bShowErrors)
        {
            RegistryKey ret = null;
            try
            {
                ret = basek.OpenSubKey(subkey, false);
            }
            catch (Exception e)
            {
                if (bShowErrors)
                {
                    MessageBox.Show(string.Format("Fout bij openenen registersleutel {0}/{1}: {2}",
                        basek.Name, subkey, e.Message));
                }
                return null;
            }
            return ret;
        }


        public bool LoadInstallerPaths()
        {            

                    
            RegistryKey lm = null;
            RegistryKey software = null;
            RegistryKey aca = null;
            RegistryKey InstallerInfo = null;
            RegistryKey LabelPrint = null;
 
            sClientServicePath = string.Empty;
            sServerServicePath = string.Empty;
            sLabelControllerPath = string.Empty;
            sLabelDesignerPath = string.Empty;
            

            try
            {
                lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            }
            catch (Exception)
            {
                MessageBox.Show("Mag registry niet lezen.");                
                return false;
            }
            software = TryOpenKey(lm, "Software", true);
            if (software == null)
            {                
                return false;
            }
            aca = TryOpenKey(software, "ACA", true);
            if (aca == null) return false;
            InstallerInfo = TryOpenKey(aca, "InstallInfo", true);
            if (InstallerInfo == null) return false;
            LabelPrint = TryOpenKey(InstallerInfo, "LabelPrint", true);
            if (LabelPrint == null) return false;

            string sFolder = LabelPrint.GetValue("Folder", "").ToString();

            
            bClientInstalled = false;
            if (LabelPrint != null)
            {
                sClientServicePath = Path.Combine(sFolder, @"Client\Service");
                string sHelkp = Path.Combine(sClientServicePath, "ACALabelXClientService.exe");               
                if (Directory.Exists(sClientServicePath))
                {
                    if (File.Exists(Path.Combine(sClientServicePath, "ACALabelXClientService.exe")))
                    {
                        bClientInstalled = true;
                    }
                }
            }

            bServerInstalled = false;
            if (LabelPrint != null)
            {
                sServerServicePath = Path.Combine(sFolder, @"Server\Service");
                if (Directory.Exists(sServerServicePath))
                {
                    if (File.Exists(Path.Combine(sServerServicePath, "ACALabelXServerService.exe")))
                    {
                        bServerInstalled = true;
                    }
                }
            }

            bControllerInstalled = false;
            if (LabelPrint != null)
            {
                sLabelControllerPath = Path.Combine(sFolder, @"LabelController");
                if (Directory.Exists(sLabelControllerPath))
                {
                    if (File.Exists(Path.Combine(sLabelControllerPath, "LabelControler.exe")))
                    {
                        bControllerInstalled = true;
                    }
                }
            }


            bDesingerInstalled = false;
            if (LabelPrint != null)
            {
                sLabelDesignerPath = Path.Combine(sFolder, @"LabelDesigner");
                if (Directory.Exists(sLabelDesignerPath))
                {
                    if (File.Exists(Path.Combine(sLabelDesignerPath, "LabelDesigner.exe")))
                    {
                        bDesingerInstalled = true;
                    }
                }
            }

            return true;
        }
    }
}
