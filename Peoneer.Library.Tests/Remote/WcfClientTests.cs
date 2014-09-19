using System;
using System.ServiceModel;
using Moq;
using NUnit.Framework;
using Peoneer.Library.Messages;
using Peoneer.Library.Remote;

namespace Peoneer.Library.Tests.Remote
{
    [TestFixture]
    public class WcfClientTests
    {
        [Test]
        public void WcfClient_with_null_innerChannel_should_throw_NullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => new WcfClient(null));
        }
        
        [Test]
        public void WcfClient_with_name_set_should_return_that_name()
        {
            var subject = new WcfClient(TestChannel.Create(null), "testname");
            Assert.That(subject.Name, Is.EqualTo("testname"));
        }

        [Test]
        public void WcfClient_with_no_name_set_should_connect_to_the_channel_to_get_name()
        {
            const string testBuildAgentName = "teszt-buildagent";
            var testChannel = TestChannel.Create(r => new BuildAgentPropertiesResponse {Name = testBuildAgentName});
            var subject = new WcfClient(testChannel);

            Assert.That(subject.Name, Is.EqualTo(testBuildAgentName));
        }

        [Test]
        public void WcfClient_with_no_name_and_failed_connection_should_return_name_as_Unknown()
        {
            var failingChannel = TestChannel.Create(r => { throw new EndpointNotFoundException(); });
            var subject = new WcfClient(failingChannel);
            Assert.That(subject.Name, Is.EqualTo("Unknown"));
        }

        [Test]
        public void WcfClients_endpointaddress_should_return_the_inner_channels_endpoint()
        {
            var testUri = "http://tesztUri.com/teszt".ToLowerInvariant();
            var testChannel = TestChannel.Create(null, testUri);
            var subject = new WcfClient(testChannel);

            Assert.That(subject.EndpointAddress, Is.Not.Null);
            Assert.That(subject.EndpointAddress, Is.EqualTo(testUri));
        }
      

        [Test]
        public void EchoMessage_should_return_echoed_message_when_the_connection_is_okay()
        {
            const string echoedMessage = "teszt echo";
            var testChannel = TestChannel.Create(r => new EchoResponse {Message = echoedMessage});

            var subject = new WcfClient(testChannel);

            var result = subject.EchoMessage("message");
            Assert.That(result.ResponseStatus, Is.EqualTo(ResponseStatus.Success));
            Assert.IsInstanceOf<EchoResponse>(result);
            Assert.That(((EchoResponse)result).Message, Is.EqualTo(echoedMessage));
        }

        [Test]
        public void Echomessage_should_return_a_failed_response_when_the_channel_fails()
        {
            var testChannel = TestChannel.Create(r =>
            {
                throw new EndpointNotFoundException();
            });

            var subject = new WcfClient(testChannel);

            var result = subject.EchoMessage("message");
            Assert.That(result.ResponseStatus, Is.EqualTo(ResponseStatus.Failure));
            Assert.IsNotInstanceOf<EchoResponse>(result);
        }
      
      
    }

    internal static class TestChannel
    {
        private const string TestUri = "http://localhost:29292/teszt";
        public static IChannel<IMessageProcessor> Create(Func<RequestBase, ResponseBase> processMessageDelegate, string endpointUri = null)
        {
            var mockedChannel = new Mock<IChannel<IMessageProcessor>>();
            mockedChannel
                .SetupGet(c => c.Client)
                .Returns(TestProcessor.Create(processMessageDelegate));

            mockedChannel
                .SetupGet(c => c.EndpointAddress)
                .Returns(new EndpointAddress(endpointUri ?? TestUri));

            return mockedChannel.Object;
        }
    }

    internal class TestProcessor : IMessageProcessor
    {
        private readonly Func<RequestBase, ResponseBase> _testProcess;

        public TestProcessor(Func<RequestBase, ResponseBase> testProcess)
        {
            _testProcess = testProcess;
        }

        public ResponseBase ProcessMessage(RequestBase request)
        {
            if (_testProcess != null) return _testProcess(request);
            return defaultProcess();
        }

        private ResponseBase defaultProcess()
        {
            return new ResponseBase();
        }

        public static TestProcessor Create(Func<RequestBase, ResponseBase> processMessageDelegate)
        {
            return new TestProcessor(processMessageDelegate);
        }
    }
}