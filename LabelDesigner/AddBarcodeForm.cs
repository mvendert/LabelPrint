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
using ACA.LabelX.Label;
using ACA.LabelX.Tools;
using ACA.LabelX.Toolbox;
using System.Windows.Forms;

namespace LabelDesigner
{
    public partial class AddBarcodeForm : FormBase
    {
        private IDictionary<string, LabelDef.Field> referenceList;
        private LabelDef.BarcodeField barcodefield;
        private string id;
        private string value;

        public IDictionary<string, LabelDef.Field> ReferenceList
        {
            get { return referenceList; }
            set { referenceList = value; }
        }

        public LabelDef.BarcodeField Barcodefield
        {
            get { return barcodefield; }
            set { barcodefield = value; }
        }

        //Startup
        public AddBarcodeForm(IDictionary<string, LabelDef.Field> mainreferenceList, string id, string value)
        {
            InitializeComponent();
            referenceList = mainreferenceList;
            typecombo.Items.AddRange(new String[] { "EAN8", "EAN13", "UPCVersionA", "Code39", "Interleaved2Of5", "EAN128", "DataMatrix","PDF417" });
            typecombo.SelectedIndex = 0;
            alignTextcombo.Items.AddRange(new String[] { "Left", "Right" });
            alignTextcombo.SelectedIndex = 0;
            rotationcombo.Items.AddRange(new String[] { "0", "90", "180", "270" });
            rotationcombo.SelectedIndex = 0;
            maxcharcountcheck.Checked = false;
            FillReferenceCombo();
            SetFormLanguage();
            //If id already exists then make a new id + _x
            if (mainreferenceList.ContainsKey(id))
            {
                bool available = false;
                int idCounter = 1;
                string tempfieldName = id;
                while (!(available))
                {
                    if (mainreferenceList.ContainsKey(tempfieldName))
                    {
                        tempfieldName = id + "_" + idCounter;
                        idCounter++;
                    }
                    else
                    {
                        available = true;
                    }
                }
                this.id = tempfieldName;
                referencecombo.Enabled = false;
                referencecombo.SelectedItem = id;
            }
            else
            {
                this.id = id;
            }
            this.value = value;

        }
        private void AddBarcodeForm_Load(object sender, EventArgs e)
        {
            IDtxt.Text = id;
            valuetxt.Text = value;
        }

