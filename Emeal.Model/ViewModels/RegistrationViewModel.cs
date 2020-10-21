using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeal.Model.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "Minimum Length")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "Minimum Length")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password not Match")]
        [Display (Name="Confirm Password")]
        public string ConfirmPassword { get; set; }

        public int CompanyId = 1;
    }
}
