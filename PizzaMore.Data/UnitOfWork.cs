using PizzaMore.Data.Interface;
using PizzaMore.Models;

namespace PizzaMore.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PizzaMoreContext context;
        private IRepository<User> users;
        private IRepository<Sesion> sesions;
        private IRepository<Pizza> pizza;

        public IRepository<User> Users => this.users??(this.users=new Repository<User>(this.context.Users));

        public IRepository<Sesion> Sesions => sesions??(this.sesions=new Repository<Sesion>(this.context.Sesions));

        public IRepository<Pizza> Pizza => pizza ?? (this.pizza=new Repository<Pizza>(this.context.Pizzas));

        public UnitOfWork()
        {
            context = new PizzaMoreContext();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
