﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task UploadImage([FromForm]IFormFile file)
        {
            await _storage.UploadFile(file);
        }
    }
}
