using System.Linq;
using INSS.FIP.Services;
using INSS.FIP.Web.ViewModels;
using Xunit;

namespace INSS.FIP.UnitTests.UnitTests.ValidationTests;

[Trait("Category", "SearchParametersViewModel Validation - Unit Tests")]
public class SearchParametersViewModelValidationTests
{
    [Theory]
    [InlineData(1, null, null, null, null, null)]
    [InlineData(null, "first", null, null, null, null)]
    [InlineData(null, null, "last", null, null, null)]
    [InlineData(null, null, null, "company", null, null)]
    [InlineData(null, null, null, null, "town", null)]
    [InlineData(null, null, null, null, null, "county")]
    [InlineData(1, "first", "last", "company", "town", "county")]
    public void SearchParametersViewModelWithValidModelReturnsSuccess(int? ipNumber, string? firstName, string? lastName, string? company, string? town, string? county)
    {
        // Arrange
        var SearchParametersViewModel = new SearchParametersViewModel
        {
            IpNumber = ipNumber,
            FirstName = firstName,
            LastName = lastName,
            Company = company,
            Town = town,
            County = county
        };

        // Act
        var result = ValidationHelpers.ValidateModel(SearchParametersViewModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Theory]
    [InlineData(null, null, null, null, null, null)]
    [InlineData(0, null, null, null, null, null)]
    public void SearchParametersViewModelWithInvalidModelReturnsFailue(int? ipNumber, string? firstName, string? lastName, string? company, string? town, string? county)
    {
        // Arrange
        var SearchParametersViewModel = new SearchParametersViewModel
        {
            IpNumber = ipNumber,
            FirstName = firstName,
            LastName = lastName,
            Company = company,
            Town = town,
            County = county
        };


        // Act
        var result = ValidationHelpers.ValidateModel(SearchParametersViewModel);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
