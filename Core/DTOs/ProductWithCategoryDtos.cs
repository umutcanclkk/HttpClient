using Core.Models;

namespace Core.DTOs
{
    public class ProductWithCategoryDto : ProductDtos
    {

        public CategoryDtos Category { get; set; }

    }
}
