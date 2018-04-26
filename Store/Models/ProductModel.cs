using Store.Database.Entities;

namespace Store.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public void SetModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
        }
    }
}
