using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    /**
     * Cущность Категория
     * @Id - идентификатор категории
     * @CategoryName - Название категории
     * */
    public class Category
    {
        [Column("CategoryId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Category Name is 60 characters.")]
        public string CategoryName { get; set; }
    }
}
