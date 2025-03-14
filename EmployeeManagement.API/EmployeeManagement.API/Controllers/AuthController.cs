using EmployeeManagement.Core.BusinessInterfaces;
using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IAuthService _authService;
        public AuthController(IUserBusiness userBusiness, IAuthService authService)
        {
            _userBusiness = userBusiness;
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var user = await _userBusiness.Login(request);
            if (user == null)
                return Unauthorized();

            var token = _authService.GenerateToken(user);
            return Ok(new { token });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest request)
        {
            await _userBusiness.AddNewUser(request);
            return Ok(new {Message = "User Created Successfully"});

        }
    }
}
