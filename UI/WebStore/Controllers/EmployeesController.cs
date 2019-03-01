using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebStore.Infrastructure.Interfaces;
using MyWebStore.Models;

namespace MyWebStore.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _employeesData = EmployeesData;   
        }

        public IActionResult Index()
        {
            return View(_employeesData.Get());
        }

        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();
            var employee = _employeesData.GetByID((int)id);
            if (employee is null) return NotFound();
            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles = MyWebStore.DomainEntities.Entities.User.AdminRole)]
        public IActionResult Edit(int? id)
        {
            if (id is null) return View(new EmployeeViewModel

                {
                FirstName="Имя",
                SurName="Фамилия",
                Patronymic="Отчество",
                Age=18
                });

            var employee = _employeesData.GetByID((int)id);
            if (employee is null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = MyWebStore.DomainEntities.Entities.User.AdminRole)]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ID == 0)
            {
                _employeesData.AddNew(model);

            }
            else
            {
                var employee = _employeesData.GetByID(model.ID);
                if (employee is null) return NotFound();
                employee.FirstName = model.FirstName;
                employee.SurName = model.SurName;
                employee.Patronymic = model.Patronymic;
                employee.Age = model.Age;
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = MyWebStore.DomainEntities.Entities.User.AdminRole)]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var employee = _employeesData.GetByID((int)id);
            if (employee is null) return NotFound();
            _employeesData.Delete((int)id);
            return RedirectToAction("Index");
        }
    }
}