using FluentValidation;
using MobUni.ApplicationCore.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Validation
{
    public class DepartmentValidator:AbstractValidator<CreateDepartmentDTO>
    {
        public DepartmentValidator()
        {
            RuleFor(d=>d.Name).NotEmpty().WithMessage("Departman ismi boş olamaz");
        }
    }
}
