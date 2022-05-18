using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.Infrastructure.Controllers;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCommentController : MobUniControllerBase
    {
        private readonly IQuestionCommentService _questionCommentService;

        public QuestionCommentController(IQuestionCommentService questionCommentService)
        {
            _questionCommentService = questionCommentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateQuestionCommentDTO createQuestionCommentDTO)
        {
            return CreateActionResultInstance(await _questionCommentService.Add(createQuestionCommentDTO));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return CreateActionResultInstance(_questionCommentService.GetById(id));
        }
    }
}
