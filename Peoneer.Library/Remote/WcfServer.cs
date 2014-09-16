using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Peoneer.Library.Core;
using Peoneer.Library.Messages;

namespace Peoneer.Library.Remote
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WcfServer : IBuildAgentServer
    {
        private readonly IBuildAgent _buildAgent;
        private const string StartupUri = "http://localhost:27500/service";
        private ServiceHost _serviceHost;

        public WcfServer() : this(new BuildAgent())
        {
            
        }

        public WcfServer(IBuildAgent buildAgent)
        {
            _buildAgent = buildAgent;
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
            // TODO: very bad temporary code!
            if (request.GetType().IsAssignableFrom(typeof (EchoRequest)))
            {
                return _buildAgent.GenerateEcho(request as EchoRequest);
            }
            else if (request.GetType().IsAssignableFrom(typeof (BuildAgentPropertiesRequest)))
            {
                return _buildAgent.GetBuildAgentProperties();
            }

            return null;
        }

        public string EndpointAddress { get { return StartupUri; }}
        public string Name { get { return "WcfAgent-1"; } }

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