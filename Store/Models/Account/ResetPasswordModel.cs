using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Store.Database.Entities;
using Store.Services;

namespace Store.Models.Account
{
    public class ResetPasswordModel : IValidatableObject
    {
        [Required(ErrorMessage = "Не указан Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        public string ResetPassword(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dataManager = validationContext.GetService<IDataManager>();
            if (dataManager.UserRepository.GetAll().FirstOrDefault(x => x.Email == Email) == null)
            {
                yield return new ValidationResult("Такого email в системе не существует", new[] { nameof(Email) });
            }
        }
    }
}
