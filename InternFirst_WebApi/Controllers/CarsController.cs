namespace InternFirst_WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly IGarage _garage;

    public CarsController(IGarage garage)
    {
        _garage = garage;
    }

    /// <summary>
    /// Get the list of cars
    /// </summary>
    /// <remarks>
    /// Request sample:
    /// GET api/cars
    /// </remarks>
    /// <returns>Returns List&lt;Car&gt;}</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    public ActionResult<List<Car>> GetAll()
    {
        return Ok(_garage.CarsList);
    }

    /// <summary>
    /// Get car by id
    /// </summary>
    /// <remarks>
    /// Request sample:
    /// GET api/cars/1
    /// </remarks>
    /// <param name="id">Car Id (int)</param>
    /// <returns>Returns Car</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If the car with such id is not exist</response>
    [HttpGet("{id}")]
    [ServiceFilter(typeof(CheckCarExistsAttribute))]
    public ActionResult<Car> Get(int id)
    {        
        var car = HttpContext.Items["car"] as Car;

        return Ok(car);
    }

    /// <summary>
    /// Delete the car by id from the list
    /// </summary>
    /// <remarks>
    /// Request sample:
    /// DELETE api/cars/1
    /// </remarks>
    /// <param name="id">Car Id (int)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="404">If the car with such id is not exist</response>
    [HttpDelete("{id}")]
    [ServiceFilter(typeof(CheckCarExistsAttribute))]
    public IActionResult Delete(int id)
    {
        var car = HttpContext.Items["car"] as Car;

        _garage.CarsList.Remove(car!);

        return NoContent();
    }

    /// <summary>
    /// Create and add new car into the list
    /// </summary>
    /// <remarks>
    /// Request sample:
    /// POST api/cars?model=tank&amp;color=green
    /// </remarks>
    /// <param name="model">Car model (string) from query string. Required</param>
    /// <param name="color">Car color (string) from query string. Required</param>
    /// <returns>Returns Car</returns>
    /// <response code="201">Success</response>
    [HttpPost]
    public ActionResult<Car> Create([FromQuery] string model, string color)
    {
        Car car = new()
        {
            Id = GetNewId(),
            Model = model,
            Color = color
        };
        _garage.CarsList.Add(car);

        return Created(car.Id.ToString(), car);
    }

    /// <summary>
    /// Update car by id
    /// </summary>
    /// <remarks>
    /// Request sample:
    /// PUT api/cars/1
    /// </remarks>
    /// <param name="id">Car Id (int)</param>
    /// <param name="model">Car model (string) from query string. Not required</param>
    /// <param name="color">Car color (string) from query string. Not required</param>
    /// <returns>Returns Car</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If the car with such id is not exist</response>
    [HttpPut("{id}")]
    [ServiceFilter(typeof(CheckCarExistsAttribute))]
    public ActionResult<Car> Update(int id, [FromQuery] string? model = null, string? color = null)
    {
        var car = HttpContext.Items["car"] as Car;

        UpdateCar(model, color, car);

        return Ok(car);
    }

    private static void UpdateCar(string? model, string? color, Car? car)
    {
        if (car is not null)
        {
            if (!string.IsNullOrEmpty(model))
                car.Model = model;
            if (!string.IsNullOrEmpty(color))
                car.Color = color; 
        }
    }

    private int GetNewId()
    {
        return _garage.CarsList.Max(c => c.Id) + 1;
    }
}
