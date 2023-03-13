using System.Collections.ObjectModel;

namespace AyBorg.SDK.Common.Ports;

public class StringCollectionPort : ValuePortGeneric<StringCollectionPort, ReadOnlyCollection<string>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringCollectionPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public StringCollectionPort(string name, PortDirection direction, ReadOnlyCollection<string> value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringCollectionPort"/> class.
    /// </summary>
    /// <param name="other">The other.</param>
    public StringCollectionPort(StringCollectionPort other) : base(other)
    {
        Value = new ReadOnlyCollection<string>(other.Value);
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.StringCollection;
}
