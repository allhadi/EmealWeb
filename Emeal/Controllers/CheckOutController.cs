using Emeal.Common;
using Emeal.Model;
using Emeal.Model.ApiModels;
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
using static Emeal.Common.Enums;

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
            var addressList = GetAddresses(AddressType.DeliveryAddress);
            return PartialView("Address", new AddressListViewModel()
            {
                Addresses = addressList,
                AddressType = Common.Enums.AddressType.DeliveryAddress
            });
        }
        public ActionResult BillingAddress()
        {
            var addressList = GetAddresses(AddressType.BillingAddress);
            return PartialView("Address", new AddressListViewModel()
            {
                Addresses = addressList,
                AddressType = Common.Enums.AddressType.BillingAddress
            });
        }

        private List<CustomerAddress> GetAddresses(AddressType addressType) 
        {
            var apiUrl = "https://localhost:44324";
            var method = "/api/Address/Get";
            var apiClient = new ApiClient(apiUrl);
            var response = apiClient.GetList<CustomerAddress>(method);
            return response.Where(x=>x.AddressType == (int)addressType).ToList();
           
        }
        [HttpGet]
        public ActionResult AddressForm(Enums.AddressType id)
        {
            return PartialView(new AddressViewModel { AddressType = id });
        }

        [HttpPost]
        public ActionResult AddDeliveryAddress(AddressViewModel models)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            var addressList = GetAddresses(AddressType.BillingAddress);
            AddAddress(models, AddressType.DeliveryAddress);
            if (!addressList.Any())
            {
                AddAddress(models, AddressType.BillingAddress);
            }
            return CurrentUmbracoPage();
        }

        [HttpPost]
        public ActionResult AddBillingAddress(AddressViewModel models)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            AddAddress(models, AddressType.BillingAddress);
            return CurrentUmbracoPage();
        }

        [HttpPost]
        public ActionResult DeleteAddress(int id)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            var apiUrl = "https://localhost:44324";
            var method = "/api/Address/Delete";
            var dataModel = new AddressDto()
            {
                Id = id
            };
            var apiClient = new ApiClient(apiUrl);
            var response = apiClient.Post<ApiResult, AddressDto>(method, dataModel);
            return CurrentUmbracoPage();
        }

        private void AddAddress(AddressViewModel models, AddressType addressType)
        {
            var apiUrl = "https://localhost:44324";
            var method = "/api/Address/Save";
            var dataModel = new AddressDto()
            {
                StreetAddress = models.StreetAddress,
                ExtendedStreetAddress = models.ExtendedStreetAddress,
                Town = models.Town,
                County = models.County,
                PostCode = models.PostCode,
                AddressType = (int)addressType

            };
            var apiClient = new ApiClient(apiUrl);
            var response = apiClient.Post<ApiResult, AddressDto>(method, dataModel);
        }
    }
}