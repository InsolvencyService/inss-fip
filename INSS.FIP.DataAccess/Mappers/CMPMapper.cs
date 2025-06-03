using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.Models.CentrallyManagedParties.ResponseModels;

namespace INSS.FIP.DataAccess.Mappers;

public class CMPMapper : Profile
{
    public CMPMapper()
    {
        CreateMap<CentrallyManagedPartyModel, BankruptcyCreditorsList>();
    }
}
