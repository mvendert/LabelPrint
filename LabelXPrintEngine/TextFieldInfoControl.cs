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
    public partial class TextFieldInfoControl : GenericFieldInfoControl
    {
        private IDictionary<string, LabelDef.FontX> localfontList;
        private Label.LabelDef.TextField textField;

        public TextFieldInfoControl(IDictionary<string, LabelDef.Field> referenceList, Label.LabelSet labelset, IDictionary<string, LabelDef.FontX> fontList, Form mainForm)
            : base(referenceList, labelset, mainForm)
        {
            InitializeComponent();
            EnableComponents();
            this.localfontList = fontList;
            alignTextcombo.Items.AddRange(new String[] { "Left", "Right" });
            alignTextcombo.SelectedIndex = 0;
            typecombo.Items.AddRange(new String[] { "Text", "Date", "Decimal:Whole", "Decimal:Integer", "Decimal:Fraction" });
            typecombo.SelectedIndex = 0;
            SetLanguage();
        }

        public override void SetLanguage()
        {
            base.SetLanguage();
            alignlbl.Text = GetString("ALIGNTEXT");
            typelbl.Text = GetString("TYPE");
            Fontlbl.Text = GetString("FONT");
            formatlbl.Text = GetString("FORMAT");
            alignTextcombo.Items.Clear();
            alignTextcombo.Items.AddRange(new String[] { GetString("LEFT"), GetString("RIGHT") });
            alignTextcombo.SelectedIndex = 0;
            typecombo.Items.Clear();
            typecombo.Items.AddRange(new String[] { GetString("TEXT"), GetString("DATE"), GetString("DECIMALWHOLE"), GetString("DECIMALINTEGER"), GetString("DECIMALFRACTION") });
            typecombo.SelectedIndex = 0;	
        }
    
        public override void SetData()
        {
            textField = (Label.LabelDef.TextField)base.Field;
            alignTextcombo.SelectedItem = textField.printFormat.format.Align.ToString();

            ACA.LabelX.Label.LabelDef.FontX tempfont = textField.Font;
            Fontcombo.Items.Clear();
            foreach (LabelDef.FontX fontToAdd in localfontList.Values)
            {
                addFontXtoCombobox(fontToAdd, Fontcombo);
            }

            Fontcombo.SelectedItem = fontToString(textField.Font);

            
            if (textField.printFormat.format is LabelDef.DateTimeFormat)
            {
                typecombo.SelectedItem = "Date";
                formattxt.Text = textField.printFormat.format.FormatString;
                formattxt.Enabled = true;
            }
            else if (textField.printFormat.format is LabelDef.DecimalFormat)
            {
                LabelDef.DecimalFormat tempformat = (LabelDef.DecimalFormat) textField.printFormat.format;
                if (tempformat.Portion == LabelDef.DecimalFormat.DecimalPortion.Entire)
                    typecombo.SelectedItem = "Decimal:Whole";
                else if (tempformat.Portion == LabelDef.DecimalFormat.DecimalPortion.Fraction)
                    typecombo.SelectedItem = "Decimal:Fraction";
                else if (tempformat.Portion == LabelDef.DecimalFormat.DecimalPortion.Integer)
                    typecombo.SelectedItem = "Decimal:Integer";
                formattxt.Text = textField.printFormat.format.FormatString;
                formattxt.Enabled = true;
            }
            else if (textField.printFormat.format is LabelDef.StringFormat)
            {
                typecombo.SelectedItem = "Text";
                formattxt.Text = string.Empty;
                formattxt.Enabled = false;
            }
        }

        private void typecombo_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            
            if (typecombo.SelectedItem.ToString().Equals("Text",StringComparison.OrdinalIgnoreCase))
            {
                LabelDef.StringFormat newformat = new LabelDef.StringFormat();
                newformat.FormatString = string.Empty;
                textField.printFormat.format = newformat;
            }
            else if (typecombo.SelectedItem.ToString().Equals("Date", StringComparison.OrdinalIgnoreCase))
            {
                LabelDef.DateTimeFormat newformat = new LabelDef.DateTimeFormat();
                newformat.FormatString = textField.printFormat.format.FormatString;
                textField.printFormat.format = newformat;
            }
            else if (typecombo.SelectedItem.ToString().Equals("Decimal:Whole", StringComparison.OrdinalIgnoreCase))
            {
                LabelDef.DecimalFormat newformat = new LabelDef.DecimalFormat();
                newformat.Portion = LabelDef.DecimalFormat.DecimalPortion.Entire;
                newformat.FormatString = textField.printFormat.format.FormatString;
                textField.printFormat.format = newformat;
            }
            else if (typecombo.SelectedItem.ToString().Equals("Decimal:Integer", StringComparison.OrdinalIgnoreCase))
            {
                LabelDef.DecimalFormat newformat = new LabelDef.DecimalFormat();
                newformat.Portion = LabelDef.DecimalFormat.DecimalPortion.Integer;
                newformat.FormatString = textField.printFormat.format.FormatString;
                textField.printFormat.format = newformat;
            }
            else if (typecombo.SelectedItem.ToString().Equals("Decimal:Fraction", StringComparison.OrdinalIgnoreCase))
            {
                LabelDef.DecimalFormat newformat = new LabelDef.DecimalFormat();
                newformat.Portion = LabelDef.DecimalFormat.DecimalPortion.Fraction;
                newformat.FormatString = textField.printFormat.format.FormatString;
                textField.printFormat.format = newformat;
            }
            
            FillData(textField);
            mainForm.Invalidate();
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

        private void alignTextcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (alignTextcombo.SelectedItem != null && textField != null)
            {
                if (alignTextcombo.SelectedItem.ToString().Equals(GetString("LEFT"), StringComparison.OrdinalIgnoreCase))
                    textField.printFormat.format.Align = LabelDef.FieldFormat.Alignment.Left;
                else if (alignTextcombo.SelectedItem.ToString().Equals(GetString("RIGHT"), StringComparison.OrdinalIgnoreCase))
                    textField.printFormat.format.Align = LabelDef.FieldFormat.Alignment.Right;
                FillData(textField);
                mainForm.Invalidate();
            }
        }

        private void Fontcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Fontcombo.SelectedItem != null && textField != null)
            {
                if (!Fontcombo.SelectedItem.ToString().Equals(fontToString(textField.Font), StringComparison.OrdinalIgnoreCase))
                {
                    string fontXID = Fontcombo.SelectedItem.ToString().Split(' ')[0];
                    Label.LabelDef.FontX tempFontX;
                    localfontList.TryGetValue(fontXID, out tempFontX);
                    textField.Font = tempFontX;
                    FillData(textField);
                    mainForm.Invalidate();
                }
            }
        }

        private void formattxt_LostFocus(object sender, EventArgs e)
        {
            if (formattxt.Text.Length > 0)
                textField.printFormat.format.FormatString = formattxt.Text;
            else
                textField.printFormat.format.FormatString = String.Empty;
            FillData(textField);
            mainForm.Invalidate();
        }

    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
