using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabelDesigner
{
    public partial class FormCustomZoomFactor : LabelDesigner.FormBase
    {
        private float zoomFactor = 1.0f;

        private int percent = 100;

        public float ZoomFactor
        {
            get 
            {
                zoomFactor = percent / 100.0f;
                return zoomFactor; 
            }
            set 
            {
                percent = (int) (zoomFactor * 100);
                zoomFactor = value; 
            }
        }

        public FormCustomZoomFactor()
        {
            InitializeComponent();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormCustomZoomFactor_Load(object sender, EventArgs e)
        {
            txtZoom.Text = percent.ToString();
            trackBar1.Value = percent;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {            
            percent = trackBar1.Value;
            txtZoom.Text = percent.ToString();
            txtZoom.Update();
        }

        private void txtZoom_Leave(object sender, EventArgs e)
        {
            int value = 50;
            try
            {
                value = int.Parse(txtZoom.Text);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                txtZoom.Focus();
                return;
            }
            if (value < 50)
            {
                value = 50;
            }
            if (value > 1000)
            {
                value = 1000;
            }
            if (value > trackBar1.Maximum)
            {
                trackBar1.Enabled = false;
                trackBar1.Value = 250;
            }
            else
            {
                trackBar1.Enabled = true;
            }
            percent = value;
        }

    }
}
