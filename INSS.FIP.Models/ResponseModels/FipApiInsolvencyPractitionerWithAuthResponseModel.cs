using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels
{
    [ExcludeFromCodeCoverage]
    public class FipApiInsolvencyPractitionerWithAuthResponseModel
    {
        public FipApiInsolvencyPractitionerResponseModel IP { get; set; }
        public FipApiAuthBodyResponseModel AuthorisingBody { get; set; }
    }
}
