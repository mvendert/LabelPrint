using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ACALP_Config
{
    class IPAddressHelper
    {
        System.Collections.Generic.List<IPAddress> theList;
        
        public IPAddressHelper()
        {
            theList = new List<IPAddress>();
        }

        public string HostName
        {
            get
            {
                return Dns.GetHostName();
            }
        }

        public List<IPAddress> GetIpAddressList(String hostString)
        {
            string retval;
            retval = string.Empty;
            theList.Clear();
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(hostString);
                retval = hostInfo.HostName;
                System.Diagnostics.Debug.WriteLine("Host name :" + hostInfo.HostName);
                hostInfo = Dns.GetHostEntry(hostInfo.HostName);
                System.Diagnostics.Debug.WriteLine("IP address List : ");
                for (int index = 0; index < hostInfo.AddressList.Length; index++)
                {
                    if (hostInfo.AddressList[index].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        theList.Add(hostInfo.AddressList[index]);
                        System.Diagnostics.Debug.WriteLine(hostInfo.AddressList[index]);
                    }
                }
                theList.Add(IPAddress.Loopback);
            }
            //catch (SocketException e)
            //{
            //   System.Diagnostics.Debug.WriteLine("SocketException caught!!!");
            //   System.Diagnostics.Debug.WriteLine("Source : " + e.Source);
            //   System.Diagnostics.Debug.WriteLine("Message : " + e.Message);
            //}
            catch (ArgumentNullException e)
            {
                System.Diagnostics.Debug.WriteLine("ArgumentNullException caught!!!");
                System.Diagnostics.Debug.WriteLine("Source : " + e.Source);
                System.Diagnostics.Debug.WriteLine("Message : " + e.Message);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception caught!!!");
                System.Diagnostics.Debug.WriteLine("Source : " + e.Source);
                System.Diagnostics.Debug.WriteLine("Message : " + e.Message);
            }
            return theList;
        }
    }
}
