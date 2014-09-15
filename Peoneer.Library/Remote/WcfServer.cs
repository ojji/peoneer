using System;
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

        public static void Configure(ServiceConfiguration config)
        {
            config.AddServiceEndpoint(typeof(IMessageProcessor), new WSHttpBinding(), new Uri(StartupUri));
            var serviceMetadataBehavior = new ServiceMetadataBehavior
            {
                HttpGetUrl = new Uri(StartupUri),
                HttpGetEnabled = true,
                MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
            };
            config.Description.Behaviors.Add(serviceMetadataBehavior);
        }

        public ResponseBase ProcessMessage(RequestBase request)
        {
            if (request.GetType().IsAssignableFrom(typeof (EchoRequest)))
            {
                var req = request as EchoRequest;
                ResponseBase response = new EchoResponse {Message = string.Format("Echo: {0}", req.Message)};
                return response;
            }

            return null;
        }

        public void Start()
        {
            if (_serviceHost != null)
            {
                Stop();
            }
            _serviceHost = new ServiceHost(typeof(WcfServer));
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