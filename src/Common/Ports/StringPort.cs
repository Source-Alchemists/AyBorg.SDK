namespace AyBorg.SDK.Common.Ports;

public class StringPort : ValuePortGeneric<StringPort, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public StringPort(string name, PortDirection direction, string value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringPort"/> class.
    /// </summary>
    public StringPort(StringPort port) : base(port)
    {
        Value = new string(port.Value);
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.String;
}