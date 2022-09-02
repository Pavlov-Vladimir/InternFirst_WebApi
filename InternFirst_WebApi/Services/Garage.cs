namespace InternFirst_WebApi.Services;

public class Garage : IGarage
{
    public List<Car> CarsList { get; set; } = null!;

	public Garage()
	{
		CarsList = new List<Car>()
		{
            new Car() { Id = 1, Model = "tavriya", Color = "white" },
            new Car() { Id = 2, Model = "slavuta", Color = "black" }
        };
	}
}
