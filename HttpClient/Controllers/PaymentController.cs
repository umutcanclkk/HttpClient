using Core.DTOs;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PaymentController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory; //?? throw new ArgumentNullException(nameof(httpClientFactory));
    }




    [HttpGet]
    public async Task<IActionResult> GetPayment()
    {
        string apiUrl = $"https://localhost:7197/api/Payment/";

        try
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<PaymentDtosApiResponse>(responseData);

                    return Ok(apiResponse);
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



    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] PaymentUpdateDtos paymentUpdateDtos)
    {
        string apiUrl = "https://localhost:7197/api/Payment";

        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(paymentUpdateDtos);
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
    public async Task<IActionResult> UpdatePayment([FromBody] PaymentDtos updateData)
    {
        string apiUrl = $"https://localhost:7197/api/Payment/";

        try
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                // Convert your update data to JSON
                string jsonContent = JsonConvert.SerializeObject(updateData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send PUT request
                HttpResponseMessage response = await httpClient.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<PaymentApiResponse>(responseData);

                    return Ok(apiResponse);
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




    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        string apiUrl = $"https://localhost:7197/api/Payment/{id}";

        try
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<PaymentApiResponse>(responseData);

                    return Ok(apiResponse);
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



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaymentById(int id)
    {
        string apiUrl = $"https://localhost:7197/api/Payment/{id}";

        try
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Silme işlemi başarılıysa 204 No Content dönebilirsiniz.
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






    [HttpGet("ByTransactionId")]
    public async Task<IActionResult> GetPaymentByTransactionId(string transactionId)
    {
        try
        {
            // API'nin endpoint'ine gönderilecek URL oluşturulur
            string apiUrl = $"https://localhost:7197/api/Payment/ByTransactionId/{transactionId}";

            // HttpClient kullanımı için using bloğu
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                // GET isteği gönderilir
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // Sunucudan başarılı bir yanıt alındıysa
                if (response.IsSuccessStatusCode)
                {
                    // Yanıtın içeriğini oku
                    string responseData = await response.Content.ReadAsStringAsync();

                    // JSON veriyi C# nesnesine dönüştür
                    var apiResponse = JsonConvert.DeserializeObject<PaymentApiResponse>(responseData);

                    // Başarılı yanıtı döndür
                    return Ok(apiResponse);
                }
                else
                {
                    // Hata durumunda uygun HTTP durumunu döndür
                    return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }
        catch (HttpRequestException ex)
        {
            // HTTP isteği sırasında bir hata oluşursa
            return BadRequest($"HTTP Request Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Diğer istisna durumları için
            return BadRequest($"Exception: {ex.Message}");
        }
    }







}
