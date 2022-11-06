using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Binarizing.Operations;

public record BinarizerDescription : OperationDescription
{
    public BinarizerDescription() : base() { }
    public BinarizerDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}