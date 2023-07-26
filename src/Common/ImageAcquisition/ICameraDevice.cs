using ImageTorque;

namespace AyBorg.SDK.Common.ImageAcquisition;

public interface ICameraDevice : IDevice
{
    ValueTask<ImageContainer> AcquisitionAsync(CancellationToken cancellationToken);
}
