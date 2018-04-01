using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Database.interfaces;

namespace Store.Database.Entities
{
    [Table("Product", Schema = "public")]
    public class Product : IDbEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public List<Product2Order> Product2Orders { get; set; }

        public Product()
        {
            Product2Orders = new List<Product2Order>();
        }
    }
}