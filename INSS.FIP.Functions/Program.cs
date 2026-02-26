using INSS.FIP.Data;
using INSS.FIP.Data.CMPDataSource;
using INSS.FIP.Data.CMPDataSource.Interfaces;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.DataAccess;
using INSS.FIP.Functions.Helper;
using INSS.FIP.Interfaces;
using INSS.FIP.Interfaces.CMP;
using INSS.FIP.Models.CentrallyManagedPartyModels;
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

        services.AddTransient<SourceCMPDbContext>(sp =>
        {
            var connectionString = Environment.GetEnvironmentVariable("sourceCMPDbContextConnectionString");
            var configuration = sp.GetRequiredService<IConfiguration>();
            return new SourceCMPDbContext(connectionString, configuration);
        });

        services.AddScoped<TargetCMPDbContext>(sp =>
        {
            var connectionString = Environment.GetEnvironmentVariable("targetCMPDbContextConnectionString");
            return new TargetCMPDbContext(connectionString);
        });

        services.AddScoped<IBankruptcyCreditorsRepository, BankruptcyCreditorsRepository>();

        services.AddTransient<IAuthBodyProvider, AuthBodyProvider>();
        services.AddTransient<IInsolvencyPractitionerProvider, InsolvencyPractitionerProvider>();
        services.AddTransient<IWebMessageProvider, WebMessageProvider>();
        services.AddTransient<IDbSync, DbSync>();

        services.AddTransient<IDbSyncData<CentrallyManagedPartyModel>, DbSyncCMPData<CentrallyManagedPartyModel>>();
        services.AddTransient<IDataSourceProvider<CentrallyManagedPartyModel>, CMPDataSourceProvider>();
        services.AddTransient<IDataTargetProvider<CentrallyManagedPartyModel>, BankruptcyCreditorsProvider>();
    })
    .Build();

host.Run();
