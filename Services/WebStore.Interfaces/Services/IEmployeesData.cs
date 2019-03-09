using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebStore.DomainNew.ViewModels;

namespace WebStore.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<EmployeeViewModel> GetAll();

        EmployeeViewModel GetByID(int id);

        EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel employee);

        void AddNew(EmployeeViewModel NewEmployee);

        void Delete(int id);

        void SaveChanges();
    }
}
