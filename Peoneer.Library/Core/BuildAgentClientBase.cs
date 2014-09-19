using Peoneer.Library.Messages;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Core
{
    public abstract class BuildAgentClientBase : DisposableBase
    {
        public string Name { get; protected set; }
        public string EndpointAddress { get; protected set; }

        //public abstract IIntegrationResult ForceBuild(string projectName);
        public abstract ResponseBase EchoMessage(string message);
    }
}