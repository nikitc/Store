using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Store.Models.Order;

namespace Store.Models.Basket
{
    public class PaymentInfoModel
    {
        public PaymentInfoModel(int orderId)
        {
            OrderId = orderId;
            DeliveryWays = new List<string>
            {
                "Курьерская доставка",
                "Самовывоз"
            };
        }

        public List<string> DeliveryWays { get; set; }

        public int OrderId { get; set; }
        public int DeliveryWayId { get; set; }
        public DeliveryWays DeliveryWay { get; set; }

        [Required]
        [Display(Prompt = "Имя")]
        public string FirstName { get; set; }
        [Display(Prompt = "Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Prompt = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Prompt = "Номер телефона")]
        public string Phone { get; set; }

        [Display(Prompt="Улица")]
        public string HouseStreet { get; set; }
        [Display(Prompt = "Номер дома")]
        public int? HouseNumber { get; set; }
        [Display(Prompt = "Номер квартиры")]
        public int? HouseAppartmentNumber { get; set; }
        [Display(Prompt = "Номер подъезда")]
        public int? HouseEntranceNumber { get; set; }
    }
}
