using AutoMapper;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.AutoMapperProfiles;

[ExcludeFromCodeCoverage]
public class DomainModelToViewModelProfiles : Profile
{
    public DomainModelToViewModelProfiles()
    {
        CreateMap<WebMessageDomainModel, SpecialMessageViewModel>()
            .ForMember(d => d.ShowMessage, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.Message)));

        CreateMap<AuthBodyDomainModel, AuthBodyViewModel>()
            .ForMember(d => d.Abbreviation, opt => opt.MapFrom(s => s.AuthBodyCode))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AuthBodyName))
            .ForMember(d => d.Address, opt => opt.MapFrom(s => String.Join(", ", $"{s.AuthBodyAddressLine1}|{s.AuthBodyAddressLine2}|{s.AuthBodyAddressLine3}|{s.AuthBodyAddressLine4}|{s.AuthBodyAddressLine5}|{s.AuthBodyPostcode}".Split('|', StringSplitOptions.RemoveEmptyEntries))))
            .ForMember(d => d.Telephone, opt => opt.MapFrom(s => s.AuthBodyPhone))
            .ForMember(d => d.Fax, opt => opt.MapFrom(s => s.AuthBodyFaxNo))
            .ForMember(d => d.Website, opt => opt.MapFrom(s => s.AuthBodyWebsite));

        CreateMap<SearchResultDomainModel, SearchResultViewModel>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.Title} {s.FirstNames} {s.LastName}".Trim()));

        CreateMap<InsolvencyPractitionerDomainModel, InsolvencyPractitionerViewModel>()
            .ForMember(d => d.Breadcrumbs, opt => opt.Ignore())
            .ForMember(d => d.Company, opt => opt.MapFrom(s => s.RegisteredFirmName))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.Title} {s.FirstNames} {s.LastName}".Trim()))
            .ForMember(d => d.Address, opt => opt.MapFrom(s => String.Join(", ", $"{s.RegisteredAddressLine1}|{s.RegisteredAddressLine2}|{s.RegisteredAddressLine3}|{s.RegisteredAddressLine4}|{s.RegisteredAddressLine5}|{s.RegisteredPostCode}".Split('|', StringSplitOptions.RemoveEmptyEntries))));
    }
}
