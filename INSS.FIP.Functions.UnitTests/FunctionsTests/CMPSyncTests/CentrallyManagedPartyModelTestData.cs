

using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Models.CentrallyManagedPartyModels;
using System.Collections.Generic;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.CMPSyncTests
{
    public partial class CentrallyManagedPartyModelTests
    {


        public static IEnumerable<object[]> GetCMPModelTestData()
        {
            yield return new object[] {
                new CentrallyManagedPartyModel { SourceRef = "123", Name = "John's B&B",
                    AddressLine1 = "40 Whiteley Croft Rise", AddressLine2 = null, AddressLine3 = null, Town = "Otley",  County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" },
                new CentrallyManagedPartyModel { SourceRef = "123", Name = "John's B&B",
                    AddressLine1 = "", AddressLine2 = "", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" }
            };

            yield return new object[] {
                new CentrallyManagedPartyModel { SourceRef = "456", Name = "John was here",
                    AddressLine1 = "Chevin Bank", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = null, Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"},
                new CentrallyManagedPartyModel { SourceRef = "456", Name = "John was here",
                    AddressLine1 = "", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"  }
            };

            //All nulls
            yield return new object[] {
                new CentrallyManagedPartyModel { SourceRef = null, Name = null,
                    AddressLine1 = null, AddressLine2 = null, AddressLine3 = null, Town = null, County = null, Country = null, PostCode = null},
                new CentrallyManagedPartyModel { SourceRef = null, Name = "",
                    AddressLine1 = "", AddressLine2 = "", AddressLine3 = "", Town = "", County = "", Country = "", PostCode = ""  }
            };

            //SourceRef emptystring
            yield return new object[] {
                new CentrallyManagedPartyModel { SourceRef = "", Name = "John was here",
                    AddressLine1 = "Chevin Bank", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = null, Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"},
                new CentrallyManagedPartyModel { SourceRef = null, Name = "John was here",
                    AddressLine1 = "", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"  }
            };
        }
    }
}
