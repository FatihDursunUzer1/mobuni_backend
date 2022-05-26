using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.Infrastructure.Controllers;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityCategoryController : MobUniControllerBase
    {

        private IActivityCategoryService _activityCategoryService;

        public ActivityCategoryController(IActivityCategoryService activityCategoryService)
        {
            _activityCategoryService = activityCategoryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return CreateActionResultInstance( _activityCategoryService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] ActivityCategoryDTO activityCategoryDTO)
        {
            return CreateActionResultInstance(await _activityCategoryService.Add(activityCategoryDTO));
        }
    }
}
