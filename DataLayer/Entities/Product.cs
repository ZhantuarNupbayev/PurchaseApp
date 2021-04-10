using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    /**
    * Cущность Продукт
    * @Id - идентификатор продукта
    * @ProductName - название продукта
    * @CategoryId - внешний ключ к сущности Категория
    * @Category - объект для внешнего ключа
    * @Price - цена 1 единицы продукта
    * */
    public class Product
    {
        [Column("ProductId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Product Name is 30 characters.")]
        public string ProductName { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        [Required(ErrorMessage = "Price is a required field.")]
        public double Price { get; set; }
    }
}
