using System;
using ACA.LabelX.Toolbox;
using System.Runtime.Remoting;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace ACA.LabelX.Managers
{
    public class LabelXClientBackupManager
    {
        private static bool moetstoppen;        
        private static bool backupResult;
        private static bool backupReady;
        public static void DoThreadWork()
        {
            LabelXClientBackupManager theManager = new LabelXClientBackupManager();
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
            backupSystem.ForClient = true;
            backupSystem.ForServer = false;
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