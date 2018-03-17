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
using ACA.LabelX.Toolbox;
using System.Collections.Specialized;

namespace LabelControler
{
    public class RemClientControlObjectProxyException : Exception
    {
        public RemClientControlObjectProxyException()
        {
        }
        public RemClientControlObjectProxyException(string message)
            : base(message)
        {
        }
        public RemClientControlObjectProxyException(string message, int hresult)
            : base(message)
        {
            HResult = hresult;
        }
      
        public RemClientControlObjectProxyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    class RemClientControlObjectProxy : IDisposable
    {
        private string sComputer;

        public string Computer
        {
            get { return sComputer; }
            set { sComputer = value; }
        }
        private string sProtocol;

        public string Protocol
        {
            get { return sProtocol; }
            set { sProtocol = value; }
        }
        private int nPortNumber;

        public int PortNumber
        {
            get { return nPortNumber; }
            set { nPortNumber = value; }
        }

        private string ServerUrl;
        private IChannel channel;

        private bool isConnected;

        public ConnectionParameter ConParameter
        {
            get
            {
                ConnectionParameter p;
                p = new ConnectionParameter();
                p.ConnectionName = "remote";
                p.Computer = sComputer;
                p.Protocol = sProtocol;
                p.PortNumber = nPortNumber;
                return p;
            }
            set
            {
                sComputer = value.Computer;
                sProtocol = value.Protocol;
                nPortNumber = value.PortNumber;
            }
        }


        public bool IsConnected
        {
            get { return isConnected; }
        }
        private ACA.LabelX.Client.RemClientControlObject remoteObj;

        public RemClientControlObjectProxy()
        {
            isConnected = false;
            ServerUrl = string.Empty;

            nPortNumber = -1;
            sProtocol = string.Empty;
            sComputer = string.Empty;
            channel = null;
            remoteObj = null;
        }

        private void Connect()
        {
            ACA.LabelX.Client.RemClientControlObject theObj;

            Start();

            theObj = (ACA.LabelX.Client.RemClientControlObject)Activator.GetObject(typeof(ACA.LabelX.Client.RemClientControlObject), ServerUrl);
            if (theObj == null)
            {
                RemClientControlObjectProxyException ex;
                ex = new RemClientControlObjectProxyException("Could not connect",0x01);
                //MessageBox.Show("Could not connect to the printer server.");
                ChannelServices.UnregisterChannel(channel);
                channel = null;
                isConnected = false;
                remoteObj = null;
                throw (ex);
            }
            else
            {
                remoteObj = theObj;
                isConnected = true;
            }
        }

        private void Start()
        {
            //If we connect to other machine with other parameters,
            //first unregister current channel.
            if (channel != null)
            {
                ChannelServices.UnregisterChannel(channel);
                channel = null;
            }
            switch (sProtocol)
            {
                case "http":
                    channel = new HttpChannel();
                    ChannelServices.RegisterChannel(channel, false);
                    break;
                case "tcp":
                    channel = new TcpChannel();
                    ChannelServices.RegisterChannel(channel, false);
                    break;
                default:
                    throw new ApplicationException(string.Format("The selected protocol should be http or tcp. Protocol = {0}", sProtocol));
            }
            ServerUrl = string.Format("{0}://{1}:{2}/{3}", sProtocol, sComputer, nPortNumber, "ACALabelXClientRemControlObject");
        }

        public void PingRemoteServer()
        {
            string name = "";
            remoteObj.GetName(out name);
            remoteObj.Ping(name);
        }

        public ACA.LabelX.Client.RemClientControlObject RemoteObject
        {
            get
            {
                if (remoteObj == null)
                {
                    Connect();
                }
                return remoteObj;
            }
        }
        public ACA.LabelX.Toolbox.PrintGroupItemList GetRemoteLabelPrintGroupsEx()
        {
            ACA.LabelX.Toolbox.PrintGroupItemList theList;
            try
            {
                theList = RemoteObject.GetLabelPrintGroupsEx();
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return theList;
        }
        public StringCollection GetLocalPrinters()
        {
            StringCollection theCol;
            try
            {
                theCol = RemoteObject.GetLocalPrinters();
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return theCol;
        }
        public PrinterItems GetLocalPrintersEx()
        {
            PrinterItems retItems = new PrinterItems();
            try
            {
                retItems = RemoteObject.GetLocalPrintersEx();
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return retItems;
        }
        public ACA.LabelX.Toolbox.PrintGroupItem GetLabelPrintGroupByName(string sName)
        {
            ACA.LabelX.Toolbox.PrintGroupItem theItem;
            try
            {
                theItem = RemoteObject.GetLabelPrintGroupByName(sName);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return theItem;
        }

        public void GetClientFolders(out string PrintJobsRootFolder, out string LabelDefinitionsRootFolder, out string PaperDefinitionsRootFolder)
        {
            try
            {
                RemoteObject.GetFolderInformation(out PrintJobsRootFolder, out LabelDefinitionsRootFolder, out PaperDefinitionsRootFolder);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
        }
        public bool AddPrinterToPrintGroupItem(PrintGroupItem it, PrinterItem  pi)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.AddPrinterToPrintGroupItem(it, pi);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
        public bool RemovePrinterFromPrinterGroup(PrintGroupItem it, PrinterItem pi)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.RemovePrinterFromPrinterGroup(it, pi);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
        public bool RemovePrinterFromPrinterGroup(PrintGroupItem it, string PrinterName)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.RemovePrinterFromPrinterGroup(it, PrinterName) ;
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
        public bool AddPrinterToPrintGroupItem(PrintGroupItem it, string PrinterName)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.AddPrinterToPrintGroupItem(it, PrinterName);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
        public StringCollection GetSupportedTraysOfPrinter(string PrinterName)
        {
            StringCollection col;
            col = new StringCollection();
            try
            {
                col = RemoteObject.GetSupportedTraysOfPrinter(PrinterName);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return col;
        }
        public PrintJobInfos GetPrintjobsForPrintgroup(PrintGroupItem it)
        {
            PrintJobInfos info;
            info = new PrintJobInfos();
            try
            {
                info = RemoteObject.GetPrintjobsForPrintgroup(it);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return info;
        }
        public StringCollection GetPaperTypes()
        {
            StringCollection ret;
            ret = new StringCollection();
            try
            {
                ret = RemoteObject.GetPaperTypes();
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return ret;
        }
        public bool UpdatePrinterForPrintgroup(PrintGroupItem printGroupItem, PrinterItem printerItem)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.UpdatePrinterForPrintgroup(printGroupItem, printerItem);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
        public bool UpdatePrinterStatus(PrintGroupItem pgi, PrinterItem pi)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.UpdatePrinterStatus(pgi, pi);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
        public bool UpdatePrinterStatus(PrintGroupItem it, string PrinterName, bool Status)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.UpdatePrinterStatus( it, PrinterName, Status);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
        public bool UpdatePrintjobStatus(PrintJobInfo fi)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.UpdatePrintjobStatus(fi);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
        public bool UpdatePrintgroupStatus(PrintGroupItem it)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.UpdatePrintgroupStatus(it);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }
 
        #region IDisposable Members

        public void Dispose()
        {
            if (channel != null)
            {
                try
                {
                    ChannelServices.UnregisterChannel(channel);
                }
                catch (Exception)
                {
                    channel = null;
                }
                channel = null;
            }
        }

        #endregion

        public bool AddPrinterpool(string p)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.AddPrinterpool(p);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet;
        }

        public bool RemovePrinterpool(string p)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.RemovePrinterpool(p);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet; 
        }
        public string[] GetPrintjobPreview(PrintJobInfo fi)
        {
            string[] ret;
            ret = null;
            try
            {
                ret = RemoteObject.GetPrintjobPreview(fi);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return ret; 
        }

        public bool RemovePrinterpool(PrintGroupItem it)
        {
            return RemovePrinterpool(it.Name);
        }

        public bool DiscardPrintJob(PrintJobInfo fi)
        {
            bool bRet = false;
            try
            {
                bRet = RemoteObject.DiscardPrintJob(fi);
            }
            catch (Exception ex)
            {
                throw new RemClientControlObjectProxyException("Call to printer service failed.", ex);
            }
            return bRet; 
        }
    }
}
/*
 AcaLabelPrint Copyright (C) 2011  Retailium Software Development BV.
 This program comes with ABSOLUTELY NO WARRANTY; 
 This is free Software, and you are welcome to redistribute it
 under certain conditions. See the License.txt file or 
 GNU GPL 3.0 License at <http://www.gnu.org/licenses>*/
