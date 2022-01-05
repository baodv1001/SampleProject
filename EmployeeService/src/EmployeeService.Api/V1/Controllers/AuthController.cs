using EmployeeService.Api.Helper;
using EmployeeService.Core.Interfaces.Services;
using EmployeeService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        // Get an Employee by Id
        // Return an Employee
        // Table used: Employees
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> Login(LoginModel user)
        {
            if (user == null || user.Username == null || user.Password == null)
            {
                return BadRequest();
            }
            var responeUser = await _userService.GetUserByUsername(user.Username).ConfigureAwait(false);
            if ( responeUser == null)
            {
                return Ok(new { message = "Invalid username" });
            }
            /*if (!BCrypt.Net.BCrypt.Verify(user.Password, responeUser.Password))
            {
                return Ok(new { message = "Invalid Password" });
            }*/
            if(! (responeUser.Password == user.Password))
            {
                return Ok(new { message = "Invalid Password" });
            }    
            string jwt = JwtService.Generate(responeUser.IdUser);
            HttpContext.Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTime.Now.AddMinutes(60),
                IsEssential = true
            });
            return Ok(new
            {
                user = responeUser,
                jwt
            });

        }
    }
}
