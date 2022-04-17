using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]UserDTO userDTO)
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
    }
}
