namespace OneOf.Linq
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [PublicAPI]
    public static partial class OneOfLinqExtensions
    {
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