using System.Collections.Immutable;

namespace AyBorg.SDK.Common.Ports;

public class StringCollectionPort : ValuePortGeneric<StringCollectionPort, ImmutableList<string>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringCollectionPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    public StringCollectionPort(string name, PortDirection direction) : base(name, direction)
    {
        Value = ImmutableList<string>.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringCollectionPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public StringCollectionPort(string name, PortDirection direction, ImmutableList<string> value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringCollectionPort"/> class.
    /// </summary>
    /// <param name="other">The other.</param>
    public StringCollectionPort(StringCollectionPort other) : base(other)
    {
        Value = ImmutableList<string>.Empty.AddRange(other.Value);
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.StringCollection;
}
