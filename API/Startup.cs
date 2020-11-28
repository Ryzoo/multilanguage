using Application.Interfaces.Database.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Application.Settings;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging()
                .AddOptions()
                .Configure<DbSettings>(_config.GetSection(DbSettings.Name))
                .AddSingleton<DatabaseContext>()
                .AddTransient<IContentLanguageService, ContentLanguageService>()
                .AddTransient<ICarRepository, CarRepository>()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            var supportedCultures = new[] { "pl", "en-US", "de" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions)
                .UseRouting()
                .UseCors("AllowAll")
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
