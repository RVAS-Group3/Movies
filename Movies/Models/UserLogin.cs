using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class UserLogin
    {
        [Display(Name = "Username or E-mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Email Required")]

        public string email { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        public string password { get; set; }

        [Display(Name = "Remember Me")]
        public bool rememberMe { get; set; }
    }
}
