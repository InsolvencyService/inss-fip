using INSS.FIP.DataAccess;
using INSS.FIP.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using INSS.FIP.Data;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        //builder.Services.AddApplicationInsightsTelemetry();
        services.AddHttpClient();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddTransient(_ =>
        {
            var connectionString = Environment.GetEnvironmentVariable("iirwebdbContextConnectionString");
            return new iirwebdbContext(connectionString);
        });

        services.AddTransient<IAuthBodyProvider, AuthBodyProvider>();
        services.AddTransient<IInsolvencyPractitionerProvider, InsolvencyPractitionerProvider>();
        services.AddTransient<IWebMessageProvider, WebMessageProvider>();
    })
    .Build();

host.Run();
