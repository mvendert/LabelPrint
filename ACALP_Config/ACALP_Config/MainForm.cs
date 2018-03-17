using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.ServiceProcess;
using Microsoft.Win32;
using System.IO;

namespace ACALP_Config
{
    public partial class frmMain : Form
    {
        bool TrackChanges;
        bool IsSomethingChanged;
        bool bUpdateOnly;

        public bool UpdateOnly
        {
            get { return bUpdateOnly; }
            set { bUpdateOnly = value; }
        }

        public frmMain()
        {
            InitializeComponent();
            TrackChanges = false;
            IsSomethingChanged = false;

            if (bUpdateOnly)
            {
                IsSomethingChanged = true;
            }
        }

        public bool IsValidIP(string addr, ref string retIP)
        {
            retIP = string.Empty;
            //create our match pattern
            string pattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            if (!valid)
            {
                //Maybe the entered value is a dns name.
                //try to resolve it.
                IPHostEntry entry = null;
                try
                {
                    entry = Dns.GetHostEntry(addr);
                }
                catch (Exception)
                {
                    valid = false;
                }
                if (entry != null)
                {
                    if (entry.AddressList.Count() > 0)
                    {
                        valid = true;
                        foreach (IPAddress addr2 in entry.AddressList)
                        {
                            //Make sure a IPV4 address is available for this endpoint.
                            if (addr2.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {                                
                                retIP = addr2.ToString();
                                valid = true;
                                break;
                            }
                        }
                    }
                }

            }
            return valid;
        }

        private void butResetToInitialValues_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Al uw huidige instellingen zullen verloren gaan. Verder gaan met het herstellen van de beginwaarden?", "Waarschuwing", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Properties.Settings st;
                st = Properties.Settings.Default;

                txtIPThisComputer.Text = st.LocalIPNumber;
                txtIpNumberServer.Text = st.ServerIPNumber;
                txtPortCentralServer.Text = st.ServerPort.ToString();
                txtPortPrintClient.Text = st.ClientPort.ToString();
                txtDataFolder.Text = st.BaseFolder;
                txtUniqueComputerName.Text = st.UniqueName;
                SetClientVals(st.ClientErrorLevel);
                SetServerVals(st.ServerErrorLevel);
                chkClientLogAppend.Checked = st.ClientAppend;
                chkServerLogAppend.Checked = st.ServerAppend;
                txtClientPictureFolder.Text = st.ClientPictureFolder;
                txtServerPictureFolder.Text = st.ServerPictureFolder;
                standaloneclientcheck.Checked = st.standaloneclient;
                SetServerVals(st.ServerErrorLevel);
                SetClientVals(st.ClientErrorLevel);
                IsSomethingChanged = true;
                SetOKButtons();                
            }
        }

        private void butIPOphalen_Click(object sender, EventArgs e)
        {
            IPAddressHelper test;
            List<IPAddress> theList;
            test = new IPAddressHelper();

            theList = test.GetIpAddressList(Dns.GetHostName());
            if (theList.Count == 1)
            {
                txtIPThisComputer.Text = theList[0].ToString();
            }
            else
            {
                FrmMultipleIp theForm;
                theForm = new FrmMultipleIp(theList);
                if (theForm.ShowDialog() == DialogResult.OK)
                {
                    txtIPThisComputer.Text = theForm.SelectedIPAddress.ToString();
                }
            }
        }


        private void SetServerVals(int p)
        {
            chkServerDebug.Checked =   (p & 0x00000001) != 0;
            chkServerInfo.Checked =    (p & 0x00000002) != 0;
            chkServerSuccess.Checked = (p & 0x00000004) != 0;
            chkServerWarning.Checked = (p & 0x00000008) != 0;
            chkServerError.Checked =   (p & 0x00000010) != 0;
            chkServerFatal.Checked =   (p & 0x00000020) != 0;
        }

        private void SetClientVals(int p)
        {
            chkClientDebug.Checked = (p & 0x00000001) != 0;
            chkClientInfo.Checked = (p & 0x00000002) != 0;
            chkClientSuccess.Checked = (p & 0x00000004) != 0;
            chkClientWarning.Checked = (p & 0x00000008) != 0;
            chkClientError.Checked = (p & 0x00000010) != 0;
            chkClientFatal.Checked = (p & 0x00000020) != 0;            
        }
        private int GetServerVals()
        {
            int retval;
            retval = 0;
            retval = chkServerDebug.Checked ? retval + 0x00000001 : retval;
            retval = chkServerInfo.Checked ? retval + 0x00000002 : retval;
            retval = chkServerSuccess.Checked ? retval + 0x00000004 : retval;
            retval = chkServerWarning.Checked ? retval + 0x00000008 : retval;
            retval = chkServerError.Checked ? retval + 0x00000010 : retval;
            retval = chkServerFatal.Checked ? retval + 0x00000020 : retval;
            return retval;
        }
        private int GetClientVals()
        {
            int retval;
            retval = 0;
            retval = chkClientDebug.Checked ? retval + 0x00000001 : retval;
            retval = chkClientInfo.Checked ? retval + 0x00000002 : retval;
            retval = chkClientSuccess.Checked ? retval + 0x00000004 : retval;
            retval = chkClientWarning.Checked ? retval + 0x00000008 : retval;
            retval = chkClientError.Checked ? retval + 0x00000010 : retval;
            retval = chkClientFatal.Checked ? retval + 0x00000020 : retval;
            return retval;        
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Text = Text + " (" + Application.ProductVersion.ToString() + ")";
            LoadSettings();
            TrackChanges = true;
            SetOKButtons();
            butResetToInitialValues.Enabled = true;
            UpdateServiceStatus();
            timerServiceStatus.Enabled = true;

            InstallerPaths myPath;
            myPath = new InstallerPaths();
            myPath.LoadInstallerPaths();
            chkServer.Checked = myPath.ServerInstalled;
            chkClient.Checked = myPath.ClientInstalled;
            chkController.Checked = myPath.ControllerInstalled;
            chkDesigner.Checked = myPath.DesingerInstalled;

            lbClientRunning.Text = string.Empty;
            lbServerRunning.Text = string.Empty;
            lblServerStatus.Text = string.Empty;
            lblClientStatus.Text = string.Empty;

            if (bUpdateOnly)
            {
                SomethingChanged();
                butOK_Click(sender, e);
            }
        }

