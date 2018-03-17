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
    public partial class BarcodeFieldInfoControl : GenericFieldInfoControl
    {
        Label.LabelDef.BarcodeField barcodeField;

        public BarcodeFieldInfoControl(IDictionary<string, LabelDef.Field> referenceList, Label.LabelSet labelset, Form mainForm)
            : base(referenceList, labelset, mainForm)
        {
            InitializeComponent();
            EnableComponents();
            manualwhcheck.Enabled = false;
            widthlbl.Enabled = true;
            widthtxt.Enabled = true;
            heightlbl.Enabled = true;
            heighttxt.Enabled = true;

            typecombo.Items.AddRange(new String[] { "EAN8", "EAN13", "UPCVersionA", "Code39", "Interleaved2Of5", "EAN128", "DataMatrix","PDF417" });
            typecombo.SelectedIndex = 0;
            alignTextcombo.Items.AddRange(new String[] { "Left", "Right" });
            alignTextcombo.SelectedIndex = 0;
            SetLanguage();
        }

        public override void SetData()
        {
            barcodeField = (Label.LabelDef.BarcodeField)base.Field;
            maxcharcounttxt.Text = barcodeField.MaxCharCount.ToString();
            typecombo.SelectedItem = barcodeField.Type.ToString();
            alignTextcombo.SelectedItem = barcodeField.printFormat.format.Align.ToString();
            showTextCheck.Checked = barcodeField.printText;

            //barcode always needs a manually input width and height
            manualwhcheck.Checked = true;
            widthtxt.Enabled = true;
            heighttxt.Enabled = true;
        }

        public override void SetLanguage()
        {
            base.SetLanguage();
            alignlbl.Text = GetString("ALIGNTEXT");
            typelbl.Text = GetString("TYPE");
            maxcharcountlbl.Text = GetString("CHARACTERLIMIT");
            showTextCheck.Text = GetString("SHOWTEXT");
            alignTextcombo.Items.Clear();
            alignTextcombo.Items.AddRange(new String[] { GetString("LEFT"), GetString("RIGHT") });
            alignTextcombo.SelectedIndex = 0;
        }

        private void alignTextcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (alignTextcombo.SelectedItem != null && barcodeField != null)
            {
                if (alignTextcombo.SelectedItem.ToString().Equals(GetString("LEFT"), StringComparison.OrdinalIgnoreCase))
                    barcodeField.printFormat.format.Align = LabelDef.FieldFormat.Alignment.Left;
                else if (alignTextcombo.SelectedItem.ToString().Equals(GetString("RIGHT"), StringComparison.OrdinalIgnoreCase))
                    barcodeField.printFormat.format.Align = LabelDef.FieldFormat.Alignment.Right;
                FillData(barcodeField);
                mainForm.Invalidate();

            }
        }

        private void typecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typecombo.SelectedItem != null && barcodeField != null)
            {
                string barcodetype = typecombo.SelectedItem.ToString();
                switch (barcodetype)
                {
                    case "EAN8":
                        barcodeField.Type = LabelDef.BarcodeField.BarcodeType.EAN8;
                        break;
                    case "EAN13":
                        barcodeField.Type = LabelDef.BarcodeField.BarcodeType.EAN13;
                        break;
                    case "UPCVersionA":
                        barcodeField.Type = LabelDef.BarcodeField.BarcodeType.UPCVersionA;
                        break;
                    case "Code39":
                        barcodeField.Type = LabelDef.BarcodeField.BarcodeType.Code39;
                        break;
                    case "Interleaved2Of5":
                        barcodeField.Type = LabelDef.BarcodeField.BarcodeType.Interleaved2Of5;
                        break;
                    case "EAN128":
                        barcodeField.Type = LabelDef.BarcodeField.BarcodeType.EAN128;
                        break;
                    case "DataMatrix":
                        barcodeField.Type = LabelDef.BarcodeField.BarcodeType.DataMatrix;
                        break;
                    case "PDF417":
                        barcodeField.Type = LabelDef.BarcodeField.BarcodeType.PDF417;
                        break;
                }
                FillData(barcodeField);
                mainForm.Invalidate();
            }
        }

        private void maxcharcounttxt_LostFocus(object sender, EventArgs e)
        {
            barcodeField.MaxCharCount = Convert.ToInt32(maxcharcounttxt.Text);
            FillData(barcodeField);
            mainForm.Invalidate();
        }

        private void showTextCheck_CheckedChanged(object sender, EventArgs e)
        {
            barcodeField.printText = showTextCheck.Checked;
            FillData(barcodeField);
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
