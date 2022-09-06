using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EnerGov.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private DbHelper dbHelper;


        public EmployeeController()
        {
            dbHelper = new DbHelper();

        }
        
        [HttpGet("GetEmployeesByManagerId")]
        public IActionResult GetEmployeeListByManager(string managerId)
        {
            try
            {
                return Ok(dbHelper.GetEmployeesByManager(managerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            try
            {
                return Ok(dbHelper.GetAllEmployees());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee(Employee emp)
        {
            try
            {
                return Ok(dbHelper.CreateEmployee(emp.EmployeeId, emp.FirstName,emp.LastName,emp.Roles,emp.ManagerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
