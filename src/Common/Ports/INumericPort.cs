namespace AyBorg.SDK.Common.Ports;

public interface INumericPort<TValue> : IPortGeneric<TValue>
{
    /// <summary>
    /// Determines the maximum of the parameters.
    /// </summary>
    /// <value>
    /// The maximum.
    /// </value>
    TValue Max { get; }

    /// <summary>
    /// Determines the minimum of the parameters.
    /// </summary>
    /// <value>
    /// The minimum.
    /// </value>
    TValue Min { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    TCast GetValue<TCast>();
}
