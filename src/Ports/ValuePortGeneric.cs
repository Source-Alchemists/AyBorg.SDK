namespace Atomy.SDK.Ports;

public abstract class ValuePortGeneric<TPort, TValue> : BasePort<TPort>, IPortGeneric<TValue> where TPort : class, IPort
{
    private TValue? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValuePortGeneric{TPort}"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The direction.</param>
    protected ValuePortGeneric(string name, PortDirection direction) : base(name, direction)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValuePortGeneric{TPort}"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The direction.</param>
    /// <param name="value">The value.</param>
    protected ValuePortGeneric(string name, PortDirection direction, TValue value) : base(name, direction)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValuePortGeneric{TPort}"/> class.
    /// </summary>
    protected ValuePortGeneric(ValuePortGeneric<TPort, TValue> port) : base(port)
    {
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public TValue Value
    {
        get
        {
            if (_link != null && _link.SourceId != Id)
            {
                // If the source port and the target port are not from the same brand, it will be checked if the value can be converted.
                // If not the original value will be returned.
                if (_link.Source.Brand != Brand)
                {
                    if (PortConverter.IsConvertable(_link.Source, this))
                    {
                        return PortConverter.Convert<TValue>(_link.Source, _value!);
                    }
                    return _value!;
                }
                var sourcePort = _link.GetSource<ValuePortGeneric<TPort, TValue>>();
                return sourcePort.Value;
            }
            return _value!;
        }
        set => _value = value;
    }

    /// <summary>
    /// Gets a value indicating whether this instance is link convertable.
    /// </summary>
    public bool IsLinkConvertable
    {
        get
        {
            if (!IsConnected) return false; // Not connected, so no conversion possible.
            if (_link!.Source.Brand == Brand) return true; // Same brand, so conversion is possible.
            return PortConverter.IsConvertable(_link.Source, this); // Check if conversion is possible.
        }
    }
}