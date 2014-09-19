using Peoneer.Library.Core;

namespace Peoneer.Library.Messages
{
    public class IntegrationResponse : ResponseBase
    {
        public IIntegrationResult BuildResult { get; set; }
    }
}