using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Exam
    {
        [Key]
        [Required]
        public int ExamIndex { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        [RegularExpression("^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$", ErrorMessage = "date invalid. format: dd/mm/yyyy")]
        public string MoedADate { get; set; }
        [Required]
        [RegularExpression("^(f|s|g)[-]([0][0-9]|[1-2][0-9]{2})$", ErrorMessage = "class invalid. format: (f/s/g - XXX)")]
        public string ClassA { get; set; }
        [Required]
        [RegularExpression("^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$", ErrorMessage = "date invalid. format: dd/mm/yyyy")]
        public string MoedBDate { get; set; }
        [Required]
        [RegularExpression("^(f|s|g)[-]([0][0-9]|[1-2][0-9]{2})$", ErrorMessage = "class invalid. format: (f/s/g - XXX)")]
        public string ClassB { get; set; }
        [RegularExpression("(([0-9])|([1-9][0-9])|([1][0][0]))$",ErrorMessage = "grade should be between 0-100")]
        public string Grade { get; set; }

    }
}