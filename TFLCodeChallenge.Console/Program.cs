using System.Threading.Tasks;
using TFLCodeChallenge;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRoadStatusService, RoadStatusService>()
                .BuildServiceProvider();

            // call the method
            var roadService = serviceProvider.GetService<IRoadStatusService>();
            if (roadService != null)
            {
                await roadService.GetRoadStatus();
            }
        }
    }
}