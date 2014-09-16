using Peoneer.Library.Remote;

namespace Peoneer.Library.Core
{
    public interface IBuildAgentServer : IMessageProcessor
    {
        string EndpointAddress { get; }
        string Name { get; }
        void Start();
        void Stop();
    }
}