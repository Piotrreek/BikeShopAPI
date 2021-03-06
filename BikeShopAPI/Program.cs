using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using BikeShopAPI.Authorization;
using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Others;
using BikeShopAPI.Services;
using BikeShopAPI.Middleware;
using BikeShopAPI.Models;
using BikeShopAPI.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

var client = new SmtpClient("smtp.gmail.com", 587)
{
    Credentials = new NetworkCredential("adres@gmail.com", "password"),
    EnableSsl = true
};


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddFluentEmail("adres@gmail.com", "Bike Shop")
    .AddSmtpSender(client);
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<CreateBikeShopDto>, CreateBikeShopDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateBikeShopDto>, UpdateBikeShopDtoValidator>();
builder.Services.AddScoped<IValidator<CreateBikeDto>, CreateBikeDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateBikeDto>, UpdateBikeDtoValidator>();
builder.Services.AddScoped<IValidator<CreateSpecificationDto>, CreateSpecificationDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateSpecificationDto>, UpdateSpecificationDtoValidator>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<BuyNowDto>, BuyNowDtoValidator>();


builder.Services.AddScoped<IAuthorizationHandler, UserServiceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, BikeShopServiceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, BikeServiceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, BagServiceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ProductServiceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, BasketServiceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, OrderServiceOperationRequirementHandler>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddDbContext<BikeShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BikeShopDbConnection")));
builder.Services.AddScoped<BikeShopSeeder>();
builder.Services.AddScoped<IBikeShopService, BikeShopService>();
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<ISpecificationService, SpecificationService>();
builder.Services.AddScoped<IBagService, BagService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderBikeService, OrderBikeService>();
builder.Services.AddScoped<IOrderBikeService, OrderBikeService>();
builder.Services.AddScoped<IOrderBagService, OrderBagService>();
builder.Services.AddScoped<IOrderProductService, OrderProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();




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
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program {}