using Peoneer.Library.Messages;
using Peoneer.Library.Remote;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Core
{
    public abstract class BuildAgentClientBase : DisposableBase, IMessageProcessor
    {
        public abstract string Name { get; protected set; }
        public string EndpointAddress { get; protected set; }
        public abstract ResponseBase ProcessMessage(RequestBase request);
    }
}