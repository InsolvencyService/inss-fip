using AutoMapper;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Models.CentrallyManagedPartyModels;

namespace INSS.FIP.DataAccess.Mappers;

public class CMPMapper : Profile
{
    public CMPMapper()
    {
        CreateMap<CentrallyManagedPartyModel, BankruptcyCreditorsList>();
    }
}
