using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Immutables.Tests
{
    [TestFixture]
    public class PatternMatchingTests
    {
        [Test]
        public void API_Test()
        {
            var result = 12345.Match<int,string>(c => c.Case(j => j > 10000).Then(j => "yessir"),
                    c => c.Case(j => j > 200 ).Then(j => "youbetchya"),
                    c => c.Else(j => "no way jose"));            
            Assert.That(result, Is.EqualTo("yessir"));

            result = 9999.Match<int, string>(c => c.Case(j => j > 10000).Then(j => "yessir"),
                    c => c.Case(j => j > 200).Then(j => "youbetchya"),
                    c => c.Else(j => "no way jose"));
            Assert.That(result, Is.EqualTo("youbetchya"));


            result = 99.Match<int, string>(c => c.Case(j => j > 10000).Then(j => "yessir"),
                    c => c.Case(j => j > 200).Then(j => "youbetchya"),
                    c => c.Else(j => "no way jose"));
            Assert.That(result, Is.EqualTo("no way jose"));
        }
    }
}
