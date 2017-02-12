using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using PizzaMore.Data;
using PizzaMore.Models;

namespace PizzaMore.Utility
{
    public static class WebUtil
    {
        public static bool IsPost()
        {
            string method = Environment.GetEnvironmentVariable("REQUEST_METHOD");
            if (method == "POST")
            {
                return true;
            }
            return false;
        }
        public static bool IsGet()
        {
            string method = Environment.GetEnvironmentVariable("REQUEST_METHOD");
            if (method == "GET")
            {
                return true;
            }
            return false;
        }

        public static IDictionary<string, string> RetriveGetParameters()
        {
            string value = WebUtility.UrlDecode(Environment.GetEnvironmentVariable("QUERY_STRING"));
            return RetriveRequestParametres(value);
        }

        public static IDictionary<string, string> RetrivePostParametres()
        {
            string value = WebUtility.UrlDecode(Console.ReadLine());
            return RetriveRequestParametres(value);
        }

        public static ICookieCollection GetCookies()
        {
            CookieCollection cookies = new CookieCollection();
            string cookieHeader = Environment.GetEnvironmentVariable("HTTP_COOKIE");
            if (cookieHeader == null)
            {
                return cookies;
            }
            string []cookiesData = cookieHeader.Split(';');
            foreach (var cookieString in cookiesData)
            {
                string cookieName = cookieString.Split('=').Select(x=>x.Trim()).ToArray()[0];
                string cookieValue = cookieString.Split('=').Select(x => x.Trim()).ToArray()[1];
                Cookie cookie =  new Cookie(cookieName,cookieValue);
                cookies.AddCookie(cookie);
            }
            return cookies;
        }

        public static Sesion GetSesion()
        {
            ICookieCollection cookies = GetCookies();
            if (cookies.ContainsKey("sid"))
            {
                Cookie cookie = cookies["sid"];
                UnitOfWork unitOfWork = new UnitOfWork();
                int cookieValue = int.Parse(cookie.Value);
                return unitOfWork.Sesions.FirstOrDefault(s => s.ID == cookieValue);
            }
            return null;
        }

        public static void PrintFileContent(string path)
        {
            string text = File.ReadAllText(path, Encoding.Default);
            Console.WriteLine(text);
            
        }

        public static void PageNotAllowed()
        {
            PrintFileContent("../htdocs/PageNotAllowed/index.html");
        }
        private static IDictionary<string, string> RetriveRequestParametres(string value)
        {
            Logger.Log(value);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] parameters = value.Split('&');
            if (parameters[0].Contains("="))
            {
                foreach (var parameter in parameters)
                {
                    string[] keyValuePair = parameter.Split('=');
                    dictionary.Add(keyValuePair[0], keyValuePair[1]);
                }
            }
            return dictionary;
        }
    }
}
