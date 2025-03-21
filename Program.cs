using BookFinderAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using BookFinderAPI.Interfaces;
using BookFinderAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BookFinder API",
        Version = "v1",
        Description = "An API for finding books"
    });
     
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {your JWT token}' below. Example: Bearer eyJhbGciOiJIUz...",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
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
                }
            },
            new string[] {}
        }
    });
});
// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
        };
    });

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IGenerateJwtToken, GenerateJwtToken>();

builder.Services.AddAuthorization();

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookFinder API v1");
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
 
app.MapControllers();

app.Run();
