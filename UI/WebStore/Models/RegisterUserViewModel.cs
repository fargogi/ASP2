using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebStore.Models
{
    public class RegisterUserViewModel
    {
        [Required, Display(Name ="Имя пользователя"), MaxLength(256)]
        public string UserName { get; set; }

        [Required, Display(Name = "Пароль"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Display(Name = "Подтверждение пароля"), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
