using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IGarage, Garage>();


builder.Services.AddSwaggerGen(config =>
{
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
    //config.IncludeXmlComments(GetXmlCommentsPath());
    config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Garage API", Version = "v1" });
});

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
