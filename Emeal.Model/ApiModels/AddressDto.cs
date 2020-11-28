using System;
using System.Collections.Generic;
using System.Text;

namespace Emeal.Model
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public int UserId { get; set; }
    }
}
