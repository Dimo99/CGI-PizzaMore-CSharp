﻿using System.Collections.Generic;

namespace PizzaMore.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
