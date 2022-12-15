using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.Models.RequestModels;
using INSS.FIP.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace INSS.FIP.Web.UnitTests.ControllerTests.IpControllerTests;

[Trait("Category", "IP Controller - Search Unit Tests")]
public class IpControllerSearchTests : BaseIpController
{
    [Fact]
    public void IpControllerSearchReturnsSuccess()
    {
        // Arrange
        using var controller = BuildIpController();

        // Act
        var result = controller.Search();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SearchParametersViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);
    }

    [Fact]
    public async Task IpControllerSearchWithParametersReturnsResultsSuccess()
    {
        // Arrange
        const int expectedResults = 2;
        var serviceSearchResults = A.CollectionOfDummy<SearchResultDomainModel>(expectedResults);
        var searchParametersViewModel = A.Dummy<SearchParametersViewModel>();
        using var controller = BuildIpController();

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).Returns(serviceSearchResults);
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).Returns(A.Dummy<FipApiSearchRequestModel>());
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).Returns(A.Dummy<SearchResultViewModel>());

        // Act
        var result = controller.Search(searchParametersViewModel);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public async Task IpControllerResultsReturnsResultsSuccess()
    {
        // Arrange
        const int expectedResults = 2;
        var serviceSearchResults = A.CollectionOfDummy<SearchResultDomainModel>(expectedResults);
        var searchParametersViewModel = A.Dummy<SearchParametersViewModel>();
        using var controller = BuildIpController();

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).Returns(serviceSearchResults);
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).Returns(A.Dummy<FipApiSearchRequestModel>());
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).Returns(A.Dummy<SearchResultViewModel>());

        // Act
        var result = await controller.Results();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SearchResultsViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);
        Assert.NotNull(model.Breadcrumbs);
        Assert.NotNull(model.Paged);
        Assert.NotNull(model.SearchResults);
        Assert.Equal(expectedResults, model.SearchResults!.Count());

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).MustHaveHappened(expectedResults, Times.Exactly);
    }

    [Fact]
    public async Task IpControllerResultsWithPageNumberReturnsResultsSuccess()
    {
        // Arrange
        const int expectedResults = 2;
        var serviceSearchResults = A.CollectionOfDummy<SearchResultDomainModel>(expectedResults);
        var searchParametersViewModel = A.Dummy<SearchParametersViewModel>();
        using var controller = BuildIpController();

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).Returns(serviceSearchResults);
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).Returns(A.Dummy<FipApiSearchRequestModel>());
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).Returns(A.Dummy<SearchResultViewModel>());

        // Act
        var result = await controller.Results(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SearchResultsViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);
        Assert.NotNull(model.Breadcrumbs);
        Assert.NotNull(model.Paged);
        Assert.NotNull(model.SearchResults);
        Assert.Equal(expectedResults, model.SearchResults!.Count());

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).MustHaveHappened(expectedResults, Times.Exactly);
    }

    [Fact]
    public async Task IpControllerResultsWithNullResultsReturnsResultsSuccess()
    {
        // Arrange
        const int expectedResults = 0;
        IList<SearchResultDomainModel>? nullServiceSearchResults = default;
        var searchParametersViewModel = A.Dummy<SearchParametersViewModel>();
        using var controller = BuildIpController();

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).Returns(nullServiceSearchResults);
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).Returns(A.Dummy<FipApiSearchRequestModel>());
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).Returns(A.Dummy<SearchResultViewModel>());

        // Act
        var result = await controller.Results(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SearchParametersViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);
        Assert.Null(model.Breadcrumbs);

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).MustHaveHappened(expectedResults, Times.Exactly);
    }

    [Fact]
    public async Task IpControllerResultsWithNoResultsReturnsResultsSuccess()
    {
        // Arrange
        const int expectedResults = 0;
        var serviceSearchResults = A.CollectionOfDummy<SearchResultDomainModel>(expectedResults);
        var searchParametersViewModel = A.Dummy<SearchParametersViewModel>();
        using var controller = BuildIpController();

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).Returns(serviceSearchResults);
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).Returns(A.Dummy<FipApiSearchRequestModel>());
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).Returns(A.Dummy<SearchResultViewModel>());

        // Act
        var result = await controller.Results(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SearchParametersViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);
        Assert.Null(model.Breadcrumbs);
        Assert.Equal("No matches found", model.ShowWarningMessage);

        A.CallTo(() => _fakeInsolvencyPractitionerService.SearchAsync(A<FipApiSearchRequestModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<FipApiSearchRequestModel>(A<SearchParametersViewModel>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<SearchResultViewModel>(A<SearchResultDomainModel>.Ignored)).MustHaveHappened(expectedResults, Times.Exactly);
    }

    [Fact]
    public async Task IpControllerIpReturnsSuccess()
    {
        // Arrange
        var serviceGetByIpResults = A.Dummy<InsolvencyPractitionerDomainModel>();
        using var controller = BuildIpController();

        A.CallTo(() => _fakeInsolvencyPractitionerService.IpGetByIpNumberAsync(A<int>.Ignored)).Returns(serviceGetByIpResults);
        A.CallTo(() => _fakeMapper.Map<InsolvencyPractitionerViewModel>(A<InsolvencyPractitionerDomainModel>.Ignored)).Returns(A.Dummy<InsolvencyPractitionerViewModel>());

        // Act
        var result = await controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<InsolvencyPractitionerViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);
        Assert.NotNull(model.Breadcrumbs);

        A.CallTo(() => _fakeInsolvencyPractitionerService.IpGetByIpNumberAsync(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<InsolvencyPractitionerViewModel>(A<InsolvencyPractitionerDomainModel>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task IpControllerIpReturnsNotFound()
    {
        // Arrange
        InsolvencyPractitionerDomainModel? nullServiceGetByIpResults = default;
        using var controller = BuildIpController();

        A.CallTo(() => _fakeInsolvencyPractitionerService.IpGetByIpNumberAsync(A<int>.Ignored)).Returns(nullServiceGetByIpResults);

        // Act
        var result = await controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<NotFoundResult>(result);

        Assert.Equal((int)HttpStatusCode.NotFound, viewResult.StatusCode);

        A.CallTo(() => _fakeInsolvencyPractitionerService.IpGetByIpNumberAsync(A<int>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<InsolvencyPractitionerViewModel>(A<InsolvencyPractitionerDomainModel>.Ignored)).MustNotHaveHappened();
    }
}
