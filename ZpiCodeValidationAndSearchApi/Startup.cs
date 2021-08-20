using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using ViaCep;
using ZipCodeValidationAndSearchApi.Application.Behaviors;
using ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode;
using ZipCodeValidationAndSearchApi.Infrastructure.SearchAddressByZipCode;

namespace ZipCodeValidationAndSearchApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Address search API v1", Version = "v1" });

                c.ExampleFilters();

                //The lines below allow to take controller classes xml comments to Swagger documentation. The more documented, the better!
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
            services.AddMediatR(typeof(ApplicationValidationBehavior<,>).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ApplicationValidationBehavior<,>));
            services.AddValidatorsFromAssemblies(new Assembly[] { typeof(ApplicationValidationBehavior<,>).Assembly });
            services.AddScoped<IAddressSearchClient, AddressSearchClient>();
            services.AddHttpClient<IViaCepClient, ViaCepClient>(client => { client.BaseAddress = new Uri(Configuration["URI_VIACEP"]); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Address search API v1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
