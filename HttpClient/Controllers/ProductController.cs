using Core.DTOs;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ProductController()
    {
        _httpClient = new HttpClient();
    }

    [HttpGet]
    public async Task<IActionResult> Products()
    {
        string apiUrl = "https://localhost:7197/api/Products";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ProductDtosApiResponse>(responseData);
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




    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductUpdateDtos productUpdateDtos)
    {
        string apiUrl = "https://localhost:7197/api/Products";

        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(productUpdateDtos);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return Created(apiUrl, responseData); // 201 Created durum koduyla dön
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

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductDtos productDtos)
    {
        string apiUrl = "https://localhost:7197/api/Products";

        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(productDtos);
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



    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        string apiUrl = $"https://localhost:7197/api/Products/{id}";

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




    [HttpDelete("{productsId}")]
    public async Task<IActionResult> DeleteProduct(int productsId)
    {
        string apiUrl = $"https://localhost:7197/api/Products/{productsId}";

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

}

