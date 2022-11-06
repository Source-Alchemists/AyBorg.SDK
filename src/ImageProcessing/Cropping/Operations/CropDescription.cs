using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Cropping.Operations;

public record CropDescription : OperationDescription
{
    public CropDescription() : base() { }
    public CropDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}