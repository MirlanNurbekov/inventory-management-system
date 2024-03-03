using System;

namespace Digitalkirana.BusinessLogicLayer
{
    public class ProductBLL
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
    }
}