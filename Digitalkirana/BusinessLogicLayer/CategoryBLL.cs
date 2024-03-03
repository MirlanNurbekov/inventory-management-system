using System;

namespace Digitalkirana.BusinessLogicLayer
{
    public class CategoryBLL
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
    }
}