using System.Diagnostics.CodeAnalysis;
using INSS.FIP.Interfaces;
using INSS.FIP.Models.ClientOptionsModels;
using INSS.FIP.Models.PollyModels;
using INSS.FIP.Services;
using INSS.FIP.Web.Extensions;
using Joonasw.AspNetCore.SecurityHeaders;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices(services =>
{
    RetryPolicyOptions retryPolicyOptions = new()
    {
        BackoffPower = 2,
        Count = 3
    };

    services.AddCsp(nonceByteAmount: 32);
    services.AddApplicationInsightsTelemetry();
    services.AddHttpClient();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.TryAddSingleton(builder.Configuration.GetRequiredSection(nameof(FipApiConnectorClientOptions)).Get<FipApiConnectorClientOptions>());
    services.AddTransient<IAuthBodyService, AuthBodyService>();
    services.AddTransient<IInsolvencyPractitionerService, InsolvencyPractitionerService>();
    services.AddTransient<IWebMessageService, WebMessageService>();

    Polly.Registry.IPolicyRegistry<string> policyRegistry = services.AddPolicyRegistry();
    const string retryPolicyName = "RetryPolicyName";

    services
        .AddPolicies(policyRegistry, retryPolicyName, retryPolicyOptions)
        .AddHttpClient<IFipApiConnector, FipApiConnector, FipApiConnectorClientOptions>(retryPolicyName);

    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(10);
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/errors", "?statusCode={0}");

    app.UseExceptionHandler("/Errors");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseCsp(csp =>
{
    csp.ByDefaultAllow.FromNowhere();

    csp.AllowScripts
        .FromSelf()
        .AddNonce();
    csp.AllowStyles
        .FromSelf();
    csp.AllowFonts
        .FromSelf();
    csp.AllowImages
        .FromSelf();

    if (app.Environment.IsDevelopment())
    {
        //Allows hot-reload script to work in dev only
        csp.AllowConnections
            .To("wss:");
    }

    csp.AllowConnections
        .ToSelf();

});

var options = new RewriteOptions()
    .AddRedirect("security.txt$", @"https://security.insolvency.gov.uk/.well-known/security.txt");

app.UseRewriter(options);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=IP}/{action=Index}/{id?}");

app.Run();

namespace INSS.FIP.Web
{
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1050:Declare types in namespaces", Justification = "Used to eliminate from code coverage")]
    public partial class Program { }
}
