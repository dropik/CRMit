using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CRMit.E2E
{
    public class ItemsE2E
    {
        [Test]
        public async Task OnItemCreate_ItIsPresentInItemsService_AndThenOnDelete_Removed()
        {
            var client = new HttpClient();
            const string endpoint = "https://localhost:8002/crmit/v1/items/";
            const string testName = "Laptop";
            await WaitForServiceAvailable(client, endpoint);

            var response = await client.PostAsJsonAsync(endpoint,
                                                        new
                                                        {
                                                            name = testName,
                                                            description = "A brand new laptop.",
                                                            price = 100
                                                        });
            response.EnsureSuccessStatusCode();

            var location = response.Headers.Location;
            response = await client.GetAsync(location);
            response.EnsureSuccessStatusCode();
            var content = response.Content;
            var result = await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(await content.ReadAsStreamAsync());
            var resultName = result["name"].GetString();
            Assert.That(resultName, Is.EqualTo(testName));

            response = await client.DeleteAsync(location);
            response.EnsureSuccessStatusCode();

            response = await client.GetAsync(location);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        private static async Task WaitForServiceAvailable(HttpClient client, string endpoint)
        {
            while (true)
            {
                try
                {
                    var response = await client.GetAsync(endpoint);
                    response.EnsureSuccessStatusCode();
                    break;
                }
                catch (Exception)
                {
                    Console.Error.WriteLine("Waiting for Items service...");
                    Thread.Sleep(5000);
                }
            }
        }
    }
}