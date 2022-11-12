using AyBorg.SDK.ImageProcessing.Binarizing.Operations;
using AyBorg.SDK.ImageProcessing.Buffers;
using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Binarizing;

public class Binarizer : Operation<BinarizerDescription, BinarizerParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(BinarizerParameters parameters)
    {
        var description = Descriptions.Where(o => o.GetType() == typeof(BinarizerDescription)
                                            && o.InputType == parameters.Input!.GetType()).FirstOrDefault();
        if (description == null)
            throw new InvalidOperationException($"No binarizer found for {parameters.Input!.GetType()}.");

        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}