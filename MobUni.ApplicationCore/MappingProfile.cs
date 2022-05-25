using System;
using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Entities.UserAggregate;

namespace MobUni.ApplicationCore
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDTO>()
				.ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (int)src.UserType)).ReverseMap();
			CreateMap<Activity, ActivityDTO>().ReverseMap();
			CreateMap<Question, QuestionDTO>().ReverseMap();
			CreateMap<Comment, CommentDTO>().ReverseMap();
			CreateMap<Department, DepartmentDTO>().ReverseMap();
			CreateMap<University, UniversityDTO>().ReverseMap();
			CreateMap<Activity, CreateActivityDTO>().ReverseMap();
			CreateMap<User, CreateUserDTO>()
				.ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (int)src.UserType)).ReverseMap();
			CreateMap<User, LoginUserDTO>().ReverseMap();
			CreateMap<CreateQuestionDTO, Question>().ReverseMap();
			CreateMap<CreateUniversityDTO, University>().ReverseMap();
			CreateMap<CreateQuestionCommentDTO, QuestionComment>().ReverseMap();
			CreateMap<QuestionCommentDTO, QuestionComment>().ReverseMap();
			CreateMap<TokenDTO, Token>().ReverseMap();
			CreateMap<LikeQuestionDTO, LikeQuestion>().ReverseMap();
			CreateMap<LikeDTO, LikeQuestion>().ReverseMap();
			CreateMap<ActivityCategory, ActivityCategoryDTO>().ReverseMap();
			CreateMap<ActivityParticipant,ActivityParticipantDTO>().ReverseMap();
			CreateMap<CreateActivityParticipantDTO, ActivityParticipant>().ReverseMap();
		}
	}
}

