using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;
using System.ServiceProcess;

namespace ACALP_Config
{
    public partial class frmMainRemove : Form
    {
        string txtIPThisComputer;
        string txtIpNumberServer;
        string txtPortCentralServer;
        string txtPortPrintClient;
        string txtDataFolder;
        string txtUniqueComputerName;
        int    intClientVals;
        int    intServerVals;
        bool   chkClientLogAppend;
        bool   chkServerLogAppend;
        bool   chkStandaloneclient;
        string txtClientPictureFolder;
        string txtServerPictureFolder;
        string  txtLogFolder;

        bool bClientServerviceAfterRemove;
        bool bServerServiceAfterRemove;
        bool bControllerAfterRemove;
        bool bDesingerAfterRemove;
        bool bIkzelfAfterRemove;
        bool bFullRemove = false;

        public frmMainRemove()
        {
            InitializeComponent();
            AskTheQuestion();

            if (bFullRemove)
            {
                bClientServerviceAfterRemove = false;
                bServerServiceAfterRemove = false;
                bControllerAfterRemove = false;
                bDesingerAfterRemove = false;
                bIkzelfAfterRemove = false;
            }
            else
            {
                InstallerPaths myPath = new InstallerPaths();
                if (myPath.LoadInstallerPaths())
                {
                    bClientServerviceAfterRemove = myPath.ClientInstalled;
                    bServerServiceAfterRemove = myPath.ServerInstalled;
                    bControllerAfterRemove = myPath.ControllerInstalled;
                    bDesingerAfterRemove = myPath.DesingerInstalled;
                    bIkzelfAfterRemove = true;
                }
            }
            timer1.Start();
        }

        public frmMainRemove(bool bClientServiceInst, bool bServerServiceInst, bool bControllerInst, bool bDesignerInst, bool bIkzelfInst)
        {
            InitializeComponent();

            InstallerPaths myPath = new InstallerPaths();
            if (myPath.LoadInstallerPaths())
            {
                if ((bClientServiceInst != myPath.ClientInstalled) ||
                    (bServerServiceInst != myPath.ServerInstalled) ||
                    (bControllerInst != myPath.ControllerInstalled) ||
                    (bDesignerInst != myPath.DesingerInstalled))
                {
                    AskTheQuestion();
                }
            }
            else
            {
                bFullRemove = true;
            }
            if (bFullRemove)
            {
                bClientServerviceAfterRemove = bClientServiceInst;
                bServerServiceAfterRemove = bServerServiceInst;
                bControllerAfterRemove = bControllerInst;
                bDesingerAfterRemove = bDesignerInst;
                bIkzelfAfterRemove = bIkzelfInst;
            }
            else
            {
                //By selection the previous install values even on a remove of a component
                //the settings will be preserved and not removed. And this is what we need to accomplish.
                //InstallerPaths myPath = new InstallerPaths();
                if (myPath.LoadInstallerPaths())
                {
                    bClientServerviceAfterRemove = myPath.ClientInstalled;
                    bServerServiceAfterRemove = myPath.ServerInstalled;
                    bControllerAfterRemove = myPath.ControllerInstalled;
                    bDesingerAfterRemove = myPath.DesingerInstalled;
                    bIkzelfAfterRemove = true;
                }
            }
            timer1.Start();
        }

