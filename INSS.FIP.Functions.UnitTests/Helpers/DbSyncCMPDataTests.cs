using AutoMapper;
using FakeItEasy;
using INSS.FIP.Data;
using INSS.FIP.Functions.Helper;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.CentrallyManagedParties.ResponseModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace INSS.FIP.Functions.UnitTests.Helpers;

public class DbSyncCMPDataTests
{
    private readonly IDataSourceProvider<CentrallyManagedPartyModel> _fakeSourceProvider = A.Fake<IDataSourceProvider<CentrallyManagedPartyModel>>();
    private readonly IDataTargetProvider<BankruptcyCreditorsList> _fakeTargetRepository = A.Fake<IDataTargetProvider<BankruptcyCreditorsList>>();
    private readonly ILogger<DbSyncCMPData<CentrallyManagedPartyModel, BankruptcyCreditorsList>> _fakeLogger = A.Fake<ILogger<DbSyncCMPData<CentrallyManagedPartyModel, BankruptcyCreditorsList>>>();
    private readonly IMapper _fakeMapper = A.Fake<IMapper>();
    private readonly DbSyncCMPData<CentrallyManagedPartyModel, BankruptcyCreditorsList> _dbSyncCMPData;

    public DbSyncCMPDataTests()
    {
        _dbSyncCMPData = new DbSyncCMPData<CentrallyManagedPartyModel, BankruptcyCreditorsList>(
            _fakeSourceProvider,
            _fakeTargetRepository,
            _fakeLogger,
            _fakeMapper);
    }

    [Fact]
    public async Task SynchronizeBankruptcyCreditorsAsync_ShouldReturnTrue_WhenSynchronizationIsSuccessful()
    {
        // Arrange
        const string viewName = ConstantValues.ViewName;
        const string orderByColumn = ConstantValues.OrderByColumn;

        var dummySourceData = A.CollectionOfDummy<CentrallyManagedPartyModel>(2).ToList();
        var dummyMappedData = A.CollectionOfDummy<BankruptcyCreditorsList>(2).ToList();

        A.CallTo(() => _fakeSourceProvider.GetDataFromViewAsync())
            .Returns(Task.FromResult(dummySourceData.Cast<CentrallyManagedPartyModel>().ToList()));
        A.CallTo(() => _fakeMapper.Map<List<BankruptcyCreditorsList>>(dummySourceData)).Returns(dummyMappedData);
        A.CallTo(() => _fakeTargetRepository.TruncateAndInsertAsync(A<List<BankruptcyCreditorsList>>._)).Returns(Task.CompletedTask);


        // Act
        var result = await _dbSyncCMPData.SynchronizeBankruptcyCreditorsAsync(orderByColumn);

        // Assert
        Assert.True(result, ConstantValues.SynchronizationSuccessfulMessage);

        A.CallTo(() => _fakeSourceProvider.GetDataFromViewAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<List<BankruptcyCreditorsList>>(dummySourceData)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeTargetRepository.TruncateAndInsertAsync(A<List<BankruptcyCreditorsList>>.That.Matches(list => list.Count == dummyMappedData.Count))).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task SynchronizeBankruptcyCreditorsAsync_WithEmptyData_HandlesEmptyList()
    {
        // Arrange
        const string viewName = ConstantValues.ViewName;
        const string orderByColumn = ConstantValues.OrderByColumn;

        var emptySourceData = new List<CentrallyManagedPartyModel>();
        var emptyMappedData = new List<BankruptcyCreditorsList>();

        A.CallTo(() => _fakeSourceProvider.GetDataFromViewAsync())
            .Returns(Task.FromResult(emptySourceData.Cast<CentrallyManagedPartyModel>().ToList()));
        A.CallTo(() => _fakeMapper.Map<List<BankruptcyCreditorsList>>(emptySourceData.AsEnumerable())).Returns(emptyMappedData);
        A.CallTo(() => _fakeTargetRepository.TruncateAndInsertAsync(A<List<BankruptcyCreditorsList>>._)).Returns(Task.CompletedTask);

        // Act
        var result = await _dbSyncCMPData.SynchronizeBankruptcyCreditorsAsync(orderByColumn);

        // Assert
        Assert.True(result, "Synchronization return true with empty data");

        A.CallTo(() => _fakeSourceProvider.GetDataFromViewAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<List<BankruptcyCreditorsList>>(emptySourceData.AsEnumerable())).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeTargetRepository.TruncateAndInsertAsync(A<List<BankruptcyCreditorsList>>.That.IsEmpty())).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void BankruptcyCreditorsList_CompareTo_HandlesNullNames()
    {
        // Arrange
        var item1 = new BankruptcyCreditorsList { Name = string.Empty };
        var item2 = new BankruptcyCreditorsList { Name = "Alice" };

        // Act & Assert
        Assert.True(item1.CompareTo(item2) < 0, "Null Name should come before non-null Name");
        Assert.True(item2.CompareTo(item1) > 0, "Non-null Name should come after null Name");
    }

    [Fact]
    public void BankruptcyCreditorsList_CompareTo_SortsByNameCorrectly()
    {
        // Arrange
        var item1 = new BankruptcyCreditorsList { Name = "Alice", Id = 1 };
        var item2 = new BankruptcyCreditorsList { Name = "Bob", Id = 2 };
        var item3 = new BankruptcyCreditorsList { Name = "Alice", Id = 3 }; // Same name, different Id

        // Act & Assert
        Assert.True(item1.CompareTo(item2) < 0, "Alice should come before Bob");
        Assert.True(item2.CompareTo(item1) > 0, "Bob should come after Alice");
    }
}
