using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.WebAPI
{
    public class ActionFilter: IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           //TODO:
        }
    }
}
