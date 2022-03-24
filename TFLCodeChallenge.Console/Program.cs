using System;
using System.Threading.Tasks;
using TFLCodeChallenge;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            GetRoadStatusService(host.Services);
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IRoadStatusService, RoadStatusService>());
        }

        public static async void GetRoadStatusService(IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var roadStatusService = provider.GetRequiredService<IRoadStatusService>();

            if (roadStatusService != null)
            {
                await roadStatusService.GetRoadStatus();
            }
        }
    }
}