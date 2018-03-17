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
using System.ServiceProcess;
using System.Threading;
using ACA.LabelX;

namespace ACALabelXClientService
{
    public partial class LabelXClientServ : ServiceBase
    {
        Thread thrd1;
        Thread thrd2;
        Thread thrd3;
        Thread thrdBackup;

        public LabelXClientServ()
        {
            thrd1 = null;
            thrd2 = null;
            thrd3 = null;
            thrdBackup = null;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Threading.Thread.CurrentThread.Name = "MT";

            GlobalDataStore.Logger.Warning("Starting ACA LabelX Client Service...");

            thrdBackup = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXClientBackupManager.DoThreadWork));
            thrdBackup.Name = "BK";
            thrdBackup.Start();

            thrd1 = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXClientServerManager.DoThreadWork));
            thrd1.Name = "ST";
            thrd1.Start();

            thrd2 = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXRemoteControlManager.DoThreadWork));
            thrd2.Name = "RT";
            thrd2.Start();

            thrd3 = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXPrintgroupsManager.DoThreadWork));
            thrd3.Name = "PT";
            thrd3.Start();
        }

        protected override void OnShutdown()
        {
            OnStop();
        }

        protected override void OnStop()
        {
            GlobalDataStore.Logger.Error("Stopping the ACA Labelprint Client");
            //We request each thread to stop.
            if (thrd1 != null)
            {
                ACA.LabelX.Managers.LabelXClientServerManager.Stop();
            }
            if (thrd2 != null)
            {
                ACA.LabelX.Managers.LabelXRemoteControlManager.Stop();
            }
            if (thrd3 != null)
            {
                ACA.LabelX.Managers.LabelXPrintgroupsManager.Stop();
            }
            if (thrdBackup != null)
            {
                ACA.LabelX.Managers.LabelXClientBackupManager.Stop();
            }
            //
            //Wait for each thread to complete the stop. This can take some time.
            if (thrd1 != null)
                if (thrd1.IsAlive)
                {
                    GlobalDataStore.Logger.Info("Waiting for ST to stop.");
                    if (thrd1.Join(10000))
                        GlobalDataStore.Logger.Info("Stopped ST");
                }
            if (thrd2 != null)
                if (thrd2.IsAlive)
                {
                    GlobalDataStore.Logger.Info("Waiting for RT to stop.");
                    if (thrd2.Join(5000))
                        GlobalDataStore.Logger.Info("Stopped RT");
                }
            if (thrd3 != null)
                if (thrd3.IsAlive)
                {
                    GlobalDataStore.Logger.Info("Waiting for PT to stop.");
                    if (thrd3.Join(5000))
                    {
                        GlobalDataStore.Logger.Info("Stopped PT");
                    }
                }
            if (thrdBackup != null)
                if (thrdBackup.IsAlive)
                {
                    GlobalDataStore.Logger.Info("Waiting for BK to stop.");
                    if (thrdBackup.Join(5000))
                    {
                        GlobalDataStore.Logger.Info("Stopped BK");
                    }
                }

            if (thrd1 != null)
                if (thrd1.IsAlive)
                {
                    GlobalDataStore.Logger.Warning("Killed ST");
                    thrd1.Abort();                    
                }
            if (thrd2 != null)
                if (thrd2.IsAlive)
                {
                    GlobalDataStore.Logger.Warning("Killed RT");
                    thrd2.Abort();
                }
            if (thrd3 != null)
                if (thrd3.IsAlive)
                {
                    GlobalDataStore.Logger.Warning("Killed PT");
                    thrd3.Abort();
                }
            if (thrdBackup != null)
                if (thrdBackup.IsAlive)
                {
                    GlobalDataStore.Logger.Warning("Killed BK");
                    thrdBackup.Abort();
                }

            //if (thrd1.IsAlive | thrd2.IsAlive | thrd3.IsAlive)
            //{
            //    System.Environment.Exit(1);
            //}
            if (thrd1 != null)
                if (thrd1.IsAlive)
                {   
                    System.Environment.Exit(1);                    
                }
            if (thrd2 != null)
                if (thrd2.IsAlive)
                {                    
                    System.Environment.Exit(2);                                        
                }
            if (thrd3 != null)
                if (thrd3.IsAlive)
                {
                    System.Environment.Exit(3);                                        
                }
            if (thrdBackup != null)
                if (thrdBackup.IsAlive)
                {
                    System.Environment.Exit(4);
                }
            base.OnStop();
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
