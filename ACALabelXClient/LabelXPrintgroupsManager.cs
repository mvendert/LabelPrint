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
using System.Xml;
using ACA.LabelX.Toolbox;
using System.Threading;
using System.Xml.XPath;
using ACA.LabelX.Client;
using System.Security;

namespace ACA.LabelX.Managers
{

    public class LabelXPrintgroupsManager
    {
        private string PrintJobsRootFolder;
        private string LabelDefinitionsRootFolder;
        private string PaperDefinitionsRootFolder;
        private string SettingsRootFolder;
        private static bool moetstoppen = false;
        private static int currentLanguage = 1043;

        public LabelXPrintgroupsManager()
        {
            PrintJobsRootFolder = string.Empty;
        }

        public static void DoThreadWork()
        {
            LabelXPrintgroupsManager theManager;
            theManager = new LabelXPrintgroupsManager();
            theManager.Start();
        }

        public static int SetCurrentLanguage
        {
            set
            {
                currentLanguage = value;
            }
            get
            {
                return currentLanguage;
            }
        }

        public static void Stop()
        {
            moetstoppen = true;
        }

        /*
         * What do we want to do?
         * We want to shedule printjobs which are available on the system to the printgroup.
         * So we collect the printgroups and the printjobs. Then we determine the desired
         * papertypes for each labeltype named in the printjob.
         * If a label has more than one suitable papertype, we only take the default for now.
         * It is the intention to let the user release the job to another papertype in a later stage.
         * 
         * We then query all available printers which are part of the desired printgroup
         * and determine the windows printer queue length. We also determine if a printer
         * is online or offline.
         * We only consider online printers. If more than one printer with the same papertype
         * is available we take the one for which the windows queuelength is the smallest.
         */

        public bool Start()
        {
            int printLanguage;
            PrintGroupItemList PrintGroups;
            PrintGroups = new PrintGroupItemList();
            GlobalDataStore.Logger.Warning("Starting printing engine...");
            string AppPath = GlobalDataStore.AppPath; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClient.config.xml";
            if (!File.Exists(RemotingConfigFilePath))
                throw new LabelXRemClientControlOjectException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));
            
            //not need all but fasted to use this for now...

            string MachineName = string.Empty ;
            int PollFrequency;
            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();

            //When a user changes the printlanguage, we print the total of the
            //current printjob in one language. We only check the variable when
            //handling a new job.
            string slang = toolbox.GetClientConfigurationLanguage(AppPath + @"\ACALabelXClient.config.xml");

            try
            {
                currentLanguage = int.Parse(slang);
            }
            catch (Exception)
            {
                currentLanguage = 1043;
            }

            printLanguage = currentLanguage;

            while (true)
            {
                if (moetstoppen)
                {
                    break;
                }
                //
                //Reread the printgroup XML... mayby not needed everytime...
                //
                if (PrintGroups.Count > 0)
                {
                    PrintGroups.Clear();
                }

                //Remark MVE:
                //  Hier lezen we de XML. De manager uppdate deze bevoorbeeld met een printer 'offline'
                //  maar in het loopje hier beneden, lezen we dit niet telkens opnieuw. D.w.z. dat het
                //  disabelen niet helpt zolang we in de verwerking van printjobs zijn. pas als alle printjobs
                //  klaar zijn gaan we hier uit en is de disable/enable pas definitief
                //  Zie plaats gemarkeerd met ***2 waar we de 'enabled' status van een printer opnieuw zouden moeten inlezen.
                try
                {
                    toolbox.GetGeneralClientConfiguratonEx(AppPath + @"\ACALabelXClient.config.xml",
                                    out PrintJobsRootFolder,
                                    out LabelDefinitionsRootFolder,
                                    out PaperDefinitionsRootFolder,
                                    out SettingsRootFolder,
                                    out MachineName,
                                    out PollFrequency,
                                    ref PrintGroups);
                }
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Error("Could not retrieve client configuration: " + e.Message);
                }

