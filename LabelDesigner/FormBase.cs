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
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Reflection;
using ACA.LabelX;

namespace LabelDesigner
{
    public partial class FormBase : Form
    {
        protected LabelXResourceManager rcManager;

        public FormBase()
        {
            int nLanguageId;
            nLanguageId = GlobalDataStore.LanguageId;
            try
            {
                CultureInfo cultureInfo = new CultureInfo(nLanguageId, true);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            catch
            {
                //Nope;
            }
            InitializeComponent();

            rcManager = new LabelXResourceManager();
            ResourceManager myRes = new ResourceManager("ACALabelXToolbox.strings", Assembly.GetAssembly(typeof(ACA.LabelX.Toolbox.Toolbox)));
            rcManager.RegisterResource(myRes);
            ResourceManager myRes2 = new ResourceManager("LabelDesigner.strings", Assembly.GetExecutingAssembly());
            rcManager.RegisterResource(myRes2);

        }
        public ACA.LabelX.LabelXResourceManager ACARM
        {
            get
            {
                return rcManager;
            }
        }
        public string GetString(string sName)
        {
            return rcManager.GetString(sName);
        }
        public string NC(string sName)
        {
            return sName;
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
