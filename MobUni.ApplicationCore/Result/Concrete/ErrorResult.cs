using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Result.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message,int statusCode) : base(message, false,statusCode)
        {
        }

        public ErrorResult():base(false,400)
        {

        }
        public ErrorResult(int statusCode):base(false,statusCode)
        { }
    }
}
