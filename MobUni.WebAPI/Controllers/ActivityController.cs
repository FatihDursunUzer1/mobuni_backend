using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Filters;
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
   

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]
           ActivityFilter filter )
        {
           
            return CreateActionResultInstance(await _activtyService.GetAll(filter));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CreateActivityDTO activityDTO)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            return CreateActionResultInstance(await _activtyService.Add(activityDTO,userId));
        }
        [HttpPut]
        public async Task<IActionResult> Update(int activityId,int maxUser,bool timeOut)
        {
            return CreateActionResultInstance(await _activtyService.Update(activityId,maxUser,timeOut));
        }
       
    }
}

