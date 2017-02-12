using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaMore.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Recipe { get; set; }
        public string ImageUrl { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }
    }
}
