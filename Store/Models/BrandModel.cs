using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.Database.Entities;
using Store.Services;

namespace Store.Models
{
    public class BrandModel : IValidatableObject
    {
        [Display(Prompt = "Название")]
        [Required(ErrorMessage = "Необходимо указать название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не выбран логотип")]
        public IFormFile Image { get; set; }

        public Image ImageData { get; set; }
        public int BrandId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dataManager = validationContext.GetService<IDataManager>();
            if (dataManager.BrandRepository.GetAll().FirstOrDefault(x => x.Name == Name) != null)
            {
                yield return new ValidationResult("Бренд с таким именем уже существует", new[] { nameof(Name) });
            }
        }
    }
}
