using Peoneer.Library.Core;
using Peoneer.Library.Messages;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Remote
{
    public class WcfClient : BuildAgentClientBase
    {
        private readonly WcfChannelFactoryWrapper<IMessageProcessor> _innerProcessor;

        public WcfClient(string endpointAddress)
        {
            EndpointAddress = endpointAddress;
            _innerProcessor = new WcfChannelFactoryWrapper<IMessageProcessor>(endpointAddress);
            Name = GetBuildAgentName();
        }

        private string GetBuildAgentName()
        {
            var request = new BuildAgentPropertiesRequest();
            var response = ProcessMessage(request) as BuildAgentPropertiesResponse;
            return response.Name;
        }

        public override sealed string Name { get; protected set; }

        public override ResponseBase ProcessMessage(RequestBase request)
        {
            return _innerProcessor.GetClient().ProcessMessage(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                _innerProcessor.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}