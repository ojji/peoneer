using System;
using Peoneer.Library.Core;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Messages
{
    public class TimeStampedMessage
    {
        public TimeStampedMessage() : this(new SystemClock())
        {
        }

        public TimeStampedMessage(IClock clock)
        {
            TimeStamp = clock.Now;
        }

        public DateTime TimeStamp { get; private set; }
    }
}