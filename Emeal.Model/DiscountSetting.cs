namespace Emeal.Model
{
    using System;
    using static Emeal.Common.Enums;

    /// <summary>
    /// Defines the <see cref="DiscountSetting" />.
    /// </summary>
    public class DiscountSetting
    {
        /// <summary>
        /// Gets or sets the DiscountName.
        /// </summary>
        public string DiscountName { set; get; }
        /// <summary>
        /// Gets or sets the MinOrder.
        /// </summary>
        public decimal MinOrder { set; get; }
        /// <summary>
        /// Gets or sets the OrderType.
        /// </summary>
        public DiscountorderType OrderType { set; get; }
        /// <summary>
        /// Gets or sets the DiscountAmount.
        /// </summary>
        public decimal DiscountAmount { set; get; }
        /// <summary>
        /// Gets or sets the DiscountCode.
        /// </summary>
        public string DiscountCode { set; get; }
        /// <summary>
        /// Gets or sets the ExpirationDate.
        /// </summary>
        public DateTime ExpirationDate { set; get; }
        /// <summary>
        /// Gets or sets the StartingDate.
        /// </summary>
        public DateTime StartingDate { set; get; }
    }
}
