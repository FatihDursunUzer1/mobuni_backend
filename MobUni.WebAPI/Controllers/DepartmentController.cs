using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.Entities;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        [HttpPost]
        public async Task<bool> Add([FromBody] Department department)
        {
            using (var context= new MobUniDbContext())
            {
                await context.AddAsync(department);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
