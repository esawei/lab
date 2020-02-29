using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboKeyComparer
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
            var finalCompareResult = 0;
            var firstCompareResult = FirstComparer.Compare(employee, minElement);
            if (firstCompareResult < 0)
            {
                finalCompareResult = firstCompareResult;
            }
            else if (firstCompareResult == 0)
            {
                var secondCompareResult = SecondComparer.Compare(employee, minElement);
                if (secondCompareResult < 0)
                {
                    finalCompareResult = secondCompareResult;
                }
            }

            return finalCompareResult;
        }
    }
}