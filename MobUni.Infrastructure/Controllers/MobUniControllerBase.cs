using Microsoft.AspNetCore.Mvc;
using MobUni.ApplicationCore.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Controllers
{
    public class MobUniControllerBase:ControllerBase
    {

        public IActionResult CreateActionResultInstance<T>(IDataResult<T> result)
        {
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
    }
}
