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
        public string StreetAddress { get; set; }

        public string ExtendedStreetAddress { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Town { get; set; }
        [Required(ErrorMessage = "Required")]
        public string County { get; set; }

        [Required(ErrorMessage = "Required")]
        public string PostCode { get; set; }

    }
}
