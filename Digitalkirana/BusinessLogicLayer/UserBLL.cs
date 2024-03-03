using System;

namespace Digitalkirana.BusinessLogicLayer
{
    public class UserBLL
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string UserType { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }
        public bool Active { get; set; }
    }
}