using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyLastTests
    {
        [Test]
        public void get_last_employee()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Cash", LastName = "Li"},
            };

            var employee = JoeyLast(employees);

            new Employee {FirstName = "Cash", LastName = "Li"}
                .ToExpectedObject().ShouldMatch(employee);
        }

        [Test]
        public void get_last_employee_when_no_employee()
        {
            var employees = new Employee[]
            {
            };

            TestDelegate action = () => JoeyLast(employees);
            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void get_last_chen()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Cash", LastName = "Li"},
            };

            var employee = JoeyLastWithCondition(employees, current => current.LastName == "Chen");

            new Employee {FirstName = "David", LastName = "Chen"}
                .ToExpectedObject().ShouldMatch(employee);
        }

        [Test]
        public void get_last_chen_when_no_last_chen()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Cash", LastName = "Li"},
            };

            TestDelegate action = () => JoeyLastWithCondition(employees, current => current.LastName == "Chen");
            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void get_last_chen_when_no_employee()
        {
            var employees = new List<Employee>
            {
            };

            TestDelegate action = () => JoeyLastWithCondition(employees, current => current.LastName == "Chen");
            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void get_last_chen_when_first_employee_is_last_chen()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Cash", LastName = "Li"},
            };

            var employee = JoeyLastWithCondition(employees, current => current.LastName == "Chen");

            new Employee { FirstName = "David", LastName = "Chen" }
                .ToExpectedObject().ShouldMatch(employee);
        }

        private Employee JoeyLastWithCondition(IEnumerable<Employee> employees, Func<Employee, bool> predicate)
        {
            var enumerator = employees.GetEnumerator();
            var hasMatch = false;
            Employee employee = null;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    hasMatch = true;
                    employee = current;
                }
            }

            return hasMatch ? employee : throw new InvalidOperationException($"{nameof(employees)} is empty.");
        }

        private Employee JoeyLast(IEnumerable<Employee> employees)
        {
            var enumerator = employees.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException($"{nameof(employees)} is empty.");
            }

            var last = enumerator.Current;
            while (enumerator.MoveNext())
            {
                last = enumerator.Current;
            }

            return last;

            // Why don't use null to determinate has any item like below code?
            // Because if input sources last item is null, will make wrong result.

            //var enumerator = employees.GetEnumerator();
            //Employee lastItem = null;
            //while (enumerator.MoveNext())
            //{
            //    lastItem = enumerator.Current;
            //}

            //return lastItem ?? throw new InvalidOperationException($"{nameof(employees)} is empty.");
        }
    }
}