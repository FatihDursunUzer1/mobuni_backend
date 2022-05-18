using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Repositories
{
    public class QuestionCommentRepository : EfRepositoryBase<QuestionComment>, IQuestionCommentRepository
    {
        public QuestionCommentRepository(MobUniDbContext mobUniDbContext) : base(mobUniDbContext)
        {
        }
    }
}
