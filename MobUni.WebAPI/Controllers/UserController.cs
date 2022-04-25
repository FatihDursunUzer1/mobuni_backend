using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Result.Concrete;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
         return Ok(await _userService.Register(userDTO));
            
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CreateUserDTO userDTO)
        {
            var data=_userService.Login(userDTO);
            var dataType = data.GetType();
            if(dataType== typeof(SuccessDataResult<Token>))
            {
                return Ok(data);
            }
                return StatusCode(400, data);
            
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CreateUserDTO userDTO)
        {
            return Ok(await _userService.Add(userDTO));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }
        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId([FromQuery] string UserId)
        {
            return Ok(_userService.GetById(UserId));
        }

        [HttpGet("Me")]
        public IActionResult GetMyAccount()
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId != null)
                return Ok(_userService.GetById(userId));
            return new UnauthorizedObjectResult(value:"sadasd");
        }

     
        [HttpPut]
        public async Task< IActionResult> Update([FromBody] UserDTO user)
        {
            return Ok(await _userService.Update(user));
        }
    }
}
