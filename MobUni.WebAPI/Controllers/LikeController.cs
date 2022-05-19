using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.Infrastructure.Controllers;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : MobUniControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService= likeService;
        }
        [HttpPost]
        public async Task<IActionResult> Like([FromQuery] CreateLikeDTO createLikeDTO)
        {
            var userId=HttpContext.Items["UserId"].ToString();
            return CreateActionResultInstance(await _likeService.Like(createLikeDTO,userId));
        }
    }
}
