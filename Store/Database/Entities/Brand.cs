using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Database.interfaces;

namespace Store.Database.Entities
{
    [Table("Brand", Schema = "public")]
    public class Brand : IDbEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
