using Core.DTOs;

namespace Core.Response
{
    public class ProductApiResponse
    {
        public ProductDtos data { get; set; }
        public object Errors { get; set; }
        public ProductUpdateDtos UpdateData { get; set; }
    }
}
