namespace OneOf.Linq
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public sealed class MoreThanOneElement
    {
    }

    public struct NoElements
    {
    }

    public struct IndexOutOfBounds
    {
    }

    [PublicAPI]
    public static class OneOfLinqExtensions
    {
        public static OneOf<TSource, IndexOutOfBounds> ElementAtOrReasonWhyNot<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int index)
        {
            // Mimicking the .NET BCL implementation
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (index >= 0)
            {
                if (source is IList<TSource> list)
                {
                    if (index < list.Count)
                    {
                        return list[index];
                    }
                }
                else
                {
                    using (IEnumerator<TSource> e = source.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!e.MoveNext())
                            {
                                break;
                            }

                            if (index == 0)
                            {
                                return e.Current;
                            }

                            index--;
                        }
                    }
                }
            }

            return default(IndexOutOfBounds);
        }

        public static OneOf<TSource, NoElements, MoreThanOneElement> SingleOrReasonWhyNot<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            // Mimicking the .NET BCL implementation
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is IList<TSource> list)
            {
                switch (list.Count)
                {
                    case 0: return default(NoElements);
                    case 1: return list[0];
                }
            }
            else
            {
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (!e.MoveNext())
                    {
                        return default(NoElements);
                    }

                    TSource result = e.Current;
                    if (!e.MoveNext())
                    {
                        return result;
                    }
                }
            }

            return default(MoreThanOneElement);
        }

        public static OneOf<TSource, NoElements, MoreThanOneElement> SingleOrReasonWhyNot<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate)
        {
            // Mimicking the .NET BCL implementation
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            TSource result = default(TSource);
            long count = 0;
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    result = element;
                    checked
                    {
                        count++;
                    }
                }
            }

            switch (count)
            {
                case 0: return default(NoElements);
                case 1: return result;
            }

            return default(MoreThanOneElement);
        }

        public static OneOf<TSource, NoElements> LastOrReasonWhyNot<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            // Mimicking the .NET BCL implementation
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is IList<TSource> list)
            {
                int count = list.Count;
                if (count > 0)
                {
                    return list[count - 1];
                }
            }
            else
            {
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (e.MoveNext())
                    {
                        TSource result;
                        do
                        {
                            result = e.Current;
                        } while (e.MoveNext());

                        return result;
                    }
                }
            }

            return default(TSource);
        }

        public static OneOf<TSource, NoElements> LastOrReasonWhyNot<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate)
        {
            // Mimicking the .NET BCL implementation 
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            TSource result = default(TSource);
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    result = element;
                }
            }

            return result;
        }

        public static OneOf<TSource, NoElements> FirstOrReasonWhyNot<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            // Mimicking the .NET BCL implementation 
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is IList<TSource> list)
            {
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            else
            {
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (e.MoveNext())
                    {
                        return e.Current;
                    }
                }
            }

            return default(NoElements);
        }

        public static OneOf<TSource, NoElements> FirstOrReasonWhyNot<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate)
        {
            // Mimicking the .NET BCL implementation 
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    return element;
                }
            }

            return default(NoElements);
        }
    }
}