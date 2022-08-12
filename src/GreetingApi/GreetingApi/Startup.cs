using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreetingApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var hosts = Configuration.GetSection(nameof(ExternalServicesHostsOptions)).Get<ExternalServicesHostsOptions>();
            services.AddHttpClient("rest_client", options =>
            {
                options.BaseAddress = new System.Uri(hosts.Rest);
            });
            services.AddSingleton<ExternalServicesHostsOptions>(hosts);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Greeting Api is running.");
                });
            });
        }
    }
}
