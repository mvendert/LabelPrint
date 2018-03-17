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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACA.LabelX.Label;

namespace ACA.LabelX.Controls
{
    public partial class ConcatFieldInfoControl : GenericFieldInfoControl
    {
        private IDictionary<string, LabelDef.FontX> localfontList;
        private Label.LabelDef.TextFieldGroup textfieldgroup;
        private LabelDef.TextField selectedTextfield = null;
        
        public ConcatFieldInfoControl(IDictionary<string, LabelDef.Field> referenceList, Label.LabelSet labelset, IDictionary<string, LabelDef.FontX> fontList, Form mainForm)
            : base(referenceList, labelset, mainForm)
        {
            InitializeComponent();
            EnableComponents();
            this.localfontList = fontList;
            alignTextcombo.Items.AddRange(new String[] { "Left", "Right" });
            alignTextcombo.SelectedIndex = 0;
            concatmethodcombo.Items.AddRange(new String[] { "Horizontal", "Vertical" });
            concatmethodcombo.SelectedIndex = 0;
            SetLanguage();
        }

        public override void SetLanguage()
        {
            base.SetLanguage();
            aligntxtlbl.Text = GetString("ALIGNTEXT");
            concatmethodlbl.Text = GetString("CONCATMETHOD");
            linkedtextfieldslbl.Text = GetString("LINKEDFIELDS");
            fontlbl.Text = GetString("FONT");
            formatlbl.Text = GetString("FORMAT");

            alignTextcombo.Items.Clear();
            alignTextcombo.Items.AddRange(new String[] { GetString("LEFT"), GetString("RIGHT") });
            alignTextcombo.SelectedIndex = 0;
            concatmethodcombo.Items.Clear();
            concatmethodcombo.Items.AddRange(new String[] { GetString("HORIZONTAL"), GetString("VERTICAL") });
            concatmethodcombo.SelectedIndex = 0;

        }

        public override void SetFieldData()
        {
            xpostxt.Text = field.PositionX.length.ToString();           
            ypostxt.Text = field.PositionY.length.ToString();           
            rotationcombo.SelectedItem = field.Rotation.ToString();     
                                                                        
            if (field.Width == null && field.Height == null)        
            {
                manualwhcheck.Checked = false;
                widthtxt.Text = String.Empty;
                heighttxt.Text = String.Empty;
            }
            else
            {
                manualwhcheck.Checked = true;
                if (field.Width != null)
                    widthtxt.Text = field.Width.length.ToString();

                if (field.Height != null)
                    heighttxt.Text = field.Height.length.ToString();
            }

            string tempreference = null;
            if (selectedTextfield != null)
                tempreference = selectedTextfield.ValueRef;
            
            referencecombo.Items.Clear();
            referencecombo.Items.Add(GetString("NOTHING"));
            foreach (LabelDef.Field fieldToAdd in localreferenceList.Values)
            {
                if (!(fieldToAdd is LabelDef.TextFieldGroup))
                    referencecombo.Items.Add(fieldToAdd.ID);
            }

            if (tempreference != null)
                referencecombo.SelectedItem = tempreference;
            else
                referencecombo.SelectedIndex = 0;
        }

        public override void SetData()
        {
            textfieldgroup = (Label.LabelDef.TextFieldGroup)base.Field;
            alignTextcombo.SelectedItem = textfieldgroup.printFormat.format.Align.ToString();
            concatmethodcombo.SelectedItem = textfieldgroup.concatMethod.ToString();
            
            LabelDef.TextField previouslySelectedTextField = null;
            if (selectedTextfield != null)
                 previouslySelectedTextField = selectedTextfield;

            linkedtextfieldslist.Items.Clear();
            foreach (LabelDef.TextField temptextfield in textfieldgroup.fields)
            {
                linkedtextfieldslist.Items.Add(temptextfield.ID);
            }
            if (previouslySelectedTextField != null)
                selectedTextfield = previouslySelectedTextField;
            else
                selectedTextfield = textfieldgroup.fields.ElementAt(0);


            ACA.LabelX.Label.LabelDef.FontX tempfont = null;
            if (selectedTextfield != null)
                tempfont = selectedTextfield.Font;

            Fontcombo.Items.Clear();
            foreach (LabelDef.FontX fontToAdd in localfontList.Values)
            {
                addFontXtoCombobox(fontToAdd, Fontcombo);
            }
            if (tempfont != null)
                Fontcombo.SelectedItem = fontToString(tempfont);
            else
                Fontcombo.SelectedIndex = 0;


            if (selectedTextfield != null)
            {
                if (selectedTextfield.ValueRef == null)
                    referencecombo.SelectedIndex = 0;
                else
                    referencecombo.SelectedItem = selectedTextfield.ValueRef;
            }

            if (selectedTextfield != null && selectedTextfield.printFormat.format.FormatString != null){
                formattxt.Text = selectedTextfield.printFormat.format.FormatString;
            }
            else
                formattxt.Text = String.Empty;

        }

       

