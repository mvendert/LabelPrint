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
using System.Globalization;
using ACA.LabelX.Label;
using ACA.LabelX.Paper;
using ACA.LabelX.Tools;
using ACA.LabelX.Toolbox;

namespace LabelDesigner
{
    public partial class NewPaperTypeForm : FormBase
    {
        private enum Mode { create, edit };
        public ACA.LabelX.Paper.PaperDef newpaperdef;
        private string paperDefinitionRootFolder = string.Empty;
        public string PaperType = string.Empty;
        private Mode mode = Mode.create;
        private PaperDef paperDefToEdit;
        private IDictionary<string, Offset> LocalOffsets;        

        //Startup
        public NewPaperTypeForm(string paperDefinitionRootFolder)
        {
            this.paperDefinitionRootFolder = paperDefinitionRootFolder;
            InitializeComponent();
            unitcombo.Items.AddRange(new string[] { "Millimeter" }); //TODO: which ones are supported?
            unitcombo.SelectedIndex = 0;
            LocalOffsets = new Dictionary<string,Offset>();
            SetFormLanguage();
            RefreshSpecOffsetListBox("");
        }
        public NewPaperTypeForm(string paperDefinitionRootFolder, ACA.LabelX.Paper.PaperDef paperDefToEdit)
            : this(paperDefinitionRootFolder)
        {
            mode = Mode.edit;
            this.paperDefToEdit = paperDefToEdit;
            LocalOffsets = paperDefToEdit.Offsets;
            idtxt.Text = paperDefToEdit.ID;
            idtxt.Enabled = false;

            DPIXtxt.Text = paperDefToEdit.coordinateSystem.dpiFactor.X.ToString();
            DPIYtxt.Text = paperDefToEdit.coordinateSystem.dpiFactor.Y.ToString();
            unitcombo.Enabled = false;
            DPIXtxt.Enabled = false;
            DPIYtxt.Enabled = false;

            sizextxt.Text = paperDefToEdit.size.Width.length.ToString();
            sizeytxt.Text = paperDefToEdit.size.Height.length.ToString();
            horzmargintxt.Text = paperDefToEdit.labelLayout.LeftMargin.length.ToString();
            vertmargintxt.Text = paperDefToEdit.labelLayout.TopMargin.length.ToString();
            nrhorzlblstxt.Text = paperDefToEdit.labelLayout.HorizontalCount.ToString();
            nrvertlblstxt.Text = paperDefToEdit.labelLayout.VerticalCount.ToString();
            horzgaptxt.Text = paperDefToEdit.labelLayout.HorizontalInterlabelGap.length.ToString();
            vertgaptxt.Text = paperDefToEdit.labelLayout.VerticalInterlabelGap.length.ToString();
            
            string defhorzoffsetstr = "0";
            string defvertoffsetstr = "0";
            if (LocalOffsets.ContainsKey("@"))
            {
                defhorzoffsetstr = LocalOffsets["@"].LeftMarginOffset.length.ToString();
                defvertoffsetstr = LocalOffsets["@"].TopMarginOffset.length.ToString();
            }
            defhorzoffsettxt.Text = defhorzoffsetstr;
            defvertoffsettxt.Text = defvertoffsetstr;

            //Specific Offsets Tab
            RefreshSpecOffsetListBox("");
            
        }
        
        //Main Functions
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                newpaperdef = new ACA.LabelX.Paper.PaperDef();
                newpaperdef.ID = idtxt.Text;
                newpaperdef.coordinateSystem = new CoordinateSystem(Convert.ToUInt32(DPIXtxt.Text), Convert.ToUInt32(DPIYtxt.Text), unitcombo.SelectedItem.ToString());
                newpaperdef.size = new ACA.LabelX.Tools.Size(new Length(Convert.ToSingle(sizextxt.Text,CultureInfo.InvariantCulture), newpaperdef.coordinateSystem.units), new Length(Convert.ToSingle(sizeytxt.Text), newpaperdef.coordinateSystem.units));

