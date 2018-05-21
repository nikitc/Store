using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Store.Database.Entities;
using Store.Models.Order;

namespace Store.Models.Basket
{
    public class PaymentInfoModel
    {
        public PaymentInfoModel(int orderId)
        {
            OrderId = orderId;
        }

        public PaymentInfoModel()
        { }

        public int? OrderId { get; set; }
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

        [Display(Prompt = "Улица")]
        public string HouseStreet { get; set; }
        [Display(Prompt = "Номер дома")]
        public int? HouseNumber { get; set; }
        [Display(Prompt = "Номер квартиры")]
        public int? HouseAppartmentNumber { get; set; }
        [Display(Prompt = "Номер подъезда")]
        public int? HouseEntranceNumber { get; set; }

        public void ApplyChanges(PaymentInfo info)
        {
            info.DeliveryWayId = (int)DeliveryWay;
            info.FirstName = FirstName;
            info.LastName = LastName;
            info.MiddleName = MiddleName;
            info.HouseStreet = HouseStreet;
            info.HouseNumber = HouseNumber;
            info.HouseAppartmentNumber = HouseAppartmentNumber;
            info.HouseEntranceNumber = HouseEntranceNumber;
            info.Phone = Phone;
        }

        public void SetModel(PaymentInfo info)
        {
            DeliveryWay = (DeliveryWays)info.DeliveryWayId;
            FirstName = info.FirstName;
            LastName = info.LastName;
            MiddleName = info.MiddleName;
            HouseStreet = info.HouseStreet;
            HouseNumber = info.HouseNumber;
            HouseAppartmentNumber = info.HouseAppartmentNumber;
            HouseEntranceNumber = info.HouseEntranceNumber;
            Phone = info.Phone;
        }

        public string GetFIO()
        {
            var full = $"{LastName} {FirstName}";
            if (MiddleName != null)
                full += $" {MiddleName}";
            return full;
        }

        public string GetFullAddress()
        {
            if (DeliveryWay == DeliveryWays.Pickup)
                return "";
            var address = $"{HouseStreet}, {HouseNumber}";
            if (HouseAppartmentNumber.HasValue)
                address += $"-{HouseAppartmentNumber}";
            if (HouseEntranceNumber.HasValue)
                address += $", подъезд №{HouseEntranceNumber}";

            return address;
        }

        public string GetDeliveryWay()
        {
            return DeliveryWay == DeliveryWays.Courier ? "Курьерская доставка" : "Самовывоз";
        }
    }
}
