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
using System.Text;
using System.Runtime.Remoting;
using System.Xml;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;

namespace ACA.LabelX.ClientEngine.Remote
{
    public class AcaLabelXClientRemoteEngine
    {
        private string ConfigFilePath = "";
        public void Start(string CfgFilePath)
        {
            GlobalDataStore.Logger.Debug("AcaLabelXClientRemoteEngine.Start");

            ConfigFilePath = CfgFilePath;
            RemotingConfiguration.Configure(ConfigFilePath,false);           
            string Protocol;
            string Address;
            string Port;
            string Uri;
            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetRemotingClientRemoteConfiguration(ConfigFilePath, out Protocol, out Address, out Port, out Uri);
                        
            switch (Protocol.ToLower())
            {
                case "http":
                    ChannelServices.RegisterChannel(new HttpClientChannel(), false);
                    break;
                case "tcp":
                    ChannelServices.RegisterChannel(new TcpClientChannel(), false);
                    break;
                default:
                    throw new ApplicationException(string.Format("Could not obtain the correct protocol from: {0}\r\nFound protocol: {1}\r\nShould be: http, or tcp", ConfigFilePath, Protocol));
            }
            //Address = "localhost";
            Address = "127.0.0.1";
            string ClientURL = string.Format("{0}://{1}:{2}/{3}", Protocol, Address, Port, Uri);

            ACA.LabelX.Client.RemClientControlObject objLabelXRemObject = (ACA.LabelX.Client.RemClientControlObject)Activator.GetObject(
                typeof(ACA.LabelX.Client.RemClientControlObject), ClientURL);

            if (objLabelXRemObject != null)
            {
                objLabelXRemObject.InitServer();
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
