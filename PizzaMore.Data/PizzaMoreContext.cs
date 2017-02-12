using System.Data.Entity;
using PizzaMore.Models;

namespace PizzaMore.Data
{
    public class PizzaMoreContext : DbContext
    {
        public PizzaMoreContext()
            : base("name=PizzaMoreContext")
        {
            
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Sesion> Sesions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}