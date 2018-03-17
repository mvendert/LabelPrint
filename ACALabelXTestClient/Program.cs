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
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.Remoting;
using System.Net;
using System.IO;
using ACA.Support.Tools.ACAException;

namespace ACA.LabelX
{
    class Program
    {
        static void Main(string[] args)
        {
            UnhandledExceptionManager.AddHandler();

            System.Threading.Thread.CurrentThread.Name = "MT";
            GlobalDataStore.RunningWithConsole = true;

            GlobalDataStore.Logger.Warning("Starting ACA LabelX Test Client...");
            GlobalDataStore.Logger.AutoFlush = true;
            
            //Testing structure on XP/Visa 32 and 64!!!
            //bool bRet = ACA.LabelX.Toolbox.Win32ApiFunctions.DeleteFilesToRecycleBin(@"c:\temp\test.xml");

            Thread thrdBackup = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXClientBackupManager.DoThreadWork));
            thrdBackup.Name = "BK";
            thrdBackup.Start();
            
            Thread thrd1 = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXClientServerManager.DoThreadWork));
            thrd1.Name = "ST";
            thrd1.Start();

            Thread thrd2 = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXRemoteControlManager.DoThreadWork));
            thrd2.Name = "RT";
            thrd2.Start();

            Thread thrd3 = new Thread(new ThreadStart(ACA.LabelX.Managers.LabelXPrintgroupsManager.DoThreadWork));
            thrd3.Name = "PT";
            thrd3.Start();

            Console.ReadLine();
            ACA.LabelX.Managers.LabelXClientBackupManager.Stop();
            ACA.LabelX.Managers.LabelXClientServerManager.Stop();
            ACA.LabelX.Managers.LabelXRemoteControlManager.Stop();
            ACA.LabelX.Managers.LabelXPrintgroupsManager.Stop();

            Console.WriteLine("Stopping threads and waiting on completion.");
            if (thrdBackup.IsAlive)
            {
                if (!thrdBackup.Join(5000))
                {
                    thrdBackup.Abort();
                    Console.WriteLine("BK aborted");
                }
                else
                {
                    Console.WriteLine("BK Stopped");
                }
            }

            if (thrd1.IsAlive)
            {
                if (!thrd1.Join(5000))
                {
                    thrd1.Abort();
                    Console.WriteLine("ST aborted");
                }
                else
                {
                    Console.WriteLine("ST Stopped");
                }
            }

            if (thrd2.IsAlive)
            {
                if (!thrd2.Join(5000)) 
                {
                    thrd2.Abort();
                    Console.WriteLine("RT Aborted");
                } 
                else
                {
                Console.WriteLine("RT Stopped");
                }
            }

            if (thrd3.IsAlive)
            {
                if (!thrd3.Join(5000))
                {
                    thrd3.Abort();
                    Console.WriteLine("PT aborted.");
                }
                else
                {
                    Console.WriteLine("PT Stopped");
                }
            }
            if (thrd1.IsAlive | thrd2.IsAlive | thrd3.IsAlive)
            {
                Environment.Exit(1);
            }
        }
    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
