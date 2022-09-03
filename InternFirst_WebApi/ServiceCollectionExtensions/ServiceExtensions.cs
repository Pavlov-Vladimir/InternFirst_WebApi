namespace InternFirst_WebApi.ServiceCollectionExtensions;

public static class ServiceExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            config.IncludeXmlComments(xmlPath);
            config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Garage API", Version = "v1" });
        });
    }
}
