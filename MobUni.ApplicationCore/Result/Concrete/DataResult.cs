using MobUni.ApplicationCore.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Result.Concrete
{
    public class DataResult<T> :Result, IDataResult<T>
    {
        public T Data { get; set; }

        public DataResult(string message, bool success, T data,int statusCode):base(message,success,statusCode:statusCode)
        {
            Data = data;
        }
        public DataResult(bool success,T data,int statusCode):base(success,statusCode)
        {
            Data=data;
        }
    }
}
