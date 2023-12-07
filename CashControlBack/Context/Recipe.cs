using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CashControl.Context
{
    public class Recipe
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(150)]
        public string Title { get; set; }

        public string Difficulty { get; set; }

        public int Chef_ID { get; set; }

        [ForeignKey("Chef_ID")]
        public Chef AssociatedChef { get; set; }
    }
}
