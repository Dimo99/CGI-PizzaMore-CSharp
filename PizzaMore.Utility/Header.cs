using System;
using System.Text;

namespace PizzaMore.Utility
{
    public class Header
    {
        public Header()
        {
            Type = "Content-Type: text/html";
            Cookies = new CookieCollection();
        }
        public string Type { get; set; }
        public string Location { get; set; }
        public ICookieCollection Cookies { get; set; }

        public void AddLocation(string location)
        {
            Location = $"Location: {location}";
        }

        public void AddCookie(Cookie cookie)
        {
            Cookies.AddCookie(cookie);
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{Type}\n");
            foreach (var cookie in Cookies)
            {
                stringBuilder.Append($"Set-Cookie: {cookie}");
            }
            if (Location != null)
            {
                stringBuilder.Append($"{Location}");
            }
            stringBuilder.Append("\n");
            stringBuilder.Append("\n");
            return stringBuilder.ToString();
        }
    }
}
