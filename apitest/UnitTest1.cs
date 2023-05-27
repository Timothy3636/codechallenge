using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.test  
{
    [TestFixture]
    public class ApiTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            // Initialize HttpClient
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7135");
        }

        // [Test]
        public async Task GetFxRate_ReturnsSuccess()
        {
            // Arrange
            string fromCCY = "AUD";
            string convertCCY = "HKD";
            string endpoint = $"/api1/fxrate?fromCCY={fromCCY}&convertCCY={convertCCY}";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            // Assert
            response.EnsureSuccessStatusCode(); // Ensure a successful HTTP status code (2xx)
            // Add more assertions to validate the response content, headers, etc. if needed
        }
    }
}
