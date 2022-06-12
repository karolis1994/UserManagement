using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using Xunit;

namespace UserManagement.API.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        public HttpClient Client { get; }
        public TestServer TestServer { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<UserManagement.API.Startup>()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "..\\..\\..\\..\\UserManagement.API"));

                    config.AddJsonFile("appsettings.json");
                });

            TestServer = new TestServer(builder);

            Client = TestServer.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:8888");
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}
