﻿using System.ComponentModel.DataAnnotations;

namespace Store.Models.Account
{
    public class LoginModel
    {
        [Display(Prompt = "Логин")]
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Display(Prompt = "Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
