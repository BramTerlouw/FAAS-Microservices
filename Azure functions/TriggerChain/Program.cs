using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Drawing.Text;
using TriggerChain.Services;

var host = new HostBuilder()
      .ConfigureAppConfiguration(configurationBuilder =>
      {
      })
      .ConfigureFunctionsWorkerDefaults()
      .ConfigureServices(services =>
      {
          services.AddTransient<IQueueService, QueueService>();
          //services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
      })
      .Build();

host.Run();

//private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
//{
//    var databaseName = configurationSection["DatabaseName"];
//    var containerName = configurationSection["ContainerName"];
//    var account = configurationSection["Account"];
//    var key = configurationSection["key"];

//    var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
//    var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
//    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

//    var cosmosDbService = new CosmosDbService(client, databaseName, containerName);
//    return cosmosDbService;
//}
