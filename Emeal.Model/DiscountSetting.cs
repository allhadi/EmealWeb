namespace Emeal.Model
{
    using System;
    using static Emeal.Common.Enums;
    public class DiscountSetting
    {
        public string DiscountName { set; get; }
        public decimal MinOrder { set; get; }
        public DiscountorderType OrderType { set; get; }
        public decimal DiscountAmount { set; get; }
        public string DiscountCode { set; get; }
        public DateTime ExpirationDate { set; get; }
        public DateTime StartingDate { set; get; }
    }
}