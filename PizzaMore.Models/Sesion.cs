using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaMore.Models
{
    public class Sesion
    {
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public override string ToString()
        {
            return $"{ID}\t{User.ID}";
        }
    }
}
