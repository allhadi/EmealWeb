namespace Emeal.Common
{
    using System.ComponentModel;

    public class Enums
    {
        public enum OrderType
        {
            Delivery,
            Collection
        }

        public enum DiscountorderType
        {
            [Description("Delivery")]
            Delivery,
            [Description("Collection")]
            Collection,
            [Description("Both")]
            Both
        }

        public enum AddressType
        {
           [Description("Delivery")]
           DeliveryAddress = 1,
           [Description("BillingAddress")]
           BillingAddress = 2
        }
    }
}
