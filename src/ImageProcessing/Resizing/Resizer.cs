using Autodroid.SDK.ImageProcessing.Buffers;
using Autodroid.SDK.ImageProcessing.Resizing.Operations;
using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Resizing;

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