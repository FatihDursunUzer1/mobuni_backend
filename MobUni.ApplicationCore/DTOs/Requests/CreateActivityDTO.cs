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
    public class CreateActivityDTO: BaseCreateDTO<int>
    {
        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public string Title { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public string? Content { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int UniversityId { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public DateTime ActivityStartTime { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public DateTime ActivityEndTime { get; set; } 
        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public bool IsExternal { get; set; }
       
        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int? MaxUser { get; set; }
        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int? TicketPrice { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int[]? ActivityCategories { get; set; } 

        public IFormFile? Image { get; set; }
     
    }
}
