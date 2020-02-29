using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeyOrderByLastName(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var firstComparer =
                new CombineKeyComparer<Employee, string>(employee => employee.LastName, Comparer<string>.Default);
            var secondComparer =
                new CombineKeyComparer<Employee, string>(employee => employee.FirstName, Comparer<string>.Default);
            var actual = JoeyOrderByLastNameAndFirstName(
                employees,
                new ComboKeyComparer<Employee>(
                    firstComparer,
                    secondComparer));

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_and_firstName_and_nickName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen", NickName = "91"},
                new Employee {FirstName = "Joey", LastName = "Chen", NickName = "19"},
            };

            var comparer = new ComboKeyComparer<Employee>(
                new CombineKeyComparer<Employee, string>(employee => employee.LastName, Comparer<string>.Default),
                new CombineKeyComparer<Employee, string>(employee => employee.FirstName, Comparer<string>.Default));
            comparer = new ComboKeyComparer<Employee>(
                comparer,
                new CombineKeyComparer<Employee, string>(e => e.NickName, Comparer<string>.Default));

            var actual = JoeyOrderByLastNameAndFirstName(employees, comparer);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", NickName = "19"},
                new Employee {FirstName = "Joey", LastName = "Chen", NickName = "91"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_and_firstName_and_age()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 25},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 18},
            };

            var comparer = new ComboKeyComparer<Employee>(
                new CombineKeyComparer<Employee, string>(employee => employee.LastName, Comparer<string>.Default),
                new CombineKeyComparer<Employee, string>(employee => employee.FirstName, Comparer<string>.Default));
            comparer = new ComboKeyComparer<Employee>(
                comparer,
                new CombineKeyComparer<Employee, int>(e => e.Age, Comparer<int>.Default));

            var actual = JoeyOrderByLastNameAndFirstName(employees, comparer);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 18},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 25},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeyOrderByLastNameAndFirstName<TSource>(
            IEnumerable<TSource> employees,
            IComparer<TSource> comparer)
        {
            //selection sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var employee = elements[i];

                    var finalCompareResult = comparer.Compare(employee, minElement);
                    if (finalCompareResult < 0)
                    {
                        minElement = employee;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}