        private void alignTextcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (alignTextcombo.SelectedItem != null && textfieldgroup != null)
            {
                if (alignTextcombo.SelectedItem.ToString().Equals(GetString("LEFT"), StringComparison.OrdinalIgnoreCase))
                    textfieldgroup.printFormat.format.Align = LabelDef.FieldFormat.Alignment.Left;
                else if (alignTextcombo.SelectedItem.ToString().Equals(GetString("RIGHT"), StringComparison.OrdinalIgnoreCase))
                    textfieldgroup.printFormat.format.Align = LabelDef.FieldFormat.Alignment.Right;
                FillData(textfieldgroup);
                mainForm.Invalidate();
            }
        }

        private void Fontcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Fontcombo.SelectedItem != null && selectedTextfield != null)
            {
                if (!Fontcombo.SelectedItem.ToString().Equals(fontToString(selectedTextfield.Font), StringComparison.OrdinalIgnoreCase))
                {
                    string fontXID = Fontcombo.SelectedItem.ToString().Split(' ')[0];
                    Label.LabelDef.FontX tempFontX;
                    localfontList.TryGetValue(fontXID, out tempFontX);
                    selectedTextfield.Font = tempFontX;
                    FillData(textfieldgroup);
                    mainForm.Invalidate();
                }
            }
        }

        private void Fontcombo_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void concatmethodcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (concatmethodcombo.SelectedItem != null && textfieldgroup != null)
            {
                if (concatmethodcombo.SelectedItem.ToString().Equals(GetString("HORIZONTAL"), StringComparison.OrdinalIgnoreCase))
                    textfieldgroup.concatMethod = LabelDef.TextFieldGroup.ConcatMethod.Horizontal;
                else if (concatmethodcombo.SelectedItem.ToString().Equals(GetString("VERTICAL"), StringComparison.OrdinalIgnoreCase))
                    textfieldgroup.concatMethod = LabelDef.TextFieldGroup.ConcatMethod.Vertical;
                FillData(textfieldgroup);
                mainForm.Invalidate();
            }
        }

        public override void referencecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (referencecombo.SelectedItem != null && field != null)
            {
                if (selectedTextfield != null)
                {
                    if (referencecombo.SelectedItem.ToString().Equals(GetString("NOTHING")) || referencecombo.SelectedItem.ToString().Equals(field.ID))
                        selectedTextfield.ValueRef = null;
                    else
                        selectedTextfield.ValueRef = referencecombo.SelectedItem.ToString();
                }
                IsCallBack = true;
                FillData(field);
                mainForm.Invalidate();
            }
        }

        private void linkedtextfieldslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (linkedtextfieldslist.SelectedItems.Count == 1)
            {
                foreach (LabelDef.TextField searchField in textfieldgroup.fields) //TODO : Make more efficient
                {
                    if (searchField.ID.Equals(linkedtextfieldslist.SelectedItem.ToString()))
                        selectedTextfield = searchField;
                }
                
                ShowTextFieldProperties();
                FillData(textfieldgroup);
                mainForm.Invalidate();
            }
        }

        public void ShowTextFieldProperties()
        {
            linkedtextfieldslbl.Visible = false;
            linkedtextfieldslist.Visible = false;
            fontlbl.Visible = true;
            Fontcombo.Visible = true;
            referencelbl.Visible = true;
            referencecombo.Visible = true;
            formatlbl.Visible = true;
            formattxt.Visible = true;
            backbtn.Visible = true;
        }

        public void HideTextFieldProperties()
        {
            linkedtextfieldslbl.Visible = true;
            linkedtextfieldslist.Visible = true;
            fontlbl.Visible = false;
            Fontcombo.Visible = false;
            referencelbl.Visible = false;
            referencecombo.Visible = false;
            formatlbl.Visible = false;
            formattxt.Visible = false;
            backbtn.Visible = false;
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            HideTextFieldProperties();
        }

        private void formattxt_LostFocus(object sender, EventArgs e)
        {
            if (formattxt.Text.Length > 0)
                selectedTextfield.printFormat.format.FormatString = formattxt.Text;
            else
                selectedTextfield.printFormat.format.FormatString = String.Empty;
            FillData(textfieldgroup);
            mainForm.Invalidate();
        }

        private void addFontXtoCombobox(LabelDef.FontX font, ComboBox combo)
        {
            combo.Items.Add(fontToString(font));
        }

        private string fontToString(LabelDef.FontX font)
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
            return text;
        }
    }

}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
