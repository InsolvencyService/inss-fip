using INSS.FIP.ApiModels.Models.ResponseModels;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Models.ResponseModels.WebMessage;

[ExcludeFromCodeCoverage]
public class WebMessageResponseModel 
{
    public IList<FipApiWebMessageResponseModel> Payload { get; set; } = new List<FipApiWebMessageResponseModel>();
}
