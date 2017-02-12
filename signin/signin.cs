using System;
using System.Collections.Generic;
using PizzaMore.Data;
using PizzaMore.Models;
using PizzaMore.Utility;

namespace signin
{
    class signin
    {
        private static IDictionary<string, string> RequestParametres;
        private static Header header = new Header();
        static void Main()
        {
            
            if(WebUtil.IsPost())
            {
                LogIn();
            }
            ShowPage();
        }

        private static void LogIn()
        {
            RequestParametres = WebUtil.RetrivePostParametres();
            string email = RequestParametres["email"];
            string password = PasswordHasher.Hash(RequestParametres["password"]);
            UnitOfWork unitOfWork = new UnitOfWork();
            User user = unitOfWork.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                Sesion sesion = new Sesion()
                {
                    User = user
                };
                unitOfWork.Sesions.Add(sesion);
                unitOfWork.Save();
                header.AddCookie(new Cookie("sid", sesion.ID.ToString()));
            }
            else
            {
                //Console.WriteLine("<p>User is not found</p><p>Po hubav dizain ma marzi mnogo trudno bace</p>");
            }
        }
        private static void ShowPage()
        {
            header.Print();
            WebUtil.PrintFileContent("../htdocs/signin.html");
        }
    }
}