                bool writePrintgroupsToXml = false;
                //JBOS, 20-08-2013, Removing lock: It may cause an issue where this thread (PT) stops. It'll cause the program to stop printing until it is restarted.
                //lock (GlobalDataStore.LockClass)
                //{
                    if ((GlobalDataStore.IsStandAlone) && (GlobalDataStore.MustWriteStandAlonePrintGroups))
                    {
                        //write clients.xml file in SettingsRootFolder
                        writePrintgroupsToXml = true;
                        GlobalDataStore.MustWriteStandAlonePrintGroups = false;
                    }
                //}
                if (writePrintgroupsToXml)
                {
                    try
                    {
                        WritePrintGroupsToLocalClientXML(PrintGroups, SettingsRootFolder + "Clients.xml", MachineName);
                    }
                    catch
                    {
                        GlobalDataStore.Logger.Info("Error writing printgroups to Clients.xml");
                        GlobalDataStore.MustWriteStandAlonePrintGroups = true;
                    }
                }
                SortedList<DateTime, PrintJobInfo> datelist = new SortedList<DateTime, PrintJobInfo>();
                foreach (PrintGroupItem it in PrintGroups)
                {
                    ///mve
                    /// **2 hier zouden we de status van de printer opnieuw moeten lezen
                    /// 
                    // We may not handle disabled printgroupitems
                    if (!it.Enabled)
                        continue;

                    PrintJobInfos printjobs;
                    printjobs = new PrintJobInfos();
                    RemClientControlObject theObj;
                    theObj = new RemClientControlObject();
                    printjobs = theObj.GetPrintjobsForPrintgroup(it);

                    //
                    //Now we have the name of all the printjobs for the group
                    //
                    foreach (PrintJobInfo jobinfo in printjobs)
                    {
                        ///
                        ///hier zouden we de printerstatus moeten opnieuw lezen en vullen.
                        ///
                        //Add dates and jobid to an array
                        if (jobinfo.LastPrinted != null && jobinfo.LastPrinted > new DateTime(1970, 1, 1))
                        {
                            try
                            {
                                datelist.Add(jobinfo.LastPrinted, jobinfo);
                            }
                            catch (ArgumentException)
                            {   //Make sure theres no key exception
                                bool notadded = true;
                                int counter = 1;
                                while(notadded){
                                    if (!(datelist.ContainsKey(jobinfo.LastPrinted.AddMilliseconds(counter))))
                                    {
                                        datelist.Add(jobinfo.LastPrinted.AddMilliseconds(counter), jobinfo);
                                        notadded = false;
                                    }
                                    else
                                        counter++;
                                }
                            }
                        }

                        //Printjobs which are not released may not be printed.
                        if (!jobinfo.AutoRelease)
                            continue;

                        if (moetstoppen)
                            break;

                        printLanguage = currentLanguage;

                        GlobalDataStore.Logger.Info("Attempting to print " + jobinfo.ID);
                        bool succes = HandlePrintJob(it, jobinfo, PrintJobsRootFolder, PaperDefinitionsRootFolder, LabelDefinitionsRootFolder, printLanguage);
                        if (succes)
                        {
                            jobinfo.AutoRelease = false;
                            GlobalDataStore.Logger.Info("Succesfully printed " + jobinfo.ID);
                        }
                   } 
                    if (moetstoppen)
                        break;
                }

                //remove oldest if more than 5 in list
                //remove if older than 3 days

