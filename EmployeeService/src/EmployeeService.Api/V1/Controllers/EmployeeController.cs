using EmployeeService.Core.Interfaces.Services;
using EmployeeService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        // Get All Employees
        // Return List Employees
        // Table used: Employees
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var response = await _employeeService.GetAllEmployees().ConfigureAwait(false);
            return response == null ? NoContent() : Ok(response);
        }

        // Get an Employee by Id
        // Return an Employee
        // Table used: Employees
        [HttpGet("{id}", Name ="GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }    
            var response = await _employeeService.GetEmployeeById(id).ConfigureAwait(false);
            return response == null ? NoContent() : Ok(response);
        }

        // Create Employee
        // Return an Employee created
        // Table used: Employees
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }    

            var response = await _employeeService.CreateEmployee(employee).ConfigureAwait(false);

            return CreatedAtRoute(nameof(GetEmployeeById), new { id = response.Id }, response);
        }

        // Delete Employee
        // Return true/false
        // Table used: Employees
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteEmployee(int id)
        {
            if(id<=0)
            {
                return BadRequest();
            }    

            return await _employeeService.DeleteEmployee(id).ConfigureAwait(false);
        }

        // Delete Employee
        // Return true/false
        // Table used: Employees
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> UpdateEmployee(Employee employee, int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }    
            if (id <= 0)
            {
                return BadRequest();
            }
            var response = await _employeeService.UpdateEmployee(employee, id).ConfigureAwait(false);

            return response == null ? NoContent() : Ok(response);
        }
    }
}
