using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces;
using MyWebStore.DomainNew.ViewModels;

namespace WebStore.Services
{
    public class InMemoryEmployeesData : IEmployeesData
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
            
                NewEmployee.ID = _employees.Count == 0 ? 1 : _employees.Max(employee => employee.ID) + 1;
            _employees.Add(NewEmployee);
        }

        public void Delete(int id)
        {
            var employee = GetByID(id);
            if (employee is null) return;
            _employees.Remove(employee);
        }

        public IEnumerable<EmployeeViewModel> GetAll() => _employees;

        public EmployeeViewModel GetByID(int id) => _employees.FirstOrDefault(employee => employee.ID == id);

        public void SaveChanges() { }

        public EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            var exist_employee = GetByID(id);
            if (exist_employee is null) throw new InvalidOperationException($"Сотрудник с id {id} не найден.");

            exist_employee.FirstName = employee.FirstName;
            exist_employee.SurName = employee.SurName;
            exist_employee.Patronymic = employee.Patronymic;
            exist_employee.Age = employee.Age;

            return exist_employee;

        }
    }
}
