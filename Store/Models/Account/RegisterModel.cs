using System.ComponentModel.DataAnnotations;

namespace Store.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "Логин может состоять только из букв и цифр")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина может быть от 3 до 20 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен состоять минимум из 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
