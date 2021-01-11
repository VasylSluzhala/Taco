using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Taco.Core;
using Taco.DAL;
using Taco.DAL.DataProvider;
using Taco.Services;
using Taco.WebApi.Middleware;

namespace Taco.WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _currentEnvironment;
        private IContainer _applicationContainer;
        private readonly IConfiguration _configuration;
        private readonly ConfigurationFactory _config;

        public Startup(IWebHostEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());

            Console.WriteLine($"Environment:{env.EnvironmentName}");
            configurationBuilder
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

            _configuration = configurationBuilder.Build();
            _currentEnvironment = env;
            _config = new ConfigurationFactory(_configuration, _currentEnvironment.ContentRootPath);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            ConfigureSqlServerConnection(services);

            services.AddCors(options => options.AddPolicy("defaultPolicy", policyBuilder => policyBuilder.WithOrigins(_config.CorsOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddOptions();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Taco 1.0 API", Version = "v1" });
            });

            services.AddProblemDetails(x =>
            {
                ProblemDetailsMIddleware.ConfigureProblemDetailsOptions(x, _currentEnvironment);
            });

            services.AddHttpContextAccessor();

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfiguration());
            });

            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<ServiceDependencyModule>();
            builder.RegisterModule<RepositoryDependencyModule>();

            _applicationContainer = builder.Build();

            var serviceProvider = new AutofacServiceProvider(_applicationContainer);

            return serviceProvider;
        }

        private void ConfigureSqlServerConnection(IServiceCollection services)
        {
            if (_config.DataBase.DataSource == "DB")
            {
                Console.WriteLine($"Connection String Name:{_config.ConnectionString}");

                services.AddDbContextPool<TacoDbContext>(options => options
                    .UseSqlServer(_config.ConnectionString));

                services.AddScoped<IDataProvider, SqlServerProvider>();
            }
            else if (_config.DataBase.DataSource == "JSON")
            {
                Console.WriteLine($"Data Source Path:{_config.DataBase.DataSource}");

                services.AddScoped<IDataProvider, JsonProvider>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("defaultPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseProblemDetails();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taco API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
