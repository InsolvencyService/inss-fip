using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Models.RequestModels;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Models.ResponseModels;

namespace INSS.FIP.Functions.AutoMapperProfiles;

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
            .ForMember(d => d.Town, opt => opt.MapFrom(s => s.RegisteredAddressLine4))
            .ForMember(d => d.Postcode, opt => opt.MapFrom(s => s.RegisteredPostCode));

        CreateMap<CiIp, FipApiInsolvencyPractitionerResponseModel>()
            .ForMember(d => d.IpNumber, opt => opt.MapFrom(s => s.IpNo))
            .ForMember(d => d.FirstNames, opt => opt.MapFrom(s => s.Forenames))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Surname))
            .ForMember(d => d.Telephone, opt => opt.MapFrom(s => s.RegisteredPhone))
            .ForMember(d => d.Fax, opt => opt.MapFrom(s => s.RegisteredFax))
            .ForMember(d => d.RegisteredAddressLine4, opt => opt.MapFrom(s => s.RegisteredAddressLine4))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.IpEmailAddress));

        CreateMap<WebMessage, FipApiWebMessageResponseModel>()
            .ForMember(d => d.HideSearch, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.HideSearch) && s.HideSearch.Equals("Y", StringComparison.OrdinalIgnoreCase)));
    }
}
