using System;

namespace DataTransfer.DTO.Purchases
{
    public class PurchaseForUpdateDto : PurchaseForManipulationDto
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
