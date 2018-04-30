using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Database.interfaces;

namespace Store.Database.Entities
{
    [Table("Image", Schema = "public")]
    public class Image : IDbEntity
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
    }
}
