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
            RuleFor(u => u.DepartmentId).NotEmpty().When(u => u.UserType == 2).WithMessage("Üniversite kullanıcısının Departmanı boş olamaz").Empty().When(u => u.UserType != 2).
                WithMessage("Departman seçimi sadece Üniversite kullanıcısına özel bir özelliktir."); ;
            RuleFor(u => u.UniversityId).NotEmpty().When(u => u.UserType == 2).WithMessage("Üniversite kullanıcısının Üniversitesi boş olamaz").Empty().When(u => u.UserType != 2).
                WithMessage("Üniversite seçimi sadece Üniversite kullanıcısına özel bir özelliktir.");
            RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(6).WithMessage("Şifreniz en az 6 karakter uzunluğunda olması gerekmektedir.");
            RuleFor(u => u.Surname).NotEmpty().WithMessage("Soy isim kısmı boş bırakılamaz");
            RuleFor(u => u.Name).NotEmpty().WithMessage("İsim kısmı boş bırakılamaz");
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Kullanıcı adı kısmı boş bırakılamaz");
        
        }

        private bool IsUserType(int val)
        {
            if (val >= 1 && val <= 3)
                return true;
            else
                return false;
        }
    }
}
