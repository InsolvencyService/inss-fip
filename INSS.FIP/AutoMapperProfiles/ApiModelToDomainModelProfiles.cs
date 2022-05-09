using AutoMapper;
using INSS.FIP.ApiModels.Models.RequestModels;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.AutoMapperProfiles;

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
    }
}
