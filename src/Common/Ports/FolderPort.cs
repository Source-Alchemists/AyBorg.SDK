namespace Autodroid.SDK.Common.Ports;

public sealed class FolderPort : StringPort
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FolderPort"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="direction">The port direction.</param>
    /// <param name="value">The value.</param>
    public FolderPort(string name, PortDirection direction, string value) : base(name, direction, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FolderPort"/> class.
    /// </summary>
    public FolderPort(FolderPort port) : base(port)
    {
    }

    /// <summary>
    /// Gets the brand.
    /// </summary>
    public override PortBrand Brand => PortBrand.Folder;
}