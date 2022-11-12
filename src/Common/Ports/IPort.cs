namespace AyBorg.SDK.Common.Ports;

public interface IPort
{
    /// <summary>
    /// Gets the brand.
    /// </summary>
    /// <value>
    /// The brand.
    /// </value>
    PortBrand Brand { get; }

    /// <summary>
    /// Gets the direction.
    /// </summary>
    /// <value>
    /// The direction.
    /// </value>
    PortDirection Direction { get; }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    Guid Id { get; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    string Name { get; }

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
    /// Sets the identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    void SetId(Guid id);
}
