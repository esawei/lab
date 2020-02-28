﻿using System;
using System.Collections.Generic;
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
            var actual = MyGroupSum(accounts, 3, account => account.Saving);

            var expected = new[] {60, 150, 240, 210};

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> MyGroupSum<TSource>(IEnumerable<TSource> sources, int size, Func<TSource, int> selector)
        {
            var enumerator = sources.GetEnumerator();
            int sum = 0;
            var count = 0;
            while (enumerator.MoveNext())
            {
                var source = enumerator.Current;
                if (count < size)
                {
                    sum += selector(source);
                    count++;
                }

                if (count == size)
                {
                    yield return sum;
                    sum = 0;
                    count = 0;
                }
            }

            if (count > 0)
            {
                yield return sum;
            }
        }
    }

    public class Account
    {
        public int Saving { get; set; }
        public string Name { get; set; }
    }
}