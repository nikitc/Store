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

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public virtual Image Image { get; set; }

        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }

        public double Price { get; set; }

        public double? OldPrice { get; set; }

        public virtual List<Product2Order> Product2Orders { get; set; }
    }
}