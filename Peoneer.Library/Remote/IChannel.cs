using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Remote
{
    public interface IChannel<out T> : IDisposable where T : class
    {
        EndpointAddress EndpointAddress { get; }
        Binding Binding { get; }
        T Client { get; }
    }
}