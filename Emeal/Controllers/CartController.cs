namespace Emeal.Controllers
{
    using Emeal.Common;
    using Emeal.Model;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.PublishedModels;
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
            return PartialView(cart);
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
