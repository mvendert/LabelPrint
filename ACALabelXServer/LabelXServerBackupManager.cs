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
using ACA.LabelX.Toolbox;

namespace ACA.LabelX.Managers
{
    public class LabelXServerBackupManager
    {
        private static bool moetstoppen;
        private static bool backupResult;
        private static bool backupReady;
        public static void DoThreadWork()
        {
            LabelXServerBackupManager theManager = new LabelXServerBackupManager();
            theManager.Start();
        }

        public static void Stop()
        {
            moetstoppen = true;
        }

        public void Start()
        {
            bool bRet = true;
            backupReady = false;
            ACA.LabelX.Toolbox.BackupFolderstructure backupSystem;
            backupSystem = new BackupFolderstructure();
            backupSystem.ForClient = false;
            backupSystem.ForServer = true;

            GlobalDataStore.Logger.Info("Backup of configuration files starting.");
            backupSystem.BackupNow();
            GlobalDataStore.Logger.Info("Backup of configuration files finished.");
            DateTime lastRun = DateTime.Now;
            while (!moetstoppen)
            {
                System.Threading.Thread.Sleep(1000);
                DateTime currentTime = DateTime.Now;
                TimeSpan span = currentTime - lastRun;
                if (span.TotalHours > 24)
                {
                    GlobalDataStore.Logger.Info("Backup of configuration files starting.");
                    backupSystem.BackupNow();
                    GlobalDataStore.Logger.Info("Backup of configuration files finished.");
                    lastRun = DateTime.Now;
                }
            }
            GlobalDataStore.Logger.Info("Backupworker about to stop.");
            backupReady = true;
            backupResult = bRet;
        }
    }
}
