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
        public MobUniDbContext(DbContextOptions options):base(options)
        {

        } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Question> Questions { get; set; }
        //public DbSet<Comment> Comments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<LikeQuestion> LikeQuestion { get; set; }
        public DbSet<QuestionComment> QuestionComments { get; set; }
        public DbSet<ActivityCategory> ActivityCategories { get; set; }
        public DbSet<ActivityParticipant> ActivityParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasOne(u => u.University).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().HasOne(u => u.Department).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Question>().HasOne(u => u.User).WithMany(u => u.Questions).HasForeignKey(q => q.UserId);

            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries().Where(i => i.State == EntityState.Added).Select(i => (BaseEntity)i.Entity);
            PrepareAddeddEntities(addedEntities);

            var modifiedEntities= ChangeTracker.Entries().Where(i => i.State == EntityState.Modified).Select(i => (BaseEntity)i.Entity);
            PrepareModifiedEntities(modifiedEntities);
        }

        private void PrepareAddeddEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedTime = DateTime.Now.ToLocalTime();
                entity.UpdatedTime = DateTime.Now.ToLocalTime();
            }
        }

        private void PrepareModifiedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach(var entity in entities)
            {
                entity.UpdatedTime = DateTime.Now.ToLocalTime();
            }
        }
    }
}
