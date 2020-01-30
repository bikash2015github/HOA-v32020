using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZNetCS.AspNetCore.IPFiltering;

namespace ProductAPI
{
    public partial class Startup
    {
        /// <summary>
        /// NOTE: This is a placeholder Startup used for Unit Tests since .Net Core is smart enough to use environment variables to override startup methods
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureUnitTestServices(IServiceCollection services)
        {
            // NOTE: Have Kubernetes set up ASPNETCORE_ENVIRONMENT environment variable to equal "Development" for TEST and QA and "Production" for Produciton.
            //      see: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-2.0#startup-conventions

            // Add Serilog logging
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            // Add supported request/response formatter types
            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                /*options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("json", MediaTypeHeaderValue.Parse("application/json"));*/
            })
            .AddXmlSerializerFormatters();

            //## Set description information in Swagger
            var swaggerdescription = new StringBuilder();
            swaggerdescription.AppendLine("Unit Testing The Service API");
            swaggerdescription.AppendLine($"Environment: {HostingEnvironment.EnvironmentName}");
            swaggerdescription.AppendLine($"Build Number: {Configuration["ApplicationInformation:BuildNumber"]}");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Unit Testing The Service API",
                    Version = "v1",
                    Description = swaggerdescription.ToString(),
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "ITS M&E/Safety",
                        Email = "me.safety@alaskaair.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //##  Add IP Filtering Services
            // Using Open Source From: https://github.com/msmolka/ZNetCS.AspNetCore.IPFiltering
            services.AddIPFiltering(filteroptions =>
            {
                filteroptions.DefaultBlockLevel = DefaultBlockLevel.All;
                filteroptions.HttpStatusCode = HttpStatusCode.NotFound;
                filteroptions.Whitelist = Configuration.GetSection("IPFiltering:Whitelist").Get<string[]>();
            });
        }
    }
}
