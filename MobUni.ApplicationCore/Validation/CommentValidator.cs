using FluentValidation;
using MobUni.ApplicationCore.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Validation
{
    public class CommentValidator:AbstractValidator<CreateQuestionCommentDTO>
    {
        public CommentValidator()
        {
            RuleFor(c => c.QuestionId).NotEmpty().When(c => c.ActivityId == null).WithMessage("Bir yorumda Activity ve Question aynı anda boş olamaz");
            RuleFor(c => c.ActivityId).NotEmpty().When(c => c.QuestionId == null).WithMessage("Bir yorumda Activity ve Question aynı anda boş olamaz");
        }
    }
}
