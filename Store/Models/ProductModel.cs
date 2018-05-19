using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Store.Database.Entities;
using Store.Services;

namespace Store.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Display(Prompt = "Название товара")]
        [Required(ErrorMessage = "Укажите название товара")]
        public string Name { get; set; }

        [Display(Prompt = "Цена")]
        [Required(ErrorMessage = "Укажите цену")]
        public double? Price { get; set; }

        [Display(Prompt = "Старая цена")]
        public double? OldPrice { get; set; }

        [Display(Prompt = "Описание")]
        [Required(ErrorMessage = "Укажите описание")]
        public string Description { get; set; }
        public Image ImageData { get; set; }

        [Required(ErrorMessage = "Не загружена картинка")]
        public IFormFile Image { get; set; }


        public int BrandId { get; set; }
        public string Brand { get; set; }
        public List<string> Brands;

        public void SetModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            ImageData = product.Image;
            OldPrice = product.OldPrice;
            Brand = product.Brand.Name;
            BrandId = product.BrandId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(Brand))
                errors.Add(new ValidationResult("Выберите бренд", new List<string> { "Brand" }));

            if (OldPrice.HasValue && OldPrice.Value > Price.Value)
                errors.Add(new ValidationResult("Старая цена не может быть больше предыдущей", new List<string> { "OldPrice" }));

            return errors;
        }

        public void Fill(IDataManager dataManager)
        {
            Brands = dataManager.BrandRepository.GetAll().Select(x => x.Name).ToList();
        }

        public void ApplyChanges(Product product, IDataManager dataManager)
        {
            var brand = dataManager.BrandRepository.GetAll().First(x => x.Name == Brand);

            string content;
            using (var binaryReader = new BinaryReader(Image.OpenReadStream()))
            {
                var imageData = binaryReader.ReadBytes((int)Image.Length);
                content = Convert.ToBase64String(imageData);
            }
            var image = new Image
            {
                Name = Image.FileName,
                Content = content
            };

            product.Name = Name;
            product.Brand = brand;
            product.Description = Description;
            product.Price = Price.Value;
            product.OldPrice = OldPrice;
            product.Image = image;
        }
    }
}
