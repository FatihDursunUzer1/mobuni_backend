using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class DepartmentController : MobUniControllerBase
    {
        private readonly IDepartmentService _departmentService;


        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }



        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDepartmentDTO department)
        {
            return CreateActionResultInstance(await _departmentService.Add(department));
        }

        [HttpGet("ALL")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _departmentService.GetAll());
        }
    }
}