                ACA.LabelX.Paper.LabelLayout templabellayout = new ACA.LabelX.Paper.LabelLayout(newpaperdef.coordinateSystem.units);
                templabellayout.LeftMargin = new Length(Convert.ToSingle(horzmargintxt.Text, CultureInfo.InvariantCulture), newpaperdef.coordinateSystem.units);
                templabellayout.TopMargin = new Length(Convert.ToSingle(vertmargintxt.Text, CultureInfo.InvariantCulture), newpaperdef.coordinateSystem.units);
                templabellayout.HorizontalCount = Convert.ToUInt32(nrhorzlblstxt.Text);
                templabellayout.VerticalCount = Convert.ToUInt32(nrvertlblstxt.Text);
                templabellayout.HorizontalInterlabelGap = new Length(Convert.ToSingle(horzgaptxt.Text, CultureInfo.InvariantCulture), newpaperdef.coordinateSystem.units);
                templabellayout.VerticalInterlabelGap = new Length(Convert.ToSingle(vertgaptxt.Text, CultureInfo.InvariantCulture), newpaperdef.coordinateSystem.units);

                newpaperdef.labelLayout = templabellayout;

                //Add default offset to local offsets
                Offset tempDefaultOffset = new Offset(newpaperdef.coordinateSystem.units);
                tempDefaultOffset.LeftMarginOffset = new Length(Convert.ToSingle(defhorzoffsettxt.Text,CultureInfo.InvariantCulture),newpaperdef.coordinateSystem.units);
                tempDefaultOffset.TopMarginOffset = new Length(Convert.ToSingle(defvertoffsettxt.Text,CultureInfo.InvariantCulture),newpaperdef.coordinateSystem.units);
                tempDefaultOffset.Machine = "";
                tempDefaultOffset.Printer = "";   
                if (LocalOffsets.ContainsKey("@")){
                    LocalOffsets.Remove("@");
                }
                LocalOffsets.Add("@",tempDefaultOffset); 
                
                //Add all offsets to new paper
                newpaperdef.Offsets = LocalOffsets;


