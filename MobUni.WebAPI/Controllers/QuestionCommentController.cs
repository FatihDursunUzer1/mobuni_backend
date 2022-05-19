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
            var userId = HttpContext.Items["UserId"].ToString();
            return CreateActionResultInstance(await _questionCommentService.AddComment(createQuestionCommentDTO,userId));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return CreateActionResultInstance(_questionCommentService.GetById(id));
        }

        [HttpGet("GetByQuestionId/{questionId}")]
        public async Task<IActionResult> GetByQuestionId(int questionId)
        {
            return CreateActionResultInstance(await _questionCommentService.GetByQuestionId(questionId));
        }

        [HttpGet("GetByActivityId/{activityId}")]
        public async Task<IActionResult> GetByActivityId(int activityId)
        {
            return CreateActionResultInstance(await _questionCommentService.GetByActivityId(activityId));
        }
    }
}
