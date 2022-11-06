namespace Autodroid.SDK.Common.Ports;

public sealed class NumericPort : ValuePortGeneric<NumericPort, double>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NumericPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    /// <param name="min">The minimum.</param>
    /// <param name="max">The maximum.</param>
    public NumericPort(string name, PortDirection direction, double value, double min = double.MinValue, double max = double.MaxValue) : base(name, direction, value)
    {
        Min = min;
        Max = max;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NumericPort"/> class.
    /// </summary>
    public NumericPort(NumericPort port) : base(port)
    {
        Value = port.Value;
        Min = port.Min;
        Max = port.Max;
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.Numeric;

    /// <summary>
    /// Determines the maximum of the parameters.
    /// </summary>
    public double Max { get; }

    /// <summary>
    /// Determines the minimum of the parameters.
    /// </summary>
    public double Min { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public T GetValue<T>()
    {
        return (T)Convert.ChangeType(Value, typeof(T));
    }
}