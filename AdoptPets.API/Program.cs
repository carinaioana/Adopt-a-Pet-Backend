using AdoptPets.Application;
using AdoptPets.API.Services;
using AdoptPets.Application.Contracts.Interfaces;
using AdoptPets.Application.Models;
using AdoptPets.Infrastructure;
using Identity;
using Microsoft.OpenApi.Models;
using AdoptPets.API.Utility;
using AdoptPets.Application.Persistence;
using Amazon.S3;
using S3Service = AdoptPets.Infrastructure.Repositories.S3Service;
using Identity.Services;
using Aspose.Cells.Charts;


var builder = WebApplication.CreateBuilder(args);

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Register services
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddHttpClient<ImageSimilarityService>();

// Add infrastructure and identity services to DI
builder.Services.AddInfrastrutureIdentityToDI(builder.Configuration);
builder.Services.AddInfrastructureToDI(builder.Configuration);

// Add application services
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IUserRepository, ApplicationUserRepository>();
// AWS S3 configuration
builder.Services.AddAWSService<IAmazonS3>();

// Register S3Service
builder.Services.AddTransient<IS3Service, S3Service>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Adopt a Pet API",
    });

    c.OperationFilter<FileResultContentTypeOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("Open");

// Use HTTPS redirection
app.UseHttpsRedirection();

// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();