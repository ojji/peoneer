using System;
using NUnit.Framework;
using Peoneer.Library.Core;
using Peoneer.Library.Messages;

namespace Peoneer.Tests.Messages
{
    [TestFixture]
    public class TimeStampedMessageTests
    {
        private static readonly DateTime TestDateTime = new DateTime(1999,1,1,12,0,0);

        private class TestClock : IClock
        {
            public bool NowWasCalled = false;

            public DateTime Now
            {
                get { NowWasCalled = true; return TestDateTime;}
            }
        }
        [Test]
        public void Should_use_the_clocks_now()
        {
            var testClock = new TestClock();
            var subject = new TimeStampedMessage(testClock);
            Assert.IsTrue(testClock.NowWasCalled);
            Assert.That(subject.TimeStamp, Is.EqualTo(TestDateTime));
        }
    }
}