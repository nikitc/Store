using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Store.Database.Entities;
using Store.Models.Order;

namespace Store.Models.Basket
{
    public class PaymentInfoModel : IValidatableObject
    {
        public PaymentInfoModel(int orderId)
        {
            OrderId = orderId;
        }

        public PaymentInfoModel()
        {
        }

        public int? OrderId { get; set; }
        public DeliveryWays DeliveryWay { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Prompt = "Имя")]
        public string FirstName { get; set; }

        [Display(Prompt = "Отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Prompt = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        [RegularExpression(
            @"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)",
            ErrorMessage = "Некорректный номер")]
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
            info.DeliveryWayId = (int) DeliveryWay;
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
            DeliveryWay = (DeliveryWays) info.DeliveryWayId;
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DeliveryWay == Order.DeliveryWays.Courier)
            {
                if (string.IsNullOrEmpty(HouseStreet))
                {
                    yield return new ValidationResult("Поле обазятельно для заполнения", new[] {nameof(HouseStreet)});
                }

                if (HouseNumber == null)
                {
                    yield return new ValidationResult("Поле обазятельно для заполнения", new[] {nameof(HouseNumber)});
                }

                if (HouseAppartmentNumber == null)
                {
                    yield return new ValidationResult("Поле обазятельно для заполнения",
                        new[] {nameof(HouseAppartmentNumber)});
                }

                if (HouseEntranceNumber == null)
                {
                    yield return new ValidationResult("Поле обазятельно для заполнения",
                        new[] {nameof(HouseEntranceNumber)});
                }

            }
        }
    }
}
    
