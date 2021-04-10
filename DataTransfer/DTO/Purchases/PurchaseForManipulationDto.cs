using System.ComponentModel.DataAnnotations;
using System;
using DataTransfer.DTO.Products;

namespace DataTransfer.DTO.Purchases
{
    public abstract class PurchaseForManipulationDto
    {
        [Required(ErrorMessage = "Quantity is a required field.")]
        public int Quantity { get; set;  }

        [Required(ErrorMessage = "Buy date is a required field.")]
        public DateTime BuyDate { get; set; }

        [Required(ErrorMessage = "Total Price is a required field.")]
        public double TotalPrice { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
    }
}
