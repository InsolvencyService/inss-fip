using System.Linq;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Services;
using Xunit;

namespace INSS.FIP.FnApp.UnitTests.ValidationTests;

[Trait("Category", "IpSearchRequestModel Validation - Unit Tests")]
public class IpSearchRequestModelValidationTests
{
    [Theory]
    [InlineData(1, null, null, null, null, null)]
    [InlineData(null, "first", null, null, null, null)]
    [InlineData(null, null, "last", null, null, null)]
    [InlineData(null, null, null, "company", null, null)]
    [InlineData(null, null, null, null, "town", null)]
    [InlineData(null, null, null, null, null, "county")]
    [InlineData(1, "first", "last", "company", "town", "county")]
    public void IpSearchRequestModelWithValidModelReturnsSuccess(int? ipNumber, string? firstName, string? lastName, string? company, string? town, string? county)
    {
        // Arrange
        var ipSearchRequestModel = new IpSearchRequestModel
        {
            IpNumber = ipNumber,
            FirstName = firstName,
            LastName = lastName,
            Company = company,
            Town = town,
            County = county
        };

        // Act
        var result = ValidationHelpers.ValidateModel(ipSearchRequestModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Theory]
    [InlineData(null, null, null, null, null, null)]
    [InlineData(0, null, null, null, null, null)]
    public void IpSearchRequestModelWithInvalidModelReturnsFailue(int? ipNumber, string? firstName, string? lastName, string? company, string? town, string? county)
    {
        // Arrange
        var ipSearchRequestModel = new IpSearchRequestModel
        {
            IpNumber = ipNumber,
            FirstName = firstName,
            LastName = lastName,
            Company = company,
            Town = town,
            County = county
        };


    // Act
    var result = ValidationHelpers.ValidateModel(ipSearchRequestModel);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
