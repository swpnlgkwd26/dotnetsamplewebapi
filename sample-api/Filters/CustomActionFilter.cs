using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_api.Filters
{
    public class CustomActionFilter :ActionFilterAttribute
    {
        // When the execution is in progress
        public override void OnActionExecuting(ActionExecutingContext context)
        {

          //  var data = context.ActionArguments["productBindingTarget"];

            var param = context.ActionArguments.SingleOrDefault();
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Model Cant not be null");              
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("Model is Invalid");               
            }
        }

        // When the execution is completed
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        // Gets Executed Before an Action Method


    }
}
