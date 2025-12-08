using ActivitiesAndTasksAPI.Data;
using ActivitiesAndTasksAPI.Interfaces;
using ActivitiesAndTasksAPI.Models;
using ActivitiesAndTasksAPI.Repositories;
using Microsoft.EntityFrameworkCore;
// removed Microsoft.OpenApi.Models to avoid missing package issues
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ActivitiesAndTasksAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy
			.AllowAnyOrigin()      // Allow all domains
			.AllowAnyHeader()      // Allow all headers
			.AllowAnyMethod();     // Allow GET, POST, PUT, DELETE, OPTIONS, etc.
	});
});


// DbContext (SQL Server)
var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Repos & Services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();
builder.Services.AddScoped<UserModel>();
builder.Services.AddScoped<AuthModel>();

// Bind Jwt settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddTransient<JwtModel>();

// Configure Authentication - JWT Bearer
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>() ?? new JwtSettings();
var key = Encoding.UTF8.GetBytes(jwtSettings.Key ?? string.Empty);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ✅ Swagger services
builder.Services.AddEndpointsApiExplorer();
// Use default AddSwaggerGen without referencing Microsoft.OpenApi types to avoid build-time package issues
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
//}



app.UseHttpsRedirection();

// Use authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

