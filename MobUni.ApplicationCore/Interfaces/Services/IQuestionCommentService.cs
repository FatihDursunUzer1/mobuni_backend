using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces.Services
{
    public interface IQuestionCommentService:IService<QuestionCommentDTO,CreateQuestionCommentDTO>
    {
        IDataResult<List<QuestionCommentDTO>> GetByQuestionId(int questionId);
        Task<IDataResult<bool>> AddComment(CreateQuestionCommentDTO dto, string? userId = null);
        IDataResult<List<QuestionCommentDTO>> GetByActivityId(int activityId);
    }
}
