using System.Collections.Generic;
using System.IO;

namespace Peoneer.Library.Messages
{
    public class ResponseBase : TimeStampedMessage
    {
        private readonly List<string> _errorMessages = new List<string>();
        private ResponseStatus _responseStatus = ResponseStatus.Success;
        public ResponseStatus ResponseStatus { get { return _responseStatus; } }
        public List<string> ErrorMessages { get { return _errorMessages; } }

        public void AddErrorMessage(string errorMessage, bool setStatusToFailed = true)
        {
            _errorMessages.Add(errorMessage);
            if (setStatusToFailed)
            {
                _responseStatus = ResponseStatus.Failure;
            }
        }
    }
}