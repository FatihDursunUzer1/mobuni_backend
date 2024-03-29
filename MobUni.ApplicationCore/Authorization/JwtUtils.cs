﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Authorization
{
    public interface IJwtUtils
    {
        public TokenDTO GenerateJwtToken(User user);
        public string? ValidateJwtToken(string token);
        //public RefreshToken GenerateRefreshToken(string ipAddress);
    }
    public class JwtUtils:IJwtUtils
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public JwtUtils(IConfiguration configuration, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public TokenDTO GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretJWTkey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).AddMonths(3);
            var token = new JwtSecurityToken("mobuni",
             "mobuni",
              claims: new Claim[] {new Claim("id",user.Id),new Claim("role",user.UserType.ToString())},
              expires: expires,
              signingCredentials: credentials);

            return new TokenDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expires,
                User=_mapper.Map<UserDTO>(user)
            };
        }

        public string? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["SecretJWTkey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidIssuer= "mobuni",
                    ValidAudience= "mobuni",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                if (_unitOfWork.Users.GetById(userId) != null)
                    return userId;
                else
                    throw (new Exception("User not found"));
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
