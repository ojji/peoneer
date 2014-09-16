using NUnit.Framework;
using Peoneer.Library.Messages;
using Peoneer.Library.Utilities;

namespace Peoneer.Library.Tests.Utilities
{
    [TestFixture]
    public class WcfKnownTypesProviderTests
    {
        [Test]
        public void Should_not_return_base_class()
        {
            var types = WcfKnownTypesProvider.LoadKnownMessages(null);
            Assert.That(types, Has.No.Member(WcfKnownTypesProvider.BaseType));
        }

        [Test]
        public void Should_return_types_derived_from_base_class()
        {
            var types = WcfKnownTypesProvider.LoadKnownMessages(null);
            Assert.Contains(typeof(EchoRequest), types);
            Assert.Contains(typeof(EchoResponse), types);
        }
    }
}