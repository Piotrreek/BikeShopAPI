using System.Reflection;
using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Others;
using BikeShopAPI.Services;
using BikeShopAPI.Middleware;
using BikeShopAPI.Models;
using BikeShopAPI.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

// dodac ProductController, autentykacja, autoryzacja, zamawianie, koszyk na zakupy, e-mail service
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IValidator<CreateBikeShopDto>, CreateBikeShopDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateBikeShopDto>, UpdateBikeShopDtoValidator>();
builder.Services.AddScoped<IValidator<CreateBikeDto>, CreateBikeDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateBikeDto>, UpdateBikeDtoValidator>();
builder.Services.AddScoped<IValidator<CreateSpecificationDto>, CreateSpecificationDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateSpecificationDto>, UpdateSpecificationDtoValidator>();


builder.Services.AddDbContext<BikeShopDbContext>();
builder.Services.AddScoped<BikeShopSeeder>();
builder.Services.AddScoped<IBikeShopService, BikeShopService>();
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<ISpecificationService, SpecificationService>();
builder.Services.AddScoped<IBagService, BagService>();
builder.Services.AddScoped<IProductService, ProductService>();



var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<BikeShopSeeder>();

seeder.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
