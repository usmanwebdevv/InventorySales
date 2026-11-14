using FluentValidation;
using FluentValidation.AspNetCore;
using InventorySales.Api.Middleware;
using InventorySales.Application.Interfaces;
using InventorySales.Application.Validators;
using InventorySales.Infrastructure.Data;
using InventorySales.Infrastructure.Repositories;
using InventorySales.Api.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// 2. Dependency Injection (Repository)
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// 3. Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Add FlunetValidaions
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

var app = builder.Build();

//Gobal ExpectationHandle
app.UseMiddleware<GlobalExceptionHandler>();

// 4. Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();