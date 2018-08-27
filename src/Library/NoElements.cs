namespace OneOf.Linq
{
    /// <summary>
    /// Unit type for <see cref="OneOfExtensions"/>.
    /// Signifies that at least 1 element was expected, but none were found.
    /// </summary>
    public struct NoElements : IReasonWhyNot
    {
    }
}