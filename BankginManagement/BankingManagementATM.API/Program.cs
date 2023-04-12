using BankingManagement.Application.Infrastructure.Extensions;
using BankingManagement.Infrastucture.Infrastructure.Extensions;
using BankingManagementATM.API.Infrastucture.Extensions;
using BankingManagementATM.API.Infrastucture.Logger;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSeriLog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Configuration =>
{
    Configuration.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Banking Management ATM.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Email = "avtukalaz@gmail.com",
            Name = "Avtandil Lazishvili",
            Url = new Uri("https://www.facebook.com/avtukalaz")
        },
        Description = "API for ATM"
    });

    Configuration.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Provide your Bearer token here",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    Configuration.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        }, Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    Configuration.IncludeXmlComments(xmlPath);
});

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();
app.UseCustomMiddlewares();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Banking API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
Log.CloseAndFlush();