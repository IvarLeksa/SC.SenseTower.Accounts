using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SC.SenseTower.Common.Exceptions;

namespace SC.SenseTower.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CommonExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ScException exception)
            {
                var model = new ModelError(exception, exception.Message);
                context.Result = new JsonResult(model);
            }
            else
                throw context.Exception;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            await Task.CompletedTask;
        }
    }
}
