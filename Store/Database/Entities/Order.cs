using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Database.interfaces;

namespace Store.Database.Entities
{
    [Table("Order", Schema = "public")]
    public class Order : IDbEntity
    {
        [Key]
        public int Id { get; set; }

        public int StateId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int Number { get; set; }

        public List<Product2Order> Product2Orders { get; set; }

        public Order()
        {
            Product2Orders = new List<Product2Order>();
        }
    }
}