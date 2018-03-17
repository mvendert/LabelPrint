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
using LuaInterface;

namespace LabelDesigner
{
    public partial class LuaEditor : LabelDesigner.FormBase
    {
        public string code;
        private ACA.LabelX.Label.LabelSet labelset;
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public ACA.LabelX.Label.LabelSet Labelset
        {
            get { return labelset; }
            set { labelset = value; }
        }

        public LuaEditor(string code, ACA.LabelX.Label.LabelSet labelset)
        {
            this.code = code;
            this.labelset = labelset;
            InitializeComponent();
            init();
        }

        private void init()
        {
            luaTxt.Text = code;

            LabelToLuaVars(labelset.CurrentLabel, "cl");
            LabelToLuaVars(labelset.DefaultLabel, "dl");
            LabelToLuaVars(labelset.BaseLabel,"bl");
        }
        private void LabelToLuaVars(ACA.LabelX.Label.Label label,string prefix)
        {
            foreach (KeyValuePair<string,ACA.LabelX.Label.Label.Value> pair in label.Values)
            {
                string var = VarNameToLua(pair.Key,prefix);
                variableList.Items.Add(var);
                dictionary.Add(var, pair.Value.Data);
            }
        }
        private string VarNameToLua(string varName, string prefix)
        {
            return prefix + "_" + varName.Replace('\\', '_').Replace(' ', '_').Replace('.', '_').Replace('-','_');
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            this.code = luaTxt.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void checkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Lua lua = new Lua();
                foreach (string item in variableList.Items)
                {
                    string value;
                    dictionary.TryGetValue(item, out value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        lua[item] = value;
                    }
                }
                lua.DoString(luaTxt.Text);
                MessageBox.Show("Ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void variableList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                int indexOfItem = variableList.IndexFromPoint(e.X, e.Y);
                if (indexOfItem >= 0 && indexOfItem < variableList.Items.Count)
                {
                    variableList.DoDragDrop(variableList.Items[indexOfItem], DragDropEffects.Copy);
                }
            }
        }
        private void luaTxt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void luaTxt_DragDrop(object sender, DragEventArgs e)
        {
            string t = e.Data.GetData(DataFormats.Text).ToString();

            int editorCursorLocation = luaTxt.GetCharIndexFromPosition(luaTxt.PointToClient(new Point(e.X, e.Y)));
            // Get start position to drop the text.
            string s = luaTxt.Text.Substring(editorCursorLocation);
            luaTxt.Text = luaTxt.Text.Substring(0, editorCursorLocation);

            // Drop the text on to the textBox.
            luaTxt.Text = luaTxt.Text + t;
            luaTxt.Text = luaTxt.Text + s;

        }

       
        
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
