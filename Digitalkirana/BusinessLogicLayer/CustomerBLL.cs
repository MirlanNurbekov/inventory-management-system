using System;

namespace Digitalkirana.BusinessLogicLayer
{
    public class CustomerBLL
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
    }

    public class ParkingBLL
    {
        public int ParkingId { get; set; }
        public int CustomerId { get; set; }
        public CustomerBLL Customer { get; set; }
        public int SlotNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }

        public ParkingBLL()
        {
            Customer = new CustomerBLL();
        }

        // Calculates the total hours parked
        public double CalculateParkingDuration()
        {
            TimeSpan duration = EndTime.Subtract(StartTime);
            return duration.TotalHours;
        }

        // Calculates the total parking fee based on a fixed rate per hour
        public decimal CalculateParkingFee(decimal ratePerHour)
        {
            double hours = CalculateParkingDuration();
            return (decimal)hours * ratePerHour;
        }
    }
}
