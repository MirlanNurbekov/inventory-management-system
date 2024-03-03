using System;

namespace Digitalkirana.BusinessLogicLayer
{
    public class PurchaseDetailsBLL
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
        public int SupplierId { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public int PurchaseId { get; set; }
    }
}