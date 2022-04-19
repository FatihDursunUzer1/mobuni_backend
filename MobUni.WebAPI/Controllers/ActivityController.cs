using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ActivityController : Controller
    {
        private readonly IActivityService _activtyService;

        public ActivityController(IActivityService activityService)
        {
            _activtyService = activityService;
        }
   

        [HttpGet("GetAll")]
        public  IActionResult GetAll()
        {
            return Ok( _activtyService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateActivityDTO activityDTO)
        {
            return Ok(await _activtyService.Add(activityDTO));
        }

    }
}

