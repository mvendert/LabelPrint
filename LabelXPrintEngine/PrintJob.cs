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
using System.Xml;
using System.Collections;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using ACA.LabelX.Tools;
using LuaInterface;

namespace ACA.LabelX.PrintJob
{
    public class PrintJob
    {
        static int counter;
        public class Destination
        {
            public String LabelType = null;
            public String MachineName = null;
            public String PrintGroup = null;
            public DateTime LastPrinted;
            public String PrintedTo = null;

            public void Parse(XmlNode node)
            {
                LabelType = null;
                MachineName = null;
                PrintGroup = null;
                //LastPrinted = null;
                PrintedTo = null;

                foreach (XmlNode nodex in node)
                {
                    if (nodex.Name.Equals("printqueue", StringComparison.OrdinalIgnoreCase))
                    {
                        MachineName = nodex.InnerText;
                    }
                    else if (nodex.Name.Equals("labeltype", StringComparison.OrdinalIgnoreCase))
                    {
                        LabelType = nodex.InnerText;
                    }
                    else if (nodex.Name.Equals("printgroup", StringComparison.OrdinalIgnoreCase))
                    {
                        PrintGroup = nodex.InnerText;
                    }
                    else if (nodex.Name.Equals("lastprinted", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            LastPrinted = DateTime.Parse(nodex.InnerText);
                        }//TODO 17/03/2009 11:26:53 FormatException
                        catch {}
                    }
                    else if (nodex.Name.Equals("printedto", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!nodex.InnerText.Equals(""))
                        PrintedTo = nodex.InnerText;
                    }
                }
            }
        }

        public class PrintLanguage
        {
            public int Id = -1;
            public void Parse(XmlNode node)
            {
                foreach (XmlAttribute attrib in node.Attributes)
                {
                    if (attrib.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    {
                        Id = int.Parse(attrib.Value);
                    }
                }
            }
        }

        public class Languages : List<PrintLanguage>
        {
            public void Parse(XmlNode node)
            {
                foreach (XmlNode nodex in node)
                {
                    if (nodex.Name.Equals("language", StringComparison.OrdinalIgnoreCase))
                    {
                        PrintLanguage theLang;
                        theLang = new PrintLanguage();
                        theLang.Parse(nodex);
                        this.Add(theLang);
                    }
                }
            }
        }


        public String ID = "";
        public String Group = "";
        public String From = "";
        public String User = "";
        
        public Destination destination = null;
        public Languages languages = null;

        public String CreateTime = "";
        public String PreviewFileName = "";
        public bool AutoRelease = false;
        private PrinterBounds theBounds;
        private Boolean theBoundsCached;

        private Label.LabelDef labelDef = null;

        public Label.LabelDef LabelDef
        {
            get { return labelDef; }
            set 
            { 
                labelDef = value; 
            }
        }
        public Paper.PaperDef PaperDef = null;

        private Label.LabelList Labels = new Label.LabelList();

        public Label.LabelList labels
        {
            get { return Labels; }
            set { Labels = value; }
        }

        private Label.Label DefaultLabel = null;
        private Label.Label StaticVarsLabel = new ACA.LabelX.Label.Label();        

        public Label.Label Defaultlabel
        {
            get { return DefaultLabel; }
            set { DefaultLabel = value; }
        }
        public Label.Label StaticVarslabel
        {
            get { return StaticVarsLabel; }
            set { StaticVarsLabel = value; }
        }
        

        string PaperDefinitionsPath = "";
        string LabelDefinitionsPath = "";

        uint FromPage = 0;
        uint ToPage = 0;
        uint NextPage = 0;
        uint LabelsPerPage = 0;
        public bool DrawBorders = false;
        int Language = 0;

        public PrintJob(string PaperDefinitionsPath, string LabelDefinitionsPath)
        {
            this.PaperDefinitionsPath = PaperDefinitionsPath;
            this.LabelDefinitionsPath = LabelDefinitionsPath;
        }

        private void PreparePrint(string Printer, string PaperType)
        {
            PaperDef = new Paper.PaperDef();
            string path = PaperDefinitionsPath + @"\" + PaperType + ".xml";
            if (!File.Exists(path))
                throw new ApplicationException(string.Format("Cannot find file: {0}", path));
                
            PaperDef.Parse(path);
            PaperDef.SetDestination(destination.MachineName, Printer);
            LabelsPerPage = PaperDef.labelLayout.HorizontalCount * PaperDef.labelLayout.VerticalCount;
        }
        

