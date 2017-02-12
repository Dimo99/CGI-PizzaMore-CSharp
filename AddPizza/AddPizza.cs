using System.Collections.Generic;
using PizzaMore.Data;
using PizzaMore.Models;
using PizzaMore.Utility;

namespace AddPizza
{
    class AddPizza
    {
        private static IDictionary<string, string> RetrivePostParametres;
        private static Sesion Sesion;
        private static Header Header = new Header();
        static void Main()
        {
            Sesion = WebUtil.GetSesion();
            if (Sesion == null)
            {
                WebUtil.PageNotAllowed();
            }
            else
            {
                if (WebUtil.IsGet())
                {
                    ShowPage();
                }
                else if(WebUtil.IsPost())
                {
                    RetrivePostParametres = WebUtil.RetrivePostParametres();
                    string title = RetrivePostParametres["title"];
                    string recipe = RetrivePostParametres["recipe"];
                    string url = RetrivePostParametres["url"];
                    Pizza pizza = new Pizza()
                    {
                        Title = title,
                        Recipe = recipe,
                        ImageUrl = url
                    };
                    UnitOfWork unitOfWork = new UnitOfWork();
                    unitOfWork.Pizza.Add(pizza);
                    unitOfWork.Users.First(u=>u.ID==Sesion.UserId).Pizzas.Add(pizza);
                    unitOfWork.Save();
                    ShowPage();
                }
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../htdocs/addpizza.html");
        }
    }
}
