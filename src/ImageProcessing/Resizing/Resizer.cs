using AyBorg.SDK.ImageProcessing.Buffers;
using AyBorg.SDK.ImageProcessing.Resizing.Operations;
using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Resizing;

public class Resizer : Operation<ResizerDescription, ResizerParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(ResizerParameters parameters)
    {   
        var description = Descriptions.Where(o => o.GetType() == typeof(ResizerDescription)
                                                && o.InputType == parameters.Input!.GetType()).FirstOrDefault();

        if (description == null)
            throw new InvalidOperationException($"No resizer found for {parameters.Input!.GetType()}.");
        
        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}