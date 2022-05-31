using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
       

        public ActivityController(IActivityService activityService, IActivityParticipantService activityParticipantService,IPushNotification pushNotification)
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
            return CreateActionResultInstance(await _activityParticipantService.AddParticipant(createActivityParticipantDTO,userId));
        }

        [HttpGet("ActivityParticipants")]
        public IActionResult GetActivityParticipantsByActivityId( int activityId,[FromQuery] PaginationQuery paginationQuery)
        {
            return CreateActionResultInstance(_activityParticipantService.GetActivityParticipantsByActivityId(activityId,paginationQuery));
        }
        
        
        [HttpGet("GetActivityCountsByUniversityId")]
        public IActionResult GetActivityCountByUniversityId([FromQuery] ActivityFilter activityFilter, DateTime? dateTime = null)
        {

            if (dateTime != null)
                dateTime = dateTime.Value.ToUniversalTime();
            return CreateActionResultInstance(_activtyService.GetActivitiesByUniversityId(activityFilter, dateTime));
        }

        [HttpGet("GetMyJoinedActivities")]
        public IActionResult GetMyJoinedActivities([FromQuery] PaginationQuery paginationQuery)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            return CreateActionResultInstance(_activtyService.GetMyJoinedActivities(userId, paginationQuery));
        }


    }
}

