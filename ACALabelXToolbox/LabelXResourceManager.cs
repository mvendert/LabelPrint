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
using System.Linq;
using System.Text;

namespace ACA.LabelX
{
    public class LabelXResourceManager
    {
        System.Collections.ArrayList rcList;

        public LabelXResourceManager()
		{
			rcList = new System.Collections.ArrayList();
		}

		public bool RegisterResource(System.Resources.ResourceManager theManager)
		{
			bool bRet = true;
			theManager.IgnoreCase = true;
			rcList.Add(theManager);
			return bRet;
		}

		public string GetString(string sName)
		{
			return InternalGetString(sName);
		}

		public string InternalGetString(string sName)
		{
			string sRet;
			string sRet2;
			int nMax;

			sRet = "~" + sName + "~";

			nMax = rcList.Count;

			System.Collections.IEnumerator myEnum = rcList.GetEnumerator();
			while (nMax-1 >= 0)
			{
				try
				{
					sRet2 = ((System.Resources.ResourceManager)rcList[nMax-1]).GetString(sName);
					if (sRet2 != null)
					{
						sRet  = sRet2;
						break;			// Een string gevonden in 1 van de resources.
					}
				}
				catch (System.Resources.MissingManifestResourceException)
				{
                    System.Diagnostics.Debug.WriteLine(sRet + " not found.");
				}
				finally
				{
					nMax--;
				}
			}
			return sRet;
		}
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
