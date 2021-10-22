using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceBusExample.Api.Middlewares;
using ServiceBusExample.Api.StartupConfigurations;
using ServiceBusExample.Application;
using ServiceBusExample.Infrastructure;

namespace ServiceBusExample.Api
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
            services.AddServiceBusInfrastructure(Configuration);
            services.AddApplication();
            services.AddCors(Configuration);
            services.SwaggerConfigureServices();
            services.RegisterAllDependencies();

            services.CheckAndConfigurations(Configuration);
            services.AddHttpContextAccessor();
            services.AddControllers()
            .ConfigureApiBehaviorOptions(options => { options.SuppressMapClientErrors = true; });
            services.AddDefaultCorrelationId();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.SwaggerConfigure();
            app.UseCorrelationId();
            app.UseHttpsRedirection();
            app.ConfigureCustomMiddlewares();
            app.UseRouting();
            app.UseCorsConfig();
            app.UseAuthorization();
            app.SettingsEndpoints(env);
        }
    }
}