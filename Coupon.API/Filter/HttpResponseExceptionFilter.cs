using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coupon.API.Filter
{
    public  class HttpResponseExceptionFilter :  IActionFilter
    {
        public static async  Task<HttpResponseExceptionFilter> Create()
         => await Task.FromResult(new HttpResponseExceptionFilter());
        

        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid)
                return;


            var messageErro = context.ModelState.Values
                   .SelectMany(x => x.Errors)
                   .Select(x => x.ErrorMessage).ToList();

            context.Result = new BadRequestObjectResult(messageErro);
        }
    }
}
