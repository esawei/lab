using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class GroupSumTests
    {
        [Test]
        public void group_sum_of_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };

            //sum of all Saving of each group which 3 Account per group
            var actual = MyGroupSum(accounts, 3, x => x.Saving);

            var expected = new[] {60, 150, 240, 210};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_per_3_item_sum_of_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
                new Account {Name = "Bruce", Saving = 120},
            };

            //sum of all Saving of each group which 3 Account per group
            var actual = MyGroupSum(accounts, 3, x => x.Saving);

            var expected = new[] {60, 150, 240, 330};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_per_2_item_sum_of_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };

            var actual = MyGroupSum(accounts, 11, x => x.Saving);

            var expected = new[] {660};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> MyGroupSum<TSource>(IEnumerable<TSource> sources, int pageSize,
            Func<TSource, int> selector)
        {
            var list = sources.ToList();
            var pageIndex = 0;
            while (pageIndex * pageSize < list.Count)
            {
                yield return list.Skip(pageIndex * pageSize).Take(pageSize).Sum(selector);
                pageIndex++;
            }
        }

        //private static IEnumerable<int> MyGroupSum<TSource>(IEnumerable<TSource> sources, int size, Func<TSource, int> selector)
        //{
        //    var enumerator = sources.GetEnumerator();
        //    var count = 0;
        //    var sum = 0;
        //    while (enumerator.MoveNext())
        //    {
        //        var source = enumerator.Current;
        //        if (count < size)
        //        {
        //            sum += selector(source);
        //            count++;
        //        }

        //        if (count == size)
        //        {
        //            yield return sum;
        //            sum = 0;
        //            count = 0;
        //        }
        //    }

        //    if (count > 0)
        //    {
        //        yield return sum;
        //    }
        //}
    }

    public class Account
    {
        public int Saving { get; set; }
        public string Name { get; set; }
    }
}