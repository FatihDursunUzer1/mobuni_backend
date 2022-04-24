using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Result.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(string message, T data) : base(message, true, data)
        {

        }
        public SuccessDataResult(T data) : base("Success",true, data)
        {
            
        }
        public SuccessDataResult(string message) : base(message, true, default)
        {//data degeri vermek istemediğimiz zaman default kullanabiliriz

        }
        public SuccessDataResult() : base(true, default)
        { }
    }
}
