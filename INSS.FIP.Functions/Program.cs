using INSS.FIP.Data;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.DataAccess;
using INSS.FIP.DataAccess.Repository;
using INSS.FIP.Functions.Helper;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.CentrallyManagedParties.ResponseModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddTransient<SourceDbContext>(_ =>
        {
            var connectionString = Environment.GetEnvironmentVariable("sourceDbContextConnectionString");
            return new SourceDbContext(connectionString);
        });


        services.AddTransient<iirwebdbContext>(sp =>
        {
            var connectionString = Environment.GetEnvironmentVariable("iirwebdbContextConnectionString");
            return new iirwebdbContext(connectionString);
        });

        services.AddTransient<sourceCMPDbContext>(sp =>
        {
            var connectionString = Environment.GetEnvironmentVariable("sourceCMPDbContextConnectionString");
            var configuration = sp.GetRequiredService<IConfiguration>();
            return new sourceCMPDbContext(connectionString, configuration);
        });

        services.AddTransient<targetCMPDbContext>(sp =>
        {
            var connectionString = Environment.GetEnvironmentVariable("targetCMPDbContextConnectionString");
            return new targetCMPDbContext(connectionString);
        });

        services.AddTransient<IAuthBodyProvider, AuthBodyProvider>();
        services.AddTransient<IInsolvencyPractitionerProvider, InsolvencyPractitionerProvider>();
        services.AddTransient<IWebMessageProvider, WebMessageProvider>();
        services.AddTransient<IDbSync, DbSync>();
        services.AddTransient<IDbSyncData<CentrallyManagedPartyModel, BankruptcyCreditorsList>, DbSyncCMPData<CentrallyManagedPartyModel, BankruptcyCreditorsList>>();
        services.AddTransient<IDataSourceProvider<CentrallyManagedPartyModel>, CMPDataSourceProvider>();
        services.AddTransient<IDataTargetProvider<BankruptcyCreditorsList>, BankruptcyCreditorsRepository>();
    })
    .Build();

host.Run();
