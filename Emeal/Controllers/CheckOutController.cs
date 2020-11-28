using Emeal.Model;
using Emeal.Model.ViewModels;
using Emeal.OrderApi;
using Emeal.Security;
using RestSharp;
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
            var apiUrl = "https://localhost:44324";
            var method = "/api/Address/Get";
            var apiClient = new ApiClient(apiUrl);
            var response = apiClient.GetList<Address>(method);

            return response;
           
        }
    }
}