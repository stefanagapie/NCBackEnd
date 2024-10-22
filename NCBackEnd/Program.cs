using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NCBackEnd.Controllers;
using NCBackEnd.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

builder.Services.AddControllers();
// configure formats: https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-8.0

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Random User Statistics API", 
        Description = """
A simple API that computes the following percentages from the given user data:
1. Percentage of gender in each category
2. Percentage of first names that start with A-M versus N-Z
3. Percentage of last names that start with A-M versus N-Z
4. Percentage of people in each state, up to the top 10 most populous states
5. Percentage of females in each state, up to the top 10 most populous states
6. Percentage of males in each state, up to the top 10 most populous states
7. Percentage of people in the following age ranges: 0-20, 21-40, 41-60, 61-80, 81-100, 100+
""",
    });

    // Tell swagger where to load the XML file
    var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var filepath = Path.Combine(AppContext.BaseDirectory, filename);
    options.IncludeXmlComments(filepath);
});

// Style customization
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI");

        // Set to empty to serve the Swagger UI at the app's root
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
