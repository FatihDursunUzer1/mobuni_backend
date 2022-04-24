using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace MobUni.WebAPI
{
    public class ActionFilterTest : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //TODO:
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           //TODO:
        }
    }
}
