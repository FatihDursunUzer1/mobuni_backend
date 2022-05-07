using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Storage
{
    public interface IStorage
    {
        public Task UploadFile(IFormFile files);
    }
}
