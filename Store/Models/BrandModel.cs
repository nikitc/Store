using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Store.Database.Entities;

namespace Store.Models
{
    public class BrandModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Необходимо указать название")]
        public string Name { get; set; }
        [Display(Name = "Картинка")]
        [Required(ErrorMessage = "Не выбран логотип")]
        public IFormFile Image { get; set; }
        public Image ImageData { get; set; }
    }
}
