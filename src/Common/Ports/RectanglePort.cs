using ImageTorque;

namespace AyBorg.SDK.Common.Ports;

public sealed class RectanglePort : ValuePortGeneric<RectanglePort, Rectangle>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RectanglePort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public RectanglePort(string name, PortDirection direction, Rectangle value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RectanglePort"/> class.
    /// </summary>
    /// <param name="port">The port to copy.</param>
    public RectanglePort(RectanglePort port) : base(port)
    {
        Value = new Rectangle(port.Value);
    }

    /// <summary>
    /// Gets the port type.
    /// </summary>
    public override PortBrand Brand => PortBrand.Rectangle;
}
