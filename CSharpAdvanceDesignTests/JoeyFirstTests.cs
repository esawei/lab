using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

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
                new Girl() {Age = 60},
                new Girl() {Age = 20},
                new Girl() {Age = 30},
            };

            var girl = JoeyFirst(girls);
            var expected = new Girl {Age = 60};

            expected.ToExpectedObject().ShouldEqual(girl);
        }


        [Test]
        public void get_first_girl_when_no_girls()
        {
            var girls = new Girl[]
            {
            };

            Assert.Catch<InvalidOperationException>(() => JoeyFirst(girls));
        }

        [Test]
        public void get_first_chen()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
            var employee = JoeyFirst(employees, employee1 => employee1.LastName == "Chen");
            new Employee() {FirstName = "Joey", LastName = "Chen"}.ToExpectedObject().ShouldMatch(employee);
        }

        private TSource JoeyFirst<TSource>(IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            var enumerator = sources.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var employee = enumerator.Current;
                if (predicate(employee))
                {
                    return employee;
                }
            }

            throw new InvalidOperationException($"{nameof(sources)} is empty");
        }

        private TSource JoeyFirst<TSource>(IEnumerable<TSource> sources)
        {
            var enumerator = sources.GetEnumerator();
            return enumerator.MoveNext()
                ? enumerator.Current
                : throw new InvalidOperationException($"{nameof(sources)} is empty");
        }
    }
}