using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobUni.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")=="Develop")
            {
                services.AddDbContext<MobUniDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("ProdConnection")));
            }
            else
            {
                services.AddDbContext<MobUniDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("DevConnection")));
            }

        }
    }
}
