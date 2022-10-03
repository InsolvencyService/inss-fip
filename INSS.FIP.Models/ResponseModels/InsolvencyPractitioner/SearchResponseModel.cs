using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels.InsolvencyPractitioner;

[ExcludeFromCodeCoverage]
public class SearchResponseModel
{
    public IList<FipApiSearchResultResponseModel> Payload { get; set; } = new List<FipApiSearchResultResponseModel>();
}
