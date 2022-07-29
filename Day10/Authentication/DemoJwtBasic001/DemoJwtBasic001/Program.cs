using DemoJwtBasic001.Models.Data;
using DemoJwtBasic001.Models.Interface;
using DemoJwtBasic001.Models.JwtHelper;
using DemoJwtBasic001.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DBContext
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(

    builder.Configuration.GetConnectionString("DefaultConnection")

));

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AddCorsUse",

            builder =>
            {
                builder.WithOrigins("http://localhost:5129").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            }

        );
});

//Add Dependency Injection 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtTokenHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Use Cors
app.UseCors("AddCorsUse");

app.UseAuthorization();

app.MapControllers();

app.Run();
