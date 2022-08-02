using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RefreshToken0011.Models.Helpers;
using RefreshToken0011.Models.Interface;
using RefreshToken0011.Models.ModelDatabase;
using RefreshToken0011.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.ASCII.GetBytes(JwtTokenHelper.secureKey)
        ),
            ClockSkew = TimeSpan.Zero,
            ValidateAudience = true,
        };
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AddCors",
        builder =>
        {
            builder.WithOrigins("http://localhost:5196").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtTokenHelper>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AddCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
