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
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Collections;

namespace ACA.LabelX.TestServer
{
    public class ACALabelXController
    {
        private string ConfigFilePath = "";
        private string PrintJobsRootFolder = null;
        private string LabelDefinitionsRootFolder = null;
        private string PaperDefinitionsRootFolder = null;
        private string SettingsRootFolder = null;
        private string PictureRootFolder = null;
        private string UpdateRootFolder = null;

        public void Start(string ConfigFilePath)
        {
            const bool methode1 = true;
            Hashtable Props;
            //IChannel ServiceChannel;
            string Protocol;
            string Address;
            string Port;
            string Uri;
            TcpServerChannel theTcp;

            if (methode1)
            {
                this.ConfigFilePath = ConfigFilePath;

                RemotingConfiguration.Configure(ConfigFilePath, false);
                
                Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
                toolbox.GetRemotingServerConfiguraton(ConfigFilePath, out Protocol, out Address, out Port, out Uri);

                string sipnr = GlobalDataStore.GetAppSetting("localhost");
                Address = sipnr;
                string ClientURL = string.Format("{0}://{1}:{2}/{3}", Protocol, Address, Port, Uri);
                ACA.LabelX.RemoteObject objLabelXRemoteObject = (ACA.LabelX.RemoteObject)Activator.GetObject(
                typeof(ACA.LabelX.RemoteObject), ClientURL);
                if (objLabelXRemoteObject != null)
                {
                    toolbox.GetGeneralServerConfiguraton(ConfigFilePath, out PrintJobsRootFolder, out LabelDefinitionsRootFolder, out PaperDefinitionsRootFolder, out SettingsRootFolder, out PictureRootFolder, out UpdateRootFolder);
                    objLabelXRemoteObject.InitServer(PrintJobsRootFolder, LabelDefinitionsRootFolder, PaperDefinitionsRootFolder, SettingsRootFolder, UpdateRootFolder);
                }
            }
            else
            {
                this.ConfigFilePath = ConfigFilePath;

                //RemotingConfiguration.Configure(ConfigFilePath, false);
                Props = new Hashtable();
                
                Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
                toolbox.GetRemotingServerConfiguraton(ConfigFilePath, out Protocol, out Address, out Port, out Uri);

                HttpChannel theHttp2;
                Hashtable myTable = new Hashtable();
                SoapServerFormatterSinkProvider theProvider = new SoapServerFormatterSinkProvider();
                //BinaryServerFormatterSinkProvider theProvider = new BinaryServerFormatterSinkProvider();
                //theProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                ClientIPInjectorSinkProvider injectorProvider;
                injectorProvider = new ClientIPInjectorSinkProvider();

                //BinaryClientFormatterSinkProvider theClientProv = new BinaryClientFormatterSinkProvider();
                //Props["port"] = "8080";
                Props["port"] = "18080";
                Props["name"] = "ACALabelXRemoteObject";
                //Props["address"] = "localhost";
                Props["address"] = "127.0.0.1";

                theProvider.Next = injectorProvider;
                //theProvider.Next = new ClientIPInjectorSinkProvider();

                switch (Protocol.ToLower())
                {
                    case "http":
                        myTable["name"] = "serversite";
                        theHttp2 = new HttpChannel(Props, null, theProvider);
                        ChannelServices.RegisterChannel(theHttp2, false);
                        break;
                    case "tcp":
                        myTable["name"] = "serversite";
                        theTcp = new TcpServerChannel(myTable, theProvider);
                        ChannelServices.RegisterChannel(theTcp, false);
                        break;
                    default:
                        throw new ApplicationException(string.Format("Could not obtain the correct protocol from: {0}\r\nFound protocol: {1}\r\nShould be: http, or tcp", ConfigFilePath, Protocol));
                }
                RemoteObject theObject = new RemoteObject();
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(ACA.LabelX.RemoteObject), "ACALabelXRemoteObject", WellKnownObjectMode.Singleton);

                //Address = "localhost";
                Address = "127.0.0.1";
                string ClientURL = string.Format("{0}://{1}:{2}/{3}", Protocol, Address, Port, Uri);
                ACA.LabelX.RemoteObject objLabelXRemoteObject = (ACA.LabelX.RemoteObject)Activator.GetObject(
                typeof(ACA.LabelX.RemoteObject), ClientURL);
                if (objLabelXRemoteObject != null)
                {
                    toolbox.GetGeneralServerConfiguraton(ConfigFilePath, out PrintJobsRootFolder, out LabelDefinitionsRootFolder, out PaperDefinitionsRootFolder, out SettingsRootFolder, out PictureRootFolder, out UpdateRootFolder);
                    objLabelXRemoteObject.InitServer(PrintJobsRootFolder, LabelDefinitionsRootFolder, PaperDefinitionsRootFolder, SettingsRootFolder, UpdateRootFolder);
                }
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
