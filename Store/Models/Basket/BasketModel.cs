using System.Collections.Generic;
using System.Linq;

namespace Store.Models.Basket
{
    public class BasketModel
    {
        public List<BasketProductModel> Products { get; set; } 

        public int Sum { get; set; }

        public void ApplyChanges(Database.Entities.Order order)
        {
            
        }

        public void SetModel(Database.Entities.Order order)
        {
            Products = new List<BasketProductModel>();
            foreach (var orderProduct2Order in order.Product2Orders)
            {
                Products.Add(new BasketProductModel
                {
                    Count = orderProduct2Order.ProductCount,
                    ImageData = orderProduct2Order.Product.Image,
                    Name = orderProduct2Order.Product.Name,
                    Price = (int)orderProduct2Order.Product.Price,
                    ProductId = orderProduct2Order.ProductId
                });
            }
            Sum = Products.Sum(x => x.Price * x.Count);
        }
    }
}
