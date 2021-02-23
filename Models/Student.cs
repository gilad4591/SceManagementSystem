using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int studIndex { get; set; }
        [Required]
        public string StudId { get; set; }
        [Required]
        public string StudName { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}