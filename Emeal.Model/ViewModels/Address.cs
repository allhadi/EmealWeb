using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeal.Model.ViewModels
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public int UserId { get; set; }
        public string AddressLine => $"{Address1},{City},{PostCode}";
    }
}
