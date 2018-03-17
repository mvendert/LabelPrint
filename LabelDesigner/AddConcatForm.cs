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
    public partial class AddConcatForm : FormBase
    {
        private IDictionary<string, LabelDef.FontX> fontlist;
        private IDictionary<string, LabelDef.Field> referencelist;
        private IDictionary<string, LabelDef.TextField> textfieldlist = new Dictionary<string, LabelDef.TextField>();
        private LabelDef.TextFieldGroup textfieldgroup;
        private Form mainForm;

        #region "Properties"
        public IDictionary<string, LabelDef.FontX> Fontlist
        {
            get { return fontlist; }
            set { fontlist = value; }
        }
        public LabelDef.TextFieldGroup Textfieldgroup
        {
            get { return textfieldgroup; }
            set { textfieldgroup = value; }
        }
        public IDictionary<string, LabelDef.TextField> Textfieldlist
        {
            get { return textfieldlist; }
            set { textfieldlist = value; }
        }
        #endregion

        //Startup
        public AddConcatForm(IDictionary<string, LabelDef.FontX> mainfontList, IDictionary<string, LabelDef.Field> mainreferenceList, Form mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            fontlist = mainfontList;
            referencelist = mainreferenceList;
            alignTextcombo.Items.AddRange(new String[] { "Left", "Right" });
            alignTextcombo.SelectedIndex = 0;
            rotationcombo.Items.AddRange(new String[] { "0", "90", "180", "270" });
            rotationcombo.SelectedIndex = 0;
            concatmethodcombo.Items.AddRange(new String[] { "Horizontal", "Vertical" });
            concatmethodcombo.SelectedIndex = 0;
            GetTextFields(referencelist); //Fill up textfieldlist and availabletextfieldlistbox from entire referencelist
            SetFormLanguage();
        }
        private void GetTextFields(IDictionary<string, LabelDef.Field> localReferenceList)
        {
            foreach (KeyValuePair<string, LabelDef.Field> pair in localReferenceList)
            {
                if (pair.Value is LabelDef.TextField && !(pair.Value is LabelDef.TextFieldGroup))
                {
                    LabelDef.TextField temptextfield = (LabelDef.TextField)pair.Value;
                    textfieldlist.Add(temptextfield.ID, temptextfield);
                    availabletextfieldslist.Items.Add(temptextfield.ID);
                }
            }
            if (availabletextfieldslist.Items.Count < 2)
            {
                MessageBox.Show("You need atleast two available textfields to be able to link them.", "Not enough available textfields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                this.Close();
            }

        }

        //Main functions
        private void Okbtn_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                if (concatmethodcombo.SelectedItem.ToString().Equals("Horizontal", StringComparison.OrdinalIgnoreCase))
                    textfieldgroup = new LabelDef.TextFieldGroup(new CoordinateSystem(Convert.ToUInt32(mainForm.CreateGraphics().DpiX), Convert.ToUInt32(mainForm.CreateGraphics().DpiY), "Millimeter"), LabelDef.TextFieldGroup.ConcatMethod.Horizontal);
                else
                    textfieldgroup = new LabelDef.TextFieldGroup(new CoordinateSystem(Convert.ToUInt32(mainForm.CreateGraphics().DpiX), Convert.ToUInt32(mainForm.CreateGraphics().DpiY), "Millimeter"), LabelDef.TextFieldGroup.ConcatMethod.Vertical);

                textfieldgroup.ID = IDtxt.Text;
                textfieldgroup.PositionX = new Length(System.Convert.ToInt32(XPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                textfieldgroup.PositionY = new Length(System.Convert.ToInt32(YPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                textfieldgroup.Rotation = System.Convert.ToInt32(rotationcombo.SelectedItem.ToString());

                if (manualwhcheck.Checked)
                {
                    textfieldgroup.Height = new Length(System.Convert.ToInt32(heighttxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                    textfieldgroup.Width = new Length(System.Convert.ToInt32(widthtxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                }

                LabelDef.StringFormat stringformat = new LabelDef.StringFormat();

                if (alignTextcombo.SelectedItem.ToString().Equals(GetString("LEFT"), StringComparison.OrdinalIgnoreCase))
                    stringformat.Align = LabelDef.FieldFormat.Alignment.Left;
                else
                    stringformat.Align = LabelDef.FieldFormat.Alignment.Right;

                textfieldgroup.printFormat = new LabelDef.PrintFormat(stringformat);

                IList<LabelDef.TextField> addedtextfieldlist = new List<LabelDef.TextField>();

                foreach (string textfieldIDToAdd in addedtextfieldslist.Items)
                {
                    LabelDef.Field fieldvalue;
                    referencelist.TryGetValue(textfieldIDToAdd, out fieldvalue);
                    LabelDef.TextField textfieldvalue = (LabelDef.TextField)fieldvalue;
                    textfieldgroup.fields.Add(textfieldvalue);
                }

                DialogResult = DialogResult.OK;
                this.Close();


            }
            else
            {
                MessageBox.Show(GetString("INVALIDINPUTERROR"), GetString("INVALIDINPUTERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool inputCheck()
        {
            bool result = true;
            if (string.IsNullOrEmpty(IDtxt.Text))
                result = false;

            try
            {
                Length testxpos = new Length(System.Convert.ToInt32(XPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                Length testypos = new Length(System.Convert.ToInt32(YPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                if (manualwhcheck.Checked)
                {
                    Length testheight = new Length(System.Convert.ToInt32(heighttxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                    Length testwidth = new Length(System.Convert.ToInt32(widthtxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                }
            }
            catch
            {
                result = false;
            }

            if (addedtextfieldslist.Items.Count < 2)
                result = false;

            return result;
        }
        
        //Internal Functions
        private void addFontXtoCombobox(LabelDef.FontX font, ComboBox combo)
        {
            string text = font.ID + " (" + font.Font.Name + ", " + font.Font.SizeInPoints + "pt";
            if (font.Font.Bold)
                text += ", " + GetString("BOLD");
            if (font.Font.Italic)
                text += ", " + GetString("ITALIC");
            if (font.Font.Underline)
                text += ", " + GetString("UNDERLINE");
            if (font.Font.Strikeout)
                text += ", " + GetString("STRIKEOUT");
            if (font.InversePrint)
                text += ", " + GetString("INVERSED");

            text += ")";
            combo.Items.Add(text);

        }

        private void SetFormLanguage()
        {
            this.Text = GetString("ADDCONCAT");
            idlbl.Text = GetString("ID");
            xposlbl.Text = GetString("XPOSITION");
            yposlbl.Text = GetString("YPOSITION");
            rotationlbl.Text = GetString("ROTATION");
            alignlbl.Text = GetString("ALIGNTEXT");
            manualwhcheck.Text = GetString("USEMANUALWIDTHANDHEIGHT");
            concatmethodlbl.Text = GetString("CONCATMETHOD");
            widthlbl.Text = GetString("WIDTH");
            heightlbl.Text = GetString("HEIGHT");
            alignTextcombo.Items.Clear();
            alignTextcombo.Items.AddRange(new String[] { GetString("LEFT"), GetString("RIGHT") });
            alignTextcombo.SelectedIndex = 0;
            concatmethodcombo.Items.Clear();
            concatmethodcombo.Items.AddRange(new String[] { GetString("HORIZONTAL"), GetString("VERTICAL") });
            concatmethodcombo.SelectedIndex = 0;
            Okbtn.Text = GetString("DONE");
            addeditemslbl.Text = GetString("ADDEDTEXTFIELDS");
            availabletextfieldslbl.Text = GetString("AVAILABLETEXTFIELDS");
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
        
        private void addtextfieldbtn_Click(object sender, EventArgs e)
        {
            if (availabletextfieldslist.SelectedItem != null)
            {
                addedtextfieldslist.Items.Add(availabletextfieldslist.SelectedItem.ToString());
                availabletextfieldslist.Items.Remove(availabletextfieldslist.SelectedItem);
            }
            if (addedtextfieldslist.Items.Count >= 2)
            {
                Okbtn.Enabled = true;
            }
        }
        private void removetextfieldbtn_Click(object sender, EventArgs e)
        {
            if (addedtextfieldslist.SelectedItem != null)
            {
                availabletextfieldslist.Items.Add(addedtextfieldslist.SelectedItem.ToString());
                addedtextfieldslist.Items.Remove(addedtextfieldslist.SelectedItem);
            }
            if (addedtextfieldslist.Items.Count < 2)
            {
                Okbtn.Enabled = false;
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
