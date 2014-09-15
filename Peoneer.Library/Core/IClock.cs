using System;

namespace Peoneer.Library.Core
{
    public interface IClock
    {
        DateTime Now { get; } 
    }
}