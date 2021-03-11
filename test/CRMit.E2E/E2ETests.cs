using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRMit.E2E
{
    public class E2ETests
    {
        [Test]
        public async Task OnCustomerCreate_ItIsPresentInCustomersService()
        {
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync("https://crmit-customers/crmit/v1/customers/",
                                                        new
                                                        {
                                                            name = "Ivan",
                                                            surname = "Petrov",
                                                            email = "ivan.petrov@example.com"
                                                        });
            response.EnsureSuccessStatusCode();
            response = await client.GetAsync(response.Headers.Location);
            response.EnsureSuccessStatusCode();
            var content = response.Content;
            var result = await JsonSerializer.DeserializeAsync<Dictionary<string, dynamic>>(await content.ReadAsStreamAsync());
            Assert.That(result["email"], Is.EqualTo("ivan.petrov@example.com"));
        }
    }
}