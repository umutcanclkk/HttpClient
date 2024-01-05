using Core.DTOs;

public class ApiResponse
{

    public List<CustomerDtos> data { get; set; }
    public object errors { get; set; }
    public CustomerUpdateDtos updatedata { get; set; }

   

}