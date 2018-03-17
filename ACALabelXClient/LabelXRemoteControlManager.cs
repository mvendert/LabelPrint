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
using System.Linq;
using System.Text;
using System.Threading;

namespace ACA.LabelX.Managers
{

    public class LabelXRemoteControlManager
    {
        private static bool moetstoppen = false;

        public LabelXRemoteControlManager()
        {
            
        }
        public static void DoThreadWork()
        {
            LabelXRemoteControlManager theManager;
            theManager = new LabelXRemoteControlManager();
            theManager.Start();
        }
        public static void Stop()
        {
            moetstoppen = true;
        }
        public bool Start()
        {
            bool bRet = true;
            moetstoppen = false;
            string AppPath = GlobalDataStore.AppPath;
            GlobalDataStore.Logger.Warning("Starting the controller listener...");

            try
            {
                ACA.LabelX.ClientEngine.Remote.AcaLabelXClientRemoteEngine eg = new ACA.LabelX.ClientEngine.Remote.AcaLabelXClientRemoteEngine();
                eg.Start(AppPath + @"ACALabelXClientRemote.config.xml");
                GlobalDataStore.Logger.Info("The controller listener is listening.");
                //This thread must be kept allive untit stoppen = true;
                while (!moetstoppen)
                {
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                GlobalDataStore.Logger.Error(string.Format("Error: {0} Target site: {1} Stack trace: {2}", e.Message, e.TargetSite, e.StackTrace));
                bRet = false;
            }
            return bRet;
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