        public void PrintPreview(string Printer, string PaperType, string Tray, uint FromPage, uint ToPage, int Language)
        {
            PrinterSettings p;
            bool bGood = false;
            p = new PrinterSettings();
            if (PrinterSettings.InstalledPrinters.Count > 0)
            {
                foreach (string s in PrinterSettings.InstalledPrinters)
                {
                    if (s.Equals(Printer, StringComparison.OrdinalIgnoreCase))
                    {
                        p.PrinterName = s;
                        bGood = true;
                    }
                }
                if (!bGood)
                {
                    p.PrinterName = PrinterSettings.InstalledPrinters[0];
                    bGood = true;
                }
            }
            if (bGood == false)
            {
                MessageBox.Show("No installed printers were found.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            PreparePrint(Printer, PaperType);

            this.FromPage = FromPage;
            this.ToPage = ToPage;
            this.NextPage = FromPage;
            this.Language = Language;
            
            PrintDocument printDocument = new PrintDocument();            
            printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            printDocument.PrinterSettings = p;

            PaperSize gevondenSize = null;
            foreach (PaperSize ps in printDocument.PrinterSettings.PaperSizes)
            {
                if (ps.PaperName.Equals(PaperType, StringComparison.OrdinalIgnoreCase))
                {
                    gevondenSize = ps;
                    break;
                }
            }
            if (gevondenSize == null)
            {
                System.Drawing.Printing.PrinterSettings.PaperSizeCollection col;
                col = printDocument.PrinterSettings.PaperSizes;

                Tools.Size theSize = PaperDef.GetLabelSize();

                PaperSize sz = new PaperSize(PaperType, (int)(PaperDef.GetPhysicalLabelSize().Width.InInch() * 100), (int)(PaperDef.GetPhysicalLabelSize().Height.InInch() * 100));
                //PaperSize sz = new PaperSize(PaperType, (int)(PaperDef.GetPhysicalLabelSize().Width.InInch() * 96), (int)(PaperDef.GetPhysicalLabelSize().Height.InInch() * 96)); 
                //TODO: DPI values

                printDocument.PrinterSettings.DefaultPageSettings.PaperSize = sz;
            }
            else
            {
                printDocument.PrinterSettings.DefaultPageSettings.PaperSize = gevondenSize;
            }
            if ((FromPage > 0) || (ToPage != uint.MaxValue))
            {
                printDocument.PrinterSettings.PrintRange = PrintRange.SomePages;
                printDocument.PrinterSettings.FromPage = (int)FromPage;
                printDocument.PrinterSettings.ToPage = (int)ToPage;
            }
            else
            {
                printDocument.PrinterSettings.PrintRange = PrintRange.AllPages;
            }

            printDocument.PrinterSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            printDocument.DocumentName = "Preview document";
            printDocument.DefaultPageSettings.PaperSource.SourceName = Tray;

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            
            printPreviewDialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            printPreviewDialog.UseAntiAlias = true; //remove later if proves to be to slow.
            ((ToolStrip)printPreviewDialog.Controls[1]).Items[0].Enabled = false;
                        
            printPreviewDialog.ShowDialog();
            StaticVarslabel.Values.Clear(); //Clear static variables after printjob
            printPreviewDialog.Dispose();
        }

        public void Print(string DocumentName,string Printer, string Tray, string PaperType, uint FromPage, uint ToPage, int Language)
        {
            PrinterSettings p = new PrinterSettings();
            p.PrinterName = Printer;
            try
            {
                PreparePrint(Printer, PaperType);
            }
            catch (ApplicationException e){
                GlobalDataStore.Logger.Error("Error: " + e.Message);
            }

            this.FromPage = FromPage;
            this.ToPage = ToPage;
            this.NextPage = FromPage;
            this.Language = Language;

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            //printDocument.PrinterSettings.PrinterName = Printer; 
            printDocument.PrinterSettings = p;

            PaperSize gevondenSize = null;
            foreach (PaperSize ps in printDocument.PrinterSettings.PaperSizes)
            {
                if (ps.PaperName.Equals(PaperType, StringComparison.OrdinalIgnoreCase))
                {
                    gevondenSize = ps;
                    break;
                }
            }
            if (gevondenSize == null)
            {
                System.Drawing.Printing.PrinterSettings.PaperSizeCollection col;
                col = printDocument.PrinterSettings.PaperSizes;

                Tools.Size theSize = PaperDef.GetLabelSize();

                PaperSize sz = new PaperSize(PaperType, (int)(PaperDef.GetPhysicalLabelSize().Width.InInch() * 100), (int)(PaperDef.GetPhysicalLabelSize().Height.InInch() * 100));

                printDocument.PrinterSettings.DefaultPageSettings.PaperSize = sz;
            }
            else
            {
                printDocument.PrinterSettings.DefaultPageSettings.PaperSize = gevondenSize;
            }


            if ((FromPage > 0) || (ToPage != uint.MaxValue))
            {
                printDocument.PrinterSettings.PrintRange = PrintRange.SomePages;
                printDocument.PrinterSettings.FromPage = (int)FromPage;
                printDocument.PrinterSettings.ToPage = (int)ToPage;
            }
            else
            {
                printDocument.PrinterSettings.PrintRange = PrintRange.AllPages;
            }

            printDocument.DocumentName = DocumentName;

            try
            {

                printDocument.DefaultPageSettings.PaperSource.SourceName = Tray;
            }
            catch (InvalidPrinterException)
            {
                GlobalDataStore.Logger.Error("Printer " + printDocument.DefaultPageSettings.PrinterSettings.PrinterName + " does not exist.");
            }
            catch (Exception ex)
            {
                GlobalDataStore.Logger.Error(ex.Message);
            }
            printDocument.PrintController = new StandardPrintController();
            
            if (printDocument.PrinterSettings.PrinterName.Contains("Microsoft XPS Document Writer"))
            {
                try
                {
                    printDocument.PrinterSettings.PrintToFile = true;
                    printDocument.PrinterSettings.PrintFileName = PreviewFileName + "tmp";
                    theBoundsCached = false;
                    printDocument.Print();
                    printDocument.Dispose();
                    FileInfo fi = new FileInfo(PreviewFileName + "tmp");
                    try
                    {
                        if (File.Exists(PreviewFileName))
                        {                        
                            File.Delete(PreviewFileName);
                        }
                    }
                    catch (Exception)
                    {
                        GlobalDataStore.Logger.Error(string.Format("Cannot remove file {0}",PreviewFileName));
                    }
                  
                    try
                    {
                        fi.MoveTo(PreviewFileName);
                    }
                    catch
                    {
                        int i = 0;
                        bool ready = false;
                        while ((!ready) && (i < 10))
                        {
                            i++;
                            //Windows 8.1 locks files longer. Try another move after a wait
                            System.Threading.Thread.Sleep(100);
                            try
                            {
                                fi.MoveTo(PreviewFileName);
                                ready = true;
                            }
                            catch (Exception)
                            {
                                //GlobalDataStore.Logger.Error("Cannot rename file from " + PreviewFileName + "tmp " + "to " + PreviewFileName + ". ");
                            }
                        }
                        if (!ready)
                        {
                            GlobalDataStore.Logger.Error("Cannot rename file from " + PreviewFileName + "tmp " + "to " + PreviewFileName + ".");
                        }
                    }
                }
                catch (ArgumentNullException)
                {
                    GlobalDataStore.Logger.Error("Cannot print to an XPS writer without a filename!");
                }
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Error("An unexpected error occured: " + e.Message);
                }
            }
            else
            {
                try
                {
                    printDocument.Print();
                }
                catch (InvalidPrinterException pe)
                {
                    GlobalDataStore.Logger.Error("Printing error. Selected printerdriver removed from your system?");
                    GlobalDataStore.Logger.Error(pe.Message);
                }
            }
            StaticVarslabel.Values.Clear();//Clear all static variables after printjob
            printDocument.Dispose();
        
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {            
            Length LeftMargin;
            Length TopMargin;

            //The labelnumber of the first label to be printed on this page!
            //This is NOT necessary the index of the labelnumer in the list as
            //each label can have a quantity to be printed of more than one.

            //Euhhh NextPage contains the number of the page to be drawn next.
            //But because we a now drawing it... it's the page to be drawn now.
            //Upon leave we increase this number. We work zero based, to the first page
            //actually has numbe 0
            uint FirstLabel = NextPage * LabelsPerPage;
            
            //LastLabel = the labelnumber of the max last label on THIS page, 
            //(thus not the end of the document)
            uint LastLabel = ( (NextPage+1) * LabelsPerPage) - 1;

            //A limit can be given bij pages (print page 5 to 7) or
            //by reaching the last label.
            //So the number labels max to go is:
            uint LastLabelOfJob;
            if (ToPage == uint.MaxValue)
            {
                LastLabelOfJob = uint.MaxValue;
            } else
            {
                LastLabelOfJob = (ToPage+1) * LabelsPerPage - 1;
            }
            //uint LastLabelOfJob = ((ToPage+1) * LabelsPerPage) - 1;
            uint AllLabelsCount;

            AllLabelsCount = Labels.CountAll-1; //make this max also zero based

            //If we requested pages 0 to n but we need less than n pages to
            //print all labels, we limit to the last label.
            if (LastLabelOfJob > AllLabelsCount)
                LastLabelOfJob = AllLabelsCount;

            //If we have less labels than would fit the page, will still need to
            //stop at the last label...
            if (LastLabel > AllLabelsCount)
                LastLabel = AllLabelsCount;

            //Adjust for physical margings of the printer in respect to
            //paperdefinitions.
            //test
            //PrinterBounds theBounds;
            //theBounds = new PrinterBounds(ev);
            if (!theBoundsCached) {
                theBounds = new PrinterBounds(ev);
                theBoundsCached = true;
            }
            //*
            LeftMargin = theBounds.HardMarginLeft;
            TopMargin = theBounds.HardMarginTop;
                        
            PaperDef.PhysicalLeftMargin = LeftMargin;
            PaperDef.PhysicalTopMargin = TopMargin;

            ev.Graphics.PageUnit = GraphicsUnit.Pixel;
            float dpiX = ev.Graphics.DpiX;
            float dpiY = ev.Graphics.DpiY;
            
            uint LabelIndex = 0;
            uint HorizontalIndex = 0;
            uint VerticalIndex = 0;
            uint QuantityCount = 0;

            foreach (Label.Label CurrentLabel in Labels)
            {
                if (LabelIndex + CurrentLabel.Quantity < FirstLabel)
                {
                    LabelIndex += CurrentLabel.Quantity;
                    continue;
                }

                // Stop if we don't need any more labels
                if (LabelIndex > LastLabel)
                    break;

                for (QuantityCount = 0; QuantityCount < CurrentLabel.Quantity; QuantityCount++)
                {
                    // skip labels that don't need printing..
                    if (LabelIndex < FirstLabel)
                    {
                        LabelIndex++;
                        continue;
                    }

                    // Stop if we don't need any more labels
                    if (LabelIndex > LastLabel)
                        break;

                    Tools.Rectangle rect = PaperDef.GetLabelRectangle((int)HorizontalIndex, (int)VerticalIndex);
                    System.Drawing.Rectangle convRect = new System.Drawing.Rectangle((int)rect.Left.InPixels(dpiX), (int)rect.Top.InPixels(dpiY), (int)rect.Size.Width.InPixels(dpiX), (int)rect.Size.Height.InPixels(dpiY));
                    
                    ACA.LabelX.Label.LabelSet labelset = new ACA.LabelX.Label.LabelSet();
                    
                    labelset.CurrentLabel = CurrentLabel;
                    labelset.DefaultLabel = DefaultLabel;
                    labelset.BaseLabel    =  LabelDef.DefaultLabel;
                    labelset.StaticVarsLabel = StaticVarslabel;


                    executeLua(labelset.CurrentLabel, labelset.DefaultLabel, labelset.BaseLabel,labelset.StaticVarsLabel);

                    CurrentLabel.Draw(ev.Graphics, convRect, LabelDef, labelset, Language, DrawBorders);
                    counter++;
                    if (counter > 200)
                    {
                        GC.Collect();
                        counter = 0;
                    }
                    HorizontalIndex++;

                    if (HorizontalIndex >= PaperDef.labelLayout.HorizontalCount)
                    {
                        HorizontalIndex = 0;
                        VerticalIndex++;
                    }

                    if (VerticalIndex >= PaperDef.labelLayout.VerticalCount)
                        VerticalIndex = 0;

                    LabelIndex++;
                }
            }

            if (LabelIndex <= LastLabelOfJob)
            {
                NextPage++;
                ev.HasMorePages = true;
            }
            else
            {
                NextPage = 0;
                ev.HasMorePages = false;
            }
        }

        public void Parse(string FilePath)
        {
            if (!File.Exists(FilePath))
                throw new ApplicationException(string.Format("Cannot find file: {0}", FilePath));

            Labels.Clear();
            DefaultLabel = null;
            destination = null;

            ID = "";
            From = "";
            Group = "";
            User = "";           
            
            CreateTime = "";
            PreviewFileName = "";
            AutoRelease = false;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FilePath);
            XmlNodeList node= xDoc.GetElementsByTagName("printjob");
            foreach (XmlNode nodex in node)
            {
                foreach (XmlAttribute attrib in nodex.Attributes)
                {
                    if (attrib.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    {
                        ID = attrib.Value;
                    }
                    else if (attrib.Name.Equals("group", StringComparison.OrdinalIgnoreCase))
                    {
                        Group = attrib.Value;
                    }
                    else if (attrib.Name.Equals("from", StringComparison.OrdinalIgnoreCase))
                    {
                        From = attrib.Value;
                    }
                    else if (attrib.Name.Equals("user", StringComparison.OrdinalIgnoreCase))
                    {
                        User = attrib.Value;
                    }
                }

                foreach (XmlNode nodexx in nodex.ChildNodes)
                {
                    if (nodexx.Name.Equals("destination", StringComparison.OrdinalIgnoreCase))
                    {
                        destination = new Destination();
                        destination.Parse(nodexx);

                        if (destination.LabelType != null)
                        {
                            LabelDef = new Label.LabelDef();
                            string path = LabelDefinitionsPath + @"\" + destination.LabelType + ".xml";
                            if (File.Exists(path))
                            {
                                LabelDef.Parse(path); 
                            }
                        }
                    }  
                    else if (nodexx.Name.Equals("languages",StringComparison.OrdinalIgnoreCase))
                    {
                        languages = new Languages();
                        languages.Parse(nodexx);
                    } else if (nodexx.Name.Equals("createtime", StringComparison.OrdinalIgnoreCase))
                    {
                        CreateTime = nodexx.InnerText;
                    }
                    else if (nodexx.Name.Equals("previewfilename", StringComparison.OrdinalIgnoreCase))
                    {
                        PreviewFileName = nodexx.InnerText;
                    }
                    else if (nodexx.Name.Equals("autorelease", StringComparison.OrdinalIgnoreCase))
                    {
                        AutoRelease = nodexx.InnerText.Equals("true", StringComparison.OrdinalIgnoreCase);
                    }
                    else if (nodexx.Name.Equals("labels", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (XmlNode nodexxx in nodexx.ChildNodes)
                        {
                            if (nodexxx.Name.Equals("label", StringComparison.OrdinalIgnoreCase))
                            {
                                Label.Label NewLabel = new Label.Label();
                                NewLabel.Parse(nodexxx);
                                if (NewLabel.Type == Label.Label.LabelType.DefaultLabel)
                                {
                                    DefaultLabel = NewLabel;
                                }
                                else
                                {
                                    Labels.Add(NewLabel);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void executeLua(ACA.LabelX.Label.Label currentLabel, ACA.LabelX.Label.Label defaultLabel, ACA.LabelX.Label.Label baseLabel, ACA.LabelX.Label.Label staticVarsLabel)
        {

            //Lua lua = new Lua();
            if (!String.IsNullOrEmpty(labelDef.luaCode))
            {
                Lua lua = new Lua();
                //try
                //{
                Dictionary<string, string> LabelVariablesDict = new Dictionary<string, string>();
                LabelVarsToLua(currentLabel, "cl", LabelVariablesDict,lua);
                LabelVarsToLua(defaultLabel, "dl", LabelVariablesDict,lua);
                LabelVarsToLua(baseLabel, "bl", LabelVariablesDict,lua);
                LabelVarsToLua(staticVarsLabel, "staticvar", LabelVariablesDict,lua);
                try
                {
                    lua.DoString(labelDef.luaCode);
                }
                catch (LuaException ex4)
                {
                    // A Lua Exception occurred. This can be a programming
                    // error, or for example a call to error()
                    //Future:
                    // In the future this has to lead to a skip of this label.
                    // Now we hide the error... This should not be done if called
                    // from the label designer... Also to be done in the future.
                    GlobalDataStore.Logger.Error(string.Format("Lua Exception: {0}", ex4.Message));
                }

                LuaTable globalLuaTable = lua.GetTable("_G");
                Dictionary<string, string> LuaVariablesDict = new Dictionary<string, string>();
                
                foreach (DictionaryEntry de in globalLuaTable){
                    LuaVariablesDict.Add(de.Key.ToString(), de.Value.ToString()); 
                }

                LuaVarsToLabel(LabelVariablesDict, currentLabel, defaultLabel, baseLabel,staticVarsLabel,LuaVariablesDict,lua);
                //}
                //catch (Exception e)
                //{
                //    System.Diagnostics.Debug.WriteLine(e.Message);
                //}
                lua.Dispose();
            }
            //lua.Dispose();
        }
        private void LabelVarsToLua(ACA.LabelX.Label.Label label,string prefix, Dictionary<string,string> dictionary, Lua lua)
        {//Converts label variables to lua variables, puts the lua variables in a dictionary and adds them to lua.
            foreach (KeyValuePair<string,ACA.LabelX.Label.Label.Value> pair in label.Values)
            {
                string luaVarName = VarNameToLua(pair.Key, prefix);
                try
                {
                    dictionary.Add(luaVarName, pair.Key); //TODO handle duplicates
                }
                catch (Exception e)
                {
                    GlobalDataStore.Logger.Error(e.Message);
                }
                lua[luaVarName] = pair.Value.Data;
            }
        }
        private string VarNameToLua(string varName,string prefix)
        {
            return prefix + "_" + varName.Replace('\\', '_').Replace(' ', '_').Replace('.', '_').Replace('-','_');
        }
        private void LuaVarsToLabel(Dictionary<string, string> LabelVariablesDict, ACA.LabelX.Label.Label currentLabel, ACA.LabelX.Label.Label defaultLabel, ACA.LabelX.Label.Label baseLabel, ACA.LabelX.Label.Label staticVarsLabel, Dictionary<string,string> LuaVariablesDict, Lua lua)
        {//Converts Dictionary with lua variable names back to their labels

            foreach (KeyValuePair<string, string> pair in LuaVariablesDict)
            {
                ACA.LabelX.Label.Label labelToUse = null;
                if (pair.Key.StartsWith("cl_"))
                {
                    labelToUse = currentLabel;
                }
                else if (pair.Key.StartsWith("dl_"))
                {
                    labelToUse = defaultLabel;
                }
                else if (pair.Key.StartsWith("bl_"))
                {
                    labelToUse = baseLabel;
                }
                else if (pair.Key.StartsWith("staticvar_"))
                {
                    labelToUse = staticVarsLabel;
                }
                if (labelToUse != null)
                {
                    ACA.LabelX.Label.Label.Value value;
                    string labelPrintNaam;

                    if (LabelVariablesDict.TryGetValue(pair.Key, out labelPrintNaam))
                    {
                        if (labelToUse.Values.TryGetValue(labelPrintNaam, out value))
                        {
                            string luaValue = lua[pair.Key].ToString();

                            if (luaValue != null)
                            {
                                value.Data = luaValue;
                            }
                            /* mve fix
                            if (!string.IsNullOrEmpty(luaValue))
                            {
                                value.Data = luaValue;
                            }
                             */
                        }
                    }
                    else if (pair.Key.StartsWith("staticvar_")) //If the key doesn't exist but is a staticvar, add it
                    {
                        string[] stringAr = pair.Key.Split('_');
                        if (isInteger(stringAr[stringAr.Length -1])) //If last part isn't a language code, don't bother
                        {
                            string languagecode = stringAr[stringAr.Length - 1];
                            string variableName = "";
                            for (int i = 1; i < stringAr.Length - 1 ; i++)
                            {
                                variableName = variableName + stringAr[i] + '_'; 
                            }
                            variableName = variableName.Substring(0, variableName.Length - 1);
                            variableName = variableName + '.' + languagecode;
                            
                            ACA.LabelX.Label.Label.Value tempvalue = new ACA.LabelX.Label.Label.Value();
                            tempvalue.Type = "xs:string";
                            tempvalue.Key = variableName;
                            tempvalue.Data = pair.Value;
                            KeyValuePair<string, ACA.LabelX.Label.Label.Value> tempKeyValuePair = new KeyValuePair<string, ACA.LabelX.Label.Label.Value>(variableName,tempvalue);                        
                            labelToUse.Values.Add(tempKeyValuePair);
                        }
                    }
                }

            }
        }
        private static bool isInteger(string input)
        {
            try
            {
                Convert.ToInt32(input);
                return true;
            }
            catch
            {
                return false;
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
