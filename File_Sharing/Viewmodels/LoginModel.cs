using System;
using System.ComponentModel.DataAnnotations;

namespace File_Sharing.Viewmodels
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email not specified")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password not specified")]
        public string Password { get; set; }
        
        
    }
}
