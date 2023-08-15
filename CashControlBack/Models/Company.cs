using System.ComponentModel.DataAnnotations;
namespace CashControl.Models
{
    public class Company
    {
        [Key]

        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }
    }
}