        private bool AskTheQuestion()
        {
            string smes = "Wilt u alle instellingen geforceerd verwijderen?\n" +
                          "Indien u een herinstallatie wil uitvoeren en instellingen zoals printgroepen, labels en mappen nog wil gebruiken, moet u dit niet doen.\n" +
                          "Als u kiest voor instellingen verwijderen, gaan alle instellingen en bestanden definitief verloren.\n" +
                          "Opmerking: De map met de foto's wordt NOOIT verwijderd van de server. Deze moet u altijd handmatig verwijderen indien dit nodig is.\n"+
                          "Instellingen verwijderen?";
            if (MessageBox.Show(smes, "Vraag", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                bFullRemove = true;
            }
            else
            {
                bFullRemove = false;
            }
            return bFullRemove;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false; //never again...
            //Verwijder bestanden
            lblVoortgang.Text = "Bestuderen van instellingen.";
            lblVoortgang.Update();
            progressBar1.Value = 20;
            progressBar1.Update();
            Application.DoEvents();            
            LoadSettings();

            lblVoortgang.Text = "Stoppen van de services...";
            lblVoortgang.Update();
            progressBar1.Value = 20;
            progressBar1.Update();
            Application.DoEvents();
            StopRunningServices();

            lblVoortgang.Text = "Verwijderen van bestanden en mappen.";
            lblVoortgang.Update();
            progressBar1.Value = 30;
            progressBar1.Update();
            Thread.Sleep(1000);
            ExpandAndRemove();

            lblVoortgang.Text = "Verwijderen van registerinstellingen.";
            lblVoortgang.Update();            
            progressBar1.Value = 50;
            progressBar1.Update();            
            Thread.Sleep(1000);

            CleanRegistry();
            progressBar1.Value = 70;
            progressBar1.Update();
            lblVoortgang.Text = "Verwijderen van tijdelijke bestanden.";
            lblVoortgang.Update();            
            Thread.Sleep(1000);
            progressBar1.Value = 90;
            progressBar1.Update();
            //Verwijder..
            lblVoortgang.Text = "Klaar";
            lblVoortgang.Update();
            Thread.Sleep(500);
            progressBar1.Value = 99;
            progressBar1.Update();
            Thread.Sleep(500);
            this.Close();
        }

        private void StopRunningServices()
        {
            ServiceControllerStatus serverStatus;                        
            ServerServiceManager sv1;

            sv1 = new ServerServiceManager();
            serverStatus = sv1.GetServiceStatus();
            if ( (serverStatus != ServiceControllerStatus.Stopped) && (serverStatus != ServiceControllerStatus.StopPending))
            {
                sv1.StopService();
            }

            ClientServiceManager sv2;
            sv2 = new ClientServiceManager();
            serverStatus = sv2.GetServiceStatus();
            if ( (serverStatus != ServiceControllerStatus.Stopped) && (serverStatus != ServiceControllerStatus.StopPending))
            {
                sv2.StopService();
            }            
        }

        private void frmMainRemove_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 10;
            progressBar1.Update();
            LoadSettings();
        }

        private void LoadSettings()
        {
            Properties.Settings st;
            st = new Properties.Settings();
            st.Reload();

            txtIPThisComputer = st.LocalIPNumber;
            txtIpNumberServer = st.ServerIPNumber;
            txtPortCentralServer = st.ServerPort.ToString();
            txtPortPrintClient = st.ClientPort.ToString();
            txtDataFolder = st.BaseFolder;
            txtUniqueComputerName = st.UniqueName;
            intClientVals = st.ClientErrorLevel;
            intServerVals =st.ServerErrorLevel;
            chkClientLogAppend = st.ClientAppend;
            chkServerLogAppend = st.ServerAppend;
            chkStandaloneclient = st.standaloneclient;
            txtClientPictureFolder = st.ClientPictureFolder;
            txtServerPictureFolder= st.ServerPictureFolder;                        
            txtLogFolder = st.baselogfolder;

            RegistryKey key = Application.CommonAppDataRegistry;

            if (key.GetValue("LocalIpNumber") != null)
            {
                txtIPThisComputer = key.GetValue("LocalIpNumber").ToString();
            }
            if (key.GetValue("ServerIPNumber") != null)
            {
                txtIpNumberServer = key.GetValue("ServerIPNumber").ToString();
            }
            if (key.GetValue("ServerPort") != null)
            {
                txtPortCentralServer = key.GetValue("ServerPort").ToString();
            }
            if (key.GetValue("ClientPort") != null)
            {
                txtPortPrintClient = key.GetValue("ClientPort").ToString();
            }
            if (key.GetValue("BaseFolder") != null)
            {
                txtDataFolder = key.GetValue("BaseFolder").ToString();
            }
            if (key.GetValue("baselogfolder") != null)
            {
                txtLogFolder = key.GetValue("baselogfolder").ToString();
            }
            if (key.GetValue("UniqueName") != null)
            {
                txtUniqueComputerName = key.GetValue("UniqueName").ToString();
            }
            if (key.GetValue("ClientErrorLevel") != null)
            {
                intClientVals =((int)key.GetValue("ClientErrorLevel"));
            }
            if (key.GetValue("ServerErrorLevel") != null)
            {
                intServerVals = ((int)key.GetValue("ServerErrorLevel"));
            }
            if (key.GetValue("ClientAppend") != null)
            {
                object o = key.GetValue("ClientAppend");
                chkClientLogAppend = (bool)((string)key.GetValue("ClientAppend") == "True");
            }
            if (key.GetValue("ServerAppend") != null)
            {
                chkServerLogAppend = (bool)((string)key.GetValue("ServerAppend") == "True");
            }
            if (key.GetValue("ClientPictureFolder") != null)
            {
                txtClientPictureFolder = key.GetValue("ClientPictureFolder").ToString();
            }
            if (key.GetValue("ServerPictureFolder") != null)
            {
                txtServerPictureFolder = key.GetValue("ServerPictureFolder").ToString();
            }
            if (key.GetValue("standaloneclient") != null)
            {
                chkStandaloneclient = (bool)((string)key.GetValue("standaloneclient") == "True");
            }
        }

