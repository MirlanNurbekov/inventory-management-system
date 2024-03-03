using System;
namespace Digitalkirana.BusinessLogicLayer
{
    public class SupplierBLL
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
    }
}