        //Main Function
        private void Okbtn_Click(object sender, EventArgs e)
        {
            try
            {
                barcodefield = new LabelDef.BarcodeField(new ACA.LabelX.Tools.CoordinateSystem(200, 200, GetString("MILLIMETER")));
                barcodefield.ID = IDtxt.Text;
                barcodefield.PositionX = new Length(System.Convert.ToInt32(XPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                barcodefield.PositionY = new Length(System.Convert.ToInt32(YPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                barcodefield.Rotation = System.Convert.ToInt32(rotationcombo.SelectedItem.ToString());
                barcodefield.Height = new Length(System.Convert.ToInt32(heighttxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                barcodefield.Width = new Length(System.Convert.ToInt32(widthtxt.Text), System.Drawing.GraphicsUnit.Millimeter);

                LabelDef.StringFormat stringformat = new LabelDef.StringFormat(true);
                if (alignTextcombo.SelectedItem.ToString().Equals(GetString("LEFT"), StringComparison.OrdinalIgnoreCase))
                    stringformat.Align = LabelDef.FieldFormat.Alignment.Left;
                else if (alignTextcombo.SelectedItem.ToString().Equals(GetString("RIGHT"), StringComparison.OrdinalIgnoreCase))
                    stringformat.Align = LabelDef.FieldFormat.Alignment.Right;

                string barcodetype = typecombo.SelectedItem.ToString();
                switch (barcodetype)
                {
                    case "EAN8":
                        barcodefield.Type = LabelDef.BarcodeField.BarcodeType.EAN8;
                        break;
                    case "EAN13":
                        barcodefield.Type = LabelDef.BarcodeField.BarcodeType.EAN13;
                        break;
                    case "UPCVersionA":
                        barcodefield.Type = LabelDef.BarcodeField.BarcodeType.UPCVersionA;
                        break;
                    case "Code39":
                        barcodefield.Type = LabelDef.BarcodeField.BarcodeType.Code39;
                        break;
                    case "Interleaved2Of5":
                        barcodefield.Type = LabelDef.BarcodeField.BarcodeType.Interleaved2Of5;
                        break;
                    case "EAN128":
                        barcodefield.Type = LabelDef.BarcodeField.BarcodeType.EAN128;
                        break;
                    case "DataMatrix":
                        barcodefield.Type = LabelDef.BarcodeField.BarcodeType.DataMatrix;
                        break;
                    case "PDF417":
                        barcodefield.Type = LabelDef.BarcodeField.BarcodeType.PDF417;
                        break;
                }

                barcodefield.MaxCharCount = System.Convert.ToInt32(maxcharcounttxt.Text);         
                barcodefield.printText = showtextcheck.Checked;                             
                barcodefield.printFormat = new LabelDef.PrintFormat(stringformat);

                if (referencecombo.SelectedItem.ToString().Equals(GetString("NOTHING")))
                    barcodefield.ValueRef = null;
                else
                    barcodefield.ValueRef = referencecombo.SelectedItem.ToString();

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show(GetString("INVALIDINPUTERROR"), GetString("INVALIDINPUTERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Internal functions
        private void SetFormLanguage()
        {
            this.Text = GetString("ADDBARCODE");
            idlbl.Text = GetString("ID");
            valuelbl.Text = GetString("VALUE");
            typelbl.Text = GetString("TYPE");
            xposlbl.Text = GetString("XPOSITION");
            yposlbl.Text = GetString("YPOSITION");
            rotationlbl.Text = GetString("ROTATION");
            alignlbl.Text = GetString("ALIGNTEXT");
            widthlbl.Text = GetString("WIDTH");
            heightlbl.Text = GetString("HEIGHT");
            referencelbl.Text = GetString("REFERENCE");
            maxcharcountcheck.Text = GetString("USECHARACTERLIMIT");
            maxcharcountlbl.Text = GetString("CHARACTERLIMIT");
            showtextcheck.Text = GetString("SHOWTEXT");
            Okbtn.Text = GetString("DONE");

            alignTextcombo.Items.Clear();
            alignTextcombo.Items.AddRange(new String[] { GetString("LEFT"), GetString("RIGHT") });
            alignTextcombo.SelectedIndex = 0;
            FillReferenceCombo();
        }
        private void FillReferenceCombo()
        {
            referencecombo.Items.Clear();
            try
            {
                referencecombo.Items.Add(GetString("NOTHING"));
            }
            catch
            {
                referencecombo.Items.Add("Nothing");
            }
            foreach (LabelDef.Field field in referenceList.Values)
            {
                if (!(field is LabelDef.TextFieldGroup))
                    referencecombo.Items.Add(field.ID);
            }
            if (referencecombo.Items.Count == 0)
                referencecombo.Enabled = false;
            else
            {
                referencecombo.Enabled = true;
                referencecombo.SelectedIndex = 0;
            }
        }

        private void maxcharcountcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (maxcharcountcheck.Checked)
            {
                maxcharcounttxt.Enabled = true;
                maxcharcountlbl.Enabled = true;
            }
            else
            {
                maxcharcounttxt.Enabled = false;
                maxcharcountlbl.Enabled = false;
            }
        }
        private void typecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (typecombo.SelectedItem.ToString())
            {
                case "EAN8":
                    maxcharcounttxt.Text = "8";
                    break;
                case "EAN13":
                    maxcharcounttxt.Text = "13";
                    break;
                case "UPCVersionA":
                    maxcharcounttxt.Text = "13";
                    break;
                case "Code39":
                    maxcharcounttxt.Text = "22";
                    break;
                case "Interleaved2Of5":
                    maxcharcounttxt.Text = "22";
                    break;
                case "EAN128":
                    maxcharcounttxt.Text = "22";
                    break;
                case "DataMatrix":
                    maxcharcounttxt.Text = "3000";
                    showtextcheck.Checked = false;
                    break;
                case "PDF417":
                    maxcharcounttxt.Text = "3000";
                    showtextcheck.Checked = false;
                    break;
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
