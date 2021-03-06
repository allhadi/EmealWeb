﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeal.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "Minimum Length")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int CompanyId { get; set; }
    }
}
