using CollegeApp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDto
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage ="student name is required")]
        [StringLength(40)]
        public string StudentName { get; set; }
        
        //[Range(10,20)]
        //public int Age { get; set; }

        [EmailAddress(ErrorMessage = "pls enter valid email")]
        public string Email { get; set; }

        //public string Password { get; set; }
        //[Compare(nameof(Password))]
        //public string Confirmpassword { get; set; }
        //[Required]
        public string Address { get; set; }
        //[DateCheck]
        //public DateTime AdmissionDate { get; set; }

        public DateTime Dob { get; set; }
    }

}
