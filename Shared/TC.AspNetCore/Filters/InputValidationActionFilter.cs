using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TC.AspNetCore.Filters;


internal class InputValidationErrorMessage
{
    public string Field { get; set; }
    public string[] Errors { get; set; }
}

public class InputValidationActionFilter : IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // we can even *still* use model state properly…
        if (context.ModelState.IsValid) return;
        
        var responseObj = new
        {
            message = "The input is not valid",
            fileds = context.ModelState.Keys
                .Where(i => context.ModelState[i]?.Errors?.Any() == true)
                .Select(i => new InputValidationErrorMessage
                {
                    Field = i,
                    Errors = context.ModelState[i]?.Errors.Select(i => i.ErrorMessage).ToArray()
                })
        };

        // setting the result shortcuts the pipeline, so the action is never executed
        context.Result = new JsonResult(responseObj)
        {
            StatusCode = 400
        };
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}