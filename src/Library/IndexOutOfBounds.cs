namespace OneOf.Linq
{
    /// <summary>
    /// Unit type for <see cref="OneOfExtensions"/>.
    /// Signifies that an index was requested that is outside of the valid range.
    /// </summary>
    public struct IndexOutOfBounds : IReasonWhyNot
    {
    }
}