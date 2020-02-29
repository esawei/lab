using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboKeyComparer : IComparer<Employee>
    {
        public ComboKeyComparer(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<Employee> FirstComparer { get; private set; }
        public IComparer<Employee> SecondComparer { get; private set; }

        public int Compare(Employee employee, Employee minElement)
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