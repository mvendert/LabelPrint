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
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace ACA.Support.Tools.ACAException
{
    public partial class ExceptionDialog : Form
    {
        private int teller;
        private const int intSpacing = 10;
        private MessageBoxIcon theMessageBoxIcon;
        private MessageBoxButtons theMessageBoxButtons;

        public ExceptionDialog()
        {
            teller = 0;
            InitializeComponent();
        }

        private void LaunchLink(string strUrl)
        {
            try
            {
                System.Diagnostics.Process.Start(strUrl);
            }
            catch (System.Security.SecurityException)
            {
                // Oeps. Launching url not allowed. DO nothing then...
            }
        }

        private void SizeBox(RichTextBox ctl)
        {
            Graphics g = null;
            try
            {
                SizeF objSizeF;
                g = Graphics.FromHwnd(ctl.Handle);
                objSizeF = g.MeasureString(ctl.Text, ctl.Font, new SizeF(ctl.Width, ctl.Height));
                g.Dispose();
                ctl.Height = Convert.ToInt32(objSizeF.Height) + 5;
            }
            catch (System.Security.SecurityException)
            {
                //Oeps no action
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }
            }
        }

        public static string ReplaceStringVals(string sin)
        {
            string strRet;

            if (sin == null)
            {
                return string.Empty;
            }
            strRet = sin.Replace("{app}", Application.ProductName);
            strRet = strRet.Replace("{contact}", "ACA Support");
            return strRet;
        }

        public string WhatHappened
        {
            set
            {
                txtError.Text = ReplaceStringVals(value);
            }
        }

        public string HowUserAffected
        {
            set
            {
                txtScope.Text = ReplaceStringVals(value);
            }
        }

        public string WhatUserCanDo
        {
            set
            {
                txtAction.Text = ReplaceStringVals(value);
            }
        }

        public string MoreDetails
        {
            set
            {
                txtMore.Text = ReplaceStringVals(value);
            }
        }

        public System.Windows.Forms.MessageBoxButtons Buttons
        {
            set
            {
                theMessageBoxButtons = value;
            }
        }

        public MessageBoxIcon DialogIcon
        {
            set
            {
                theMessageBoxIcon = value;
            }
        }

        private void ExceptionDialog_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
            this.txtMore.Anchor = AnchorStyles.None;
            this.txtMore.Visible = false;

            SizeBox(txtScope);
            SizeBox(txtAction);
            SizeBox(txtError);

            lblScopeHeading.Top = txtError.Top + txtError.Height + intSpacing;
            txtScope.Top = lblScopeHeading.Top + lblScopeHeading.Height + intSpacing;

            lblActionHeading.Top = txtScope.Top + txtScope.Height + intSpacing;
            txtAction.Top = lblActionHeading.Top + lblActionHeading.Height + intSpacing;

            lblMoreHeading.Top = txtAction.Top + txtAction.Height + intSpacing;
            butMoreInfo.Top = lblMoreHeading.Top - 3;

            this.Height = butMoreInfo.Top + butMoreInfo.Height + intSpacing + 45;
            this.CenterToScreen();

            switch (theMessageBoxIcon)
            {
                case MessageBoxIcon.Error:
                    pictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap();
                    break;
                case MessageBoxIcon.Exclamation:
                    pictureBox1.Image = System.Drawing.SystemIcons.Exclamation.ToBitmap();
                    break;
                //case MessageBoxIcon.Stop:
                //    pictureBox1.Image = System.Drawing.SystemIcons.Stop.ToBitmap();
                //    break;
                case MessageBoxIcon.Information:
                    pictureBox1.Image = System.Drawing.SystemIcons.Information.ToBitmap();
                    break;
                case MessageBoxIcon.Question:
                    pictureBox1.Image = System.Drawing.SystemIcons.Question.ToBitmap();
                    break;
                default:
                    pictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap();
                    break;
            }
            if (theMessageBoxButtons == MessageBoxButtons.OK)
            {
                butCancel.Enabled = false;
            }
            else
            {
                butCancel.Enabled = true;
            }
            toolTipDlg.IsBalloon = true;
            toolTipDlg.UseAnimation = true;
        }

        private void butMoreInfo_Click(object sender, EventArgs e)
        {
            if (butMoreInfo.Text == ">>")
            {
                this.Height += 300;
                butMoreInfo.Text = "<<";
                txtMore.Location = new System.Drawing.Point(lblMoreHeading.Left, lblMoreHeading.Top + lblMoreHeading.Height + intSpacing);
                txtMore.Height = this.ClientSize.Height - txtMore.Top - 45;
                txtMore.Width = this.ClientSize.Width - 2 * intSpacing;
                txtMore.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                txtMore.Visible = true;
                button1.Focus();
            }
            else
            {
                SuspendLayout();
                butMoreInfo.Text = ">>";
                this.Height = butMoreInfo.Top + butMoreInfo.Height + intSpacing + 45;
                txtMore.Visible = false;
                txtMore.Anchor = AnchorStyles.None;
                ResumeLayout();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            this.DialogResult = DialogResult.OK;
        }

        private void txtError_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            LaunchLink(e.LinkText);
        }

        private void txtScope_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            LaunchLink(e.LinkText);
        }

        private void txtAction_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            LaunchLink(e.LinkText);
        }

        private void txtMore_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            LaunchLink(e.LinkText);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            teller++;

            switch (teller)
            {
                case 1:
                    toolTipDlg.SetToolTip(this.pictureBox2, "Never seen George.exe?");
                    break;
                case 2:
                    toolTipDlg.SetToolTip(this.pictureBox2, "It's the same guy who lives in OpenStore.");
                    break;
                case 3:
                    toolTipDlg.SetToolTip(this.pictureBox2, "And he only shows when you realy don't want yo be disturbed.");
                    break;
                case 4:
                    toolTipDlg.SetToolTip(this.pictureBox2, "But now you have the chance to get rid of him.");
                    break;
                case 5:
                    toolTipDlg.SetToolTip(this.pictureBox2, "Call ACA and report this error.");
                    break;
                default:
                    toolTipDlg.SetToolTip(this.pictureBox2, "George");
                    teller = 0;
                    break;
            }
        }


    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
