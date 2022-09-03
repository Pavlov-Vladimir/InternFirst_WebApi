var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<CheckCarExistsAttribute>();

builder.Services.AddSingleton<IGarage, Garage>();

builder.Services.ConfigureSwagger();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Garage API v1");
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
