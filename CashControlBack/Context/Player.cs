using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CashControl.Context
{
    public class Player
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public int Number { get; set; }

        public DateTime? BirthYear { get; set; }

        public int Team_ID { get; set; }

        [ForeignKey("Team_ID")]
        public Team AssociatedTeam { get; set; }
    }
}
