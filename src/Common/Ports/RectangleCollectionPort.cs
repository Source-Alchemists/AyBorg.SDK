using System.Collections.ObjectModel;
using ImageTorque;

namespace AyBorg.SDK.Common.Ports;

public class RectangleCollectionPort : ValuePortGeneric<RectangleCollectionPort, ReadOnlyCollection<Rectangle>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RectangleCollectionPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public RectangleCollectionPort(string name, PortDirection direction, ReadOnlyCollection<Rectangle> value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RectangleCollectionPort"/> class.
    /// </summary>
    /// <param name="other">The other.</param>
    public RectangleCollectionPort(RectangleCollectionPort other) : base(other)
    {
        Value = new ReadOnlyCollection<Rectangle>(other.Value);
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.RectangleCollection;
}
