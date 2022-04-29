using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Result.Concrete
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message,int statusCode) : base(message,true,statusCode)
        {

        }
        public SuccessResult() : base(true,200)
        {

        }

        public SuccessResult(int statusCode):base(true,statusCode)
        {

        }
    }
}
