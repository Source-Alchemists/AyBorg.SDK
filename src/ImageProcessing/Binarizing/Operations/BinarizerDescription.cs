using Atomy.SDK.Operations;

namespace Atomy.SDK.ImageProcessing.Binarizing.Operations;

public record BinarizerDescription : OperationDescription
{
    public BinarizerDescription() : base() { }
    public BinarizerDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}