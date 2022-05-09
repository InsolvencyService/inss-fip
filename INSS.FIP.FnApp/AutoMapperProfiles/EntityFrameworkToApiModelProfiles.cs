using AutoMapper;
using INSS.FIP.ApiModels.Models.RequestModels;
using INSS.FIP.ApiModels.Models.ResponseModels;
using INSS.FIP.FnApp.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Repository.Models;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.AutoMapperProfiles;

[ExcludeFromCodeCoverage]
public class EntityFrameworkToApiModelProfiles : Profile
{
    public EntityFrameworkToApiModelProfiles()
    {
        CreateMap<FipApiSearchRequestModel, IpSearchRequestModel>();

        CreateMap<CiIpAuthorisingBody, FipApiAuthBodyResponseModel>();

        CreateMap<CiIp, FipApiSearchResultResponseModel>()
            .ForMember(d => d.IpNumber, opt => opt.MapFrom(s => s.IpNo))
            .ForMember(d => d.FirstNames, opt => opt.MapFrom(s => s.Forenames))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Surname))
            .ForMember(d => d.Company, opt => opt.MapFrom(s => s.RegisteredFirmName))
            .ForMember(d => d.Postcode, opt => opt.MapFrom(s => s.RegisteredPostCode));

        CreateMap<CiIp, FipApiInsolvencyPractitionerResponseModel>()
            .ForMember(d => d.IpNumber, opt => opt.MapFrom(s => s.IpNo))
            .ForMember(d => d.FirstNames, opt => opt.MapFrom(s => s.Forenames))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Surname))
            .ForMember(d => d.Telephone, opt => opt.MapFrom(s => s.RegisteredPhone))
            .ForMember(d => d.Fax, opt => opt.MapFrom(s => s.RegisteredFax))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.IpEmailAddress));

        CreateMap<WebMessage, FipApiWebMessageResponseModel>()
            .ForMember(d => d.HideSearch, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.HideSearch) && s.HideSearch.Equals("Y", StringComparison.OrdinalIgnoreCase)));
    }
}
