using System;
using System.ServiceModel;
using NUnit.Framework;
using Peoneer.Library.Remote;

namespace Peoneer.Library.Tests.Utilities
{
    [TestFixture]
    public class WcfChannelTests
    {
        [ServiceContract]
        internal interface IEmptyClient
        {
            [OperationContract]
            void Foo();
        }

        private const string TestUri = @"http://localhost:8898/teszt";

        [Test]
        public void Calling_Dispose_on_a_closed_channel_should_not_throw()
        {
            TestDelegate alreadyClosedChannel = () =>
            {
                var subject = new WcfChannel<IEmptyClient>(TestUri);
                IEmptyClient client = subject.Client;
                subject.Dispose();
                subject.Dispose();
            };

            Assert.DoesNotThrow(alreadyClosedChannel);
        }

        [Test]
        public void Calling_the_client_after_dispose_should_throw_InvalidOperationException()
        {
            var subject = new WcfChannel<IEmptyClient>(TestUri);

            IEmptyClient client = subject.Client;
            subject.Dispose();

            Assert.Throws<InvalidOperationException>(() => { IEmptyClient secondAccess = subject.Client; });
        }

        [Test]
        public void Empty_url_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new WcfChannel<IEmptyClient>(""));
        }

        [Test]
        public void Valid_url_should_create_a_WSHttpBinding()
        {
            using (var subject = new WcfChannel<IEmptyClient>(TestUri))
            {
                Assert.IsInstanceOf<WSHttpBinding>(subject.Binding);
            }
        }

        [Test]
        public void Valid_url_should_point_to_the_specified_endpoint_address()
        {
            using (var subject = new WcfChannel<IEmptyClient>(TestUri))
            {
                Assert.That(subject.EndpointAddress.Uri.ToString(), Is.EqualTo(TestUri));
            }
        }

        [Test]
        public void Wrapper_should_return_a_specified_client_instance()
        {
            using (var subject = new WcfChannel<IEmptyClient>(TestUri))
            {
                IEmptyClient client = subject.Client;
                Assert.IsInstanceOf<IEmptyClient>(subject.Client);
                Assert.IsNotNull(client);
            }
        }
    }
}