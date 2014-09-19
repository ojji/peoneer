using NUnit.Framework;
using Peoneer.Library.Messages;
using Peoneer.Library.Remote;

namespace Peoneer.IntegrationTests
{
    [TestFixture]
    public class Service_closing_causes_exception_in_client_when_it_is_disposed_bug
    {
        [Test]
        public void Calling_dispose_on_a_WcfClient_should_not_throw_EndpointNotFoundException()
        {
            // start the server
            var wcfServer = new WcfServer();
            wcfServer.Start();

            // create the client and test it
            var client = new WcfClient(new WcfChannel<IMessageProcessor>(wcfServer.EndpointAddress), "testclient");
            Assert.DoesNotThrow(() =>
            {
                var response = client.EchoMessage("teszt");
                Assert.IsInstanceOf<EchoResponse>(response);
                Assert.That(((EchoResponse)response).Message, Is.StringContaining("Echo: teszt"));
            });

            // now stop the server and dispose the client and check if it throws
            wcfServer.Stop();
            Assert.DoesNotThrow(client.Dispose);
        }
      
    }
}
