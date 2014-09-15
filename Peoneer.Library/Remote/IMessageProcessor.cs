using System.ServiceModel;
using Peoneer.Library.Messages;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Remote
{
    [ServiceContract]
    [ServiceKnownType("LoadKnownMessages", typeof(WcfKnownTypesProvider))]
    public interface IMessageProcessor
    {
        [OperationContract]
        ResponseBase ProcessMessage(RequestBase request);
    }
}