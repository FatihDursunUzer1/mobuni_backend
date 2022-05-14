using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Result.Concrete;
using MobUni.Infrastructure.Controllers;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MobUniControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService= userService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO userDTO)
        {
         return CreateActionResultInstance(await _userService.Register(userDTO));
            
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            return CreateActionResultInstance(_userService.Login(userDTO));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CreateUserDTO userDTO)
        {
            return CreateActionResultInstance(await _userService.Add(userDTO));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _userService.GetAll());
        }
        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId([FromQuery] string UserId)
        {
            return CreateActionResultInstance(_userService.GetById(UserId));
        }

        [HttpGet("Me")]
        public IActionResult GetMyAccount()
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId != null)
                return CreateActionResultInstance(_userService.GetById(userId));
            return new UnauthorizedObjectResult(value:"Unauthorized");
        }
     
        [HttpPut]
        public async Task< IActionResult> Update([FromBody] UserDTO user)
        {
            return CreateActionResultInstance(await _userService.Update(user));
        }
    }
}
