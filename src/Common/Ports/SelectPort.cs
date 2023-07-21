namespace AyBorg.SDK.Common.Ports;

public sealed class SelectPort : ValuePortGeneric<SelectPort, SelectPort.ValueContainer>
{

    /// <summary>
    /// Gets the port type.
    /// </summary>
    public override PortBrand Brand => PortBrand.Select;

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public SelectPort(string name, PortDirection direction, ValueContainer value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectPort"/> class.
    /// </summary>
    /// <param name="port">The port to copy.</param>
    public SelectPort(SelectPort other) : base(other)
    {
        Value = other.Value with { };
    }

    /// <summary>
    /// Updates the value.
    /// </summary>
    public override void UpdateValue(IPort port)
    {
        var sourcePort = (SelectPort)port;
        Value = sourcePort.Value;
    }

    /// <summary>
    /// Container for the value.
    /// </summary>
    public record ValueContainer(string SelectedValue, IReadOnlyCollection<string> Values);
}
