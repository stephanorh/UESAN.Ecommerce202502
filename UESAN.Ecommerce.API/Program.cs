using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UESAN.Ecommerce.CORE.Core.Interfaces;
using UESAN.Ecommerce.CORE.Core.Services;
using UESAN.Ecommerce.CORE.Infrastructure.Data;
using UESAN.Ecommerce.CORE.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var _configuration = builder.Configuration;
var connectionString = _configuration.GetConnectionString("DevConnection");

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();


builder.Services.AddDbContext<StoreDbContext>(
    options => options.UseSqlServer(connectionString));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
