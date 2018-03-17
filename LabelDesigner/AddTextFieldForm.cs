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
    public partial class AddTextFieldForm : FormBase
    {
        private IDictionary<string, LabelDef.FontX> fontList;
        private IDictionary<string, LabelDef.Field> referenceList;
        private LabelDef.TextField textfield;
        private string id;
        private string value;
        #region "Properties"
        public IDictionary<string, LabelDef.FontX> FontList
        {
            get { return fontList; }
            set { fontList = value; }
        }
        public IDictionary<string, LabelDef.Field> ReferenceList
        {
            get { return referenceList; }
            set { referenceList = value; }
        }
        public LabelDef.TextField Textfield
        {
            get { return textfield; }
            set { textfield = value; }
        }
        #endregion

        //Startup
        public AddTextFieldForm(IDictionary<string, LabelDef.FontX> mainfontList, IDictionary<string, LabelDef.Field> mainreferenceList, string id, string value, int X, int Y):this(mainfontList,mainreferenceList,id,value)
        {
            XPostxt.Text = X.ToString();
            YPostxt.Text = Y.ToString();
        }
        public AddTextFieldForm(IDictionary<string, LabelDef.FontX> mainfontList, IDictionary<string, LabelDef.Field> mainreferenceList, string id, string value)
        {
            InitializeComponent();
            fontList = mainfontList;
            referenceList = mainreferenceList;
            alignTextcombo.Items.AddRange(new String[] { GetString("LEFT"), GetString("RIGHT") });
            alignTextcombo.SelectedIndex = 0;
            rotationcombo.Items.AddRange(new String[] { "0", "90", "180", "270" });
            rotationcombo.SelectedIndex = 0;
            typecombo.Items.AddRange(new String[] { GetString("TEXT"), GetString("DATE"), GetString("DECIMALWHOLE"), GetString("DECIMALINTEGER"), GetString("DECIMALFRACTION") });
            typecombo.SelectedIndex = 0;

            foreach (LabelDef.FontX font in fontList.Values)
            {
                addFontXtoCombobox(font, Fontcombo);
            }
            if (Fontcombo.Items.Count == 0)
                Fontcombo.Enabled = false;
            else
            {
                Fontcombo.Enabled = true;
                Fontcombo.SelectedIndex = 0;
            }

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
        private void AddTextFieldForm_Load(object sender, EventArgs e)
        {
            IDtxt.Text = id;
            valuetxt.Text = value;
        }

        //Main functions
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textfield = new LabelDef.TextField(new ACA.LabelX.Tools.CoordinateSystem(200, 200, GetString("MILLIMETER")));
                textfield.ID = IDtxt.Text;
                textfield.PositionX = new Length(System.Convert.ToInt32(XPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                textfield.PositionY = new Length(System.Convert.ToInt32(YPostxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                textfield.Rotation = System.Convert.ToInt32(rotationcombo.SelectedItem.ToString());

                string selectedfont = Fontcombo.SelectedItem.ToString().Split(' ')[0].Trim();
                fontList.TryGetValue(selectedfont, out textfield.Font);

                if (manualwhcheck.Checked)
                {
                    textfield.Height = new Length(System.Convert.ToInt32(heighttxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                    textfield.Width = new Length(System.Convert.ToInt32(widthtxt.Text), System.Drawing.GraphicsUnit.Millimeter);
                }

                LabelDef.FieldFormat.Alignment alignment = LabelDef.FieldFormat.Alignment.Left;
                if (alignTextcombo.SelectedItem.ToString().Equals(GetString("LEFT"), StringComparison.OrdinalIgnoreCase))
                    alignment = LabelDef.FieldFormat.Alignment.Left;
                else if (alignTextcombo.SelectedItem.ToString().Equals(GetString("RIGHT"), StringComparison.OrdinalIgnoreCase))
                    alignment = LabelDef.FieldFormat.Alignment.Right;

                string formatString = null;
                if (formatstringlist.SelectedItem == null || formatstringlist.SelectedItem.ToString().Length < 1)
                    formatString = String.Empty;
                else
                    formatString = formatstringlist.SelectedItem.ToString();

                if (typecombo.SelectedItem.ToString().Equals(GetString("TEXT"), StringComparison.OrdinalIgnoreCase))
                {
                    LabelDef.StringFormat stringformat = new LabelDef.StringFormat();
                    stringformat.Align = alignment;
                    stringformat.FormatString = formatString;
                    stringformat.IsBarcode = false;
                    textfield.printFormat = new LabelDef.PrintFormat(stringformat);
                }
                else if (typecombo.SelectedItem.ToString().Equals(GetString("DATE"), StringComparison.OrdinalIgnoreCase))
                {
                    LabelDef.DateTimeFormat datetimeformat = new LabelDef.DateTimeFormat();
                    datetimeformat.Align = alignment;
                    datetimeformat.FormatString = formatString;
                    textfield.printFormat = new LabelDef.PrintFormat(datetimeformat);
                }
                else if (typecombo.SelectedItem.ToString().Equals(GetString("DECIMALWHOLE"), StringComparison.OrdinalIgnoreCase))
                {
                    LabelDef.DecimalFormat decimalformat = new LabelDef.DecimalFormat();
                    decimalformat.Align = alignment;
                    decimalformat.FormatString = formatString;
                    decimalformat.Portion = LabelDef.DecimalFormat.DecimalPortion.Entire;
                    textfield.printFormat = new LabelDef.PrintFormat(decimalformat);
                }
                else if (typecombo.SelectedItem.ToString().Equals(GetString("DECIMALINTEGER"), StringComparison.OrdinalIgnoreCase))
                {
                    LabelDef.DecimalFormat decimalformat = new LabelDef.DecimalFormat();
                    decimalformat.Align = alignment;
                    decimalformat.FormatString = formatString;
                    decimalformat.Portion = LabelDef.DecimalFormat.DecimalPortion.Integer;
                    textfield.printFormat = new LabelDef.PrintFormat(decimalformat);
                }
                else if (typecombo.SelectedItem.ToString().Equals(GetString("DECIMALFRACTION"), StringComparison.OrdinalIgnoreCase))
                {
                    LabelDef.DecimalFormat decimalformat = new LabelDef.DecimalFormat();
                    decimalformat.Align = alignment;
                    decimalformat.FormatString = formatString;
                    decimalformat.Portion = LabelDef.DecimalFormat.DecimalPortion.Fraction;
                    textfield.printFormat = new LabelDef.PrintFormat(decimalformat);
                }




                if (referencecombo.SelectedItem.ToString().Equals(GetString("NOTHING")))
                    textfield.ValueRef = null;
                else
                    textfield.ValueRef = referencecombo.SelectedItem.ToString();

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show(GetString("INVALIDINPUTERROR"), GetString("INVALIDINPUTERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void fontbtn_Click(object sender, EventArgs e)
        {
            InputBox inputbox = new InputBox(GetString("FONTNAMEQUESTION"));
            if (inputbox.ShowDialog() != DialogResult.Cancel)
            {
                if (inputbox.Answer.Equals("") || inputbox.Answer.Contains(' '))
                    MessageBox.Show(GetString("INVALIDFONTNAME"), GetString("INVALIDFONTNAME"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (fontList.ContainsKey(inputbox.Answer))
                {
                    MessageBox.Show(GetString("FONTNAMEEXISTSERROR"), GetString("FONTNAMEEXISTSERRORTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto fontfound;
                }
                else
                {
                    FontDialog fontdialog = new FontDialog();
                    if (fontdialog.ShowDialog() != DialogResult.Cancel)
                    {
                        {
                            LabelDef.FontX newfontx = new LabelDef.FontX();
                            newfontx.ID = inputbox.Answer;
                            newfontx.Font = fontdialog.Font;
                            newfontx.Style = fontdialog.Font.Style;

                            switch (MessageBox.Show(GetString("INVERTFONTQUESTION"), GetString("INVERTFONTQUESTIONTITLE"), MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    newfontx.InversePrint = true;
                                    break;
                                case DialogResult.No:
                                    newfontx.InversePrint = false;
                                    break;
                            }
                            fontList.Add(newfontx.ID, newfontx);
                            addFontXtoCombobox(newfontx, Fontcombo);
                            Fontcombo.SelectedIndex = Fontcombo.Items.Count - 1;
                        }
                    }
                }
            fontfound:
                this.Invalidate(); //Meaningless line of code to allow for the goto call
            }
        }
        private void addformatstringbtn_Click(object sender, EventArgs e)
        {
            InputBox inputbox = new InputBox(GetString("FORMATSTRINGNAMEQUESTION"));
            if (inputbox.ShowDialog() != DialogResult.Cancel)
            {
                if (inputbox.Answer.Equals(""))
                {
                    MessageBox.Show(GetString("INVALIDFORMATSTRING"), GetString("INVALIDFORMATSTRING"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    formatstringlist.Items.Add(inputbox.Answer);
                    formatstringlist.SelectedIndex = formatstringlist.Items.Count - 1;
                }
            }
        }

        //Internal Functions
        private void SetFormLanguage()
        {
            this.Text = GetString("ADDTEXTFIELD");
            idlbl.Text = GetString("ID");
            valuelbl.Text = GetString("VALUE");
            xposlbl.Text = GetString("XPOSITION");
            yposlbl.Text = GetString("YPOSITION");
            rotationlbl.Text = GetString("ROTATION");
            Fontlbl.Text = GetString("FONT");
            fontbtn.Text = GetString("ADDFONT");
            alignlbl.Text = GetString("ALIGNTEXT");
            alignTextcombo.Items.Clear();
            alignTextcombo.Items.AddRange(new String[] { GetString("LEFT"), GetString("RIGHT") });
            alignTextcombo.SelectedIndex = 0;
            referencelbl.Text = GetString("REFERENCE");
            typelbl.Text = GetString("TYPE");
            typecombo.Items.Clear();
            typecombo.Items.AddRange(new String[] { GetString("TEXT"), GetString("DATE"), GetString("DECIMALWHOLE"), GetString("DECIMALINTEGER"), GetString("DECIMALFRACTION") });
            typecombo.SelectedIndex = 0;
            formatStringlbl.Text = GetString("FORMAT");
            addformatstringbtn.Text = GetString("ADDFORMAT");
            manualwhcheck.Text = GetString("USEMANUALWIDTHANDHEIGHT");
            widthlbl.Text = GetString("WIDTH");
            heightlbl.Text = GetString("HEIGHT");
            Okbtn.Text = GetString("DONE");
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
        private void typecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typecombo.SelectedItem.ToString().Equals(GetString("TEXT")))
            {
                formatstringlist.Items.Clear();
                formatstringlist.Enabled = false;
                formatStringlbl.Enabled = false;
                addformatstringbtn.Enabled = false;
            }
            else if (typecombo.SelectedItem.ToString().Equals(GetString("DATE")))
            {
                formatstringlist.Items.Clear();
                formatstringlist.Enabled = true;
                formatStringlbl.Enabled = true;
                addformatstringbtn.Enabled = true;
                formatstringlist.Items.Add("");
                formatstringlist.Items.Add("dd/MM/yyyy");
                formatstringlist.Items.Add("ddd dd MMMM yyyy");
                formatstringlist.Items.Add("dd MM yyyy");
                formatstringlist.Items.Add("hh:mm:ss");

            }
            else if (typecombo.SelectedItem.ToString().Contains(GetString("DECIMAL")))
            {
                formatstringlist.Items.Clear();
                formatstringlist.Enabled = true;
                formatStringlbl.Enabled = true;
                addformatstringbtn.Enabled = true;
                formatstringlist.Items.Add("");
                formatstringlist.Items.Add("0.##");
            }
        }

        private void fontcombo_DropDown(object sender, System.EventArgs e)
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

        

        

    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
