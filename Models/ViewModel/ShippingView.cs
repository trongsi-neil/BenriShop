

namespace BenriShop.Models.ViewModel
{
    public class ShippingView
    {
        public string ShippingId { get; set; }
        public int ShippingCost { get; set; }
        public string ShipFullName { get; set; }
        public string ShipPhoneNumber { get; set; }
        public string ShipAdress { get; set; }
        public Order Order { get; set; }

        public string Note { get; set; }
    }
}
