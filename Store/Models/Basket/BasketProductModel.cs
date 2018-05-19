using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Store.Database.Entities;

namespace Store.Models.Basket
{
    public class BasketProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Count { get; set; }
        public Image ImageData { get; set; }
        public int Price { get; set; }
    }
}
