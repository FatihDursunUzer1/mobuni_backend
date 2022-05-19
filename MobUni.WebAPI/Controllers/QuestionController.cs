﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetByQuestionId")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            return CreateActionResultInstance(_questionService.GetById(id));
        }
        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _questionService.GetAll());
        }

        [HttpGet("GetMyQuestions")]
        public async Task<IActionResult> GetMyQuestions()
        {
            
            return CreateActionResultInstance(await _questionService.GetMyQuestions(_userId));
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
        [HttpGet("GetByUniversityId")]
        public async Task<IActionResult> GetQuestionsByUniversityId(int universityId)
        {
            return CreateActionResultInstance(await _questionService.GetByUniversityId(universityId));
        }

         [HttpGet("GetQuestionCountsByUniversityId")]
        public async Task<IActionResult> GetQuestionCountByUniversityId(int universityId)
        {
            return CreateActionResultInstance(_questionService.GetQuestionCountByUniversityId(universityId));
        }
    }
}
