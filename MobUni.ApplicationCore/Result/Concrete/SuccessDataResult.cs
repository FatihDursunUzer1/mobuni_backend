using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Result.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(string message, T data,int statusCode) : base(message, true, data,statusCode)
        {

        }
        public SuccessDataResult(T data) : base("Success",true, data,200)
        {
            
        }
        public SuccessDataResult(string message,int statusCode) : base(message, true, default,statusCode)
        {//data degeri vermek istemediğimiz zaman default kullanabiliriz

        }
        public SuccessDataResult() : base(true, default,200)
        { }
    }
}
