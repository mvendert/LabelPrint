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

namespace LabelControler
{
    public partial class FormXmlPreview : FormBase
    {
        public string[] LabelData;

        public FormXmlPreview()
        {
            InitializeComponent();
            LabelData = null;
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormXmlPreview_Load(object sender, EventArgs e)
        {
            if (LabelData == null)
            {
                return;
            }

            StringBuilder theStrings;
            theStrings = new StringBuilder();

            int MaxLen;
            MaxLen = LabelData.GetLength(0);
            int MaxFound = 0;
            for (int i = 0; i < MaxLen; i++)
            {
                if (LabelData[i] != null)
                {
                    theStrings.Append(LabelData[i]);
                    theStrings.Append("\n");
                    MaxFound = i;
                }
            }
            if (MaxFound < 900)
                lblFirst1000.Visible = false;

            rtbPreview.Text = theStrings.ToString();
            SetFormTeksten();
        }
        private void SetFormTeksten()
        {
            label1.Text = GetString("LABELDATA");
            butClose.Text = GetString("CLOSE1");
            lblFirst1000.Text = GetString("ONLYFIRST1000LINES");
            this.Text = GetString("PREVIEWXMLLABELS");
        }
    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
