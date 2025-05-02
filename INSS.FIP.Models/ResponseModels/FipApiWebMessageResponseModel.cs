using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels;

[ExcludeFromCodeCoverage]
public class FipApiGetWebPageBannerMessageResponseModel
{
    public bool HideSearch { get; set; }

    public string Message { get; set; }
}
