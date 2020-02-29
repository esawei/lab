using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyFirstTests
    {
        [Test]
        public void get_first_girl()
        {
            var girls = new[]
            {
                new Girl(){Age = 60},
                new Girl(){Age = 20},
                new Girl(){Age = 30},
            };

            var girl = girls.JoeyFirst();
            var expected = new Girl { Age = 60 };

            expected.ToExpectedObject().ShouldEqual(girl);
        }


        [Test]
        public void get_first_girl_when_no_girls()
        {
            var girls = new Girl[]
            {
            };

            Assert.Catch<InvalidOperationException>(() => girls.JoeyFirst());
        }
    }
}