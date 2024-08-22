using DomainEntities.Model;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace IdentityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ClientController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
            
           // https://localhost:7194/api/Address

                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7051/api/Responce");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
               
                    List<Address> addresses = JsonConvert.DeserializeObject<List<Address>>(content);
                   
                    return Ok(addresses);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve data from the remote API.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(string url, AddressDto address)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (address == null)
            {
                return BadRequest("Address object is null.");
            }

            try
            {
                string jsonContent = JsonConvert.SerializeObject(address);

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                response.EnsureSuccessStatusCode(); 

                string responseData = await response.Content.ReadAsStringAsync();

                return Ok(responseData);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                string url = $"https://localhost:7194/api/Address/?{id}";
                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentNullException(nameof(url));
                }

                HttpResponseMessage response = await _httpClient.DeleteAsync(url);

                response.EnsureSuccessStatusCode(); // Throws if response status code is not success

                return Ok(await response.Content.ReadAsStringAsync());
            }

            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
}
}
