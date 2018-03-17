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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using ACA.LabelX.Label;
using ACA.LabelX.Tools;
using ACA.LabelX.Toolbox;
using System.Threading;

namespace LabelDesigner
{
    public partial class NewLabelDefForm : FormBase
    {
        private enum Mode { create , edit };
        private string paperDefinitionsRootFolder;
        private string labelDefinitionsRootFolder;
        private LabelDef newLabelDef;
        private string setAsDefault = string.Empty;
        private LabelDef labeldeftoLoad;
        private IDictionary<string, ACA.LabelX.Paper.PaperDef> PaperTypeList = new Dictionary<string, ACA.LabelX.Paper.PaperDef>();
        public string LabelName = string.Empty;

        private Mode mode = Mode.create;

        //Startup
        public NewLabelDefForm(string paperDefinitionsRootFolder, string labelDefinitionsRootFolder)
        {
            this.paperDefinitionsRootFolder = paperDefinitionsRootFolder;
            this.labelDefinitionsRootFolder = labelDefinitionsRootFolder;
            InitializeComponent();
            unitcombo.Items.AddRange(new string[] {"Millimeter"}); //TODO: which ones are supported?
            unitcombo.SelectedIndex = 0;
            LoadAvailablePaperTypes();
            SetFormLanguage();
        }

