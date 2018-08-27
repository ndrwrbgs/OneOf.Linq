namespace OneOf.Linq
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public static partial class OneOfLinqExtensions
    {
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

            return default(NoElements);
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

            bool hasAny = false;
            TSource result = default(TSource);
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    result = element;
                    hasAny = true;
                }
            }

            if (!hasAny)
            {
                return default(NoElements);
            }

            return result;
        }
    }
}