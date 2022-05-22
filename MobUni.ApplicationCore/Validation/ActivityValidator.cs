using FluentValidation;
using MobUni.ApplicationCore.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Validation
{
    public class ActivityValidator:AbstractValidator<CreateActivityDTO>
    {
        public ActivityValidator()
        {
            RuleFor(a => a.ActivityStartTime).GreaterThan(DateTime.Now).LessThan(a=>a.ActivityEndTime).WithMessage("Başlangıç tarihi, bitiş tarihinden sonra veya geçmiş bir tarih olmamalıdır.");
            RuleFor(a => a.ActivityEndTime).GreaterThan(DateTime.Now).WithMessage("Bitiş tarihi geçmiş bir tarih içermemelidir.");
            RuleFor(a => a.MaxUser).GreaterThanOrEqualTo(0).WithMessage("Maksimum Kullanıcı sayısı 0'dan büyük olmalıdır.");
            RuleFor(a => a.TicketPrice).GreaterThanOrEqualTo(0).WithMessage("Bilet fiyatları - değer alamaz");
            RuleFor(a => a.UniversityId).GreaterThanOrEqualTo(1).WithMessage("Geçersiz üniversite giriş denemesi");
        }
    }
}
