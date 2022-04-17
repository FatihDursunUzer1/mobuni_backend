using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;


        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }



        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDepartmentDTO department)
        {
            return Ok(await _departmentService.Add(department));
        }

        [HttpGet("ALL")]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _departmentService.GetAll());
        }
    }
}
