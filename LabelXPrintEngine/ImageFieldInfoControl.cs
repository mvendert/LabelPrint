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
    public partial class ImageFieldInfoControl : GenericFieldInfoControl
    {
        Label.LabelDef.ImageField imageField;
        public ImageFieldInfoControl(IDictionary<string, LabelDef.Field> referenceList, Label.LabelSet labelset, Form mainForm)
        :base(referenceList, labelset,mainForm)
        {
            InitializeComponent();
            resizestylecombo.Items.AddRange(new String[] { "Normal", "Stretch" });
            resizestylecombo.SelectedIndex = 0;
            greyscalecheck.Checked = false;
            SetLanguage();
            EnableComponents();
        }

        public override void SetLanguage()
        {
            base.SetLanguage();
            rezisestylelbl.Text = GetString("RESIZESTYLE");
            keepratiocheck.Text = GetString("KEEPRATIO");
            greyscalecheck.Text = GetString("GREYSCALE");

            resizestylecombo.Items.Clear();
            resizestylecombo.Items.AddRange(new String[] { GetString("NORMAL"), GetString("STRETCH") });
            resizestylecombo.SelectedIndex = 0;

        }
        public override void SetData()
        {
            imageField = (Label.LabelDef.ImageField)base.Field;
            resizestylecombo.SelectedItem = imageField.Scale.ToString();
            keepratiocheck.Checked = imageField.KeepRatio;

            if (imageField.Scale.ToString().Equals(GetString("NORMAL"), StringComparison.OrdinalIgnoreCase))
            {
                keepratiocheck.Checked = false;
                keepratiocheck.Enabled = false;
                manualwhcheck.Checked = false;
                manualwhcheck.Enabled = false;
                imageField.Width = null;
                imageField.Height = null;
            }
            else if (imageField.Scale.ToString().Equals(GetString("STRETCH"), StringComparison.OrdinalIgnoreCase))
            {
                keepratiocheck.Enabled = true;
                manualwhcheck.Checked = true;
                manualwhcheck.Enabled = false;
                if (imageField.Width == null)
                    try
                    {
                        imageField.Width = new ACA.LabelX.Tools.Length(Convert.ToInt32(widthtxt.Text), new ACA.LabelX.Tools.CoordinateSystem.Units(GraphicsUnit.Millimeter));
                    }
                    catch 
                    { }
                if (imageField.Height == null)
                    try
                    {
                        imageField.Height = new ACA.LabelX.Tools.Length(Convert.ToInt32(heighttxt.Text), new ACA.LabelX.Tools.CoordinateSystem.Units(GraphicsUnit.Millimeter));
                    }
                    catch 
                    { }
            }


            if (manualwhcheck.Checked)
            {
                widthtxt.Enabled = true;
                heighttxt.Enabled = true;
            }
            else 
            {
                widthtxt.Enabled = false;
                heighttxt.Enabled = false;
            }

            if (imageField.Color == Label.LabelDef.ImageField.ColorStyle.Grayscale)
                greyscalecheck.Checked = true;
            else
                greyscalecheck.Checked = false;

        }

        private void resizestylecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resizestylecombo.SelectedItem != null && imageField != null)
            {
                if (resizestylecombo.SelectedItem.ToString().Equals(GetString("NORMAL"), StringComparison.OrdinalIgnoreCase))
                {
                    imageField.Scale = LabelDef.ImageField.ScaleStyle.Normal;
                    manualwhcheck.Checked = false;
                    manualwhcheck.Enabled = false;
                }

                else if (resizestylecombo.SelectedItem.ToString().Equals(GetString("STRETCH"), StringComparison.OrdinalIgnoreCase))
                {
                    imageField.Scale = LabelDef.ImageField.ScaleStyle.Stretch;
                    manualwhcheck.Checked = true;
                    manualwhcheck.Enabled = true;
                }


                FillData(imageField);
                mainForm.Invalidate();
            }
        }

        private void keepratiocheck_CheckedChanged(object sender, EventArgs e)
        {
            imageField.KeepRatio = keepratiocheck.Checked;
            FillData(imageField);
            mainForm.Invalidate();
        }

        private void greyscalecheck_CheckedChanged(object sender, EventArgs e)
        {
            if (greyscalecheck.Checked)
                imageField.Color = LabelDef.ImageField.ColorStyle.Grayscale;
            else
                imageField.Color = LabelDef.ImageField.ColorStyle.Color;
            FillData(imageField);
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
