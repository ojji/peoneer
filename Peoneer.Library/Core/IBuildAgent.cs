using Peoneer.Library.Messages;

namespace Peoneer.Library.Core
{
    public interface IBuildAgent
    {
        BuildAgentPropertiesResponse GetBuildAgentProperties();
        EchoResponse GenerateEcho(EchoRequest request);
    }
}