        private void chkClientFatal_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void SomethingChanged()
        {
            IsSomethingChanged = true;
            SetOKButtons();
            SetTextColors();
        }
        private bool CheckCanOK()
        {
            if ((txtIpNumberServer.Text == "@@SET@@") ||
                (txtIPThisComputer.Text == "@@SET@@") ||
                (txtUniqueComputerName.Text == "@@SET@@"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void SetOKButtons()
        {
            if (CheckCanOK())
            {
                butApply.Enabled = true;
                butOK.Enabled = true;
                butCancel.Enabled = true;
            }
            else
            {
                butCancel.Enabled = true;
                butApply.Enabled = false;
                butOK.Enabled = false;
            }
            if (!IsSomethingChanged)
            {
                butApply.Enabled = false;
            }
            if (bUpdateOnly)
            {
                butCancel.Enabled = false;
            }
        }
    

        private void chkClientError_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkClientWarning_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkClientSuccess_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkClientInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkClientDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkClientLogAppend_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void txtClientPictureFolder_TextChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void txtIpNumberServer_TextChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void txtIPThisComputer_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtPortCentralServer_TextChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void txtPortPrintClient_TextChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void txtDataFolder_TextChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void txtUniqueComputerName_TextChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkServerFatal_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkServerError_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkServerWarning_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkServerSuccess_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkServerInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkServerDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void chkServerLogAppend_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void txtServerPictureFolder_TextChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
        private void txtUniqueComputerName_Enter(object sender, EventArgs e)
        {
            if (txtUniqueComputerName.Text == "@@SET@@")
            {
                txtUniqueComputerName.Text = string.Empty;
            }                
        }
        private void txtUniqueComputerName_Leave(object sender, EventArgs e)
        {
            txtUniqueComputerName.Text = txtUniqueComputerName.Text.Trim();
            if (txtUniqueComputerName.Text == string.Empty)
            {
                txtUniqueComputerName.Text = "@@SET@@";
            }
        }
        private void txtIPThisComputer_Enter(object sender, EventArgs e)
        {
            if (txtIPThisComputer.Text == "@@SET@@")
            {
                txtIPThisComputer.Text = string.Empty;
            }
        }
        private void txtIPThisComputer_Leave(object sender, EventArgs e)
        {
            lblIPNumberThis.Text = string.Empty;
            txtIPThisComputer.Text = txtIPThisComputer.Text.Trim();
            if (txtIPThisComputer.Text == string.Empty)
            {
                txtIPThisComputer.Text = "@@SET@@";
            }
            else
            {
                string strMes = string.Empty;
                if (IsValidIP(txtIPThisComputer.Text, ref strMes))
                {
                    lblIPNumberThis.Text = strMes;
                }
                else
                {
                    MessageBox.Show(string.Format("{0} is geen geldig ipv4 nummer of de DNS naam is niet oplosbaar (op deze computer)", txtIPThisComputer.Text));
                    txtIPThisComputer.Focus();
                }
            }
        }
        private void txtIpNumberServer_Enter(object sender, EventArgs e)
        {
            if (txtIpNumberServer.Text == "@@SET@@")
            {
                txtIpNumberServer.Text = string.Empty;
            }
        }
        private void txtIpNumberServer_Leave(object sender, EventArgs e)
        {
            lblIPServer.Text = string.Empty;
            txtIpNumberServer.Text = txtIpNumberServer.Text.Trim();
            if (txtIpNumberServer.Text == string.Empty)
            {
                txtIpNumberServer.Text = "@@SET@@";
            }
            else
            {
                string strMes = string.Empty;                
                if (IsValidIP(txtIpNumberServer.Text, ref strMes))
                {
                    lblIPServer.Text = strMes;                    
                }
                else
                {
                    MessageBox.Show(string.Format("{0} is geen geldig ipv4 nummer of de DNS naam is niet oplosbaar (op deze computer)", txtIpNumberServer.Text));
                    txtIpNumberServer.Focus();
                }
            }
        }
        private void SetTextColors()
        {
            if (txtIPThisComputer.Text == "@@SET@@")
            {
                txtIPThisComputer.ForeColor = SystemColors.GrayText;
            }
            else
            {
                txtIPThisComputer.ForeColor = SystemColors.WindowText;
            }
            if (txtUniqueComputerName.Text == "@@SET@@")
            {
                txtUniqueComputerName.ForeColor = SystemColors.GrayText;
            }
            else
            {
                txtUniqueComputerName.ForeColor = SystemColors.WindowText;
            }
            if (txtIpNumberServer.Text == "@@SET@@")
            {
                txtIpNumberServer.ForeColor = SystemColors.GrayText;
            }
            else
            {
                txtIpNumberServer.ForeColor = SystemColors.WindowText;
            }
            if (txtClientPictureFolder.Text == "@@DEFAULT@@")
            {
                txtClientPictureFolder.ForeColor = SystemColors.GrayText;
            }
            else
            {
                txtClientPictureFolder.ForeColor = SystemColors.WindowText;
            }
            if (txtServerPictureFolder.Text == "@@DEFAULT@@")
            {
                txtServerPictureFolder.ForeColor = SystemColors.GrayText;
            }
            else
            {
                txtServerPictureFolder.ForeColor = SystemColors.WindowText;
            }
        }
        private void txtClientPictureFolder_Enter(object sender, EventArgs e)
        {
            if (txtClientPictureFolder.Text == "@@DEFAULT@@")
            {
                txtClientPictureFolder.Text = string.Empty;
            }
        }
        private void txtClientPictureFolder_Leave(object sender, EventArgs e)
        {
            txtClientPictureFolder.Text = txtClientPictureFolder.Text.Trim();
            if (txtClientPictureFolder.Text == string.Empty)
            {
                txtClientPictureFolder.Text = "@@DEFAULT@@";
            }
        }
        private void txtServerPictureFolder_Enter(object sender, EventArgs e)
        {
            if (txtServerPictureFolder.Text == "@@DEFAULT@@")
            {
                txtServerPictureFolder.Text = string.Empty;
            }
        }
        private void txtServerPictureFolder_Leave(object sender, EventArgs e)
        {
            txtServerPictureFolder.Text = txtServerPictureFolder.Text.Trim();
            if (txtServerPictureFolder.Text == string.Empty)
            {
                txtServerPictureFolder.Text = "@@DEFAULT@@";
            }
        }
        private void timerServiceStatus_Tick(object sender, EventArgs e)
        {
            UpdateServiceStatus();
        }
        private void UpdateServiceStatus()
        {
            ClientServiceManager clientService;
            ServerServiceManager serverService;
            ServiceControllerStatus clientStatus;
            ServiceControllerStatus serverStatus;

            clientStatus = ServiceControllerStatus.Stopped;
            serverStatus = ServiceControllerStatus.Stopped;

            clientService = new ClientServiceManager();
            serverService = new ServerServiceManager();

            if (clientService.ServiceExists())
            {
                clientStatus = clientService.GetServiceStatus();
                lblClientStatus.Text = clientStatus.ToString();

                switch (clientStatus)
                {
                    case ServiceControllerStatus.StopPending:
                    case ServiceControllerStatus.ContinuePending:
                    case ServiceControllerStatus.StartPending:
                    case ServiceControllerStatus.PausePending:
                        lblClientStatus.Text = clientStatus.ToString() + ". Wachten...";
                        butStartStopClient.Text = "Wait...";
                        butStartStopClient.Enabled = false;
                        butRestartClient.Enabled = false; 
                        break;                                        

                    case ServiceControllerStatus.Stopped:
                        if (ProcessExists("ACALabelXClientService"))
                        {
                            lblClientStatus.Text = "Stopping. (programma)";
                            butStartStopClient.Text = "Wacht...";
                            butStartStopClient.Enabled = false;
                            butRestartClient.Enabled = false;                         
                        } else
                        {
                            butStartStopClient.Text = "Start";
                            butStartStopClient.Enabled = true;
                            butRestartClient.Enabled = false;                                                                                      
                        }
                        break;
                    case ServiceControllerStatus.Paused:
                        lblClientStatus.Text = clientStatus.ToString() + ". Wacht op systeembeheer.";
                        butStartStopClient.Text = "Wacht...";
                        butStartStopClient.Enabled = false;
                        butRestartClient.Enabled = false;                         
                        break;                                                            
                    case ServiceControllerStatus.Running:
                        lblClientStatus.Text = string.Empty;
                        butStartStopClient.Text = "Stop";
                        butRestartClient.Enabled = true;
                        butStartStopClient.Enabled = true;
                        break;
                }

            }
            else
            {
                lblClientStatus.Text = "Niet geinstalleerd op dit systeem";
                butStartStopClient.Enabled = false;
                butRestartClient.Enabled = false;
            }

            if (serverService.ServiceExists())
            {
                serverStatus = serverService.GetServiceStatus();
                lblServerStatus.Text = serverStatus.ToString();

                switch (serverStatus)
                {
                    case ServiceControllerStatus.StopPending:
                    case ServiceControllerStatus.ContinuePending:
                    case ServiceControllerStatus.StartPending:
                    case ServiceControllerStatus.PausePending:
                        lblServerStatus.Text = clientStatus.ToString() + ". Wachten...";
                        butStartStopServer.Text = "Wait...";
                        butStartStopServer.Enabled = false;
                        butRestartServer.Enabled = false;
                        break;

                    case ServiceControllerStatus.Stopped:
                        if (ProcessExists("ACALabelXServerService"))
                        {
                            lblServerStatus.Text = "Stopping. (programma)";
                            butStartStopServer.Text = "Wacht...";
                            butStartStopServer.Enabled = false;
                            butRestartServer.Enabled = false;
                        }
                        else
                        {
                            butStartStopServer.Text = "Start";
                            butStartStopServer.Enabled = true;
                            butRestartServer.Enabled = false;
                        }
                        break;
                    case ServiceControllerStatus.Paused:
                        lblServerStatus.Text = clientStatus.ToString() + ". Wacht op systeembeheer.";
                        butStartStopServer.Text = "Wacht...";
                        butStartStopServer.Enabled = false;
                        butRestartServer.Enabled = false;
                        break;
                    case ServiceControllerStatus.Running:
                        lblServerStatus.Text = string.Empty;
                        butStartStopServer.Text = "Stop";
                        butRestartServer.Enabled = true;
                        butStartStopServer.Enabled = true;
                        break;
                }
            }
            else
            {
                lblServerStatus.Text = "Niet geinstalleerd op dit systeem";
                butStartStopServer.Enabled = false;
                butRestartServer.Enabled = false;
            }

            if (butRestartClient.Enabled && butRestartServer.Enabled)
            {
                butRestartAll.Enabled = true;
            }
            else
            {
                butRestartAll.Enabled = false;
            }
            UpdateProcessStatus();
        }

        private bool ProcessExists(string ProcessName)
        {
            System.Diagnostics.Process[] processes;
            processes = System.Diagnostics.Process.GetProcessesByName(ProcessName);
            if (processes.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UpdateProcessStatus()
        {
            System.Diagnostics.Process[] processes;
                
            processes = System.Diagnostics.Process.GetProcessesByName("ACALabelXClientService");
            if (processes.Length == 0)
            {                
                pictureBoxClient.Image = imageListImages.Images[1];
            }
            else
            {            
                pictureBoxClient.Image = imageListImages.Images[2];
            }
            processes = System.Diagnostics.Process.GetProcessesByName("ACALabelXServerService");
            if (processes.Length == 0)
            {
                pictureBoxServer.Image = imageListImages.Images[1];
            }
            else
            {
                pictureBoxServer.Image = imageListImages.Images[2];
            }

            //processes = System.Diagnostics.Process.GetProcesses();
            //foreach (System.Diagnostics.Process p in processes)
            // {
            //    System.Diagnostics.Debug.WriteLine(p.ProcessName);
            // }
        }


        private void butStartStopClient_Click(object sender, EventArgs e)
        {
            if (butApply.Enabled)
            {
                if (MessageBox.Show("Er zijn nog instellingen die eerst toegepast moeten worden. Nu toepassen?", "Vraag", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    butApply_Click(sender, e);
                }
                else
                {
                    return;
                }
            }
            this.Cursor = Cursors.WaitCursor;
            butStartStopClient.Enabled = false;
            ClientServiceManager clientService;
            clientService = new ClientServiceManager();
            ServiceControllerStatus clientStatus;
            clientStatus = clientService.GetServiceStatus();
            if ((clientStatus == ServiceControllerStatus.Stopped) ||
                 (clientStatus == ServiceControllerStatus.StopPending))
            {
                clientService.StartService();
            }
            else
            {
                clientService.StopService();
            }
            this.Cursor = Cursors.Default;
        }
        private void butStartStopService_Click(object sender, EventArgs e)
        {
            if (butApply.Enabled)
            {
                if (MessageBox.Show("Er zijn nog instellingen die eerst toegepast moeten worden. Nu toepassen?", "Vraag", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    butApply_Click(sender, e);
                }
                else
                {
                    return;
                }
            }
            this.Cursor = Cursors.WaitCursor;
            butStartStopServer.Enabled = false;
            ServerServiceManager serverService;
            serverService = new ServerServiceManager();
            ServiceControllerStatus serverStatus;
            serverStatus = serverService.GetServiceStatus();
            if ((serverStatus == ServiceControllerStatus.Stopped) ||
                 (serverStatus == ServiceControllerStatus.StopPending))
            {
                serverService.StartService();
            }
            else
            {
                serverService.StopService();
            }
            this.Cursor = Cursors.Default;
        }
        private void butRestartServer_Click(object sender, EventArgs e)
        {
            if (butApply.Enabled)
            {
                if (MessageBox.Show("Er zijn nog instellingen die eerst toegepast moeten worden. Nu toepassen?", "Vraag", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    butApply_Click(sender, e);
                }
            }
            else
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            ServerServiceManager serverService;
            serverService = new ServerServiceManager();
            ServiceControllerStatus serverStatus;
            serverStatus = serverService.GetServiceStatus();
            if ((serverStatus == ServiceControllerStatus.Stopped) ||
                 (serverStatus == ServiceControllerStatus.StopPending))
            {
                serverService.StartService();
            }
            else
            {
                serverService.RestartService();
            }
            this.Cursor = Cursors.Default;
        }
        private void butRestartClient_Click(object sender, EventArgs e)
        {
            if (butApply.Enabled)
            {
                if (MessageBox.Show("Er zijn nog instellingen die eerst toegepast moeten worden. Nu toepassen?", "Vraag", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    butApply_Click(sender, e);
                }
            }
            else
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            ClientServiceManager clientService;
            clientService = new ClientServiceManager();
            ServiceControllerStatus clientStatus;
            clientStatus = clientService.GetServiceStatus();
            if ((clientStatus == ServiceControllerStatus.Stopped) ||
                 (clientStatus == ServiceControllerStatus.StopPending))
            {
                clientService.StartService();
            }
            else
            {
                clientService.RestartService();
            }
            this.Cursor = Cursors.Default;
        }
        private void butCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void butApply_Click(object sender, EventArgs e)
        {
            if (IsSomethingChanged)
            {
                if (ValidateContent())
                {
                    SaveSettings();
                    IsSomethingChanged = false;
                    SetOKButtons();
                }
            }        
        }
        private void butOK_Click(object sender, EventArgs e)
        {
            if (IsSomethingChanged)
            {
                if (ValidateContent())
                {
                    SaveSettings();
                    IsSomethingChanged = false;
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void SaveSettings()
        {
            Properties.Settings st;
            st = new Properties.Settings();
            if (ValidateContent())
            {
                RegistryKey key = Application.CommonAppDataRegistry;
                key.SetValue("LocalIpNumber", txtIPThisComputer.Text);
                key.SetValue("ServerIPNumber", txtIpNumberServer.Text);
                key.SetValue("ServerPort", int.Parse(txtPortCentralServer.Text));
                key.SetValue("ClientPort", int.Parse(txtPortPrintClient.Text));
                key.SetValue("BaseFolder", txtDataFolder.Text);
                key.SetValue("UniqueName", txtUniqueComputerName.Text);
                key.SetValue("ClientErrorLevel", GetClientVals());
                key.SetValue("ServerErrorLevel", GetServerVals());
                key.SetValue("ClientAppend", chkClientLogAppend.Checked);
                key.SetValue("ServerAppend", chkServerLogAppend.Checked);
                key.SetValue("ClientPictureFolder", txtClientPictureFolder.Text);
                key.SetValue("ServerPictureFolder", txtServerPictureFolder.Text);
                key.SetValue("baselogfolder", txtLogFolder.Text);
                key.SetValue("standaloneclient", standaloneclientcheck.Checked); 

                DefaultValues theVal;
                theVal = new DefaultValues();
                theVal.Values["ipnumberserver"].Value = txtIpNumberServer.Text;
                theVal.Values["clientremip"].Value =  txtIPThisComputer.Text;
                theVal.Values["portserver"].Value =  txtPortCentralServer.Text;
                theVal.Values["clientremport"].Value =  txtPortPrintClient.Text;
                theVal.Values["basefolder"].Value =  txtDataFolder.Text;
                theVal.Values["identifyingname"].Value =  txtUniqueComputerName.Text;
                theVal.Values["clientloglevel"].Value =  GetClientVals().ToString();
                theVal.Values["serverloglevel"].Value =  GetServerVals().ToString();
                theVal.Values["clientlogappend"].Value =  chkClientLogAppend.Checked ? "true":"false";
                theVal.Values["serverlogappend"].Value =  chkServerLogAppend.Checked ? "true":"false";
                theVal.Values["baselogfolder"].Value = txtLogFolder.Text;
                theVal.Values["standaloneclient"].Value = standaloneclientcheck.Checked ? "true" : "false";

                theVal.SetInstallerPaths();
                theVal.ExpandAll();

                if (txtClientPictureFolder.Text != "@@DEFAULT@@")
                {
                    theVal.Values["clientptrootfolder"].Value =  txtClientPictureFolder.Text;
                }
                
                if (txtServerPictureFolder.Text != "@@DEFAULT@@")
                {
                    theVal.Values["picturesrootfolder"].Value = txtServerPictureFolder.Text;
                }

                ExpanderClass exp;
                exp = new ExpanderClass();

                InstallerPaths myPath = new InstallerPaths();
                if (myPath.LoadInstallerPaths())
                {
                    exp.SetInstalled(myPath.ClientInstalled, myPath.ServerInstalled,myPath.ControllerInstalled, myPath.DesingerInstalled);
                    exp.DefValues = theVal;                
                    exp.ExpandAllFiles(theVal);
                }
            }

        }

        private void LoadSettings()
        {
            Properties.Settings st;
            st = new Properties.Settings();
            st.Reload();

            txtIPThisComputer.Text = st.LocalIPNumber;
            txtIpNumberServer.Text = st.ServerIPNumber;
            txtPortCentralServer.Text = st.ServerPort.ToString();
            txtPortPrintClient.Text = st.ClientPort.ToString();
            txtDataFolder.Text = st.BaseFolder;
            txtUniqueComputerName.Text = st.UniqueName;
            SetClientVals(st.ClientErrorLevel);
            SetServerVals(st.ServerErrorLevel);
            chkClientLogAppend.Checked = st.ClientAppend;
            chkServerLogAppend.Checked = st.ServerAppend;
            txtClientPictureFolder.Text = st.ClientPictureFolder;
            txtServerPictureFolder.Text = st.ServerPictureFolder;
            SetServerVals(st.ServerErrorLevel);
            SetClientVals(st.ClientErrorLevel);
            txtLogFolder.Text = st.baselogfolder;
            standaloneclientcheck.Checked = st.standaloneclient;

            RegistryKey key = Application.CommonAppDataRegistry;

            #region MoveRegistrySettingsAfterUpdate            
            //*****************************************************************************************************
            //Oeps... the CommonAppDateRegisty is version dependant. If we update to a new version we have to copy
            //the old settings to the new registry place. This is done in this peace of code
            if (bUpdateOnly)
            {
                string name = key.Name;
                //Remove local machine and version from keyname 
                name = name.Remove(0, @"HKEY_LOCAL_MACHINE\".Length);
                int pos = name.LastIndexOf("\\");
                name = name.Remove(pos, name.Length - pos);
                RegistryKey importKey = Registry.LocalMachine.OpenSubKey(name);
                //

                //Each version leaves it subkeys in this parentkey
                //BUT after a remove of a version - for example we have 1.4 -> 1.5 -> 2.0
                //and we remove 2.0.0.0 the key 2.0.0.0 still exists, but there are no subkeys.
                //If we reinstall 1.5 and then update, we want to use the 1.5 keys.
                
                string[] namelist = importKey.GetSubKeyNames();
                Version highest = null;                
                Version tocheck = null;

                for (int i = 0; i < namelist.Count(); i++)
                {
                    RegistryKey vKey = importKey.OpenSubKey(namelist[i]);
                    if (vKey.GetValue("LocalIpNumber") == null)
                    {
                        // this is not a valid version
                        namelist[i] = string.Empty;
                    }
                }
                foreach (string sk in namelist)
                {
                    if (sk.Length > 0) //skip the invalid ones.
                    {
                        if (highest == null)
                        {
                            highest = new Version(sk);                         
                        }
                        tocheck = new Version(sk);
                        if (tocheck > highest)
                        {                    
                            highest = tocheck;
                        }
                    }
                }
                if (highest != null)
                {
                    RegistryKey versionKey = importKey.OpenSubKey(highest.ToString());
                    if (versionKey.GetValue("LocalIpNumber") != null)
                    {
                        txtIPThisComputer.Text = versionKey.GetValue("LocalIpNumber").ToString();
                    }
                    if (versionKey.GetValue("ServerIPNumber") != null)
                    {
                        txtIpNumberServer.Text = versionKey.GetValue("ServerIPNumber").ToString();
                    }
                    if (versionKey.GetValue("ServerPort") != null)
                    {
                        txtPortCentralServer.Text = versionKey.GetValue("ServerPort").ToString();
                    }
                    if (versionKey.GetValue("ClientPort") != null)
                    {
                        txtPortPrintClient.Text = versionKey.GetValue("ClientPort").ToString();
                    }
                    if (versionKey.GetValue("BaseFolder") != null)
                    {
                        txtDataFolder.Text = versionKey.GetValue("BaseFolder").ToString();
                    }
                    if (versionKey.GetValue("baselogfolder") != null)
                    {
                        txtLogFolder.Text = versionKey.GetValue("baselogfolder").ToString();
                    }
                    if (versionKey.GetValue("UniqueName") != null)
                    {
                        txtUniqueComputerName.Text = versionKey.GetValue("UniqueName").ToString();
                    }
                    if (versionKey.GetValue("ClientErrorLevel") != null)
                    {
                        SetClientVals((int)versionKey.GetValue("ClientErrorLevel"));
                    }
                    if (versionKey.GetValue("ServerErrorLevel") != null)
                    {
                        SetServerVals((int)versionKey.GetValue("ServerErrorLevel"));
                    }
                    if (versionKey.GetValue("ClientAppend") != null)
                    {
                        object o = versionKey.GetValue("ClientAppend");
                        chkClientLogAppend.Checked = (bool)((string)versionKey.GetValue("ClientAppend") == "True");
                    }
                    if (versionKey.GetValue("ServerAppend") != null)
                    {
                        chkServerLogAppend.Checked = (bool)((string)versionKey.GetValue("ServerAppend") == "True");
                    }
                    if (versionKey.GetValue("ClientPictureFolder") != null)
                    {
                        txtClientPictureFolder.Text = versionKey.GetValue("ClientPictureFolder").ToString();
                    }
                    if (versionKey.GetValue("ServerPictureFolder") != null)
                    {
                        txtServerPictureFolder.Text = versionKey.GetValue("ServerPictureFolder").ToString();
                    }
                    if (versionKey.GetValue("standaloneclient") != null)
                    {
                        standaloneclientcheck.Checked = (bool)((string)versionKey.GetValue("standalone") == "True");
                    }
                    versionKey.Close();
                    importKey.Close();
                    key.SetValue("LocalIpNumber", txtIPThisComputer.Text);
                    key.SetValue("ServerIPNumber", txtIpNumberServer.Text);
                    key.SetValue("ServerPort", int.Parse(txtPortCentralServer.Text));
                    key.SetValue("ClientPort", int.Parse(txtPortPrintClient.Text));
                    key.SetValue("BaseFolder", txtDataFolder.Text);
                    key.SetValue("UniqueName", txtUniqueComputerName.Text);
                    key.SetValue("ClientErrorLevel", GetClientVals());
                    key.SetValue("ServerErrorLevel", GetServerVals());
                    key.SetValue("ClientAppend", chkClientLogAppend.Checked);
                    key.SetValue("ServerAppend", chkServerLogAppend.Checked);
                    key.SetValue("ClientPictureFolder", txtClientPictureFolder.Text);
                    key.SetValue("ServerPictureFolder", txtServerPictureFolder.Text);
                    key.SetValue("baselogfolder", txtLogFolder.Text);
                    key.SetValue("standaloneclient", standaloneclientcheck.Checked);
                }
            }

            #endregion
            if (key.GetValue("LocalIpNumber") != null)
            {
                txtIPThisComputer.Text = key.GetValue("LocalIpNumber").ToString();
            }
            if (key.GetValue("ServerIPNumber") != null)
            {
                txtIpNumberServer.Text = key.GetValue("ServerIPNumber").ToString();
            }
            if (key.GetValue("ServerPort") != null)
            {
                txtPortCentralServer.Text = key.GetValue("ServerPort").ToString();
            }
            if (key.GetValue("ClientPort") != null)
            {
                txtPortPrintClient.Text = key.GetValue("ClientPort").ToString();
            }
            if (key.GetValue("BaseFolder") != null)
            {
                txtDataFolder.Text = key.GetValue("BaseFolder").ToString();
            }
            if (key.GetValue("baselogfolder") != null)
            {
                txtLogFolder.Text = key.GetValue("baselogfolder").ToString();
            }
            if (key.GetValue("UniqueName") != null)
            {
                txtUniqueComputerName.Text = key.GetValue("UniqueName").ToString();
            }
            if (key.GetValue("ClientErrorLevel") != null)
            {
                SetClientVals((int)key.GetValue("ClientErrorLevel"));
            }
            if (key.GetValue("ServerErrorLevel") != null)
            {
                SetServerVals((int)key.GetValue("ServerErrorLevel"));
            } 
            if (key.GetValue("ClientAppend") != null)
            {
                object o = key.GetValue("ClientAppend");
                chkClientLogAppend.Checked = (bool)((string)key.GetValue("ClientAppend")=="True");
            }
            if (key.GetValue("ServerAppend") != null)
            {
                chkServerLogAppend.Checked = (bool)((string)key.GetValue("ServerAppend")=="True");
            }
            if (key.GetValue("ClientPictureFolder") != null)
            {
                txtClientPictureFolder.Text = key.GetValue("ClientPictureFolder").ToString();
            }
            if (key.GetValue("ServerPictureFolder") != null)
            {
                txtServerPictureFolder.Text = key.GetValue("ServerPictureFolder").ToString();
            }
            if (key.GetValue("standaloneclient") != null)
            {
                standaloneclientcheck.Checked = (bool)((string)key.GetValue("standaloneclient") == "True");
            }

            SetOKButtons();
            SetTextColors();
        }

        private bool ValidateContent()
        {
            bool bRet = true;

            TrackChanges = false;
            txtDataFolder.Text = txtDataFolder.Text.Trim();
            txtLogFolder.Text = txtLogFolder.Text.Trim();
            txtClientPictureFolder.Text = txtClientPictureFolder.Text.Trim();
            txtIpNumberServer.Text = txtIpNumberServer.Text.Trim();
            txtIPThisComputer.Text = txtIPThisComputer.Text.Trim();
            txtPortCentralServer.Text = txtPortCentralServer.Text.Trim();
            txtPortPrintClient.Text = txtPortPrintClient.Text.Trim();
            txtServerPictureFolder.Text = txtServerPictureFolder.Text.Trim();
            txtUniqueComputerName.Text = txtUniqueComputerName.Text.Trim();            
            TrackChanges = true;
            try
            {
                int port = int.Parse(txtPortCentralServer.Text);
                if (port != 18080)
                {
                    MessageBox.Show("Serverpoort afwijkend van de standaard.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Serverpoort foutief", "Error", MessageBoxButtons.OK);
                bRet = false;
            }

            try
            {
                int port = int.Parse(txtPortPrintClient.Text);
                if (port != 18081)
                {
                    MessageBox.Show("Poort printcomputer afwijkend van de standaard.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Poort printcomputer foutief", "Error", MessageBoxButtons.OK);
                bRet = false;
            }
            string strMes = string.Empty;
            if (!IsValidIP(txtIPThisComputer.Text, ref strMes))
            {
                MessageBox.Show("Het veld ipnummer van deze computer bevat geen geldig ipv4-nummer of de DNS naam is niet oplosbaar. Controller of het correct is.", "Warning", MessageBoxButtons.OK);
            }
            if (!IsValidIP(txtIpNumberServer.Text, ref strMes))
            {
                MessageBox.Show("Het ipnummer van de server bevat geen gelding IPv4-nummer of de DNS naam is niet oplosbaar. Controlller of het correct is.", "Warning", MessageBoxButtons.OK);
            }
            if (!System.IO.Directory.Exists(txtDataFolder.Text))
            {
                MessageBox.Show(string.Format("Momenteel bestaat de map {0} niet, of is geen lokale map maar een netwerk map.",txtDataFolder.Text), "Warning", MessageBoxButtons.OK);
                return false;
            }
            if (!System.IO.Directory.Exists(txtLogFolder.Text))
            {
                MessageBox.Show(string.Format("Momenteel bestaat de map {0} niet, of is geen lokale map maar een netwerk map.", txtLogFolder.Text), "Warning", MessageBoxButtons.OK);
                return false;
            }
            
            System.IO.FileInfo fi;
            bool bIsNetworkDrive = false;
            try
            {
                fi = new FileInfo(txtDataFolder.Text);
                DirectoryInfo dir;
                dir = new System.IO.DirectoryInfo(fi.DirectoryName);
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    if (dir.Root.FullName.Equals(d.Name))
                    {
                        if (d.DriveType == DriveType.Network)
                        {
                            bIsNetworkDrive = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("De basismap van de gegevens moet een lokale map zijn waarop je lees en schrijfrechten hebt.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bRet = false;
            }

            if (bIsNetworkDrive)
            {
                MessageBox.Show("Je mag ALLEEN een lokale map selecteren als datamap voor LabelPrint!");
                bRet = false;
            }
        
            return bRet;
        }

        private void butServerPictureFolder_Click(object sender, EventArgs e)
        {
            if (txtServerPictureFolder.Text == "@@DEFAULT@@")
            {
                folderBrowserDialog1.SelectedPath = txtDataFolder.Text;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (folderBrowserDialog1.SelectedPath.ToLower() == txtDataFolder.Text.ToLower())
                    {
                        txtServerPictureFolder.Text = "@@DEFAULT@@";
                    }
                    else
                    {
                        txtServerPictureFolder.Text = folderBrowserDialog1.SelectedPath;
                    }
                    SomethingChanged();
                }
            }
            else
            {
                MessageBox.Show("Eerst het ingave veld leeg maken (op default zetten)");
            }
            
        }

        private void butSelectFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtDataFolder.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDataFolder.Text = folderBrowserDialog1.SelectedPath;
            }
            SomethingChanged();
        }

        private void butClientPictFolder_Click(object sender, EventArgs e)
        {
            if (txtClientPictureFolder.Text == "@@DEFAULT@@")
            {
                folderBrowserDialog1.SelectedPath = txtDataFolder.Text;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (folderBrowserDialog1.SelectedPath.ToLower() == txtDataFolder.Text.ToLower())
                    {
                        txtClientPictureFolder.Text = "@@DEFAULT@@";
                    }
                    else
                    {
                        txtClientPictureFolder.Text = folderBrowserDialog1.SelectedPath;
                    }
                    SomethingChanged();
                }
            }
            else
            {
                MessageBox.Show("Eerst het ingaveveld leegmaken (op default zetten)!");
            }
        }

        private void butRetrieveName_Click(object sender, EventArgs e)
        {
            txtUniqueComputerName.Text = System.Environment.MachineName;
            MessageBox.Show("U mag deze naam alleen gebruiken als u zeker bent dat hij uniek is voor alle computers binnen uw bedrijf", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tabStats_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtLogFolder.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLogFolder.Text = folderBrowserDialog1.SelectedPath;
            }
            SomethingChanged();
        }

        private void txtLogFolder_TextChanged(object sender, EventArgs e)
        {
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(txtDataFolder.Text);
            }
            catch (Exception E3)
            {
                MessageBox.Show(E3.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(txtLogFolder.Text);
            }
            catch (Exception E3)
            {
                MessageBox.Show(E3.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butRestartAll_Click(object sender, EventArgs e)
        {
            if (butApply.Enabled)
            {
                if (MessageBox.Show("Er zijn nog instellingen die eerst toegepast moeten worden. Nu toepassen?", "Vraag", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    butApply_Click(sender, e);
                }
            }
            else
            {
                return;
            }

            butRestartServer_Click(sender, e);
            butRestartClient_Click(sender, e);
        }

        private void txtIPThisComputer_Leave_1(object sender, EventArgs e)
        {

        }

        private void txtUniqueComputerName_Enter_1(object sender, EventArgs e)
        {
            if (txtUniqueComputerName.Text == "@@SET@@")
            {
                txtUniqueComputerName.Text = string.Empty;
            }
        }

        private void txtUniqueComputerName_Leave_1(object sender, EventArgs e)
        {
            if (txtUniqueComputerName.Text == string.Empty)
            {
                txtUniqueComputerName.Text = "@@SET@@";
            }
        }

        private void standaloneclientcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (standaloneclientcheck.Checked)
            {
                txtIPThisComputer.Text = "127.0.0.1";
                txtIPThisComputer.Enabled = false;
                txtIpNumberServer.Text = "127.0.0.1";
                txtIpNumberServer.Enabled = false;
            }
            else
            {
                lblIPNumberThis.Text = string.Empty;
                txtIPThisComputer.Text = "@@SET@@";
                txtIPThisComputer.Enabled = true;
                txtIpNumberServer.Text = "@@SET@@";
                txtIpNumberServer.Enabled = true;
            }
            if (TrackChanges)
            {
                SomethingChanged();
            }
        }
    }
}
