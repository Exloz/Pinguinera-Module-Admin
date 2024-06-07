using System.Text;
using cotizaciones.pinguinera.project.Models.Factories;
using cotizaciones.pinguinera.project.Models.Factories.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pinguinera_final_module.Database;
using pinguinera_final_module.Database.Interfaces;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Repositories;
using pinguinera_final_module.Models.Repositories.Interfaces;
using pinguinera_final_module.Services;
using pinguinera_final_module.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Pingüinera Admin API",
        Description = "ASP.NET 8.0 Core Web API for managing admin of pinguinera"
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<Database>(options => options.UseNpgsql(builder.Configuration["SQLConnectionString"]));
builder.Services.AddScoped<IDatabase, Database>();
builder.Services.AddScoped<ISupplierItemRepository, SupplierItemRepository>();
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IQuoteFactory, QuoteFactory>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISupplierItemService, SupplierItemService>();
builder.Services.AddTransient<IQuoteService, QuoteService>();
builder.Services.AddScoped<IValidator<UserUpdateDTO>, UserUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<UserRequestDTO>, UserRequestDTOValidator>();

builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = signinKey
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();


if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();