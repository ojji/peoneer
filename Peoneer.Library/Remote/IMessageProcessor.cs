using System.ServiceModel;
using Peoneer.Library.Messages;

namespace Peoneer.Library.Remote
{
    [ServiceContract]
    public interface IMessageProcessor
    {
        [OperationContract]
        ResponseBase ProcessMessage(RequestBase request);
    }
}