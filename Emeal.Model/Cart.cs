namespace Emeal.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Emeal.Common.Enums;

    /// <summary>
    /// Defines the <see cref="Cart" />.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets or sets the OrderType from front-end 
        /// which is selected by a Radiobutton in Cart- Basket.cshtml.
        /// </summary>
        public OrderType OrderType { set; get; }
        /// <summary>
        /// Gets or sets the List of Cart Lines. When Customer Add a product 
        /// a new Cart Line will generate.
        /// </summary>
        public List<CartLine> Lines { set; get; }
        /// <summary>
        /// Gets or sets the DiscountSettings from Umbraco Setting Umbraco.webPublished.ModelBuildes.
        /// </summary>
        public List<DiscountSetting> DiscountSettings { set; get; }
        /// <summary>
        /// Gets or sets the DiscountApplied when the validation and calculation of discount is done
        /// the Applicable discount is saved in this property.
        /// </summary>
        public DiscountSetting DiscountApplied { set; get; }
        /// <summary>
        /// Gets or sets the DiscountValue. It is the calculate result of the discount amount. 
        /// DiscountValue = (DiscountAmount * total) / 100
        /// </summary>
        public double DiscountValue { set; get; }
        /// <summary>
        /// Gets or sets the DiscountCode from the front-end cart(Basket.cshtml)
        /// When a coustomer apply a code the string will save in this property through a validation process in DiscountCodeValidation
        /// </summary>
        public string DiscountCode { set; get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Cart"/> class.
        /// </summary>
        public Cart()
        {
            Lines = new List<CartLine>();
            OrderType = OrderType.Collection;
            DiscountSettings = new List<DiscountSetting>();
        }
        /// <summary>
        /// The Add method is used for Adding products in cart(Basket.cshtml)
        /// When a customer Click/Tap in a product's Add button this mathod will Add this product in the Cart.
        /// </summary>
        /// <param name="cartLine">The cartLine<see cref="CartLine"/>.</param>
        public void Add(CartLine cartLine)
        {
            //Add item
            if (Lines.Any(x => x.Id == cartLine.Id))
            {
                //if item already added find the item find the index and incraese quantity.
                CartLine item = Lines.First(x => x.Id == cartLine.Id);
                int index = Lines.IndexOf(item);
                Lines[index].Quantity += cartLine.Quantity;
            }
            else
            {
                Lines.Add(cartLine);
            }
        }

        /// <summary>
        /// When a customer Click/Tap in the minus(-) button in the cart the particular product will be removed.
        /// And this remove button will remove the product from the cart.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        public void Remove(int index)
        {
            //Check Lines has a item and index is valid or not.
            if (Lines.Any() && index <= Lines.Count)
            {
                Lines.RemoveAt(index);
            }
        }

        /// <summary>
        /// The QtyAdd.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        public void QtyAdd(int index)
        {
            //Increase quantity.
            if (Lines.Any() && index <= Lines.Count)
            {
                Lines[index].Quantity++;
            }
        }

        /// <summary>
        /// The QtySubtract.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        public void QtySubtract(int index)
        {
            //Decrease quantity.
            if (Lines.Any() && index <= Lines.Count)
            {
                if (Lines[index].Quantity > 1)
                {
                    Lines[index].Quantity--;
                }
                //if item quantity is 1 then remove the item.
                else
                {
                    Lines.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Gets the GrandTotal.
        /// </summary>
        public double GrandTotal
        {
            //Total Calculation
            get
            {
                CalculateDiscount();
                return Lines.Sum(x => x.Total) - DiscountValue;
            }
        }

        /// <summary>
        /// The DiscountCodeValidation.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool DiscountCodeValidation(string code)
        {
            //Discount Validation
            return DiscountSettings.Exists(x => x.DiscountCode == code && x.ExpirationDate > DateTime.Now && x.StartingDate < DateTime.Now);
        }

        /// <summary>
        /// The CalculateDiscount.
        /// </summary>
        private void CalculateDiscount()
        {
            var total = Convert.ToDecimal(Lines.Sum(x => x.Total));
            DiscountValue = 0;
            DiscountApplied = null;
            foreach (var discount in DiscountSettings)
            {
                //Check Discount Type
                if (discount.OrderType == DiscountorderType.Both || (int)OrderType == (int)discount.OrderType)
                {
                    //Check Discount Code
                    if (!string.IsNullOrEmpty(discount.DiscountCode))
                    {
                        //Check Discount over the Discount Code
                        if (!discount.DiscountCode.Equals(DiscountCode))
                        {
                            continue;
                        }
                    }

                    //Discount Calculation
                    if (discount.MinOrder <= total)
                    {
                        var dis = Convert.ToDouble((discount.DiscountAmount * total) / 100);
                        if (dis > DiscountValue)
                        {
                            DiscountValue = dis;
                            DiscountApplied = discount;
                        }
                    }
                }
            }
        }
    }
}
