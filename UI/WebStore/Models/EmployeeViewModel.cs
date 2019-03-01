using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyWebStore.Models
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Имя должно быть обязательно.")]
        [RegularExpression("^[A-ZА-Я][a-z,а-я]*",ErrorMessage ="Имя должно начинаться с заглавной буквы и содержать только буквы")]
        [Display(Name ="Имя сотрудника")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия тоже обязательна")]
        [RegularExpression("^[A-ZА-Я][a-z,а-я]*", ErrorMessage = "Фамилия должна начинаться с заглавной буквы и содержать только буквы")]

        [Display(Name = "Фамилия сотрудника")]
        public string SurName { get; set; }

        [Display(Name = "Отчество сотрудника")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage ="Не указан возраст.")]
        [Range(minimum:18, maximum:150,ErrorMessage = "Возраст должен быть в диапазоне 18 - 150 лет")]
        public int Age { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
