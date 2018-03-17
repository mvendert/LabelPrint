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
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Configuration;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.Mime;

namespace ACA.Support.Tools.ACAException
{
    /// <summary>
    /// Unhandled Exception Manager, based upon the 'User Friendly Exception Handling' article
    /// on www.codeproject.com by wumpus1.
    /// </summary>
    public class UnhandledExceptionManager
    {
        private static bool bLogToEventLog;
        private static bool bLogToFile;
        private static bool bLogToEmail;
        private static bool bLogToScreenshot;
        private static bool bLogToUI;
        private static string sMailFrom = "unknown@customer.nl";

        private static bool bLogToFileOK;
        //private static bool bLogToEmailOK;
        private static bool bLogToScreenshotOK;
        //private static bool bLogToUIOK;
        private static bool bLogToEventLogOK;

        private static bool     bEmailIncludeScreenshot;
        private static          System.Drawing.Imaging.ImageFormat screenshotImageFormat = System.Drawing.Imaging.ImageFormat.Png;
        private static string   screenshotFullPath;
        private static string   logFullPath;

        private static bool     bConsoleApp;
        private static System.Reflection.Assembly objParentAssembly = null;
        private static string strException;
        private static string strExceptionType;

        private static bool bIgnoreDebugErrors;
        private static bool bKillAppOnException;

        private const string StrLogName = @"UnhandledExceptionLog.txt";
        private const string StrScreenshotName = @"UnhandledException";
        private const string StrClassName = "UnhandledExceptionManager";
        private const string StrKeyNotPressent = "The key <{0}> is not present in the <appSettings> section of .config file";
        private const string StrKeyError = "Error {0} retrieving key <{1}> from <appSettings> section of .config file";

        #region Properties
        public static bool IgnoreDebugErrors
        {
            get { return bIgnoreDebugErrors; }
            set { bIgnoreDebugErrors = value; }
        }
        public static  bool DisplayDialog
        {
            get { return bLogToUI; }
            set { bLogToUI = value; }
        }
        public static bool EmailScreenshot
        {
            get { return bEmailIncludeScreenshot; }
            set { bEmailIncludeScreenshot = value; }
        }
        public static bool KillAppOnException
        {
            get { return bKillAppOnException; }
            set { bKillAppOnException = value; }
        }
        public static System.Drawing.Imaging.ImageFormat ScreenshotImageFormat
        {
            get { return screenshotImageFormat; }
            set { screenshotImageFormat = value;}
        }
        public static bool LogToFile
        {
            get { return bLogToFile;}
            set { bLogToFile  = value;}
        }
        public static bool LogToEventLog
        {
            get { return bLogToEventLog;}
            set { bLogToEventLog = value;}
        }
        public static bool SendEmail
        {
            get { return bLogToEmail;}
            set { bLogToEmail = value;}
        }
        public static bool TakeScreenshot
        {
            get { return bLogToScreenshot;}
            set { bLogToScreenshot = value;}
        }
        public static string MailFrom
        {
            set { sMailFrom = value; }
            get { return sMailFrom; }
        }
#endregion
        #region Win32ScreenshotCalls
        //To be implemented later...
        #endregion

        private static System.Reflection.Assembly ParentAssembly()
        {
            if (objParentAssembly == null)
            {
                if (System.Reflection.Assembly.GetEntryAssembly() == null)
                {
                    objParentAssembly = System.Reflection.Assembly.GetCallingAssembly();
                }
                else
                {
                    objParentAssembly = System.Reflection.Assembly.GetEntryAssembly();
                }
            }
            return objParentAssembly;
        }
        private static void LoadConfigSettings()
        {
            SendEmail = GetConfigBoolean("SendEmail", true);
            TakeScreenshot = GetConfigBoolean("TakeScreenshot", true);
            EmailScreenshot = GetConfigBoolean("EmailScreenshot", true);
            LogToEventLog = GetConfigBoolean("LogToEventLog", false);
            LogToFile = GetConfigBoolean("LogToFile", true);
            DisplayDialog = GetConfigBoolean("DisplayDialog", true);
            IgnoreDebugErrors = GetConfigBoolean("IgnoreDebugErrors", true);
            KillAppOnException = GetConfigBoolean("KillAppOnException", true);
        }
        public static void AddHandler()
        {
            AddHandler(false);
        }
        public static void AddHandler(bool theConsoleApp)
        {
            //Check config file for optional override settings
            LoadConfigSettings();

            //if we have the IgnoreDebugErrors flag, we do not want to use the unhandler
            //exception handler within the debugger. The debugger is our unhandled exception
            //handler.
            if (bIgnoreDebugErrors)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    return;
                }
            }

