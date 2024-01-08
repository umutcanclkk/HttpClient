using Core.Models;

namespace Core.DTOs
{
    public class CategoryDtos
    {
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
