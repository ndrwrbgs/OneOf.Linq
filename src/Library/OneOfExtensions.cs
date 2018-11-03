namespace OneOf.Linq
{
    using System;

    /// <summary>
    /// Extensions for <see cref="OneOf{T0}"/> in relation to <see cref="IReasonWhyNot"/>, for expressive
    /// authoring of code that uses <see cref="OneOfLinqExtensions"/>.
    /// </summary>
    public static class OneOfExtensions
    {
        public static bool IsSuccess<T, TWhyNot>(
            this OneOf<T, TWhyNot> source)
            where TWhyNot : IReasonWhyNot
        {
            return source.IsT0;
        }

        public static T Value<T, TWhyNot>(
            this OneOf<T, TWhyNot> source)
            where TWhyNot : IReasonWhyNot
        {
            return source.AsT0;
        }

        public static TWhyNot WhyNot<T, TWhyNot>(
            this OneOf<T, TWhyNot> source)
            where TWhyNot : IReasonWhyNot
        {
            return source.AsT1;
        }

        public static bool IsSuccess<T, TWhyNot1, TWhyNot2>(
            this OneOf<T, TWhyNot1, TWhyNot2> source)
            where TWhyNot1 : IReasonWhyNot
            where TWhyNot2 : IReasonWhyNot
        {
            return source.IsT0;
        }

        public static T Value<T, TWhyNot1, TWhyNot2>(
            this OneOf<T, TWhyNot1, TWhyNot2> source)
            where TWhyNot1 : IReasonWhyNot
            where TWhyNot2 : IReasonWhyNot
        {
            return source.AsT0;
        }

        public static IReasonWhyNot WhyNot<T, TWhyNot1, TWhyNot2>(
            this OneOf<T, TWhyNot1, TWhyNot2> source)
            where TWhyNot1 : IReasonWhyNot
            where TWhyNot2 : IReasonWhyNot
        {
            return source
                .Match<IReasonWhyNot>(
                    _ => throw new InvalidOperationException(),
                    whyNot => whyNot,
                    whyNot => whyNot);
        }
    }
}