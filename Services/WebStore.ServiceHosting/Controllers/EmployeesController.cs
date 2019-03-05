using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class EmployeesController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData) => _employeesData = employeesData;

        [HttpPost, ActionName("Post")]
        public void AddNew(EmployeeViewModel NewEmployee)
        {
            _employeesData.AddNew(NewEmployee);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeesData.Delete(id);
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employeesData.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public EmployeeViewModel GetByID(int id)
        {
            return _employeesData.GetByID(id);
        }

        [NonAction]
        public void SaveChanges()
        {
            _employeesData.SaveChanges();
        }

        [HttpPut("{id}"), ActionName("Put")]
        public EmployeeViewModel UpdateEmployee(int id, [FromBody] EmployeeViewModel employee)
        {
            return _employeesData.UpdateEmployee(id, employee);
        }
    }
}