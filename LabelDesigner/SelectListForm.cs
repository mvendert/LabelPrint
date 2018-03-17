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

namespace LabelDesigner
{
    public partial class SelectListForm : FormBase
    {
        private List<string> list = new List<string>();
        public string result = string.Empty;
        
        //Startup
        public SelectListForm(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        //Main Functions
        private void okbtn_Click(object sender, EventArgs e)
        {
            if (itemslist.SelectedItem != null)
                result = itemslist.SelectedItem.ToString();
            else
                result = "";
            DialogResult = DialogResult.OK;
            this.Close();
        }
        
        //Internal Functions
        public void AddItem(string item)
        {
            list.Add(item);
            LoadList();   
        }
        private void LoadList()
        {
            itemslist.BeginUpdate();
            itemslist.Items.Clear();
            foreach (string item in list)
            {
                itemslist.Items.Add(item);
            }
            itemslist.EndUpdate();
        }
        public void ClearList()
        {
            list.Clear();
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
