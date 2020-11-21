using Emeal.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Emeal.Controllers
{
    [Authorize]
    public class CheckOutController : SurfaceController
    {
        public CheckOutController()
        {

        }
        // GET: CheckOut
        public ActionResult DeliveryAddress()
        {
            var addressList = GetAddresses();
            return PartialView("Address", new AddressListViewModel()
            {
                Addresses = addressList,
                AddressType = Common.Enums.AddressType.DeliveryAddress
            });
        }
        public ActionResult BillingAddress()
        {
            var addressList = GetAddresses();
            return PartialView("Address", new AddressListViewModel()
            {
                Addresses = addressList,
                AddressType = Common.Enums.AddressType.BillingAddress
            });
        }

        private List<Address> GetAddresses() 
        {

            return new List<Address>()
            {
                new Address()
                {
                     Address1 = "abc",
                      City = "Dhaka",
                       PostCode = "3200"
                },
                new Address()
                {
                     Address1 = "wer",
                      City = "Sylhet",
                       PostCode = "3200"
                }
            };
        }
    }
}