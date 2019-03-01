using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebStore.Infrastructure.Interfaces;
using MyWebStore.Models;

namespace MyWebStore.Infrastructure.Implementations
{
    class InMemoryEmployeesData : IEmployeesData
    {

        private readonly List<EmployeeViewModel> _employees = new List<EmployeeViewModel>
        {
            new EmployeeViewModel
            {
                ID = 1,
                FirstName = "Иван",
                Patronymic="Иванович",
                SurName="Иванов",
                Age=44

            },
             new EmployeeViewModel
            {
                ID = 2,
                FirstName = "Петр",
                Patronymic="Петрович",
                SurName="Петров",
                Age=57

            }
        };

        public void AddNew(EmployeeViewModel NewEmployee)
        {
            if (_employees.Contains(NewEmployee))
                return;
            if (_employees.Count==0)
                NewEmployee.ID = 0;
            else
            NewEmployee.ID = _employees.Max(employee => employee.ID) + 1;
            _employees.Add(NewEmployee);
        }

        public void Delete(int id)
        {
            var employee = GetByID(id);
            if (employee is null) return;
            _employees.Remove(employee);
        }

        public IEnumerable<EmployeeViewModel> Get() => _employees;

        public EmployeeViewModel GetByID(int id) => _employees.FirstOrDefault(employee => employee.ID == id);

        public void SaveChanges(){}
    }
}
