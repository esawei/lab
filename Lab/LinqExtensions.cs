using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinqExtensions
    {
        public static List<TSource> JoeyWhere<TSource>(this List<TSource> source, Predicate<TSource> predicate)
        {
            var result = new List<TSource>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var result = new List<TResult>();
            foreach (var item in source)
            {
                result.Add(selector(item));
            }

            return result;
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> numbers, Func<TSource, int, bool> predicate)
        {
            var index = 0;
            foreach (var number in numbers)
            {
                if (predicate(number, index))
                {
                    yield return number;
                }

                index++;
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, int, TResult> selector)
        {
            var result = new List<TResult>();
            var index = 0;
            foreach (var url in urls)
            {
                result.Add(selector(url, index));
                index++;
            }
            return result;
        }
    }
}