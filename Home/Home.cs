using System;
using PizzaMore.Data;
using PizzaMore.Models;
using PizzaMore.Utility;

namespace Home
{
    using System.Collections.Generic;

    internal class Home
    {
        private static IDictionary<string, string> RequestParameters;
        private static Sesion Session;
        private static Header Header = new Header();
        private static string Language;

        static void Main()
        {
            AddDefaultLanguageCookie();

            if (WebUtil.IsGet())
            {
                RequestParameters = WebUtil.RetriveGetParameters();
                TryLogOut();
                Language = WebUtil.GetCookies()["lang"].Value;
            }
            else if (WebUtil.IsPost())
            {
                RequestParameters = WebUtil.RetrivePostParametres();
                Header.AddCookie(new Cookie("lang", RequestParameters["language"]));
                Language = RequestParameters["language"];
            }

            ShowPage();
        }

        private static void TryLogOut()
        {
            if (RequestParameters.ContainsKey("logout"))
            {
                if (RequestParameters["logout"] == "true")
                {
                    Session = WebUtil.GetSesion();
                    UnitOfWork unitOfWork = new UnitOfWork();
                    var s = unitOfWork.Sesions.GetById(Session.ID);
                    unitOfWork.Sesions.Delete(s);
                    unitOfWork.Save();

                }
            }
        }

        private static void AddDefaultLanguageCookie()
        {
            if (!WebUtil.GetCookies().ContainsKey("lang"))
            {
                Header.AddCookie(new Cookie("lang", "EN"));
                Language = "EN";
                ShowPage();
            }
        }

        private static void ShowPage()
        {
            Console.WriteLine(Header.ToString());
            if (Language.Equals("BG"))
            {
                ServeHtmlBg();
            }
            else
            {
                
                ServeHtmlEn();
            }
        }

        private static void ServeHtmlBg()
        {
            WebUtil.PrintFileContent("../htdocs/home.html");
        }

        private static void ServeHtmlEn()
        {
            WebUtil.PrintFileContent("../htdocs/home-en.html");
        }

    }

}