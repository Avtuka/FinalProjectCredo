using BankingManagement.Application.Infrastructure.Extensions;
using BankingManagement.Infrastucture.Infrastructure.Extensions;
using BankingManagement.InsideSystem.API.Infrastucture.Auth;
using BankingManagement.InsideSystem.API.Infrastucture.Extensions;
using BankingManagement.InsideSystem.API.Infrastucture.Logger;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSeriLog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Configuration =>
{
    Configuration.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inside System Management.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Email = "avtukalaz@gmail.com",
            Name = "Avtandil Lazishvili",
            Url = new Uri("https://www.facebook.com/avtukalaz")
        },
        Description = "API for Credo Bank"
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
});

builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection(nameof(JWTConfiguration)));
builder.Services.AddTokenAuthentication(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

app.UseCustomMiddlewares();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();