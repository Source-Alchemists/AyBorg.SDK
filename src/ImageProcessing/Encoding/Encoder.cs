using Atomy.SDK.ImageProcessing.Encoding.Operations;
using Atomy.SDK.ImageProcessing.Operations;

namespace Atomy.SDK.ImageProcessing.Encoding;

public class Encoder : Operation<EncoderDescription, EncoderParameters, object>
{
    public override object Execute(EncoderParameters parameters)
    {
        var description = Descriptions.Where(o => o.GetType() == typeof(EncoderDescription) 
                                            && o.InputType == parameters.Input!.GetType()).FirstOrDefault();
        if (description == null)
            throw new InvalidOperationException($"No encoder found for {parameters.Input!.GetType()}.");
        
        description.Operation!.DynamicInvoke(parameters);
        return null!;
    }
}