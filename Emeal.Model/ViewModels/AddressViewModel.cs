using Emeal.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeal.Model.ViewModels
{
    public class AddressViewModel
    {
        public Enums.AddressType AddressType { set; get; }
        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PostCode { get; set; }
    }
}
