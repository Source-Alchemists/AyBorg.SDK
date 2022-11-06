using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Converting.Operations;

public record GrayscaleConverterDescription : OperationDescription
{
    public GrayscaleConverterDescription() : base() { }
    public GrayscaleConverterDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}