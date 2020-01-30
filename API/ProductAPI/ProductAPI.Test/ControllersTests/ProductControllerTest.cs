using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Newtonsoft.Json;
using ProductAPI.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using ProductAPI.Test.ManagersTestData;
using Microsoft.Extensions.DependencyInjection;
using ZNetCS.AspNetCore.IPFiltering;
using ProductAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProductAPI.Test.ControllersTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ProductControllerTest
    {
        private IConfiguration configuration;
        IHostingEnvironment hostingEnvironment;
        IWebHostBuilder webHostBuilder;

        #region Setup
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
        #endregion

        #region GetProduct Action Method Test
        [TestCase("", "")]
        [Test, Category("ProductController")]
        public void GetProducts_CalledThroughEmptyManager_ReturnsNoContentFound(string manufacturer, string type)
        {

            //Set DI if not setup by Startup
            webHostBuilder.ConfigureServices(services =>
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
                    var response = client.GetAsync($"/api/product?manufacturer={manufacturer}&type={type}").Result;
                    var results = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);
                }
            }
        }

        [TestCase("assssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss", "assssssssssssssas")]
        [Test, Category("ProductController")]
        public void GetProducts_CalledWithDataManager_ReturnsBadRequest(string manufacturer, string type)
        {

            //Set DI if not setup by Startup
            webHostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IProductManager, MockProductManager_Success>();
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
                    var response = client.GetAsync($"/api/product?manufacturer={manufacturer}&type={type}").Result;
                    var results = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
                }
            }
        }

        [TestCase("", "")]
        [TestCase("ONEPLUS", "MOBILE")]
        [TestCase("", "MOBILE")]
        [TestCase("ONEPLUS", "")]
        [Test, Category("ProductController")]
        public void GetProducts_CalledWithDataManager_ReturnsSuccess(string manufacturer, string type)
        {

            //Set DI if not setup by Startup
            webHostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IProductManager, MockProductManager_Success>();
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
                    var response = client.GetAsync($"/api/product?manufacturer={manufacturer}&type={type}").Result;
                    var results = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestCase("ONEPLUS", "")]
        [Test, Category("ProductController")]
        public void GetProducts_CalledThroughExceptionManager_ReturnsInternalServerError(string manufacturer, string type)
        {

            //Set DI if not setup by Startup
            webHostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IProductManager, MockProductManager_Exception>();
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
                    var response = client.GetAsync($"/api/product?manufacturer={manufacturer}&type={type}").Result;
                    var results = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.InternalServerError);
                }
            }
        }
        #endregion

        #region UpsertProduct Action Method Test
        [TestCase("", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "mobile")] // id is blank
        [TestCase("001", "", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "mobile")] //description is blank
        [TestCase("001", "Android SmartPhone", "", "Oneplus", "2019-06-21", "Oneplus", "mobile")] //dateofExpiry is blank
        [TestCase("001", "Android SmartPhone", "2019-06-21", "", "2019-06-21", "Oneplus", "mobile")] //manufacturer is blank
        [TestCase("001", "Android SmartPhone", "2019-06-21", "Oneplus", "", "Oneplus", "mobile")] //dateofManufacturing is blank
        [TestCase("001", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "", "mobile")] //name is blank
        [TestCase("001", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "")] // type is blank
        [TestCase("001", "Android SmartPhone", "21/06/2019", "Oneplus", "2019-06-21", "Oneplus", "mobile")] //dateOfExpiry format is not correct
        [TestCase("001", "Android SmartPhone", "2019-06-21", "Oneplus", "21/06/2019", "Oneplus", "mobile")] //dateOfManufacturing format is not correct
        [Test, Category("ProductController")]
        public void UpsertProduct_CalledThroughEmptyManager_ReturnsBadRequest(string id,string description,string dateOfExpiry,string manufacturer,string dateOfManufacturing,string name,string type)
        {
            Product product = new Product()
            {
                description = description,
                dateOfExpiry = dateOfExpiry,
                productId = id,
                manufacturer = manufacturer,
                dateOfManufacturing = dateOfManufacturing,
                name = name,
                type = type
            };

            //Set DI if not setup by Startup
            webHostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IProductManager, MockProductManager_Success>();
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

                    var productReq = JsonConvert.SerializeObject(product);
                    var productReqContent = new StringContent(productReq);
                    productReqContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = client.PostAsync($"/api/product",productReqContent).Result;
                    var results = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
                }
            }
        }

        [TestCase("0001", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "mobile")]
        [Test, Category("ProductController")]
        public void UpsertProduct_CalledWithDataManager_ReturnsSuccess(string id, string description, string dateOfExpiry, string manufacturer, string dateOfManufacturing, string name, string type)
        {
            Product product = new Product()
            {
                description = description,
                dateOfExpiry = dateOfExpiry,
                productId = id,
                manufacturer = manufacturer,
                dateOfManufacturing = dateOfManufacturing,
                name = name,
                type = type
            };

            //Set DI if not setup by Startup
            webHostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IProductManager, MockProductManager_Success>();
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

                    var productReq = JsonConvert.SerializeObject(product);
                    var productReqContent = new StringContent(productReq);
                    productReqContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = client.PostAsync($"/api/product", productReqContent).Result;
                    var results = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                }
            }

        }


        [TestCase("0001", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "mobile")]
        [Test, Category("ProductController")]
        public void UpsertProduct_CalledThroughExceptionManager_ReturnsInternalServerError(string id, string description, string dateOfExpiry, string manufacturer, string dateOfManufacturing, string name, string type)
        {
            Product product = new Product()
            {
                description = description,
                dateOfExpiry = dateOfExpiry,
                productId = id,
                manufacturer = manufacturer,
                dateOfManufacturing = dateOfManufacturing,
                name = name,
                type = type
            };

            //Set DI if not setup by Startup
            webHostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IProductManager, MockProductManager_Exception>();
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

                    var productReq = JsonConvert.SerializeObject(product);
                    var productReqContent = new StringContent(productReq);
                    productReqContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = client.PostAsync($"/api/product", productReqContent).Result;
                    var results = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.InternalServerError);

                }
            }
        }
        #endregion
    }
}