        public void ExpandAndRemove()
        {
            DefaultValues theVal;
            theVal = new DefaultValues();
            theVal.Values["ipnumberserver"].Value = txtIpNumberServer;
            theVal.Values["clientremip"].Value = txtIPThisComputer;
            theVal.Values["portserver"].Value = txtPortCentralServer;
            theVal.Values["clientremport"].Value = txtPortPrintClient;
            theVal.Values["basefolder"].Value = txtDataFolder;
            theVal.Values["identifyingname"].Value = txtUniqueComputerName;
            theVal.Values["clientloglevel"].Value = intClientVals.ToString();
            theVal.Values["serverloglevel"].Value = intServerVals.ToString();
            theVal.Values["clientlogappend"].Value = chkClientLogAppend ? "true" : "false";
            theVal.Values["serverlogappend"].Value = chkServerLogAppend ? "true" : "false";
            theVal.Values["baselogfolder"].Value = txtLogFolder;
            theVal.Values["standaloneclient"].Value = chkStandaloneclient ? "true" : "false";
            theVal.SetInstallerPaths();
            theVal.ExpandAll();

            if (txtClientPictureFolder != "@@DEFAULT@@")
            {
                theVal.Values["clientptrootfolder"].Value = txtClientPictureFolder;
            }

            if (txtServerPictureFolder != "@@DEFAULT@@")
            {
                theVal.Values["serverpicturefolder"].Value = txtServerPictureFolder;
            }

            ExpanderClass exp;
            exp = new ExpanderClass();

            InstallerPaths myPath = new InstallerPaths();
            if (myPath.LoadInstallerPaths())
            {
                exp.SetInstalled(myPath.ClientInstalled, myPath.ServerInstalled, myPath.ControllerInstalled, myPath.DesingerInstalled);
                exp.SetAfterRemove(bClientServerviceAfterRemove, bServerServiceAfterRemove, bControllerAfterRemove, bDesingerAfterRemove);
                exp.DefValues = theVal;
                exp.RemoveAllFiles(theVal);
            }
        }
        public void CleanRegistry()
        {
            bool bClientInst, bServerInst, bControllerInst, bDesignerInst;

            RegistryKey key = Application.CommonAppDataRegistry;
            try
            {
                InstallerPaths myPath = new InstallerPaths();
                if (myPath.LoadInstallerPaths())
                {
                    bClientInst = myPath.ClientInstalled;
                    bServerInst = myPath.ServerInstalled;
                    bControllerInst = myPath.ControllerInstalled;
                    bDesignerInst = myPath.DesingerInstalled;
                } else
                {
                    bClientInst = bServerInst = bDesignerInst = bControllerInst = false;    
                }

                if (! ( bClientServerviceAfterRemove ||
                        bServerServiceAfterRemove ||
                        bControllerAfterRemove ||
                        bDesingerAfterRemove))
                {
                    //Remove All
                    key.DeleteValue("LocalIpNumber");
                    key.DeleteValue("ServerIPNumber");
                    key.DeleteValue("ServerPort");
                    key.DeleteValue("ClientPort");
                    key.DeleteValue("BaseFolder");
                    key.DeleteValue("UniqueName");
                    key.DeleteValue("ClientErrorLevel");
                    key.DeleteValue("ServerErrorLevel");
                    key.DeleteValue("ClientAppend");
                    key.DeleteValue("ServerAppend");
                    key.DeleteValue("ClientPictureFolder");
                    key.DeleteValue("ServerPictureFolder");
                    key.DeleteValue("baselogfolder");
                    key.DeleteValue("standaloneclient");
                } else
                {
                    if  ( (bDesignerInst && (!bDesingerAfterRemove))  ||
                          (bClientInst && (!bClientServerviceAfterRemove)) ||
                          (bServerInst && (!bServerServiceAfterRemove)) ||
                          (bControllerInst && (!bControllerAfterRemove)) )
                    {
                        //we removed something...
                        key.DeleteValue("UniqueName");
                    }                                       
                }                
            }
            catch
            {
                //nop;
            }
        }
    }
}
