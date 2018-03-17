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
    public partial class FormConnectionParameters : FormBase
    {
        private string sComputer;

        public string Computer
        {
            get { return sComputer; }
            set { sComputer = value; }
        }
        private string sProtocol;

        public string Protocol
        {
            get { return sProtocol; }
            set { sProtocol = value; }
        }
        private int nPortNumber;

        public int PortNumber
        {
            get { return nPortNumber; }
            set { nPortNumber = value; }
        }
        public FormConnectionParameters()
        {
            InitializeComponent();
        }

        private void FormConnectionParameters_Load(object sender, EventArgs e)
        {
            txtComputer.Text = sComputer;
            txtPort.Text = nPortNumber.ToString();
            if (sProtocol.ToUpper().Equals(NC("HTTP")))
            {
                cmbProtocol.SelectedIndex = 0;
            }
            else
            {
                cmbProtocol.SelectedIndex = 1;
            }
            SetFormTeksten();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            sComputer = txtComputer.Text;
            nPortNumber = int.Parse(txtPort.Text);
            sProtocol = cmbProtocol.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void SetFormTeksten()
        {
            label1.Text = GetString("COMPUTER");
            label2.Text = GetString("PORT");
            label3.Text = GetString("PROTOCOL");
            butCancel.Text = GetString("CANCEL");
            this.Text = GetString("CONNECTIONPARAMETERS");
        }

    }
}

/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
