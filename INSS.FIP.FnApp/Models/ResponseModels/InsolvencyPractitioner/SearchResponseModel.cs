using INSS.FIP.ApiModels.Models.ResponseModels;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Models.ResponseModels.InsolvencyPractitioner;

[ExcludeFromCodeCoverage]
public class SearchResponseModel
{
    public IList<FipApiSearchResultResponseModel> Payload { get; set; } = new List<FipApiSearchResultResponseModel>();
}
