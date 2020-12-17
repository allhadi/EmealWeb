using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Emeal.Common.Enums;

namespace Emeal.Model.ViewModels
{
    public class AddressListViewModel
    {
        public AddressType AddressType { set; get; }
        public List<CustomerAddress> Addresses { set; get; }

        public AddressListViewModel()
        {
            Addresses = new List<CustomerAddress>(); 
        }
    }
}
