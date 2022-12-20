using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.Models.RequestModels;
using INSS.FIP.Models.ResponseModels;
using INSS.FIP.Web.ViewModels;

namespace INSS.FIP.Web.AutoMapperProfiles;

[ExcludeFromCodeCoverage]
public class ApiModelToDomainModelProfiles : Profile
{
    public ApiModelToDomainModelProfiles()
    {
        CreateMap<FipApiAuthBodyResponseModel, AuthBodyDomainModel>();

        CreateMap<FipApiWebMessageResponseModel, WebMessageDomainModel>();

        CreateMap<FipApiInsolvencyPractitionerResponseModel, InsolvencyPractitionerDomainModel>();

        CreateMap<FipApiSearchResultResponseModel, SearchResultDomainModel>();

        CreateMap<SearchParametersViewModel, FipApiSearchRequestModel>();

        CreateMap<FipApiInsolvencyPractitionerWithAuthResponseModel, InsolvencyPractitionerWithAuthBodyDomainModel>()
            .ForMember(m => m.InsolvencyPractitioner, opt => opt.MapFrom(rm => rm.IP))
            .ForMember(m => m.AuthBodyDomainModel, opt => opt.MapFrom(rm => rm.AuthorisingBody));
    }
}