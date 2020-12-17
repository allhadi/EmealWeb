using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeal.Model.ViewModels
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string ExtendedStreetAddress { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public string County { get; set; }
        public int UserId { get; set; }
        public int AddressType { get; set; }
        public string AddressLine => $"{StreetAddress},{ExtendedStreetAddress},{Town},{PostCode}";
    }
}
