using AzureFuncWithDependencyInj.DAL;
using AzureFuncWithDependencyInj.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
      .ConfigureAppConfiguration(configurationBuilder =>
      {
      })
      .ConfigureFunctionsWorkerDefaults()
      .ConfigureServices(services =>
      {
          services.AddTransient<IProductService, ProductService>();
          services.AddTransient<IFakeProductDB, FakeProductsDB>();
      })
            .Build();

host.Run();
