using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.DomainModels;

[ExcludeFromCodeCoverage]
public class GetWebPageBannerMessageDomainModel
{
    public bool HideSearch { get; set; }

    public string Message { get; set; }
}
