using System.Collections.Generic;
using PizzaMore.Data;
using PizzaMore.Models;
using PizzaMore.Utility;

namespace Signup
{
    class SignUp
    {
        private static IDictionary<string, string> RequestParametres;
        private static Header Header = new Header();
        static void Main()
        {
            if (WebUtil.IsPost())
            {
                RegisterUser();
            }
            ShowPage();
        }

        private static void RegisterUser()
        {
            RequestParametres = WebUtil.RetrivePostParametres();
            string email = RequestParametres["email"];
            string password = PasswordHasher.Hash(RequestParametres["password"]);
            PizzaMoreContext unitOfWork = new PizzaMoreContext();
            unitOfWork.Users.Add(new User()
            {
                Email = email,
                Password = password
            });
            unitOfWork.SaveChanges();
        }
        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../htdocs/signup.html");
        }
    }
}
