using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

    }
}