using MobUni.ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces
{
    public interface IUnitOfWork
    {
        IActivityCategoryRepository ActivityCategories { get; }
        IActivityRepository Activities { get; }
        IDepartmentRepository Departments { get;  }
        ILikeQuestionRepository Likes { get; }
        IQuestionCommentRepository Comments { get; }

        IQuestionRepository Questions { get; }
        IUniversityRepository Universities { get; }
        IUserRepository Users { get; }
        Task Save();
    }
}
