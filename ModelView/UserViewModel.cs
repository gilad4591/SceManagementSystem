using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Models;
namespace MVC.ModelView
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<User> Users { get; set; }
    }
}