using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Mirroring.Operations;
using Atomy.SDK.ImageProcessing.Operations;

namespace Atomy.SDK.ImageProcessing.Mirroring;

public class Mirror : Operation<MirrorDescription, MirrorParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(MirrorParameters parameters)
    {
        if (parameters.Input == null)
            throw new ArgumentNullException(nameof(parameters.Input));

        var description = Descriptions.Where(o => o.GetType() == typeof(MirrorDescription)
                                                && o.InputType == parameters.Input.GetType()).FirstOrDefault();

        if (description == null)
            throw new InvalidOperationException($"No mirror found for {parameters.Input.GetType()}.");

        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}