using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.Infrastructure.Controllers;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UniversityController : MobUniControllerBase
    {
        private readonly IUniversityService _universityService;

        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUniversityDTO university)
        {
            return CreateActionResultInstance(await _universityService.Add(university));
        }
        [HttpGet]
        public  IActionResult GetUniversityById([FromQuery] int id)
        {
            return CreateActionResultInstance(_universityService.GetById(id));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UniversityDTO universityDTO)
        {
            return CreateActionResultInstance(await _universityService.Update(universityDTO));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _universityService.GetAll());
        }
    }
}
