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
    public partial class FormPrintPreviewLimit : Form
    {

        private uint nStartLabel;

        /// <summary>
        /// Startlabel inclusive
        /// </summary>
        /// <remarks>For end user purposes the first labels is numbered 1, and startlabel is inclusive, so 1 means take first label and show it first</remarks>
        public uint StartLabel
        {
            get { return nStartLabel; }
            set
            {
                nStartLabel = value;
                nStartLabel = nStartLabel < 1 ? 1 : nStartLabel;
                if (nStartLabel > nEndLabel)
                {
                    nEndLabel = nStartLabel;
                }
                if (nEndLabel - nStartLabel > nMaxRange)
                {
                    nEndLabel = nStartLabel + nMaxRange;
                }
                if (nudStart != null)
                {
                    nudStart.Value = nStartLabel;
                }
                if (nudEnd != null)
                {
                    nudEnd.Maximum = nStartLabel + nMaxRange;
                    if (nudEnd.Maximum > nMaxValue)
                    {
                        nudEnd.Maximum = nMaxValue;
                    }
                    nudEnd.Value = nudEnd.Maximum;
                }
            }
        }

        private uint nEndLabel;

        /// <summary>
        /// Endlabel, inclusive
        /// </summary>
        /// <remarks> 100 means the onehundred label, and starting with one
        /// </remarks>
        public uint EndLabel
        {
            get { return nEndLabel; }
            set
            {
                nEndLabel = value;
                if (nEndLabel < 1) nEndLabel = 1;

                if (nEndLabel < nStartLabel)
                {
                    nEndLabel = nStartLabel;
                }
                if (nEndLabel - nStartLabel > nMaxRange)
                {
                    nEndLabel = nStartLabel + nMaxRange;
                }
                if (nudEnd != null)
                {
                    if (nudEnd.Maximum < nEndLabel)
                    {
                        nudEnd.Maximum = nEndLabel;
                    }
                    nudEnd.Value = nEndLabel;
                }
            }
        }

        //The range that can be max between start and end.
        private uint nMaxRange;

        public uint MaxRange
        {
            get { return nMaxRange; }
            set { nMaxRange = value; }
        }

        private uint nMaxValue;

        public uint MaxValue
        {
            get { return nMaxValue; }
            set
            {
                nMaxValue = value;
                if (nEndLabel > nMaxValue)
                {
                    EndLabel = nMaxValue;
                }
            }
        }




        public FormPrintPreviewLimit()
        {
            nStartLabel = 1;  //Inclusive
            nEndLabel = 100;  //Inclusive
            nMaxRange = 100;

            InitializeComponent();
        }

        private void lblSeqstart_Click(object sender, EventArgs e)
        {

        }

        private void FormPrintPreviewLimit_Load(object sender, EventArgs e)
        {
            nudStart.Minimum = 1;
            nudStart.Maximum = nMaxValue;

            nudEnd.Minimum = nStartLabel;
            nudEnd.Maximum = nStartLabel + nMaxRange;
            if (nudEnd.Maximum > nMaxValue)
            {
                nudEnd.Maximum = nMaxValue;
            }
        }

        private void nudStart_ValueChanged(object sender, EventArgs e)
        {
            StartLabel = (uint)nudStart.Value;
        }

        private void nudEnd_ValueChanged(object sender, EventArgs e)
        {
            EndLabel = (uint)nudEnd.Value;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
