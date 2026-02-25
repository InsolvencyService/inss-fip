using AutoMapper;
using FakeItEasy;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Functions.Helper;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.CentrallyManagedPartyModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.Helpers;

public class DbSyncCMPDataTests
{
    private readonly IDataSourceProvider<CentrallyManagedPartyModel> _fakeSourceProvider = A.Fake<IDataSourceProvider<CentrallyManagedPartyModel>>();
    private readonly IDataTargetProvider<CentrallyManagedPartyModel> _fakeTargetRepository = A.Fake<IDataTargetProvider<CentrallyManagedPartyModel>>();
    private readonly ILogger<DbSyncCMPData<CentrallyManagedPartyModel>> _fakeLogger = A.Fake<ILogger<DbSyncCMPData<CentrallyManagedPartyModel>>>();
    private readonly IMapper _fakeMapper = A.Fake<IMapper>();
    private readonly DbSyncCMPData<CentrallyManagedPartyModel> _dbSyncCMPData;

    public DbSyncCMPDataTests()
    {
        _dbSyncCMPData = new DbSyncCMPData<CentrallyManagedPartyModel>(
            _fakeSourceProvider,
            _fakeTargetRepository,
            _fakeLogger);
    }

    [Fact]
    public async Task SynchronizeBankruptcyCreditorsAsync_ShouldReturnTrue_WhenSynchronizationIsSuccessful()
    {
        // Arrange
        const string viewName = ConstantValues.ViewName;


        var dummySourceData = A.CollectionOfDummy<CentrallyManagedPartyModel>(2).ToList();
        var dummyMappedData = A.CollectionOfDummy<BankruptcyCreditorsList>(2).ToList();

        A.CallTo(() => _fakeSourceProvider.GetDataFromViewAsync())
            .Returns(Task.FromResult(dummySourceData.Cast<CentrallyManagedPartyModel>().ToList()));       
        A.CallTo(() => _fakeTargetRepository.TruncateAndInsertAsync(A<List<CentrallyManagedPartyModel>>._)).Returns(Task.CompletedTask);

        // Act
        var result = await _dbSyncCMPData.SynchronizeBankruptcyCreditorsAsync();

        // Assert
        Assert.True(result, ConstantValues.SynchronizationSuccessfulMessage);

        A.CallTo(() => _fakeSourceProvider.GetDataFromViewAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeTargetRepository.TruncateAndInsertAsync(A<List<CentrallyManagedPartyModel>>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task SynchronizeBankruptcyCreditorsAsync_WithEmptyData_HandlesEmptyList()
    {
        // Arrange
        const string viewName = ConstantValues.ViewName;

        var emptySourceData = new List<CentrallyManagedPartyModel>();
        
        A.CallTo(() => _fakeSourceProvider.GetDataFromViewAsync())
            .Returns(Task.FromResult(emptySourceData.Cast<CentrallyManagedPartyModel>().ToList()));
        A.CallTo(() => _fakeTargetRepository.TruncateAndInsertAsync(A<List<CentrallyManagedPartyModel>>._)).Returns(Task.CompletedTask);

        // Act
        var result = await _dbSyncCMPData.SynchronizeBankruptcyCreditorsAsync();

        // Assert
        Assert.True(result, "Synchronization return true with empty data");

        A.CallTo(() => _fakeSourceProvider.GetDataFromViewAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeTargetRepository.TruncateAndInsertAsync(A<List<CentrallyManagedPartyModel>>.That.IsEmpty())).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void BankruptcyCreditorsList_CompareTo_HandlesNullNames()
    {
        // Arrange
        var item1 = new CentrallyManagedPartyModel { Name = string.Empty };
        var item2 = new CentrallyManagedPartyModel { Name = "Alice" };

        // Act & Assert
        Assert.True(item1.CompareTo(item2) < 0, "Null Name should come before non-null Name");
        Assert.True(item2.CompareTo(item1) > 0, "Non-null Name should come after null Name");
    }

    [Fact]
    public void BankruptcyCreditorsList_CompareTo_SortsByNameCorrectly()
    {
        // Arrange
        var item1 = new CentrallyManagedPartyModel { Name = "Alice"};
        var item2 = new CentrallyManagedPartyModel { Name = "Bob" };
        var item3 = new CentrallyManagedPartyModel { Name = "Alice" }; // Same name, different Id

        // Act & Assert
        Assert.True(item1.CompareTo(item2) < 0, "Alice should come before Bob");
        Assert.True(item2.CompareTo(item1) > 0, "Bob should come after Alice");
    }
}
