using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Result.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(string message, T data,int statusCode) : base(message, false, data,statusCode)
        {
        }
        public ErrorDataResult(T data):base(false,data,400)
        {

        }
        public ErrorDataResult(string message,int statusCode):base(message,false,default,statusCode)
        {

        }
        public ErrorDataResult():base(false,default,400)
        {

        }
    }
}
