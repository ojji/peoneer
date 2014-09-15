using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Peoneer.Service
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            var serviceInstaller = new ServiceInstaller
            {
                ServiceName = "PeoneerService"
            };

            var serviceprocessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            Installers.Add(serviceprocessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
