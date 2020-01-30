using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.DataAccess;
using ProductAPI.Managers;
using Serilog;
using Serilog.Exceptions;

namespace ProductAPI
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// 
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Gets the hosting environment.
        /// </summary>
        /// <value>
        /// The hosting environment.
        /// </value>
        public IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }


        public Startup(IHostingEnvironment environment)
        {
            HostingEnvironment = environment;
            //## Setup appsettings[.<envrironment>].json file handling
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build();

            #region Configure the Serilog logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty(
                    name: "Application"
                    , value: Configuration["Logging:SourceName"]
                )
                .Enrich.WithProperty(
                    name: "Environment"
                    , value: environment.EnvironmentName.ToUpper()
                )
                .Enrich.WithProperty(
                    name: "RequestId"
                    , value: Guid.NewGuid()
                )
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithExceptionDetails()
                .WriteTo.RollingFile(
                    pathFormat: Configuration["Logging:RollingFileLog"]
                    , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose
                    , outputTemplate: Configuration["Logging:LoggingTemplate"]
                    , formatProvider: null
                    , fileSizeLimitBytes: 1073741824
                    , retainedFileCountLimit: 7
                    , levelSwitch: null
                    , buffered: false
                    , shared: true
                    , flushToDiskInterval: null
                )
                //unfold this configuration while using appinsight
                //.WriteTo.ApplicationInsightsEvents(
                //    instrumentationKey: Configuration["ApplicationInsights:InstrumentationKey"]
                //)
                //.WriteTo.ApplicationInsightsTraces(
                //    instrumentationKey: Configuration["ApplicationInsights:InstrumentationKey"])               
                .WriteTo.Console(
                    outputTemplate: Configuration["Logging:LoggingTemplate"]
                )
                .CreateLogger();
            #endregion
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //## Add Serilog logging service
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            //## Add Application Insights service
            //services.AddApplicationInsightsTelemetry(this.Configuration);

            services.AddDbContextPool<ProductDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProductDBConnection")));

            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                /*options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("json", MediaTypeHeaderValue.Parse("application/json"));*/
            })
           .AddXmlSerializerFormatters();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IProductRepository, SQLProductRepository>();            
            #region Swagger Configuration
            //## Set custom description information in Swagger
            var swaggerdescription = new StringBuilder();
            swaggerdescription.AppendLine($"Purpose: <b>Web Service API to manage products </b><br/>");
            swaggerdescription.AppendLine($"Environment: <b>{HostingEnvironment.EnvironmentName} </b>");
            swaggerdescription.AppendLine($"Build Number: <b>{Configuration["ApplicationInformation:BuildNumber"]} </b><br/>");
            // NOTE: The Middleware Team would like us to include a Release Date but the field is not easily attained nor easy to set as
            //       Builds and Releases are not always in line.  Commenting out for now.
            //swaggerdescription.AppendLine("Release Date: <b>{env.ReleaseDate} </b><br/>");
            swaggerdescription.AppendLine($"Source Repo Url:  <br/>");
            swaggerdescription.AppendLine("Owner: <b> Bikash Das</b>. Email: bikash.chandradas94@gmail.com <br/>");
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Product Service API",
                    Version = "v1",
                    Description = swaggerdescription.ToString(),
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Product Store",
                        Email = "productstore@productcompany.com",
                        Url = "www.productdetails.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // Enable the Swashbuckle.AspNetCore.Annotations package
                c.EnableAnnotations(); 

                
               
            }); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware<SerilogHttpRequestResponseLogger>
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            app.UseMvc();

            //## Use Swagger for API Configuration
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Product Service API V1");
                //Set routeprefix so it launches swagger UI right away
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
