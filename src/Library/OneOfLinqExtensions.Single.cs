namespace OneOf.Linq
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public static partial class OneOfLinqExtensions
    {
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
    }
}