                if (mode == Mode.edit)
                {
                    File.Delete(paperDefinitionRootFolder + @"\" + newpaperdef.ID + @".xml");
                }
                savePaperDefTo(paperDefinitionRootFolder + @"\" + newpaperdef.ID + @".xml");
                PaperType = newpaperdef.ID;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show(GetString("INVALIDINPUTERROR"), GetString("INVALIDINPUTERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void savePaperDefTo(string path)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(path, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("paper");
            xmlWriter.WriteAttributeString("type", newpaperdef.ID);

            xmlWriter.WriteStartElement("coordinates");
            xmlWriter.WriteStartElement("dpifactor");
            xmlWriter.WriteAttributeString("x", newpaperdef.coordinateSystem.dpiFactor.X.ToString());
            xmlWriter.WriteAttributeString("y", newpaperdef.coordinateSystem.dpiFactor.Y.ToString());
            xmlWriter.WriteEndElement(); //</dpifactor>
            xmlWriter.WriteElementString("units", newpaperdef.coordinateSystem.units.UnitType.ToString());
            xmlWriter.WriteEndElement(); //</coordinates>

            xmlWriter.WriteStartElement("size");
            xmlWriter.WriteStartElement("width");
            xmlWriter.WriteAttributeString("units", newpaperdef.size.Width.units.UnitType.ToString());
            xmlWriter.WriteRaw(newpaperdef.size.Width.length.ToString());
            xmlWriter.WriteEndElement(); //</width>

            xmlWriter.WriteStartElement("height");
            xmlWriter.WriteAttributeString("units", newpaperdef.size.Height.units.UnitType.ToString());
            xmlWriter.WriteRaw(newpaperdef.size.Height.length.ToString());
            xmlWriter.WriteEndElement(); //</height>
            xmlWriter.WriteEndElement(); //</size>

            xmlWriter.WriteStartElement("labelpos");
            xmlWriter.WriteElementString("horizontal", newpaperdef.labelLayout.HorizontalCount.ToString());
            xmlWriter.WriteElementString("vertical", newpaperdef.labelLayout.VerticalCount.ToString());
            xmlWriter.WriteElementString("horzoffset", newpaperdef.labelLayout.LeftMargin.length.ToString());
            xmlWriter.WriteElementString("vertoffset", newpaperdef.labelLayout.TopMargin.length.ToString());
            xmlWriter.WriteElementString("horzinterlabelgap", newpaperdef.labelLayout.HorizontalInterlabelGap.length.ToString());
            xmlWriter.WriteElementString("vertinterlabelgap", newpaperdef.labelLayout.VerticalInterlabelGap.length.ToString());
            xmlWriter.WriteEndElement(); //</labelpos>

            //TODO: Can be implemented, is supported in code
            //<offset horzoffset="20" vertoffset="10" horzinterlabelgapdelta="0" vertinterlabeldelta="0" machine="nav01" printer="tec2" /> 
            xmlWriter.WriteStartElement("offsets");
            
            foreach (Offset offset in newpaperdef.Offsets.Values)
            {
                xmlWriter.WriteStartElement("offset");                
                xmlWriter.WriteAttributeString("horzoffset", offset.LeftMarginOffset.length.ToString());
                xmlWriter.WriteAttributeString("vertoffset", offset.TopMarginOffset.length.ToString());
                xmlWriter.WriteAttributeString("machine", offset.Machine);
                xmlWriter.WriteAttributeString("printer", offset.Printer);
                xmlWriter.WriteEndElement(); //</offset>
            }            
            xmlWriter.WriteEndElement(); //</offsets>
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        //Internal Functions
        private void SetFormLanguage()
        {
            this.Text = GetString("CREATENEWPAPERDEFINITION");
            idlbl.Text = GetString("ID");
            DPIGroupBox.Text = GetString("DPI");
            unitlbl.Text = GetString("UNIT");
            PaperSizeGroupBox.Text = GetString("SIZE");
            horzmarginlbl.Text = GetString("HORZOFFSET");
            vertmarginlbl.Text = GetString("VERTOFFSET");
            horzlblslbl.Text = GetString("NUMBERHORZLBLS2");
            vertlblslbl.Text = GetString("NUMBERVERTLBLS2");
            horzinterlblgaplbl.Text = GetString("HORZINTERLBLGAP2");
            verticalinterlblgaplbl.Text = GetString("VERTINTERLBLGAP2");
            defhorzoffsetlbl.Text = GetString("HORZOFFSET");
            defvertoffsetlbl.Text = GetString("VERTOFFSET");

            GeneralTab.Text = GetString("GENERALTAB");
            SpecificOffsetTab.Text = GetString("SPECIFICOFFSETTAB");
            unitcombo.Items.Clear();
            unitcombo.Items.AddRange(new string[] { GetString("MILLIMETER") });
            unitcombo.SelectedIndex = 0;

            //Specific Offset Tab --> TODO Finish
            CurrentOffsetsLbl.Text = GetString("CURRENTOFFSETS");
            PropertyBox.Text = GetString("PROPERTIES");
            ClientNamelbl.Text = GetString("CLIENTNAME");
            PrinterNamelbl.Text = GetString("PRINTERNAME");
            SpecHorzOffsetlbl.Text = GetString("HORZOFFSET");
            SpecVertOffsetlbl.Text = GetString("VERTOFFSET");
            DeleteOffsetBtn.Text = GetString("DELETE");
            SaveOffsetBtn.Text = GetString("SAVE");

            
        }
        private bool CheckInput()
        {
            bool result = true;

            if (idtxt.Text.Length < 1)
                result = false;

            try
            {
                int tempDPIx = Convert.ToInt32(DPIXtxt.Text);
                int tempDPIy = Convert.ToInt32(DPIYtxt.Text);
                int tempsizex = Convert.ToInt32(sizextxt.Text);
                int tempsizey = Convert.ToInt32(sizeytxt.Text);
                int temphorzoffset = Convert.ToInt32(horzmargintxt.Text);
                int tempvertoffset = Convert.ToInt32(vertmargintxt.Text);
                int tempnrhorzlbls = Convert.ToInt32(nrhorzlblstxt.Text);
                int tempnrvertlbls = Convert.ToInt32(nrvertlblstxt.Text);
                int temphorzgap = Convert.ToInt32(horzgaptxt.Text);
                int tempvertgap = Convert.ToInt32(vertgaptxt.Text);
                int tempdefhorzoffset = Convert.ToInt32(defhorzoffsettxt.Text);
                int tempdefvertoffset = Convert.ToInt32(defvertoffsettxt.Text);
            }
            catch
            {
                result = false;
            }

            return result;
        }
        
        //Special Offsets
        private void RefreshSpecOffsetListBox(string keytoselect)
        {
            SpecOffsetListbox.Items.Clear();

            foreach (string key in LocalOffsets.Keys)
            {
                if (!key.Equals("@"))
                {
                    SpecOffsetListbox.Items.Add(key);
                    if (keytoselect.Equals(key))
                    {
                        SpecOffsetListbox.SetSelected(SpecOffsetListbox.Items.Count - 1, true);
                    }
                }
            }

            if (SpecOffsetListbox.Items.Count > 0)
            {
                SpecOffsetListbox_SelectedIndexChanged(null, null);
            }
            else
            {
                SpecOffsetClientTxt.Text = "";
                SpecOffsetPrinterTxt.Text = "";
                SpecOffsetVertOffsetTxt.Text = "";
                SpecOffsetHorzOffsetTxt.Text = "";
                PropertyBox.Enabled = true;
            }

        }

        private void SpecOffsetListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SpecOffsetListbox.SelectedItems.Count == 1)
            {
                PropertyBox.Enabled = true;
                Offset currentSelectedOffset;
                LocalOffsets.TryGetValue(SpecOffsetListbox.SelectedItems[0].ToString(), out currentSelectedOffset);
                if (currentSelectedOffset != null)
                {
                    FillOffsetProperties(currentSelectedOffset);
                }                
            }
            else
            {
                PropertyBox.Enabled = false;
            }
        }

        private void SpecOffsetListbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Y > (SpecOffsetListbox.ItemHeight * SpecOffsetListbox.Items.Count))
            {
                SpecOffsetListbox.SelectedItems.Clear();
                SpecOffsetClientTxt.Text = "";
                SpecOffsetPrinterTxt.Text = "";
                SpecOffsetVertOffsetTxt.Text = "";
                SpecOffsetHorzOffsetTxt.Text = "";
                RefreshSpecOffsetListBox("");
            }
        }

