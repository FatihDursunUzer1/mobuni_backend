using FluentValidation;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Validation
{
    public class UserValidator:AbstractValidator<CreateUserDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserType).Must(IsUserType).WithMessage("Geçersiz kullanıcı tipi ataması").NotEmpty().WithMessage("Kullanıcı tipi seçimi zorunludur.");
            RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(8).WithMessage("Şifreniz en az 8 karakter uzunluğunda olması gerekmektedir.");
            RuleFor(u => u.Surname).NotEmpty().WithMessage("Soy isim kısmı boş bırakılamaz");
            RuleFor(u => u.Name).NotEmpty().WithMessage("İsim kısmı boş bırakılamaz");
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Kullanıcı adı kısmı boş bırakılamaz");
            RuleFor(u => u.UniversityId).NotEmpty().When(u => u.DepartmentId != null).WithMessage("Departman bilgisi girildiği takdirde Üniversite bilgisi boş bırakılamaz");
            RuleFor(u => u.DepartmentId).NotEmpty().When(u => u.UniversityId != null).WithMessage("Üniversite bilgisi girildiği takdirde Departman bilgisi boş bırakılamaz");
            RuleFor(u => u.IsUniversityStudent).NotEqual(true).When(u => u.UniversityId == null).WithMessage("Üniversite öğrencisi olmayan kullanıcının Departman ve Üniversite bilgileri dolu olmamalıdır");
            RuleFor(u => u.IsUniversityStudent).NotEqual(false).When(u => u.UniversityId != null).WithMessage("Üniversite bilgisi dolu olan kullanıcının Üniversite öğrencisi tipinde olması gerekir");

        }
        private bool IsUserType(int val)
        {
            if (val==0 || val==1)
                return true;
            else
                return false;
        }
    }
}
