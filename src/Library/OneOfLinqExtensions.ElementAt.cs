namespace OneOf.Linq
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Overrides for <see cref="System.Linq.Enumerable"/> static methods that are ambiguous in their return
    /// values - e.g. <see cref="System.Linq.Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource})"/>
    /// when the first value is <see langword="null" />
    /// </summary>
    public static partial class OneOfLinqExtensions
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
    }
}