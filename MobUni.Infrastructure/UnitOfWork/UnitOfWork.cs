using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IActivityCategoryRepository ActivityCategories { get; }

        public IActivityRepository Activities { get; }

        public IDepartmentRepository Departments { get; }

        public ILikeQuestionRepository Likes { get; }

        public IQuestionCommentRepository Comments { get; }

        public IQuestionRepository Questions { get; }

        public IUniversityRepository Universities { get; }

        public IUserRepository Users { get; }

        private MobUniDbContext _dbContext;

        public UnitOfWork(IActivityCategoryRepository activityCategories, IActivityRepository activities, IDepartmentRepository departments, 
            ILikeQuestionRepository likes, IQuestionCommentRepository comments, IQuestionRepository questions, IUniversityRepository universities,
            IUserRepository users, MobUniDbContext dbContext)
        {
            ActivityCategories = activityCategories;
            Activities = activities;
            Departments = departments;
            Likes = likes;
            Comments = comments;
            Questions = questions;
            Universities = universities;
            Users = users;
            _dbContext = dbContext;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
