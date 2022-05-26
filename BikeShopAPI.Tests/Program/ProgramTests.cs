namespace BikeShopAPI.Tests.Program
{
    public class ProgramTests : IClassFixture<WebApplicationFactory<global::Program>>
    {
        private readonly List<Type> _controllerTypes;
        private readonly WebApplicationFactory<global::Program> _factory;

        public ProgramTests(WebApplicationFactory<global::Program> factory)
        {
            _controllerTypes = typeof(global::Program)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
                .ToList();
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        _controllerTypes.ForEach(c => services.AddScoped(c));
                    });
                });
        }
        [Fact]
        public void ConfigureServies_ForControllers_RegistersAllDependencies()
        {
            // arrange
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            // assert
            _controllerTypes.ForEach(t =>
            {
                var controller = scope.ServiceProvider.GetService(t);
                controller.Should().NotBeNull();
            });
        }
    }
}
