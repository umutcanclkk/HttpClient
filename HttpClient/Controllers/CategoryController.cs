using Core.DTOs;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public CategoryController()
    {
        _httpClient = new HttpClient();
    }



    [HttpGet]
    public async Task<IActionResult> Category()
    {
        string apiUrl = "https://localhost:7197/api/Categories";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<CategoryDtosApiResponse>(responseData);
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
    public async Task<IActionResult> GetCategoryById(int id)
    {
        string apiUrl = $"https://localhost:7197/api/Categories/GetSingleCategoryByIdWithProducts/{id}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
             //var apiResponse = JsonConvert.DeserializeObject<CategoryDtos>(responseData);
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
    public async Task<IActionResult> AddCategory([FromBody] CategoryDtos categoryDtosdata)
    {
        string apiUrl = "https://localhost:7197/api/Categories";

        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(categoryDtosdata);
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


    [HttpPut]
    public async Task<IActionResult> UpdateCategory(CategoryUpdateDtos categoryUpdatedata)
    {
        string apiUrl = "https://localhost:7197/api/Categories";

        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(categoryUpdatedata);
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




    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        string apiUrl = $"https://localhost:7197/api/Categories/{id}";

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
