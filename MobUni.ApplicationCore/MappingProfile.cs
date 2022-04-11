﻿using System;
using AutoMapper;
using MobUni.ApplicationCore.DTOs;
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
		}
	}
}

