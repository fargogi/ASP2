using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using MyWebStore.DomainNew.ViewModels;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration configuration) : base(configuration) => ServiceAddress = "api/employees";

        public void AddNew(EmployeeViewModel NewEmployee)
        {
            Post(ServiceAddress, NewEmployee);
        }

        public void Delete(int id)
        {
            Delete($"{ServiceAddress}/{id}");
        }

        public IEnumerable<EmployeeViewModel> GetAll() => Get<List<EmployeeViewModel>>(ServiceAddress);

        public EmployeeViewModel GetByID(int id)
        {
           return Get<EmployeeViewModel>($"{ServiceAddress}/{id}");
        }

        public void SaveChanges() { }

        public EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel employee)
        {
            var response = Put($"{ServiceAddress}/{id}", employee);
            return response.Content.ReadAsAsync<EmployeeViewModel>().Result;
        }
    }
}
