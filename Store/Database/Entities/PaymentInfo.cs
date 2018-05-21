using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Database.Entities
{
    [Table("PaymentInfo", Schema = "public")]
    public class PaymentInfo
    {
        [Key]
        public int Id { get; set; }
        public int DeliveryWayId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string HouseStreet { get; set; }
        public int? HouseNumber { get; set; }
        public int? HouseAppartmentNumber { get; set; }
        public int? HouseEntranceNumber { get; set; }
    }
}
