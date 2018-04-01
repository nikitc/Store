using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Database.Entities
{
    [Table("Product2Order", Schema = "public")]
    public class Product2Order
    {
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}