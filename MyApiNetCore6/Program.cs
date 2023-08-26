using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Repositories;
using MyApiNetCore6.Services;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add COSR
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//Add Identity
//builder.Services.AddIdentity<AplicationUser, IdentityRole>().AddEntityFrameworkStores<BookDbContext>().AddDefaultTokenProviders();

// Add Connection String
builder.Services.AddDbContext<BookDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("BookStore")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add Scoped/ Life cycle DI
builder.Services.AddScoped<IBookRepository, BookRepository>();

//Map object AppSettings -> AppSetting 
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("Appsettings"));

//DI LoaiRepository
builder.Services.AddScoped<ILoaiRepository, LoaiRepository>();

//DI HangHoaRepository
builder.Services.AddScoped<IHangHoaRepository, HangHoaRepository>();

//Add Authorization
builder.Services.AddAuthorization();

//Khai báo Secret Key
var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

//Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(otp =>
    {
        otp.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            //Tự cấp Token - Có thể sử dụng 0Auth -> SSO để cấp tự động
            ValidateIssuer = false,   //Xác thực nhà phát hành = false
            ValidateAudience = false, //Xác thực đối tượng = false

            //Ký vào Token
            ValidateIssuerSigningKey = true,
            // Tạo mới SecretKey đối xứng được mã hóa
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            ClockSkew = TimeSpan.Zero
    };
    }
    );


var app = builder.Build();

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
