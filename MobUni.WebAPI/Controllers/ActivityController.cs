using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Interfaces;
using MobUni.Infrastructure.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : MobUniControllerBase
    {
        private readonly IActivityService _activtyService;

        public ActivityController(IActivityService activityService)
        {
            _activtyService = activityService;
        }
   

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResultInstance(await _activtyService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateActivityDTO activityDTO)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            return CreateActionResultInstance(await _activtyService.Add(activityDTO,userId));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ActivityDTO activityDTO)
        {
            return CreateActionResultInstance(await _activtyService.Update(activityDTO));
        }
        [HttpGet("GetByActivityId")]
        public async Task<IActionResult> GetByActivityId([FromQuery] int id)
        {
            return CreateActionResultInstance( _activtyService.GetById(id));
        }

        [HttpGet("GetActivitiesByUniversityId")]
        public async Task<IActionResult> GetActivitiesByUniversityId([FromQuery] int id)
        {
            return CreateActionResultInstance(await _activtyService.GetActivitiesByUniversityId(id));
        }
    }
}

