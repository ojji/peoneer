using System;
using System.ServiceModel;
using Peoneer.Library.Core;
using Peoneer.Library.Messages;

namespace Peoneer.Library.Remote
{
    public class WcfClient : BuildAgentClientBase
    {
        private readonly IChannel<IMessageProcessor> _innerChannel;

        public WcfClient(IChannel<IMessageProcessor> innerChannel) : this(innerChannel, null) { }
        public WcfClient(IChannel<IMessageProcessor> innerChannel, string name)
        {
            if (innerChannel == null) { throw new ArgumentNullException("innerChannel"); }
            _innerChannel = innerChannel;
            EndpointAddress = innerChannel.EndpointAddress.ToString();
            // if name is not set, try to get it from the buildagent itself
            Name = string.IsNullOrEmpty(name) ? GetBuildAgentName() : name;
        }

        private string GetBuildAgentName()
        {
            var request = new BuildAgentPropertiesRequest();
            var response = SendMessage(request);
            if (response is BuildAgentPropertiesResponse && response.ResponseStatus != ResponseStatus.Failure)
            {
                return ((BuildAgentPropertiesResponse)response).Name;
            }
            return string.Format("Unknown");
        }
        
        private ResponseBase SendMessage(RequestBase request)
        {
            ResponseBase response;
            try
            {
                response = _innerChannel.Client.ProcessMessage(request);
            }
            catch (EndpointNotFoundException)
            {
                response = new ResponseBase();
                response.AddErrorMessage("Can't connect to buildagent.");
            }
            catch (Exception ex)
            {
                response = new ResponseBase();
                response.AddErrorMessage(ex.Message);
            }
            return response;
        }

        //public override IIntegrationResult ForceBuild(string projectName)
        //{
        //    throw new NotImplementedException();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                _innerChannel.Dispose();
            }
            
            base.Dispose(disposing);
        }

        public override ResponseBase EchoMessage(string message)
        {
            return SendMessage(new EchoRequest {Message = message});
        }
    }
}