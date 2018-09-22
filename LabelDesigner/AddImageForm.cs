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
    public partial class AddImageForm : FormBase
    {
        private IDictionary<string, LabelDef.Field> referenceList;
        private LabelDef.ImageField imagefield;
        private string id;
        private string value;

        public LabelDef.ImageField Imagefield
        {
            get { return imagefield; }
            set { imagefield = value; }
        }

        public IDictionary<string, LabelDef.Field> ReferenceList
        {
            get { return referenceList; }
            set { referenceList = value; }
        }

        //Startup
        public AddImageForm(IDictionary<string, LabelDef.Field> mainreferenceList, string id, string value)
        {
            InitializeComponent();
            referenceList = mainreferenceList;
            rotationcombo.Items.AddRange(new String[] { "0", "90", "180", "270" });
            rotationcombo.SelectedIndex = 0;
            resizestylecombo.Items.AddRange(new String[] { "Normal", "Stretch" });
            resizestylecombo.SelectedIndex = 0;
            FillReferenceCombo();
            SetFormLanguage();

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
        private void AddImageForm_Load(object sender, EventArgs e)
        {
            IDtxt.Text = id;
            valuetxt.Text = value;
        }

        //Main function
        private void Okbtn_Click(object sender, EventArgs e)
        {
            try
            {
                imagefield = new LabelDef.ImageField(new ACA.LabelX.Tools.CoordinateSystem(200, 200, GetString("MILLIMETER")));
                imagefield.ID = IDtxt.Text;
                imagefield.PositionX = new Length(System.Convert.ToInt32(XPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                imagefield.PositionY = new Length(System.Convert.ToInt32(YPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                imagefield.Rotation = System.Convert.ToInt32(rotationcombo.SelectedItem.ToString());

                imagefield.KeepRatio = keepratiocheck.Checked;

                if (greyscalecheck.Checked)
                    imagefield.Color = LabelDef.ImageField.ColorStyle.Grayscale;
                else
                    imagefield.Color = LabelDef.ImageField.ColorStyle.Color;

                if (manualwhcheck.Checked && !heighttxt.Text.Equals("0"))
                {
                    imagefield.Height = new Length(System.Convert.ToInt32(heighttxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                }

                if (manualwhcheck.Checked && !widthtxt.Text.Equals("0"))
                {
                    imagefield.Width = new Length(System.Convert.ToInt32(widthtxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                }

                if (resizestylecombo.SelectedIndex == 0)
                    imagefield.Scale = LabelDef.ImageField.ScaleStyle.Normal;
                else if (resizestylecombo.SelectedIndex == 1)
                    imagefield.Scale = LabelDef.ImageField.ScaleStyle.Stretch;

                if (referencecombo.SelectedItem.ToString().Equals(GetString("NOTHING")))
                    imagefield.ValueRef = null;
                else
                    imagefield.ValueRef = referencecombo.SelectedItem.ToString();

                //mve,autorotate
                if (autorotatecombo.SelectedIndex == 0)
                {
                    Imagefield.AutoRotate = LabelDef.ImageField.AutoRotateStyle.NoAutoRotate;
                }
                else if (autorotatecombo.SelectedIndex == 1)
                {
                    Imagefield.AutoRotate = LabelDef.ImageField.AutoRotateStyle.AutoRotateClockwise;
                }
                else if (autorotatecombo.SelectedIndex == 2)
                {
                    Imagefield.AutoRotate = LabelDef.ImageField.AutoRotateStyle.AutoRotateCounterClockwise;
                }           
                //*

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show(GetString("INVALIDINPUTERROR"), GetString("INVALIDINPUTERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Internal functions
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
        private void SetFormLanguage()
        {
            this.Text = GetString("ADDIMAGE");
            idlbl.Text = GetString("ID");
            valuelbl.Text = GetString("VALUE");
            xposlbl.Text = GetString("XPOSITION");
            yposlbl.Text = GetString("YPOSITION");
            rotationlbl.Text = GetString("ROTATION");
            resizestylelbl.Text = GetString("RESIZESTYLE");
            resizestylecombo.Items.Clear();
            resizestylecombo.Items.AddRange(new String[] { GetString("NORMAL"), GetString("STRETCH") });
            resizestylecombo.SelectedIndex = 0;
            referencelbl.Text = GetString("REFERENCE");
            keepratiocheck.Text = GetString("KEEPRATIO");
            greyscalecheck.Text = GetString("GREYSCALE");
            manualwhcheck.Text = GetString("USEMANUALWIDTHANDHEIGHT");
            widthlbl.Text = GetString("WIDTH");
            heightlbl.Text = GetString("HEIGHT");
            Okbtn.Text = GetString("DONE");
        }

        private void manualwhcheck_CheckedChanged(object sender, EventArgs e)
        {

            if (manualwhcheck.Checked)
            {
                widthlbl.Enabled = true;
                widthtxt.Enabled = true;
                heightlbl.Enabled = true;
                heighttxt.Enabled = true;
            }
            else
            {
                widthlbl.Enabled = false;
                widthtxt.Enabled = false;
                widthtxt.Text = String.Empty;
                heightlbl.Enabled = false;
                heighttxt.Enabled = false;
                heighttxt.Text = String.Empty;
            }

        }
        private void resizestylecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resizestylecombo.SelectedItem.ToString().Equals(GetString("NORMAL")))
            {
                keepratiocheck.Checked = false;
                keepratiocheck.Enabled = false;
                manualwhcheck.Checked = false;
                manualwhcheck.Enabled = false;


            }
            else if (resizestylecombo.SelectedItem.ToString().Equals(GetString("STRETCH")))
            {
                keepratiocheck.Enabled = true;
                manualwhcheck.Checked = true;
                manualwhcheck.Enabled = true;
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
