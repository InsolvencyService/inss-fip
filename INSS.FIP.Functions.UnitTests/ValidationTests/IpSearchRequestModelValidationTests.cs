using System.Linq;
using INSS.FIP.Models.RequestModels.InsolvencyPractitioner;
using INSS.FIP.Services;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.ValidationTests;

[Trait("Category", "IpSearchRequestModel Validation - Unit Tests")]
public class IpSearchRequestModelValidationTests
{
    [Theory]
    [InlineData("first", null, null, null, null)]
    [InlineData(null, "last", null, null, null)]
    [InlineData(null, null, "company", null, null)]
    [InlineData(null, null, null, "town", null)]
    [InlineData(null, null, null, null, "NN1 1DB")]
    [InlineData("first", "last", "company", "town", "NN1 1DB")]
    public void IpSearchRequestModelWithValidModelReturnsSuccess(string? firstName, string? lastName, string? company, string? town, string? postcode)
    {
        // Arrange
        var ipSearchRequestModel = new IpSearchRequestModel
        {
            FirstName = firstName,
            LastName = lastName,
            Company = company,
            Town = town,
            Postcode = postcode
        };

        // Act
        var result = ValidationHelpers.ValidateModel(ipSearchRequestModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Theory]
    [InlineData(null, null, null, null, null)]
    public void IpSearchRequestModelWithInvalidModelReturnsFailure(string? firstName, string? lastName, string? company, string? town, string? postcode)
    {
        // Arrange
        var ipSearchRequestModel = new IpSearchRequestModel
        {
            FirstName = firstName,
            LastName = lastName,
            Company = company,
            Town = town,
            Postcode = postcode
        };


        // Act
        var result = ValidationHelpers.ValidateModel(ipSearchRequestModel);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
