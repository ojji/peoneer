using System;
using Peoneer.Library.Core;

namespace Peoneer.Library.Utilities
{
    public class SystemClock : IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}