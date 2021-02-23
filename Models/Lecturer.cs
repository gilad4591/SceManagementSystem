using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Lecturer
    {
        [Key]
        [Required]
        public int lecIndex { get; set; }
        [Required]
        public string LecId { get; set; }
        [Required]
        public string LecName { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}