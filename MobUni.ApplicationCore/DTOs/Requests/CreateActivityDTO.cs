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
        // public UserDTO User { get; set; }
        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public string Title { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public string Content { get; set; }
        //public UniversityDTO University { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int UniversityId { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public DateTime? ActivityStartTime { get; set; } = DateTime.Now;

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public DateTime? ActivityEndTime { get; set; } = DateTime.Now;

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int CommentCount { get; set; }

        [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
        public int LikeCount { get; set; }

        public IFormFile? Image { get; set; }
     
    }
}
