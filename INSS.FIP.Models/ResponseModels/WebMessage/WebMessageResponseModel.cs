using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels.WebMessage;

[ExcludeFromCodeCoverage]
public class WebMessageResponseModel 
{
    public IList<FipApiWebMessageResponseModel> Payload { get; set; } = new List<FipApiWebMessageResponseModel>();
}
