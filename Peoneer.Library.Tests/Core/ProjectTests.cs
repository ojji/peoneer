using System;
using NUnit.Framework;
using Peoneer.Library.Core;

namespace Peoneer.Library.Tests.Core
{
    [TestFixture]
    public class ProjectTests
    {
        [Test]
        public void Project_with_empty_name_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Project(null, "git://www.something.com"));
            Assert.Throws<ArgumentNullException>(() => new Project("", "git://www.something.com"));
            Assert.DoesNotThrow((() => new Project("teszt project", "git://www.something.com")));
        }
      
    }
}