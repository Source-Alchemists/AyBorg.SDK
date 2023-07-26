namespace AyBorg.SDK.Common.Ports;

public interface IPort
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    PortBrand Brand { get; }

    /// <summary>
    /// Gets the direction.
    /// </summary>
    PortDirection Direction { get; }

    /// <summary>
    /// Gets a value indicating whether this instance is connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Connects with the specified link.
    /// </summary>
    /// <param name="link">The link.</param>
    void Connect(PortLink link);

    /// <summary>
    /// Disconnects this instance.
    /// </summary>
    void Disconnect();

    /// <summary>
    /// Updates the value.
    /// </summary>
    /// <param name="port">The port.</param>
    void UpdateValue(IPort port);

    /// <summary>
    /// Sets the identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    void SetId(Guid id);
}
