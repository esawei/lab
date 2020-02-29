using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinqExtensions
    {
        public static bool JoeyAll<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            var enumerator = sources.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (!predicate(current))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            var enumerator = sources.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var source = enumerator.Current;
                if (predicate(source))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> sources)
        {
            return sources.GetEnumerator().MoveNext();
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return selector(enumerator.Current);
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> sources,
            Func<TSource, int, TResult> selector)
        {
            var enumerator = sources.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                yield return selector(enumerator.Current, index);
                index++;
            }
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(this IEnumerable<TSource> sources, int count)
        {
            var enumerator = sources.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index >= count)
                {
                    yield return enumerator.Current;
                }

                index++;
            }
        }

        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> sources, int count)
        {
            var enumerator = sources.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    yield return enumerator.Current;
                }
                else
                {
                    yield break;
                }

                index++;
            }
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this List<TSource> source, Predicate<TSource> predicate)
        {
            return JoeyWhere(source, (x, index) => predicate(x));
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources,
            Func<TSource, int, bool> predicate)
        {
            var index = 0;
            var enumerator = sources.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current, index))
                {
                    yield return enumerator.Current;
                }

                index++;
            }
        }
    }
}