using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebStore.Models;

namespace MyWebStore.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<EmployeeViewModel> Get();

        EmployeeViewModel GetByID(int id);

        void AddNew(EmployeeViewModel NewEmployee);

        void Delete(int id);

        void SaveChanges();
    }
}