        private void FillOffsetProperties(Offset currentOffset)
        {
            SpecOffsetClientTxt.Text = currentOffset.Machine;
            SpecOffsetPrinterTxt.Text = currentOffset.Printer;
            SpecOffsetVertOffsetTxt.Text = currentOffset.TopMarginOffset.length.ToString();
            SpecOffsetHorzOffsetTxt.Text = currentOffset.LeftMarginOffset.length.ToString();
        }

        private void SaveOffsetBtn_Click(object sender, EventArgs e)
        {
            try
            {

                if ((SpecOffsetClientTxt.Text != "") && (SpecOffsetPrinterTxt.Text != "") && (SpecOffsetListbox.SelectedItems.Count <= 1))
                {
                    CoordinateSystem.Units tempUnit = new CoordinateSystem.Units(GraphicsUnit.Millimeter);
                    Offset offset = new Offset(tempUnit);
                    offset.Machine = SpecOffsetClientTxt.Text;
                    offset.Printer = SpecOffsetPrinterTxt.Text;
                    offset.TopMarginOffset = new Length(Convert.ToSingle(SpecOffsetVertOffsetTxt.Text, CultureInfo.InvariantCulture), tempUnit);
                    offset.LeftMarginOffset = new Length(Convert.ToSingle(SpecOffsetHorzOffsetTxt.Text, CultureInfo.InvariantCulture), tempUnit);

                    string key = offset.Printer + "@" + offset.Machine;
                    Offset oldOffset;
                    if (LocalOffsets.TryGetValue(key, out oldOffset))
                    {
                        LocalOffsets.Remove(key);
                    }

                    LocalOffsets.Add(new KeyValuePair<string, Offset>(key, offset));
                    RefreshSpecOffsetListBox(key);
                }
            }
            catch { }
        }

        private void DeleteOffsetBtn_Click(object sender, EventArgs e)
        {
            string key = SpecOffsetPrinterTxt.Text + "@" + SpecOffsetClientTxt.Text;
            LocalOffsets.Remove(key);
            SpecOffsetClientTxt.Text = "";
            SpecOffsetPrinterTxt.Text = "";
            SpecOffsetVertOffsetTxt.Text = "";
            SpecOffsetHorzOffsetTxt.Text = "";
            RefreshSpecOffsetListBox("");

        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
