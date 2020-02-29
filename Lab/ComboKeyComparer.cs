using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboKeyComparer<TSource> : IComparer<TSource>
    {
        public ComboKeyComparer(IComparer<TSource> firstComparer, IComparer<TSource> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<TSource> FirstComparer { get; private set; }
        public IComparer<TSource> SecondComparer { get; private set; }

        public int Compare(TSource employee, TSource minElement)
        {
            var firstCompareResult = FirstComparer.Compare(employee, minElement);
            if (firstCompareResult != 0)
            {
                return firstCompareResult;
            }

            var secondCompareResult = SecondComparer.Compare(employee, minElement);
            if (secondCompareResult != 0)
            {
                return secondCompareResult;
            }

            return 0;
        }
    }
}