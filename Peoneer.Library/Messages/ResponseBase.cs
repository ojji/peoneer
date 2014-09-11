using System;

namespace Peoneer.Library.Messages
{
    public class ResponseBase
    {
        public object Payload { get; set; }
        public Exception Exception { get; set; }
    }
}