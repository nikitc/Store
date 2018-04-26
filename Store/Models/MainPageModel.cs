using System.Collections.Generic;
using Store.Database.Entities;

namespace Store.Models
{
    public class MainPageModel
    {
        public List<ProductModel> SaleProducts { get; set; }

        public void Fill(List<Product> products) {
            SaleProducts = new List<ProductModel>();
            foreach (var product in products)
            {
                var currentProduct = new ProductModel();
                currentProduct.SetModel(product);
                SaleProducts.Add(currentProduct);
            }
        }
    }
}
