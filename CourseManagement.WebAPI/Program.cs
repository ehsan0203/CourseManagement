using CourseManagement.Application.Interfaces;
using CourseManagement.Application.Services;
using CourseManagement.Core.Interfaces;
using CourseManagement.Infrastructure.Data;
using CourseManagement.Infrastructure.Repositories;
using CourseManagement.WebAPI.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//اتصال به sql server
builder.Services.AddDbContext<AppDbContext>(option =>
{
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//DI برای Repository
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseDtoValidator>();
builder.Services.AddEndpointsApiExplorer(); // برای Swagger
builder.Services.AddSwaggerGen();           // فعال‌سازی Swagger
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAngularClient");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
