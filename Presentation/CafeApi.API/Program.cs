using CafeApi.Application.Interfaces;
using CafeApi.Application.Mapping;
using CafeApi.Application.Services.Abstract;
using CafeApi.Application.Services.Concrete;
using CafeApi.Application.Validators.Category;
using CafeApi.Application.Validators.MenuItem;
using CafeApi.Application.Validators.Order;
using CafeApi.Application.Validators.OrderItem;
using CafeApi.Application.Validators.Table;
using CafeApi.Persistence.Context;
using CafeApi.Persistence.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddScoped<IOrderItemServices, OrderItemService>();
builder.Services.AddScoped<IMenuItemServices,MenuItemServices>();
builder.Services.AddScoped<ICategoryServices,CategoryServices>();
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();



builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddTableValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddOrderItemValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddOrderValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderItemValidator>();




builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapScalarApiReference(
    opt =>
    {
        opt.Title = "CafeApi";
        opt.Theme = ScalarTheme.BluePlanet;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
