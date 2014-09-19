using System;
using Peoneer.Library.Messages;

namespace Peoneer.Library.Core
{
    public class BuildAgent : IBuildAgent
    {
        private string _name = "BuildAgent-1";

        public BuildAgentPropertiesResponse GetBuildAgentProperties()
        {
            return new BuildAgentPropertiesResponse
            {
                Name = _name,
                Os = "Windows"
            };
        }

        public EchoResponse GenerateEcho(EchoRequest request)
        {
            return new EchoResponse
            {
                Message = string.Format("Echo: {0}",request.Message)
            };
        }
    }
}