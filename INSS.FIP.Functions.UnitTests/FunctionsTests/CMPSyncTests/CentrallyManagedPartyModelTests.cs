using INSS.FIP.Models.CentrallyManagedPartyModels;
using System.Collections.Generic;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.CMPSyncTests
{
    public partial class CentrallyManagedPartyModelTests
    {
        //Checks on the handling of null values in the CMP model, with exception of SourceRef, any property should return "" if it is set to null 
        //In accordance with the findings from https://inssdigital.atlassian.net/wiki/x/BwByFwE
        //If SourceRef is null, this should be returned as null, in the target database this is required - so likely cause a rollback - which is handy for integration tests
        [Theory]
        [MemberData(nameof(GetCMPModelTestData))]
        public void Except_SourceRef_and_CMPModelProperties_should_return_emptystring_when_set_to_null(CentrallyManagedPartyModel input, CentrallyManagedPartyModel expected)
        {
            //Arrange
            //Act
            //Assert 
            Assert.Equal(expected.Name, input.Name);
            Assert.Equal(expected.AddressLine1, input.AddressLine1);
            Assert.Equal(expected.AddressLine2, input.AddressLine2);
            Assert.Equal(expected.AddressLine3, input.AddressLine3);
            Assert.Equal(expected.Town, input.Town);
            Assert.Equal(expected.County, input.County);
            Assert.Equal(expected.Country, input.Country);
            Assert.Equal(expected.PostCode, input.PostCode);
        }

        [Theory]
        [MemberData(nameof(GetCMPModelTestData))]
        public void AddressLine1_should_always_return_an_emptystring(CentrallyManagedPartyModel input)
        {
            //Arrange
            //Act
            //Assert 
            Assert.Equal("",input.AddressLine1);
        }

        [Theory]
        [MemberData(nameof(GetCMPModelTestData))]
        public void SourceRef_should_return_nonemptystrings_or_null(CentrallyManagedPartyModel input, CentrallyManagedPartyModel expected)
        {
            //Arrange
            //Act
            //Assert 
            Assert.Equal(expected.SourceRef,input.SourceRef);
        }

    }
}
