using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.RequestModels.GetWebPageBannerMessage;

[ExcludeFromCodeCoverage]
public class GetWebPageBannerMessageRequestModel
{
    [Required]
    public string ApplicationPrefix { get; set; }
}
