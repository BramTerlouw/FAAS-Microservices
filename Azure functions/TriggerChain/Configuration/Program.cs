using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TriggerChain.Models;
using TriggerChain.Services;
using TriggerChain.Services.Interfaces;

var host = new HostBuilder()
      .ConfigureAppConfiguration(configurationBuilder =>
      {
      })
      .ConfigureFunctionsWorkerDefaults()
      .ConfigureServices(services =>
      {
          services.AddSingleton<IQueueService, QueueService>();
          services.AddTransient<ICosmosDbService<Product>, ProductService>();
          services.AddTransient<ITableStorageService<Order>, OrderService>();

      })
      .Build();

host.Run();
