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
        public OrderType OrderType { set; get; }
        public List<CartLine> Lines { set; get; }
        public List<DiscountSetting> DiscountSettings { set; get; }
        public DiscountSetting DiscountApplied { set; get; }
        public double DiscountValue { set; get; }
        public string DiscountCode { set; get; }

        public Cart()
        {
            Lines = new List<CartLine>();
            OrderType = OrderType.Collection;
            DiscountSettings = new List<DiscountSetting>();
        }

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

        public void Remove(int index)
        {
            //Check Lines has a item and index is valid or not.
            if (Lines.Any() && index <= Lines.Count)
            {
                Lines.RemoveAt(index);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void QtyAdd(int index)
        {
            //Increase quantity.
            if (Lines.Any() && index <= Lines.Count)
            {
                Lines[index].Quantity++;
            }
        }

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

        public double GrandTotal
        {
            //Total Calculation
            get
            {
                CalculateDiscount();
                return Lines.Sum(x => x.Total) - DiscountValue;
            }
        }

        public bool DiscountCodeValidation(string code)
        {
            //Discount Validation
                return DiscountSettings.Exists(x => x.DiscountCode == code && x.ExpirationDate > DateTime.Now && x.StartingDate < DateTime.Now);
        }

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
