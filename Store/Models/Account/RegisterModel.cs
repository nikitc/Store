using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Store.Services;

namespace Store.Models.Account
{
    public class RegisterModel : IValidatableObject
    {
        [Display(Prompt = "Логин")]
        [Required(ErrorMessage = "Не указан логин")]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "Логин может состоять только из букв и цифр")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина может быть от 3 до 20 символов")]
        public string Login { get; set; }

        [Display(Prompt = "Email")]
        [Required(ErrorMessage = "Не указан Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        [Display(Prompt = "Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен состоять минимум из 6 символов")]
        public string Password { get; set; }

        [Display(Prompt = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dataManager = validationContext.GetService<IDataManager>();
            if (dataManager.UserRepository.GetAll().FirstOrDefault(x => x.Login == Login) != null)
            {
                yield return new ValidationResult("Пользователь с таким логином уже существует", new[] { nameof(Login) });
            }

            if (dataManager.UserRepository.GetAll().FirstOrDefault(x => x.Email == Email) != null)
            {
                yield return new ValidationResult("Пользователь с таким email уже существует", new[] { nameof(Email) });
            }
        }
    }
}
