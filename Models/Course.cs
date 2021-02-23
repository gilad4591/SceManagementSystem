using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MVC.Models
{
    public class Course
    {
        [Key]
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        [RegularExpression("sunday|monday|tuesday|wednsday|thursday|friday", ErrorMessage = "day not valid, try to enter day in a small leters")]
        public string Day { get; set; }
        [Required]
        [RegularExpression("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$" , ErrorMessage = "Time not valid")]
        public string Hour { get; set; }
        [Required]
        [RegularExpression("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Time not valid")]
        public string Finish { get; set; }
        [Required]
        public string Class { get; set; }
    }
}