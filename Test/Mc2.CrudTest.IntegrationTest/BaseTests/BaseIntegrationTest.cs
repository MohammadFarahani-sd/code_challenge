using Mc2.CrudTest.Presentation;
using Mc2.CrudTest.Presentation.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.IntegrationTest.BaseTests;

[AutoRollback]
public class BaseIntegrationTest
{
    protected readonly WebApplicationFactory<Program> Application;
    protected readonly IConfiguration Configuration;
    private HttpClient _clientRequest;

    protected BaseIntegrationTest()
    {
        Application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseTestServer(testServerOptions => testServerOptions.PreserveExecutionContext = true);
                builder.UseEnvironment("Test");

                builder.ConfigureServices(services =>
                {
                    builder.ConfigureAppConfiguration((c, b) =>
                    {
                        b.AddJsonFile("appsettings.Test.json");
                        b.AddEnvironmentVariables();
                    });
                });
            });
        Configuration = Application.Server.Services.GetService<IConfiguration>()!;
    }

    protected HttpClient ClientRequest
    {
        get { return _clientRequest; }
    }
}