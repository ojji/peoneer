using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Peoneer.Library.Core;
using Peoneer.Library.Remote;

namespace Peoneer.Service
{
    public class ServiceRunner : ServiceBase
    {
        private readonly IBuildAgentServer _server;
        public ServiceRunner()
        {
            ServiceName = "PeoneerService";
            _server = new WcfServer();
        }

        public static void Main(string[] args)
        {
            Run(new ServiceBase[] { new ServiceRunner() });
        }

        protected override void OnStart(string[] args)
        {
            _server.Start();
        }

        protected override void OnStop()
        {
            _server.Stop();
        }
    }
}
