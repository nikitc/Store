using System;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Database.interfaces;

namespace Store.Database.Entities
{
    [Table("UserToken", Schema = "public")]
    public class UserToken : IDbEntity
    {
        public int Id { get; set; }

        public string Token { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatingDateTime { get; set; }
    }
}
