using Microsoft.OpenApi.Models;
using NLog;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Application.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// NLog configuration
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));



// Configure Swagger to include Bearer token input
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RealWorld API", Version = "v1", Description = "JWT Authentication" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token in the following format: {your token here} do not add the word 'Bearer' before it."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Application services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


var app = builder.Build();

app.UseCors("CorsPolicy");




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
