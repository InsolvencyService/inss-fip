using AutoMapper;
using INSS.FIP.Data;
using INSS.FIP.DataAccess;
using INSS.FIP.Functions.AutoMapperProfiles;
using INSS.FIP.Interfaces;
using Microsoft.EntityFrameworkCore;
using FakeItEasy;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace INSS.FIP.Functions.UnitTests.ProviderTests
{
    public class AuthBodyProviderTests
    {

        private IMapper _mapper;
        private readonly IConfiguration _fakeConfig = A.Fake<IConfiguration>();

        [Theory]
        [InlineData("true", 2)]
        [InlineData("false", 0)]
        [InlineData(null, 0)]
        public void UseINSSigthDataTest(string? configFlagValue, int recordCount)
        {
            //Arrange
            var options = new DbContextOptionsBuilder<iirwebdbContext>()
                .UseInMemoryDatabase(databaseName: "iirDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new iirwebdbContext(options))
            {
                context.FindIpAuthBodies.Add(new Data.FCMCDataSource.FindIpAuthBody { AuthBodyCode = "1", AuthBodyName = "John" });
                context.FindIpAuthBodies.Add(new Data.FCMCDataSource.FindIpAuthBody { AuthBodyCode = "2", AuthBodyName = "Peter" });
                //You cannot 'Add' records to EF table which has no primary key, context.Database.ExecuteSQLRaw can be used but not on an InMemoryDatabase 
                //context.CiIpAuthorisingBodies.Add(new CiIpAuthorisingBody { AuthBodyCode = "3", AuthBodyName = "Mary" });   
                context.SaveChanges();

            }

            MapperConfiguration mapperConfig = new(
                 cfg =>
                 {
                     cfg.AddProfile(new EntityFrameworkToApiModelProfiles());
                 });

            _mapper = new Mapper(mapperConfig);

            A.CallTo(() => _fakeConfig["usingFCMCtableView"]).Returns(configFlagValue);

            using (var context = new iirwebdbContext(options))
            {
                IAuthBodyProvider authProvider = new AuthBodyProvider(_mapper, context, _fakeConfig);

                //Act   
                var authRecords = authProvider.GetAsync().Result;

                //Assert
                Assert.Equal(recordCount, authRecords.Count);

                //Clean up InMemory database
                context.Database.EnsureDeleted();
            }
        }
    }
}
