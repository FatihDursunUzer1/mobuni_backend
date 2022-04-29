﻿using Microsoft.IdentityModel.Tokens;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Entities.UserAggregate;
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
        public Token GenerateJwtToken(User user);
        public string? ValidateJwtToken(string token);
        //public RefreshToken GenerateRefreshToken(string ipAddress);
    }
    public class JwtUtils:IJwtUtils
    {
        //private readonly AppSettings _appSettings;
        
        public JwtUtils()
        {

        }
        public Token GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MobUniMobilProgramxxxs"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMonths(3);
            var token = new JwtSecurityToken("mobuni",
             "mobuni",
              claims: new Claim[] {new Claim("id",user.Id)},
              expires: expires,
              signingCredentials: credentials);

            return new Token
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expires,
            };
        }

        public string? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MobUniMobilProgramxxxs");
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

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}