        public NewLabelDefForm(string paperDefinitionsRootFolder, string labelDefinitionsRootFolder, LabelDef labeldefToLoad)
            :this(paperDefinitionsRootFolder,labelDefinitionsRootFolder)
        {
            mode = Mode.edit;
            this.labeldeftoLoad = labeldefToLoad;
            idtxt.Text = labeldeftoLoad.ID;
            idtxt.Enabled = false;

            DPIXtxt.Text = labeldeftoLoad.coordinateSystem.dpiFactor.X.ToString();
            DPIYtxt.Text = labeldeftoLoad.coordinateSystem.dpiFactor.Y.ToString();
            unitcombo.Enabled = false;
            DPIXtxt.Enabled = false;
            DPIYtxt.Enabled = false;

            foreach (LabelDef.PaperType papertype in labeldeftoLoad.PaperTypes)
            {
                if (availablepapertypelist.Items.Contains(papertype.ID))
                {
                    availablepapertypelist.SelectedItem = papertype.ID;
                    addpapertypebtn_Click(null, null);
                    if (papertype.Default)
                    {
                        setAsDefault = papertype.ID;
                    }
                }
                else
                {
                    MessageBox.Show("Paper ID " + papertype.ID + " is supported but was not found.","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            if (usedpapertypelist.Items.Count < 1)
            {
                Okbtn.Enabled = false;
            }

        }
        private void LoadAvailablePaperTypes()
        {
            string[] filepaths = Directory.GetFiles(paperDefinitionsRootFolder);
            ACA.LabelX.Paper.PaperDef paperdef;
            PaperTypeList.Clear();
            foreach (string path in filepaths)
            {
                if (path.EndsWith(@".xml"))
                {
                    paperdef = new ACA.LabelX.Paper.PaperDef();
                    paperdef.Parse(path);
                    try
                    {
                        PaperTypeList.Add(paperdef.ID, paperdef);
                    }
                    catch (ArgumentException)
                    {
                        System.Diagnostics.Debug.WriteLine("Duplicate papertype found and ignored");
                    }
                }
            }
            ShowAvailablePaperTypes();
        }
        private void ShowAvailablePaperTypes()
        {
            availablepapertypelist.Items.Clear();
            foreach (string paperdefid in PaperTypeList.Keys)
            {
                if (!(usedpapertypelist.Items.Contains(paperdefid)))
                {
                    availablepapertypelist.Items.Add(paperdefid);
                }
            }
        }

        //Main Functions
        private void Okbtn_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                setDefaultLabel();
                if (mode == Mode.create)
                {
                    newLabelDef = new LabelDef();
                    newLabelDef.ID = idtxt.Text;
                    newLabelDef.coordinateSystem = new CoordinateSystem(Convert.ToUInt32(DPIXtxt.Text), Convert.ToUInt32(DPIYtxt.Text), unitcombo.SelectedItem.ToString());

                    List<LabelDef.PaperType> tempPaperTypeList = new List<LabelDef.PaperType>();
                    foreach (ACA.LabelX.Paper.PaperDef tempPaperDef in PaperTypeList.Values)
                    {
                        if (usedpapertypelist.Items.Contains(tempPaperDef.ID))
                        {

                            LabelDef.PaperType tempPaperType = new LabelDef.PaperType();
                            tempPaperType.ID = tempPaperDef.ID;
                            if (tempPaperDef.ID.Equals(setAsDefault))
                            {
                                tempPaperType.Default = true;
                            }
                            else
                                tempPaperType.Default = false;

                            tempPaperTypeList.Add(tempPaperType);
                        }


                    }
                    newLabelDef.PaperTypes = tempPaperTypeList;
                    saveLabelDefTo(labelDefinitionsRootFolder + @"\" + newLabelDef.ID + ".xml");
                    LabelName = newLabelDef.ID;
                    DialogResult = DialogResult.OK;
                }
                else if (mode == Mode.edit)
                {
                    lock (ACA.LabelX.GlobalDataStore.LockClass)
                    {
                        setDefaultLabel();
                        string path = labelDefinitionsRootFolder + @"\" + labeldeftoLoad.ID + ".xml";
                        XmlDocument theDoc = new XmlDocument();
                        theDoc.Load(path);

                        XmlNode validpapertypes = theDoc.SelectSingleNode("/labeldef/validpapertypes");
                        validpapertypes.RemoveAll();
                        foreach (string papertype in usedpapertypelist.Items)
                        {
                            XmlElement paper = theDoc.CreateElement("paper");
                            paper.SetAttribute("type", papertype);
                            if (setAsDefault.Equals(papertype, StringComparison.OrdinalIgnoreCase))
                                paper.SetAttribute("default", "true");
                            else
                                paper.SetAttribute("default", "false");

                            validpapertypes.AppendChild(paper);
                        }


                        int teller = 0;
                        bool succes = false;
                        while ((teller < 5) && (succes == false))
                        {
                            theDoc.Save(path);
                            Thread.Sleep(2000);
                            succes = true;
                            teller++;
                        }

                        if (!succes)
                        {
                            MessageBox.Show("Error while saving file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    DialogResult = DialogResult.OK;
                    LabelName = idtxt.Text;
                }
                this.Close();
            }
            else
                MessageBox.Show(GetString("INVALIDINPUTERROR"), GetString("INVALIDINPUTERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void setDefaultLabel()
        {
            if (string.IsNullOrEmpty(setAsDefault))
            {
                setAsDefault = usedpapertypelist.Items[0].ToString();
            }
        }
        private void saveLabelDefTo(string path)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(path, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("labeldef");
            xmlWriter.WriteAttributeString("id", newLabelDef.ID);
            xmlWriter.WriteStartElement("validpapertypes");
            foreach (LabelDef.PaperType papertype in newLabelDef.PaperTypes)
            {
                xmlWriter.WriteStartElement("paper");
                xmlWriter.WriteAttributeString("type", papertype.ID);
                xmlWriter.WriteAttributeString("default", papertype.Default.ToString().ToLower());
                xmlWriter.WriteEndElement(); //</paper>
            }
            xmlWriter.WriteEndElement(); //</validpapertypes>

            xmlWriter.WriteStartElement("coordinates");
            xmlWriter.WriteStartElement("dpifactor");
            xmlWriter.WriteAttributeString("x", newLabelDef.coordinateSystem.dpiFactor.X.ToString());
            xmlWriter.WriteAttributeString("y", newLabelDef.coordinateSystem.dpiFactor.Y.ToString());
            xmlWriter.WriteEndElement(); //</dpifactor>
            xmlWriter.WriteElementString("units", newLabelDef.coordinateSystem.units.UnitType.ToString());
            xmlWriter.WriteEndElement(); //</coordinates>

            xmlWriter.WriteStartElement("definition");
            xmlWriter.WriteStartElement("fonts");
            xmlWriter.WriteStartElement("font");
            xmlWriter.WriteAttributeString("id", "Default");
            xmlWriter.WriteElementString("typeface", "Arial");
            xmlWriter.WriteElementString("size", "8");
            xmlWriter.WriteElementString("inverseprint", "false");
            xmlWriter.WriteEndElement(); //</font>
            xmlWriter.WriteEndElement(); //</fonts>

            xmlWriter.WriteStartElement("fields");
            xmlWriter.WriteEndElement(); //fields

            xmlWriter.WriteEndElement(); //</definition>
            xmlWriter.WriteStartElement("label");
            xmlWriter.WriteAttributeString("type", "default");
            xmlWriter.WriteStartElement("options");
            xmlWriter.WriteElementString("quantity", "1");
            xmlWriter.WriteStartElement("cultureinfos");
            xmlWriter.WriteStartElement("cultureinfo");
            xmlWriter.WriteAttributeString("language", "system");
            xmlWriter.WriteEndElement(); //</cultureinfo>
            xmlWriter.WriteEndElement(); //</cultureinfos>
            xmlWriter.WriteEndElement(); //</options>
            xmlWriter.WriteStartElement("values");
            xmlWriter.WriteEndElement(); //</values>
            xmlWriter.WriteEndElement(); //</label>
            xmlWriter.WriteEndElement(); //</labeldef>
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
        }
        private void newBtn_Click(object sender, EventArgs e)
        {
            NewPaperTypeForm newPaperTypeForm = new NewPaperTypeForm(paperDefinitionsRootFolder);
            if (newPaperTypeForm.ShowDialog() == DialogResult.OK)
            {
                PaperTypeList.Add(newPaperTypeForm.newpaperdef.ID, newPaperTypeForm.newpaperdef);
                usedpapertypelist.Items.Add(newPaperTypeForm.newpaperdef.ID);
                setAsDefault = newPaperTypeForm.newpaperdef.ID;
                usedpapertypelist.SelectedItem = newPaperTypeForm.newpaperdef.ID;
                Okbtn.Enabled = true;
            }
        }

        //Internal Functions
        private void SetFormLanguage()
        {
            this.Text = GetString("CREATENEWLABELDEFINITION");
            idlbl.Text = GetString("ID");
            supportedpapertypeslbl.Text = GetString("SUPPORTEDPAPERTYPES");
            defaultcheck.Text = GetString("SETPAPERASDEFAULT");
            newBtn.Text = GetString("NEW");
            DPIlbl.Text = GetString("DPI");
            unitlbl.Text = GetString("UNIT");
            availablepapertypeslbl.Text = GetString("AVAILABLEPAPERTYPES");
            propertieslbl.Text = GetString("PROPERTIES");
            propidlbl.Text = GetString("ID");
            propDPIlbl.Text = GetString("DPI");
            propsizelbl.Text = GetString("SIZE");
            propnrhorzlblslbl.Text = GetString("NUMBERHORZLBLS");
            propnrvertlblslbl.Text = GetString("NUMBERVERTLBLS");
            prophorzinterlblgaplbl.Text = GetString("HORZINTERLBLGAP");
            propvertinterlblgaplbl.Text = GetString("VERTINTERLBLGAP");
            unitcombo.Items.Clear();
            unitcombo.Items.AddRange(new string[] { GetString("MILLIMETER") }); //TODO: which ones are supported?
            unitcombo.SelectedIndex = 0;
            Okbtn.Text = GetString("DONE");

            
        }
        
        private void ShowPaperTypeProperties(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem != null)
            {
                ACA.LabelX.Paper.PaperDef temppaperdef;
                PaperTypeList.TryGetValue(((ListBox)sender).SelectedItem.ToString(), out temppaperdef);
                propid.Text = temppaperdef.ID;
                propDPI.Text = temppaperdef.coordinateSystem.dpiFactor.X + " x " + temppaperdef.coordinateSystem.dpiFactor.Y + " " + temppaperdef.coordinateSystem.units.UnitType.ToString();
                propsize.Text = temppaperdef.size.Width.length.ToString() + " x " + temppaperdef.size.Height.length.ToString();
                prophorzlbls.Text = temppaperdef.labelLayout.HorizontalCount.ToString();
                propvertlbls.Text = temppaperdef.labelLayout.VerticalCount.ToString();
                prophorzinterlblgap.Text = temppaperdef.labelLayout.HorizontalInterlabelGap.length.ToString();
                propvertinterlblgap.Text = temppaperdef.labelLayout.VerticalInterlabelGap.length.ToString();
            }
        }
        
        private void usedpapertypelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPaperTypeProperties(sender, e);

            this.defaultcheck.CheckedChanged -= new System.EventHandler(this.defaultcheck_CheckedChanged);
            if (usedpapertypelist.SelectedItem != null)
            {
                if (usedpapertypelist.SelectedItem.ToString().Equals(setAsDefault))
                    defaultcheck.Checked = true;
                else
                    defaultcheck.Checked = false;
            }
            this.defaultcheck.CheckedChanged += new System.EventHandler(this.defaultcheck_CheckedChanged);

            if (usedpapertypelist.SelectedItems.Count < 1)
                defaultcheck.Enabled = false;
            else
                defaultcheck.Enabled = true;
        }
        private void defaultcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (usedpapertypelist.SelectedItem != null)
                setAsDefault = usedpapertypelist.SelectedItem.ToString();
        }

        private void addpapertypebtn_Click(object sender, EventArgs e)
        {
            if (availablepapertypelist.SelectedItem != null)
            {
                usedpapertypelist.Items.Add(availablepapertypelist.SelectedItem);
                availablepapertypelist.Items.Remove(availablepapertypelist.SelectedItem);
            }

            if (availablepapertypelist.Items.Count < 1)
                addpapertypebtn.Enabled = false;
            if (usedpapertypelist.Items.Count > 0)
            {
                removepapertypebtn.Enabled = true;
                Okbtn.Enabled = true;
            }

        }
        private void removepapertypebtn_Click(object sender, EventArgs e)
        {
            if (usedpapertypelist.SelectedItem != null)
            {
                if (usedpapertypelist.SelectedItem.ToString().Equals(setAsDefault))
                {
                    setAsDefault = string.Empty;
                    this.defaultcheck.CheckedChanged -= new System.EventHandler(this.defaultcheck_CheckedChanged);
                    defaultcheck.Checked = false;
                    this.defaultcheck.CheckedChanged += new System.EventHandler(this.defaultcheck_CheckedChanged);
                }
                availablepapertypelist.Items.Add(usedpapertypelist.SelectedItem);
                usedpapertypelist.Items.Remove(usedpapertypelist.SelectedItem);
            }

            if (usedpapertypelist.Items.Count < 1)
            {
                removepapertypebtn.Enabled = false;
                Okbtn.Enabled = false;
            }
            if (availablepapertypelist.Items.Count > 0)
                addpapertypebtn.Enabled = true;
            
        }

        private bool CheckInput()
        {
            bool result = true;
            if (idtxt.Text.Length < 1)
                result = false;

            if (usedpapertypelist.Items.Count < 1)
                result = false;

            try
            {
                int tempDPIx = Convert.ToInt32(DPIXtxt.Text);
            }
            catch
            {
                result = false;
            }

            try
            {
                int tempDPIy = Convert.ToInt32(DPIYtxt.Text);
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