                List<PrintJobInfo> tempprintjobinfolist = new List<PrintJobInfo>();
                try
                {
                    foreach (KeyValuePair<DateTime, PrintJobInfo> pair in datelist)
                    {
                        if (pair.Key < DateTime.Now.AddDays(-3))
                        {
                            if (RemovePrintJob(pair.Value))
                                GlobalDataStore.Logger.Info("Deleted " + pair.Value.FullFilename);
                        }
                        else
                            tempprintjobinfolist.Add(pair.Value);
                    }
                }
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Error("Unable to remove oldest printjobs (older than 3 days): " + e.Message);
                }

                try
                {
                    while (tempprintjobinfolist.Count > 5)
                    {
                        if (RemovePrintJob(tempprintjobinfolist[0]))
                            GlobalDataStore.Logger.Info("Deleted " + tempprintjobinfolist[0].FullFilename);
                        tempprintjobinfolist.RemoveAt(0);
                    }
                }                
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Error("Unable to remove oldest printjobs (more than 5 found): " + e.Message);
                }


                if (moetstoppen)
                    break;
                //Then we sleep a while (say 3 second)
                Thread.Sleep(3000);
            }
            return true;
        }
        
        private bool RemovePrintJob(PrintJobInfo jobinfo)
        {
            int teller = 0;
            bool bRet = false;
            while ((teller < 5) && (bRet == false))
            {
                //It can be another tread is reading this file.
                //Wait sometime until they release the file.
                try
                {
                    File.Delete(jobinfo.FullFilename);
                    bRet = true;
                }
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Error("Unable to remove printjob file: " + e.Message);
                }                
                teller++;
                if (!bRet)
                    System.Threading.Thread.Sleep(2000);
            }            
            return bRet;
        }

        private bool HandlePrintJob(PrintGroupItem it, PrintJobInfo jobinfo, string PrintJobsRootFolder, string PaperDefinitionsRootFolder, string LabelDefinitionsRootFolder, int language)
        {
            bool ret = true;
            string sXMLFile;
            sXMLFile = jobinfo.FullFilename;

            //Retrieve some information for the selected printjob
            // The requested queueu, the printgroup (should be the current), and the LabelType
            string sPrintQueue;
            string sPrintGroup;
            string sLabelType;

            sPrintGroup = string.Empty;
            sPrintQueue = string.Empty;
            sLabelType = string.Empty;
            try
            {
                XPathDocument doc;
                doc = new XPathDocument(sXMLFile);

                XPathNavigator nav;
                nav = doc.CreateNavigator();

                XPathNodeIterator theNode;
                theNode = nav.Select("/printjob/destination/printqueue");

                if (theNode != null)
                {
                    theNode.MoveNext();
                    sPrintQueue = theNode.Current.Value;
                }
                theNode = nav.Select("/printjob/destination/printgroup");

                if (theNode != null)
                {
                    theNode.MoveNext();
                    sPrintGroup = theNode.Current.Value;
                }

                theNode = nav.Select("/printjob/destination/labeltype");

                if (theNode != null)
                {
                    theNode.MoveNext();
                    sLabelType = theNode.Current.Value;
                }
            }
            catch (Exception e)
            {
                GlobalDataStore.Logger.Warning(string.Format("The following error occured while handeling file {0}:", jobinfo.FullFilename)
                                               + Environment.NewLine + e.Message);
                return false;
            }
            //We now have the labeltype requested... we gather information on which paper this can be printed...
            List<string> papertypes;

            papertypes = new List<string>();

            try
            {
                papertypes = GetPaperTypesOfLabelType(sLabelType);
            }
            catch (System.IO.FileNotFoundException)
            {
                //dit zou alleen mogen gebeuren als het labeltype niet bestaat als xmlbestand...
                GlobalDataStore.Logger.Warning(string.Format("Label {0} not defined. Job on hold.",sLabelType));
                
                if (papertypes.Count > 0)
                {
                    papertypes.Clear();
                }
                return false;
            }

            try
            {
                if (papertypes.Count == 0)
                {
                    //can not print on no paper...
                    GlobalDataStore.Logger.Warning(string.Format("Label {0} has no defined paper types. Job on hold.", sLabelType));
                    return false;
                }
                else
                {
                    if (papertypes.Count > 1)
                    {
                        //only one default allowed for now.
                        GlobalDataStore.Logger.Warning(string.Format("Label {0} has more than one default paper type. Job on hold.", sLabelType));
                        return false;
                    }
                    //check if papertype exists as an xml file
                    string paperFileName = PaperDefinitionsRootFolder + papertypes[0] + ".xml";
                    if (!File.Exists(paperFileName))
                    {
                        GlobalDataStore.Logger.Warning(string.Format("Papertype {0} has no xml file. Job on hold.", papertypes[0]));
                        return false;
                    }
                }
                PrinterItemLocals pils;
                pils = new PrinterItemLocals();
                foreach (PrinterItem pit in it.GroupPrinters)
                {
                    PrinterItemLocal pil = new PrinterItemLocal();
                    pil.item = pit;
                    pils.Add(pil);
                    //
                    //Gather windowsinformation for each printer in the windows printer queue
                    // We gather the numberofjobs and if the printer is online.
                    //
                }
                PrinterItemLocal bestFit;
                bestFit = null;
                foreach (PrinterItemLocal pil in pils)
                {
                    //we do not print to printers that are physically offline
                    if (!pil.item.Online)
                        continue;

                    //we do not print to printers that are marked disabled.
                    if (!pil.item.Enabled)
                        continue;

                    if (pil.item.Trays != null)
                    {
                        foreach (PrinterTrayItem tit in pil.item.Trays)
                        {
                            if (tit.CurrentPapertypeName.Equals(papertypes[0], StringComparison.OrdinalIgnoreCase) || pil.item.LongName.Contains("Microsoft XPS Document Writer"))
                            {
                                //papiersoort klopt..
                                if (bestFit == null)
                                {
                                    pil.Tray = tit;
                                    bestFit = pil;
                                }
                                else
                                {
                                    if (pil.item.QueueLength < bestFit.item.QueueLength)
                                    {
                                        pil.Tray = tit;
                                        bestFit = pil;
                                    }
                                }
                            }
                        }
                    }
                }
                //We have selected a printer to print to
                if (bestFit != null)
                {
                    ACA.LabelX.PrintJob.PrintJob p = new ACA.LabelX.PrintJob.PrintJob(PaperDefinitionsRootFolder, LabelDefinitionsRootFolder);
                    p.Parse(jobinfo.FullFilename);

                    //
                    if (p.labels.Count == 0)
                    {
                        //Oeps.. a printjob with a default label, but no actual label is detected
                        //Mark this job as handled now, without error
                        ret = MarkPrintJobReady(jobinfo, "No Label in job.");
                        return ret;
                    }

                    //Check if all pictures are available
                    ACA.LabelX.Label.LabelSet labelset = new ACA.LabelX.Label.LabelSet();
                    labelset.CurrentLabel = p.labels[0];
                    labelset.DefaultLabel = p.Defaultlabel;
                    labelset.BaseLabel = p.LabelDef.DefaultLabel;
                    labelset.StaticVarsLabel = p.StaticVarslabel;

                    if (!p.LabelDef.CheckAllPicturesAvailable(labelset, language, true))
                    {
                        GlobalDataStore.Logger.Warning(string.Format("Missing picture to print. Job on hold.", sLabelType));
                        return false;
                    }
                    //
                    //check if th language requested is pressent in the printjob
                    //
                    bool bFound = false;
                    if (p.languages != null)
                    {
                        if (p.languages.Count > 0)
                        {
                            foreach (PrintJob.PrintJob.PrintLanguage x in p.languages)
                            {
                                if (x.Id == language)
                                {
                                    bFound = true;
                                }
                            }
                        }
                    }

                    //We have selected a language this job can be printed in...
                    if (bFound)
                    {

                        ACA.LabelX.PrintEngine.PrintEngine pi = new ACA.LabelX.PrintEngine.PrintEngine(Environment.MachineName);
                        pi.DesignMode = false;
                        pi.AddPrintJob(p);
                        GlobalDataStore.Logger.Info("Printing " + p.ID.ToString());
                        pi.Print(p.ID.ToString(), bestFit.item.LongName, bestFit.Tray.TrayName, papertypes[0], 0, uint.MaxValue, language);
                        bestFit.item.QueueLength++;     //There is now one job more in the queue..


                        ret = MarkPrintJobReady(jobinfo, bestFit.item.LongName);
                        //Voeg printernaam en datum toe aan printjob xml file
                        //mve, 2016-03-06
                        /*
                        lock (GlobalDataStore.LockClass)
                        {
                            string path = jobinfo.FullFilename;
                            XmlDocument theDoc = new XmlDocument();
                            if (!File.Exists(path))
                            {
                                return false;
                            }
                            theDoc.Load(path);

                            XmlNode printedto = theDoc.SelectSingleNode("/printjob/destination/printedto");
                            if (printedto == null)
                            {
                                XmlNode destinationnode = theDoc.SelectSingleNode("/printjob/destination");
                                XmlElement NewPrintedTo = theDoc.CreateElement("printedto");
                                NewPrintedTo.InnerText = bestFit.item.LongName;
                                destinationnode.AppendChild(NewPrintedTo);
                            }
                            else
                            {
                                printedto.InnerText = bestFit.item.LongName;
                            }

                            XmlNode lastprinted = theDoc.SelectSingleNode("/printjob/destination/lastprinted");
                            if (lastprinted == null)
                            {
                                XmlNode lastprintednode = theDoc.SelectSingleNode("/printjob/destination");
                                XmlElement NewLastPrinted = theDoc.CreateElement("lastprinted");
                                NewLastPrinted.InnerText = System.DateTime.Now.ToString("s");
                                lastprintednode.AppendChild(NewLastPrinted);
                            }
                            else
                            {
                                lastprinted.InnerText = System.DateTime.Now.ToString("s");
                            }

                            XmlNode autorelease = theDoc.SelectSingleNode("/printjob/autorelease");
                            autorelease.InnerText = "false";


                            int counter = 0;
                            bool saveSuccessful = false;
                            while ((counter < 5) && (saveSuccessful == false))
                            {
                                try
                                {
                                    theDoc.Save(path);
                                    Thread.Sleep(2000);
                                    saveSuccessful = true;
                                }
                                catch (Exception ex)
                                {
                                    Thread.Sleep(2000);
                                    counter += 1;
                                    GlobalDataStore.Logger.Warning(string.Format("The file {0} could not be saved: {1}", theDoc.Name, ex.Message));
                                }
                            }
                            if (!saveSuccessful)
                                ret = false;

                        }
                        */

                    }
                    else
                    {
                        GlobalDataStore.Logger.Warning(String.Format("Language of printjob {0} not suitable for this location.", p.ID.ToString()));
                        ret = false;
                    }

                }
                else
                {
                    GlobalDataStore.Logger.Warning(String.Format("No suitable printer found for file {0}.", jobinfo.FullFilename));
                    ret = false;
                }
            }
            catch (Exception egen)
            {
                GlobalDataStore.Logger.Error(string.Format("Unexpected Error in HandlePrintJob: {0}.", egen.Message));
                GlobalDataStore.Logger.Error(string.Format("Extra info: {0}.", egen.StackTrace));                
                if (egen.InnerException != null)
                {
                    GlobalDataStore.Logger.Error(string.Format("Extra info: {0}.", egen.InnerException.Message));
                }
                return false;
            }
            
            return ret;
        }

        private bool MarkPrintJobReady(PrintJobInfo jobinfo, string printedToName)
        {
            bool ret = true;
            lock (GlobalDataStore.LockClass)
            {
                string path = jobinfo.FullFilename;
                XmlDocument theDoc = new XmlDocument();
                if (!File.Exists(path))
                {
                    return false;
                }
                theDoc.Load(path);

                XmlNode printedto = theDoc.SelectSingleNode("/printjob/destination/printedto");
                if (printedto == null)
                {
                    XmlNode destinationnode = theDoc.SelectSingleNode("/printjob/destination");
                    XmlElement NewPrintedTo = theDoc.CreateElement("printedto");
                    NewPrintedTo.InnerText = printedToName;
                    destinationnode.AppendChild(NewPrintedTo);
                }
                else
                {
                    printedto.InnerText = printedToName;
                }

                XmlNode lastprinted = theDoc.SelectSingleNode("/printjob/destination/lastprinted");
                if (lastprinted == null)
                {
                    XmlNode lastprintednode = theDoc.SelectSingleNode("/printjob/destination");
                    XmlElement NewLastPrinted = theDoc.CreateElement("lastprinted");
                    NewLastPrinted.InnerText = System.DateTime.Now.ToString("s");
                    lastprintednode.AppendChild(NewLastPrinted);
                }
                else
                {
                    lastprinted.InnerText = System.DateTime.Now.ToString("s");
                }

                XmlNode autorelease = theDoc.SelectSingleNode("/printjob/autorelease");
                autorelease.InnerText = "false";


                int counter = 0;
                bool saveSuccessful = false;
                while ((counter < 5) && (saveSuccessful == false))
                {
                    try
                    {
                        theDoc.Save(path);
                        Thread.Sleep(2000);
                        saveSuccessful = true;
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(2000);
                        counter += 1;
                        GlobalDataStore.Logger.Warning(string.Format("The file {0} could not be saved: {1}", theDoc.Name, ex.Message));
                    }
                }
                if (!saveSuccessful)
                    ret = false;
            }
            return ret;
        }
        /*
        private void HandlePrintJob(PrintGroupItemList PrintGroups, LabelXItem it, string PrintJobsRootFolder, string PaperDefinitionsRootFolder, string LabelDefinitionsRootFolder)
        {
            string sXmlFile;
            string sPrintQueue;
            string sPrintGroup;
            string sLabelType;

            sPrintGroup = string.Empty;
            sPrintQueue = string.Empty;
            sLabelType = string.Empty;

            sXmlFile = PrintJobsRootFolder + it.Name + ".XML";

            XPathDocument doc;
            doc = new XPathDocument(sXmlFile);

            XPathNavigator nav;
            nav = doc.CreateNavigator();

            XPathNodeIterator theNode;
            theNode = nav.Select("/printjob/destination/printqueue");

            if (theNode != null)
            {
                theNode.MoveNext();
                sPrintQueue = theNode.Current.Value;
            }
            theNode = nav.Select("/printjob/destination/printgroup");

            if (theNode != null)
            {
                theNode.MoveNext();
                sPrintGroup = theNode.Current.Value;
            }

            theNode = nav.Select("/printjob/destination/labeltype");

            if (theNode != null)
            {
                theNode.MoveNext();
                sLabelType = theNode.Current.Value;
            }

            //
            // Retrieve Papertype of labeltype
            //
            List<string> papertypes;

            papertypes = new List<string>();
            papertypes = GetPaperTypesOfLabelType(sLabelType);
            

            doc = null;
           
            GlobalDataStore.Logger.Info("Handling: " + it.Name);

            bool bRendered = false;
            foreach (PrintGroupItem item in PrintGroups)
            {
                if (item.Name.Equals(sPrintGroup, StringComparison.OrdinalIgnoreCase))
                {
                    bRendered = false;
                    foreach (string s in papertypes)
                    {
                        foreach (PrinterItem pit in item.GroupPrinters)
                        {
                            if (pit.Trays != null)
                            {
                                foreach (PrinterTrayItem ptit in pit.Trays)
                                {
                                    if (ptit.CurrentPapertypeName.Equals(s, StringComparison.OrdinalIgnoreCase))
                                    {
                                        GlobalDataStore.Logger.Info("Would start rendering " + it.Name + " on " + pit.Name);
                                        ACA.LabelX.PrintJob.PrintJob p = new ACA.LabelX.PrintJob.PrintJob(PaperDefinitionsRootFolder, LabelDefinitionsRootFolder);
                                        sXmlFile = PrintJobsRootFolder + it.Name + ".XML";
                                        p.Parse(sXmlFile);

                                        ACA.LabelX.PrintEngine.PrintEngine pi = new ACA.LabelX.PrintEngine.PrintEngine("pc-mve2");
                                        pi.DesignMode = false;
                                        pi.AddPrintJob(p);
                                        pi.Print();
                                        
                                        int teller = 0;
                                        bool bRet = false;
                                        while ((teller < 5) && (bRet == false))
                                        {
                                            //
                                            //It can be another tread is reading this file.
                                            //Wait sometime until they release the file.
                                            bRet = Win32ApiFunctions.DeleteFilesToRecycleBin(sXmlFile);
                                            Thread.Sleep(2000);
                                        }
                                        bRendered = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (bRendered)
                        {
                            break;
                        }
                    }
                }
                if (!bRendered)
                {
                    break;
                }
            }
            if (!bRendered)
            {
                GlobalDataStore.Logger.Info("Could not render " + it.Name);
            }   
        }*/

        private void GetLocalPrintJobs(ref List<ACA.LabelX.Toolbox.LabelXItem> items, PrintGroupItemList PrintGroups)
        {
            foreach (PrintGroupItem PrintGroup in PrintGroups)
            {
                ACA.LabelX.Toolbox.Toolbox.GetItemsFromFolder(PrintJobsRootFolder + PrintGroup.Name, ref items, PrintGroup.Name + @"\", LabelX.Toolbox.Toolbox.FileFilterXML);
            }
        }

        private List<string> GetPaperTypes()
        {
            List<string> theList;
            List<LabelXItem> itemList;
            itemList = new List<LabelXItem>();
            theList = new List<string>();
            Toolbox.Toolbox.GetItemsFromFolder(PaperDefinitionsRootFolder,ref itemList,string.Empty);

            foreach (LabelXItem it in itemList)
            {
                theList.Add(it.Name);
            }
            return theList;
        }

        private List<string> GetPaperTypesOfLabelType(string sLabelType )
        {
            List<string> theList;
            string sLabelXML;
            sLabelXML =  LabelDefinitionsRootFolder + sLabelType + ".XML";
            XPathDocument theDoc;
            theDoc = new XPathDocument(sLabelXML);
            XPathNavigator nav = theDoc.CreateNavigator();
            XPathNodeIterator nit;
            theList = new List<string>();
            nit = nav.Select("/labeldef/validpapertypes/paper");
            if (nit != null)
            {
                while (nit.MoveNext())
                {
                    string sHelp = nit.Current.GetAttribute("type",string.Empty);
                    if (sHelp != string.Empty)
                    {
                        //Only add default papertypes for now...
                        string sHelp2 = nit.Current.GetAttribute("default", string.Empty);

                        if (sHelp == string.Empty)
                            sHelp = "false";

                        if (sHelp2.Equals("true", StringComparison.OrdinalIgnoreCase))
                        {
                            theList.Add(sHelp);
                        }
                    }
                }
            }
            return theList;
        }

        private void WritePrintGroupsToLocalClientXML(PrintGroupItemList printgroups, string confPath, string MachineName)
        {
            //Only used in standalone version
            XmlDocument theDoc = new XmlDocument();
            XmlNode rootNode;

            if (File.Exists(confPath))
            {
                try
                {
                    theDoc.Load(confPath);
                    rootNode = theDoc.SelectSingleNode("/clients");
                }
                catch
                {
                    //clients.xml damaged, recreate clients.xml
                    XmlDeclaration xmlDeclaration = theDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    // Create the root element
                    rootNode = theDoc.CreateElement("clients");
                    theDoc.InsertBefore(xmlDeclaration, theDoc.DocumentElement);
                    theDoc.AppendChild(rootNode);                    
                }

            }
            else
            {
                XmlDeclaration xmlDeclaration = theDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                // Create the root element
                rootNode = theDoc.CreateElement("clients");
                theDoc.InsertBefore(xmlDeclaration, theDoc.DocumentElement);
                theDoc.AppendChild(rootNode);
            }

            string xpath = "/clients/machine[@name='" + MachineName + "']";
            XmlNodeList lst = theDoc.SelectNodes(xpath);
            XmlNode node2;
            if (lst == null)
            {
                node2 = null;
            }
            else
            {
                node2 = lst.Count > 0 ? lst[0] : null;
            }
            //XmlNode node2 = theDoc.SelectSingleNode(xpath);
            if (node2 != null)
            {
                foreach (XmlNode chld in node2.ChildNodes)
                {
                    node2.RemoveChild(chld);
                }                
                node2.Attributes["lastseen"].Value = DateTime.Now.ToString("o");
                node2.Attributes["externalip"].Value = "127.0.0.1";
                node2.Attributes["internalip"].Value = "127.0.0.1";
                node2.Attributes["description"].Value = "";              
            }
            else
            {
                XmlElement el = theDoc.CreateElement("machine");
                el.SetAttribute("name", MachineName);                
                el.SetAttribute("firstseen", DateTime.Now.ToString("o"));
                el.SetAttribute("lastseen", DateTime.Now.ToString("o"));
                el.SetAttribute("externalip", "127.0.0.1");
                el.SetAttribute("internalip", "127.0.0.1");
                el.SetAttribute("description", "");                
                node2 = rootNode.AppendChild(el);
            }
            XmlElement pgsnode = theDoc.CreateElement("printgroups");

            foreach (PrintGroupItem o in printgroups)
            {
                XmlElement gnode = theDoc.CreateElement("printgroup");
                gnode.SetAttribute("name", o.Name);
                pgsnode.AppendChild(gnode);
            }
            node2.AppendChild(pgsnode);
            theDoc.Save(confPath);            
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
