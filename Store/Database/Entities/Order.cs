﻿using System.Collections.Generic;
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

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int Number { get; set; }

        public string Guid { get; set; }

        public int? PaymentInfoId { get; set; }

        [ForeignKey(nameof(PaymentInfoId))]
        public virtual PaymentInfo PaymentInfo { get; set; }

        public virtual List<Product2Order> Product2Orders { get; set; }
    }
}