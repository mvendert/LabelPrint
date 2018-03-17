using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;

namespace ACALP_Config
{
    public class ServiceManager
    {
        public void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                // ...
            }
        }
        public void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch
            {
                // ...
            }
        }
        public void RestartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                // ...
            }
        }
        public bool ServiceExists(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices(".");
            /*
            foreach (ServiceController q in services)
            {
                System.Diagnostics.Debug.WriteLine(q.ServiceName);
            }
            */
            return services.Any(s => s.ServiceName == serviceName);
        }
        public ServiceControllerStatus GetServiceStatus(string serviceName)
        {
            ServiceController service = new ServiceController();
            service.MachineName = ".";
            service.ServiceName = serviceName;
            if (ServiceExists(serviceName))
            {
                return service.Status;
            }
            else
            {
                return ServiceControllerStatus.Stopped;
            }
        }
        public void WaitForStatus(string serviceName, ServiceControllerStatus desiredStatus, TimeSpan timeOut)
        {
            ServiceController service = new ServiceController();
            service.MachineName = ".";
            service.ServiceName = serviceName;
            if (ServiceExists(serviceName))
            {
                service.WaitForStatus(desiredStatus, timeOut);
            }
        }
    }
    public class ClientServiceManager :  ServiceManager
    {
        private int timeoutMilliseconds = 30000;
        private string clientServiceName = "ACALabelXClientService";
    
        public void StartService()
        {
            StartService(clientServiceName, timeoutMilliseconds);
        }
        public void StopService()
        {
            StopService(clientServiceName, timeoutMilliseconds);
        }
        public void RestartService()
        {
            RestartService(clientServiceName, timeoutMilliseconds*2);
        }
        public bool ServiceExists()
        {
            return ServiceExists(clientServiceName);
        }
        public ServiceControllerStatus GetServiceStatus()
        {
            return GetServiceStatus(clientServiceName);
        }
    }
    public class ServerServiceManager : ServiceManager
    {
        private int timeoutMilliseconds = 30000;
        private string serverServiceName = "ACALabelXServerService";

        public void StartService()
        {
            StartService(serverServiceName, timeoutMilliseconds);
        }
        public void StopService()
        {
            StopService(serverServiceName, timeoutMilliseconds);
        }
        public void RestartService()
        {
            RestartService(serverServiceName, timeoutMilliseconds*2);
        }
        public bool ServiceExists()
        {
            return ServiceExists(serverServiceName);
        }
        public ServiceControllerStatus GetServiceStatus()
        {
            return GetServiceStatus(serverServiceName);
        }
    }
}
