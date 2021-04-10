using System.ComponentModel.DataAnnotations;

namespace DataTransfer.DTO.Products
{
    public abstract class ProductForManipulationDto
    {
        [Required(ErrorMessage = "Product Name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Product Name is 30 characters.")]
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
