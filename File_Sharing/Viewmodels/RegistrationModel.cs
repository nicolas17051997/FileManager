using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace File_Sharing.Viewmodels
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Email not specified")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pasword { get; set; }


        [Required(ErrorMessage = "Passwords do not match")]
        [Compare("Password", ErrorMessage = "Password not specied")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm your password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage ="Name not specified")]
        public string Name { get; set; }

        [Required(ErrorMessage ="LastName not specified")]
        public string LastName { get; set; }
    }
}
