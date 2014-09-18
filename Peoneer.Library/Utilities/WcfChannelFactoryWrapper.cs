using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Peoneer.Library.Utilities
{
    public class WcfChannelFactoryWrapper<T> : DisposableBase where T: class
    {
        public WcfChannelFactoryWrapper(string endpointUrl)
        {
            if (string.IsNullOrEmpty(endpointUrl))
            {
                throw new ArgumentNullException("endpointUrl");
            }

            Endpoint = new EndpointAddress(endpointUrl);
            Binding = new WSHttpBinding();
        }

        public T Client { get; private set; }
        public EndpointAddress Endpoint { get; private set; }
        public Binding Binding { get; private set; }

        public T GetClient()
        {
            var channelFactory = new ChannelFactory<T>(Binding, Endpoint);
            Client = channelFactory.CreateChannel();
            return Client;
        }

        protected override void Dispose(bool isDisposing)
        {
            if (Client != null && !IsDisposed)
            {
                bool success = false;
                var clientAsCommunicationObject = (ICommunicationObject)Client;
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
                        Client = null;
                    }
                }
            }

            base.Dispose(isDisposing);
        }
    }
}