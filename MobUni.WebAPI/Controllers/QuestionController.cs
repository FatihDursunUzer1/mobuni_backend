using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Pagination;
using MobUni.Infrastructure.Controllers;

namespace MobUni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : MobUniControllerBase
    {
        private readonly IQuestionService _questionService;
        private  readonly string? _userId;
        private IHttpContextAccessor _contextAccessor;

        public QuestionController(IQuestionService questionService, IHttpContextAccessor contextAccessor = null)
        {
            _questionService = questionService;
            
            _contextAccessor = contextAccessor;
            _userId = _contextAccessor.HttpContext.Items["UserId"].ToString();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateQuestionDTO questionDTO)
        {
            return CreateActionResultInstance(await _questionService.Add(questionDTO,_userId));
        }

        //Pagination

        [HttpGet("GetByQuestionId")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            return CreateActionResultInstance(_questionService.GetById(id));
        }

        //Pagination

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance( _questionService.GetAll());
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QuestionDTO question)
        {
            return CreateActionResultInstance(await _questionService.Update(question));
        }
        
        [HttpPut("LikeQuestion")]
        public async Task<IActionResult> LikeQuestion(int questionId)
        {
           return CreateActionResultInstance(await _questionService.LikeQuestion(questionId, _userId));
        }

        /* [HttpGet("GetMyLikedQuestion")]
        public IActionResult GetMyLikesQuestion()
        {
            return CreateActionResultInstance(_questionService.GetMyLikedQuestions(_userId));
        } */

        //Pagination
        [HttpGet("GetByUniversityId")]
        public IActionResult GetQuestionsByUniversityId(int universityId, [FromQuery] PaginationQuery? paginationQuery, bool isUniversityStudent=true)
        {
            if(paginationQuery.PageSize==0 || paginationQuery.PageIndex==0)
                return CreateActionResultInstance(_questionService.GetByUniversityId(universityId));
            return CreateActionResultInstance(_questionService.GetQuestionsByUniversityIdPagination(universityId, paginationQuery, isUniversityStudent));
        }

         [HttpGet("GetQuestionCountsByUniversityId")]
        public IActionResult GetQuestionCountByUniversityId(int universityId, DateTime? dateTime = null)
        {
            if(dateTime!=null)
                dateTime = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
            return CreateActionResultInstance(_questionService.GetQuestionCountByUniversityId(universityId,dateTime));
        }

        //Pagination

        [HttpGet("GetQuestionsByUserId/{userId}")]
        public async Task<IActionResult> GetQuestionsByUserId(string userId, [FromQuery] PaginationQuery? paginationQuery)
        {
            if (paginationQuery.PageSize == 0 || paginationQuery.PageIndex == 0)
                return CreateActionResultInstance(_questionService.GetQuestionsByUserId(userId));
            else
                return CreateActionResultInstance(_questionService.GetQuestionsByUserIdPagination(userId, paginationQuery));
        }
    }
}
