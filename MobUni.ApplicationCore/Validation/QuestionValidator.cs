using FluentValidation;
using MobUni.ApplicationCore.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Validation
{
    public class QuestionValidator:AbstractValidator<CreateQuestionDTO>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.Text).NotEmpty().WithMessage("Sorunun içeriği boş olamaz");
            RuleFor(q => q.UniversityId).NotEmpty().WithMessage("Üniversite kısmı boş olamaz");
  
        }
    }
}
