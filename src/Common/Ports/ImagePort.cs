using ImageTorque;

namespace AyBorg.SDK.Common.Ports;

public sealed class ImagePort : ValuePortGeneric<ImagePort, Image>, IDisposable
{
    private bool _disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImagePort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public ImagePort(string name, PortDirection direction, Image value = default!) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImagePort"/> class.
    /// </summary>
    public ImagePort(ImagePort port) : base(port)
    {
        Value = new Image(port.Value);
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.Image;

    /// <summary>
    /// Updates the value.
    /// </summary>
    /// <param name="port">The port.</param>
    public override void UpdateValue(IPort port) {
        var sourcePort = (ImagePort)port;
        Value = new Image(sourcePort.Value);
    }

    public void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Value?.Dispose();
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
