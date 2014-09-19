using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Remote
{
    public class WcfChannel<T> : DisposableBase, IChannel<T> where T : class
    {
        private T _client;
        private readonly EndpointAddress _endpointAddress;
        private readonly Binding _binding;

        public WcfChannel(string endpointUrl)
        {
            if (string.IsNullOrEmpty(endpointUrl))
            {
                throw new ArgumentNullException("endpointUrl");
            }

            _endpointAddress = new EndpointAddress(endpointUrl);
            _binding = new WSHttpBinding();
        }

        public Binding Binding { get { return _binding; } }
        public EndpointAddress EndpointAddress { get { return _endpointAddress; } }

        public T Client
        {
            get
            {
                if (IsDisposed)
                {
                    throw new InvalidOperationException("This channel is already disposed.");
                }
                return _client ?? (_client = GetClient());
            }
        }
        private T GetClient()
        {
            var channelFactory = new ChannelFactory<T>(_binding, _endpointAddress);
            return channelFactory.CreateChannel();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (_client != null && !IsDisposed)
            {
                bool success = false;
                var clientAsCommunicationObject = (ICommunicationObject) _client;
                try
                {
                    if (clientAsCommunicationObject.State != CommunicationState.Faulted)
                    {
                        clientAsCommunicationObject.Close();
                        success = true;
                    }
                }
                catch (TimeoutException ex)
                {
                    // todo: log
                }
                catch (CommunicationException ex)
                {
                    // todo: log
                }
                finally
                {
                    if (!success)
                    {
                        clientAsCommunicationObject.Abort();
                        _client = null;
                    }
                }
            }

            base.Dispose(isDisposing);
        }
    }
}