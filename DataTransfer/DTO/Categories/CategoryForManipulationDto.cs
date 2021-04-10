using DataTransfer.DTO.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer.DTO.Categories
{
    public abstract class CategoryForManipulationDto
    {
        [Required(ErrorMessage = "Category Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Category Name is 60 characters.")]
        public string CategoryName { get; set; }

        public IEnumerable<ProductForCreationDto> Products { get; set; }
    }
}
