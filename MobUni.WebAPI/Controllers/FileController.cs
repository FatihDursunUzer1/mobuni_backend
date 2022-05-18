using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.Infrastructure.Storage;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IStorage _storage;

        public FileController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpPost]
        public async Task<string> UploadImage([FromForm]IFormFile file)
        {
            return await _storage.UploadQuestionImage(file);
        }

        [HttpGet]
        public async Task<IActionResult> GetImage([FromQuery]string filepath)
        {
            var fileBytes=await _storage.GetFile(filepath);
            return File(fileBytes, "image/webp");
        }
    }
}
