
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

        public Result(string message,bool success=true)
        {
            Success = success;
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }

    }
}
