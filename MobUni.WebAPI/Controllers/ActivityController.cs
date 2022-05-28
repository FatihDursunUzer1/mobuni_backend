using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Filters;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.ApplicationCore.Pagination;
using MobUni.Infrastructure.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : MobUniControllerBase
    {
        private readonly IActivityService _activtyService;
        private readonly IActivityParticipantService _activityParticipantService;

        public ActivityController(IActivityService activityService, IActivityParticipantService activityParticipantService)
        {
            _activtyService = activityService;
            _activityParticipantService = activityParticipantService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]
           ActivityFilter filter, [FromQuery] PaginationQuery? paginationQuery)
        {
            if (paginationQuery.PageSize==0 || paginationQuery.PageIndex==0)
                return CreateActionResultInstance(await _activtyService.GetAll(filter));
            else
                return CreateActionResultInstance(_activtyService.GetAllPaginated(filter,paginationQuery));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CreateActivityDTO activityDTO)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            return CreateActionResultInstance(await _activtyService.Add(activityDTO,userId));
        }
        [HttpPut]
        public async Task<IActionResult> Update(int activityId,int? maxUser,bool? timeOut)
        {
            return CreateActionResultInstance(await _activtyService.Update(activityId,maxUser,timeOut));
        }

        [HttpPost("JoinOrLeave")]
        public async Task<IActionResult> JoinOrLeave([FromBody] CreateActivityParticipantDTO createActivityParticipantDTO)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            return CreateActionResultInstance(await _activityParticipantService.Add(createActivityParticipantDTO,userId));
        }

        [HttpGet("GetActivityCountsByUniversityId")]
        public IActionResult GetActivityCountByUniversityId(int universityId, DateTime? dateTime = null)
        {
            if (dateTime != null)
                dateTime = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
            return CreateActionResultInstance(_activtyService.GetActivitiesByUniversityId(universityId, dateTime));
        }


    }
}