            //try to track the parent assembly. We set this handler here to get the right assembly
            //at exception time
            ParentAssembly();

            //Windows forms exception handler
            Application.ThreadException -= new ThreadExceptionEventHandler(ThreadExceptionHandler);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionHandler);

            //For Console Applications
            System.AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(UnhandledExceptionHander);
            System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHander);

            //Somebody knowns how to detect a console app?
            bConsoleApp = theConsoleApp;
        }
        public static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs t)
        {
            GenericExceptionHandler(t.Exception);
        }
        public static void UnhandledExceptionHander(object sender, UnhandledExceptionEventArgs a)
        {
            Exception e;
            e = (Exception)a.ExceptionObject;
            GenericExceptionHandler(e);
        }
        private static DateTime AssemblyFileTime(System.Reflection.Assembly objAssembly)
        {
            try
            {
                return System.IO.File.GetLastWriteTime(objAssembly.Location);
            }
            catch (Exception)
            {
                return DateTime.MaxValue;
            }
        }
        private static DateTime AssemblyBuildDate(System.Reflection.Assembly objAssembly)
        {
            return AssemblyBuildDate(objAssembly, false);
        }
        private static DateTime AssemblyBuildDate(System.Reflection.Assembly objAssembly, bool bForceFileDate)
        {
            System.Version objVersion;
            DateTime dtBuild;

            objVersion = objAssembly.GetName().Version;
            if (bForceFileDate)
            {
                dtBuild = AssemblyFileTime(objAssembly);
            }
            else
            {
                //assuming AssemblyVersion("1.0.*") in assemblyinfo.cs
                dtBuild = DateTime.Parse("1/1/2000").AddDays(objVersion.Build).AddSeconds(objVersion.Revision * 2);
                if (
                      TimeZone.IsDaylightSavingTime(
                            DateTime.Now,
                            TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year)
                      )
                    )
                {
                    dtBuild.AddHours(1);
                }
                if ( (dtBuild > DateTime.Now) || (objVersion.Build < 730) || (objVersion.Revision == 0))
                {
                    dtBuild = AssemblyFileTime(objAssembly);
                }
            }
            return dtBuild;
        }
        private static string StackFrameToString(StackFrame sf) 
        {
            System.Text.StringBuilder sb;
            int intParam;
            MemberInfo mi;

            mi = sf.GetMethod();

            sb = new System.Text.StringBuilder();
                //Extract method name
            sb.Append("   ");
            sb.Append(mi.DeclaringType.Namespace);
            sb.Append(".");
            sb.Append(mi.DeclaringType.Name);
            sb.Append(".");
            sb.Append(mi.Name);

            //Method params...
            ParameterInfo [] objParameters = sf.GetMethod().GetParameters();
            
            sb.Append("(");
            intParam = 0;
            foreach (ParameterInfo objParameter in objParameters)
            {
                intParam += 1;
                if (intParam > 1) 
                {
                    sb.Append(", ");
                }
                sb.Append(objParameter.ParameterType.Name);
                sb.Append(" ");
                sb.Append(objParameter.Name);
            }
            sb.Append(");");
            sb.Append(Environment.NewLine);
            sb.Append("        ");

            if  ( (sf.GetFileName() == null) || (sf.GetFileName().Length == 0))
            {
                sb.Append(System.IO.Path.GetFileName(ParentAssembly().CodeBase));
                sb.Append(": N");
                sb.Append(string.Format("{0:#00000}",sf.GetNativeOffset()));
            }
            else
            {
                sb.Append(System.IO.Path.GetFileName(sf.GetFileName()));
                sb.Append(": line ");
                sb.Append(string.Format("{0:#0000}",sf.GetFileLineNumber()));
                sb.Append(", col ");
                sb.Append(string.Format("{0:#00}",sf.GetFileColumnNumber()));
                if (sf.GetILOffset() != StackFrame.OFFSET_UNKNOWN)
                {
                    sb.Append(", IL ");
                    sb.Append(string.Format("{0:#0000}",sf.GetILOffset()));
                }
            }
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }
        private static /*override*/ string EnhancedStackTrace( StackTrace objStackTrace, string strSkipClassName)
        {
            int intFrame;
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append("----------- Stack Trace ------------");
            sb.Append(Environment.NewLine);

            for (intFrame = 0; intFrame < objStackTrace.FrameCount - 1; intFrame++)
            {
                StackFrame sf;
                MemberInfo mi;
                sf = objStackTrace.GetFrame(intFrame);
                mi = sf.GetMethod();

                if ((strSkipClassName != string.Empty) &&
                       (mi.DeclaringType.Name.IndexOf(strSkipClassName) > -1))
                {
                    //not this classname
                }
                else
                {
                    sb.Append(StackFrameToString(sf));
                }
                sb.Append(Environment.NewLine);

            }
            return sb.ToString();
        }
        private static string EnhancedStackTrace(StackTrace objStackTrace)
        {
            return EnhancedStackTrace(objStackTrace, string.Empty);
        }
        private static string EnhancedStackTrace(Exception ex)
        {
            StackTrace objStackTrace;
            objStackTrace = new StackTrace(ex, true);
            return EnhancedStackTrace(objStackTrace, string.Empty);
        }
        private static string EnhancedStackTrace()
        {
            StackTrace objStackTrace;
            objStackTrace = new StackTrace(true);

            return EnhancedStackTrace(objStackTrace, string.Empty);
        }
        private static void GenericExceptionHandler(Exception objException)
        {
            try
            {
                strException = ExceptionToString(objException);
                strExceptionType = objException.GetType().FullName ;
            }
            catch (Exception ex)
            {
                strException = "Error '" + ex.Message + "' where generating exceptionstring";
                strExceptionType = string.Empty;
            }

            if (! bConsoleApp)
            {
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            }

            try
            {
                if (bLogToScreenshot)   { ExceptionToScreenshot(); }
                if (bLogToEventLog)     { ExceptionToEventLog();}
                if (bLogToFile)         { ExceptionToFile();}
                if (bLogToEmail)        { ExceptionToEmail();}
            }
            catch (Exception)
            {
               //Oeps... failuere of the handler... just fail...
            }

            if (! bConsoleApp)
            {
                Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
            if (bLogToUI)
            {
                ExceptionToUI();
            }

            if (bKillAppOnException)
            {
                KillApp();
                Application.Exit();
            }
        }
        private static void KillApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private static string FormatExceptionForUser(bool bConsoleApp)
        {
            System.Text.StringBuilder sb;
            string strBullet;

            if (bConsoleApp)
            {
                strBullet = "-";
            } else{
                strBullet = "�";
            }

            sb = new StringBuilder();

            if (!bConsoleApp)
            {
                //sb.Append("The ACA development team has developed and tested this application thoroughly.");
                sb.Append("De ontwikkelaars bij ACA hebben deze toepassing door en door getest. ");
                sb.Append("Maar, er is toch iets aan onze aandacht ontsnapt.");
                sb.Append("Voor deze situaties hebben dit programmaonderdeel gemaakt. ");
                sb.Append("Het verzameld informatie over het probleem. Als u verbonden bent met het internet ");
                sb.Append("en als de optie automatisch naar ACA sturen aan staat, is er een e-mail over ");
                sb.Append("het probmeem naar ACA gestuurd. ");
                sb.Append(Environment.NewLine);
                sb.Append("ACA zal deze gegevens gebruiken om het programma te verbeteren in een volgende versie. ");
                sb.Append("Voor het oplossen van dringende problemen dient u nog steeds contact op te nemen met ACA support. ");
                sb.Append(Environment.NewLine);
                sb.Append("De gegevens die ACA nodig heeft kan u hieronder bekijken, en zijn ook op uw harde schijf opgeslagen. ");
                sb.Append("ACA Support kan deze gegevens opvragen indien dit nodig mocht zijn.");
                //sb.Append("But it seems something slipped our attention.");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("Kijk op http://www.aca.nl/nederlands/support/default.asp voor meer informatie.");
                //sb.Append("This report contains vital information about what happened. It also contains information that will help ACA to resolve the issue");
                //sb.Append("Please report this error to ACA or check http://www.aca.nl");
            }
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            //sb.Append("The following information was automatically captured: ");
            sb.Append("Volgende informatie hebben we automatisch verzameld: ");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            if (bLogToScreenshot)
            {
                sb.Append(" ");
                sb.Append(strBullet);
                sb.Append(" ");
                if (bLogToScreenshotOK)
                {
                    sb.Append("Een schermafdruk is opgeslagen als: ");
                    sb.Append(Environment.NewLine);
                    sb.Append("    ");
                    sb.Append(screenshotFullPath);
                }
                else
                {
                    sb.Append("Een schermafdruk is niet opgeslagen.");
                }
                sb.Append(Environment.NewLine);
            }
            if (bLogToEventLog)
            {
                sb.Append(" ");
                sb.Append(strBullet);
                sb.Append(" ");
                if (bLogToEventLogOK)
                {
                    sb.Append("De gegevens zijn opgeslagen in het windows logboek");
                    //an event was writen to the application log");
                }
                else
                {
                    //sb.Append("an event could not be written to the application log");
                    sb.Append("De gegevens konden niet bewaard worden in het windows logboek.");
                }
                sb.Append(Environment.NewLine);
            }
            if (bLogToFile)
            {
                sb.Append(" ");
                sb.Append(strBullet);
                sb.Append(" ");
                if (bLogToFileOK)
                {
                    //sb.Append("details were writen to a text log: ");
                    sb.Append("details zijn weggeschreven in het tekst bestand: ");
                }
                else
                {
                    //sb.Append("details could not be written to the textlog at: ");
                    sb.Append("detail konden NIET geschreven worden in: ");
                }
                sb.Append(Environment.NewLine);
                sb.Append("    ");
                sb.Append(logFullPath);
                sb.Append(Environment.NewLine);
            }
            if (bLogToEmail)
            {
                sb.Append(" ");
                sb.Append(strBullet);
                sb.Append(" ");
                //sb.Append("attempted to send an email to: ");
                sb.Append("We proberen een e-mail te versturen naar: ");
                sb.Append(Environment.NewLine);
                sb.Append("    ");
                sb.Append(GetConfigString("EmailTo","mvendert@aca.nl"));
                sb.Append(Environment.NewLine);
            }
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            //sb.Append("Detailed error information follows:");
            sb.Append("Gedetaileerde informatie over de fout: ");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(strException);
            return sb.ToString();
        }
        private static void ExceptionToUI()
        {
            string sWhatHappened;
            string sHowUserAffected;
            string sWhatUserCanDo;

            sWhatHappened  = "Alhoewel we {app} grondig hebben getest, is er een onverwacht probleem opgetreden." ;
                //"Although the ACA developmentteam tested {app}, something unexpected happened.";

            sWhatUserCanDo = "Herstart {app}, en herhaal uw laatste aktie opnieuw, of probleer alternatieven om uw doel te bereiken.";
                //"Restart {app}, and try repeating your last action. alternative methods of performing the same action.";
            
            if (KillAppOnException)
            {
                //sHowUserAffected = "When you click OK, {app} will close.";
                sHowUserAffected = "Wanneer u op OK klikt zal {app} stoppen.";
            }
            else
            {
                //sHowUserAffected = "The action you requested was not performed.";
                sHowUserAffected = "De aktie is niet uitgevoerd.";
            }


            if (!bConsoleApp)
            {
                //HandledExceptionManager.EmailErro = ! SendEmail;
                ExceptionDialog myDialog;
                myDialog                 = new ExceptionDialog();
                myDialog.WhatHappened    = sWhatHappened;
                myDialog.HowUserAffected = sHowUserAffected;
                myDialog.WhatUserCanDo   = sWhatUserCanDo;
                myDialog.MoreDetails     = FormatExceptionForUser(false);
                myDialog.DialogIcon      = MessageBoxIcon.Stop;
                myDialog.Buttons         = MessageBoxButtons.OK;
                myDialog.ShowDialog();
            }
            else
            {
                ExceptionToConsole();
            }
        }
        private static string GetApplicationPath()
        {
            if (ParentAssembly().CodeBase.StartsWith("http://"))
            {
                return @"c:\" +  Regex.Replace(ParentAssembly().CodeBase, @"[\/\\\:\*\?\""\<\>\|]", "_") + ".";
            } else
            {
                return System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName + ".";
            }
        }
        private static void ExceptionToScreenshot()
        {
            try
            {
                screenshotFullPath = GetApplicationPath() + StrScreenshotName;
                TakeScreenshotPrivate(screenshotFullPath);
                bLogToScreenshotOK = true;
            }
            catch (Exception)
            {
                bLogToScreenshotOK = false;
            }
        }
        private static void TakeScreenshotPrivate(string strFilename)
        {
            Rectangle objRectangle;
            Bitmap objBitmap;
            Graphics objGraphics;
            IntPtr hdcDest;
            IntPtr hdcSrc;
            const int SRCCOPY = 0xcc0020;
            string strFormatExtension;

            
            objRectangle = Screen.PrimaryScreen.Bounds;
            objBitmap = new Bitmap(objRectangle.Right, objRectangle.Bottom);
            objGraphics = Graphics.FromImage(objBitmap);
            hdcSrc = PlatformInvokeGDI32.GetDC(0);
            hdcDest = objGraphics.GetHdc();
            PlatformInvokeGDI32.BitBlt(hdcDest, 0, 0, objRectangle.Right, objRectangle.Bottom, hdcSrc, 0, 0, SRCCOPY);
            objGraphics.ReleaseHdc(hdcDest);
            PlatformInvokeGDI32.ReleaseDC(0, hdcSrc);
            strFormatExtension = screenshotImageFormat.ToString().ToLower();
            if (System.IO.Path.GetExtension(strFilename) != "." + strFormatExtension)
            {
                strFilename += "." + strFormatExtension;
            }
            switch (strFormatExtension)
            {
                case "jpeg":
                    BitmapToJPEG(objBitmap, strFilename, 80);
                    break;
                default:
                    objBitmap.Save(strFilename, screenshotImageFormat);
                    break;
            }
            screenshotFullPath = strFilename;
        }
        private static void BitmapToJPEG(Bitmap objBitmap, string strFilename)
        {
            BitmapToJPEG(objBitmap, strFilename, 75);
        }
        private static void BitmapToJPEG(Bitmap objBitmap, string strFilename, long lngCompression)
        {
            System.Drawing.Imaging.EncoderParameters objEncoderParameters;
            System.Drawing.Imaging.ImageCodecInfo objImageCodecInfo;

            objEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
            objImageCodecInfo = GetEncoderInfo("image/jpeg");
            objEncoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, lngCompression);
            objBitmap.Save(strFilename, objImageCodecInfo, objEncoderParameters);
        }
        private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string strMimeType)
        {
            int j;
            System.Drawing.Imaging.ImageCodecInfo [] objImageCodecInfo;
            objImageCodecInfo = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            j = 0;
            while (j < objImageCodecInfo.Length)
            {
                if (objImageCodecInfo[j].MimeType.Equals(strMimeType))
                {
                    return objImageCodecInfo[j];
                }
            }
            return null;
        }
        private static void ExceptionToEventLog()
        {
            try
            {
                System.Diagnostics.EventLog.WriteEntry(System.AppDomain.CurrentDomain.FriendlyName,
                    Environment.NewLine+ strException,
                    EventLogEntryType.Error);
                bLogToEventLogOK = true;
            }
            catch (Exception)
            {
                bLogToEventLogOK = false;
            }
        }
        private static void ExceptionToConsole()
        {
            //Console.WriteLine("This application has encountered an unexpected problem.");
            Console.WriteLine("Deze toepassing is een onverwacht probleem tegengekomen.");
            Console.WriteLine(FormatExceptionForUser(true));
            Console.WriteLine("De toepassing zal nu beeindigen. Druk op enter.");
            //Console.WriteLine("The application must now terminate. Press enter to continue...");
            Console.ReadLine();
        }
        private static void ExceptionToFile()
        {
            logFullPath = GetApplicationPath() + StrLogName;
            try
            {
                System.IO.StreamWriter wr = new System.IO.StreamWriter(logFullPath,true);
                wr.Write(strException);
                wr.WriteLine();
                wr.Close();
                bLogToFileOK = true;
            }
            catch (Exception)
            {
                bLogToFile = false;
            }
        }
        private static void ThreadHandler()
        {
            string server;
            string sVersionInfo;


            sVersionInfo =  Application.ProductName + 
                            " " + 
                            Application.ProductVersion.ToString() + 
                            " encoutered a problem "
                            + strExceptionType;
            
            server = GetConfigString("EMailServer","mail.aca.nl");

            System.Net.Mail.MailMessage objMail;
            System.Net.Mail.Attachment objAttachment = null;
            objMail = new System.Net.Mail.MailMessage(sMailFrom,
                                                      GetConfigString("EmailTo", "mvendert@aca.nl"),
                                                      sVersionInfo,
                                                      strException);

            if (bLogToScreenshot)
            {

                objAttachment = new System.Net.Mail.Attachment(screenshotFullPath);

                ContentDisposition disposition = objAttachment.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(screenshotFullPath);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(screenshotFullPath);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(screenshotFullPath);

                objMail.Attachments.Add(objAttachment);
            }

            SmtpClient client = new SmtpClient(server);
            try
            {
                client.Send(objMail);
                //bLogToEmailOK = true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Mail send failed" + e.Message );
                //bLogToEmailOK = false;
            }

            if (bLogToScreenshot)
            {
                objAttachment.Dispose();
            }

        }
        private static void ExceptionToEmail()
        {
            Thread objThread;
            objThread = new Thread(new ThreadStart(ThreadHandler));
            objThread.Name="SendExceptionEmail";
            objThread.Start();
        }
        /// <summary>
        /// try to retrieve current user of computer...
        /// </summary>
        /// <returns></returns>
        private static string CurrentWindowItendity()
        {
            try
            {
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private static string CurrentEnvironmentIdentity()
        {
            try
            {
                return System.Environment.UserDomainName + @"\" + System.Environment.UserName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private static string UserIdentity()
        {
            string strTemp;
            strTemp = CurrentWindowItendity();
            if (strTemp == string.Empty)
            {
                strTemp = CurrentEnvironmentIdentity();
            }
            return strTemp;
        }
        private static string SysInfoToString()
        {
            return SysInfoToString(false);
        }
        private static string SysInfoToString(bool bIncludeStackTrace)
        {
            StringBuilder sb;
            sb = new StringBuilder();
            sb.Append("Date and Time:         "); sb.Append (DateTime.Now); sb.Append(Environment.NewLine);
            sb.Append("Machine Name :         ");
            try
            {
                sb.Append(Environment.MachineName);
            }
            catch (Exception ex)
            {
                sb.Append(ex.Message);
            }
            sb.Append(Environment.NewLine);

            sb.Append("IP Address:            "); sb.Append(GetCurrentIP()); sb.Append(Environment.NewLine);
            sb.Append("Current User:          "); sb.Append(UserIdentity()); sb.Append(Environment.NewLine);
            sb.Append("ApplicationDomain:     ");
            try
            {
                sb.Append(System.AppDomain.CurrentDomain.FriendlyName);
            }
            catch(Exception  ex2)
            {
                   sb.Append(ex2.Message);
            }
            sb.Append(Environment.NewLine);
            sb.Append("Assembly codebase:     ");
            try
            {
                sb.Append(ParentAssembly().CodeBase);
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);
            
            sb.Append("Assembly Full Name:    ");
            try
            {
                sb.Append(ParentAssembly().FullName);
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);
            
            sb.Append("Assembly Version:      ");
            try
            {
                sb.Append(ParentAssembly().GetName().Version.ToString());
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);

            sb.Append("Assembly Build Date:   ");
            try
            {
                sb.Append(AssemblyBuildDate(ParentAssembly()).ToString());
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            if (bIncludeStackTrace)
            {
                sb.Append(EnhancedStackTrace());
            }
            return sb.ToString();
        }
        private static string ExceptionToString(Exception objException)
        {
            StringBuilder sb;
            sb = new StringBuilder();

            if (objException.InnerException != null)
            {
                sb.Append("(Inner Exception)");
                sb.Append(Environment.NewLine);
                sb.Append(ExceptionToString(objException.InnerException));
                sb.Append(Environment.NewLine);
                sb.Append("(Outer Exception)");
                sb.Append(Environment.NewLine);
            }
                
            sb.Append(SysInfoToString());

            sb.Append("Exception Source:      " );
            try
            {
                sb.Append(objException.Source);
            }
            catch(Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);
            
            sb.Append("Exception Type:        ");
            try
            {
                sb.Append(objException.GetType().FullName);
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);

            sb.Append("Exception Message:     ");
            try
            {
                sb.Append(objException.Message);
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);

            sb.Append("Exception Target Site: ");
            try
            {
                sb.Append(objException.TargetSite.Name);
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);

            try
            {
                string s;
                s = EnhancedStackTrace(objException);
                sb.Append(s);
            }
            catch ( Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }
        private static string GetCurrentIP()
        {
            string strIp;
            strIp = string.Empty;
            try
            {
                string sHost = System.Net.Dns.GetHostName();
                System.Net.IPAddress[] addr = System.Net.Dns.GetHostEntry(sHost).AddressList;
                for (int i = 0; i < addr.Length; i++)
                {
                    strIp += '/' + addr[i].ToString();
                }
                //strIp = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0].ToString();                
            }
            catch (Exception )
            {
                strIp = "127.0.0.1";
            }
            return strIp;
        }
        public static string GetAppSetting(string sPropName)
        {
            AppSettingsReader reader;
            string sRet;
            try
            {
                reader = new AppSettingsReader();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in your config file. " + e.Message);
                sRet = null;
                return sRet;
            }
            sRet = null;
            try
            {
                //sRet = global::GlobalExceptionHandler.Properties.Settings.[sPropName].ToString();
                sRet = reader.GetValue(sPropName, typeof(string)).ToString();
            }
            catch (System.Exception)
            {
                sRet = null ;
            }
            return sRet;
        }
        private static string GetConfigString(string strKey, string strDefault)
        {
            string strTemp = null;
            strTemp = GetAppSetting(strKey);
            if (strTemp == null)
            {
                strTemp = strDefault;
            }
            return strTemp;
        }
        private static bool GetConfigBoolean(string strKey, bool bDefault)
        {
            bool bRet;
            bRet = bDefault;
            string strTemp;
            strTemp = GetAppSetting(strKey);
            if (strTemp != null)
            {
                try
                {
                    bRet = bool.Parse(strTemp);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    bRet = false;
                }
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
