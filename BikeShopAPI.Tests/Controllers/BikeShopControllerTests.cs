using System.Net;
using System.Runtime.InteropServices;
using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using BikeShopAPI.Tests.Helpers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BikeShopAPI.Tests.Controllers
{
    public class BikeShopControllerTests : IClassFixture<WebApplicationFactory<global::Program>>
    {
        private readonly WebApplicationFactory<global::Program> _factory;
        private readonly HttpClient _client;
        public BikeShopControllerTests(WebApplicationFactory<global::Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service =>
                        service.ServiceType == typeof(DbContextOptions<BikeShopDbContext>));
                    services.Remove(dbContextOptions);
                    services.AddDbContext<BikeShopDbContext>(options => options.UseInMemoryDatabase("BikeShopDb"));
                    services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            });
            _client = _factory.CreateClient();
        }
        private void SeedBikeShop(BikeShop shop)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<BikeShopDbContext>();
            _dbContext.BikeShops.Add(shop);
            _dbContext.SaveChanges();
        }
        private static IEnumerable<object[]> GetShopModels()
        {
            var list = new List<string>()
            {
               "name",
               "name1",
               "name2",
               "name3"
            };
            return list.Select(q => new object[] { q });
        }
        [Theory]
        [MemberData(nameof(GetShopModels))]
        public async Task GetShopById_WithValidShopId_ReturnsOkResult(string name)
        {
            // arrange
            var shop = new BikeShop()
            {
                Name = name
            };
            SeedBikeShop(shop);
            // act
            var response =  await _client.GetAsync($"shop/{shop.Id}");
            // assert   
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Theory]
        [InlineData(589)]
        [InlineData(10)]
        [InlineData(17)]
        [InlineData(50)]
        [InlineData(99)]
        public async Task GetShopById_WithInvalidShopId_ReturnsNotFound(int id)
        {
            // act
            var response = await _client.GetAsync($"shop/{id}");
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task GetAllShops_WithValidQueryParams_ReturnOkResult()
        {
            // act
            var response = await _client.GetAsync("shop");
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task CreateBikeShop_WithValidModel_ReturnsCreatedStatus()
        {
            // arrange
            var bikeShopModel = new CreateBikeShopDto()
            {
                Name = "name",
                City = "city",
                Street = "street",
                PostalCode = "00-000",
                ContactEmail = "mail@mail.com",
                ContactNumber = "000000000"
            };
            var httpContent = bikeShopModel.ToJsonHttpContent();
            // act
            var response = await _client.PostAsync("shop", httpContent);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }
        private static IEnumerable<object[]> GetInvalidCreateBikeShopDtoModels()
        {
            var list = new List<CreateBikeShopDto>()
            {
                new CreateBikeShopDto()
                {
                    Name = "",
                    City = "city",
                    Street = "street",
                    PostalCode = "00-000",
                    ContactEmail = "mail@mail.com",
                    ContactNumber = "000000000"
                },
                new CreateBikeShopDto()
                {
                    Name = "nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn",
                    City = "city",
                    Street = "g",
                    PostalCode = "00-000",
                    ContactEmail = "mail@mail.com",
                    ContactNumber = "0000g00000"
                },
                new CreateBikeShopDto()
                {
                    Name = "fsdfdf",
                    City = "city",
                    Street = "",
                    PostalCode = "00-000",
                    ContactEmail = "mailmail.com",
                    ContactNumber = "000000000"
                },
                new CreateBikeShopDto()
                {
                    Name = "",
                    City = "city",
                    Street = "street",
                    PostalCode = "00-000",
                    ContactEmail = "",
                    ContactNumber = ""
                },
            };
            return list.Select(q => new object[] { q });
        }
        [Theory]
        [MemberData(nameof(GetInvalidCreateBikeShopDtoModels))]
        public async Task CreateBikeShop_WithInvalidModel_ReturnsBadRequest(CreateBikeShopDto model)
        {
            var httpContent = model.ToJsonHttpContent();
            // act
            var response = await _client.PostAsync("shop", httpContent);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteBikeShop_ForBikeShopOwner_ReturnsNoContent()
        {
            // arrange
            var bikeShop = new BikeShop()
            {
                CreatedById = 1,
                Name = "name"
            };
            SeedBikeShop(bikeShop);
            // act
            var response = await _client.DeleteAsync($"shop/{bikeShop.Id}");
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteBikeShop_ForNonBikeShopOwner_ReturnsForbidden()
        {
            // arrange
            var bikeShop = new BikeShop()
            {
                CreatedById = 5,
                Name = "name"
            };
            SeedBikeShop(bikeShop);
            // act
            var response = await _client.DeleteAsync($"shop/{bikeShop.Id}");
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task DeleteBikeShop_ForNonExistingBikeShop_ReturnsNotFound()
        {
            // act
            var response = await _client.DeleteAsync($"shop/{24}");
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateBikeShop_ForBikeShopOwner_ReturnsOkResult()
        {
            // arrange
            var bikeShop = new BikeShop()
            {
                CreatedById = 1,
                Name = "name"
            };
            var updateBikeShop = new UpdateBikeShopDto()
            {
                Name = "namee",
                ContactEmail = "email@email.com",
                ContactNumber = "000000000"
            };
            SeedBikeShop(bikeShop);
            var httpContent = updateBikeShop.ToJsonHttpContent();
            // act
            var response = await _client.PatchAsync($"shop/{bikeShop.Id}", httpContent);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task UpdateBikeShop_ForNonBikeShopOwner_ReturnsForbidden()
        {
            // arrange
            var bikeShop = new BikeShop()
            {
                CreatedById = 5,
                Name = "name"
            };
            var updateBikeShop = new UpdateBikeShopDto()
            {
                Name = "nameee",
                ContactEmail = "email@email.com",
                ContactNumber = "000000000"
            };
            SeedBikeShop(bikeShop);
            var httpContent = updateBikeShop.ToJsonHttpContent();
            // act
            var response = await _client.PatchAsync($"shop/{bikeShop.Id}", httpContent);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        [Fact]
        public async Task UpdateBikeShop_InvalidDataModel_ReturnsBadRequest()
        {
            // arrange
            var bikeShop = new BikeShop()
            {
                CreatedById = 1,
                Name = "name"
            };
            var updateBikeShop = new UpdateBikeShopDto()
            {
                Name = "",
                ContactEmail = "emailemail.com",
                ContactNumber = "000000000"
            };
            SeedBikeShop(bikeShop);
            var httpContent = updateBikeShop.ToJsonHttpContent();
            // act
            var response = await _client.PatchAsync($"shop/{bikeShop.Id}", httpContent);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task UpdateBikeShop_ForNonExistingBikeShop_ReturnsNotFound()
        {
            // arrange
            var updateBikeShop = new UpdateBikeShopDto()
            {
                Name = "nameee",
                ContactEmail = "email@email.com",
                ContactNumber = "000000000"
            };
            var httpContent = updateBikeShop.ToJsonHttpContent();
            // act
            var response = await _client.PatchAsync($"shop/100", httpContent);
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
