using System.ComponentModel.DataAnnotations;

namespace CashControl.Context
{
    public class Chef
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]

        public int BirthYear { get; set; }
    }
}
