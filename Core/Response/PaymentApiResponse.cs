using Core.DTOs;

namespace Core.Response
{
    public class PaymentApiResponse
    {
        public PaymentDtos Data { get; set; }
        public object Errors { get; set; }
        public PaymentUpdateDtos DataUpdate { get; set; }
    }
}





