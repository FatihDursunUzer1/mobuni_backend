using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Data.Contexts
{
    public class MobUniDbContext:DbContext
    {
       /* public MobUniDbContext(DbContextOptions options):base(options)
        {

        } */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* optionsBuilder.
               UseSqlServer(configuration.GetConnectionString("DevConnection"));*/
            optionsBuilder.
              UseSqlServer("Data Source=mobuni.c9uwcgm4xelz.us-east-2.rds.amazonaws.com,1433;Initial Catalog=MobUni;User ID=admin;Password=oz15ar47uz28;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p=>p.Id).ValueGeneratedOnAdd();
            builder.Entity<Activity>().HasKey(p => p.Id);
            builder.Entity<Activity>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Department>().HasKey(p => p.Id);
            builder.Entity<Department>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<University>().HasKey(p => p.Id);
            builder.Entity<University>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Question>().HasKey(p => p.Id);
            builder.Entity<Question>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Comment>().HasKey(p => p.Id);
            builder.Entity<Comment>().Property(p => p.Id).ValueGeneratedOnAdd();
        }


    }
}
