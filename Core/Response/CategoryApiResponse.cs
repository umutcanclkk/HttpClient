using Core.DTOs;

namespace Core.Response
{
    public class CategoryApiResponse
    {
        public CategoryDtos data { get; set; }
        public object Errors { get; set; }
        public CategoryUpdateDtos updateDtosdata { get; set; }
    }
}
