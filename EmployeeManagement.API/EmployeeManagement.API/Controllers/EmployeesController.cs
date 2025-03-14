using EmployeeManagement.Core.BusinessInterfaces;
using EmployeeManagement.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeBusiness _employee;
        public EmployeesController(IEmployeeBusiness employee)
        {
            _employee = employee;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employee.GetAllEmployeesAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] CreateEmployeeDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _employee.CreateEmployeeAsync(createDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = createDto.Name }, createDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employee.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound(new { message = "There is No Employee With This Id!" });

            return Ok(employee);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employee.UpdateEmployeeAsync(id, updateDto);
            if (!result)
                return NotFound(new { message = "There Is No Employee With This Id!" });

            return CreatedAtAction(nameof(GetEmployee), new { id = updateDto.Name }, updateDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = await _employee.DeleteEmployeeAsync(id);
            if (!result)
                return NotFound(new { message = "There is No Employee With This Id!" });
            return Ok("Employee Deleted Successfully!");
        }
    }
}
