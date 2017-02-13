using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMore.Data;
using PizzaMore.Models;
using PizzaMore.Utility;

namespace YourSuggestions
{
    class YourSuggestions
    {
        private static IDictionary<string, string> RequestParameters;
        private static Sesion Sesion = WebUtil.GetSesion();
        private static Header Header  = new Header();
        static void Main()
        {
            
            if(WebUtil.IsPost())
            {
                DeletePizza();
            }
            ShowPage();
        }

        private static void DeletePizza()
        {
            RequestParameters = WebUtil.RetrivePostParametres();
            UnitOfWork unitOfWork = new UnitOfWork();
            User user = unitOfWork.Users.GetById(Sesion.UserId);
            Pizza pizza = user.Pizzas.First(p=>p.Id==int.Parse(RequestParameters["pizzaId"]));
            unitOfWork.Pizza.Delete(pizza);
            unitOfWork.Save();
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../htdocs/yoursuggestions-top.html");
            PrintListOfSuggestedItems();
            WebUtil.PrintFileContent("../htdocs/yoursuggestions-bottom.html");
        }

        private static void PrintListOfSuggestedItems()
        {
            ICollection<Pizza> pizzas = new List<Pizza>();
            pizzas = Sesion.User.Pizzas;
            Console.WriteLine("<ul>");
            foreach (var pizza in pizzas)
            {
                Console.WriteLine("<form method=\"POST\">");
                Console.WriteLine($"<li><a href=\"DetailsPizza.exe?pizzaid={pizza.Id}\">{pizza.Title}</a> <input type=\"hidden\" name=\"pizzaId\" value=\"{pizza.Id}\"/> <input type=\"submit\" class=\"btn btn-sm btn-danger\" value=\"X\"/></li>");
                Console.WriteLine("</form>");
            }
            Console.WriteLine("</ul>");
        }
    }
}
