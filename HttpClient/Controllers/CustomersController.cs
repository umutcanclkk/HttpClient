using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public CustomersController()
    {
        _httpClient = new HttpClient();
    }

    [HttpGet]
    public async Task<IActionResult> Customers()
    {
        string apiUrl = "https://localhost:7197/api/Customers";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseData);
                return Ok(apiResponse.data); // data property'sini döndür
            }
            else
            {
                return BadRequest($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception: {ex.Message}");
        }
        finally
        {
            _httpClient.Dispose();
        }
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        string apiUrl = $"https://localhost:7197/api/Customers/{id}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                return Ok(responseData);
            }
            else
            {
                return BadRequest($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception: {ex.Message}");
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] CustomerDtos customerData)
    {
        string apiUrl = "https://localhost:7197/api/Customers";

        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(customerData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return Created(apiUrl, responseData); // 201 Created durum koduyla ve URI ile birlikte dön
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    return BadRequest($"Error: {response.StatusCode} - {response.ReasonPhrase}. {errorResponse}");
                }
            }
        }
        catch (HttpRequestException ex)
        {
            return BadRequest($"HTTP Request Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception: {ex.Message}");
        }
    }







    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        string apiUrl = $"https://localhost:7197/api/Customers/{customerId}";

        try
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return NoContent();
                }
                else
                {
                    return BadRequest($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }
        catch (HttpRequestException ex)
        {
            return BadRequest($"HTTP Request Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception: {ex.Message}");
        }
    }




    [HttpPut]
    public async Task<IActionResult> UpdateCustomer(CustomerUpdateDtos updatedCustomerData)
    {
        string apiUrl = "https://localhost:7197/api/Customers/";

        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(updatedCustomerData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return NoContent();
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    return BadRequest($"Error: {response.StatusCode} - {response.ReasonPhrase}. {errorResponse}");
                }
            }
        }
        catch (HttpRequestException ex)
        {
            return BadRequest($"HTTP Request Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception: {ex.Message}");
        }
    }
}
