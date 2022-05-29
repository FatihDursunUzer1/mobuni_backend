using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.ModelBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateQuestionDTO: BaseCreateDTO<int>
    {
        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int UniversityId { get; set; }
        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public bool IsUniversityStudent { get; set; } = true;

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int? DepartmentId { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public string Text { get; set; }
        public IFormFile? Image { get; set; }
      
    }

}
