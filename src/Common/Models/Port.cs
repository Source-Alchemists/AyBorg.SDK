using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common.Models;

public sealed record Port : IDisposable
{
    private bool _disposedValue;

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the direction.
    /// </summary>
    public PortDirection Direction { get; init; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public object? Value { get; init; }

    /// <summary>
    /// Gets or sets the brand.
    /// </summary>
    public PortBrand Brand { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is connected.
    /// </summary>
    public bool IsConnected { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is link convertable.
    /// </summary>
    public bool IsLinkConvertable { get; init; }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue && disposing)
        {
            if (Value is IDisposable disposableObject)
            {
                disposableObject.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
