
using System.Collections.Generic;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Models.CentrallyManagedPartyModels;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.CMPSyncTests
{
    public partial class BankruptcyCreditorsDataSourceProviderTests
    {
        public static IEnumerable<object[]> GetCMPTransformationTestData()
        {
            yield return new object[] {
                new List<CentrallyManagedPartyModel> {
                    new CentrallyManagedPartyModel { SourceRef = "123", Name = "John's B&B",
                        AddressLine1 = "40 Whiteley Croft Rise", AddressLine2 = null, AddressLine3 = null, Town = "Otley",  County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" },
                    new CentrallyManagedPartyModel { SourceRef = "765", Name = "Alphabetically ordered results by name",
                        AddressLine1 = "Chevin Bank", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = null, Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"},
                    new CentrallyManagedPartyModel { SourceRef = "456", Name = "John was here",
                        AddressLine1 = "Chevin Bank", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = null, Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"}
                },
                new List<BankruptcyCreditorsList> {
                    new BankruptcyCreditorsList { Id = 1, SourceRef = "765", Name = "Alphabetically ordered results by name",
                        AddressLine1 = "", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"  },
                    new BankruptcyCreditorsList { Id = 2, SourceRef = "456", Name = "John was here",
                        AddressLine1 = "", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"  },
                    new BankruptcyCreditorsList { Id = 3, SourceRef = "123", Name = "John's B&B",
                        AddressLine1 = "", AddressLine2 = "", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" }
                }
            };
        }


        public static IEnumerable<object[]> GetCMPTransformation_Sort_TestData()
        {
            yield return new object[] {
                new List<CentrallyManagedPartyModel> {
                    new CentrallyManagedPartyModel { SourceRef = "123", Name = "John's B&B",
                        AddressLine1 = "40 Whiteley Croft Rise", AddressLine2 = null, AddressLine3 = null, Town = "Otley",  County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" },
                    new CentrallyManagedPartyModel { SourceRef = "765", Name = "Alphabetically ordered results by name",
                        AddressLine1 = "Chevin Bank", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = null, Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"}
                },
                new List<BankruptcyCreditorsList> {
                    new BankruptcyCreditorsList { Id = 1, SourceRef = "765", Name = "Alphabetically ordered results by name",
                        AddressLine1 = "", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"  },
                    new BankruptcyCreditorsList { Id = 2, SourceRef = "123", Name = "John's B&B",
                        AddressLine1 = "", AddressLine2 = "", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" }
                }
            };
        }

        public static IEnumerable<object[]> GetCMPTransformation_RemoveDuplicatesByNameAndSourceRef_TestData()
        {
            yield return new object[] {
                new List<CentrallyManagedPartyModel> {
                    new CentrallyManagedPartyModel { SourceRef = "765", Name = "Alphabetically ordered results by name",
                        AddressLine1 = "Chevin Bank", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = null, Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"},
                    //Following record should not be present in results as it has same name and source ref as above record, even though address details differ, as per findings from https://inssdigital.atlassian.net/wiki/x/BwByFwE
                    new CentrallyManagedPartyModel { SourceRef = "765", Name = "Alphabetically ordered results by name",
                        AddressLine1 = "A Building", AddressLine2 = "2 City Walk", AddressLine3 = null, Town = "Leeds", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LS1 1HH"},
                    new CentrallyManagedPartyModel { SourceRef = "123", Name = "John's B&B",
                        AddressLine1 = "40 Whiteley Croft Rise", AddressLine2 = null, AddressLine3 = null, Town = "Otley",  County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" },
                    //Following record will be present in results, as SourceRef differs from above record 
                    new CentrallyManagedPartyModel { SourceRef = "456", Name = "John's B&B",
                        AddressLine1 = "40 Whiteley Croft Rise", AddressLine2 = null, AddressLine3 = null, Town = "Otley",  County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" },
                    //Following record will be present in results, as Name differs from above record
                    new CentrallyManagedPartyModel { SourceRef = "456", Name = "Johns B&B",
                        AddressLine1 = "40 Whiteley Croft Rise", AddressLine2 = null, AddressLine3 = null, Town = "Otley",  County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" }
                },
                new List<BankruptcyCreditorsList> {
                    new BankruptcyCreditorsList { Id = 1, SourceRef = "765", Name = "Alphabetically ordered results by name",
                        AddressLine1 = "", AddressLine2 = "45 Whiteley Croft Rise", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR"  },
                    new BankruptcyCreditorsList { Id = 2, SourceRef = "123", Name = "John's B&B",
                        AddressLine1 = "", AddressLine2 = "", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" },
                    new BankruptcyCreditorsList { Id = 3, SourceRef = "456", Name = "John's B&B",
                        AddressLine1 = "", AddressLine2 = "", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" },
                    new BankruptcyCreditorsList { Id = 4, SourceRef = "456", Name = "Johns B&B",
                        AddressLine1 = "", AddressLine2 = "", AddressLine3 = "", Town = "Otley", County = "West Yorkshire", Country = "United Kingdom", PostCode = "LE21 3NR" }
                }
            };
        }




    }
}
