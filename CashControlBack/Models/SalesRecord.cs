using System;
namespace CashControl.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int ProductIndex { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductRemainings { get; set; }
        public decimal Price { get; set; }
        public decimal Profit { get; set; }
    }

}
