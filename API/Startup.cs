using System;
using System.Globalization;
using System.Linq;
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
                .AddLocalization()
                .Configure<DbSettings>(props => _config.GetSection("DbSettings").Bind(props))
                .AddSingleton<DatabaseContext>()
                .AddTransient<IContentLanguageService, ContentLanguageService>()
                .AddTransient<ICarRepository, CarRepository>()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures &~ CultureTypes.NeutralCultures)
                .Where(cul => !String.IsNullOrEmpty(cul.Name))
                .Select(x => x.Name)
                .ToArray();
            
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture("pl")
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);
                
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseRouting()
                .UseCors("AllowAll")
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
