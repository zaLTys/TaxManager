using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace TaxManagerIntegration.Tests
{
    public class TaxManagerIntegrationTests
    {
        [Fact]
        public async Task RenderApplicationForm()
        {
           // var builder = new WebHostBuilder() 
           //     .UseContentRoot(@"C:\Users\povil\")
           //     .UseEnvironment("Production")
           //     .UseStartup<TaxManager.Api.Startup>();
           //
           // var server = new TestServer(builder); 
           //
           // var client = server.CreateClient(); 
           //
           // var response = await client.GetAsync("/index");
           //
           // response.EnsureSuccessStatusCode();
           //
           // var responseString = await response.Content.ReadAsStringAsync();
           //
           // Assert.Contains("Welcome to VetClinic", responseString);
        }
    }
}
