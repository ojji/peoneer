using System;
using Peoneer.Library.Core;
using Peoneer.Library.Messages;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Remote
{
    public class WcfClient : BuildAgentClientBase
    {
        private readonly WcfChannelFactoryWrapper<IMessageProcessor> _innerProcessor;

        public WcfClient(string endpointAddress) : this(endpointAddress, null) { }
        public WcfClient(string endpointAddress, string name)
        {
            EndpointAddress = endpointAddress;
            _innerProcessor = new WcfChannelFactoryWrapper<IMessageProcessor>(endpointAddress);
            // if name is not set, try to get it from the buildagent itself
            if (string.IsNullOrEmpty(name))
            {
                Name = GetBuildAgentName();
            }
        }

        private string GetBuildAgentName()
        {
            var request = new BuildAgentPropertiesRequest();
            var response = ProcessMessage(request) as BuildAgentPropertiesResponse;
            if (response == null || response.ResponseStatus == ResponseStatus.Failure)
            {
                return "Unknown";
            }
            return response.Name;
        }

        public override ResponseBase ProcessMessage(RequestBase request)
        {
            ResponseBase response;
            try
            {
                response = _innerProcessor.GetClient().ProcessMessage(request);
            }
            catch (Exception ex)
            {
                response = new ResponseBase();
                response.AddErrorMessage(ex.Message);
            }
            return response;
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