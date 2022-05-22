using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SC.SenseTower.Common.Exceptions;
using System.Net;

namespace SC.SenseTower.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CommonExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ScException || context.Exception is ValidationException)
            {
                var message = context.Exception is ValidationException
                    ? $"Validation error: {context.Exception.Message}"
                    : context.Exception.Message;
                var model = new ModelError(message);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(model);
                context.ExceptionHandled = true;
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
