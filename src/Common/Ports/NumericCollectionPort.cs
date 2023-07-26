using System.Collections.Immutable;

namespace AyBorg.SDK.Common.Ports;

public class NumericCollectionPort : ValuePortGeneric<NumericCollectionPort, ImmutableList<double>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NumericCollectionPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    public NumericCollectionPort(string name, PortDirection direction) : base(name, direction)
    {
        Value = ImmutableList<double>.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NumericCollectionPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public NumericCollectionPort(string name, PortDirection direction, ImmutableList<double> value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NumericCollectionPort"/> class.
    /// </summary>
    /// <param name="other">The other.</param>
    public NumericCollectionPort(NumericCollectionPort other) : base(other)
    {
        Value = ImmutableList<double>.Empty.AddRange(other.Value);
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.NumericCollection;

    /// <summary>
    /// Updates the value.
    /// </summary>
    /// <param name="port">The port.</param>
    public override void UpdateValue(IPort port) {
        var sourcePort = (NumericCollectionPort)port;
        Value = sourcePort.Value;
    }
}
