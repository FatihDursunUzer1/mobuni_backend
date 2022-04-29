using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.Entities;
using MobUni.Infrastructure.Controllers;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : MobUniControllerBase
    {
        [HttpPost]
        public async Task<int> Add(University university)
        {
            using (var context=new MobUniDbContext())
            {
                if (context.Universities.AsNoTracking().FirstOrDefault(u => u.Id == university.Id) == null)
                {
                    await context.AddAsync<University>(university);
                    await context.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    context.Update(university);
                    return 2;
                }
            }
        }
    }
}
