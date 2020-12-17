namespace Emeal.Controllers
{
    using Braintree;
    using Emeal.Common;
    using Emeal.Model;
    using Emeal.OrderApi;
    using Emeal.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.PublishedModels;
    using static Emeal.Common.Enums;
    using DiscountSetting = Emeal.Model.DiscountSetting;

    /// <summary>
    /// Defines the <see cref="CartController" />.
    /// </summary>
    public class CartController : SurfaceController
    {
        /// <summary>
        /// Defines the _discountList.
        /// </summary>
        internal List<Umbraco.Web.PublishedModels.DiscountSetting> _discountList = new List<Umbraco.Web.PublishedModels.DiscountSetting>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        public CartController()
        {
        }

        /// <summary>
        /// The AddDiscounts.
        /// </summary>
        private void AddDiscounts()
        {
            //Get Discount Setting From Umbraco Model
            var home = Umbraco.ContentAtRoot().FirstOrDefault();
            var setting = home.Descendants<Setting>("Setting").FirstOrDefault();
            List<Umbraco.Web.PublishedModels.DiscountSetting> tempDiscountSettings = setting.DiscountSetting.ToList();
            Cart cart = CartSession;
            foreach (Umbraco.Web.PublishedModels.DiscountSetting item in tempDiscountSettings)
            {
                cart.DiscountSettings.Add(new DiscountSetting()
                {
                    //Add Settings
                    DiscountAmount = decimal.Parse(item.DiscountAmount),
                    DiscountName = item.DiscountName,
                    MinOrder = decimal.Parse(item.MinOrder),
                    DiscountCode = item.DiscountCode,
                    OrderType = Utility.GetEnumValueFromDescription<Enums.DiscountorderType>(item.OrderType),
                    StartingDate = item.StartingDate,
                    ExpirationDate = item.ExpirationDate
                });
            }

            CartSession = cart;
        }

        /// <summary>
        /// The GetDiscountCode.
        /// </summary>
        /// <param name="discountCode">The discountCode<see cref="string"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        public ActionResult GetDiscountCode(string discountCode)
        {
            //Get Discount From Front End and Set the Code if it is Valid

            Cart cart = CartSession;
            if (cart.DiscountCodeValidation(discountCode))
            {
                cart.DiscountCode = discountCode;
            }
            CartSession = cart;

            return null;
        }

        /// <summary>
        /// The OrderType.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult OrderType()
        {
            //Get Customer Order Type

            Cart cart = CartSession;
            cart.OrderType = cart.OrderType == Common.Enums.OrderType.Collection ? Common.Enums.OrderType.Delivery : Common.Enums.OrderType.Collection;
            CartSession = cart;
            return null;
        }

        /// <summary>
        /// The Add.
        /// </summary>
        /// <param name="Id">The Id<see cref="int"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Add(int Id)
        {
            // Add item in cart

            Cart cart = CartSession;
            var pro = Umbraco.Content(Id);
            cart.Add(new CartLine()
            {
                Id = pro.Id,
                Quantity = 1,
                Name = pro.Name,
                Price = (double)pro.Value<decimal>("Price")
            });
            CartSession = cart;
            return null;
        }

        /// <summary>
        /// The QtyAdd.
        /// </summary>
        /// <param name="Id">The Id<see cref="int"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult QtyAdd(int Id)
        {
            //Add Quantity in Cart

            int index = Id;
            Cart cart = CartSession;
            cart.QtyAdd(index);
            CartSession = cart;
            return null;
        }

        /// <summary>
        /// The QtySubtract.
        /// </summary>
        /// <param name="Id">The Id<see cref="int"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult QtySubtract(int Id)
        {
            //Subtract Quantity in Cart

            int index = Id;
            Cart cart = CartSession;
            cart.QtySubtract(index);
            CartSession = cart;
            return null;
        }

        /// <summary>
        /// The Basket.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Basket()
        {
            Cart cart = CartSession;
            var address = GetAddresses(AddressType.BillingAddress);
            if (address != null && address.Any())
            {
                cart.BillingAddress = address.FirstOrDefault().Id;
            }
            return PartialView(cart);
        }
        public ActionResult BraintreePayment(FormCollection collection)
        {
            Cart cart = CartSession;
            if(cart.BillingAddress == 0)
            {
                return CurrentUmbracoPage();
            }

            //Save order to database via API 
            var addressList = GetAddresses(AddressType.BillingAddress);
            var billingAddresss = addressList.FirstOrDefault(x=>x.Id==cart.BillingAddress);
            var user = AuthenticateTicket.GetCurrentUser();
            var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "zwcjj4ss488mzsy7",
                PublicKey = "yddq2ybzjk5xy2z5",
                PrivateKey = "5917ab582f1790eb830644e90927e1fa"
            };
            //save cart to database 
            //payment method , payment status , payment type , cart 
            //use the order in trans
            string nonceFromTheClient = collection["payment_method_nonce"];
            var request = new TransactionRequest()
            {
                BillingAddress = new AddressRequest()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    StreetAddress = billingAddresss.StreetAddress,
                    ExtendedAddress = billingAddresss.ExtendedStreetAddress,
                    Locality = billingAddresss.Town,
                    PostalCode = billingAddresss.PostCode,
                    CountryCodeAlpha2 = "GB"
                },
                OrderId = "Todo",
                Amount =Convert.ToDecimal(cart.GrandTotal),
                Channel = "Emeal",
                PaymentMethodNonce = nonceFromTheClient,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                    PayeeEmail = user.Email
                }
            };
            Result<Transaction> result = gateway.Transaction.Sale(request);
            //check trans status if successful update order status to paid 
            // remove cart from session 
            //send confr email to customer and company.
            // forward customer a thank you page 
            return CurrentUmbracoPage();
        }

        private List<Emeal.Model.ViewModels.CustomerAddress> GetAddresses(AddressType addressType)
        {
            var apiUrl = "https://localhost:44324";
            var method = "/api/Address/Get";
            var apiClient = new ApiClient(apiUrl);
            var response = apiClient.GetList<Emeal.Model.ViewModels.CustomerAddress>(method);
            return response.Where(x => x.AddressType == (int)addressType).ToList();
        }

        public ActionResult ChangeAddress(string addressType, int id)
        {
            Cart cart = CartSession;
            if (addressType == "Delivery")
            {
                cart.DeliveryAddress = id;
            }
            else
            {
                cart.BillingAddress = id;
            }
            CartSession = cart;
            return null;
        }
        public ActionResult PaymentMethod()
        {
            Cart cart = CartSession;
            var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "zwcjj4ss488mzsy7",
                PublicKey = "yddq2ybzjk5xy2z5",
                PrivateKey = "5917ab582f1790eb830644e90927e1fa"
            };
            // pass clientToken to your front-end
            var clientToken = gateway.ClientToken.Generate();
            cart.ClientToken = clientToken;
            return PartialView("Braintree",cart);
        }

        /// <summary>
        /// Gets or sets the CartSession.
        /// </summary>
        private Cart CartSession
        {
            get
            {
                if (Session == null || Session["eCart"] == null)
                {
                    Session.Add("eCart", new Cart());
                    AddDiscounts();
                }
                Cart cart = (Cart)Session["eCart"];
                return cart;
            }
            set
            {
                Session["eCart"] = value;
            }
        }

    }
}