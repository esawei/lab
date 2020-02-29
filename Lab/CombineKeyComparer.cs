using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class CombineKeyComparer : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, string> firstKeySelector, IComparer<string> firstKeyComparer)
        {
            FirstKeySelector = firstKeySelector;
            FirstKeyComparer = firstKeyComparer;
        }

        private Func<Employee, string> FirstKeySelector { get; set; }
        private IComparer<string> FirstKeyComparer { get; set; }

        public int Compare(Employee x, Employee y)
        {
            var firstCompareResult = FirstKeyComparer.Compare(FirstKeySelector(x),
                FirstKeySelector(y));
            return firstCompareResult;
        }
    }
}