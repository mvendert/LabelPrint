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
using System.Configuration;
using System.Reflection;

namespace ACA.LabelX
{
    public class ClientConfigLock
    {
        public bool locked;
    }
    public class GlobalDataStore
    {
        public static ClientConfigLock LockClass = new ClientConfigLock();
        private static bool created = false;
        private static GlobalDataStore single;
        private static string appPath;
        private static bool runningWithConsole = false;
        private static bool mustresendinfo = false;
        private static ACA.LabelX.Toolbox.ACALabelXSimpleLogging globLogfile = null;

        private static string logFilename = string.Empty;
        private static bool logAppend = true;
        private static uint logLevel = (uint)ACA.LabelX.Toolbox.ACALabelXSimpleLogging.Level.All; //Set logging level
        private static double programVersion = 0.05;

        private bool mDesignMode;
        private static int languageId = 1033; //Engels
        //private static int languageId = 1043; //Nederlands
        
        private static bool isStandAlone = false;
        public static bool IsStandAlone
        {
            get { return isStandAlone; }
            set { isStandAlone = value; }

        }
        private static bool mustWriteStandAlonePrintgroups = true;
        public static bool MustWriteStandAlonePrintGroups
        {
            get { return mustWriteStandAlonePrintgroups; }
            set { mustWriteStandAlonePrintgroups = value; }
        }        
        public static int LanguageId
        {
            get { return GlobalDataStore.languageId; }
            set { GlobalDataStore.languageId = value; }
        }

        public static void ResendInfo()
        {
            mustresendinfo = true;
        }
        public static void ResetResendInfo()
        {
            mustresendinfo = false;
        }

        public static bool MustResendInfo
        {
            get
            {
                return mustresendinfo;
            }
        }

        public static bool RunningWithConsole
        {
            set
            {
                runningWithConsole = value;
            }
        }

        public bool DesignMode
        {
            get { return mDesignMode; }
            set { mDesignMode = value; }
        }

        private GlobalDataStore()
        {
            single = this;
            created = true;
            mDesignMode = false;
            appPath = string.Empty;
        }

        public static GlobalDataStore GetInstance()
        {
            if (!created)
            {
                single = new GlobalDataStore();
                created = true;
            }
            return single;
        }
        public static string GetAppSetting(string sPropName)
        {
            AppSettingsReader reader;
            string sRet;
            reader = new AppSettingsReader();
            sRet = null;
            try
            {
                sRet = reader.GetValue(sPropName, typeof(string)).ToString();
            }
            catch (System.Exception)
            {
                sRet = null;
            }
            return sRet;
        }
        public static string GetAppPath()
        {
            GlobalDataStore theStore;
            theStore = GlobalDataStore.GetInstance(); // construct if not constructed...

            if (appPath == string.Empty)
            {
                appPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                appPath += @"\";
            }
            return appPath;
        }

        public static string AppPath
        {
            get
            {
                return GetAppPath();
            }
        }

        public static ACA.LabelX.Toolbox.ACALabelXSimpleLogging Logger
        {
            get
            {
                if (globLogfile == null)
                {
                    string sHelp;
                    sHelp = GetAppSetting("logfile");
                    if (sHelp != null)
                    {
                        logFilename = sHelp;
                    }
                    else
                    {
                        logFilename = @"C:\ACA\ACALabelX.Log.txt";
                    }
                    sHelp = GetAppSetting("append");
                    if (sHelp != null)
                    {
                        if (sHelp.Equals("true",StringComparison.OrdinalIgnoreCase))
                        {
                            logAppend = true;
                        } 
                        else
                        {
                            logAppend = false;
                        }
                    }
                    sHelp = GetAppSetting("loglevel");

                    if (sHelp != null)
                    {
                        if (sHelp.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                        {
                            logLevel = (uint)ACA.LabelX.Toolbox.ACALabelXSimpleLogging.Level.All;
                        }
                        else
                        {
                            try
                            {
                                logLevel = uint.Parse(sHelp);
                            }
                            catch (Exception)
                            {
                                logLevel = (uint)ACA.LabelX.Toolbox.ACALabelXSimpleLogging.Level.All;
                            }
                        }
                    }
                    
                    globLogfile = new ACA.LabelX.Toolbox.ACALabelXSimpleLogging(logFilename, logAppend, logLevel);

                    sHelp = GetAppSetting("autoflush");
                    if (sHelp != null)
                    {
                        if (sHelp.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            globLogfile.AutoFlush = true;
                        }
                        else
                        {
                            globLogfile.AutoFlush = false;
                        }
                    }
                    globLogfile.Start();
                }
                globLogfile.RunningWithConsole = runningWithConsole;
                return globLogfile;
            }
        }

        public static double ProgramVersion
        {
            get { return programVersion; }
            set { programVersion = value; }
        }
        
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
