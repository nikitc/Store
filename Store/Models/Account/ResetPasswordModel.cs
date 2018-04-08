using System;
using System.ComponentModel.DataAnnotations;
using Store.Database.Entities;

namespace Store.Models.Account
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        public string ResetPassword(User user)
        {
            throw new NotImplementedException();
        }
    }
}
