namespace OneOf.Linq
{
    /// <summary>
    /// Unit type for <see cref="OneOfExtensions"/>.
    /// Signifies that only 1 element was expected, but more than one was found.
    /// </summary>
    public struct MoreThanOneElement : IReasonWhyNot
    {
    }
}