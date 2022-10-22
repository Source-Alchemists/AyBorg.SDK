using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Resizing.Operations;
using Atomy.SDK.Operations;

namespace Atomy.SDK.ImageProcessing.Resizing;

public class Resizer : Operation<ResizerDescription, ResizerParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(ResizerParameters parameters)
    {
        if(parameters.Input == null)
            throw new ArgumentNullException(nameof(parameters.Input));
        
        var description = Descriptions.Where(o => o.GetType() == typeof(ResizerDescription)
                                                && o.InputType == parameters.Input.GetType()).FirstOrDefault();

        if (description == null)
            throw new InvalidOperationException($"No resizer found for {parameters.Input.GetType()}.");
        
        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}