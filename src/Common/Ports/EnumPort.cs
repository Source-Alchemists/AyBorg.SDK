namespace Atomy.SDK.Common.Ports;

public sealed class EnumPort : ValuePortGeneric<EnumPort, Enum>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public EnumPort(string name, PortDirection direction, Enum value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumPort"/> class.
    /// </summary>
    public EnumPort(EnumPort port) : base(port)
    {
        Value = port.Value;
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.Enum;
}