using DataTransfer.DTO.Products;
using System;

namespace DataTransfer.DTO.Purchases
{
    public class PurchaseDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime BuyDate { get; set; }
        public double TotalPrice { get; set; }      
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
