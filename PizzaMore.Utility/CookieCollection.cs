using System.Collections;
using System.Collections.Generic;

namespace PizzaMore.Utility
{
    class CookieCollection : ICookieCollection
    {
        private Dictionary<string, Cookie> dictionary;

        public CookieCollection()
        {
            dictionary = new Dictionary<string, Cookie>();
        }

        public void AddCookie(Cookie cookie)
        {
            dictionary.Add(cookie.Name,cookie);
        }

        public void RemoveCookie(string cookieName)
        {
            dictionary.Remove(cookieName);
        }

        public bool ContainsKey(string key)
        {
            return dictionary.ContainsKey(key);
        }

        public int Count => dictionary.Count;

        public Cookie this[string key]
        {
            get { return dictionary[key]; }
            set { dictionary[key] = value; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            foreach (var keyValuePair in dictionary)
            {
                yield return keyValuePair.Value;
            }
        }
    }

}
