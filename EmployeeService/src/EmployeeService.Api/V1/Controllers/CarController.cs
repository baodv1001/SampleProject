using EmployeeService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EmployeeService.Api.DependencyInjection;

namespace EmployeeService.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IHorn _horn;
        public CarController(ServiceResolver serviceResolver)
        {
            _horn = serviceResolver(ServiceType.Heavy);
        }
        [HttpGet]
        public async Task<ActionResult<string>> GetLevel()
        {
            return Ok(new { level = _horn.Beep() });
        }

    }
}
