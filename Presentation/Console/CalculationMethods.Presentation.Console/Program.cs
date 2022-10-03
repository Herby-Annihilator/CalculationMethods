using CalculationMethods.Core.Services.Factories.Base;
using CalculationMethods.Infrastructure.Services.Factories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CalculationMethods.Presentation.ConsoleUI
{
    public class Program
    {
        private IHost _host;
        private IHost Host => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        private IServiceProvider Services => Host.Services;
        

        private IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigureAppConfiguration)
            .ConfigureServices(ConfigureServices);

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddTransient<IFactory<double>, DoubleFactory>();
            services.AddTransient<IFactory<string>, StringFactory>();
        }

        private void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", false, true);
            context.Configuration = builder.Build();
        }


        public async Task Main(string[] args)
        {
            using var host = Host;
            await host.StartAsync();

            await host.StopAsync();
        }
    }
}