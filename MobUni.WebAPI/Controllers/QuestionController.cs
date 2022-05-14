using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Interfaces;
using MobUni.Infrastructure.Controllers;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : MobUniControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateQuestionDTO questionDTO)
        {
            return CreateActionResultInstance(await _questionService.Add(questionDTO));
        }

        [HttpGet("GetByQuestionId")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            return CreateActionResultInstance(_questionService.GetById(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _questionService.GetAll());
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QuestionDTO question)
        {
            return CreateActionResultInstance(await _questionService.Update(question));
        }
    }
}
