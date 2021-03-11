using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            const string endpoint = "https://crmit-customers/crmit/v1/customers/";
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
            var result = await JsonSerializer.DeserializeAsync<Dictionary<string, dynamic>>(await content.ReadAsStreamAsync());
            Assert.That(result["email"], Is.EqualTo(testEmail));

            response = await client.DeleteAsync(location);
            response.EnsureSuccessStatusCode();

            response = await client.GetAsync(location);
            Assert.That(response, Is.InstanceOf<NotFoundResult>());
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
                    Thread.Sleep(5000);
                }
            }
        }
    }
}