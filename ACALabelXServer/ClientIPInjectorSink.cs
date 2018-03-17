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
using System.Runtime.Remoting.Messaging;
using System.Net;
using System.Collections;

namespace ACA.LabelX.TestServer
{
    public class ClientIPInjectorSink : BaseChannelObjectWithProperties,  IServerChannelSink
    {
        private IServerChannelSink _NextSink;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ClientIPInjectorSink"/> class.
        /// </summary>
        /// <param name="nextSink">The next sink.</param>
        public ClientIPInjectorSink(IServerChannelSink nextSink)
        {
            _NextSink = nextSink;
        }

        #region IServerChannelSink Members
        /// <summary>
        /// Gets the response stream.
        /// </summary>
        /// <param name="sinkStack">The sink stack.</param>
        /// <param name="state">The state.</param>
        /// <param name="message">The message.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        public System.IO.Stream GetResponseStream( IServerResponseChannelSinkStack sinkStack, object state, IMessage message, ITransportHeaders headers)
        {
            return null;
        }

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="sinkStack">The sink stack.</param>
        /// <param name="requestmessage">The request message.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="requestStream">The request stream.</param>
        /// <param name="responseMessage">The response message.</param>
        /// <param name="responseHeaders">The response headers.</param>
        /// <param name="responseStream">The response stream.</param>
        /// <returns></returns>
        public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack,IMessage requestmessage,
         ITransportHeaders requestHeaders,
         System.IO.Stream requestStream,
         out IMessage responseMessage,
         out ITransportHeaders responseHeaders,
         out System.IO.Stream responseStream)
        {
            //get the client's ip address, and put it in the call context. This valuewill be
            //extracted later so we can determine the actual address of the client.
            try
            {
                IPAddress ipAddr =
                (IPAddress)requestHeaders[CommonTransportKeys.IPAddress];
                CallContext.SetData("ClientIP", ipAddr);
                //GlobalDataStore.Logger.Debug(string.Format("Messagesink retrieved IP: {0}",ipAddr.ToString()));
            }
            catch (Exception)
            {
                //do nothing
            }
            //pushing onto stack and forwarding the call
            sinkStack.Push(this, null);

            ServerProcessing srvProc = _NextSink.ProcessMessage(
                sinkStack,
                requestmessage,
                requestHeaders,
                requestStream,
                out responseMessage,
                out responseHeaders,
                out responseStream);
            if (srvProc == ServerProcessing.Complete)
            {
                    //TODO - implement post processing
            }
            return srvProc;
        }

        /// <summary>
        /// Asyncs the process response.
        /// </summary>
        /// <param name="sinkStack">The sink stack.</param>
        /// <param name="state">The state.</param>
        /// <param name="message">The message.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="stream">The stream.</param>
        public void AsyncProcessResponse(
         IServerResponseChannelSinkStack sinkStack,
         object state,
         IMessage message,
         ITransportHeaders headers,
         System.IO.Stream stream)
        {
            // get the client's ip address, and put it in the call context. This value will be
            // extracted later so we can determine the actual address of the client.
            try
            {
                IPAddress ipAddr = (IPAddress)headers[CommonTransportKeys.IPAddress];
                CallContext.SetData("ClientIP", ipAddr);
            }
            catch (Exception)
            {
                //do nothing
            }
            //forward to stack for further processing
            sinkStack.AsyncProcessResponse(message, headers, stream);
        }

        /// <summary>
        /// Gets the next server channel sink in the server sink chain.
        /// </summary>
        /// <value></value>
        /// <returns>The next server channel sink in the server sink chain.</returns>
        /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have the required <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Infrastructure"></see> permission. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure"/></PermissionSet>
        public IServerChannelSink NextChannelSink
        {
            get
            {
                return _NextSink;
            }
        }
        #endregion IServerChannelSink
    }

    /// <summary>
    /// 
    /// </summary>
    public class ClientIPInjectorSinkProvider : IServerChannelSinkProvider
    {
        private IServerChannelSinkProvider nextProvider;

        #region IServerChannelSinkProvider Members

        public ClientIPInjectorSinkProvider(IDictionary properties, ICollection providerdata)
        {
            //not for now
        }
        public ClientIPInjectorSinkProvider()
        {
        }
        /// <summary>
        /// Creates the sink.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns></returns>
        public IServerChannelSink CreateSink(IChannelReceiver channel)
        {
            //create other sinks in the chain
            IServerChannelSink nextSink = nextProvider.CreateSink(channel);
            return new ClientIPInjectorSink(nextSink);
        }

        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        /// <value>The next.</value>
        public IServerChannelSinkProvider Next
        {
            get
            {
                return nextProvider;
            }
            set
            {
                nextProvider = value;
            }
        }

        /// <summary>
        /// Gets the channel data.
        /// </summary>
        /// <param name="channelData">The channel data.</param>
        public void GetChannelData(IChannelDataStore channelData)
        {
            //not needed
        }
        #endregion IServerChannelSinkProvider Members
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
