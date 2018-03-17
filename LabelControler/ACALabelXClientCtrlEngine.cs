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
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;
using System.IO;
using ACA;

namespace ACA.LabelX.ClientCtrlEngine
{
    public class LabelXClientCtrlEngineException : ApplicationException
    {
        public LabelXClientCtrlEngineException()
        {
        }
        public LabelXClientCtrlEngineException(string message)
            : base(message)
        {
        }
        public LabelXClientCtrlEngineException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    class ACALabelXClientCtrlEngine
    {
        private bool StartCalled = false;
        public string ServerURL = "";

        public void EnsureStartIsCalled()
        {
            if (StartCalled == false)
                throw new LabelXClientCtrlEngineException("Not connected to the remote object, call Start first!");
        }

        private ACA.LabelX.Client.RemClientControlObject GetRemoteObject()
        {
            EnsureStartIsCalled();

            ACA.LabelX.Client.RemClientControlObject obj = (ACA.LabelX.Client.RemClientControlObject)Activator.GetObject(typeof(ACA.LabelX.Client.RemClientControlObject), ServerURL);
            if (obj == null)
                throw new RemotingException("Cannot get the remote object.");

            return obj;
        }

        public void PingRemoteServer()
        {
            ACA.LabelX.Client.RemClientControlObject remClientControlObject = GetRemoteObject();
            string name = "";
            remClientControlObject.GetName(out name);
            remClientControlObject.Ping(name);
        }

        public void Start()
        {
            StartCalled = true;

            string AppPath = ".\\"; // System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string RemotingConfigFilePath = AppPath + @"\ACALabelXClientRemote.config.xml";
            if (!File.Exists(RemotingConfigFilePath))
                throw new LabelXClientCtrlEngineException(string.Format("Remoting configuration file doesn't exist: {0}", RemotingConfigFilePath));

            string Protocol;
            string Address;
            string Port;
            string Uri;

            Toolbox.Toolbox toolbox = new Toolbox.Toolbox();
            toolbox.GetRemotingClientConfiguraton(RemotingConfigFilePath, out Protocol, out Address, out Port, out Uri);

            switch (Protocol.ToLower())
            {
                case "http":
                    ChannelServices.RegisterChannel(new HttpClientChannel(), false);
                    break;
                case "tcp":
                    ChannelServices.RegisterChannel(new TcpClientChannel(), false);
                    break;
                default:
                    throw new LabelXClientCtrlEngineException(string.Format("Cannot obtain the correct protocol from the Remoting configuration file: {0}\r\nProtocol found: {1}\r\nShould be: http, or tcp", RemotingConfigFilePath, Protocol));
            }

            ServerURL = string.Format("{0}://{1}:{2}/{3}", Protocol, Address, Port, Uri);
        }
        private string Remote_GetLabelPrintGroups()
        {
            byte[] CompressedData = null;
            int UncompressedDataLength = 0;
            GetRemoteObject().GetLabelPrintGroups(ref CompressedData, ref UncompressedDataLength);

            byte[] UncompressedData = PSLib.Compression.Decompress(CompressedData, UncompressedDataLength);
            return Encoding.ASCII.GetString(UncompressedData);
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
