using System;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using Peoneer.Library.Core;
using Peoneer.Library.Messages;

namespace Peoneer.Library.Remote
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WcfServer : IBuildAgentServer, IMessageProcessor
    {
        private const string StartupUri = "http://localhost:27500/service";
        private ServiceHost _serviceHost;
        public WcfServer()
        {

        }

        public ResponseBase ProcessMessage(RequestBase request)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            if (_serviceHost != null)
            {
                Stop();
            }
            _serviceHost = new ServiceHost(typeof(WcfServer), new Uri(StartupUri));
            ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();
            serviceMetadataBehavior.HttpGetEnabled = true;
            serviceMetadataBehavior.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            _serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);

            _serviceHost.Open();
        }

        public void Stop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }
    }
}