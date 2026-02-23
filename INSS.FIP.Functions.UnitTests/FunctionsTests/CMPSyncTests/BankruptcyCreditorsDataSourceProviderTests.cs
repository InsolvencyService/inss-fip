
using AutoMapper;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Data.CMPDataSource.Interfaces;
using INSS.FIP.DataAccess;
using INSS.FIP.DataAccess.Mappers;
using INSS.FIP.Models.CentrallyManagedPartyModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Functions.UnitTests.FunctionsTests.CMPSyncTests
{
    
    public class BankruptcyCreditorsDataSourceProviderTests
    {
        private IMapper _mapper;

        [Fact]  
        public async Task CentrallyManagedPartyTransformationTests()
        {

            //Arrange
            var repoMock = new Mock<IBankruptcyCreditorsRepository>();
            var logger = new Mock<ILogger<BankruptcyCreditorsProvider>>();

            List<BankruptcyCreditorsList> args = null;
            repoMock.Setup(c => c.AddBankruptcyCreditorsListAsync(It.IsAny<List<BankruptcyCreditorsList>>()))
                    .Callback<List<BankruptcyCreditorsList>>((bcl) => args = bcl);

            var entitiesToAdd = new List<BankruptcyCreditorsList> {new BankruptcyCreditorsList { Id = 1 },
                                new BankruptcyCreditorsList { Id = 2 }};


            MapperConfiguration mapperConfig = new(
                cfg =>
                {
                    cfg.AddProfile(new CMPMapper());

                });

            _mapper = new Mapper(mapperConfig);

            var aProvider = new BankruptcyCreditorsProvider(repoMock.Object, logger.Object, _mapper);

            //Act
            await aProvider.TruncateAndInsertAsync(new List<CentrallyManagedPartyModel> 
            {
                new CentrallyManagedPartyModel (),
                new CentrallyManagedPartyModel ()
            });

            //Assert 
            Assert.Equal(entitiesToAdd, args, new BankruptcyCreditorsListListComparer());
        }



        private class BankruptcyCreditorsListListComparer : IEqualityComparer<List<BankruptcyCreditorsList>>
        {

            public bool Equals(List<BankruptcyCreditorsList>? x, List<BankruptcyCreditorsList>? y)
            {
                if (x == null && y == null)
                {
                    return true;
                }

                if (x == null || y == null)
                {
                    return false;
                }

                if (x.Count != y.Count)
                {
                    return false;
                }

                var bclComparer = new BankruptcyCreditorsListComparer();

                for (int i = 0; i < x.Count; i++)
                {
                    if (!bclComparer.Equals(x[i], y[i]))
                    {
                        return false;
                    }
                }

                return true;

            }

            public int GetHashCode([DisallowNull] List<BankruptcyCreditorsList> obj)
            {
                int value = 0;

                for (int i = 0; i < obj.Count; i++)
                {
                    value = value + (int)Math.Pow(2, i);
                }

                return value;
            }
        }



        private class BankruptcyCreditorsListComparer : IEqualityComparer<BankruptcyCreditorsList>
        {

            public bool Equals(BankruptcyCreditorsList? x, BankruptcyCreditorsList? y)
            {
                if (x == null && y == null)
                {
                    return true;
                }

                if (x == null || y == null)
                {
                    return false;
                }

                var properties = typeof(BankruptcyCreditorsList).GetProperties();
                foreach (var property in properties)
                {
                    if (property.GetValue(x)?.ToString() != property.GetValue(y)?.ToString())
                    {
                        return false;
                    }
                }

                return true; 
            }

            public int GetHashCode([DisallowNull] BankruptcyCreditorsList obj)
            {
                return obj.ToString().ToLower().GetHashCode();
            }

        }
    }
}
