using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TFLCodeChallenge
{
    public class RoadStatusService : IRoadStatusService
    {
        private string roadId;

        public async Task GetRoadStatus()
        {
            do
            {
                Console.WriteLine("Enter Road id");
                roadId = Console.ReadLine();
                await RoadStatus(roadId);
                Console.WriteLine("----> Enter 'Y' to continue, any other key to exit <----");
            }
            while (Console.ReadLine().Equals("Y", StringComparison.OrdinalIgnoreCase));
        }

        private async Task RoadStatus(string roadId)
        {
            var client = new HttpClient();
            // Request headers
            client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
            var uri = $"https://api.tfl.gov.uk/Road/{roadId}";

            var response = await client.GetAsync(uri);
            try
            {
                response.EnsureSuccessStatusCode();
                var dataJson = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Road>>(dataJson);
                if (data != null && data.Count > 0)
                {
                    Console.WriteLine($"Road Display name  is {data.First().DisplayName} ");
                    Console.WriteLine($"Road Status  is {data.First().StatusSeverity} ");
                    Console.WriteLine($"Road Status Description  is {data.First().StatusSeverityDescription} ");
                }
                else
                {
                    Console.WriteLine($"{roadId} is not a valid road.");
                }
            }
            catch
            {
                Console.WriteLine($"{roadId} is not a valid road.");
            }
        }
    }
}