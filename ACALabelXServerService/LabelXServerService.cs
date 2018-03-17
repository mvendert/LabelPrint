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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ACA.LabelX;
using System.Threading;


namespace ACALabelXServerService
{
    public partial class LabelXServerService : ServiceBase
    {
        Thread thrd1;
        Thread thrdBackup;

        public LabelXServerService()
        {
            thrd1 = null;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Threading.Thread.CurrentThread.Name = "MT";
            thrd1 = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXServerManager.DoThreadWork));
            thrd1.Name = "SV";
            thrd1.Start();

            thrdBackup = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXServerBackupManager.DoThreadWork));
            thrdBackup.Name = "BK";
            thrdBackup.Start();
        }

        protected override void OnShutdown()
        {
            OnStop();
        }

        protected override void OnStop()
        {
            GlobalDataStore.Logger.Error("Stopping the ACA LabelPrint Server Service");
            if (thrd1 != null)
            {
                ACA.LabelX.Managers.LabelXServerManager.Stop();
            }
            if (thrd1 != null)
                if (thrd1.IsAlive)
                {
                    GlobalDataStore.Logger.Warning("Waiting for SV to stop.");
                    if (thrd1.Join(10000))
                        GlobalDataStore.Logger.Warning("Stopped SV");
                }
            if (thrd1 != null)
                if (thrd1.IsAlive)
                {
                    GlobalDataStore.Logger.Error("Killed SV");
                    thrd1.Abort();
                }

            if (thrdBackup != null)
            {
                ACA.LabelX.Managers.LabelXServerBackupManager.Stop();
            }
            if (thrdBackup != null)
                if (thrdBackup.IsAlive)
                {
                    GlobalDataStore.Logger.Warning("Waiting for BK to stop.");
                    if (thrdBackup.Join(10000))
                        GlobalDataStore.Logger.Warning("Stopped BK");
                }
            if (thrdBackup != null)
                if (thrdBackup.IsAlive)
                {
                    GlobalDataStore.Logger.Error("Killed BK");
                    thrd1.Abort();
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
