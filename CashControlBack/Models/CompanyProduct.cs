using System;

namespace CashControlBack.Models
{
    public class CompanyProduct
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public int ProductQuantity { get; set; }

    public int ProductRemainings { get; set; }

    public decimal SellingPrice { get; set; }
    public decimal Profit { get; set; }

    public decimal CalculateProfit()
    {
        return (ProductQuantity - ProductRemainings) * SellingPrice; //this equals the profit
    }
}

}

