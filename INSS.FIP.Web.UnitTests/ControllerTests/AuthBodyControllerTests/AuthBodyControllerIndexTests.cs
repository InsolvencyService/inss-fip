using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using INSS.FIP.Models.DomainModels;
using INSS.FIP.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace INSS.FIP.Web.UnitTests.ControllerTests.AuthBodyControllerTests;

[Trait("Category", "AuthBody Controller - Index Unit Tests")]

public class AuthBodyControllerIndexTests : BaseAuthBodyController
{
    [Fact]
    public async Task AuthBodyControllerIndexReturnsSuccess()
    {
        // Arrange
        const int expectedAuthBodies = 2;
        var serviceGetResults = A.CollectionOfDummy<AuthBodyDomainModel>(expectedAuthBodies);
        using var controller = BuildAuthBodyController();

        A.CallTo(() => _fakeAuthBodyService.GetAsync()).Returns(serviceGetResults);
        A.CallTo(() => _fakeMapper.Map<AuthBodyViewModel>(A<AuthBodyDomainModel>.Ignored)).Returns(A.Dummy<AuthBodyViewModel>());

        // Act
        var result = await controller.Index(1, "my name");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<AuthBodiesViewModel>(viewResult.ViewData.Model);

        Assert.NotNull(model);
        Assert.NotNull(model.Breadcrumbs);
        Assert.True(model.Breadcrumbs!.Any());
        Assert.NotNull(model.AuthBodies);
        Assert.Equal(expectedAuthBodies, model.AuthBodies!.Count());

        A.CallTo(() => _fakeAuthBodyService.GetAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<AuthBodyViewModel>(A<AuthBodyDomainModel>.Ignored)).MustHaveHappened(expectedAuthBodies, Times.Exactly);
    }
}