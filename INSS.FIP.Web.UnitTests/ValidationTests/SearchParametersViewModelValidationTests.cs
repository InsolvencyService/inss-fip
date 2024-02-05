using INSS.FIP.Services;
using INSS.FIP.Web.ViewModels;
using System.Linq;
using Xunit;
namespace INSS.FIP.Web.UnitTests.ValidationTests;

[Trait("Category", "SearchParametersViewModel Validation - Unit Tests")]
public class SearchParametersViewModelValidationTests
{
    [Theory]
    [InlineData("first", null, "Re10", null, null)]
    [InlineData("first.first", null, null, null, null)]
    [InlineData("first-first", null, null, null, null)]
    [InlineData(null, "last", null, null, null)]
    [InlineData(null, "last,last", null, null, null)]
    [InlineData(null, null, "company", null, null)]
    [InlineData(null, null, "comp & comp1", null, null)]
    [InlineData(null, null, "comp1 & comp2", null, null)]
    [InlineData(null, null, "comp(Re10) comp", null, null)]
    [InlineData(null, null, "comp(Re10) & Moore T/A comp", null, null)]
    [InlineData(null, null, null, "town", null)]
    [InlineData(null, null, null, "town'town", null)]
    [InlineData(null, null, null, null, "NN1 1DB")]
    [InlineData(null, null, null, null, "NN11DB")]
    [InlineData("first", "last", "company", "town", "NN1 1DB")]
    public void SearchParametersViewModelValidationSuccess(string? firstName, string? lastName, string? company, string? town, string? postcode)
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
    [InlineData("dJqRvzLxYbCmQwInsdPoFuHdGtAsXeRcVbNuYmJiKoLpZmXnOpQrStUvWxYzAbCdEfGhIjKlMnOpQrStUvWxYzAbCdEfGhIjKlMnd", null, null, null, null)]
    [InlineData(null, "dJqRvzLxYbCmQwInsdPoFuHdGtAsXeRcVbNuYmJiKoLpZmXnOpQrStUvWxYzAbCdEfGhIjKlMnOpQrStUvWxYzAbCdEfGhIjKlMnd", null, null, null)]
    [InlineData(null, null, "dJqRvzLxYbCmQwInsdPoFuHdGtAsXeRcVbNuYmJiKoLpZmXnOpQrStUvWxYzAbCdEfGhIjKlMnOpQrStUvWxYzAbCdEfGhIjKlMnd", null, null)]
    [InlineData(null, null, null, "dJqRvzLxYbCmQwInsdPoFuHdGtAsXeRcVbNuYmJiKoLpZmXnOpQrStUvWxYzAbCdEfGhIjKlMnOpQrStUvWxYzAbCdEfGhIjKlMnd", null)]
    [InlineData(null, null, null, null, "NN11 11DB")]
    public void SearchParametersViewModelValidationInValidLength(string? firstName, string? lastName, string? company, string? town, string? postcode)
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
        Assert.True(result.Any());
    }

    [Theory]
    [InlineData("first1 & first2", null, null, null, null)]
    [InlineData(null, "last1 last2", null, null, null)]
    [InlineData(null, null, "company1 @ company2 ", null, null)]
    [InlineData(null, null, null, "town1 & town2", null)]
    [InlineData(null, null, null,null, "ls10 NS20")]
    public void SearchParametersViewModelValidationInValidcharacters(string? firstName, string? lastName, string? company, string? town, string? postcode)
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
        Assert.True(result.Any());
    }
}
