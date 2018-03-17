using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace ACALP_Config
{
    public partial class FrmMultipleIp : Form
    {
        List<IPAddress> theList;
        IPAddress selectedAddress;
        public FrmMultipleIp(List<IPAddress> theListT)
        {
            InitializeComponent();
            theList = theListT;
            selectedAddress = IPAddress.None;
        }

        private void frmMultipleIp_Load(object sender, EventArgs e)
        {
            cmbIP.Items.Clear();
            foreach (IPAddress ip in theList)
            {
                cmbIP.Items.Add(ip);
            }
            if (cmbIP.Items.Count > 0)
            {
                cmbIP.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( (cmbIP.Items.Count > 0) && (cmbIP.SelectedIndex >= 0))
            {
                selectedAddress = (IPAddress) cmbIP.SelectedItem;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public IPAddress SelectedIPAddress
        {
            get
            {
                return selectedAddress;
            }
        }   
    }
}
