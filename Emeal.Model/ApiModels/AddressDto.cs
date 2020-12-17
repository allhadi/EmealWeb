using System;
using System.Collections.Generic;
using System.Text;
using static Emeal.Common.Enums;

namespace Emeal.Model
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string ExtendedStreetAddress { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public int UserId { get; set; }
        public int AddressType { set; get; }
    }
}
