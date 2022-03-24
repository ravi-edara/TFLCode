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
        public async Task GetRoadStatus()
        {
            do
            {
                Console.WriteLine("Enter Road id");
                var id = Console.ReadLine();
                var data = await GetRoadStatusById(id);
                if (data != null)
                {
                    Console.WriteLine($"Road Display name  is {data.DisplayName} ");
                    Console.WriteLine($"Road Status  is {data.StatusSeverity} ");
                    Console.WriteLine($"Road Status Description  is {data.StatusSeverityDescription} ");
                }
                else
                {
                    Console.WriteLine($"{id} is not a valid road.");
                }

                Console.WriteLine("----> Enter 'Y' to continue, any other key to exit <----");
            }
            while (Console.ReadLine().Equals("Y", StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Road> GetRoadStatusById(string roadId)
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
                    return data.First();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}