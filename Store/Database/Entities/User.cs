using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Database.interfaces;

namespace Store.Database.Entities
{
    [Table("User", Schema = "public")]
    public class User : IDbEntity
    {
        [Key]
        public int Id { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public List<Order> Orders { get; set; }
    }
}