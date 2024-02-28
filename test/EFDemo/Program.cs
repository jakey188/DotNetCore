using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.EntityFrameworkCore;
using EFDemo;
using DotNetCore;
using EFDemo.Services.Impl;
using DotNetCore.Data;

var builder = WebApplication.CreateBuilder(args);

// 自动注入服务
builder.Services.AddAutoDependencyInjection();


//BookDbContext默认注册第一个,仓储无需指定BookDbContext
builder.Services.AddRepository<BookDbContext>(ops => ops.UseMySql(builder.Configuration["BookDbConnectionString"],ServerVersion.AutoDetect(builder.Configuration["BookDbConnectionString"])));
builder.Services.AddRepository<UserDbContext>(ops => ops.UseMySql(builder.Configuration["UserDbConnectionString"], ServerVersion.AutoDetect(builder.Configuration["UserDbConnectionString"])));
builder.Services.AddRepository<UserReadonlyDbContext>(ops => ops.UseMySql(builder.Configuration["UserReadonlyConnectionString"], ServerVersion.AutoDetect(builder.Configuration["UserReadonlyConnectionString"])));
builder.Services.AddRepository<UserBookDbContext>(ops => ops.UseMySql(builder.Configuration["UserBookConnectionString"], ServerVersion.AutoDetect(builder.Configuration["UserBookConnectionString"])));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// 初始化sqllite数据库
app.InitSeedData();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
