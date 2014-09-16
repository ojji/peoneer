using System;
using System.ServiceModel;
using NUnit.Framework;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Tests.Utilities
{
    [TestFixture]
    public class WcfChannelFactoryWrapperTests
    {
        [ServiceContract]internal interface IEmptyClient { [OperationContract] void Foo(); }
        private const string TestUri = @"http://localhost:8898/teszt";

        [Test]
        public void Empty_url_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new WcfChannelFactoryWrapper<IEmptyClient>(""));
        }

        [Test]
        public void Valid_url_should_point_to_the_specified_endpoint_address()
        {
            using (var subject = new WcfChannelFactoryWrapper<IEmptyClient>(TestUri))
            {
                Assert.That(subject.Endpoint.Uri.ToString(), Is.EqualTo(TestUri));
            }
        }

        [Test]
        public void Valid_url_should_create_a_WSHttpBinding()
        {
            using (var subject = new WcfChannelFactoryWrapper<IEmptyClient>(TestUri))
            {
                Assert.IsInstanceOf<WSHttpBinding>(subject.Binding);
            }
        }

        [Test]
        public void Wrapper_should_return_a_specified_client_instance()
        {
            using (var subject = new WcfChannelFactoryWrapper<IEmptyClient>(TestUri))
            {
                IEmptyClient client = subject.GetClient();
                Assert.IsInstanceOf<IEmptyClient>(subject.Client);
                Assert.IsNotNull(client);
            }
        }

        [Test]
        public void Accessing_the_Client_property_before_GetClient_should_return_null()
        {
            using (var subject = new WcfChannelFactoryWrapper<IEmptyClient>(TestUri))
            {
                Assert.IsNull(subject.Client);
            }
        }

        [Test]
        public void Calling_Dispose_on_a_closed_channel_should_throw_InvalidOperationException()
        {
            TestDelegate alreadyClosedChannel = () =>
            {
                var subject = new WcfChannelFactoryWrapper<IEmptyClient>(TestUri);
                subject.GetClient();
                subject.Dispose();
                subject.Dispose();
            };

            Assert.Throws<InvalidOperationException>(alreadyClosedChannel);
        }
    }
}