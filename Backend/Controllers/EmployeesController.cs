using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAngularAPI.Data;
using TestAngularAPI.Models;

namespace TestAngularAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly TestCRUDDbContext _testCRUDDbContext;

        public EmployeesController(TestCRUDDbContext testCRUDDbContext)
        {
            _testCRUDDbContext = testCRUDDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _testCRUDDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _testCRUDDbContext.AddAsync<Employee>(employeeRequest);
            await _testCRUDDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _testCRUDDbContext.Employees.FirstOrDefaultAsync<Employee>(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _testCRUDDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _testCRUDDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _testCRUDDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _testCRUDDbContext.Employees.Remove(employee);
            await _testCRUDDbContext.SaveChangesAsync();

            return Ok(employee);
        }

    }
}