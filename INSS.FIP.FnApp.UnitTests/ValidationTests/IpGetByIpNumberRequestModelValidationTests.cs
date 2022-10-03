using System.Linq;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Services;
using Xunit;

namespace INSS.FIP.FnApp.UnitTests.ValidationTests;

[Trait("Category", "IpGetByIpNumberRequestModel Validation - Unit Tests")]
public class IpGetByIpNumberRequestModelValidationTests
{
    [Fact]
    public void IpGetByIpNumberRequestModelWithValidModelReturnsSuccess()
    {
        // Arrange
        var IpGetByIpNumberRequestModel = new IpGetByIpNumberRequestModel
        {
            IpNumber =1,
        };

        // Act
        var result = ValidationHelpers.ValidateModel(IpGetByIpNumberRequestModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    public void IpGetByIpNumberRequestModelWithInvalidModelReturnsFailue(int? ipnumber)
    {
        // Arrange
        var IpGetByIpNumberRequestModel = new IpGetByIpNumberRequestModel
        {
            IpNumber = ipnumber,
        };

        // Act
        var result = ValidationHelpers.ValidateModel(IpGetByIpNumberRequestModel);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
