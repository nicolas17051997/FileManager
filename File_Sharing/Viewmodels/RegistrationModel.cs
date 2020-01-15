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
        public string Email { get; set; }
        [Required(ErrorMessage = "Password not specied")]
        public string Pasword { get; set; }
        [Required(ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Name not specified")]
        public string Name { get; set; }
        [Required(ErrorMessage ="LastName not specified")]
        public string LastName { get; set; }
    }
}
