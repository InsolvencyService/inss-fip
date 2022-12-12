using System.Linq;
using INSS.FIP.Services;
using INSS.FIP.Web.ViewModels;
using Xunit;

namespace INSS.FIP.Web.UnitTests.ValidationTests;

[Trait("Category", "SearchParametersViewModel Validation - Unit Tests")]
public class SearchParametersViewModelValidationTests
{
    [Theory]
    [InlineData("first", null, null, null, null)]
    [InlineData(null, "last", null, null, null)]
    [InlineData(null, null, "company", null, null)]
    [InlineData(null, null, null, "town", null)]
    [InlineData(null, null, null, null, "NN1 1DB")]
    [InlineData("first", "last", "company", "town", "NN1 1DB")]
    public void SearchParametersViewModelWithValidModelReturnsSuccess(string? firstName, string? lastName, string? company, string? town, string? postcode)
    {
        // Arrange
        var SearchParametersViewModel = new SearchParametersViewModel
        {
            FirstName = firstName,
            LastName = lastName,
            Company = company,
            Town = town,
            Postcode = postcode
        };

        // Act
        var result = ValidationHelpers.ValidateModel(SearchParametersViewModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Theory]
    [InlineData(null, null, null, null, null)]
    public void SearchParametersViewModelWithInvalidModelReturnsFailue(string? firstName, string? lastName, string? company, string? town, string? postcode)
    {
        // Arrange
        var SearchParametersViewModel = new SearchParametersViewModel
        {
            FirstName = firstName,
            LastName = lastName,
            Company = company,
            Town = town,
            Postcode = postcode
        };


        // Act
        var result = ValidationHelpers.ValidateModel(SearchParametersViewModel);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
