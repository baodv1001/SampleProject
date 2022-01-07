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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List Employees</returns>
        /// <tableusing> Employees </tableusing> 
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An employee</returns>
        [HttpGet("{id}", Name = "GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var response = await _employeeService.GetEmployeeById(id).ConfigureAwait(false);
            return response == null ? NoContent() : Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>An employee</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _employeeService.CreateEmployee(employee).ConfigureAwait(false);

            return CreatedAtRoute(nameof(GetEmployeeById), new { id = response.Id }, response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            return await _employeeService.DeleteEmployee(id).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="id"></param>
        /// <returns>Object include boolean and Employee updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Object>> UpdateEmployee(Employee employee, int id)
        {
            if (!ModelState.IsValid)
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
