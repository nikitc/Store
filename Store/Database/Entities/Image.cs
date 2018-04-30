using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Database.Entities
{
    [Table("Image", Schema = "public")]
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Extension { get; set; }
    }
}
