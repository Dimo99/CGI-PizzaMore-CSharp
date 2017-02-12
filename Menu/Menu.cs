using System;
using System.Collections.Generic;
using PizzaMore.Data;
using PizzaMore.Models;
using PizzaMore.Utility;

namespace Menu
{
    class Menu
    {
        private static IDictionary<string, string> RequestParameters;
        private static Sesion Sesion;
        private static Header Header = new Header();
        static void Main()
        {
            Sesion = WebUtil.GetSesion();
            if (Sesion == null)
            {
                Header.Print();
                WebUtil.PageNotAllowed();
            }
            else
            {
                if (WebUtil.IsGet())
                {
                        ShowPage();
                }
                else if (WebUtil.IsPost())
                {
                    VoteForPizza();
                    ShowPage();
                }
            }


        }

        private static void VoteForPizza()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            RequestParameters = WebUtil.RetrivePostParametres();
            string pizzaVote = RequestParameters["pizzaVote"];
            int pizzaId = int.Parse(RequestParameters["pizzaid"]);
            Pizza pizza = unitOfWork.Pizza.GetById(pizzaId);
            if (pizzaVote == "up")
            {
                pizza.UpVotes++;
            }
            else if (pizzaVote == "down")
            {
                pizza.DownVotes++;
            }
            unitOfWork.Save();
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../htdocs/menu-top.html");
            GenerateNavbar();
            GenerateAllSuggestions();
            WebUtil.PrintFileContent("../htdocs/menu-bottom.html");
        }

        private static void GenerateAllSuggestions()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            IEnumerable<Pizza> pizzas = unitOfWork.Pizza.GetAll();
            Console.WriteLine("<div class=\"card-deck\">");
            foreach (var pizza in pizzas)
            {
                Console.WriteLine("<div class=\"card\">");
                Console.WriteLine($"<img class=\"card-img-top\" src=\"{pizza.ImageUrl}\" width=\"200px\"alt=\"Card image cap\">");
                Console.WriteLine("<div class=\"card-block\">"); Console.WriteLine($"<h4 class=\"card-title\">{pizza.Title}</h4>");
                Console.WriteLine($"<p class=\"card-text\"><a href=\"DetailsPizza.exe?pizzaid={pizza.Id}\">Recipe</a></p>");
                Console.WriteLine("<form method=\"POST\">");
                Console.WriteLine($"<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"up\">Up</label></div>"); Console.WriteLine($"<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"down\">Down</label></div>"); Console.WriteLine($"<input type=\"hidden\" name=\"pizzaid\" value=\"{pizza.Id}\" />");
                Console.WriteLine("<input type=\"submit\" class=\"btn btn-primary\" value=\"Vote\" />");
                Console.WriteLine("</form>");
                Console.WriteLine("</div>");
                Console.WriteLine("</div>");
            }
            Console.WriteLine("</div>");

        }

        private static void GenerateNavbar()
        {
            if (Sesion.User != null)
            {
                Console.WriteLine("<nav class=\"navbar navbar-default\">" +
                                  "<div class=\"container-fluid\">" +
                                  "<div class=\"navbar-header\">" +
                                  "<a class=\"navbar-brand\" href=\"Home.exe\">PizzaMore</a>" +
                                  "</div>" +
                                  "<div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\">" +
                                  "<ul class=\"nav navbar-nav\">" +
                                  "<li ><a href=\"AddPizza.exe\">Suggest Pizza</a></li>" +
                                  "<li><a href=\"YourSuggestions.exe\">Your Suggestions</a></li>" +
                                  "</ul>" +
                                  "<ul class=\"nav navbar-nav navbar-right\">" +
                                  "<p class=\"navbar-text navbar-right\"></p>" +
                                  "<p class=\"navbar-text navbar-right\"><a href=\"Home.exe?logout=true\" class=\"navbar-link\">Sign Out</a></p>" +
                                  $"<p class=\"navbar-text navbar-right\">Signed in as {Sesion.User.Email}</p>" +
                                  "</ul> </div></div></nav>");
            }
            else
            {
                Console.WriteLine("<nav class=\"navbar navbar-default\">" +
                                  "<div class=\"container-fluid\">" +
                                  "<div class=\"navbar-header\">" +
                                  "<a class=\"navbar-brand\" href=\"Home.exe\">PizzaMore</a>" +
                                  "</div>" +
                                  "<div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\">" +
                                  "<ul class=\"nav navbar-nav\">" +
                                  "<li ><a href=\"AddPizza.exe\">Suggest Pizza</a></li>" +
                                  "<li><a href=\"YourSuggestions.exe\">Your Suggestions</a></li>" +
                                  "</ul>" +
                                  "<ul class=\"nav navbar-nav navbar-right\">" +
                                  "<p class=\"navbar-text navbar-right\"></p>" +
                                  "<p class=\"navbar-text navbar-right\"><a href=\"Home.exe?logout=true\" class=\"navbar-link\">Sign Out</a></p>" +
                                  $"<p class=\"navbar-text navbar-right\">Signed in as </p>" +
                                  "</ul> </div></div></nav>");
            }
        }
    }
}
