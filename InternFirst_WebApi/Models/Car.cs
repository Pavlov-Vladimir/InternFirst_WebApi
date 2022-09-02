namespace InternFirst_WebApi.Models;

public class Car
{    
    public int Id { get; set; }

    [Required]
    public string Model { get; set; } = null!;

    [Required]
    public string Color { get; set; } = null!;

}
