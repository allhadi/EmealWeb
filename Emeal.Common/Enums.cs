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
    }
}
