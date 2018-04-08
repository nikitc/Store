using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Database.Entities
{
    [Table("Product2Order", Schema = "public")]
    public class Product2Order
    {
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}