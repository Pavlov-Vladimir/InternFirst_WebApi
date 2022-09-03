namespace InternFirst_WebApi.ActionFilters;

public class CheckCarExistsAttribute : Attribute, IActionFilter
{
    private readonly IGarage _garage;

    public CheckCarExistsAttribute(IGarage garage)
    {
        _garage = garage;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        int id = 0;
        if (context.ActionArguments.ContainsKey("id"))
        {
            id = (int)context.ActionArguments["id"]!;
        }
        else
        {
            context.Result = new BadRequestObjectResult("Bad id parameter");
            return;
        }

        var car = _garage.CarsList.SingleOrDefault(x => x.Id == id);
        if (car == null)
        {
            context.Result = new NotFoundResult();
        }
        else
        {
            context.HttpContext.Items.Add("car", car);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
