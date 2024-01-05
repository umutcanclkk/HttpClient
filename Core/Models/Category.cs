﻿namespace Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
