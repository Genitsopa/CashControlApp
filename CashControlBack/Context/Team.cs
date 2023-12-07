using System.ComponentModel.DataAnnotations;

namespace CashControl.Context
{
    public class Team
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

    }
}
