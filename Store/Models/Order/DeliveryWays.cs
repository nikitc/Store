using System.ComponentModel.DataAnnotations;

namespace Store.Models.Order
{
    public enum DeliveryWays
    {
        [Display(Name = "Самовывоз")]
        Pickup = 1,
        [Display(Name = "Курьерская доставка")]
        Courier = 2,
    }
}
