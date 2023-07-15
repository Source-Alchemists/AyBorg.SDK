using System.Collections.Immutable;
using ImageTorque;

namespace AyBorg.SDK.Common.Ports;

public class RectangleCollectionPort : ValuePortGeneric<RectangleCollectionPort, ImmutableList<Rectangle>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RectangleCollectionPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    public RectangleCollectionPort(string name, PortDirection direction) : base(name, direction)
    {
        Value = ImmutableList<Rectangle>.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RectangleCollectionPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public RectangleCollectionPort(string name, PortDirection direction, ImmutableList<Rectangle> value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RectangleCollectionPort"/> class.
    /// </summary>
    /// <param name="other">The other.</param>
    public RectangleCollectionPort(RectangleCollectionPort other) : base(other)
    {
        Value =  ImmutableList<Rectangle>.Empty.AddRange(other.Value);
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.RectangleCollection;

    /// <summary>
    /// Updates the value.
    /// </summary>
    /// <param name="port">The port.</param>
    public override void UpdateValue(IPort port) {
        var sourcePort = (RectangleCollectionPort)port;
        Value = sourcePort.Value;
    }
}
