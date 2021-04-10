using DataTransfer.DTO.Categories;
using System;

namespace DataTransfer.DTO.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public CategoryDto Category { get; set; }
        public double Price { get; set; }
    }
}
