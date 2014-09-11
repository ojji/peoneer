using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Peoneer.Service
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            var serviceInstaller = new ServiceInstaller()
            {
                ServiceName = "PeoneerService"
            };

            var serviceprocessInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem
            };

            Installers.Add(serviceprocessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
