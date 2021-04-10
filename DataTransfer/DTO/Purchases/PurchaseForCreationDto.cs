using System;

namespace DataTransfer.DTO.Purchases
{
    public class PurchaseForCreationDto : PurchaseForManipulationDto
    {
        public DateTime DateCreated { get; set; }
    }
 
}
