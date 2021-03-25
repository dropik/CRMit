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
    public class E2ETests
    {
        [Test]
        public async Task OnCustomerCreate_ItIsPresentInCustomersService_AndThenOnDelete_Removed()
        {
            var client = new HttpClient();
            const string endpoint = "http://localhost:8000/crmit/v1/customers/";
            const string testEmail = "ivan.petrov@example.com";
            await WaitForServiceAvailable(client, endpoint);

            var response = await client.PostAsJsonAsync(endpoint,
                                                        new
                                                        {
                                                            name = "Ivan",
                                                            surname = "Petrov",
                                                            email = testEmail
                                                        });
            response.EnsureSuccessStatusCode();

            var location = response.Headers.Location;
            response = await client.GetAsync(location);
            response.EnsureSuccessStatusCode();
            var content = response.Content;
            var result = await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(await content.ReadAsStreamAsync());
            var resultEmail = result["email"].GetString();
            Assert.That(resultEmail, Is.EqualTo(testEmail));

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
                    Console.Error.WriteLine("Unable to connect to Customers service");
                    Thread.Sleep(5000);
                }
            }
        }
    }
}