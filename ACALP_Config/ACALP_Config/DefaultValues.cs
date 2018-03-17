using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace ACALP_Config
{
    public class DefaultValue
    {
        string  name;
        string value;
        bool forServer;
        bool forClient;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public bool ForServer
        {
            get { return forServer; }
            set { forServer = value; }
        }
        public bool ForClient
        {
            get { return forClient; }
            set { forClient = value; }
        }
        public DefaultValue()
        {
            name = string.Empty;
            value = string.Empty;
            forServer = false;
            forClient = false;
        }
        public DefaultValue(string PropertyName, string PropertyValue, bool IsForServer, bool IsForClient)
        {
            name = PropertyName;
            value = PropertyValue;
            forServer = IsForServer;
            forClient = IsForClient;
        }
    }
    
    public class DefaultValues
    {
        private string sClientServicePath = @"C:\temp\LabelPrint\ClientService";
        private string sServerServicePath = @"C:\temp\LabelPrint\ServerService";
        private string sLabelControlerPath = @"C:\temp\LabelPrint\LabelControler";
        private string sLabelDesignerPath = @"C:\temp\LabelPrint\LabelDesigner";
        private string sRunningExeBase = @"C:\Users\mve.ACA\Documents\Visual Studio 2010\Projects\ACALP_Config\DataFiles";

        System.Collections.Generic.Dictionary<string,DefaultValue> theValues;

        public DefaultValues()
        {
            theValues = new Dictionary<string,DefaultValue>();
            Initialize();
        }
        public void Initialize()
        {
            theValues.Add("ipnumberserver",new DefaultValue("ipnumberserver","@@SET@@",true,true));
            theValues.Add("portserver", new DefaultValue("portserver","18080",true,true));
            theValues.Add("basefolder", new DefaultValue("basefolder",@"C:\ACA\ACALabelPrint",true,true));
            theValues.Add("baselogfolder", new DefaultValue("baselogfolder", @"C:\ACA\Logs", true, true));
            theValues.Add("serverprintjobs", new DefaultValue("printjobs","Server PrintJobs",true,false));
            theValues.Add("serversettings",new DefaultValue("serversettings","Server Settings",true,false));
            theValues.Add("serverlabeldefinition", new DefaultValue("serverlabeldefinition","Server LabelDefinitions",true,false));
            theValues.Add("serverpaperdefinition", new DefaultValue("serverpaperdefinition","Server PaperDefinitions",true,false));
            theValues.Add("serverpictures", new DefaultValue("serverpictures","Server Pictures",true,false));
            theValues.Add("serverupdate", new DefaultValue("serverupdate","Server Updates",true,false));
            theValues.Add("serverlog", new DefaultValue("serverlog","ACALabelPrintServerLogging.txt",true,false));
            theValues.Add("serverloglevel",new DefaultValue("serverloglevel","56",true,false));
            theValues.Add("serverlogappend", new DefaultValue("serverlogappend","true",true,false));
            theValues.Add("serverxmlbase", new DefaultValue("serverxmlbase", "ACALabelXServer.config.xml", true, false));
            theValues.Add("serverserviceconfig", new DefaultValue("serverserviceconfig", "ACALabelXServerService.exe.config", true, false));
            theValues.Add("clientxmlbase", new DefaultValue("clientxmlbase", "ACALabelXClient.config.xml", false, true));
            theValues.Add("clientremoteconfig", new DefaultValue("clientremoteconfig", "ACALabelXClientRemote.config.xml", false, true));
            theValues.Add("clientserviceconfig", new DefaultValue("clientserviceconfig", "ACALabelXClientService.exe.config", false, true));
            theValues.Add("labelcontrolerconfig", new DefaultValue("labelcontrolerconfig", "LabelControler.exe.config", false, true));
            theValues.Add("labeldesignerconfig", new DefaultValue("labeldesignerconfig", "LabelDesigner.exe.config", false, true));
            theValues.Add("identifyingname", new DefaultValue("identifyingname", "@@SET@@", false, true));
            theValues.Add("pjrootname", new DefaultValue("pjrootname", "Client PrintJobs", false, true));
            theValues.Add("ldrootname", new DefaultValue("ldrootname", "Client LabelDefinitions", false, true));
            theValues.Add("pdrootname", new DefaultValue("pdrootname", "Client PaperDefinitions", false, true));
            theValues.Add("strootname", new DefaultValue("strootname", "Client Settings", false, true));
            theValues.Add("ptrootname", new DefaultValue("ptrootname", "Client Pictures", false, true));
            theValues.Add("uprootname", new DefaultValue("uprootname", "Client Updates", false, true));
            theValues.Add("clientlog", new DefaultValue("clientlog", "ACALabelPrintClientLogging.txt", false, true));
            theValues.Add("clientlogappend", new DefaultValue("clientlogappend", "false", false, true));
            theValues.Add("clientloglevel", new DefaultValue("clientloglevel", "56", false, true));
            theValues.Add("clientremip", new DefaultValue("clientremip", "@@SET@@", false, true));
            theValues.Add("clientremport", new DefaultValue("clientremport", "18081", false, true));
            theValues.Add("serverinstdir", new DefaultValue("serverinstdir", "@@INSTDIR@@", true, false));
            theValues.Add("clientinstdir", new DefaultValue("clientinstdir", "@@INSTDIR@@", false, true));
            theValues.Add("labelcontrolerinstdir", new DefaultValue("labelcontrolerinstdir", "@@INSTDIR@@", false, true));
            theValues.Add("labeldesignerinstdir", new DefaultValue("labeldesignerinstdir", "@@INSTDIR@@", false, true));
            theValues.Add("masterpath", new DefaultValue("masterpath", "", false, false));
            theValues.Add("controllerlog", new DefaultValue("controllerlog", "LabelControllerLogging.txt", false, true));
            theValues.Add("designerlog", new DefaultValue("designerlog","LabelDesignerLogging.txt",true,false));
            theValues.Add("controllerlogappend", new DefaultValue("controllerlogappend", "true", false, true));
            theValues.Add("designerlogappend", new DefaultValue("designerlogappend", "true", true, false));
            theValues.Add("standaloneclient", new DefaultValue("standaloneclient", "false", false, true));
        }
        public void ExpandAll()
        {
            ExpandOne("printjobsrootfolder", "basefolder", @"\", "serverprintjobs");
            ExpandOne("labeldefinitionrootfolder", "basefolder", @"\", "serverlabeldefinition");
            ExpandOne("paperdefinitionsrootfolder", "basefolder", @"\", "serverpaperdefinition");
            ExpandOne("settingsrootfolder", "basefolder", @"\", "serversettings");
            ExpandOne("picturesrootfolder", "basefolder", @"\", "serverpictures");
            ExpandOne("updaterootfolder", "basefolder", @"\", "serverupdate");
            ExpandOne("serverlogfile", "baselogfolder", @"\", "serverlog");
            ExpandOne("clientpjrootfolder", "basefolder", @"\", "pjrootname");
            ExpandOne("clientldrootfolder", "basefolder", @"\", "ldrootname");
            ExpandOne("clientpdrootfolder", "basefolder", @"\", "pdrootname");
            ExpandOne("clientstrootfolder", "basefolder", @"\", "strootname");
            ExpandOne("clientptrootfolder", "basefolder", @"\", "ptrootname");
            ExpandOne("clientuprootfolder", "basefolder", @"\", "uprootname");
            ExpandOne("clientlogfile", "baselogfolder", @"\", "clientlog");
            ExpandOne("controllerlogfile", "baselogfolder", @"\", "controllerlog");
            ExpandOne("designerlogfile", "baselogfolder", @"\", "designerlog");

            ExpandOne("serverconfigxml", "serverinstdir", @"\", "serverxmlbase");
            ExpandOne("serverserviceconfigfile", "serverinstdir", @"\", "serverserviceconfig");

            ExpandOne("clientconfigxml", "clientinstdir", @"\", "clientxmlbase");
            ExpandOne("clientremoteconfigfile", "clientinstdir", @"\", "clientremoteconfig");
            ExpandOne("clientserviceconfigfile", "clientinstdir", @"\", "clientserviceconfig");

            ExpandOne("labelcontrolerconfigfile", "labelcontrolerinstdir", @"\", "labelcontrolerconfig");
            ExpandOne("labeldesignerconfigfile", "labeldesignerinstdir", @"\", "labeldesignerconfig");
            if (theValues["standaloneclient"].Value == "true")
            {
                ExpandOne("designerconfigxml", "clientinstdir", @"\", "clientxmlbase");
            }
            else
            {
                ExpandOne("designerconfigxml", "serverinstdir", @"\", "serverxmlbase");
            }
        }
        public void ExpandOne(string target, string basepart, string seperator, string addition)
        {
            DefaultValue newValue;
            newValue = new DefaultValue();
            newValue.Name = target;
            newValue.Value = theValues[basepart].Value + seperator + theValues[addition].Value;
            newValue.ForClient = theValues[addition].ForClient;
            newValue.ForServer = theValues[addition].ForServer;
            if (theValues.ContainsKey(target))
            {
                theValues[target] = newValue;
            }
            else
            {
                theValues.Add(target, newValue);
            }
        }
        public System.Collections.Generic.Dictionary<string,DefaultValue> Values
        {
            get
            {
                return theValues;
            }
        }

        public void SetInstallerPaths()
        {            
            System.IO.FileInfo fi = new System.IO.FileInfo(System.Windows.Forms.Application.ExecutablePath);
            sRunningExeBase = fi.DirectoryName;

            InstallerPaths myPaths;
            myPaths = new InstallerPaths();
            myPaths.LoadInstallerPaths();
            
            sClientServicePath = string.Empty;
            sServerServicePath = string.Empty;
            sLabelControlerPath = string.Empty;
            sLabelDesignerPath = string.Empty;
            //sRunningExeBase = string.Empty;

            if (myPaths.ClientInstalled)
                sClientServicePath = myPaths.ClientServicePath;

            if (myPaths.ServerInstalled)
                sServerServicePath = myPaths.ServerServicePath;
            
            if (myPaths.ControllerInstalled)
            {
                sLabelControlerPath = myPaths.LabelControllerPath;
            }
            if (myPaths.DesingerInstalled)
            {
                sLabelDesignerPath = myPaths.LabelDesignerPath;
            }
            theValues["masterpath"].Value = sRunningExeBase;
            theValues["clientinstdir"].Value = sClientServicePath;
            theValues["serverinstdir"].Value = sServerServicePath;
            theValues["labelcontrolerinstdir"].Value = sLabelControlerPath;
            theValues["labeldesignerinstdir"].Value = sLabelDesignerPath;
        }
    }
}

