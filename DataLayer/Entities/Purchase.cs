using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    /**
    * Cущность Покупка / Приобретение
    * @Id - идентификатор покупки
    * @Quantity - количество купленного продукта
    * @DateCreated - дата добавления покупки
    * @DateUpdated - дата обновления данных покупки
    * @BuyDate - дата покупки
    * @TotalPrice - полная стоимость покупки (цена продукта * количество купленного продукта)
    * @ProductId - внешний ключ к сущности Продукт
    * @Product - объект для внешнего ключа
    * @UserId - внешний ключ к сущности Пользователь
    * @User - объект для внешнего ключа
    * */
    public class Purchase
    {
        [Column("PurchaseId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Quantity is a required field.")]
        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        [Required(ErrorMessage = "Buy date is a required field.")]
        public DateTime BuyDate { get; set; }

        [Required(ErrorMessage = "Total Price is a required field.")]
        public double TotalPrice { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }        
        public Product Product { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
