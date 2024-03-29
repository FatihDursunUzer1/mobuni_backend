﻿
using MobUni.ApplicationCore.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Result.Concrete
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string? Message { get;  }
        public int? StatusCode { get; }

        public Result(string message,bool success=true,int statusCode=200)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }
        public Result(bool success,int statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }

    }
}
