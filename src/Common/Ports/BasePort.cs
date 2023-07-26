namespace AyBorg.SDK.Common.Ports;

public abstract class BasePort<TPort> : IPort where TPort : class, IPort
{
    protected PortLink? _link;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePort{TPort}"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The direction.</param>
    protected BasePort(string name, PortDirection direction) {
        Name = name;
        Direction = direction;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePort"/> class.
    /// </summary>
    protected BasePort(BasePort<TPort> port)
    {
        Id = port.Id;
        Name = port.Name;
        Direction = port.Direction;
        _link = port._link;
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public abstract PortBrand Brand { get; }

    /// <summary>
    /// Gets the direction.
    /// </summary>
    public PortDirection Direction { get; }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets a value indicating whether this instance is connected.
    /// </summary>
    public bool IsConnected => _link != null;

    /// <summary>
    /// Connects with the specified link.
    /// </summary>
    /// <param name="link">The port.</param>
    /// <exception cref="System.ArgumentNullException">port</exception>
    /// <exception cref="System.InvalidOperationException">Input ports can only be linked with output ports!</exception>
    public void Connect(PortLink link)
    {
        _link = link ?? throw new ArgumentNullException(nameof(link));
    }

    /// <summary>
    /// Disconnects this instance.
    /// </summary>
    public void Disconnect()
    {
        _link = null;
    }

    /// <summary>
    /// Updates the value.
    /// </summary>
    /// <param name="port">The port.</param>
    public abstract void UpdateValue(IPort port);

    /// <summary>
    /// Sets the identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    public void SetId(Guid id)
    {
        Id = id;
    }
}
