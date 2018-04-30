using System.Collections.Generic;
using System.Linq;
using Store.Database.Entities;

namespace Store.Models
{
    public class MainPageModel
    {
        public List<ProductModel> SaleProducts { get; set; }

        public void Fill(IQueryable<Product> products)
        {
            SaleProducts = products.Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }
    }
}
