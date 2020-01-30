using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ProductAPI.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using ProductAPI.Test.ManagersTest;

namespace ProductAPI.Test.ControllersTests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ProductControllerTest
    {
        private IConfiguration configuration;
        IHostingEnvironment hostingEnvironment;
        IWebHostBuilder webHostBuilder;

        [SetUp]
        public void SetUp()
        {

            webHostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    hostingEnvironment = hostingContext.HostingEnvironment;
                    builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                    configuration = builder.Build();

                })
                .UseEnvironment("UnitTest");
        }

        [TestCase("abcd")]
        [Test, Category("ProductController")]
        public void GetAircraftScheduleUnavailability_CalledThroughEmptyManager_ReturnsBadRequest(string airlineCode)
        {

            //Set DI if not setup by Startup
            var webHostBuilder = new WebHostBuilder().ConfigureServices(services =>
            {
                services.AddTransient<IProductManager, MockProductManager_Empty>();
                services.AddIPFiltering(filteroptions =>
                {
                    filteroptions.DefaultBlockLevel = DefaultBlockLevel.All;
                    filteroptions.HttpStatusCode = HttpStatusCode.NotFound;
                    filteroptions.Whitelist = configuration.GetSection("IPFiltering:Whitelist").Get<string[]>();
                });
            })
                .UseStartup(typeof(Startup));

            using (var server = new TestServer(webHostBuilder))
            {
                using (var client = server.CreateClient())
                {
                    //Fake Client Up Address for Unit Testing
                    client.DefaultRequestHeaders.Add("X-Forwarded-For", "192.168.0.10");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync($"/1/airlines/{airlineCode}/stations/aircraft/schedules").Result;
                    var results = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
                }
            }
        }
    }
}
